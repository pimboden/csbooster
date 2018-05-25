using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            int messageCount = Messages.GetInboxUnreadCount(UserProfile.Current.UserId, null);
            int requestCount = Messages.GetRequestUnreadCount(UserProfile.Current.UserId);
            int commentsCount;
            QuickParameters quickParams = new QuickParameters
                                              {
                                                  Udc = UserDataContext.GetUserDataContext(),
                                                  CommunityID = UserProfile.Current.ProfileCommunityID,
                                                  ObjectType = Helper.GetObjectTypeNumericID("Comment"),
                                                  FromInserted = DateTime.Now - new TimeSpan(1, 0, 0, 0)

                                              };

            DataObjectList<DataObjectComment> doComments = DataObjects.Load<DataObjectComment>(quickParams);
            commentsCount = doComments.Count;
            if (messageCount == 0)
                lnkNewMessages.Text = language.GetString("TextReceivedMessagesNoUnread");
            else if (messageCount == 1)
                lnkNewMessages.Text = language.GetString("TextReceivedMessages1Unread");
            else
                lnkNewMessages.Text = string.Format(language.GetString("TextReceivedMessagesXUnread"), messageCount);
            lnkNewMessages.NavigateUrl = Helper.GetDashboardLink(Common.Dashboard.MessagesInbox);
            lnkNewMessages.ID = null;

            if (requestCount == 0)
                lnkNewRequests.Text = language.GetString("TextFriendRequestNoUnread");
            else if (requestCount == 1)
                lnkNewRequests.Text = language.GetString("TextFriendRequest1Unread");
            else
                lnkNewRequests.Text = string.Format(language.GetString("TextFriendRequestXUnread"), requestCount);
            lnkNewRequests.NavigateUrl = Helper.GetDashboardLink(Common.Dashboard.FriendsRequestsReceived);
            lnkNewRequests.ID = null;

            if (commentsCount == 0)
                lnkNewComments.Text = language.GetString("TextCommentNoNew");
            else if (commentsCount == 1)
                lnkNewComments.Text = language.GetString("TextComment1New");
            else
                lnkNewComments.Text = string.Format(language.GetString("TextCommentXNew"), commentsCount);
            lnkNewComments.NavigateUrl = Helper.GetDashboardLink(Common.Dashboard.CommentsReceived);
            lnkNewComments.ID = null;

            LoadVisitors();

            LoadUserActivities();
        }

        private void LoadVisitors()
        {
            DataObject community = DataObject.Load<DataObject>(UserProfile.Current.ProfileCommunityID);
            QuickParametersUser paras = new QuickParametersUser();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.ViewLogParams = new ViewLogParams() { ObjectID = UserProfile.Current.ProfileCommunityID };
            paras.Amount = 12;
            paras.PageNumber = 1;
            paras.PageSize = 12;
            paras.DisablePaging = false;
            paras.SortBy = QuickSort.InsertedDate;
            paras.IgnoreCache = false;
            paras.WithCopy = false;
            paras.OnlyConverted = true;
            paras.ShowState = ObjectShowState.Published;
            Control repeaterControl = LoadControl("~/UserControls/Repeaters/UserLists.ascx");
            IRepeater overview = repeaterControl as IRepeater;
            if (overview != null)
            {
                overview.QuickParameters = paras;
                overview.OutputTemplate = "UserListsSmall.ascx";
                overview.TopPagerVisible = false;
                overview.BottomPagerVisible = false;
                overview.ItemNameSingular = " ";
                overview.ItemNamePlural = " ";
            }
            ISettings settings = repeaterControl as ISettings;
            if (settings != null)
            {
                if (settings.Settings == null)
                    settings.Settings = new System.Collections.Generic.Dictionary<string, object>();

                if (!settings.Settings.ContainsKey("ParentPageType"))
                    settings.Settings.Add("ParentPageType", (int)PageType.None);
            }
            PhFriends.Controls.Add(repeaterControl);
        }

        private void LoadUserActivities()
        {
            PnlCnt.Controls.Clear();
            Control control = LoadControl("~/UserControls/Repeaters/UserActivities.ascx");
            IUserActivity objectActivities = (IUserActivity)control;
            objectActivities.OutputTemplate = "UserActivities.ascx";
            objectActivities.UserActivityParameters = new UserActivityParameters { ObjectID = UserProfile.Current.ProfileCommunityID, ObjectType = Helper.GetObjectTypeNumericID("ProfileCommunity"), IgnoreCache = true };
            PnlCnt.Controls.Add(control);
        }

        protected void OnAddStatusClick(object sender, EventArgs e)
        {
            if (TxtInput.Text.Trim().Length > 0)
            {
                _4screen.CSB.DataAccess.Business.UserActivities.InsertDoNowThis(UserDataContext.GetUserDataContext(), TxtInput.Text.Trim());
                TxtInput.Text = string.Empty;
                LoadUserActivities();
            }
        }
    }
}