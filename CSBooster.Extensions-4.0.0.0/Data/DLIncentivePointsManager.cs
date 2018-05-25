using System;
using System.Data;
using System.Data.SqlClient;

namespace _4screen.CSB.Extensions.Data
{
    internal class DLIncentivePointsManager
    {
        private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;


        public void DoAddIncentivePoints(int pointsToAdd, string url, string rule, string description, string timestamp, string pointType, Guid userID, string objectID)
        {
            SqlCommand SetData = new SqlCommand();
            SqlConnection Conn = new SqlConnection(strConn);
            SetData.Connection = Conn;
            SetData.CommandType = CommandType.StoredProcedure;
            SetData.CommandText = "hisp_IncentivePoints_AddPoints";
            SetData.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)); //0
            SetData.Parameters.Add(new SqlParameter("@NumPoints", SqlDbType.Int)); //1
            SetData.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 2000)); //2
            SetData.Parameters.Add(new SqlParameter("@Minutes", SqlDbType.Int)); //3
            SetData.Parameters.Add(new SqlParameter("@URL", SqlDbType.NVarChar, 500)); //4
            SetData.Parameters.Add(new SqlParameter("@Rule", SqlDbType.NVarChar, 100)); //5
            SetData.Parameters.Add(new SqlParameter("@PointType", SqlDbType.NVarChar, 30)); //6
            SetData.Parameters.Add(new SqlParameter("@ObjectID", SqlDbType.UniqueIdentifier)); //7

            SetData.Parameters[0].Value = userID;
            SetData.Parameters[1].Value = pointsToAdd; //1
            SetData.Parameters[2].Value = description; //2

            if (timestamp == "*")
            {
                SetData.Parameters[3].Value = -999; //3
            }
            else
            {
                SetData.Parameters[3].Value = Convert.ToInt32(timestamp); //3
            }
            SetData.Parameters[4].Value = url; //4
            SetData.Parameters[5].Value = rule; //5
            SetData.Parameters[6].Value = pointType; //6

            if (objectID.Length > 0)
                SetData.Parameters[7].Value = new Guid(objectID);
            else
                SetData.Parameters[7].Value = DBNull.Value;

            try
            {
                Conn.Open();
                SetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        public IDataReader GetIncentivePointsHistory(Guid UserId)
        {
            SqlCommand SetData = new SqlCommand();
            SqlConnection Conn = new SqlConnection(strConn);
            SetData.Connection = Conn;
            SetData.CommandType = CommandType.StoredProcedure;
            SetData.CommandText = "hisp_IncentivePoints_GetPoints";
            SetData.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)); //0
            SetData.Parameters[0].Value = UserId;

            try
            {
                Conn.Open();
                return SetData.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                throw ex;
            }
        }

        public int GetIncentiveTotalPoints(Guid UserId)
        {
            SqlCommand SetData = new SqlCommand();
            SqlConnection Conn = new SqlConnection(strConn);
            SetData.Connection = Conn;
            SetData.CommandType = CommandType.StoredProcedure;
            SetData.CommandText = "hisp_IncentivePoints_GetPoint";
            SetData.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)); //0
            SetData.Parameters[0].Value = UserId;

            try
            {
                Conn.Open();
                return Convert.ToInt32(SetData.ExecuteScalar());
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        public int GetIncentiveTotalPointsByType(Guid UserId, string PointType)
        {
            SqlCommand SetData = new SqlCommand();
            SqlConnection Conn = new SqlConnection(strConn);
            SetData.Connection = Conn;
            SetData.CommandType = CommandType.StoredProcedure;
            SetData.CommandText = "hisp_IncentivePoints_GetPoint";
            SetData.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)); //0
            SetData.Parameters.Add(new SqlParameter("@PointType", SqlDbType.NVarChar, 30)); //1
            SetData.Parameters[0].Value = UserId;
            SetData.Parameters[1].Value = PointType;

            try
            {
                Conn.Open();
                return Convert.ToInt32(SetData.ExecuteScalar());
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        public void ClearHistory(Guid UserId)
        {
            SqlCommand SetData = new SqlCommand();
            SqlConnection Conn = new SqlConnection(strConn);
            SetData.Connection = Conn;
            SetData.CommandType = CommandType.StoredProcedure;
            SetData.CommandText = "hisp_IncentivePoints_ClearHistory";
            SetData.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)); //0
            SetData.Parameters[0].Value = UserId;

            try
            {
                Conn.Open();
                SetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }
    }
}