// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataObj.Data
{
    internal class DataObjectSurvey
    {
        public static void FillObject(Business.DataObjectSurvey item, SqlDataReader sqlReader)
        {
            if (sqlReader["Header"] != DBNull.Value) item.Header = sqlReader["Header"].ToString();
            if (sqlReader["HeaderLinked"] != DBNull.Value) item.HeaderLinked = sqlReader["HeaderLinked"].ToString();
            if (sqlReader["Footer"] != DBNull.Value) item.Footer = sqlReader["Footer"].ToString();
            if (sqlReader["FooterLinked"] != DBNull.Value) item.FooterLinked = sqlReader["FooterLinked"].ToString();
            if (sqlReader["PunkteGruen"] != DBNull.Value) item.PunkteGruen = Convert.ToDouble(sqlReader["PunkteGruen"]);
            if (sqlReader["PunkteGelb"] != DBNull.Value) item.PunkteGelb = Convert.ToDouble(sqlReader["PunkteGelb"]);
            if (sqlReader["PunkteRot"] != DBNull.Value) item.PunkteRot = Convert.ToDouble(sqlReader["PunkteRot"]);
            item.IsContest = sqlReader["IsContest"] != DBNull.Value && Convert.ToBoolean(sqlReader["IsContest"]);
            item.ShowForm = sqlReader["ShowForm"] != DBNull.Value && Convert.ToBoolean(sqlReader["ShowForm"]);
            if (sqlReader["MailTo"] != DBNull.Value) item.MailTo = sqlReader["MailTo"].ToString();
        }

        public static string GetSelectSql(DataAccess.Business.QuickParameters paras)
        {
            return ", hiobj_Survey.*";
        }

        public static string GetInsertSql(Business.DataObjectSurvey item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Survey([OBJ_ID],[Header],[HeaderLinked],[Footer],[FooterLinked],[PunkteGruen],[PunkteGelb],[PunkteRot],[IsContest],[ShowForm],[MailTo])VALUES (@OBJ_ID,@Header,@HeaderLinked,@Footer,@FooterLinked,@PunkteGruen,@PunkteGelb,@PunkteRot,@IsContest,@ShowForm,@MailTo)";
        }

        public static string GetUpdateSql(Business.DataObjectSurvey item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Survey SET [OBJ_ID] = @OBJ_ID,[Header] = @Header, [HeaderLinked] = @HeaderLinked,[Footer] = @Footer,[FooterLinked] = @FooterLinked,[PunkteGruen] = @PunkteGruen,[PunkteGelb] = @PunkteGelb,[PunkteRot] = @PunkteRot,[IsContest] =@IsContest,[ShowForm] = @ShowForm,[MailTo]=@MailTo";
        }

        public static string GetJoinSql(DataAccess.Business.QuickParameters qParas)
        {
            return "INNER JOIN hiobj_Survey ON hiobj_Survey.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSql(DataAccess.Business.QuickParameters qParas)
        {
            string WhereSentence = string.Empty;

            if (qParas != null && qParas is DataAccess.Business.QuickParametersDataObjectSurvey)
            {
                DataAccess.Business.QuickParametersDataObjectSurvey qpSurvay = qParas as DataAccess.Business.QuickParametersDataObjectSurvey;
                if (qpSurvay.IsContest.HasValue)
                {
                    WhereSentence += string.Format("(hiobj_Survey.IsContest = {0})\r\n", Convert.ToInt32(qpSurvay.IsContest.Value));
                }
            }
            return WhereSentence;
        }

        public static string GetFullTextWhereSql(DataAccess.Business.QuickParameters qParas)
        {
            //if (!string.IsNullOrEmpty(qParas.GeneralSearch))
            //{
            //    if (qParas.CatalogSearchType == DBCatalogSearchType.FreetextTable)
            //        return string.Format(" OR FREETEXT(hiobj_Survey.*, '{0}', LANGUAGE 0x0)\r\n", qParas.GeneralSearch.Replace("'", "''"));
            //    else if (qParas.CatalogSearchType == DBCatalogSearchType.ContainsTable)
            //        return " OR CONTAINS(hiobj_Survey.*, @ObjectGeneralSearch, LANGUAGE 0x0)\r\n";
            //    else
            //        return string.Empty;
            //}
            //else
            //{
            //    return string.Empty;
            //}
            return string.Empty;
        }

        public static string GetOrderBySql()
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectSurvey item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.Header)) parameters.AddWithValue("@Header", item.Header);
            else parameters.AddWithValue("@Header", DBNull.Value);

            if (!string.IsNullOrEmpty(item.HeaderLinked)) parameters.AddWithValue("@HeaderLinked", item.HeaderLinked);
            else parameters.AddWithValue("@HeaderLinked", DBNull.Value);

            if (!string.IsNullOrEmpty(item.Footer)) parameters.AddWithValue("@Footer", item.Footer);
            else parameters.AddWithValue("@Footer", DBNull.Value);

            if (!string.IsNullOrEmpty(item.FooterLinked)) parameters.AddWithValue("@FooterLinked", item.FooterLinked);
            else parameters.AddWithValue("@FooterLinked", DBNull.Value);

            parameters.AddWithValue("@PunkteGruen", item.PunkteGruen);
            parameters.AddWithValue("@PunkteGelb", item.PunkteGelb);
            parameters.AddWithValue("@PunkteRot", item.PunkteRot);
            if (!string.IsNullOrEmpty(item.MailTo)) parameters.AddWithValue("@MailTo", item.MailTo);
            else parameters.AddWithValue("@MailTo", DBNull.Value);
            parameters.AddWithValue("@IsContest", item.IsContest);
            parameters.AddWithValue("@ShowForm", item.ShowForm);

        }
    }
}
