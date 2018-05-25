//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / AW
//  Updated:   #1.0.4.0    06.12.2007 / AW
//                         - Show request message in scrollable div
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Configuration;
using System.Net.Mail;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class FriendRequest : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        string msgType;
        string msgMode;
        string msgId;
        Guid userId;
        DataAccess.Business.Message message;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.YMTYPESEL.Items.Count == 0)
            {
                Dictionary<FriendType, string> friendTypes = FriendHandler.GetFriendTypes();
                bool selected = true;
                foreach (KeyValuePair<FriendType, string> kvp in friendTypes)
                {
                    ListItem item = new ListItem(kvp.Value, kvp.Key.ToString("d"));
                    item.Selected = selected;
                    this.YMTYPESEL.Items.Add(item);
                    selected = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Friends);

            this.msgType = Request.QueryString["MsgType"];
            this.msgMode = Request.QueryString["MsgMode"];
            this.msgId = Request.QueryString["MsgId"];

            this.message = DataAccess.Business.Message.LoadMessage(new Guid(msgId), _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));

            if (message.UserId == UserProfile.Current.UserId)
            {
                DataAccess.Business.Message.SetRead(new Guid(msgId));
                this.userId = message.FromUserID;
            }
            else
            {
                this.userId = message.UserId;
            }
            Control control = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            ((SmallOutputUser2)control).DataObjectUser = DataObject.Load<DataObjectUser>(userId);
            this.YMREC.Text = ((SmallOutputUser2)control).GetHtml();

            this.MSGCNT.Text = message.MsgText; 

            if (msgMode == "Accept")
            {
                this.MSGREPLYTITLE.Text = string.Format("{0}:", language.GetString("TitleRequestAccept"));
                this.YMTYPEPAN.Visible = true;
                this.YMBLOCKPAN.Visible = false;
                this.YMBIRTH.Visible = true;
            }
            else
            {
                this.MSGREPLYTITLE.Text = string.Format("{0}:", language.GetString("TitleRequestDeny"));
                this.YMTYPEPAN.Visible = false;
                this.YMBLOCKPAN.Visible = true;
                this.YMBIRTH.Visible = false;
                this.YMBIRTH.Checked = false;
            }
        }

        protected void OnSendMsgClick(object sender, EventArgs e)
        {
            if (msgMode == "Accept")
            {
                SendMessage(userId, language.GetString("TextRequestAccepted"), this.MSGREPLYCNT.Content);

                FriendHandler.Save(UserProfile.Current.UserId, userId, false, int.Parse(this.YMTYPESEL.SelectedValue), this.YMBIRTH.Checked ? 1 : 0);
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("FRIEND_ACCEPT", UserDataContext.GetUserDataContext());
                DataAccess.Business.Message.DeleteMessage(message.MsgID, message.UserId);
                DataAccess.Business.Message.DeleteMessage(message.MsgID, message.FromUserID);

                litScript.Text = "<script type='text/javascript'>$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );</script>";
            }
            else
            {
                SendMessage(userId, language.GetString("TextRequestDenied"), this.MSGREPLYCNT.Content);

                if (this.YMBLOCKCB.Checked)
                {
                    FriendHandler.BlockFriend(UserProfile.Current.UserId, message.FromUserID);
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("FRIEND_BLOCK", UserDataContext.GetUserDataContext());
                }

                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("FRIEND_REJECT", UserDataContext.GetUserDataContext());
                DataAccess.Business.Message.DeleteMessage(message.MsgID, message.UserId);
                DataAccess.Business.Message.DeleteMessage(message.MsgID, message.FromUserID);

                litScript.Text = "<script type='text/javascript'>$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );</script>";
            }
        }

        private void SendMessage(Guid userId, string subject, string messageTxt)
        {
            DataAccess.Business.Message message = new DataAccess.Business.Message(_4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            message.MsgID = Guid.NewGuid();
            message.FromUserID = UserProfile.Current.UserId;
            message.UserId = userId;
            message.TypOfMessage = (int)MessageTypes.NormalMessage;
            message.Subject = subject;
            message.MsgText = messageTxt;
            message.ShowInInbox = true;
            message.ShowInOutbox = true;
            message.SendNormalMessage();
        }
    }
}