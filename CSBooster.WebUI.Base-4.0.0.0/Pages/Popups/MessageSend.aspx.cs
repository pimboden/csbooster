//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / AW
//  Updated:   #1.1.0.0    15.01.2008 PI
//                         Replace List<Friend> with List<DataObjectUser> 
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.Utils.Web;
using _4screen.WebControls;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class MessageSend : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        private string receiverType;  // friend, member, report
        private string messageType;   // msg, rec, ymr, rep, invite, pbs, pbo
        private string messageMode;
        private string messageId;
        private Guid? receiverId;
        private int? objectType;
        private Guid? objectId;
        private string rawLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);

            receiverType = (Request.QueryString["RecType"] != null) ? Request.QueryString["RecType"].ToLower() : "friend";
            messageType = (Request.QueryString["MsgType"] != null) ? Request.QueryString["MsgType"].ToLower() : string.Empty;
            messageMode = (Request.QueryString["MsgMode"] != null) ? Request.QueryString["MsgMode"].ToLower() : string.Empty;
            messageId = Request.QueryString["MsgId"] ?? string.Empty;
            receiverId = !string.IsNullOrEmpty(Request.QueryString["RecId"]) ? (Guid?)Request.QueryString["RecId"].ToGuid() : null;
            objectType = !string.IsNullOrEmpty(Request.QueryString["ObjType"]) ? (int?)Helper.GetObjectTypeNumericID(Request.QueryString["ObjType"]) : null;
            objectId = !string.IsNullOrEmpty(Request.QueryString["ObjId"]) ? (Guid?)Request.QueryString["ObjId"].ToGuid() : null;
            rawLink = (Request.QueryString["URL"] != null) ? System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["URL"])) : string.Empty;

            SetMessageType();
            SetSubjectAndBody();
        }

        protected void SetMessageType()
        {
            if (receiverId.HasValue)
            {
                Control control = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
                ((SmallOutputUser2)control).DataObjectUser = DataObject.Load<DataObjectUser>(receiverId);
                receiverPlaceHolder.Controls.Add(new LiteralControl("<div class=\"inputBlock\"><div class=\"inputBlockLabel\">" + (new LabelControl() { LanguageFile = "Pages.Popups.WebUI.Base", LabelKey = "LableRequestReceiver", TooltipKey = "TooltipRequestReceiver" }).Text + "</div><div class=\"inputBlockContent\">" + ((SmallOutputUser2)control).GetHtml() + "</div></div>"));
            }
            else
            {
                if (receiverType == "friend")
                {
                    ReceiverSelector receiverSelector = (ReceiverSelector)LoadControl("/UserControls/ReceiverSelector.ascx");
                    receiverPlaceHolder.Controls.Add(new LiteralControl("<div class=\"inputBlock\"><div class=\"inputBlockLabel\">" + (new LabelControl() { LanguageFile = "Pages.Popups.WebUI.Base", LabelKey = "LableRequestReceiver", TooltipKey = "TooltipRequestReceiver" }).Text + "</div><div class=\"inputBlockContent\">"));
                    receiverPlaceHolder.Controls.Add(receiverSelector);
                    receiverPlaceHolder.Controls.Add(new LiteralControl("</div></div>"));
                }
                else if (!UserProfile.Current.IsAnonymous && receiverType == "member")
                {
                    receiverPlaceHolder.Controls.Add(new LiteralControl("<div class=\"inputBlock\">" + (new LabelControl() { LanguageFile = "Pages.Popups.WebUI.Base", LabelKey = "MessageToAllMember", TooltipKey = "TooltipMessageToAllMember" }).Text + "</div>"));
                }
                else if (receiverType == "report")
                {
                    receiverPlaceHolder.Controls.Add(new LiteralControl("<div class=\"inputBlock\">" + (new TextControl() { LanguageFile = "Pages.Popups.WebUI.Base", TextKey = "MessageToAdmin" }).Text + "</div>"));

                    var reports = CustomizationSection.CachedInstance.ContentReports;
                    if (reports.Count > 0)
                    {
                        reportPanel.Visible = true;

                        GuiLanguage languageReport = GuiLanguage.GetGuiLanguage(CustomizationSection.CachedInstance.ContentReports.LocalizationBaseFileName);

                        foreach (ContentReport report in CustomizationSection.CachedInstance.ContentReports)
                            rcbReport.Items.Add(new RadComboBoxItem(languageReport.GetString(report.ReasonKey), report.ReasonKey));

                        int reportIndex = 0;
                        if (!string.IsNullOrEmpty(Request.Form[rcbReport.UniqueID]))
                            reportIndex = rcbReport.FindItemIndexByText(Request.Form[rcbReport.UniqueID]);
                        rcbReport.SelectedIndex = reportIndex;
                    }
                }
            }
        }

        private void SetSubjectAndBody()
        {
            if (!IsPostBack)
            {
                if (messageType.ToLower() == "rec")
                {
                    txtSubject.Text = language.GetString("LableMessageRecommedation");
                    string perparedLink = rawLink;
                    if (!rawLink.ToLower().StartsWith("http"))
                        perparedLink = _4screen.CSB.Common.SiteConfig.HostName + rawLink;

                    string mailBody = GuiLanguage.GetGuiLanguage("Templates").GetString("EmailRecommendation");
                    mailBody = mailBody.Replace("<%SITE_URL%>", _4screen.CSB.Common.SiteConfig.SiteURL);
                    if (UserDataContext.GetUserDataContext().IsAuthenticated)
                    {
                        DataObjectUser user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId);
                        mailBody = mailBody.Replace("<%FROM_USERNAME%>", user.Vorname);
                    }
                    else
                    {
                        mailBody = mailBody.Replace("<%FROM_USERNAME%>", string.Empty);
                    }
                    mailBody = mailBody.Replace("<%LINK%>", perparedLink);
                    txtBody.Content = mailBody;
                    txtSubject.ReadOnly = true;
                }
                else if (messageType.ToLower() == "msg")
                {
                    txtSubject.Text = language.GetString("LableMessagePrivateMessage");
                }
                else if (messageType.ToLower() == "ymr")
                {
                    txtSubject.Text = languageShared.GetString("CommandFriendshipQuery");
                    txtSubject.ReadOnly = true;
                }
                else if (messageType.ToLower() == "invite")
                {
                    txtSubject.Text = language.GetString("LableMessageInviteToCommunity");
                    txtSubject.ReadOnly = true;
                }
                else if (messageType.ToLower() == "rep")
                {
                    if (objectType == Helper.GetObjectTypeNumericID("User"))
                        txtSubject.Text = languageShared.GetString("CommandUserReport");
                    else if (objectType == Helper.GetObjectTypeNumericID("Community"))
                        txtSubject.Text = languageShared.GetString("CommandCommunityReport");
                    else if (objectType.HasValue)
                        txtSubject.Text = string.Format(languageShared.GetString("CommandObjectReport"), Helper.GetObjectName(objectType.Value, true));
                    else
                        txtSubject.Text = languageShared.GetString("CommandPageReport");
                    txtSubject.ReadOnly = true;
                }
                else if (messageType.ToLower() == "pbs" && objectId.HasValue)
                {
                    DataObjectPinboardSearch pinboardSearch = DataObject.Load<DataObjectPinboardSearch>(objectId);
                    if (pinboardSearch.State != ObjectState.Added)
                    {
                        txtSubject.Text = language.GetString("TextPinboardSearch") + " '" + pinboardSearch.Title + "'";
                        txtSubject.ReadOnly = true;
                    }
                }
                else if (messageType.ToLower() == "pbo" && objectId.HasValue)
                {
                    DataObjectPinboardOffer pinboardOffer = DataObject.Load<DataObjectPinboardOffer>(objectId);
                    if (pinboardOffer.State != ObjectState.Added)
                    {
                        txtSubject.Text = language.GetString("TextPinboardOffer") + " '" + pinboardOffer.Title + "'";
                        txtSubject.ReadOnly = true;
                    }
                }
                if (receiverType.ToLower() == "member" && objectId.HasValue)
                {
                    bool isOwner = false;
                    bool isMember = false;
                    if (UserProfile.Current.UserId != Guid.Empty)
                        isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, objectId.Value, out isMember);
                    if (!isOwner)
                        receiverType = string.Empty;
                }


                if (messageMode.ToLower() == "reply")
                {
                    DataAccess.Business.Message message = DataAccess.Business.Message.LoadMessage(new Guid(messageId), _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    if (Regex.IsMatch(message.Subject, "^Re.*?:"))
                    {
                        Match match = Regex.Match(message.Subject, @"^Re\[(\d*?)\]:");
                        if (match.Groups.Count == 2) // Other replies
                        {
                            int replyNumber;
                            int.TryParse(match.Groups[1].ToString(), out replyNumber);
                            replyNumber++;
                            txtSubject.Text = Regex.Replace(message.Subject, @"^Re\[\d*?\]:", "Re[" + replyNumber + "]:");
                        }
                        else // Second reply
                        {
                            txtSubject.Text = Regex.Replace(message.Subject, @"^Re:", "Re[2]:");
                        }
                    }
                    else // First reply
                    {
                        txtSubject.Text = "Re: " + message.Subject;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetTitle", "GetRadWindow().SetTitle('" + language.GetString("LableMessageAnswer").StripForScript() + "');", true);
                    DataObjectUser doUser = DataObject.Load<DataObjectUser>(message.FromUserID);
                    if (!message.MsgText.StartsWith("<i>"))
                        message.MsgText = string.Format("<i>{0}</i>", message.MsgText);
                    txtBody.Content = string.Format("<br/><i><b>{0} {1}: </i></b><br>{2}", doUser.Nickname, language.GetString("LableMessageWrote"), message.MsgText);
                }
                else if (messageMode.ToLower() == "forward")
                {
                    DataAccess.Business.Message message = DataAccess.Business.Message.LoadMessage(new Guid(messageId), _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    txtSubject.Text = "Fw: " + message.Subject.Replace("Fw: ", string.Empty);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetTitle", "GetRadWindow().SetTitle('" + language.GetString("LableMessageForward").StripForScript() + "');", true);
                    DataObjectUser doUser = DataObject.Load<DataObjectUser>(message.FromUserID);
                    if (!message.MsgText.StartsWith("<i>"))
                        message.MsgText = string.Format("<i>{0}</i>", message.MsgText);
                    txtBody.Content = string.Format("<br/><i><b>{0} {1}: </i></b><br>{2}", doUser.Nickname, language.GetString("LableMessageWrote"), message.MsgText);
                }
            }
        }

        protected void OnSendMessageClick(object sender, EventArgs e)
        {
            bool msgSent = false;
            errorMsg.Text = string.Empty;

            List<MessageReceiver> receivers = new List<MessageReceiver>();

            if (receiverType == "friend")
            {
                if (receiverId.HasValue)
                {
                    receivers.Add(new MessageReceiver() { UserId = receiverId });
                }
                else if (Request.Form.GetValues("receiver") != null)
                {
                    QuickParametersFriends parametersFriends = new QuickParametersFriends();
                    parametersFriends.Udc = UserDataContext.GetUserDataContext();
                    parametersFriends.CurrentUserID = UserProfile.Current.UserId;
                    parametersFriends.OnlyNotBlocked = true;
                    parametersFriends.Amount = 10000;
                    parametersFriends.PageSize = 10000;
                    parametersFriends.DisablePaging = true;
                    parametersFriends.SortBy = QuickSort.Title;
                    parametersFriends.Direction = QuickSortDirection.Asc;
                    parametersFriends.ShowState = ObjectShowState.Published;
                    List<DataObjectFriend> friends = DataObjects.Load<DataObjectFriend>(parametersFriends);

                    string[] rawReceivers = Request.Form.GetValues("receiver");
                    foreach (var receiver in rawReceivers)
                    {
                        int friendType;
                        if (receiver.IsGuid() && friends.Exists(x => x.UserID == receiver.ToGuid()))
                        {
                            receivers.Add(new MessageReceiver() { UserId = receiver.ToGuid() });
                        }
                        else if (int.TryParse(receiver, out friendType))
                        {
                            foreach (var friend in friends.FindAll(x => x.FriendType == (FriendType)friendType))
                                if (!receivers.Exists(x => x.UserId == friend.UserID))
                                    receivers.Add(new MessageReceiver() { UserId = friend.UserID });
                        }
                        else
                        {
                            receivers.Add(new MessageReceiver() { EmailAddress = receiver });
                        }
                    }
                }
            }
            else if (receiverType == "member")
            {
                List<Member> list = Members.Load(objectId.Value);
                foreach (Member item in list.FindAll(x => x.UserId != UserProfile.Current.UserId))
                    receivers.Add(new MessageReceiver() { UserId = item.UserId });
            }
            else if (receiverType == "report")
            {
                foreach (var emailAddress in ConfigurationManager.AppSettings["ReportMail"].Split(','))
                    receivers.Add(new MessageReceiver() { EmailAddress = emailAddress });
            }

            if (receivers.Count == 0)
            {
                pnlError.Visible = true;
                ScriptManager.RegisterClientScriptBlock(up, up.GetType(), "AutoSize", "GetRadWindow().autoSize();", true);
                errorMsg.Text = language.GetString("ErrorMessageNoConect");
            }
            else if (string.IsNullOrEmpty(txtBody.Content.StripHTMLTags().Trim()))
            {
                pnlError.Visible = true;
                ScriptManager.RegisterClientScriptBlock(up, up.GetType(), "AutoSize", "GetRadWindow().autoSize();", true);
                errorMsg.Text = language.GetString("ErrorMessageNoText");
            }
            else
            {
                if (messageType.ToLower() == "msg")
                {
                    msgSent = SendMessage(receivers, txtSubject.Text, txtBody.Content);
                }
                else if (messageType.ToLower() == "rec")
                {
                    msgSent = SendRecommendation(receivers, rawLink, txtSubject.Text, txtBody.Content);
                }
                else if (messageType.ToLower() == "invite")
                {
                    msgSent = SendInvitation(receivers, txtSubject.Text, txtBody.Content);
                }
                else if (messageType.ToLower() == "ymr")
                {
                    msgSent = SendFriendshipRequest(receivers, txtSubject.Text, txtBody.Content);
                }
                else if (messageType.ToLower() == "rep")
                {
                    msgSent = SendReport(receivers, txtSubject.Text, txtBody.Content);
                }
                else if (messageType.ToLower() == "pbo")
                {
                    msgSent = SendPinboardResponse(receivers, txtSubject.Text, txtBody.Content, MessageTypes.PinboardOfferResponse);
                }
                else if (messageType.ToLower() == "pbs")
                {
                    msgSent = SendPinboardResponse(receivers, txtSubject.Text, txtBody.Content, MessageTypes.PinboardSearchResponse);
                }

                if (msgSent)
                {
                    ScriptManager.RegisterClientScriptBlock(up, up.GetType(), "CloseWindow", "$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );", true);
                }
                else
                {
                    pnlError.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(up, up.GetType(), "AutoSize", "GetRadWindow().autoSize();", true);
                    errorMsg.Text = language.GetString("ErrorMessageCouldNotSend");
                }
            }
        }

        private bool SendMessage(List<MessageReceiver> receivers, string messageSubject, string messageBody)
        {
            bool messageSent = true;

            // Send internal messages
            foreach (var receiver in receivers.FindAll(x => x.UserId.HasValue))
            {
                try
                {
                    DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = receiver.UserId.Value;
                    message.TypOfMessage = (int)MessageTypes.NormalMessage;
                    message.Subject = messageSubject;
                    message.MsgText = messageBody;
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (messageMode.ToLower() == "reply")
                        DataAccess.Business.Message.SetAnswered(new Guid(messageId));
                    if (messageMode.ToLower() == "forward")
                        DataAccess.Business.Message.SetForwarded(new Guid(messageId));
                    if (!message.SendNormalMessage())
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            // Send emails
            List<MailAddress> emailReceivers = new List<MailAddress>();
            foreach (var receiver in receivers.FindAll(x => !string.IsNullOrEmpty(x.EmailAddress)))
                emailReceivers.Add(new MailAddress(receiver.EmailAddress));
            if (emailReceivers.Count > 0)
            {
                try
                {
                    if (!DataAccess.Business.Message.SendEmail(emailReceivers, messageSubject, messageBody, true))
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            return messageSent;
        }

        private bool SendRecommendation(List<MessageReceiver> receivers, string url, string messageSubject, string messageBody)
        {
            bool messageSent = true;

            // Send internal messages
            foreach (var receiver in receivers.FindAll(x => x.UserId.HasValue))
            {
                try
                {
                    DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = receiver.UserId.Value;
                    message.TypOfMessage = (int)MessageTypes.NormalMessage;
                    message.Subject = messageSubject;
                    message.MsgText = Regex.Replace(messageBody, "<.html>|<.body>", "");
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (!message.SendNormalMessage())
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            // Send emails
            List<MailAddress> emailReceivers = new List<MailAddress>();
            foreach (var receiver in receivers.FindAll(x => !string.IsNullOrEmpty(x.EmailAddress)))
                emailReceivers.Add(new MailAddress(receiver.EmailAddress));
            if (emailReceivers.Count > 0)
            {
                try
                {
                    if (!DataAccess.Business.Message.SendEmail(emailReceivers, _4screen.CSB.Common.SiteConfig.SiteName + " " + messageSubject, messageBody, true))
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            return messageSent;
        }

        private bool SendInvitation(List<MessageReceiver> receivers, string messageSubject, string messageBody)
        {
            bool messageSent = true;

            // Send internal messages
            foreach (var receiver in receivers.FindAll(x => x.UserId.HasValue))
            {
                try
                {
                    DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = receiver.UserId.Value;
                    message.TypOfMessage = (int)MessageTypes.InviteToCommunity;
                    message.Subject = messageSubject;
                    message.MsgText = messageBody;
                    message.XMLData = string.Format("<root><communityid>{0}</communityid></root>", objectId);
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (!message.SendInviteMessage())
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            return messageSent;
        }

        private bool SendFriendshipRequest(List<MessageReceiver> receivers, string messageSubject, string messageBody)
        {
            bool messageSent = true;

            // Send internal messages
            foreach (var receiver in receivers.FindAll(x => x.UserId.HasValue))
            {
                try
                {
                    DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = receiver.UserId.Value;
                    message.TypOfMessage = (int)MessageTypes.FriendRequest;
                    message.Subject = messageSubject;
                    message.MsgText = messageBody;
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (!message.SendRequestsMessage())
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            return messageSent;
        }

        private bool SendReport(List<MessageReceiver> receivers, string messageSubject, string messageBody)
        {
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                try
                {
                    MailDefinition mailDefinition = new MailDefinition();
                    mailDefinition.From = new MailAddress(smtpSection.From).ToString();
                    mailDefinition.Subject = !string.IsNullOrEmpty(messageSubject) ? string.Format("{0} {1} {2}", _4screen.CSB.Common.SiteConfig.SiteName, language.GetString("TextMessageFeedback"), messageSubject) : string.Format("{0} {1}", _4screen.CSB.Common.SiteConfig.SiteName, language.GetString("TextMessageFeedback"));
                    mailDefinition.IsBodyHtml = true;

                    ListDictionary replacements = new ListDictionary();
                    replacements.Add("<%SITE_URL%>", _4screen.CSB.Common.SiteConfig.SiteURL);
                    if (UserDataContext.GetUserDataContext().IsAuthenticated)
                        replacements.Add("<%FROM_USERNAME%>", UserDataContext.GetUserDataContext().Nickname);
                    else
                        replacements.Add("<%FROM_USERNAME%>", UserDataContext.GetUserDataContext().AnonymousUserId.ToString());

                    string preparedLink = rawLink;
                    if (!rawLink.ToLower().StartsWith("http"))
                        preparedLink = _4screen.CSB.Common.SiteConfig.HostName + rawLink;

                    replacements.Add("<%LINK%>", preparedLink);
                    GuiLanguage languageReport = GuiLanguage.GetGuiLanguage(CustomizationSection.CachedInstance.ContentReports.LocalizationBaseFileName);
                    replacements.Add("<%REASON%>", languageReport.GetString(rcbReport.SelectedValue));
                    replacements.Add("<%USER_MESSAGE%>", messageBody);

                    string mailBody = GuiLanguage.GetGuiLanguage("Templates").GetString("EmailObjectReport");
                    MailMessage mailMessage = mailDefinition.CreateMailMessage(string.Join(",", receivers.FindAll(x => !string.IsNullOrEmpty(x.EmailAddress)).Select(x => x.EmailAddress).ToArray()), replacements, mailBody, this);

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Send(mailMessage);

                    return true;
                }
                catch (Exception exception)
                {
                    LogManager.WriteEntry(exception);
                    return false;
                }
            }
            catch (Exception exception)
            {
                LogManager.WriteEntry(exception);
                return false;
            }
        }

        private bool SendPinboardResponse(List<MessageReceiver> receivers, string messageSubject, string messageBody, MessageTypes messageType)
        {
            bool messageSent = true;

            // Send internal messages
            foreach (var receiver in receivers.FindAll(x => x.UserId.HasValue))
            {
                try
                {
                    DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = receiver.UserId.Value;
                    message.TypOfMessage = (int)messageType;
                    message.Subject = messageSubject;
                    message.MsgText = messageBody;
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (!message.SendNormalMessage())
                        throw new Exception();
                }
                catch
                {
                    messageSent = false;
                }
            }

            return messageSent;
        }
    }
}