using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectCommunity
    {
        public static void FillObject(Business.DataObjectCommunity item, SqlDataReader sqlReader)
        {
            if (sqlReader["VirtualURL"] != DBNull.Value) item.VirtualURL = sqlReader["VirtualURL"].ToString();
            item.Managed = (bool)sqlReader["Managed"];
            item.CreateGroupUser = (_4screen.CSB.Common.CommunityUsersType)Enum.Parse(typeof(_4screen.CSB.Common.CommunityUsersType), sqlReader["CreateGroupUser"].ToString());
            item.UploadUsers = (_4screen.CSB.Common.CommunityUsersType)Enum.Parse(typeof(_4screen.CSB.Common.CommunityUsersType), sqlReader["UploadUsers"].ToString());
            if (sqlReader["Emphasis"] != DBNull.Value) item.emphasisListXml.LoadXml(sqlReader["Emphasis"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Community.*";
        }

        public static string GetInsertSQL(Business.DataObjectCommunity item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Community ([OBJ_ID],[VirtualURL],[Managed],[CreateGroupUser],[UploadUsers],[Emphasis]) VALUES (@OBJ_ID,@VirtualURL,@Managed,@CreateGroupUser,@UploadUsers,@Emphasis)";
        }

        public static string GetUpdateSQL(Business.DataObjectCommunity item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Community SET [VirtualURL] = @VirtualURL, [Managed] = @Managed, [CreateGroupUser] = @CreateGroupUser, [UploadUsers] =  @UploadUsers, [Emphasis] = @Emphasis";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Community ON hiobj_Community.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            string retString = string.Empty;
            return retString;
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectCommunity item, SqlParameterCollection parameters)
        {
            if (!string.IsNullOrEmpty(item.VirtualURL)) parameters.AddWithValue("@VirtualURL", item.VirtualURL);
            else parameters.AddWithValue("@VirtualURL", DBNull.Value);
            parameters.AddWithValue("@Managed", item.Managed);
            parameters.AddWithValue("@CreateGroupUser", item.CreateGroupUser);
            parameters.AddWithValue("@UploadUsers", item.UploadUsers);
            parameters.AddWithValue("@Emphasis", item.emphasisListXml.OuterXml);
        }



    }
}
