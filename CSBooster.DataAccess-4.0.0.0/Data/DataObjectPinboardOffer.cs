// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPinboardOffer
    {
        public static void FillObject(Business.DataObjectPinboardOffer item, SqlDataReader sqlReader)
        {
            if (sqlReader["Price"] != DBNull.Value) item.Price = sqlReader["Price"].ToString();
            item.Amount = (int)sqlReader["Amount"];
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_PinboardOffer.*";
        }

        public static string GetInsertSQL(Business.DataObjectPinboardOffer item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_PinboardOffer ([OBJ_ID],[Price],[Amount]) VALUES (@OBJ_ID,@Price,@Amount)";
        }

        public static string GetUpdateSQL(Business.DataObjectPinboardOffer item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_PinboardOffer SET [Price] = @Price, [Amount] = @Amount";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_PinboardOffer ON hiobj_PinboardOffer.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPinboardOffer item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.Price)) parameters.AddWithValue("@Price", item.Price);
            else parameters.AddWithValue("@Price", DBNull.Value);
            parameters.AddWithValue("@Amount", item.Amount);
        }
    }
}
