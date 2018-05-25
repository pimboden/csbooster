// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Net;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class Registration : System.Web.UI.UserControl, IMinimalControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        public bool HasContent { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }

        protected override void OnInit(EventArgs e)
        {
            HasContent = true;
            RevEmail.ValidationExpression = Constants.REGEX_EMAIL;
            PnlReg.Visible = !Helper.IsClosedUserGroup();
            PnlMsg.Visible = !PnlReg.Visible;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (!string.IsNullOrEmpty(Request.Form["codeBox"]))
            {
                PnlError.Visible = true;
                LitError.Text = language.GetString("MessageInvisibleTextBoxCaptcha");
            }
            else if (Page.IsValid)
            {
                string activationCode = Helper.GetRegistrationActivationCode();
                string username = TxtLogin.Text.StripHTMLTags().Trim();
                string password = TxtPW.Text;
                string email = TxtEmail.Text;

                MembershipCreateStatus membershipCreateStatus;
                MembershipUser membershipUser = Membership.CreateUser(username, password, email, activationCode, activationCode, true, out membershipCreateStatus);
                if (membershipCreateStatus == MembershipCreateStatus.Success)
                {
                    UserProfile userProfile = (UserProfile)UserProfile.Create(membershipUser.UserName);
                    userProfile.Nickname = membershipUser.UserName;
                    userProfile.UserId = new Guid(membershipUser.ProviderUserKey.ToString());
                    userProfile.Save();

                    Roles.AddUserToRole(membershipUser.UserName, "NotActivated");
                    try
                    {
                        string fromCampaign = Request.QueryString["CTYID"];
                        if (!string.IsNullOrEmpty(fromCampaign))
                        {
                            HitblCampaignsCmp campaign = HitblCampaignsCmp.FetchByID(new Guid(fromCampaign));
                            if (campaign != null)
                            {
                                membershipUser.Comment = campaign.CmpRedirectURL;
                                Membership.UpdateUser(membershipUser);
                            }
                        }
                    }
                    catch
                    { }

                    try
                    {
                        SendEmail(username, email, activationCode);
                        PnlReg.Visible = false;
                        PnlRegistered.Visible = true;
                    }
                    catch (Exception)
                    {
                        PnlError.Visible = true;
                        LitError.Text = language.GetString("MessageCouldNotSendActivationMail");
                    }
                }
            }
        }

        private void SendEmail(string username, string email, string activationCode)
        {
            MailAddress receiver = new MailAddress(email, username);
            string subject = _4screen.CSB.Common.SiteConfig.SiteName + " " + GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("TextMailSubjectActivation");
            string body = GuiLanguage.GetGuiLanguage("Templates").GetString("EmailRegistration");
            body = body.Replace("<%USER%>", username);
            body = body.Replace("<%ACTIVATE_CODE%>", activationCode);
            body = body.Replace("<%UID%>", HttpUtility.UrlEncodeUnicode(username));
            List<MailAddress> bccList = new List<MailAddress>();
            foreach (string bccEmail in ConfigurationManager.AppSettings["RegistrationBCCMail"].Split(','))
            {
                if (!string.IsNullOrEmpty(bccEmail))
                    bccList.Add(new MailAddress(bccEmail));
            }
            Mail.SendMail(receiver, new List<MailAddress>(), bccList, subject, body, true);
        }

        protected void ValidateLogin(object sender, EventArgs e)
        {
            Page.Validate("Login");
        }

        protected void ValidateEmail(object sender, EventArgs e)
        {
            Page.Validate("Email");
        }

        protected void ValidateEmail(object sender, ServerValidateEventArgs e)
        {
            TxtEmail.Text = TxtEmail.Text.Trim();

            MembershipUserCollection members = Membership.FindUsersByEmail(TxtEmail.Text);
            if (members.Count > 0)
            {
                e.IsValid = false;
                ScriptManager.RegisterStartupScript(UpnlEmail, UpnlEmail.GetType(), "SetFocus", String.Format("document.getElementById('{0}').focus();", this.TxtEmail.ClientID), true);
            }
        }

        protected void ValidateLogin(object sender, ServerValidateEventArgs e)
        {
            TxtLogin.Text = Common.Extensions.StripHTMLTags(TxtLogin.Text).Trim();

            MembershipUserCollection members = Membership.FindUsersByName(TxtLogin.Text);
            if (members.Count > 0)
            {
                e.IsValid = false;
                ScriptManager.RegisterStartupScript(UpnlLogin, UpnlLogin.GetType(), "SetFocus", String.Format("document.getElementById('{0}').focus();", this.TxtLogin.ClientID), true);
            }
        }

        protected void ValidateAGB(object sender, ServerValidateEventArgs e)
        {
            if (!CbxAGB.Checked)
            {
                e.IsValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetFocus", String.Format("document.getElementById('{0}').focus();", this.CbxAGB.ClientID), true);
            }
        }
    }
}
