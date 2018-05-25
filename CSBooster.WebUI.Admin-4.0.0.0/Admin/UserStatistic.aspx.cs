using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class UserStatistic : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnUserStatisticClick(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "3")
            {
                RenderUserActivity();
            }
            else
            {
                RenderUserStatistic();
            }
        }

        private void RenderUserActivity()
        {
            DateTime fromDate = DateTime.Now.AddYears(-20);
            if (RadDateFrom.SelectedDate.HasValue)
                fromDate = RadDateFrom.SelectedDate.Value;

            DateTime toDate = DateTime.Now.AddDays(1);
            if (RadDateTo.SelectedDate.HasValue)
                toDate = RadDateTo.SelectedDate.Value;

            List<UserStatisticAction> include = new List<UserStatisticAction>();
            include.Add(new UserStatisticAction() { ObjectType = 3 });
            include.Add(new UserStatisticAction() { ObjectType = 4 });
            include.Add(new UserStatisticAction() { ObjectType = 11 });
            include.Add(new UserStatisticAction() { ObjectType = 15 });
            include.Add(new UserStatisticAction() { ObjectType = 19 });
            include.Add(new UserStatisticAction() { ObjectType = 24 });
            include.Add(new UserStatisticAction() { ObjectType = 25 });

            Dictionary<int, string> objectNames = new Dictionary<int, string>(); 

            StringBuilder sb = new StringBuilder(5392);
            List<UserStatisticActivity> userActivity = _4screen.CSB.DataAccess.Business.UserStatistic.GetUserActivityList(txtUser.Text.Trim(), fromDate, toDate, include);
            sb.AppendLine(string.Format("Datum;Objekt;Titel;Aktion von '{0}';", txtUser.Text.Trim()));

            int count = 0;
            foreach (var item in userActivity)
            {
                count++;
                string action = "";
                if (item.Action == "Viewed")
                    action = "besucht";
                else if (item.Action == "Inserted")
                    action = "erstellt";
                else if (item.Action == "Rated")
                    action = "bewertet";
                else if (item.Action == "Deleted")
                    action = "gelöscht";
                else if (item.Action == "Commented")
                    action = "kommentiert";
                else if (item.Action == "Updated")
                    action = "bearbeitet";
                else
                    action = item.Action;

                if (!objectNames.ContainsKey(item.ObjectType))
                {
                    objectNames.Add(item.ObjectType, Helper.GetObjectName(item.ObjectType, true));
                }

                sb.AppendLine(string.Format("{0};{1};{2};{3};", item.Date.ToString("dd.MM.yyyy HH:mm:ss"), objectNames[item.ObjectType], item.Title.Replace(";", ","), action));
                if (count == 10000)
                {
                    sb.AppendLine(string.Format("{0};{1};{2};{3};", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), "", "Maximal 10000 aktivitäten werden exportiert", ""));
                    break;
                }
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=\"useractivity.csv\"");
            Response.Clear();
            //Response.Write(sb.ToString());
            byte[] buffer = System.Text.Encoding.GetEncoding("Windows-1252").GetBytes(sb.ToString());
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();

        }

        private void RenderUserStatistic()
        {
            DateTime fromDate = DateTime.Now.AddYears(-20);
            if (RadDateFrom.SelectedDate.HasValue)
                fromDate = RadDateFrom.SelectedDate.Value;

            DateTime toDate = DateTime.Now.AddDays(1);
            if (RadDateTo.SelectedDate.HasValue)
                toDate = RadDateTo.SelectedDate.Value;

            bool allUser = ddlType.SelectedValue == "1";

            List<UserStatisticAction> include = new List<UserStatisticAction>();
            include.Add(new UserStatisticAction() { ObjectType = 3, ActionName = "Viewed", Column = 0 });
            include.Add(new UserStatisticAction() { ObjectType = 4, ActionName = "Viewed", Column = 1 });
            include.Add(new UserStatisticAction() { ObjectType = 11, ActionName = "Viewed", Column = 2 });
            include.Add(new UserStatisticAction() { ObjectType = 15, ActionName = "Viewed", Column = 3 });
            include.Add(new UserStatisticAction() { ObjectType = 24, ActionName = "Viewed", Column = 4 });
            include.Add(new UserStatisticAction() { ObjectType = 3, ActionName = "Inserted", Column = 5 });
            include.Add(new UserStatisticAction() { ObjectType = 4, ActionName = "Inserted", Column = 6 });
            include.Add(new UserStatisticAction() { ObjectType = 11, ActionName = "Inserted", Column = 7 });
            include.Add(new UserStatisticAction() { ObjectType = 15, ActionName = "Inserted", Column = 8 });
            include.Add(new UserStatisticAction() { ObjectType = 24, ActionName = "Inserted", Column = 9 });
            include.Add(new UserStatisticAction() { ObjectType = 25, ActionName = "Inserted", Column = 10 });
            include.Add(new UserStatisticAction() { ObjectType = 3, ActionName = "Rated", Column = 11 });
            include.Add(new UserStatisticAction() { ObjectType = 4, ActionName = "Rated", Column = 12 });
            include.Add(new UserStatisticAction() { ObjectType = 15, ActionName = "Rated", Column = 13 });
            include.Add(new UserStatisticAction() { ObjectType = 24, ActionName = "Rated", Column = 14 });

            StringBuilder sb = new StringBuilder(1000);
            var userStatistic = _4screen.CSB.DataAccess.Business.UserStatistic.GetUserStatisticList(txtUser.Text.Trim(), fromDate, toDate, allUser, include);
            sb.Append("User;");
            foreach (var column in userStatistic.Columns)
            {
                string name = Helper.GetObjectName(column.ObjectType, false);
                string action = "besucht";
                if (column.ActionName == "Inserted")
                    action = "erstellt";
                else if (column.ActionName == "Rated")
                    action = "bewertet";

                sb.AppendFormat("{0} {1};", name, action);
            }
            sb.AppendLine();

            foreach (var user in userStatistic.Users)
            {
                sb.Append(user.Nickname + ";");
                for (int i = 0; i < userStatistic.Columns.Count; i++)
                {
                    UserStatisticAction action = user.GetAction(i);
                    if (action != null)
                        sb.AppendFormat("{0};", action.Amount);
                    else
                        sb.Append("0;");
                }
                sb.AppendLine();
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=\"userstatistic.csv\"");
            Response.Clear();
            Response.Write(sb.ToString());
            /*byte[] buffer = System.Text.Encoding.GetEncoding("Windows-1252").GetBytes(sb.ToString());
            Response.OutputStream.Write(buffer, 0, buffer.Length);*/
            Response.End();
        }

    }
}
