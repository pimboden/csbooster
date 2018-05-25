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
    public partial class LoginSSO : System.Web.UI.UserControl, IMinimalControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public bool HasContent { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.PnlFacebook.Visible = CustomizationSection.CachedInstance.Logins["Facebook"].Enabled;
            this.PnlOpenID.Visible = CustomizationSection.CachedInstance.Logins["OpenID"].Enabled;
            this.PnlInfoCard.Visible = CustomizationSection.CachedInstance.Logins["InfoCard"].Enabled;

            HasContent = true;

            this.ImgInformationCard.Attributes.Add("onClick", "javascript:__doPostBack('" + this.BtnInformationCard.UniqueID + "', '');");

            if (Request.IsAuthenticated)
            {
                PnlLogins.Visible = false;
            }
            else
            {
                PnlLogins.Visible = true;

                if (!IsPostBack)
                {
                    HandleFacebookLogin();
                    HandleOpenIDLogin();
                }

                this.TxtOpenID.Attributes.Add("onkeypress", "DoLoginOnEnterKey(event, '" + this.LbtnOpenIDLogin.UniqueID + "')");
            }
        }


        protected void OpenIDButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtOpenID.Text))
            {
                OpenIdClient openID = new OpenIdClient();
                openID.Identity = this.TxtOpenID.Text;
                UserProfile.Current.OpenID = openID.Identity;
                UserProfile.Current.Save();

                SimpleRegistration simpleRegistration = new SimpleRegistration(openID);
                simpleRegistration.RequiredFields = "nickname,email";
                simpleRegistration.OptionalFields = "fullname,gender";

                openID.CreateRequest(false, true);
            }
            else
            {
                LitOpenIDMsg.Text = language.GetString("MessageOpenIDMissing");
            }
        }

        protected void IdentitySelectorIconClick(object sender, EventArgs e)
        {
            string secXmlToken = Request.Params["SecXmlToken"];
            if (!string.IsNullOrEmpty(secXmlToken))
            {
                Token token = new Token(secXmlToken);
                string ppid = token.Claims[ClaimTypes.PPID];
                string firstname = token.Claims[ClaimTypes.GivenName];
                string lastname = token.Claims[ClaimTypes.Surname];
                string email = token.Claims[ClaimTypes.Email];
                string gender = token.Claims[ClaimTypes.Gender];

                CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var user = csb.hisp_DataObject_GetUserIDByOpenID(ppid).ElementAtOrDefault(0);
                if (user != null) // User exists
                {
                    MembershipUser membershipUser = Membership.GetUser(user.USR_ID);
                    if (membershipUser != null)
                        PerformLogin(membershipUser.UserName);
                }
                else // User doesn't exist
                {
                    if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(email))
                    {
                        if (DataObjectUser.CreateUser(AuthenticationType.InformationCard, ppid, firstname.ToLower(), email, firstname, lastname, gender))
                            PerformLogin(firstname.ToLower());
                    }
                }
            }
            else
            {
                LitInformationCardMsg.Text = language.GetString("MessageInfoCardMissing");
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
                FormsAuthentication.SetAuthCookie(usr.UserName, false);

                UserDataContext udc = UserDataContext.GetUserDataContext(usr.UserName);
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("USER_LOGIN", udc);
                UserActivities.InsertIsNowOnline(udc);
                UserStartPage startPage = (UserStartPage)Enum.Parse(typeof(UserStartPage), System.Configuration.ConfigurationManager.AppSettings["UserStartPage"]);

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

        private void HandleFacebookLogin()
        {
            var cookie = Authentication.GetSignedCookie("fbs_" + ConfigurationManager.AppSettings["FacebookApplicationId"], ConfigurationManager.AppSettings["FacebookApplicationSecret"]);
            if (cookie != null)
            {
                string jsonProfile = Http.DownloadContent((HttpWebRequest)WebRequest.Create("https://graph.facebook.com/me?locale=en_US&access_token=" + cookie["access_token"]), null);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var profile = (Dictionary<string, object>)serializer.DeserializeObject(jsonProfile);

                string facebookUserId = profile["id"].ToString();
                CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var user = csb.hisp_DataObject_GetUserIDByFacebookUserId(facebookUserId).ElementAtOrDefault(0);
                if (user != null) // User exists
                {
                    MembershipUser membershipUser = Membership.GetUser(user.USR_ID);
                    if (membershipUser != null)
                        PerformLogin(membershipUser.UserName);
                }
                else // User doesn't exist
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LinkFacebookAccount", string.Format("$telerik.$(function() {{ radWinOpen('/Pages/Popups/LinkFacebookAccount.aspx', '{0}', 420, 100, false, null, 'settingsWin') }} );", GuiLanguage.GetGuiLanguage("Shared").GetString("CommandCreateProfile")), true);
                }
            }
            else
            {
                litLogin.Text = "<fb:login-button perms=\"email\"></fb:login-button>";
            }
        }

        private void HandleOpenIDLogin()
        {
            OpenIdClient openID = new OpenIdClient();

            switch (openID.RequestedMode)
            {
                case RequestedMode.IdResolution:
                    openID.Identity = UserProfile.Current.OpenID;
                    if (openID.ValidateResponse())
                    {
                        OpenIdUser openIDUser = openID.RetrieveUser();
                        UserProfile.Current.OpenID = string.Empty;
                        UserProfile.Current.Save();
                        CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                        var user = csb.hisp_DataObject_GetUserIDByOpenID(openIDUser.Identity).ElementAtOrDefault(0);
                        if (user != null) // User exists
                        {
                            MembershipUser membershipUser = Membership.GetUser(user.USR_ID);
                            if (membershipUser != null)
                                PerformLogin(membershipUser.UserName);
                        }
                        else // User doesn't exist
                        {
                            string nickname = openIDUser.GetValue("openid.sreg.nickname");
                            string email = openIDUser.GetValue("openid.sreg.email");
                            string fullname = openIDUser.GetValue("openid.sreg.fullname") ?? nickname;
                            string gender = openIDUser.GetValue("openid.sreg.gender");
                            string firstname = string.Empty;
                            string lastname = string.Empty;

                            if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(email))
                            {
                                string[] splitFullname = fullname.Split(' ');
                                if (splitFullname.Length > 0) firstname = splitFullname[0];
                                if (splitFullname.Length > 1) lastname = splitFullname[1];

                                if (DataObjectUser.CreateUser(AuthenticationType.OpenID, openIDUser.Identity, nickname, email, firstname, lastname, gender))
                                    PerformLogin(nickname);
                            }
                            else
                            {
                                LitOpenIDMsg.Text = language.GetString("MessageOpenIDNoDataReceived");
                            }
                        }
                    }
                    else
                    {
                        LitOpenIDMsg.Text = language.GetString("MessageOpenIDLoginNotSuccess");
                    }
                    break;
                case RequestedMode.CanceledByUser:
                    LitOpenIDMsg.Text = language.GetString("MessageOpenIDLoginCancel");
                    break;
            }
        }

    }
}