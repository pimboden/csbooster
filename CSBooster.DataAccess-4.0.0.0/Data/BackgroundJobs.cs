// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class BackgroundJobs
    {
        public static void ExecuteStoreProcedure(string name)
        {
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = name;
                GetData.CommandTimeout = 3600; 

                Conn.Open();
                GetData.ExecuteNonQuery();
                Conn.Close();  
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }
   }
}