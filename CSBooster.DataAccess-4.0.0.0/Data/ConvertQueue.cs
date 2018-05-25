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
    internal class ConvertQueue
    {
        private string strConn = Helper.GetSiemeConnectionString();

        internal void Insert(Business.ConvertQueue item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_Insert";
                cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)item.ObjectType));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_Status", SqlDbType.Int, (int)item.Status));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_UserEmail", SqlDbType.NVarChar, 250, item.UserEmail));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_VideoPreviewPictureTimepointSec", SqlDbType.Float, item.VideoPreviewPictureTimepointSec));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_EstimatedWorkTimeSec", SqlDbType.Int, item.EstimatedWorkTimeSec));

                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal bool LoadNextJob(Business.ConvertQueue item, string ServerName)
        {
            bool blnJobAvailable = false;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_LoadNextJob";
                cmd.Parameters.Add(SqlHelper.AddParameter("@ServerName", SqlDbType.NVarChar, 250, ServerName));

                Conn.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    FillObject(item, sqlReader);
                    blnJobAvailable = true;
                }
                else
                {
                    blnJobAvailable = false;
                }
                sqlReader.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return blnJobAvailable;
        }

        internal void Delete(Business.ConvertQueue item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_Delete";
                cmd.Parameters.Add(SqlHelper.AddParameter("@ID", SqlDbType.UniqueIdentifier, item.ID.Value));

                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal void Update(_4screen.CSB.DataAccess.Business.ConvertQueue item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_Update";
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_ID", SqlDbType.UniqueIdentifier, item.ID.Value));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_Status", SqlDbType.Int, (int)item.Status));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_InsertedDate", SqlDbType.DateTime, item.InsertedDate));

                if (item.LookID.HasValue)
                    cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_LookID", SqlDbType.UniqueIdentifier, item.LookID.Value));
                else
                    cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_LookID", SqlDbType.UniqueIdentifier, DBNull.Value));

                cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                cmd.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)item.ObjectType));

                if (item.LastTimeStamp > DateTime.MinValue)
                    cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_LastTimeStamp", SqlDbType.DateTime, item.LastTimeStamp));
                else
                    cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_LastTimeStamp", SqlDbType.DateTime, DBNull.Value));

                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_TryingCount", SqlDbType.Int, item.TryingCount));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_ServerName", SqlDbType.NVarChar, 250, item.ServerName));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_UserEmail", SqlDbType.NVarChar, 250, item.UserEmail));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_VideoPreviewPictureTimepointSec", SqlDbType.Float, item.VideoPreviewPictureTimepointSec));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_EstimatedWorkTimeSec", SqlDbType.Int, item.EstimatedWorkTimeSec));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_ConvertMessage", SqlDbType.NVarChar, 500, item.ConvertMessage));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_StatisticFileExtension", SqlDbType.NVarChar, 10, item.StatisticFileExtension));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_StatisticFileSizeByte", SqlDbType.Int, item.StatisticFileSizeByte));
                cmd.Parameters.Add(SqlHelper.AddParameter("@COQ_StatisticWorkTimeSec", SqlDbType.Int, item.StatisticWorkTimeSec));

                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        private void FillObject(_4screen.CSB.DataAccess.Business.ConvertQueue item, SqlDataReader sqlReader)
        {
            item.ID = sqlReader["COQ_ID"].ToString().ToGuid();
            item.Status = (MediaConvertedState)Convert.ToInt32(sqlReader["COQ_Status"]);
            item.InsertedDate = Convert.ToDateTime(sqlReader["COQ_InsertedDate"]);
            if (sqlReader["COQ_LookID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["COQ_LookID"].ToString()))
                item.LookID = sqlReader["COQ_LookID"].ToString().ToGuid();
            item.ObjectID = sqlReader["OBJ_ID"].ToString().ToGuid();
            item.ObjectType = Convert.ToInt32(sqlReader["OBJ_Type"]);

            if (sqlReader["COQ_LastTimeStamp"] != DBNull.Value)
                item.LastTimeStamp = Convert.ToDateTime(sqlReader["COQ_LastTimeStamp"]);

            item.TryingCount = Convert.ToInt32(sqlReader["COQ_TryingCount"]);
            item.ServerName = sqlReader["COQ_ServerName"].ToString();
            item.UserEmail = sqlReader["COQ_UserEmail"].ToString();

            if (sqlReader["COQ_VideoPreviewPictureTimepointSec"] != DBNull.Value)
                item.VideoPreviewPictureTimepointSec = Convert.ToDouble(sqlReader["COQ_VideoPreviewPictureTimepointSec"]);

            item.EstimatedWorkTimeSec = Convert.ToInt32(sqlReader["COQ_EstimatedWorkTimeSec"]);
            item.ConvertMessage = sqlReader["COQ_ConvertMessage"].ToString();

            if (sqlReader["COQ_StatisticFileExtension"] != DBNull.Value)
                item.StatisticFileExtension = sqlReader["COQ_StatisticFileExtension"].ToString();

            if (sqlReader["COQ_StatisticFileSizeByte"] != DBNull.Value)
                item.StatisticFileSizeByte = Convert.ToInt32(sqlReader["COQ_StatisticFileSizeByte"]);

            if (sqlReader["COQ_StatisticWorkTimeSec"] != DBNull.Value)
                item.StatisticWorkTimeSec = Convert.ToInt32(sqlReader["COQ_StatisticWorkTimeSec"]);
        }

        internal List<_4screen.CSB.DataAccess.Business.ConvertQueue> LoadRunningJobs()
        {
            List<Business.ConvertQueue> list = new List<Business.ConvertQueue>();

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_LoadRunningJobs";

                Conn.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    Business.ConvertQueue item = new Business.ConvertQueue();
                    FillObject(item, sqlReader);
                    list.Add(item);
                }
                sqlReader.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return list;
        }

        internal _4screen.CSB.DataAccess.Business.ConvertQueue LoadLastTimestamp(string ServerName)
        {
            Business.ConvertQueue item = null;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_LoadLastTimestamp";
                cmd.Parameters.Add(SqlHelper.AddParameter("@ServerName", SqlDbType.NVarChar, 250, ServerName));

                Conn.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    item = new Business.ConvertQueue();
                    FillObject(item, sqlReader);
                }
                sqlReader.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return item;
        }

        internal List<_4screen.CSB.DataAccess.Business.ConvertQueue> LoadWaitingJobs()
        {
            List<Business.ConvertQueue> list = new List<Business.ConvertQueue>();

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hisp_ConvertQueue_LoadWaitingJobs";

                Conn.Open();
                SqlDataReader sqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    Business.ConvertQueue item = new Business.ConvertQueue();
                    FillObject(item, sqlReader);
                    list.Add(item);
                }
                sqlReader.Close();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

            return list;
        }
    } // END CLASS
} // END NAMESPACE