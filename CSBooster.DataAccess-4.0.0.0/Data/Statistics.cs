// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Caching;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class Statistics
    {
        public static void UserSessionLoad(Business.StatisticSiteViewResult result, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, List<object> include)
        {
            string key = string.Format("US{0},{1},{2},{3}", from.Ticks, to.Ticks, (int)groupBy, (int)granularity);
            Business.StatisticSiteViewResult work = GetCache(key) as Business.StatisticSiteViewResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();
            string serieLabel = GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticsTypeUserSession");   

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(DateInsert) AS Datum, COUNT(LSE_SessionID) AS Anzahl, ");

            if (groupBy == GroupBy.Sex)
            {
                sql.Append("Sex AS Grp, ");
                sqlGroupBy.Append("Sex, ");
                sqlWhere.Append("AND (Sex IN (");
            }
            else if (groupBy == GroupBy.Age)
            {
                sql.Append("Age AS Grp, ");
                sqlGroupBy.Append("Age, ");
                sqlWhere.Append("AND (Age IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp, ");
            }

            sql.Append("AVG(Age) as AgeAvg");

            sql.AppendLine();
            sql.Append("FROM hitbl_LogSession_LSE");

            sql.AppendLine();
            sql.Append("WHERE (DateInsert BETWEEN @FromDate AND @ToDate)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(DateInsert), MONTH(DateInsert), DAY(DateInsert)");
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(DateInsert), MONTH(DateInsert)");
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(DateInsert)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("Datum, Anzahl DESC");


            result.Series.Clear();
            result.From = DateTime.MaxValue;
            result.To = DateTime.MinValue;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                DateTime lastDate = DateTime.MinValue;
                DateTime date = DateTime.MinValue;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (string.IsNullOrEmpty(group))
                        group = "default";

                    if (lastGroup != group || serie == null)
                    {
                        if (serie != null)
                            AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);

                        lastDate = DateTime.MinValue;
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.None)
                            serie.Label = serieLabel;
                        else if (groupBy == GroupBy.PageType)
                            serie.Label = GetPageTypeText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);
                        else if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group);
                        else if (groupBy == GroupBy.Mobile)
                            serie.Label = string.Format("Browsertype {0}", group);
                        else
                            serie.Label = group;

                        result.Series.Add(serie);

                    }

                    if (granularity == Granularity.Day)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimHour();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfMonth().AddDays(-1);
                    }
                    else if (granularity == Granularity.Month)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimDay();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfYear().AddMonths(-1);
                    }
                    else if (granularity == Granularity.Year)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimMonth();
                    }
                    else
                    {
                        date = DateTime.Now.TrimHour();
                    }

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;

                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    if (lastDate != DateTime.MinValue)
                    {
                        while (GetEmptyPoint(date, ref lastDate, granularity))
                        {
                            if (lastDate < result.From)
                                result.From = lastDate;
                            if (lastDate > result.To)
                                result.To = lastDate;

                            serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = lastDate, Y = 0 });
                        }
                    }


                    serie.Values.Add(item);
                    lastDate = date;
                }
                sqlReader.Close();

                if (serie != null)
                    AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);


                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void UniqueUserLoad(Business.StatisticSiteViewResult result, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, List<object> include)
        {
            string key = string.Format("UU{0},{1},{2},{3}", from.Ticks, to.Ticks, (int)groupBy, (int)granularity);
            Business.StatisticSiteViewResult work = GetCache(key) as Business.StatisticSiteViewResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();
            string serieLabel = GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticsTypeUniqueUser");   

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(SVI_Date) AS Datum, COUNT(DISTINCT SVI_VisitID) AS Anzahl, ");

            if (groupBy == GroupBy.Sex)
            {
                sql.Append("SVI_Sex AS Grp, ");
                sqlGroupBy.Append("SVI_Sex, ");
                sqlWhere.Append("AND (SVI_Sex IN (");
            }
            else if (groupBy == GroupBy.Age)
            {
                sql.Append("SVI_Age AS Grp, ");
                sqlGroupBy.Append("SVI_Age, ");
                sqlWhere.Append("AND (SVI_Age IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp, ");
            }

            sql.Append("AVG(SVI_Age) as AgeAvg");

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticVistit_SVI");

            sql.AppendLine();
            sql.Append("WHERE (SVI_Date BETWEEN @FromDate AND @ToDate)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(SVI_Date), MONTH(SVI_Date), DAY(SVI_Date)");
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(SVI_Date), MONTH(SVI_Date)");
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(SVI_Date)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("Datum, Anzahl DESC");


            result.Series.Clear();
            result.From = DateTime.MaxValue;
            result.To = DateTime.MinValue;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                DateTime lastDate = DateTime.MinValue;
                DateTime date = DateTime.MinValue;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (string.IsNullOrEmpty(group))
                        group = "default";

                    if (lastGroup != group || serie == null)
                    {
                        if (serie != null)
                            AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);

                        lastDate = DateTime.MinValue;
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.None)
                            serie.Label = serieLabel;
                        else if (groupBy == GroupBy.PageType)
                            serie.Label = GetPageTypeText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);
                        else if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group);
                        else if (groupBy == GroupBy.Mobile)
                            serie.Label = string.Format("Browsertype {0}", group);
                        else
                            serie.Label = group;

                        result.Series.Add(serie);

                    }

                    if (granularity == Granularity.Day)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimHour();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfMonth().AddDays(-1);
                    }
                    else if (granularity == Granularity.Month)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimDay();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfYear().AddMonths(-1);
                    }
                    else if (granularity == Granularity.Year)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimMonth();
                    }
                    else
                    {
                        date = DateTime.Now.TrimHour();
                    }

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;

                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    if (lastDate != DateTime.MinValue)
                    {
                        while (GetEmptyPoint(date, ref lastDate, granularity))
                        {
                            if (lastDate < result.From)
                                result.From = lastDate;
                            if (lastDate > result.To)
                                result.To = lastDate;

                            serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = lastDate, Y = 0 });
                        }
                    }


                    serie.Values.Add(item);
                    lastDate = date;
                }
                sqlReader.Close();

                if (serie != null)
                    AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);


                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void SiteViewDayRangeLoad(Business.StatisticSiteViewResult result, DateTime from, DateTime to, GroupBy groupBy, Guid? communityID, List<object> include)
        {
            string key = string.Format("SVDR{0},{1},{2},{3}", from.Ticks, to.Ticks, (int)groupBy, communityID);
            Business.StatisticSiteViewResult work = GetCache(key) as Business.StatisticSiteViewResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();
            string serieLabel = GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticsTypePageView");   

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(SVS_Date) AS Datum, SUM(LSS_Count) AS Anzahl, ");

            if (groupBy == GroupBy.PageType)
            {
                sql.Append("SVS_PageType AS Grp, ");
                sqlGroupBy.Append("SVS_PageType, ");
                sqlWhere.Append("AND (SVS_PageType IN (");
            }
            else if (groupBy == GroupBy.Type)
            {
                sql.Append("OBJ_Type AS Grp, ");
                sqlGroupBy.Append("OBJ_Type, ");
                sqlWhere.Append("AND (OBJ_Type IN (");
            }
            else if (groupBy == GroupBy.Role)
            {
                sql.Append("LSS_Role AS Grp, ");
                sqlGroupBy.Append("LSS_Role, ");
                sqlWhere.Append("AND (LSS_Role IN (");
            }
            else if (groupBy == GroupBy.Mobile)
            {
                sql.Append("SVS_IsMoblileDevice AS Grp, ");
                sqlGroupBy.Append("SVS_IsMoblileDevice, ");
                sqlWhere.Append("AND (SVS_IsMoblileDevice IN (");
            }
            else if (groupBy == GroupBy.Sex)
            {
                sql.Append("SVS_Sex AS Grp, ");
                sqlGroupBy.Append("SVS_Sex, ");
                sqlWhere.Append("AND (SVS_Sex IN (");
            }
            else if (groupBy == GroupBy.Age)
            {
                sql.Append("SVS_Age AS Grp, ");
                sqlGroupBy.Append("SVS_Age, ");
                sqlWhere.Append("AND (SVS_Age IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp, ");
            }


            sql.Append("AVG(SVS_Age) as AgeAvg");

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticSiteView_SVS");

            sql.AppendLine();
            sql.Append("WHERE (SVS_Date BETWEEN @FromDate AND @ToDate)");

            if (communityID.HasValue)
                sql.Append("AND (CTY_ID = @CTY_ID)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            sqlGroupBy.AppendFormat("DATEPART(hour, SVS_Date)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("DATEPART(hour, SVS_Date), Anzahl DESC");


            result.Series.Clear();
            result.From = DateTime.MaxValue;
            result.To = DateTime.MinValue;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);
                if (communityID.HasValue)
                    GetData.Parameters.AddWithValue("@CTY_ID", communityID.Value);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                DateTime lastDate = DateTime.MinValue;
                DateTime date = DateTime.MinValue;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (string.IsNullOrEmpty(group))
                        group = "default";

                    if (lastGroup != group || serie == null)
                    {
                        if (serie != null)
                            AddEndEmptyHour(serie, lastDate, lastDate.Hour + 1 , 25); 

                        lastDate = DateTime.MinValue;
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.None)
                            serie.Label = serieLabel;
                        else if (groupBy == GroupBy.PageType)
                            serie.Label = GetPageTypeText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);
                        else if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group);
                        else if (groupBy == GroupBy.Mobile)
                            serie.Label = string.Format("Browsertype {0}", group);
                        else
                            serie.Label = group;

                        result.Series.Add(serie);

                    }

                    date = Convert.ToDateTime(sqlReader["Datum"]).TrimMinute();

                    if (lastDate == DateTime.MinValue && date.Hour != 0)
                    {
                        AddEndEmptyHour(serie, date, 0, date.Hour);
                    }
                    lastDate = date; 

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;

                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    serie.Values.Add(item);
                    lastDate = date;
                }
                sqlReader.Close();

                if (serie != null)
                    AddEndEmptyHour(serie, lastDate, lastDate.Hour + 1, 24); 

                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void SiteViewLoad(Business.StatisticSiteViewResult result, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            string key = string.Format("SV{0},{1},{2},{3},{4}", from.Ticks, to.Ticks, (int)groupBy, (int)granularity, communityID);
            Business.StatisticSiteViewResult work = GetCache(key) as Business.StatisticSiteViewResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();
            string serieLabel = GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticsTypePageView");   

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(SVS_Date) AS Datum, SUM(LSS_Count) AS Anzahl, ");
            
            if (groupBy == GroupBy.PageType)
            {
                sql.Append("SVS_PageType AS Grp, "); 
                sqlGroupBy.Append("SVS_PageType, ");
                sqlWhere.Append("AND (SVS_PageType IN (");
            }
            else if (groupBy == GroupBy.Type)
            {
                sql.Append("OBJ_Type AS Grp, "); 
                sqlGroupBy.Append("OBJ_Type, ");
                sqlWhere.Append("AND (OBJ_Type IN (");
            }
            else if (groupBy == GroupBy.Role)
            {
                sql.Append("LSS_Role AS Grp, "); 
                sqlGroupBy.Append("LSS_Role, ");
                sqlWhere.Append("AND (LSS_Role IN (");
            }
            else if (groupBy == GroupBy.Mobile)
            {
                sql.Append("SVS_IsMoblileDevice AS Grp, "); 
                sqlGroupBy.Append("SVS_IsMoblileDevice, ");
                sqlWhere.Append("AND (SVS_IsMoblileDevice IN (");
            }
            else if (groupBy == GroupBy.Sex)
            {
                sql.Append("SVS_Sex AS Grp, "); 
                sqlGroupBy.Append("SVS_Sex, ");
                sqlWhere.Append("AND (SVS_Sex IN (");
            }
            else if (groupBy == GroupBy.Age)
            {
                sql.Append("SVS_Age AS Grp, "); 
                sqlGroupBy.Append("SVS_Age, ");
                sqlWhere.Append("AND (SVS_Age IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp, ");
            }


            sql.Append("AVG(SVS_Age) as AgeAvg"); 

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticSiteView_SVS");
  
            sql.AppendLine();
            sql.Append("WHERE (SVS_Date BETWEEN @FromDate AND @ToDate)");

            if (communityID.HasValue)
                sql.Append("AND (CTY_ID = @CTY_ID)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty; 
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma); 
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString()); 

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());    
                }
                else
                { 
                    sql.Append(" AND (1=2)");  
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(SVS_Date), MONTH(SVS_Date), DAY(SVS_Date)"); 
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(SVS_Date), MONTH(SVS_Date)"); 
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(SVS_Date)"); 

            if (sqlGroupBy.Length > 0) 
            {
                if(sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length -2, 2); 

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY "); 
            if (groupBy != GroupBy.None)
                sql.Append("Grp, "); 

            sql.Append("Datum, Anzahl DESC"); 


            result.Series.Clear();
            result.From = DateTime.MaxValue;
            result.To = DateTime.MinValue;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);
                if (communityID.HasValue)
                    GetData.Parameters.AddWithValue("@CTY_ID", communityID.Value);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                DateTime lastDate = DateTime.MinValue;
                DateTime date = DateTime.MinValue;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (string.IsNullOrEmpty(group))
                        group = "default";

                    if (lastGroup != group || serie == null)
                    {
                        if (serie != null)
                            AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);

                        lastDate = DateTime.MinValue;
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.None)
                            serie.Label = serieLabel;
                        else if (groupBy == GroupBy.PageType)
                            serie.Label = GetPageTypeText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);  
                        else if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group); 
                        else if (groupBy == GroupBy.Mobile)
                            serie.Label = string.Format("Browsertype {0}", group);
                        else
                            serie.Label = group;

                        result.Series.Add(serie);

                    }

                    if (granularity == Granularity.Day)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimHour();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfMonth().AddDays(-1);
                    }
                    else if (granularity == Granularity.Month)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimDay();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfYear().AddMonths(-1);
                    }
                    else if (granularity == Granularity.Year)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimMonth();
                    }
                    else
                    {
                        date = DateTime.Now.TrimHour();
                    }

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;

                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    if (lastDate != DateTime.MinValue)
                    {
                        while (GetEmptyPoint(date, ref lastDate, granularity))
                        {
                            if (lastDate < result.From)
                                result.From = lastDate;
                            if (lastDate > result.To)
                                result.To = lastDate;

                            serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = lastDate, Y = 0 });
                        }
                    }


                    serie.Values.Add(item);
                    lastDate = date;
                }
                sqlReader.Close();

                if (serie != null)
                    AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);


                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void ObjectCountLoad(Business.StatisticObjectCountResult result, int recType, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? parentID, List<object> include)
        {
            string key = string.Format("OC{0},{1},{2},{3},{4},{5}", recType, from.Ticks, to.Ticks, (int)groupBy, (int)granularity, parentID);
            Business.StatisticObjectCountResult work = GetCache(key) as Business.StatisticObjectCountResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();
 
            sql.Append("SELECT MAX(SOC_Date) AS Datum, SUM(SOC_Count) AS Anzahl, AVG(SOC_Age) as AgeAvg, ");
            if (groupBy == GroupBy.Age)
            {
                sql.Append("SOC_Age AS Grp");
                sqlGroupBy.Append("SOC_Age, ");
                sqlWhere.Append("AND (SOC_Age IN (");
            }
            else if (groupBy == GroupBy.Sex)
            {
                sql.Append("SOC_Sex AS Grp");
                sqlGroupBy.Append("SOC_Sex, ");
                sqlWhere.Append("AND (SOC_Sex IN (");
            }
            else if (groupBy == GroupBy.Type)
            {
                sql.Append("SOC_Type AS Grp");
                sqlGroupBy.Append("SOC_Type, ");
                sqlWhere.Append("AND (SOC_Sex IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp");
            }

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticObjectCount_SOC");

            sql.AppendLine();
            sql.Append("WHERE (SOC_RecType = @RecType) AND (SOC_Date BETWEEN @FromDate AND @ToDate)");
            if (parentID.HasValue)
                sql.Append(" AND (OBJ_ID = @OBJ_ID)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date), MONTH(SOC_Date), DAY(SOC_Date)");
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date), MONTH(SOC_Date)");
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("Datum, Anzahl DESC"); 

            result.Series.Clear();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);
                GetData.Parameters.AddWithValue("@RecType", recType);
                if (parentID.HasValue)
                    GetData.Parameters.AddWithValue("@OBJ_ID", parentID.Value);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (lastGroup != group || serie == null)
                    {
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (string.IsNullOrEmpty(group))
                            serie.Label = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnspecified");
                        else
                            serie.Label = group;

                        result.Series.Add(serie);
                    }
                    DateTime date;
                    if (granularity == Granularity.Day)
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimHour();
                    else if (granularity == Granularity.Month)
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimDay();
                    else if (granularity == Granularity.Year)
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimMonth();
                    else
                        date = DateTime.Now.TrimHour();

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;


                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"] == DBNull.Value ? 0 : sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    serie.Values.Add(item);
                }
                sqlReader.Close();
                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void ObjectCreationLoad(Business.StatisticObjectCreationResult result, int recType, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? parentID, List<object> include)
        {
            string key = string.Format("OR{0},{1},{2},{3},{4},{5}", recType, from.Ticks, to.Ticks, (int)groupBy, (int)granularity, parentID);
            Business.StatisticObjectCreationResult work = GetCache(key) as Business.StatisticObjectCreationResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(SON_Date) AS Datum, SUM(SON_Count) AS Anzahl, AVG(SON_Age) as AgeAvg, ");
            if (groupBy == GroupBy.Age)
            {
                sql.Append("SON_Age AS Grp");
                sqlGroupBy.Append("SON_Age, ");
                sqlWhere.Append("AND (SON_Age IN (");
            }
            else if (groupBy == GroupBy.Sex)
            {
                sql.Append("SON_Sex AS Grp");
                sqlGroupBy.Append("SON_Sex, ");
                sqlWhere.Append("AND (SON_Sex IN (");
            }
            else if (groupBy == GroupBy.Type)
            {
                sql.Append("SON_Type AS Grp");
                sqlGroupBy.Append("SON_Type, ");
                sqlWhere.Append("AND (SON_Type IN (");
            }
            else
            {
                groupBy = GroupBy.None;
                sql.Append("NULL AS Grp");
            }

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticObjectCreation_SON");

            sql.AppendLine();
            sql.Append("WHERE (SON_RecType = @RecType) AND (SON_Date BETWEEN @FromDate AND @ToDate)");
            if (parentID.HasValue)
                sql.Append(" AND (OBJ_ID = @OBJ_ID)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(SON_Date), MONTH(SON_Date), DAY(SON_Date)");
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(SON_Date), MONTH(SON_Date)");
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(SON_Date)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("Datum, Anzahl DESC"); 

            result.Series.Clear();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);
                GetData.Parameters.AddWithValue("@RecType", recType);
                if (parentID.HasValue)
                    GetData.Parameters.AddWithValue("@OBJ_ID", parentID.Value);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                DateTime lastDate = DateTime.MinValue;
                DateTime date = DateTime.MinValue;
                Business.StatisticSerie serie = null;
                while (sqlReader.Read())
                {
                    string group = string.Empty;
                    if (sqlReader["Grp"] != DBNull.Value)
                        group = sqlReader["Grp"].ToString();

                    if (string.IsNullOrEmpty(group))
                        group = "default";

                    if (lastGroup != group || serie == null)
                    {
                        if (serie != null)
                            AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);

                        lastDate = DateTime.MinValue;
                        lastGroup = group;
                        serie = new Business.StatisticSerie();
                        if (groupBy == GroupBy.Age)
                            serie.Label = GetAgeText(group);
                        else if (groupBy == GroupBy.Sex)
                            serie.Label = GetGenderText(group);
                        else if (groupBy == GroupBy.Type)
                            serie.Label = Helper.GetObjectName(group, false);
                        else if (string.IsNullOrEmpty(group))
                            group = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnspecified");
                        else
                            serie.Label = group;

                        result.Series.Add(serie);
                    }

                    if (granularity == Granularity.Day)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimHour();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfMonth().AddDays(-1);
                    }
                    else if (granularity == Granularity.Month)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimDay();
                        if (lastDate == DateTime.MinValue)
                            lastDate = date.GetStartOfYear().AddMonths(-1);
                    }
                    else if (granularity == Granularity.Year)
                    {
                        date = Convert.ToDateTime(sqlReader["Datum"]).TrimMonth();
                    }
                    else
                    {
                        date = DateTime.Now.TrimHour();
                    }

                    if (date < result.From)
                        result.From = date;
                    if (date > result.To)
                        result.To = date;

                    Business.StatisticValueDateTimeInt item = new Business.StatisticValueDateTimeInt() { X = date, Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    if (lastDate != DateTime.MinValue)
                    {
                        while (GetEmptyPoint(date, ref lastDate, granularity))
                        {
                            if (lastDate < result.From)
                                result.From = lastDate;
                            if (lastDate > result.To)
                                result.To = lastDate;

                            serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = lastDate, Y = 0 });
                        }
                    }

                    serie.Values.Add(item);
                    lastDate = date;
                }
                sqlReader.Close();

                if (serie != null)
                    AddEndEmptyPoint(serie, granularity, date, lastDate, result.From, result.To);

                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static void ObjectTotalLoad(Business.StatisticObjectTotalResult result, int recType, DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? parentID, List<object> include)
        {
            string key = string.Format("OT{0},{1},{2},{3},{4},{5}", recType, from.Ticks, to.Ticks, (int)groupBy, (int)granularity, parentID);
            Business.StatisticObjectTotalResult work = GetCache(key) as Business.StatisticObjectTotalResult;
            if (work != null)
            {
                result.From = work.From;
                result.To = work.To;
                result.Series.Clear();
                result.Series.AddRange(work.Series);
                return;
            }

            string strConn = Helper.GetSiemeConnectionString();
            string serieLabel = GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticsTypeTotalObjectsCount");   

            StringBuilder sql = new StringBuilder(200);
            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();

            sql.Append("SELECT MAX(SOC_Date) AS Datum, SUM(SOC_Count) AS Anzahl, AVG(SOC_Age) as AgeAvg, ");
            if (groupBy == GroupBy.Age)
            {
                sql.Append("SOC_Age AS Grp");
                sqlGroupBy.Append("SOC_Age, ");
                sqlWhere.Append("AND (SOC_Age IN (");
            }
            else if (groupBy == GroupBy.Sex)
            {
                sql.Append("SOC_Sex AS Grp");
                sqlGroupBy.Append("SOC_Sex, ");
                sqlWhere.Append("AND (SOC_Sex IN (");
            }
            else if (groupBy == GroupBy.Type)
            {
                sql.Append("SOC_Type AS Grp");
                sqlGroupBy.Append("SOC_Type, ");
                sqlWhere.Append("AND (SOC_Type IN (");
            }
            else
            {
                groupBy = GroupBy.None; 
                sql.Append("NULL AS Grp");
            }

            sql.AppendLine();
            sql.Append("FROM hitbl_StatisticObjectCount_SOC");

            sql.AppendLine();
            sql.Append("WHERE (SOC_RecType = @RecType) AND (SOC_Date BETWEEN @FromDate AND @ToDate)");
            if (parentID.HasValue)
                sql.Append(" AND (OBJ_ID = @OBJ_ID)");

            if (include != null && groupBy != GroupBy.None)
            {
                if (include.Count > 0)
                {
                    string komma = string.Empty;
                    foreach (object item in include)
                    {
                        sqlWhere.Append(komma);
                        if (item is string)
                            sqlWhere.AppendFormat("'{0}'", item.ToString().Replace("'", "''"));
                        else
                            sqlWhere.Append(item.ToString());

                        komma = ",";
                    }
                    sqlWhere.Append("))");
                    sql.Append(sqlWhere.ToString());
                }
                else
                {
                    sql.Append(" AND (1=2)");
                }
            }

            if (granularity == Granularity.Day)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date), MONTH(SOC_Date), DAY(SOC_Date)");
            else if (granularity == Granularity.Month)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date), MONTH(SOC_Date)");
            else if (granularity == Granularity.Year)
                sqlGroupBy.AppendFormat("YEAR(SOC_Date)");

            if (sqlGroupBy.Length > 0)
            {
                if (sqlGroupBy.ToString().EndsWith(", "))
                    sqlGroupBy.Remove(sqlGroupBy.Length - 2, 2);

                sql.AppendLine();
                sql.AppendFormat("GROUP BY {0}", sqlGroupBy.ToString());
            }

            sql.AppendLine();
            sql.Append("ORDER BY ");
            if (groupBy != GroupBy.None)
                sql.Append("Grp, ");

            sql.Append("Datum, Anzahl DESC"); 


            result.Series.Clear();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                GetData.Parameters.AddWithValue("@FromDate", from);
                GetData.Parameters.AddWithValue("@ToDate", to);
                GetData.Parameters.AddWithValue("@RecType", recType);
                if (parentID.HasValue)
                    GetData.Parameters.AddWithValue("@OBJ_ID", parentID.Value);

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                string lastGroup = string.Empty;
                Business.StatisticSerie serie = new Business.StatisticSerie();
                serie.Label = serieLabel;
                result.Series.Add(serie);

                while (sqlReader.Read())
                {
                    int group = int.Parse(sqlReader["Grp"].ToString());
                    bool singular = (Convert.ToInt32(sqlReader["Anzahl"]) < 2);
                    Business.StatisticValueStringInt item = new Business.StatisticValueStringInt() { X = Helper.GetObjectName(group, singular), Y = Convert.ToInt32(sqlReader["Anzahl"]) };
                    if (sqlReader["AgeAvg"] != DBNull.Value)
                        item.AgeAvg = Convert.ToInt32(sqlReader["AgeAvg"]);

                    serie.Values.Add(item);
                }
                sqlReader.Close();
                AddCache(key, result);
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        private static void AddEndEmptyPoint(Business.StatisticSerie serie, Granularity granularity, DateTime date, DateTime lastDate, DateTime from, DateTime to)
        {
            if (granularity == Granularity.Day)
            {
                if (date.Day < date.GetEndOfMonth().Day)
                    date = date.GetEndOfMonth().AddDays(1);
            }
            else if (granularity == Granularity.Month)
            {
                if (date.Month < 12)
                    date = date.GetEndOfYear().AddDays(1);
            }
            else
                date = lastDate;

            while (GetEmptyPoint(date, ref lastDate, granularity))
            {
                if (granularity == Granularity.Month)
                {
                    if (lastDate < from)
                        from = lastDate.GetStartOfMonth();
                    if (lastDate > to)
                        to = lastDate.GetEndOfMonth();
                }
                else
                {
                    if (lastDate < from)
                        from = lastDate;
                    if (lastDate > to)
                        to = lastDate;
                }

                serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = lastDate, Y = 0 });
            }

        }

        private static void AddEndEmptyHour(Business.StatisticSerie serie, DateTime date, int start, int stopp)
        {
            for (int i = start; i < stopp; i++)
            {
                DateTime newDate = new DateTime(date.Year, date.Month, date.Day, i, 0, 0);
                serie.Values.Add(new Business.StatisticValueDateTimeInt() { X = newDate, Y = 0 });
            }
        }


        private static bool GetEmptyPoint(DateTime date, ref DateTime lastDate, Granularity granularity)
        {
            if (granularity == Granularity.Day)
            {
                if (lastDate.Date.AddDays(1) < date.Date)
                {
                    lastDate = lastDate.Date.AddDays(1);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (granularity == Granularity.Month)
                if (lastDate.Date.AddMonths(1) < date.Date)
                {
                    lastDate = lastDate.Date.AddMonths(1);
                    return true;
                }
                else
                {
                    return false;
                }

            else if (granularity == Granularity.Year)
                if (lastDate.Date.AddYears(1) < date.Date)
                {
                    lastDate = lastDate.Date.AddYears(1);
                    return true;
                }
                else
                {
                    return false;
                }

            else
                return false;
        }

        private static string GetPageTypeText(string group)
        {
            if (group == "default")
                group = "0";

            LogSitePageType enu = (LogSitePageType)Convert.ToInt32(group);
            string name = GuiLanguage.GetGuiLanguage("DataAccess").GetString(string.Concat("EnumLogSitePageType", enu));
            if (!string.IsNullOrEmpty(name))
                return name;
            else
                return group;
        }

        private static string GetGenderText(string group)
        {
            GuiLanguage guiLanguage = GuiLanguage.GetGuiLanguage("DataAccess");
            string name;
            if (group == "0")
                name = guiLanguage.GetString("EnumGenderMale");
            else if (group == "1")
                name = guiLanguage.GetString("EnumGenderFemale");
            else
                name = guiLanguage.GetString("EnumNotSet");

            if (!string.IsNullOrEmpty(name))
                return name;
            else
                return group;
        }

        private static string GetAgeText(string group)
        {
            string name = group; ;
            string text;

            if (group == "0" || group == "default" || string.IsNullOrEmpty(group))
            {
                return GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumNotSet");
            }
            else
            {
                text = GuiLanguage.GetGuiLanguage("DataAccess").GetString("StatisticAgeYear");
                return string.Format("{0} {1}", group, text);
            }
        }

        private static object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        private static void AddCache(string key, object obj)
        {
            TimeSpan tisLifeTime = Business.DataAccessConfiguration.GetDefaultCachingTime().ToTimeSpan();
            HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.Add(tisLifeTime), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

    }
}