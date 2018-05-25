// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M
{
    public partial class Login : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogin.Text = language.GetString("CommandLogin");

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.MyContent);
        }

        protected void OnLoginClick(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(txtLogin.Text, txtPassword.Text))
            {
                MembershipUser usr = Membership.GetUser(txtLogin.Text, false);
                UserProfile currProf = UserProfile.GetProfile(usr.UserName);
                currProf.Nickname = usr.UserName;
                currProf.UserId = new Guid(usr.ProviderUserKey.ToString());
                currProf.Save();

                if (usr.PasswordQuestion == "-")
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("USER_LOGIN", UserDataContext.GetUserDataContext(usr.UserName));

                    if (!string.IsNullOrEmpty(usr.Comment))
                        Response.Redirect(string.Format("{0}", usr.Comment));
                    else if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        Response.Redirect("~/");
                    else
                        Response.Redirect(Server.UrlDecode(Request.QueryString["ReturnUrl"]));
                }
                else
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageAccountNotActivated");
                }
            }
            else
            {
                pnlStatus.Visible = true;
                litStatus.Text = language.GetString("MessageLoginFailed");
            }
        }
    }
}