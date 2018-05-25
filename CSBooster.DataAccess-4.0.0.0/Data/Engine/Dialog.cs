// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using _4screen.CSB.Extensions.Business;

namespace _4screen.CSB.DataAccess.Data
{
    internal class Dialog
    {
        /// <summary>
        /// Check if the dialog condition method return value is >= the condition true value
        /// </summary>
        /// <param name="userId">A unique user id</param>
        /// <returns>true, if the dialog should be displayed</returns>
        internal static bool IsConditionTrue(Guid userId, Business.Dialog dialog)
        {
            try
            {
                int recurring = 0;
                if (dialog.Parameters.ContainsKey("recurring"))
                    recurring = int.Parse(dialog.Parameters["recurring"]);

                MembershipUser user = Membership.GetUser(userId);
                if (user.CreationDate > dialog.ActiveFromDate && !Data.Dialog.AlreadyViewed(userId, dialog, recurring))
                {
                    int conditionReturnValue = (int) dialog.ConditionMethodInfo.Invoke(null, new object[] {userId, dialog});
                    if (conditionReturnValue >= dialog.ConditionTrueValue)
                    {
                        if (Data.Dialog.AddView(userId, dialog, recurring))
                            return true;
                    }
                }
            }
            finally
            {
            }
            return false;
        }

        internal static bool AlreadyViewed(Guid userId, Business.Dialog dialog, int recurring)
        {
            bool viewAdded = false;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Dialogs_AlreadyViewed";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@DialogId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@DialogId"].Value = dialog.DialogId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@TimeSpanSecond", SqlDbType.Int));
                sqlConnection.Command.Parameters["@TimeSpanSecond"].Value = recurring;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Viewed", SqlDbType.Bit));
                sqlConnection.Command.Parameters["@Viewed"].Direction = ParameterDirection.ReturnValue;
                sqlConnection.Command.ExecuteNonQuery();
                viewAdded = ((int) sqlConnection.Command.Parameters["@Viewed"].Value == 1) ? true : false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return viewAdded;
        }

        /// <summary>
        /// Log a dialog view
        /// </summary>
        /// <returns>true, if the view has been added</returns>
        internal static bool AddView(Guid userId, Business.Dialog dialog, int recurring)
        {
            bool viewAdded = false;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Dialogs_AddView";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@DialogId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@DialogId"].Value = dialog.DialogId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@TimeSpanSecond", SqlDbType.Int));
                sqlConnection.Command.Parameters["@TimeSpanSecond"].Value = recurring;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ViewAdded", SqlDbType.Bit));
                sqlConnection.Command.Parameters["@ViewAdded"].Direction = ParameterDirection.ReturnValue;
                sqlConnection.Command.ExecuteNonQuery();
                viewAdded = ((int) sqlConnection.Command.Parameters["@ViewAdded"].Value == 1) ? true : false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return viewAdded;
        }

        /// <summary>
        /// Get the number of days since the last login
        /// </summary>
        internal static int GetLastLoginInDays(Guid userId, Business.Dialog dialog)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userId);
                TimeSpan timeSpan = DateTime.Now - user.LastLoginDate;
                return timeSpan.Days;
            }
            catch
            {
                return 0;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get the number of activated friends
        /// </summary>
        /*internal static int GetFriendCount(Guid userId, Business.Dialog dialog)
        {
            Business.QuickList<Business.DataObjectUser> friends = Business.DataObjectUsers.LoadFriends(userId.ToString(), false, true, null, null, new Business.QuickParameters() {Udc = UserDataContext.GetUserDataContext()});
            return friends.ItemTotal;
        }*/

        /// <summary>
        /// Get the number of rated objects
        /// </summary>
        internal static int GetRatingCount(Guid userId, Business.Dialog dialog)
        {
            int ratingCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Ratings_Count";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                    ratingCount = int.Parse(sqlDataReader["RAT_Count"].ToString());
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return ratingCount;
        }

        /// <summary>
        /// Get the number of incentive points
        /// </summary>
        internal static int GetIncentivePoints(Guid userId, Business.Dialog dialog)
        {
            int incentivePoints = 0;
            try
            {
                List<IncentivePoint> points = IncentivePointsManager.GetIncentivePointsHistory(userId);
                foreach (IncentivePoint point in points)
                    incentivePoints += point.TotalPoints;
            }
            finally
            {
            }
            return incentivePoints;
        }

        /// <summary>
        /// Get the number of created objects for a given type id
        /// </summary>
        internal static int GetObjectCountByType(Guid userId, Business.Dialog dialog)
        {
            int objectCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_DataObject_CountByUserAndType";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectTypeId", SqlDbType.Int));
                sqlConnection.Command.Parameters["@ObjectTypeId"].Value = dialog.Parameters["objectTypeId"];
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                    objectCount = int.Parse(sqlDataReader["OBJ_Count"].ToString());
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return objectCount;
        }

        /// <summary>
        /// Get the number of months since the user registration
        /// </summary>
        internal static int GetMembershipAgeInMonth(Guid userId, Business.Dialog dialog)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userId);
                TimeSpan timeSpan = DateTime.Now - user.CreationDate;
                return timeSpan.Days/30;
            }
            catch
            {
                return 0;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get the number of rated objects by other users
        /// </summary>
        internal static int GetReceivedRatingCount(Guid userId, Business.Dialog dialog)
        {
            int receivedRatingCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Ratings_ReceivedCount";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                    receivedRatingCount = int.Parse(sqlDataReader["RAT_Count"].ToString());
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return receivedRatingCount;
        }

        /// <summary>
        /// Get the number of objects commented by other users
        /// </summary>
        internal static int GetReceivedFeedbackCount(Guid userId, Business.Dialog dialog)
        {
            int receivedFeedbackCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Comments_ReceivedCount";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                    receivedFeedbackCount = int.Parse(sqlDataReader["COM_Count"].ToString());
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return receivedFeedbackCount;
        }

        /// <summary>
        /// Get the number of joined communities
        /// </summary>
        internal static int GetCommunityMembershipCount(Guid userId, Business.Dialog dialog)
        {
            int communityMembershipCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Community_MembershipCount";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                    communityMembershipCount = int.Parse(sqlDataReader["CUR_Count"].ToString());
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return communityMembershipCount;
        }

        /// <summary>
        /// Get the number of days since the user registration, if the user never changed its profile
        /// </summary>
        internal static int GetProfileUnchangedInDays(Guid userId, Business.Dialog dialog)
        {
            int daysWithoutChange = 0;
            try
            {
                Business.DataObjectUser dataObjectUser = Business.DataObject.Load<Business.DataObjectUser>(userId);
                TimeSpan lifeSpan = DateTime.Now - dataObjectUser.Inserted;
                TimeSpan unChangedTimeSpan = dataObjectUser.Updated - dataObjectUser.Inserted;
                if (dialog.Parameters["type"].ToLower() == "photo")
                {
                    if (string.IsNullOrEmpty(dataObjectUser.Image) && unChangedTimeSpan.Days < 1)
                    {
                        daysWithoutChange = lifeSpan.Days;
                    }
                }
                else if (dialog.Parameters["type"].ToLower() == "data")
                {
                    if (unChangedTimeSpan.Days < 1)
                    {
                        daysWithoutChange = lifeSpan.Days;
                    }
                }
            }
            finally
            {
            }
            return daysWithoutChange;
        }

        /// <summary>
        /// Get the number of upcoming birthdays for all activated friends
        /// </summary>
        internal static int GetFriendsBirthdays(Guid userId, Business.Dialog dialog)
        {
            int friendsBirthdayCount = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_UserFriend_GetBirthdates";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    try
                    {
                        DateTime birthDate = (DateTime) sqlDataReader["UPD_Birthday"];
                        DateTime birthDay = new DateTime(DateTime.Now.Year, birthDate.Month, birthDate.Day);
                        TimeSpan daysUntilBirthday = birthDay - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                        if (daysUntilBirthday.TotalDays > 0 && daysUntilBirthday.TotalDays <= 1)
                        {
                            friendsBirthdayCount++;
                        }
                    }
                    catch
                    {
                    }
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return friendsBirthdayCount;
        }

        /// <summary>
        /// Always returns 1
        /// </summary>
        internal static int GetUnconditional(Guid userId, Business.Dialog dialog)
        {
            return 1;
        }
    }
}