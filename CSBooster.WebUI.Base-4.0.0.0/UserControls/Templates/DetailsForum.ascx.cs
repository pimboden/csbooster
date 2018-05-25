// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using _4screen.WebControls;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsForum : System.Web.UI.UserControl, IBrowsable, IForumDetails, IDataObjectWorker, ISettings
    {
        private UserDataContext udc;
        protected DataObject dataObject;
        private DataObjectForum forum;
        protected int currentPage = 1;
        private int pageSize = 10;
        private int pagerBreak = 4;
        protected int totalPages;
        private int totalNumberItems = 0;
        private bool showTopicColumn = true;
        private bool showStarterColumn = true;
        private bool showInfoColumn = true;
        private bool showLastPosterColumn = true;
        private int lastVisibleRowIndex = 0;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int PagerBreak
        {
            get { return pagerBreak; }
            set { pagerBreak = value; }
        }

        public bool ShowTopicColumn
        {
            get { return showTopicColumn; }
            set { showTopicColumn = value; }
        }

        public bool ShowStarterColumn
        {
            get { return showStarterColumn; }
            set { showStarterColumn = value; }
        }

        public bool ShowInfoColumn
        {
            get { return showInfoColumn; }
            set { showInfoColumn = value; }
        }

        public bool ShowLastPosterColumn
        {
            get { return showLastPosterColumn; }
            set { showLastPosterColumn = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide columns
            if (!showTopicColumn) this.TdTopicH.Visible = false;
            if (!showStarterColumn) this.TdStarterH.Visible = false;
            if (!showInfoColumn) this.TdInfoH.Visible = false;
            if (!showLastPosterColumn) this.TdLastPosterH.Visible = false;

            udc = UserDataContext.GetUserDataContext();

            this.FTPAGTOP.ItemNameSingular = languageShared.GetString("LabelEntrySingular");
            this.FTPAGTOP.ItemNamePlural = languageShared.GetString("LabelEntryPlural");
            this.FTPAGTOP.PageSize = pageSize;
            this.FTPAGTOP.PagerBreak = pagerBreak;
            this.FTPAGTOP.BrowsableControl = this;
            this.FTPAGTOP.CustomText = " ";
            this.FTPAGTOP.RenderHref = true;

            this.FTPAGBOT.ItemNameSingular = languageShared.GetString("LabelEntrySingular");
            this.FTPAGBOT.ItemNamePlural = languageShared.GetString("LabelEntryPlural");
            this.FTPAGBOT.PageSize = pageSize;
            this.FTPAGBOT.PagerBreak = pagerBreak;
            this.FTPAGBOT.BrowsableControl = this;
            this.FTPAGBOT.CustomText = " ";
            this.FTPAGBOT.RenderHref = true;

            forum = (DataObjectForum)dataObject;

            this.LitDesc.Text = forum.Description;

            bool isMember;
            bool isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forum.CommunityID.Value, out isMember);
            if ((forum.ThreadCreationUsers == CommunityUsersType.Owners && isOwner) ||
                (forum.ThreadCreationUsers == CommunityUsersType.Members && isMember) ||
                (forum.ThreadCreationUsers == CommunityUsersType.Authenticated && udc.IsAuthenticated) ||
                 udc.IsAdmin)
            {
                LnkAddTopic.Attributes.Add("onClick", string.Format("radWinOpen('{0}&XCN={1}&OID={2}&FID={3}', '{4}', 800, 500, false, null, 'wizardWin')", Helper.GetUploadWizardLink("ForumTopic", _4screen.CSB.Common.SiteConfig.UsePopupWindows), forum.CommunityID, Guid.NewGuid(), forum.ObjectID, language.GetString("CommandForumTopicAdd")));
            }
            else
            {
                LnkAddTopic.Enabled = false;
                LnkAddTopic.ToolTip = (new TextControl() { LanguageFile = "UserControls.Templates.WebUI.Base", TextKey = "TooltipLoginToAddForumTopic" }).Text;
            }

            RestoreState();
            Reload();
        }

        private void Reload()
        {
            DataObjectList<DataObjectForumTopic> forumTopics = DataObjects.Load<DataObjectForumTopic>(new QuickParameters { RelationParams = new RelationParams { ParentObjectID = forum.ObjectID, ParentObjectType = Helper.GetObjectTypeNumericID("Forum") }, Udc = udc, PageNumber = currentPage, PageSize = pageSize, ShowState = ObjectShowState.Published, IgnoreCache = true });
            totalNumberItems = forumTopics.ItemTotal;
            RepForum.DataSource = forumTopics;
            RepForum.DataBind();
            FTPAGTOP.InitPager(currentPage, totalNumberItems);
            FTPAGBOT.InitPager(currentPage, totalNumberItems);
        }

        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
        }

        private void SaveState()
        {
            if (currentPage != 0)
                PBPageNum.Value = "" + currentPage;
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return totalNumberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
            SaveState();
            Reload();
        }

        protected void OnForumItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!showTopicColumn) e.Item.FindControl("TdTopicC").Visible = false;
            if (!showStarterColumn) e.Item.FindControl("TdStarterC").Visible = false;
            if (!showInfoColumn) e.Item.FindControl("TdInfoC").Visible = false;
            if (!showLastPosterColumn) e.Item.FindControl("TdLastPosterC").Visible = false;

            DataObjectForumTopic forumTopic = (DataObjectForumTopic)e.Item.DataItem;

            HyperLink forumTopicLink = (HyperLink)e.Item.FindControl("LnkTopicLink");
            forumTopicLink.NavigateUrl = Helper.GetDetailLink("ForumTopic", forumTopic.ObjectID.Value.ToString());
            Literal forumTopicTitle = (Literal)e.Item.FindControl("LitTopicTitle");
            forumTopicTitle.Text = forumTopic.Title;
            Literal forumTopicDesc = (Literal)e.Item.FindControl("LitTopicDesc");
            forumTopicDesc.Text = forumTopic.Description.CropString(200);
            if (forumTopic.LastTopicItemID.HasValue)
            {
                ((HyperLink)e.Item.FindControl("LnkOpenLatestPost")).NavigateUrl = Helper.GetDetailLink(forumTopic.ObjectType, forumTopic.ObjectID.ToString(), false) + "&COID=" + forumTopic.LastTopicItemID;
                ((HyperLink)e.Item.FindControl("LnkOpenLatestPost")).Attributes.Add("style", "margin-top: 5px;");
                e.Item.FindControl("LnkOpenLatestPost").Visible = true;
                e.Item.FindControl("LnkOpenLatestPost").ID = null;
            }

            Control threadStarterControl = e.Item.FindControl("PhStarter");
            SmallOutputUser2 threadStarter = (SmallOutputUser2)this.LoadControl("~/UserControls/Templates/SmallOutputUser2.ascx");
            DataObjectUser threadStarterUser = DataObject.Load<DataObjectUser>(forumTopic.UserID, ObjectShowState.Published, false);
            if (forumTopic.UserID != Constants.ANONYMOUS_USERID.ToGuid())
            {
                threadStarter.DataObjectUser = threadStarterUser;
                threadStarter.LinkActive = true;
            }
            else
            {
                threadStarter.UserName = forumTopic.Nickname;
                threadStarter.UserDetailURL = string.Empty;
                threadStarter.UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                threadStarter.PrimaryColor = Helper.GetDefaultUserPrimaryColor();
                threadStarter.SecondaryColor = Helper.GetDefaultUserSecondaryColor();
            }
            threadStarterControl.Controls.Add(threadStarter);
            Literal threadStarterInfo = (Literal)e.Item.FindControl("LitStarterInfo");
            threadStarterInfo.Text = forumTopic.Inserted.ToShortDateString() + " " + forumTopic.Inserted.ToShortTimeString();

            Literal numberPosts = (Literal)e.Item.FindControl("LitNumberPosts");
            numberPosts.Text = forumTopic.TopicItemCount.ToString("N0");
            Literal numberViews = (Literal)e.Item.FindControl("LitNumberViews");
            numberViews.Text = forumTopic.ViewCount.ToString("N0");
            PlaceHolder rating = (PlaceHolder)e.Item.FindControl("PhRating");
            Control ratingControl = LoadControl("/UserControls/Templates/ObjectVotingTelerik.ascx");
            ((IObjectVoting)ratingControl).DataObject = forumTopic;
            rating.Controls.Add(ratingControl);

            Control lastPosterControl = e.Item.FindControl("PhLastPoster");
            Literal lastPosterInfo = (Literal)e.Item.FindControl("ListLastPosterInfo");
            if (forumTopic.TopicItemCount > 0)
            {
                SmallOutputUser2 lastPoster = (SmallOutputUser2)this.LoadControl("~/UserControls/Templates/SmallOutputUser2.ascx");
                DataObjectUser lastPosterUser = DataObject.Load<DataObjectUser>(forumTopic.LastTopicItemUserID, ObjectShowState.Published, false);
                if (forumTopic.LastTopicItemUserID != Constants.ANONYMOUS_USERID.ToGuid())
                {
                    lastPoster.DataObjectUser = lastPosterUser;
                    lastPoster.LinkActive = true;
                }
                else
                {
                    lastPoster.UserName = forumTopic.LastTopicItemNickname;
                    lastPoster.UserDetailURL = string.Empty;
                    lastPoster.UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                    lastPoster.PrimaryColor = Helper.GetDefaultUserPrimaryColor();
                    lastPoster.SecondaryColor = Helper.GetDefaultUserSecondaryColor();
                }
                lastPosterControl.Controls.Add(lastPoster);

                lastPosterInfo.Text = forumTopic.LastTopicItemDate.Value.ToShortDateString() + " " + forumTopic.LastTopicItemDate.Value.ToShortTimeString();
            }
            else
            {
                lastPosterControl.Controls.Add(new LiteralControl("&nbsp;"));
                lastPosterInfo.Text = "&nbsp;";
            }
        }
    }
}