//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Security;
using System.Reflection;
using System.Diagnostics;


namespace _4screen.CSB.MonitorService
{
  public enum Role
  {
    StatisticsViewer,
    UserManager
  }

  public class Authorization
  {
    private bool isAuthorized;
    private List<Role> roles = new List<Role>();
    private string version;

    public Authorization()
    {
    }

    public Authorization(string username, string password)
    {
      try
      {
        this.version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        MembershipUser membershipUser = Membership.GetUser(username);
        bool isAuthorized = Membership.ValidateUser(username, password);

        if (isAuthorized && !membershipUser.IsLockedOut)
        {
          this.IsAuthorized = true;
          try
          {
            InitRoles(membershipUser);
          }
          catch (Exception)
          {
            
          }
        }
        else
        {
          this.IsAuthorized = false;
        }
      }
      catch
      {
        this.IsAuthorized = false;
      }
    }

    public string Version
    {
      get { return this.version; }
      set { this.version = value; }
    }

    public bool IsAuthorized
    {
      get { return this.isAuthorized; }
      set { this.isAuthorized = value; }
    }

    public List<Role> Roles
    {
      get { return this.roles; }
      set { this.roles = value; }
    }

    private void InitRoles(MembershipUser membershipUser)
    {
      System.Data.SqlClient.SqlConnection sqlConnection;
      System.Data.SqlClient.SqlCommand sqlCommand;
      string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
      sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
      sqlConnection.Open();
      sqlCommand = new System.Data.SqlClient.SqlCommand();
      sqlCommand.Connection = sqlConnection;
      sqlCommand.CommandType = CommandType.Text;
      sqlCommand.CommandText = "SELECT Role FROM monitor_Roles WHERE UserId = '" + membershipUser.ProviderUserKey + "' AND IsMember = 'true'";
      System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
      while (sqlDataReader.Read())
      {
        this.Roles.Add((Role)Enum.Parse(typeof(Role), sqlDataReader["Role"].ToString()));
      }
      sqlDataReader.Close();
      sqlConnection.Close();
    }

    public static bool IsWebMethodPermitted(string username, string password, string webMethod)
    {
      try
      {
        MembershipUser membershipUser = Membership.GetUser(username);
        if (Membership.ValidateUser(username, password))
        {
          System.Data.SqlClient.SqlConnection sqlConnection;
          System.Data.SqlClient.SqlCommand sqlCommand;
          string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
          sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
          sqlConnection.Open();
          sqlCommand = new System.Data.SqlClient.SqlCommand();
          sqlCommand.Connection = sqlConnection;
          sqlCommand.CommandType = CommandType.Text;
          sqlCommand.CommandText = "SELECT dbo.monitor_WebMethodRoles.WebMethod " +
                                   "FROM (dbo.monitor_Roles INNER JOIN dbo.monitor_WebMethodRoles ON dbo.monitor_Roles.Role = dbo.monitor_WebMethodRoles.Role) INNER JOIN dbo.monitor_WebMethodsProhibited ON dbo.monitor_Roles.UserId = dbo.monitor_WebMethodsProhibited.UserId " +
                                   "WHERE dbo.monitor_WebMethodRoles.WebMethod <> dbo.monitor_WebMethodsProhibited.WebMethod AND dbo.monitor_Roles.UserId = '" + membershipUser.ProviderUserKey + "' AND dbo.monitor_Roles.IsMember = 'true'";
          System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
          while (sqlDataReader.Read())
          {
            if (sqlDataReader["WebMethod"].ToString() == webMethod)
              return true;
          }
          sqlDataReader.Close();
          sqlConnection.Close();
        }
      }
      catch
      {
        return false;
      }

      return false;
    }
  }
}
