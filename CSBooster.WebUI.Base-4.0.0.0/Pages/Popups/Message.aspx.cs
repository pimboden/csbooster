//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / AW
//  Updated:   
//******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class Message : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        string messageId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging); 

            this.messageId = Request.QueryString["MsgId"];

            DataAccess.Business.Message message = DataAccess.Business.Message.LoadMessage(new Guid(messageId), _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));

            Guid userId;
            string username = string.Empty;
            if (message.UserId == UserProfile.Current.UserId) // Inbox
            {
                userId = message.FromUserID;
                username = message.FromUserName;
                this.LitSenderReceiver.Text = string.Format("{0}:", language.GetString("LableRequestSender"));

                if (message.TypOfMessage != 5)
                {
                    this.REPLYLINK.Visible = true;
                    this.REPLYLINK.NavigateUrl = string.Format("/Pages/Popups/MessageSend.aspx?MsgType=Msg&MsgMode=Reply&RecId={0}&MsgId={1}", message.FromUserID, message.MsgID);
                }

                if (!FriendHandler.IsBlocked(UserProfile.Current.UserId, message.FromUserID))
                {
                    this.LbtnBlockUser.CommandArgument = message.FromUserID.ToString();
                    this.LbtnBlockUser.Visible = true;
                }
            }
            else // Outbox
            {
                userId = message.UserId;
                username = message.UserName;
                this.LitSenderReceiver.Text = string.Format("{0}:", language.GetString("LableRequestReceiver"));
            }
            Control control = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            ((SmallOutputUser2)control).DataObjectUser = DataObject.Load<DataObjectUser>(userId);
            this.YMREC.Text = ((SmallOutputUser2)control).GetHtml();

            this.MSGSUB.Text = message.Subject;
            this.MSGCNT.Text = message.MsgText;
        }

        public void OnBlockUserClick(object sender, EventArgs e)
        {
            Guid blockedUserId = new Guid(((LinkButton)sender).CommandArgument);
            FriendHandler.BlockFriend(UserProfile.Current.UserId, blockedUserId);
            this.LitScript.Text = "<script type='text/javascript'>$telerik.$(function() { GetRadWindow().Close(); } );</script>";
        }
    }
}