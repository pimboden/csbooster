using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _4screen.CSB.DataAccess.Data
{
    internal class FriendHanlder
    {
        private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        internal Dictionary<int, string> GetFriendTypes(string langCode)
        {
            CSBooster_DataAccessDataContext csbDAC = new CSBooster_DataAccessDataContext();
            return csbDAC.hisp_UserFriendType_GetTypes(langCode).ToDictionary(x => x.UFR_TypeID, x => x.UFT_TypeName);
        }

        internal bool IsFriend(Guid userId, Guid friendId)
        {
            bool blnRetVal = false;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_IsFriend";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                Conn.Open();
                blnRetVal = Convert.ToBoolean(GetData.ExecuteScalar());
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
            return blnRetVal;
        }

        internal void DeleteFriend(Guid userId, Guid friendId)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_RemoveFriend";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        internal void BirthdayNotification(string userId, string friendId, int allow)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_SetBirthdayNotification";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, new Guid(userId)));
                if (!string.IsNullOrEmpty(friendId))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, new Guid(friendId)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Allow", SqlDbType.Int, allow));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        internal void BlockFriend(Guid userId, Guid friendId, int blocked)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_Block";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Blocked", SqlDbType.Int, blocked));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        internal void Save(Guid userId, Guid friendId, int blocked, int friendType, int allowBirthdayNotification)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_SaveFriend";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Blocked", SqlDbType.Int, blocked));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendType", SqlDbType.Int, friendType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Allow", SqlDbType.Int, allowBirthdayNotification));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        internal bool IsBlocked(Guid userId, Guid friendId)
        {
            bool blnRetVal = false;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_IsBlocked";
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                Conn.Open();
                blnRetVal = Convert.ToBoolean(GetData.ExecuteScalar());
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
            return blnRetVal;
        }

        internal void TransferFriendAsCommunityMember(Guid friendId, Guid communityID)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserFriend_TransferAsCommunityMember";
                GetData.Parameters.Add(SqlHelper.AddParameter("@FriendID", SqlDbType.UniqueIdentifier, friendId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@CtyID", SqlDbType.UniqueIdentifier, communityID));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != System.Data.ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }
    }
}