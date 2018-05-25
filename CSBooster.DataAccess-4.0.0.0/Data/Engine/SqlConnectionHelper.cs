// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class SqlConnectionHelper
    {
        private System.Data.SqlClient.SqlConnection sqlConnection;
        private System.Data.SqlClient.SqlCommand sqlCommand;

        internal SqlConnectionHelper()
        {
            try
            {
                string connectionString = Helper.GetSiemeConnectionString();
                sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                sqlConnection.Open();
                sqlCommand = new System.Data.SqlClient.SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
            }
            catch
            {
            }
        }

        internal System.Data.SqlClient.SqlCommand Command
        {
            get { return sqlCommand; }
        }

        internal void Close()
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}