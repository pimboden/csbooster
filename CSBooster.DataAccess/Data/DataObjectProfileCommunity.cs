using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectProfileCommunity
    {
        public static void FillObject(Business.DataObjectProfileCommunity item, SqlDataReader sqlReader)
        {
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetInsertSQL(Business.DataObjectProfileCommunity item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_ProfileCommunity ([OBJ_ID]) VALUES (@OBJ_ID)";
        }

        public static string GetUpdateSQL(Business.DataObjectProfileCommunity item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return string.Empty;
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_ProfileCommunity ON hiobj_ProfileCommunity.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectProfileCommunity item, SqlParameterCollection parameters)
        {
        }
    }
}
