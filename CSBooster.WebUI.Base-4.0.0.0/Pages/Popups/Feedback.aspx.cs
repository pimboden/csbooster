using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Net;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RevEmail.ValidationExpression = Constants.REGEX_EMAIL;

            if (!IsPostBack && Request.IsAuthenticated)
            {
                DataObjectUser user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId);
                if (user.State != ObjectState.Added)
                {
                    TxtEmail.Text = Membership.GetUser(user.UserID).Email;
                    if (!string.IsNullOrEmpty(user.Vorname) || !string.IsNullOrEmpty(user.Name))
                        TxtName.Text = user.Vorname + " " + user.Name;
                    else
                        TxtName.Text = user.Nickname;
                }
            }

            if (!IsPostBack)
                ScriptManager.RegisterStartupScript(this, GetType(), "FocusEmail", string.Format("document.getElementById('{0}').focus();", TxtEmail.ClientID), true);
        }

        protected void OnSendClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (!string.IsNullOrEmpty(Request.Form["codeBox"]))
            {
                PnlStatus.Visible = true;
                LitStatus.Text = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("MessageInvisibleTextBoxCaptcha");
            }
            else if (Page.IsValid)
            {
                try
                {
                    bool isAttachmentValid = true;
                    List<Attachment> attachments = null;
                    if (FuAttachment.HasFile)
                    {
                        if (FuAttachment.PostedFile.ContentLength <= 5242880)
                        {
                            if (FuAttachment.PostedFile.ContentType.StartsWith("image"))
                            {
                                attachments = new List<Attachment>() { new Attachment(FuAttachment.FileContent, FuAttachment.FileName) };
                            }
                            else
                            {
                                isAttachmentValid = false;
                                PnlStatus.Visible = true;
                                LitStatus.Text = (new TextControl() { LanguageFile = "Popups.WebUI", TextKey = "ErrorEmailAttachmentInvalidContent" }).Text;
                            }
                        }
                        else
                        {
                            isAttachmentValid = false;
                            PnlStatus.Visible = true;
                            LitStatus.Text = (new TextControl() { LanguageFile = "Popups.WebUI", TextKey = "ErrorEmailAttachmentToLarge" }).Text;
                        }
                    }

                    if (isAttachmentValid)
                    {
                        Mail.SendMail(null,
                                      ConfigurationManager.AppSettings["FeedbackMail"].Split(',').ToList().ConvertAll(x => new MailAddress(x)),
                                      new List<MailAddress>(),
                                      new List<MailAddress>(),
                                      new MailAddress(TxtEmail.Text, TxtName.Text),
                                      _4screen.CSB.Common.SiteConfig.SiteName + " " + GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("EmailFeedbackSubject"),
                                      TxtFeedback.Text,
                                      false,
                                      attachments);

                        ClientScript.RegisterStartupScript(GetType(), "CloseWindow", "RefreshParentPage();CloseWindow();", true);
                    }
                }
                catch
                {
                    PnlStatus.Visible = true;
                    LitStatus.Text = (new TextControl() { LanguageFile = "Pages.Popups.WebUI.Base", TextKey = "ErrorEmailFeedback" }).Text;
                }
            }
        }
    }
}