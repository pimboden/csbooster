// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class Dashboard : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Control control = null;
            string title = string.Empty;
            GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
            
            if (!string.IsNullOrEmpty(Request.QueryString["dashboard"]))
            {
                Common.Dashboard dashboard = (Common.Dashboard)Enum.Parse(typeof(Common.Dashboard), Request.QueryString["dashboard"]);
                switch (dashboard)
                {
                    case Common.Dashboard.SettingsBasic:
                        control = LoadControl("~/UserControls/Dashboard/SettingsBasic.ascx");
                        title = language.GetString("TitleSettingsBasic");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(UserProfile.Current.UserId, 19, IsPostBack, LogSitePageType.UserProfileData);
                        break;
                    case Common.Dashboard.SettingsAdvanced:
                        control = LoadControl("~/UserControls/Dashboard/SettingsAdvanced.ascx");
                        title = language.GetString("TitleSettingsAdvanced");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(UserProfile.Current.UserId, 19, IsPostBack, LogSitePageType.UserProfileData);
                        break;
                    case Common.Dashboard.SettingsInterests:
                        control = LoadControl("~/UserControls/Dashboard/SettingsInterests.ascx");
                        title = language.GetString("TitleSettingsInterests");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(UserProfile.Current.UserId, 19, IsPostBack, LogSitePageType.UserProfileData);
                        break;
                    case Common.Dashboard.SettingsAlerts:
                        control = LoadControl("~/UserControls/Dashboard/SettingsAlerts.ascx");
                        title = language.GetString("TitleSettingsAlerts");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(UserProfile.Current.UserId, 19, IsPostBack, LogSitePageType.UserProfileData);
                        break;

                    case Common.Dashboard.MessagesInbox:
                        control = LoadControl("~/UserControls/Dashboard/Msgbox.ascx");
                        ((IMessageBox)control).MessageBoxType = MessageBoxType.Inbox;
                        title = language.GetString("TitleInbox");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);
                        break;
                    case Common.Dashboard.MessagesOutbox:
                        control = LoadControl("~/UserControls/Dashboard/Msgbox.ascx");
                        ((IMessageBox)control).MessageBoxType = MessageBoxType.Outbox;
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);
                        title = language.GetString("TitleOutbox");
                        break;
                    case Common.Dashboard.MessagesFlagged:
                        control = LoadControl("~/UserControls/Dashboard/Msgbox.ascx");
                        ((IMessageBox)control).MessageBoxType = MessageBoxType.Flagged;
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Messaging);
                        title = language.GetString("TitleMarked");
                        break;

                    case Common.Dashboard.FriendsRequestsReceived:
                        control = LoadControl("~/UserControls/Dashboard/Requests.ascx");
                        ((IFriendsRequests)control).FriendsActionType = FriendsActionType.RequestReceived;
                        title = language.GetString("TitleFriendsRequestsReceived");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Friends);
                        break;
                    case Common.Dashboard.FriendsRequestsSent:
                        control = LoadControl("~/UserControls/Dashboard/Requests.ascx");
                        ((IFriendsRequests)control).FriendsActionType = FriendsActionType.RequestSent;
                        title = language.GetString("TitleFriendsRequestsSent");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Friends);
                        break;
                    case Common.Dashboard.Friends:
                        control = LoadControl("~/UserControls/Dashboard/Friends.ascx");
                        ((IFriendsRequests)control).FriendsActionType = FriendsActionType.Friends;
                        title = language.GetString("TitleFriends");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Friends);
                        break;

                    case Common.Dashboard.BlockedUsers:
                        control = LoadControl("~/UserControls/Dashboard/BlockedUsers.ascx");
                        title = language.GetString("TitleBlockedUsers");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Unknow);
                        break;

                    case Common.Dashboard.Surveys:
                        control = LoadControl("~/UserControls/Dashboard/SurveyHistory.ascx");
                        title = language.GetString("TitleMySurveyHistory");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Unknow);
                        break;

                    case Common.Dashboard.CommentsReceived:
                        control = LoadControl("~/UserControls/Dashboard/Comments.ascx");
                        ((IComments)control).CommentsType = CommentsType.CommentsReceived;
                        title = language.GetString("TitleCommentsReceived");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Comments);
                        break;
                    case Common.Dashboard.CommentsPosted:
                        control = LoadControl("~/UserControls/Dashboard/Comments.ascx");
                        ((IComments)control).CommentsType = CommentsType.CommentsPosted;
                        title = language.GetString("TitleCommentsPosted");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Comments);
                        break;

                    case Common.Dashboard.ManageContent:
                        control = LoadControl("~/UserControls/Dashboard/ManageContent.ascx");
                        title = language.GetString("TitleManageContent");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.MyContent);
                        break;

                    case Common.Dashboard.CommunityMemberships:
                        control = LoadControl("~/UserControls/Dashboard/MyMemberships.ascx");
                        title = language.GetString("TitleMyCommunityMembers");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Memberships);
                        break;
                    case Common.Dashboard.CommunityInvitations:
                        control = LoadControl("~/UserControls/Dashboard/MyCommInvitations.ascx");
                        title = language.GetString("TitleInvitation");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Memberships);
                        break;

                    case Common.Dashboard.Alerts:
                        control = LoadControl("~/UserControls/Dashboard/Alerts.ascx");
                        title = language.GetString("TitleAlerts");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Notification);
                        break;
                    case Common.Dashboard.Favorites:
                        control = LoadControl("~/UserControls/Dashboard/MyFavorites.ascx");
                        title = language.GetString("TitleFavorites");
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Favorites);
                        break;
                }
            }
            else
            {
                control = LoadControl("~/UserControls/Dashboard/Dashboard.ascx");
                title = language.GetString("TitleDashboard");
                _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.UserProfile);
            }

            WidgetHost.SetWidgetTitle(title);
            ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderAdminPageBreadCrumbs(title);
            control.ID = "db";
            Ph.Controls.Add(control);
            return true;
        }
    }
}
