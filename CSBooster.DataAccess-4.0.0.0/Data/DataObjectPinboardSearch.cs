// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPinboardSearch
    {
        public static void FillObject(Business.DataObjectPinboardSearch item, SqlDataReader sqlReader)
        {
            if (sqlReader["Price"] != DBNull.Value) item.Price = sqlReader["Price"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_PinboardSearch.*";
        }

        public static string GetInsertSQL(Business.DataObjectPinboardSearch item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_PinboardSearch ([OBJ_ID],[Price]) VALUES (@OBJ_ID,@Price)";
        }

        public static string GetUpdateSQL(Business.DataObjectPinboardSearch item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_PinboardSearch SET [Price] = @Price";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_PinboardSearch ON hiobj_PinboardSearch.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPinboardSearch item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.Price)) parameters.AddWithValue("@Price", item.Price);
            else parameters.AddWithValue("@Price", DBNull.Value);
        }
    }
}
