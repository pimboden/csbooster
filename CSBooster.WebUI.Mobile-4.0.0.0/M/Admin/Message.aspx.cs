// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class Message : System.Web.UI.Page
    {
        private string msgType;
        private string msgMode;
        private string msgId;
        private string returnUrl;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);
            msgType = Request.QueryString["MsgType"];
            msgMode = Request.QueryString["MsgMode"];
            msgId = Request.QueryString["MsgId"];

            _4screen.CSB.DataAccess.Business.Message message = _4screen.CSB.DataAccess.Business.Message.LoadMessage(new Guid(msgId), SiteConfig.GetSiteContext(UserProfile.Current));

            Guid userId;
            if (message.UserId == UserProfile.Current.UserId) // Inbox
            {
                userId = message.FromUserID;
                lnkBack.NavigateUrl = "/M/Admin/MyMessagesInbox.aspx";
            }
            else // Outbox
            {
                userId = message.UserId;
                lnkBack.NavigateUrl = "/M/Admin/MyMessagesOutbox.aspx";
            }
            lnkBack.ID = null;
            MembershipUser user = Membership.GetUser(userId);
            litInfo.Text = language.GetString("LabelMessageFrom") + user.UserName;
            litSubject.Text = message.Subject;
            litBody.Text = message.MsgText;

            if (message.TypOfMessage != 5)
            {
                string link = string.Format("/M/Admin/MessageSend.aspx?MsgType=Msg&amp;MsgMode=Reply&amp;RecId={0}&amp;MsgId={1}&amp;ReturnUrl={2}", message.FromUserID, message.MsgID, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(lnkBack.NavigateUrl)));
                phReplyLink.Controls.Add(new LiteralControl(string.Format("<a href='{0}' class='button' target='_self'>{1}</a>", link, GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandRespond"))));
            }
        }
    }
}