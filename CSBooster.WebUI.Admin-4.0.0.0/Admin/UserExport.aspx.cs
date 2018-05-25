using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class UserExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnExportUsersClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var userExport = _4screen.CSB.DataAccess.Business.UserExport.GetUserExportList();
            sb.AppendLine(string.Join(";", userExport.Fields.ToArray()));
            foreach (var user in userExport.Users)
            {
                List<string> fields = new List<string>();
                foreach (var field in user)
                    fields.Add(field.Replace(';', ','));
                sb.AppendLine(string.Join(";", user.ToArray()));
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=\"userlist.csv\"");
            Response.Clear();
            Response.Write(sb.ToString());
            /*byte[] buffer = System.Text.Encoding.GetEncoding("Windows-1252").GetBytes(sb.ToString());
            Response.OutputStream.Write(buffer, 0, buffer.Length);*/
            Response.End();
        }
    }
}
