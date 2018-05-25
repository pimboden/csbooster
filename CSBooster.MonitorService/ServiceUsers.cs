//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//  2007.08.17  1.0.0.2  AW  Try Catch Finally fixed
//  2007.11.06  1.0.0.3  AW  Query fixed because of db changes
//*****************************************************************************************

using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Security;
using System.Configuration;

namespace _4screen.CSB.MonitorService
{
   public class ServiceUsers
   {
      public List<User> GetUsers(string username, string email, bool isLocked)
      {
			string ASPAppName = ConfigurationManager.AppSettings["ASPAppName"];
         List<User> userList = new List<User>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            string checkLockClause = "";
            if (isLocked)
            {
               checkLockClause = "AND IsLockedOut = '" + isLocked + "'";
            }

            sqlConnection.Command.CommandText = "SELECT TOP 100 dbo.aspnet_Users.LoweredUserName, dbo.aspnet_Membership.LoweredEmail, dbo.aspnet_Membership.IsLockedOut, dbo.aspnet_Membership.LastLoginDate, dbo.aspnet_Membership.LastLockoutDate, dbo.hitbl_UserProfileData_UPD.UPD_Vorname, dbo.hitbl_UserProfileData_UPD.UPD_Name " +
                                                "FROM ((dbo.aspnet_Applications INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Applications.ApplicationId = dbo.aspnet_Membership.ApplicationId) INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId) INNER JOIN dbo.hitbl_UserProfileData_UPD ON dbo.aspnet_Users.UserId = dbo.hitbl_UserProfileData_UPD.USR_ID " +
																"WHERE dbo.aspnet_Applications.ApplicationName='" + ASPAppName + "' AND LoweredUserName LIKE '%" + username.ToLower() + "%' AND LoweredEmail LIKE '%" + email + "%'" + checkLockClause;
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               string fullname = string.Empty;
               if (!string.IsNullOrEmpty(sqlDataReader["UPD_Vorname"].ToString()))
                  fullname += (string)sqlDataReader["UPD_Vorname"];
               if (!string.IsNullOrEmpty(sqlDataReader["UPD_Name"].ToString()))
                  fullname += " " + (string)sqlDataReader["UPD_Name"];
               userList.Add(new User((string)sqlDataReader["LoweredUserName"], (string)sqlDataReader["LoweredEmail"], fullname, (DateTime)sqlDataReader["LastLoginDate"], (DateTime)sqlDataReader["LastLockoutDate"], (bool)sqlDataReader["IsLockedOut"]));
            }
         }
         catch (Exception e)
         {
            throw new SoapException("Anfrage konnte nicht bearbeitet werden -> " + e.Message, SoapException.ServerFaultCode);
         }
         finally
         {
            try { sqlDataReader.Close(); }
            finally { sqlConnection.Close(); }
         }

         return userList;
      }

      public bool LockUser(string username)
      {
			string ASPAppName = ConfigurationManager.AppSettings["ASPAppName"];
         SqlConnection sqlConnection = new SqlConnection();
         try
         {
            sqlConnection.Command.CommandText = "UPDATE dbo.aspnet_Membership SET dbo.aspnet_Membership.IsLockedOut = 'True', dbo.aspnet_Membership.LastLockoutDate = getdate() " +
                                                "WHERE dbo.aspnet_Membership.UserId " +
                                                "    IN (SELECT dbo.aspnet_Membership.UserId " +
                                                "    FROM (dbo.aspnet_Applications INNER JOIN dbo.aspnet_Membership ON dbo.aspnet_Applications.ApplicationId = dbo.aspnet_Membership.ApplicationId) INNER JOIN dbo.aspnet_Users ON dbo.aspnet_Membership.UserId = dbo.aspnet_Users.UserId " +
																"    WHERE (((dbo.aspnet_Applications.ApplicationName)='" + ASPAppName + "') AND ((dbo.aspnet_Users.LoweredUserName)='" + username + "')))";
            sqlConnection.Command.ExecuteNonQuery();
            return true;
         }
         catch
         {
            return false;
         }
         finally { sqlConnection.Close(); }
      }

      public bool UnlockUser(string username)
      {
         MembershipUser user = Membership.GetUser(username);
         return user.UnlockUser();
      }

      public bool SetUserEmail(string username, string email)
      {
         MembershipUser user = Membership.GetUser(username);
         user.Email = email;
         Membership.UpdateUser(user);
         return true;
      }
   }
}
