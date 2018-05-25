// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectGeneric
    {
        public static void FillObject(Business.DataObjectGeneric item, SqlDataReader sqlReader)
        {
            if (sqlReader["GenericData"] != DBNull.Value) item.GenericData = sqlReader["GenericData"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Generic.*";
        }

        public static string GetInsertSQL(Business.DataObjectGeneric item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Generic ([OBJ_ID],[GenericData]) VALUES (@OBJ_ID,@GenericData)";
        }

        public static string GetUpdateSQL(Business.DataObjectGeneric item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Generic SET [GenericData] = @GenericData";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Generic ON hiobj_Generic.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            string retString = string.Empty;
            return retString;
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectGeneric item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.GenericData)) parameters.AddWithValue("@GenericData", item.GenericData);
            else parameters.AddWithValue("@GenericData", DBNull.Value);
        }
    }
}
