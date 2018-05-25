// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectLocation
    {
        public static void FillObject(Business.DataObjectLocation item, SqlDataReader sqlReader)
        {
            if (sqlReader["Website"] != DBNull.Value) item.Website = new Uri(sqlReader["Website"].ToString());
        }

        public static string GetSelectSQL(DataAccess.Business.QuickParameters paras)
        {
            return ", hiobj_Location.*";
        }

        public static string GetInsertSQL(Business.DataObjectLocation item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return @"INSERT INTO hiobj_Location (OBJ_ID,Website) VALUES (@OBJ_ID,@Website)";
        }

        public static string GetUpdateSQL(Business.DataObjectLocation item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Location SET [Website] =  @Website";
        }

        public static string GetJoinSQL(DataAccess.Business.QuickParameters paras)
        {
            return "INNER JOIN hiobj_Location ON hiobj_Location.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(DataAccess.Business.QuickParameters paras)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(DataAccess.Business.QuickParameters paras)
        {
            string retString = string.Empty;
            return retString;
        }


        public static string GetOrderBySQL()
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectLocation item, SqlParameterCollection parameters)
        {
            if (item.Website != null) parameters.AddWithValue("@Website", item.Website.ToString());
            else parameters.AddWithValue("@Website", DBNull.Value);
        }
    }
}
