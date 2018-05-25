// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectForumTopic
    {
        public static void FillObject(Business.DataObjectForumTopic item, SqlDataReader sqlReader)
        {
            item.TopicState = (Business.ForumTopicState)Enum.Parse(typeof(Business.ForumTopicState), sqlReader["TopicState"].ToString());
            item.TopicItemCount = (int)sqlReader["TopicItemCount"];
            if (sqlReader["LastTopicItemDate"] != DBNull.Value) item.LastTopicItemDate = (DateTime)sqlReader["LastTopicItemDate"];
            if (sqlReader["LastTopicItemID"] != DBNull.Value) item.LastTopicItemID = (Guid)sqlReader["LastTopicItemID"];
            if (sqlReader["LastTopicItemUserID"] != DBNull.Value) item.LastTopicItemUserID = (Guid)sqlReader["LastTopicItemUserID"];
            if (sqlReader["LastTopicItemNickname"] != DBNull.Value) item.LastTopicItemNickname = sqlReader["LastTopicItemNickname"].ToString();
            item.IsModerated = (bool)sqlReader["IsModerated"];
            item.TopicItemCreationUsers = (_4screen.CSB.Common.CommunityUsersType)Enum.Parse(typeof(_4screen.CSB.Common.CommunityUsersType), sqlReader["TopicItemCreationUsers"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_ForumTopic.*";
        }

        public static string GetInsertSQL(Business.DataObjectForumTopic item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_ForumTopic ([OBJ_ID],[TopicState],[TopicItemCount],[LastTopicItemDate],[LastTopicItemID],[LastTopicItemUserID],[LastTopicItemNickname],[IsModerated],[TopicItemCreationUsers]) VALUES (@OBJ_ID,@TopicState,@TopicItemCount,@LastTopicItemDate,@LastTopicItemID,@LastTopicItemUserID,@LastTopicItemNickname,@IsModerated,@TopicItemCreationUsers)";
        }

        public static string GetUpdateSQL(Business.DataObjectForumTopic item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_ForumTopic SET [TopicState] = @TopicState, [TopicItemCount] = @TopicItemCount, [LastTopicItemDate] = @LastTopicItemDate, [LastTopicItemID] =  @LastTopicItemID, [LastTopicItemUserID] = @LastTopicItemUserID, [LastTopicItemNickname] = @LastTopicItemNickname, [IsModerated] = @IsModerated, [TopicItemCreationUsers] = @TopicItemCreationUsers";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_ForumTopic ON hiobj_ForumTopic.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectForumTopic item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@TopicState", item.TopicState);
            parameters.AddWithValue("@TopicItemCount", item.TopicItemCount);
            if (item.LastTopicItemDate != null) parameters.AddWithValue("@LastTopicItemDate", item.LastTopicItemDate);
            else parameters.AddWithValue("@LastTopicItemDate", DBNull.Value);
            if (item.LastTopicItemID != null) parameters.AddWithValue("@LastTopicItemID", item.LastTopicItemID);
            else parameters.AddWithValue("@LastTopicItemID", DBNull.Value);
            if (item.LastTopicItemUserID != null) parameters.AddWithValue("@LastTopicItemUserID", item.LastTopicItemUserID);
            else parameters.AddWithValue("@LastTopicItemUserID", DBNull.Value);
            if (!string.IsNullOrEmpty(item.LastTopicItemNickname)) parameters.AddWithValue("@LastTopicItemNickname", item.LastTopicItemNickname);
            else parameters.AddWithValue("@LastTopicItemNickname", DBNull.Value);
            parameters.AddWithValue("@IsModerated", item.IsModerated);
            parameters.AddWithValue("@TopicItemCreationUsers", item.TopicItemCreationUsers);
        }
    }
}
