//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//  2007.08.17  1.0.0.2  AW  Try Catch Finally fixed
//*****************************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _4screen.CSB.MonitorService
{
   public class SqlConnection
   {
      private System.Data.SqlClient.SqlConnection sqlConnection;
      private System.Data.SqlClient.SqlCommand sqlCommand;

      public SqlConnection()
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
         catch { }
      }

      public System.Data.SqlClient.SqlCommand Command
      {
         get { return this.sqlCommand; }
      }

      public void Close()
      {
         try
         {
            sqlConnection.Close();
         }
         catch { }
      }
   }
}
