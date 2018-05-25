// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectForumTopicItem
    {
        public static void FillObject(Business.DataObjectForumTopicItem item, SqlDataReader sqlReader)
        {
            if (sqlReader["ReferencedItemId"] != DBNull.Value) item.ReferencedItemId = (Guid)sqlReader["ReferencedItemId"];
            item.ItemStatus = (_4screen.CSB.Common.CommentStatus)Enum.Parse(typeof(_4screen.CSB.Common.CommentStatus), sqlReader["Status"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_ForumTopicItem.*";
        }

        public static string GetInsertSQL(Business.DataObjectForumTopicItem item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_ForumTopicItem ([OBJ_ID],[ReferencedItemId],[Status]) VALUES (@OBJ_ID,@ReferencedItemId,@Status)";
        }

        public static string GetUpdateSQL(Business.DataObjectForumTopicItem item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_ForumTopicItem SET [ReferencedItemId] = @ReferencedItemId, [Status] = @Status";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_ForumTopicItem ON hiobj_ForumTopicItem.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectForumTopicItem item, SqlParameterCollection parameters)
        {
            if (item.ReferencedItemId.HasValue) parameters.AddWithValue("@ReferencedItemId", item.ReferencedItemId.Value);
            else parameters.AddWithValue("@ReferencedItemId", DBNull.Value);
            parameters.AddWithValue("@Status", item.ItemStatus);
        }
    }
}
