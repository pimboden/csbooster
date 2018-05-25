//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System.Data;

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
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
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