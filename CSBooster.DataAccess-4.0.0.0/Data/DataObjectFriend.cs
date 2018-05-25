// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObjectFriend
    {
        public static void FillObject(Business.DataObjectFriend item, SqlDataReader sqlReader)
        {
            DataObjectUser.FillObject(item, sqlReader);
            if (sqlReader["UFR_TypeID"] != DBNull.Value) item.FriendType = (FriendType)Convert.ToInt32(sqlReader["UFR_TypeID"]);
            if (sqlReader["FRI_Blocked"] != DBNull.Value) item.Blocked = Convert.ToBoolean(sqlReader["FRI_Blocked"]);
            if (sqlReader["FRI_AllowBirthdayNotIFication"] != DBNull.Value) item.AllowBirthdayNotification = Convert.ToInt32(sqlReader["FRI_AllowBirthdayNotIFication"]);
        }

        public static string GetSelectSQL(Business.QuickParametersFriends qpUser, SqlParameterCollection parameters)
        {
            return DataObjectUser.GetSelectSQL((Business.QuickParametersUser) qpUser, parameters);
        }

        public static string GetInsertSQL(Business.DataObjectFriend item, SqlParameterCollection parameters)
        {
            return DataObjectUser.GetUpdateSQL((Business.DataObjectUser)item, parameters);
        }

        public static string GetUpdateSQL(Business.DataObjectFriend item, SqlParameterCollection parameters)
        {
            return DataObjectUser.GetUpdateSQL((Business.DataObjectUser) item,  parameters);
        }

        public static string GetJoinSQL(Business.QuickParametersFriends qpUser, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetWhereSQL(Business.QuickParametersFriends qpUser, SqlParameterCollection parameters)
        {
            return string.Empty;

        }

        public static string GetFullTextWhereSQL(Business.QuickParametersFriends qpUser, SqlParameterCollection parameters)
        {
            string retString = string.Empty;
            return retString;
        }

        public static string GetOrderBySQL(Business.QuickParametersFriends qpUser, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        private static void SetParameters(Business.DataObjectUser item, SqlParameterCollection parameters)
        {
           
        }
    }
}
