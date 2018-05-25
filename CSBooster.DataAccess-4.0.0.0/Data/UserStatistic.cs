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
    internal class UserStatistic
    {
        internal static UserStatisticList GetUserStatisticList(string nickName, DateTime fromDate, DateTime toDate, bool allUser, List<UserStatisticAction> includeAction)
        {
            UserStatisticList userStatisticList = new UserStatisticList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT hitbl_DataObject_OBJ.USR_Nickname, ISNULL(hitbl_LogObjectActions_LOA.OBJ_Type, 0) AS OBJ_Type, hitbl_LogObjectActions_LOA.Action, COUNT(hitbl_LogObjectActions_LOA.DateInsert) AS Anzahl");
            sb.AppendLine("FROM hitbl_LogObjectActions_LOA RIGHT OUTER JOIN hitbl_LogSession_LSE ON hitbl_LogObjectActions_LOA.LSE_SessionID = hitbl_LogSession_LSE.LSE_SessionID RIGHT OUTER JOIN hitbl_DataObject_OBJ ON hitbl_LogSession_LSE.USR_ID = hitbl_DataObject_OBJ.OBJ_ID");
            sb.AppendLine("WHERE (hitbl_LogObjectActions_LOA.DateInsert BETWEEN @fromDate AND @toDate)");
            if (!string.IsNullOrEmpty(nickName))
                sb.AppendLine("AND (hitbl_DataObject_OBJ.USR_Nickname LIKE @Nickname)");
            sb.AppendLine("GROUP BY hitbl_DataObject_OBJ.USR_Nickname, hitbl_LogObjectActions_LOA.OBJ_Type, hitbl_LogObjectActions_LOA.Action");
            if (includeAction.Count > 0)
            {
                sb.Append("HAVING (");
                string or = "";
                foreach (UserStatisticAction item in includeAction)
                {
                    if (item.ObjectType > 0)
                    {
                        sb.AppendFormat("{0} (hitbl_LogObjectActions_LOA.OBJ_Type = {1} AND hitbl_LogObjectActions_LOA.Action = '{2}')", or, item.ObjectType, item.ActionName);
                    }
                    else
                        sb.AppendFormat("{0} (hitbl_LogObjectActions_LOA.Action = '{1}')", or, item.ActionName);

                    or = " OR ";
                    userStatisticList.Columns.Add(item);
                }
                sb.AppendLine(")");
            }

            sb.AppendLine("ORDER BY hitbl_DataObject_OBJ.USR_Nickname");

            SqlConnection connection = new SqlConnection(Helper.GetSiemeConnectionString());
            try
            {
                SqlCommand command = new SqlCommand(sb.ToString(), connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 300;  // 5 Minuten 
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                if (!string.IsNullOrEmpty(nickName))
                    command.Parameters.AddWithValue("@Nickname", nickName);

                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    string userName = sqlReader["USR_Nickname"].ToString();
                    int objectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
                    string actionName = sqlReader["Action"].ToString();
                    int anzahl = Convert.ToInt32(sqlReader["Anzahl"]);

                    if (!allUser && anzahl == 0)
                    {
                        continue;
                    }

                    string actionKey = string.Format("{0}{1}", objectType, actionName);

                    int column = userStatisticList.GetColumn(actionKey);

                    UserStatisticUser user = userStatisticList.GetUser(userName);
                    if (user == null)
                    {
                        user = new UserStatisticUser();
                        user.Nickname = userName;
                        userStatisticList.Users.Add(user);
                    }

                    if (anzahl > 0)
                    {
                        UserStatisticAction action = user.GetAction(actionKey);
                        if (action == null)
                        {
                            action = new UserStatisticAction();
                            action.ActionName = actionName;
                            action.ObjectType = objectType;
                            action.Action = actionKey;
                            action.Amount = anzahl;
                            action.Column = column;
                            user.Actions.Add(action);
                        }
                    }
                }
                sqlReader.Close();
            }
            finally
            {
                connection.Close();
            }

            return userStatisticList;
        }

        internal static List<UserStatisticActivity> GetUserActivityList(string nickName, DateTime fromDate, DateTime toDate, List<UserStatisticAction> includeAction)
        {
            List<UserStatisticActivity> list = new List<UserStatisticActivity>();

            if (string.IsNullOrEmpty(nickName))
                return list;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT TOP(10001) ISNULL(hitbl_LogObjectActions_LOA.OBJ_Type, 0) AS OBJ_Type, hitbl_LogObjectActions_LOA.Action, hitbl_LogObjectActions_LOA.DateInsert, hitbl_DataObject_OBJ_1.OBJ_Title");
            sb.AppendLine("FROM hitbl_DataObject_OBJ INNER JOIN hitbl_LogSession_LSE ON hitbl_DataObject_OBJ.OBJ_ID = hitbl_LogSession_LSE.USR_ID INNER JOIN hitbl_LogObjectActions_LOA ON hitbl_LogSession_LSE.LSE_SessionID = hitbl_LogObjectActions_LOA.LSE_SessionID INNER JOIN hitbl_DataObject_OBJ AS hitbl_DataObject_OBJ_1 ON hitbl_LogObjectActions_LOA.OBJ_ID = hitbl_DataObject_OBJ_1.OBJ_ID");
            sb.AppendLine("WHERE (hitbl_LogObjectActions_LOA.DateInsert BETWEEN @fromDate AND @toDate)");
            sb.AppendLine("AND (hitbl_DataObject_OBJ.USR_Nickname LIKE @Nickname)");
            if (includeAction.Count > 0)
            {
                sb.AppendLine("AND(");
                string or = "";
                foreach (UserStatisticAction item in includeAction)
                {
                    sb.Append(string.Format("{0} hitbl_LogObjectActions_LOA.OBJ_Type = {1}", or, item.ObjectType));
                    or = "OR";
                }
                sb.AppendLine(")");
            }
            sb.AppendLine("ORDER BY hitbl_LogObjectActions_LOA.DateInsert, hitbl_LogObjectActions_LOA.Action");

            SqlConnection connection = new SqlConnection(Helper.GetSiemeConnectionString());
            try
            {
                SqlCommand command = new SqlCommand(sb.ToString(), connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 300;  // 5 Minuten 
                command.Parameters.AddWithValue("@fromDate", fromDate);
                command.Parameters.AddWithValue("@toDate", toDate);
                command.Parameters.AddWithValue("@Nickname", nickName);

                connection.Open();
                SqlDataReader sqlReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlReader.Read())
                {
                    UserStatisticActivity item = new UserStatisticActivity();
                    item.Action = sqlReader["Action"].ToString();
                    item.Title = sqlReader["OBJ_Title"].ToString();
                    item.ObjectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
                    item.Date = Convert.ToDateTime(sqlReader["DateInsert"]);
                    list.Add(item);
                }
                sqlReader.Close();
            }
            finally
            {
                connection.Close();
            }

            return list;
        }
    }
}
