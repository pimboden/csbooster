// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace _4screen.CSB.DataAccess.Data
{
    internal class BadWordHelper
    {
        private static string GetActionsString(BadWordFilterActions action)
        {
            string actionsString = "";
            foreach (string actionName in Enum.GetNames(typeof (BadWordFilterActions)))
            {
                BadWordFilterActions currentAction = (BadWordFilterActions) Enum.Parse(typeof (BadWordFilterActions), actionName);
                if ((currentAction & action) == currentAction && currentAction != BadWordFilterActions.None)
                {
                    actionsString += currentAction + ", ";
                }
            }
            return actionsString.Substring(0, actionsString.Length - 2);
        }

        internal static void InformAdmin(BadWordFilterActions action, string value, string word, bool isExactMatch, Type type, FilterObjectTypes filterObjectType, Guid objectId, Guid userId)
        {
            try
            {
                string markedValue = Regex.Replace(value, @"(\w*" + word + @"\w*)(?=[^>]*?<)", "<u><font color=\"#AA0000\">$1</font></u>", RegexOptions.IgnoreCase);
                string exactValue = isExactMatch ? "Ja" : "Nein";
                string userLink = FilterEngine.GetFilterEngineConfig().ObjectLinks["User"] + userId.ToString();
                string objectLink = "";
                switch (filterObjectType)
                {
                    case FilterObjectTypes.DataObject:
                        if (FilterEngine.GetFilterEngineConfig().ObjectLinks.ContainsKey(type.ToString()))
                            objectLink = FilterEngine.GetFilterEngineConfig().ObjectLinks[type.ToString()] + objectId.ToString();
                        break;
                    case FilterObjectTypes.Comment:
                        if (FilterEngine.GetFilterEngineConfig().ObjectLinks.ContainsKey(type.ToString()))
                            objectLink = FilterEngine.GetFilterEngineConfig().ObjectLinks[type.ToString()] + objectId.ToString();
                        break;
                    case FilterObjectTypes.Profile:
                        objectLink = FilterEngine.GetFilterEngineConfig().ObjectLinks["Profile"] + objectId.ToString();
                        break;
                }

                SmtpSection smtpSection = (SmtpSection) ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpSection.From);
                foreach (KeyValuePair<string, string> adminEmail in FilterEngine.GetFilterEngineConfig().AdminEmailList)
                {
                    mailMessage.To.Add(new MailAddress(adminEmail.Key, adminEmail.Value));
                }
                mailMessage.Subject = "Bad Word Filter";
                StringBuilder bodyString = new StringBuilder(1000);
                bodyString.Append("<table border=0>");
                bodyString.Append("  <tr>");
                bodyString.Append("    <td>Aktion(en):</td><td>" + GetActionsString(action) + "</td>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td>Wort:</td><td>" + word + "<br/>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td>Genau:</td><td>" + exactValue + "<br/>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td valign=\"top\">Text:</td><td>" + markedValue + "<br/>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td>Typ:</td><td>" + filterObjectType + "<br/>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td>Object:</td><td><a href=\"" + objectLink + "\">" + objectId + "</a><br/>");
                bodyString.Append("  </tr><tr>");
                bodyString.Append("    <td>User:</td><td><a href=\"" + userLink + "\">" + userId + "</a><br/>");
                bodyString.Append("  </tr>");
                bodyString.Append("</table>");
                mailMessage.Body = bodyString.ToString();
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                new Exception("Error while sending bad word info mail", e);
            }
        }

        internal static void LockUser(Guid userId)
        {
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Membership_LockUser";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ApplicationName", SqlDbType.NVarChar));
                sqlConnection.Command.Parameters["@ApplicationName"].Value = _4screen.CSB.Common.Constants.APPLICATION_NAME;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlConnection.Command.ExecuteNonQuery();
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}