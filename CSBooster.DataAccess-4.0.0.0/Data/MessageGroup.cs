// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class MessageGroup
    {
        private string strConn = Helper.GetSiemeConnectionString();

        public void Load(List<Business.MessageGroup> list, string userID, MessageGroupTypes groupType, bool withCount)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserMessagesGroup_Load";

                GetData.Parameters.Add(SqlHelper.AddParameter("@ASP_UserId", SqlDbType.UniqueIdentifier, new Guid(userID)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Type", SqlDbType.Int, (int) groupType));

                Conn.Open();
                SqlDataReader sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    Business.MessageGroup item = new Business.MessageGroup(sqlReader["ASP_UserId"].ToString(), (MessageGroupTypes) int.Parse(sqlReader["MGR_Type"].ToString()), int.Parse(sqlReader["MGR_GroupId"].ToString()), sqlReader["MGR_Title"].ToString(), 0);
                    list.Add(item);
                }
                sqlReader.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public void Save(Business.MessageGroup item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                Conn.Open();
                if (item.Dirty)
                {
                    SqlCommand GetData = new SqlCommand();

                    GetData.Connection = Conn;
                    GetData.CommandType = CommandType.StoredProcedure;
                    GetData.CommandText = "hisp_UserMessagesGroup_Update";
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ASP_UserId", SqlDbType.UniqueIdentifier, new Guid(item.UserID)));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Type", SqlDbType.Int, (int) item.GroupType));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_GroupId", SqlDbType.Int, (int) item.GroupID));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Title", SqlDbType.NVarChar, item.Title));

                    item.GroupID = int.Parse(GetData.ExecuteScalar().ToString());
                    item.Dirty = false;
                }
            }
            finally
            {
                Conn.Close();
            }
        }

        public bool Exist(string userID, MessageGroupTypes groupType, string title)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserMessagesGroup_Exist";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ASP_UserId", SqlDbType.UniqueIdentifier, new Guid(userID)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Type", SqlDbType.Int, (int) groupType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Title", SqlDbType.NVarChar, title));

                Conn.Open();
                if (GetData.ExecuteScalar().ToString() == "1")
                    return true;
                else
                    return false;
            }
            finally
            {
                Conn.Close();
            }
        }

        public void Delete(string userID, MessageGroupTypes groupType, int groupID)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_UserMessagesGroup_Delete";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ASP_UserId", SqlDbType.UniqueIdentifier, new Guid(userID)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_Type", SqlDbType.Int, (int) groupType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@MGR_GroupId", SqlDbType.Int, (int) groupID));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}