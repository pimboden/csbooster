// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls
{
    public partial class Header : UserControl
    {
        public string Text { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string loginLink = string.Format("<a class='headerLink' target='_self' href='/M/Login.aspx?ReturnUrl=/MPDefault.aspx&amp;ref={1}'>{0}</a>", GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandLogin"), Guid.NewGuid());
            string dashboardLink = string.Format("<a class='headerLink' target='_self' href='/M/Admin/Dashboard.aspx'>{0}</a>", GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("TitleDashboard"));

            Text = UserProfile.Current.IsAnonymous ? Text.Replace("##Dashboard##", loginLink) : Text.Replace("##Dashboard##", dashboardLink);

            if (!string.IsNullOrEmpty(Text))
                this.Controls.Add(new LiteralControl(Text));
        }
    }
}