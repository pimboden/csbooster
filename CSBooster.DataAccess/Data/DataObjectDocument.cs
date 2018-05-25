using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectDocument
    {
        public static void FillObject(Business.DataObjectDocument item, SqlDataReader sqlReader)
        {
            item.SizeByte = (long)sqlReader["SizeByte"];
            if (sqlReader["URLDocument"] != DBNull.Value) item.URLDocument = sqlReader["URLDocument"].ToString();
            if (sqlReader["Author"] != DBNull.Value) item.Author = sqlReader["Author"].ToString();
            if (sqlReader["Version"] != DBNull.Value) item.Version = sqlReader["Version"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Document.*";
        }

        public static string GetInsertSQL(Business.DataObjectDocument item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Document ([OBJ_ID],[SizeByte],[URLDocument],[Author],[Version]) VALUES (@OBJ_ID,@SizeByte,@URLDocument,@Author,@Version)";
        }

        public static string GetUpdateSQL(Business.DataObjectDocument item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Document SET [SizeByte] = @SizeByte, [URLDocument] = @URLDocument, [Author] = @Author, [Version] =  @Version";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Document ON hiobj_Document.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectDocument item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@SizeByte", item.SizeByte);
            if (!string.IsNullOrEmpty(item.URLDocument)) parameters.AddWithValue("@URLDocument", item.URLDocument);
            else parameters.AddWithValue("@URLDocument", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Author)) parameters.AddWithValue("@Author", item.Author);
            else parameters.AddWithValue("@Author", DBNull.Value);
            if (!string.IsNullOrEmpty(item.Version)) parameters.AddWithValue("@Version", item.Version);
            else parameters.AddWithValue("@Version", DBNull.Value);
        }
    }
}
