// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI
{
    public partial class Admin_UserControls_UserOutput : System.Web.UI.UserControl
    {
        private UserDataContext userDataContext;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        public DataObjectUser User { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RenderControls()
        {
            userDataContext = UserDataContext.GetUserDataContext(User.Nickname);

            string detailLink =  Helper.GetDetailLink("User", User.Nickname);

            LnkUser.Text = User.Nickname.CropString(16);
            LnkUser.NavigateUrl = detailLink;
            LnkUser.ID = null;

            LbtnDelete.Attributes.Add("OnClick", string.Format("return confirm('{0}');", string.Format(language.GetString("MessageDeleteUserConfirm"), User.Nickname).StripForScript()));

            FillRoles();
            SetLock();
            SetMail();
        }

        private void SetMail()
        {
            string strUsername = User.Nickname;

            if (!string.IsNullOrEmpty(strUsername))
            {
                MembershipUser mUser = Membership.GetUser(strUsername, false);
                if (mUser != null)
                {
                    txtEmail.Text = mUser.Email;
                }
            }
        }

        private void SetLock()
        {
            if (DataObjectUser.IsUserLockedOut(User.UserID.Value))
            {
                LbtnLock.CssClass = "CSB_admin_user_locked";
            }
            else
            {
                LbtnLock.CssClass = "CSB_admin_user_unlocked";
            }
        }

        private void FillRoles()
        {

            foreach (string role in Roles.GetAllRoles())
            {
                ListItem li = new ListItem(role, role);
                li.Selected = userDataContext.UserRole.ToLower() == role.ToLower();
                DDLRoles.Items.Add(li);
            }
        }

        protected void OnSelectedRoleChanged(object sender, EventArgs e)
        {
            foreach (string role in Roles.GetAllRoles())
            {
                if (Roles.IsUserInRole(User.Nickname, role))
                {
                    Roles.RemoveUserFromRole(User.Nickname, role);
                }
            }
            Roles.AddUserToRole(User.Nickname, DDLRoles.SelectedValue);
        }

        protected void OnLockUserClick(object sender, EventArgs e)
        {
            if (LbtnLock.CssClass == "CSB_admin_user_locked")
            {
                DataObjectUser.Unlock(User.UserID.Value);
                LbtnLock.CssClass = "CSB_admin_user_unlocked";
            }
            else
            {
                DataObjectUser.LockOut(User.UserID.Value);
                LbtnLock.CssClass = "CSB_admin_user_locked";
            }
        }

        protected void OnDeleteUserClick(object sender, EventArgs e)
        {
            LitMsg.Visible = true;
            try
            {
                SubSonic.StoredProcedure sp = SPs.HispUserDeleteaspnetuser(User.ObjectID.Value);
                sp.Execute();
                ((IReloadable)this.Page).Reload();
            }
            catch
            {
                LitMsg.Text = language.GetString("MessageDeleteUserError"); 
            }
        }

        protected void OnPasswortResetClick(object sender, EventArgs e)
        {
            LitMsg.Visible = true;
            string strUsername = User.Nickname;
            if (!string.IsNullOrEmpty(strUsername))
            {
                MembershipUser mUser = Membership.GetUser(strUsername, false);
                if (mUser != null)
                {
                    string strPassword = mUser.ResetPassword();
                    string messageCulture = !string.IsNullOrEmpty(UserProfile.GetProfile(mUser.UserName).PrefferedCulture) ? UserProfile.GetProfile(mUser.UserName).PrefferedCulture : SiteConfig.DefaultLanguage;
                    string strMailBody = GuiLanguage.GetGuiLanguage("Templates", messageCulture).GetString("EmailAccountRetrieval");

                    strMailBody = strMailBody.Replace("<%SITE_URL%>", SiteConfig.SiteURL);
                    strMailBody = strMailBody.Replace("<%USER%>", strUsername);
                    strMailBody = strMailBody.Replace("<%PASSWORD%>", Server.HtmlEncode(strPassword));
                    if (mUser.PasswordQuestion != "-")
                    {
                        strMailBody = strMailBody.Replace("<%ACTIVATIONCODE%>", string.Format("<p>{0} <b>{1}</b></p>", language.GetString("MessageActivationCode"), Server.HtmlEncode(mUser.PasswordQuestion)));
                    }
                    else
                    {
                        strMailBody = strMailBody.Replace("<%ACTIVATIONCODE%>", string.Empty);
                    }
                    try
                    {
                        SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                        MailMessage objMail = new MailMessage();
                        objMail.From = new MailAddress(smtpSec.From, languageShared.GetString("EmailFromName"));
                        objMail.To.Add(new MailAddress(mUser.Email));

                        objMail.Subject = language.GetString("MessageAccountSubject");
                        objMail.Body = strMailBody;
                        objMail.IsBodyHtml = true;

                        SmtpClient objSmtp = new SmtpClient();
                        objSmtp.Send(objMail);

                        LitMsg.Text = language.GetString("MessageNewPassword");
                    }
                    catch
                    {
                        LitMsg.Text = languageShared.GetString("EmailUnableToSend");
                    }
                }
                else
                {
                    LitMsg.Text = language.GetString("MessageAccountInfoNotFound");
                }
            }
            else
            {
                LitMsg.Text = language.GetString("MessageAccountInfoNotFound");
            }
        }

        protected void OnEmailSaveClick(object sender, EventArgs e)
        {
            string strUsername = User.Nickname;
            string strEmail = string.Empty;
            LitMsg.Visible = true;
            if (Page.IsValid && !string.IsNullOrEmpty(strUsername) && !string.IsNullOrEmpty(txtEmail.Text))
            {
                MembershipUser mUser = Membership.GetUser(strUsername, false);
                if (mUser != null)
                {
                    try
                    {
                        mUser.Email = txtEmail.Text;
                        Membership.UpdateUser(mUser);
                        LitMsg.Text = language.GetString("MessageAccountSaved");
                    }
                    catch
                    {
                        LitMsg.Text = language.GetString("MessageAccountNotSaved");
                    }
                }
                else
                {
                    LitMsg.Text = language.GetString("MessageAccountInfoNotFound");
                }
            }
        }
    }
}
