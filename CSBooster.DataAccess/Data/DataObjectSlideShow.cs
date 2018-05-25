using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectSlideShow
    {
        public static void FillObject(Business.DataObjectSlideShow item, SqlDataReader sqlReader)
        {
            if (sqlReader["Effect"] != DBNull.Value) item.Effect = sqlReader["Effect"].ToString();
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_SlideShow.*";
        }

        public static string GetInsertSQL(Business.DataObjectSlideShow item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_SlideShow ([OBJ_ID],[Effect]) VALUES (@OBJ_ID,@Effect)";
        }

        public static string GetUpdateSQL(Business.DataObjectSlideShow item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_SlideShow SET [Effect] = @Effect";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_SlideShow ON hiobj_SlideShow.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectSlideShow item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.Effect)) parameters.AddWithValue("@Effect", item.Effect);
            else parameters.AddWithValue("@Effect", DBNull.Value);
        }
    }
}
