// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Claims;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Net;
using _4screen.Utils.Web;
using ExtremeSwank.OpenId;
using ExtremeSwank.OpenId.PlugIns.Extensions;
using Microsoft.IdentityModel.TokenProcessor;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class Login : System.Web.UI.UserControl, IMinimalControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public bool HasContent { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        protected CheckBox cbRememberMe;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            HasContent = true;

            if (_4screen.CSB.Common.SiteConfig.GetSetting("ShowRememberLogin").ToLower() == "true")
            {
                phRemeberMe.Controls.Add(new LiteralControl("<div style=\"margin-top: 4px;\">"));
                cbRememberMe = new CheckBox();
                cbRememberMe.ID = "RememberMe";
                cbRememberMe.Text = language.GetString("LableRemeberMeNextTime");
                phRemeberMe.Controls.Add(cbRememberMe);
                phRemeberMe.Controls.Add(new LiteralControl("</div>"));
            }
            if (Request.IsAuthenticated)
            {
                PnlLogins.Visible = false;
            }
            else
            {
                PnlLogins.Visible = true;
                Page.ClientScript.RegisterStartupScript(typeof(Page), "SetFocusToSearch", String.Format("<script type=\"text/javascript\">document.getElementById('{0}').focus();</script>", (object)this.txtLogin.ClientID));
                this.txtLogin.Attributes.Add("onkeypress", "DoLoginOnEnterKey(event, '" + this.btnLogin.UniqueID + "')");
                this.txtPassword.Attributes.Add("onkeypress", "DoLoginOnEnterKey(event, '" + this.btnLogin.UniqueID + "')");
            }
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(txtLogin.Text, txtPassword.Text))
            {
                PerformLogin(txtLogin.Text);
            }
            else
            {
                pnlStatus.Visible = true;
                litStatus.Text = language.GetString("MessageLoginNotSuccess");
            }
        }

        private void PerformLogin(string username)
        {
            MembershipUser usr = Membership.GetUser(username, false);
            UserProfile currProf = UserProfile.GetProfile(usr.UserName);
            currProf.Nickname = usr.UserName;
            currProf.UserId = new Guid(usr.ProviderUserKey.ToString());
            currProf.Save();

            if (usr.PasswordQuestion == "-")
            {
                bool blnPersistent = cbRememberMe != null ? cbRememberMe.Checked : false;
                FormsAuthentication.SetAuthCookie(usr.UserName, blnPersistent);

                UserDataContext udc = UserDataContext.GetUserDataContext(usr.UserName);
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("USER_LOGIN", udc);
                UserActivities.InsertIsNowOnline(udc);
                UserStartPage startPage = (UserStartPage)Enum.Parse(typeof(UserStartPage), System.Configuration.ConfigurationManager.AppSettings["UserStartPage"]);
                if (!string.IsNullOrEmpty(currProf.PrefferedCulture))
                {
                    HttpCookie uiLanguageCokie = new HttpCookie("Language") { Value = currProf.PrefferedCulture };
                    Response.Cookies.Add(uiLanguageCokie);
                }

                switch (startPage)
                {
                    case UserStartPage.Default:
                        if (!string.IsNullOrEmpty(usr.Comment))
                            Response.Redirect(string.Format("{0}", usr.Comment));
                        else if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                            Response.Redirect(Request.RawUrl);
                        else
                            Response.Redirect(Server.UrlDecode(Request.QueryString["ReturnUrl"]));
                        break;
                    case UserStartPage.Homepage:
                        Response.Redirect("/");
                        break;
                    case UserStartPage.Dashboard:
                        Response.Redirect(Helper.GetDetailLink("User", usr.UserName, false) + "&P=Dashboard");
                        break;
                }
            }
            else
            {
                string strUser = HttpUtility.UrlEncodeUnicode(usr.UserName);
                Response.Redirect(string.Format("{0}?U={1}", Constants.Links["LINK_TO_ACTIVATEUSER_PAGE"], strUser));
            }
        }

    }
}