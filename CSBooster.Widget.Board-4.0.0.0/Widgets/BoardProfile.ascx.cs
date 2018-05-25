// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class BoardProfile : System.Web.UI.UserControl
    {
        protected UserDataContext udc;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetBoard");
        
        public bool ShowComments
        {
            get
            {
                return PnlComments.Visible;
            }
            set
            {
                PnlComments.Visible = value;
            }
        }

        public bool ShowContents
        {
            get
            {
                return PnlContents.Visible;
            }
            set
            {
                PnlContents.Visible = value;
            }
        }

        public bool ShowFavorites
        {
            get
            {
                return PnlFavorites.Visible;
            }
            set
            {
                PnlFavorites.Visible = value;
            }
        }

        public bool ShowFriends
        {
            get
            {
                return PnlFriends.Visible;
            }
            set
            {
                PnlFriends.Visible = value;
            }
        }

        public bool ShowMembership
        {
            get
            {
                return PnlMembership.Visible;
            }
            set
            {
                PnlMembership.Visible = value;
            }
        }

        public bool ShowMessage
        {
            get
            {
                return PnlMessage.Visible;
            }
            set
            {
                PnlMessage.Visible = value;
            }
        }

        public bool ShowNotifications
        {
            get
            {
                return PnlNotifications.Visible;
            }
            set
            {
                PnlNotifications.Visible = value;
            }
        }

        public bool ShowProperties
        {
            get
            {
                return PnlProperties.Visible;
            }
            set
            {
                PnlProperties.Visible = value;
            }
        }

        public bool ShowSurvey
        {
            get
            {
                return PnlSurvey.Visible;
            }
            set
            {
                PnlSurvey.Visible = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            int intMessageCount = Messages.GetInboxUnreadCount(UserProfile.Current.UserId, null);
            int intRequestCount = Messages.GetRequestUnreadCount(UserProfile.Current.UserId);

            this.LnkProf1.NavigateUrl = Helper.GetDashboardLink(Dashboard.SettingsBasic);
            this.LnkProf3.NavigateUrl = Helper.GetDashboardLink(Dashboard.SettingsAdvanced);

            if (intMessageCount == 0)
                this.LnkMsg1.Text = language.GetString("ReceivedMessagesNoUnread");
            else if (intMessageCount == 1)
                this.LnkMsg1.Text = language.GetString("ReceivedMessages1Unread");
            else
                this.LnkMsg1.Text = string.Format(language.GetString("ReceivedMessagesXUnread"), intMessageCount);
            this.LnkMsg1.NavigateUrl = Helper.GetDashboardLink(Dashboard.MessagesInbox);
            this.LnkMsg2.NavigateUrl = Helper.GetDashboardLink(Dashboard.MessagesOutbox);
            this.LnkMsg3.NavigateUrl = Helper.GetDashboardLink(Dashboard.MessagesFlagged);
            this.LnkMsg4.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=Msg', '{0}', 450, 450, false, null)", language.GetString("WriteMessage").StripForScript());

            if (intRequestCount == 0)
                this.LnkFre1.Text = language.GetString("FriendRequestNoUnread");
            else if (intRequestCount == 1)
                this.LnkFre1.Text = language.GetString("FriendRequest1Unread");
            else
                this.LnkFre1.Text = string.Format(language.GetString("FriendRequestXUnread"), intRequestCount);
            this.LnkFre1.NavigateUrl = Helper.GetDashboardLink(Dashboard.FriendsRequestsReceived);
            this.LnkFre2.NavigateUrl = Helper.GetDashboardLink(Dashboard.Friends);
            this.LnkFre3.NavigateUrl = Helper.GetDashboardLink(Dashboard.FriendsRequestsSent);
            this.LnkFre4.NavigateUrl = Helper.GetDashboardLink(Dashboard.BlockedUsers);

            this.LnkTest1.NavigateUrl = Helper.GetDashboardLink(Dashboard.Surveys);

            this.LnkCom1.NavigateUrl = Helper.GetDashboardLink(Dashboard.CommentsReceived);
            this.LnkCom2.NavigateUrl = Helper.GetDashboardLink(Dashboard.CommentsPosted);

            this.LnkMem1.NavigateUrl = Helper.GetDashboardLink(Dashboard.CommunityMemberships);
            this.LnkMem2.NavigateUrl = Helper.GetDashboardLink(Dashboard.CommunityInvitations);

            this.LnkAlerts1.NavigateUrl = Helper.GetDashboardLink(Dashboard.Alerts);
            this.LnkAlerts2.NavigateUrl = Helper.GetDashboardLink(Dashboard.SettingsAlerts);

            this.LnkFav.NavigateUrl = Helper.GetDashboardLink(Dashboard.Favorites);

            List<SiteObjectType> siteObjects = Helper.GetObjectTypes();
            var contentObjectTypes = from allObjcets in siteObjects
                                     select new
                                     {
                                         ObjectType = allObjcets,
                                         MenuTitle = Helper.GetObjectName(allObjcets.NumericId, false)
                                     };


            List<HyperLink> contentLinks = new List<HyperLink>();
            List<InfoObject> infoObjects = InfoObjects.LoadForUser(udc, UserProfile.Current.UserId, null);
            foreach (var type in contentObjectTypes)
            {
                if (infoObjects.Exists(i => i.ObjectType == type.ObjectType.NumericId) && ((type.ObjectType.IsActive && Array.Exists(type.ObjectType.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y))) || UserDataContext.GetUserDataContext().IsAdmin))
                {
                    HyperLink link = new HyperLink();
                    link.Text = string.Format("{0} ({1})", type.MenuTitle, infoObjects.Find(x => x.ObjectType == type.ObjectType.NumericId).Count);
                    link.NavigateUrl = string.Format("{0}&P=dashboard&dashboard={1}&T={2}&I=true", Helper.GetDetailLink("User", UserProfile.Current.UserId.ToString(), false), Dashboard.ManageContent, type.ObjectType.Id);
                    link.Attributes.Add("rel", "nofollow");
                    contentLinks.Add(link);
                }
            }
            if (contentLinks.Count > 0)
            {
                for (int i = 0; i < contentLinks.Count; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.Controls.Add(contentLinks[i]);
                    this.CNTPH.Controls.Add(li);
                }
            }
            else
            {
                if (!udc.IsAdmin)
                {
                    this.CNTPH.Controls.Add(new LiteralControl(string.Format("<div>{0}</div>", language.GetString("NoContentYet"))));
                }
            }

            LnkMsg1.Attributes.Add("rel", "nofollow");
            LnkMsg2.Attributes.Add("rel", "nofollow");
            LnkMsg3.Attributes.Add("rel", "nofollow");
            LnkFre1.Attributes.Add("rel", "nofollow");
            LnkFre2.Attributes.Add("rel", "nofollow");
            LnkFre3.Attributes.Add("rel", "nofollow");
            LnkFre4.Attributes.Add("rel", "nofollow");
            LnkCom1.Attributes.Add("rel", "nofollow");
            LnkCom2.Attributes.Add("rel", "nofollow");
            LnkMem1.Attributes.Add("rel", "nofollow");
            LnkMem2.Attributes.Add("rel", "nofollow");
            LnkAlerts1.Attributes.Add("rel", "nofollow");
            LnkAlerts2.Attributes.Add("rel", "nofollow");
            LnkFav.Attributes.Add("rel", "nofollow");
            LnkProf1.Attributes.Add("rel", "nofollow");
            LnkProf3.Attributes.Add("rel", "nofollow");

            LnkMsg1.ID = null;
            LnkMsg2.ID = null;
            LnkMsg3.ID = null;
            LnkFre1.ID = null;
            LnkFre2.ID = null;
            LnkFre3.ID = null;
            LnkFre4.ID = null;
            LnkCom1.ID = null;
            LnkCom2.ID = null;
            LnkMem1.ID = null;
            LnkMem2.ID = null;
            LnkAlerts1.ID = null;
            LnkAlerts2.ID = null;
            LnkFav.ID = null;
            LnkProf1.ID = null;
            LnkProf3.ID = null;
            PnlComments.ID = null;
            PnlContents.ID = null;
            PnlFavorites.ID = null;
            PnlFriends.ID = null;
            PnlMembership.ID = null;
            PnlMessage.ID = null;
            PnlNotifications.ID = null;
            PnlProperties.ID = null;
            PnlSurvey.ID = null;
        }
    }
}
