//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   #1.1.0.0    28.01.2008 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.Utils.Web;
using _4screen.Utils.Net;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class PasswordRecover : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.UserProfileData);

            revEmail.ValidationExpression = Constants.REGEX_EMAIL;
            rfvEmail.ErrorMessage = language.GetString("MessageValidEmail");
            rfvEmail.ToolTip = language.GetString("TooltipValidEmail");
            revEmail.ErrorMessage = language.GetString("MessageValidEmail");
            revEmail.ToolTip = language.GetString("TooltipValidEmail");
        }

        protected void OnSendClick(object sender, EventArgs e)
        {
            string username = Membership.GetUserNameByEmail(txtEMail.Text);

            if (!string.IsNullOrEmpty(username))
            {
                MembershipUser user = Membership.GetUser(username, false);
                if (user != null)
                {
                    if (user.IsLockedOut)
                        user.UnlockUser();

                    string password = Helper.GeneratePassword(6, 2);
                    string temporaryPassword = user.ResetPassword();
                    user.ChangePassword(temporaryPassword, password);

                    try
                    {
                        MailAddress receiver = new MailAddress(txtEMail.Text);
                        string messageCulture = !string.IsNullOrEmpty(UserProfile.GetProfile(user.UserName).PrefferedCulture) ? UserProfile.GetProfile(user.UserName).PrefferedCulture : SiteConfig.DefaultLanguage;
                        string subject = _4screen.CSB.Common.SiteConfig.SiteName + " " + GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base", messageCulture).GetString("TextAccountSubject");
                        string body = GuiLanguage.GetGuiLanguage("Templates", messageCulture).GetString("EmailAccountRetrieval");
                        body = body.Replace("<%USER%>", username);
                        body = body.Replace("<%PASSWORD%>", Server.HtmlEncode(password));
                        if (user.PasswordQuestion != "-")
                            body = body.Replace("<%ACTIVATIONCODE%>", string.Format("<p>{0} <b>{1}</b></p>", language.GetString("TextActivationCode"), Server.HtmlEncode(user.PasswordQuestion)));
                        else
                            body = body.Replace("<%ACTIVATIONCODE%>", string.Empty);
                        List<MailAddress> bccList = new List<MailAddress>();
                        foreach (string bccEmail in ConfigurationManager.AppSettings["RegistrationBCCMail"].Split(','))
                        {
                            if (!string.IsNullOrEmpty(bccEmail))
                                bccList.Add(new MailAddress(bccEmail));
                        }
                        Mail.SendMail(receiver, new List<MailAddress>(), bccList, subject, body, true);

                        PnlRecover.Visible = false;
                        PnlSent.Visible = true;
                    }
                    catch
                    {
                        PnlError.Visible = true;
                        LitError.Text = languageShared.GetString("EmailUnableToSend");
                    }
                }
                else if (user.IsLockedOut)
                {
                    PnlError.Visible = true;
                    LitError.Text = language.GetString("MessageAccountLocked");
                }
                else
                {
                    PnlError.Visible = true;
                    LitError.Text = language.GetString("MessageAccountInfoNotFound");
                }
            }
            else
            {
                PnlError.Visible = true;
                LitError.Text = language.GetString("MessageAccountInfoNotFound");
            }
        }
    }
}
