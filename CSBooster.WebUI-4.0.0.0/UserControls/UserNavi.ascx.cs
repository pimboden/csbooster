// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web;
using _4screen.CSB.Common;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class UserNavi : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI");

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            LnkRss.NavigateUrl = "/pages/other/rssfeed.aspx" + Request.Url.Query;
            LnkRss.ID = null;

            if (!Request.IsAuthenticated)
            {
                string registerLink = "/Pages/Popups/UserRegistration.aspx";
                if (SiteConfig.UsePopupWindows)
                    sb.AppendFormat("<li class=\"userNavRegister\"><a href=\"javascript:radWinOpen('{0}', '{1}', 640, 400, false, null, 'wizardWin')\">{1}</a></li>", registerLink, language.GetString("CommandNaviRegister").StripForScript());
                else
                    sb.AppendFormat("<li class=\"userNavRegister\"><a href=\"{0}\">{1}</a></li>", registerLink, language.GetString("CommandNaviRegister"));

                sb.AppendFormat("<li><a href=\"/Default.aspx?CN=LoginPage&ReturnUrl={0}\">{1}</a></li>", HttpUtility.UrlEncode(Request.RawUrl), language.GetString("CommandNaviLogin"));
            }
            else
            {
                LnkProfile.NavigateUrl = Helper.GetDetailLink("User", UserProfile.Current.Nickname) + "/dashboard";
                LnkProfile.ID = null;
                LiProfile.Visible = true;
                LiProfile.ID = null;
                sb.AppendFormat("<li><a href=\"/Pages/Other/Logout.ashx\"><b>{0}</b> {1}</a></li>", UserDataContext.GetUserDataContext().Nickname, language.GetString("CommandNaviLogout"));
            }

            sb.AppendFormat("<li><a href=\"/Help\">{0}</a></li>", language.GetString("CommandNaviHelp"));

            this.LitMenu.Text = sb.ToString();
        }
    }
}