using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Data
{
    internal class UserExport
    {
        internal static UserExportList GetUserExportList()
        {
            UserExportList userExportList = new UserExportList();
            userExportList.Fields.Add("Benutzername");
            userExportList.Fields.Add("Email");
            userExportList.Fields.Add("Aktiviert");
            userExportList.Fields.Add("Gesperrt");
            userExportList.Fields.Add("Registrierung");
            userExportList.Fields.Add("Letze Anmeldung");
            userExportList.Fields.Add("Vorname");
            userExportList.Fields.Add("Nachname");
            userExportList.Fields.Add("Geschlecht");
            userExportList.Fields.Add("Geburtstag");

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT aspnet_Users.LoweredUserName, aspnet_Membership.LoweredEmail, aspnet_Membership.PasswordQuestion, aspnet_Membership.IsLockedOut, aspnet_Membership.CreateDate, aspnet_Membership.LastLoginDate, hiobj_User.Vorname, hiobj_User.Name, hiobj_User.Sex, hiobj_User.Birthday ");
            sb.Append("FROM aspnet_Membership ");
            sb.Append("INNER JOIN aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId ");
            sb.Append("LEFT OUTER JOIN hiobj_User ON aspnet_Membership.UserId = hiobj_User.OBJ_ID");

            SqlConnection connection = new SqlConnection(Helper.GetSiemeConnectionString());
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sb.ToString();
                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    List<string> item = new List<string>();
                    item.Add(sqlReader["LoweredUserName"].ToString());
                    item.Add(sqlReader["LoweredEmail"].ToString());
                    item.Add(sqlReader["PasswordQuestion"].ToString() == "-" ? "Ja" : "Nein");
                    item.Add(Convert.ToBoolean(sqlReader["IsLockedOut"]) ? "Ja" : "Nein");
                    item.Add(Convert.ToDateTime(sqlReader["CreateDate"]).ToShortDateString());
                    item.Add(Convert.ToDateTime(sqlReader["LastLoginDate"]).ToShortDateString());
                    item.Add(sqlReader["Vorname"] != DBNull.Value ? sqlReader["Vorname"].ToString() : "");
                    item.Add(sqlReader["Name"] != DBNull.Value ? sqlReader["Name"].ToString() : "");
                    item.Add(sqlReader["Sex"].ToString() == ";0;" ? "M" : sqlReader["Sex"].ToString() == ";1;" ? "W" : "");
                    item.Add(sqlReader["Birthday"] != DBNull.Value ? Convert.ToDateTime(sqlReader["Birthday"]).ToShortDateString() : "");
                    userExportList.Users.Add(item);
                }
                sqlReader.Close();
            }
            finally
            {
                connection.Close();
            }

            return userExportList;
        }
    }
}
