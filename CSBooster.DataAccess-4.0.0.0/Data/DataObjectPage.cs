// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectPage
    {
        public static void FillObject(Business.DataObjectPage item, SqlDataReader sqlReader)
        {
            if (sqlReader["MetaTag"] != DBNull.Value) item.MetaTag = sqlReader["MetaTag"].ToString();
            if (sqlReader["VirtualURL"] != DBNull.Value) item.VirtualURL = sqlReader["VirtualURL"].ToString();
            item.PageType = (PageType)Enum.Parse(typeof(PageType), sqlReader["PageType"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Page.*";
        }

        public static string GetInsertSQL(Business.DataObjectPage item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Page ([OBJ_ID],[MetaTag],[VirtualURL],[PageType]) VALUES (@OBJ_ID,@MetaTag,@VirtualURL,@PageType)";
        }

        public static string GetUpdateSQL(Business.DataObjectPage item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Page SET [MetaTag] = @MetaTag, [VirtualURL] = @VirtualURL, [PageType] = @PageType";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Page ON hiobj_Page.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectPage item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.MetaTag)) parameters.AddWithValue("@MetaTag", item.MetaTag);
            else parameters.AddWithValue("@MetaTag", DBNull.Value);
            if (!string.IsNullOrEmpty(item.VirtualURL)) parameters.AddWithValue("@VirtualURL", item.VirtualURL);
            else parameters.AddWithValue("@VirtualURL", DBNull.Value);
            parameters.AddWithValue("@PageType", item.PageType);
        }
    }
}
