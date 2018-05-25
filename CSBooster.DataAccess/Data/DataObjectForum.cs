using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectForum
    {
        public static void FillObject(Business.DataObjectForum item, SqlDataReader sqlReader)
        {
            item.ThreadCreationUsers = (_4screen.CSB.Common.CommunityUsersType)Enum.Parse(typeof(_4screen.CSB.Common.CommunityUsersType), sqlReader["ThreadCreationUsers"].ToString());
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return ", hiobj_Forum.*";
        }

        public static string GetInsertSQL(Business.DataObjectForum item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "INSERT INTO hiobj_Forum ([OBJ_ID],[ThreadCreationUsers]) VALUES (@OBJ_ID,@ThreadCreationUsers)";
        }

        public static string GetUpdateSQL(Business.DataObjectForum item, SqlParameterCollection parameters)
        {
            SetParameters(item, parameters);
            return "UPDATE hiobj_Forum SET [ThreadCreationUsers] = @ThreadCreationUsers";
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return "INNER JOIN hiobj_Forum ON hiobj_Forum.OBJ_ID = hitbl_DataObject_OBJ.OBJ_ID";
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

        private static void SetParameters(Business.DataObjectForum item, SqlParameterCollection parameters)
        {
            parameters.AddWithValue("@ThreadCreationUsers", item.ThreadCreationUsers);
        }
    }
}
