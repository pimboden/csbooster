// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class MessageSend : System.Web.UI.Page
    {
        private string msgMode;
        private string msgId;
        private string receiverId;
        private string returnUrl;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);

            msgMode = (Request.QueryString["MsgMode"] != null) ? Request.QueryString["MsgMode"].ToLower() : string.Empty;
            msgId = Request.QueryString["MsgId"] ?? string.Empty;
            receiverId = Request.QueryString["RecId"] ?? string.Empty;
            returnUrl = !string.IsNullOrEmpty(Request.QueryString["ReturnURL"]) ? Encoding.ASCII.GetString(Convert.FromBase64String(Request.QueryString["ReturnURL"])) : "/M/Admin/Dashboard.aspx";

            lnkBack.NavigateUrl = returnUrl;
            lnkBack.ID = null;

            if (!string.IsNullOrEmpty(receiverId))
                RenderDirectMessage();
            else
                RenderNormalMessage();

            if (msgMode.ToLower() == "reply")
            {
                _4screen.CSB.DataAccess.Business.Message message = _4screen.CSB.DataAccess.Business.Message.LoadMessage(new Guid(msgId), SiteConfig.GetSiteContext(UserProfile.Current));
                txtSubject.Text = "Re: " + message.Subject;
                DataObjectUser doUser = DataObject.Load<DataObjectUser>(message.FromUserID);
                txtBody.Text = string.Format("\r\n{0} {1}: \r\n{2}", doUser.Nickname, GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("LableMessageWrote"), message.MsgText);
            }
            else if (msgMode.ToLower() == "forward")
            {
                _4screen.CSB.DataAccess.Business.Message message = _4screen.CSB.DataAccess.Business.Message.LoadMessage(new Guid(msgId), SiteConfig.GetSiteContext(UserProfile.Current));
                txtSubject.Text = "Fw: " + message.Subject.Replace("Fw: ", string.Empty);
                DataObjectUser doUser = DataObject.Load<DataObjectUser>(message.FromUserID);

                txtBody.Text = string.Format("\r\n{0} {1}: \r\n{2}", doUser.Nickname, GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("LableMessageWrote"), message.MsgText);
            }
        }

        protected void RenderDirectMessage()
        {
            MembershipUser user = Membership.GetUser(new Guid(receiverId));
            litReceiver.Text = user.UserName;
            ddReceiver.Visible = false;
        }

        protected void RenderNormalMessage()
        {
            DataObjectList<DataObjectFriend> friends = DataObjects.Load<DataObjectFriend>(new QuickParametersFriends
                                                                                              {
                                                                                                  Udc = UserDataContext.GetUserDataContext(),
                                                                                                  CurrentUserID = UserProfile.Current.UserId,
                                                                                                  OnlyNotBlocked = true,
                                                                                                  SortBy = QuickSort.Title,
                                                                                                  Direction = QuickSortDirection.Asc,
                                                                                                  PageNumber = 1,
                                                                                                  PageSize = 1000,
                                                                                                  IgnoreCache = true
                                                                                              });
            foreach (var friend in friends)
                ddReceiver.Items.Add(new ListItem(friend.Nickname, friend.UserID.Value.ToString()));
            ddReceiver.Items.Insert(0, new ListItem("Empfänger auswählen", ""));
        }

        protected void OnSendClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(receiverId) && !ddReceiver.SelectedValue.IsGuid())
            {
                pnlStatus.Visible = true;
                litStatus.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("MessageNoUsersToSend");
            }
            else if (string.IsNullOrEmpty(txtBody.Text))
            {
                pnlStatus.Visible = true;
                litStatus.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("MessageNoText");
            }
            else
            {
                bool msgSent = true;
                if (ddReceiver.SelectedValue.IsGuid())
                    if (!SendMessage(new List<string>() { ddReceiver.SelectedValue }, txtSubject.Text, txtBody.Text, msgMode, msgId))
                        msgSent = false;
                if (receiverId.IsGuid())
                    if (!SendMessage(new List<string>() { receiverId }, txtSubject.Text, txtBody.Text, msgMode, msgId))
                        msgSent = false;

                if (msgSent)
                    Response.Redirect(returnUrl);
            }
        }

        private static bool SendMessage(List<string> friendIds, string subject, string body, string msgMode, string msgId)
        {
            try
            {
                foreach (string strFriendId in friendIds)
                {
                    _4screen.CSB.DataAccess.Business.Message message = new _4screen.CSB.DataAccess.Business.Message(SiteConfig.GetSiteContext(UserProfile.Current));
                    message.MsgID = Guid.NewGuid();
                    message.FromUserID = UserProfile.Current.UserId;
                    message.UserId = new Guid(strFriendId);
                    message.TypOfMessage = (int)MessageTypes.NormalMessage;
                    message.Subject = subject;
                    message.MsgText = body;
                    message.ShowInInbox = true;
                    message.ShowInOutbox = true;
                    if (msgMode.ToLower() == "reply")
                        _4screen.CSB.DataAccess.Business.Message.SetAnswered(new Guid(msgId));
                    if (msgMode.ToLower() == "forward")
                        _4screen.CSB.DataAccess.Business.Message.SetForwarded(new Guid(msgId));
                    message.SendNormalMessage();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}