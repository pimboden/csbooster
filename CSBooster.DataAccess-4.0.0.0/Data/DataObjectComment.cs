// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectComment
    {
        public static void FillObject(Business.DataObjectComment item, SqlDataReader sqlReader)
        {
            if (sqlReader["CommentStatus"] != DBNull.Value) item.CommentStatus = Convert.ToInt32(sqlReader["CommentStatus"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Comment.*";
        }

        public static string GetInsertSQL(Business.DataObjectComment item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Comment ([OBJ_ID], [CommentStatus]) VALUES (@OBJ_ID, @CommentStatus)";
        }

        public static string GetUpdateSQL(Business.DataObjectComment item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Comment SET  [CommentStatus] = @CommentStatus ";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Comment ON hiobj_Comment.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {

            return string.Empty;
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectComment item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@CommentStatus", item.CommentStatus);
        }
    }
}
