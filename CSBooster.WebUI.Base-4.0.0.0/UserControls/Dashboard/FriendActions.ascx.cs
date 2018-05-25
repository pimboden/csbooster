// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class FriendActions : UserControl
    {
        private FriendsActionType type;
        private DataObjectFriend friend;
        private Message message;
        private GuiLanguage language;

        public IReloadable ReloadableControl { get; set; }

        public FriendsActionType FriendsActionType
        {
            get { return type; }
            set { type = value; }
        }

        public DataObjectFriend Friend
        {
            get { return friend; }
            set { friend = value; }
        }

        public Message Message
        {
            get { return message; }
            set { message = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

            if (type == FriendsActionType.Friends)
            {
                HyperLink link = new HyperLink();
                link.CssClass = "friendMessageButton";
                link.ToolTip = language.GetString("CommandSendMessage");
                link.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=Msg&RecId={0}', '{1}', 510, 490, true)", friend.ObjectID.Value, language.GetString("CommandSendMessage").StripForScript());
                link.ID = null;
                Ph.Controls.Add(link);

                if (CustomizationSection.CachedInstance.Modules["Alerts"].Enabled)
                {
                    LinkButton birthLinkButton = new LinkButton();
                    birthLinkButton.ID = "bdButton";
                    if (friend.AllowBirthdayNotification > 0)
                    {
                        birthLinkButton.CssClass = "friendBirthdayButtonActive";
                        birthLinkButton.ToolTip = language.GetString("TooltipDeactivateBirthdayInfo");
                        birthLinkButton.Click += UnBirthLinkButtonClick;
                    }
                    else
                    {
                        birthLinkButton.CssClass = "friendBirthdayButtonInactive";
                        birthLinkButton.ToolTip = language.GetString("TooltipActivateBirthdayInfo");
                        birthLinkButton.Click += BirthLinkButtonClick;
                    }
                    birthLinkButton.CommandArgument = friend.ObjectID.Value.ToString();
                    Ph.Controls.Add(birthLinkButton);
                }

                LinkButton blockLinkButton = new LinkButton();
                blockLinkButton.ID = "blockButton";
                blockLinkButton.CssClass = "friendBlockButtonInactive";
                blockLinkButton.ToolTip = language.GetString("LableBlock");
                blockLinkButton.Click += BlockLinkButtonClick;
                blockLinkButton.CommandArgument = friend.ObjectID.Value.ToString();
                Ph.Controls.Add(blockLinkButton);

                LinkButton removeLinkButton = new LinkButton();
                removeLinkButton.ID = "removeButton";
                removeLinkButton.CssClass = "friendRemoveButton";
                removeLinkButton.ToolTip = language.GetString("TooltipRemoveFromFriends");
                removeLinkButton.CommandArgument = friend.ObjectID.Value.ToString();
                removeLinkButton.Click += RemoveLinkButtonClick;
                Ph.Controls.Add(removeLinkButton);

                Ph.Controls.Add(new LiteralControl(string.Format("<input type=\"checkbox\" name=\"YMSEL\" value=\"{0}\"/>", friend.ObjectID.Value)));
            }
            if (type == FriendsActionType.RequestReceived || type == FriendsActionType.RequestSent)
            {
                HyperLink link = new HyperLink();
                link.CssClass = "friendMessageButton";
                link.ToolTip = language.GetString("CommandSendMessage");
                link.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=Msg&RecId={0}', '{1}', 510, 490, true)", type == FriendsActionType.RequestSent ? message.UserId : message.FromUserID, language.GetString("CommandSendMessage").StripForScript());
                link.ID = null;
                Ph.Controls.Add(link);
            }
            if (type == FriendsActionType.RequestSent)
            {
                LinkButton removeLinkButton = new LinkButton();
                removeLinkButton.ID = "removeButton";
                removeLinkButton.CssClass = "friendRemoveButton";
                removeLinkButton.ToolTip = language.GetString("TooltipRemoveFromFriends");
                removeLinkButton.CommandArgument = message.MsgID.ToString();
                removeLinkButton.Click += CancelMyRequest;
                Ph.Controls.Add(removeLinkButton);
            }
        }

        private void BirthLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.BirthdayNotification(UserProfile.Current.UserId.ToString(), ((LinkButton)sender).CommandArgument, 1);
            ReloadableControl.Reload();
        }

        private void UnBirthLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.BirthdayNotification(UserProfile.Current.UserId.ToString(), ((LinkButton)sender).CommandArgument, 0);
            ReloadableControl.Reload();
        }

        private void BlockLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.BlockFriend(UserProfile.Current.UserId, new Guid(((LinkButton)sender).CommandArgument));
            ReloadableControl.Reload();
        }

        private void UnBlockLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.UnBlockFriend(UserProfile.Current.UserId, new Guid(((LinkButton)sender).CommandArgument));
            ReloadableControl.Reload();
        }

        private void RemoveLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.DeleteFriend(UserProfile.Current.UserId, new Guid(((LinkButton)sender).CommandArgument));
            ReloadableControl.Reload();
        }

        protected void CancelMyRequest(object sender, EventArgs e)
        {
            Message currentMessage = Message.LoadMessage(new Guid(((LinkButton)sender).CommandArgument), _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            Message.DeleteMessage(currentMessage.MsgID, currentMessage.UserId);
            Message.DeleteMessage(currentMessage.MsgID, currentMessage.FromUserID);
            ReloadableControl.Reload();
        }
    }
}