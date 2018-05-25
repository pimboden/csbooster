// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectEvent
    {
        public static void FillObject(Business.DataObjectEvent item, SqlDataReader sqlReader)
        {
            if (sqlReader["Content"] != DBNull.Value) item.Content = sqlReader["Content"].ToString();
            if (sqlReader["Time"] != DBNull.Value) item.Time = sqlReader["Time"].ToString();
            if (sqlReader["Age"] != DBNull.Value) item.Age = sqlReader["Age"].ToString();
            if (sqlReader["Price"] != DBNull.Value) item.Price = sqlReader["Price"].ToString();
            if (sqlReader["Website"] != DBNull.Value) item.Website = new Uri(sqlReader["Website"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Event.*";
        }

        public static string GetInsertSQL(Business.DataObjectEvent item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Event ([OBJ_ID],[Content],[Time],[Age],[Price],[Website]) VALUES (@OBJ_ID,@Content,@Time,@Age,@Price,@Website)";
        }

        public static string GetUpdateSQL(Business.DataObjectEvent item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Event SET [Content] = @Content, [Time] = @Time, [Age] = @Age, [Price] =  @Price, [Website] =  @Website";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Event ON hiobj_Event.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(qParas.GeneralSearch))
            {
                if (qParas.CatalogSearchType == DBCatalogSearchType.FreetextTable)
                    return string.Format(" OR FREETEXT(hiobj_Event.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
                else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    return " OR CONTAINS(hiobj_Event.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectEvent item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.Content)) parameters.AddWithValue("@Content", item.Content);
            else parameters.AddWithValue("@Content", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Time)) parameters.AddWithValue("@Time", item.Time);
            else parameters.AddWithValue("@Time", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Age)) parameters.AddWithValue("@Age", item.Age);
            else parameters.AddWithValue("@Age", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Price)) parameters.AddWithValue("@Price", item.Price);
            else parameters.AddWithValue("@Price", DBNull.Value);
            if (item.Website != null) parameters.AddWithValue("@Website", item.Website.ToString());
            else parameters.AddWithValue("@Website", DBNull.Value);
        }
    }
}
