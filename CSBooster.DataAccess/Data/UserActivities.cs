using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class UserActivities
    {
        public static Business.UserActivityList<Business.UserActivity> Load(Business.UserActivityParameters paras)
        {
            StringBuilder key = new StringBuilder("UserActivity" + paras.Udc.UserID.ToString());
            if (paras.WithAdminData) 
                key.Append("WAD"); 

            if (paras.Amount > 0 && paras.Amount <= Business.DataAccessConfiguration.UserActivityMaximalAmount())    
                key.Append(paras.Amount.ToString());
            else
                key.Append(Business.DataAccessConfiguration.UserActivityMaximalAmount().ToString());

            QuickCacheHandler cacheHandler = new QuickCacheHandler(key.ToString());
            cacheHandler.AlternateCacheMinutes = 2;
            cacheHandler.ItemPriority = System.Web.Caching.CacheItemPriority.Default;

            Business.UserActivityList<Business.UserActivity> list = cacheHandler.Get() as Business.UserActivityList<Business.UserActivity>;
            if (list != null)
                return list;

            list = new Business.UserActivityList<Business.UserActivity>(paras);

            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            SqlDataReader sqlReader = null;
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;

                //if (paras.ObjectType == ObjectType.Community)
                //{
                //    GetData.CommandText = "hisp_UserActivity_Community_Load";
                //    GetData.Parameters.AddWithValue("@USR_ID", paras.Udc.UserId.ToGuid()); 
                //    GetData.Parameters.AddWithValue("@CTY_ID", paras.ObjectID);
                //}
                //else
                //{
                    GetData.CommandText = "hisp_UserActivity_User_Load";
                    GetData.Parameters.AddWithValue("@USR_ID",paras.Udc.UserID);
                //}

                if (paras.WithAdminData) 
                    GetData.Parameters.AddWithValue("@WithAdminData", 1);

                if (paras.UserActivityType != null) 
                    GetData.Parameters.AddWithValue("@USA_Type", (int)paras.UserActivityType.Value);

                if (paras.Amount > 0 && paras.Amount <= Business.DataAccessConfiguration.UserActivityMaximalAmount())    
                    GetData.Parameters.AddWithValue("@Amount", paras.Amount);
                else
                    GetData.Parameters.AddWithValue("@Amount", Business.DataAccessConfiguration.UserActivityMaximalAmount());

                Conn.Open();  
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection); 
                while (sqlReader.Read())
                {
                    Business.UserActivity item = new Business.UserActivity(sqlReader["USA_ID"].ToString().ToGuid(), (UserActivityWhat)sqlReader["USA_What"].ToString().ToInt32(0));
                    item.UserID = sqlReader["USR_ID"].ToString().ToGuid();
                    item.UserNickname = sqlReader["USR_Nickname"].ToString();
                    item.Date = Convert.ToDateTime(sqlReader["USA_Date"]);
                    if (sqlReader["USA_Target_OBJ_ID"] != DBNull.Value)
                        item.TargetObjectID = sqlReader["USA_Target_OBJ_ID"].ToString().ToGuid();
                    if (sqlReader["USA_Target_OBJ_Type"] != DBNull.Value)
                        item.TargetObjectType = Convert.ToInt32(sqlReader["USA_Target_OBJ_Type"]);
                    item.TargetObjectText = sqlReader["USA_Target_OBJ_Text"].ToString();

                    if (sqlReader["USA_Detail_OBJ_ID"] != DBNull.Value)
                        item.DetailObjectID = sqlReader["USA_Detail_OBJ_ID"].ToString().ToGuid();
                    if (sqlReader["USA_Detail_OBJ_Type"] != DBNull.Value)
                        item.DetailObjectType = Convert.ToInt32(sqlReader["USA_Detail_OBJ_Type"]);

                    if (item.ActivityWhat == UserActivityWhat.DoNowThis && sqlReader["USA_Text"] != DBNull.Value)
                        item.DetailObjectText = sqlReader["USA_Text"].ToString();
                    else
                        item.DetailObjectText = sqlReader["USA_Detail_OBJ_Text"].ToString();

                    item.OnlyVisibleForAdmin = Convert.ToBoolean(sqlReader["USA_OnlyVisibleForAdmin"]);

                    list.Add(item);
                }
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
                sqlReader = null;
            }

            paras.ItemTotal = list.Count;
            cacheHandler.Insert(list);  

            return list;
        }

        public static void Insert(Guid userID, string userNickname, UserActivityWhat activityWhat, Guid? targetObjectID, Guid? detailObjectID, string text, bool? onlyVisibleForAdmin)
        {
            if (!Business.DataAccessConfiguration.UserActivityIsActivityActiv(activityWhat))
                return;

            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserActivity_Insert";

                GetData.Parameters.AddWithValue("@USR_ID", userID);
                if (!string.IsNullOrEmpty(userNickname) && userNickname.Length > 0)
                    GetData.Parameters.AddWithValue("@USR_Nickname", userNickname);

                GetData.Parameters.AddWithValue("@USA_WHAT", (int)activityWhat);
                GetData.Parameters.AddWithValue("@USA_Type", (int)GetTypeDependOnWhat(activityWhat));
                GetData.Parameters.AddWithValue("@USA_ValidUntilMinutes", Business.DataAccessConfiguration.UserActivityValidUntil(activityWhat));

                if (targetObjectID != null)
                    GetData.Parameters.AddWithValue("@USA_Target_OBJ_ID", targetObjectID.Value);

                if (detailObjectID != null)
                    GetData.Parameters.AddWithValue("@USA_Detail_OBJ_ID", detailObjectID.Value);

                if (!string.IsNullOrEmpty(text))
                    GetData.Parameters.AddWithValue("@USA_Text", text.CutRight(256));

                if (onlyVisibleForAdmin != null)
                    GetData.Parameters.AddWithValue("@USA_OnlyVisibleForAdmin", onlyVisibleForAdmin.Value.ToSqlBit());

                Conn.Open();
                GetData.ExecuteNonQuery();
                Conn.Close();

                QuickCacheHandler.RemoveCache("UserActivity" + userID.ToString());
            }
            catch
            {
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();  

                // do nothing da nicht lebenswichtig
            }
        }

        public static UserActivityType GetTypeDependOnWhat(UserActivityWhat activityWhat)
        {
            if (activityWhat == UserActivityWhat.DoNowThis || activityWhat == UserActivityWhat.IsNowOnline)
            {
                return UserActivityType.News;
            }
            else if (activityWhat == UserActivityWhat.AreNowFriends || activityWhat == UserActivityWhat.IsNowMember)
            {
                return UserActivityType.Relationship;
            }
            else
            {
                return UserActivityType.Objects;
            }
        }
    }
}
