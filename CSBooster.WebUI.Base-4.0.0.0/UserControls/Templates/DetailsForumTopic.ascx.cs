// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsForumTopic : System.Web.UI.UserControl, IBrowsable, IForumTopicDetails, IDataObjectWorker, ISettings
    {
        private UserDataContext udc;
        protected DataObject dataObject;
        private DataObjectForumTopic forumTopic;
        protected int currentPage = 1;
        private int pageSize = 10;
        private int pagerBreak = 4;
        protected int totalPages;
        private int totalNumberItems = 0;
        private bool isCommunityMember = false;
        private bool isCommunityOwner = false;
        private bool isRoleAllowed = false;
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

        protected void Page_Load(object sender, EventArgs e)
        {
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

            forumTopic = (DataObjectForumTopic)dataObject;

            SiteObjectType objectType = Helper.GetObjectType("ForumTopicItem");
            isRoleAllowed = Array.Exists(objectType.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y));
            isCommunityOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forumTopic.CommunityID.Value, out isCommunityMember);
            if ((forumTopic.TopicItemCreationUsers == CommunityUsersType.Members && isCommunityMember) ||
                (forumTopic.TopicItemCreationUsers == CommunityUsersType.Authenticated && udc.IsAuthenticated) ||
                 isRoleAllowed)
            {
                LnkAdd.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/ForumTopicItemComment.aspx?ObjID={0}&ObjType={1}', '{2}', 470, 370, true)", forumTopic.ObjectID, Helper.GetObjectTypeNumericID("ForumTopic"), languageShared.GetString("CommandEntryAdd")));
            }
            else
            {
                LnkAdd.Enabled = false;
                LnkAdd.ToolTip = (new TextControl() { LanguageFile = "UserControls.Templates.WebUI.Base", TextKey = "TooltipLoginToAddForumTopicItem" }).Text;
            }
            LnkAdd.ID = null;

            this.LitDesc.Text = forumTopic.Description;

            RestoreState();
            Reload();
        }

        private void Reload()
        {
            DataObjectList<DataObjectForumTopicItem> itemList = DataObjects.Load<DataObjectForumTopicItem>(new QuickParameters
                                                                                                               {
                                                                                                                   RelationParams = new RelationParams
                                                                                                                   {
                                                                                                                       ParentObjectID = forumTopic.ObjectID,
                                                                                                                       ParentObjectType = Helper.GetObjectTypeNumericID("ForumTopic")
                                                                                                                   },
                                                                                                                   Udc = udc,
                                                                                                                   PageNumber = currentPage,
                                                                                                                   CurrentObjectID = Request.QueryString["COID"].ToNullableGuid(),
                                                                                                                   PageSize = pageSize,
                                                                                                                   IgnoreCache = true,
                                                                                                                   SortBy = QuickSort.InsertedDate,
                                                                                                                   Direction = QuickSortDirection.Asc
                                                                                                               });
            currentPage = itemList.PageNumber;
            totalNumberItems = itemList.ItemTotal;
            FTPAGBOT.InitPager(currentPage, totalNumberItems);
            FTPAGTOP.InitPager(currentPage, totalNumberItems);

            RepForumTopic.DataSource = itemList;
            RepForumTopic.DataBind();
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

        protected void OnForumTopicItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectForumTopicItem item = (DataObjectForumTopicItem)e.Item.DataItem;
            Literal postDate = (Literal)e.Item.FindControl("LitPosterInfo");
            postDate.Text = string.Format("{0} {1}", item.Inserted.ToShortDateString(), item.Inserted.ToShortTimeString());

            Control posterControl = e.Item.FindControl("PhPoster");
            SmallOutputUser2 poster = (SmallOutputUser2)this.LoadControl("~/UserControls/Templates/SmallOutputUser2.ascx");
            if (item.UserID != Constants.ANONYMOUS_USERID.ToGuid())
            {
                poster.DataObjectUser = DataObject.Load<DataObjectUser>(item.UserID);
                poster.LinkActive = true;
                posterControl.Controls.Add(poster);
                bool isPosterCommunityMember;
                bool isPosterCommunityOwner = Community.GetIsUserOwner(item.UserID.Value, forumTopic.CommunityID.Value, out isPosterCommunityMember);
                if (isPosterCommunityOwner)
                    posterControl.Controls.Add(new LiteralControl("<div class=\"forumModerator\">" + language.GetString("LabelModerator") + "</div>"));
            }
            else
            {
                poster.UserName = item.Nickname;
                poster.UserDetailURL = string.Empty;
                poster.UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                poster.PrimaryColor = Helper.GetDefaultUserPrimaryColor();
                poster.SecondaryColor = Helper.GetDefaultUserSecondaryColor();
                posterControl.Controls.Add(poster);
            }

            Control placeHolderfunctions = (PlaceHolder)e.Item.FindControl("PhFunc");
            if (((forumTopic.TopicItemCreationUsers == CommunityUsersType.Members && isCommunityMember) ||
                 (forumTopic.TopicItemCreationUsers == CommunityUsersType.Authenticated && Request.IsAuthenticated) ||
                  isRoleAllowed) &&
                  item.ItemStatus != CommentStatus.Deleted &&
                  item.ShowState == ObjectShowState.Published)
            {
                HyperLink replyButton = new HyperLink();
                replyButton.CssClass = "inputButton";
                replyButton.Text = languageShared.GetString("CommandComment");
                replyButton.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/ForumTopicItemComment.aspx?ObjID={0}&ObjIDRef={1}&ObjType={2}', '{3}', 470, 370, true)", forumTopic.ObjectID.Value, item.ObjectID.Value, Helper.GetObjectTypeNumericID("ForumTopic"), languageShared.GetString("CommandComment"));
                placeHolderfunctions.Controls.Add(replyButton);
            }
            if (isCommunityOwner && item.ShowState == ObjectShowState.Draft)
            {
                LinkButton publishButton = new LinkButton();
                publishButton.CssClass = "inputButton";
                publishButton.Text = language.GetString("CommandForumReleasing");
                publishButton.CommandArgument = item.ObjectID.ToString();
                publishButton.Click += new EventHandler(OnPublishItemClick);
                placeHolderfunctions.Controls.Add(publishButton);
            }
            if (isCommunityOwner && item.ItemStatus == CommentStatus.None)
            {
                LinkButton removeButton = new LinkButton();
                removeButton.CssClass = "inputButton";
                removeButton.Text = language.GetString("CommandForumRemove");
                removeButton.CommandArgument = item.ObjectID.ToString();
                removeButton.Click += new EventHandler(OnRemoveItemClick);
                placeHolderfunctions.Controls.Add(removeButton);
            }

            if (item.ShowState == ObjectShowState.Draft)
            {
                ((HtmlTableRow)e.Item.FindControl("Tr1")).Attributes.Add("class", "forumDraft");
                ((HtmlTableRow)e.Item.FindControl("Tr2")).Attributes.Add("class", "forumDraft");
            }

            Control topicItemContent = e.Item.FindControl("PhContent");
            if (item.ReferencedItemId.HasValue)
            {
                DataObjectForumTopicItem referencedItem = DataObject.Load<DataObjectForumTopicItem>(item.ReferencedItemId.Value, ObjectShowState.Published, false);
                if (referencedItem != null && referencedItem.State != ObjectState.Added)
                {
                    topicItemContent.Controls.Add(new LiteralControl(string.Format("<div class=\"forumCitation\"><div class=\"forumInfo\">{0}{1}</div><div>{2}</div></div>",
                        referencedItem.UserID != Constants.ANONYMOUS_USERID.ToGuid() ? string.Format("<a href='{0}'>{1}</a>", Helper.GetDetailLink("User", referencedItem.Nickname), referencedItem.Nickname) : referencedItem.Nickname,
                        string.Format(language.GetString("MessageForumTopicHasWrite"), referencedItem.Inserted.ToShortDateString() + " " + referencedItem.Inserted.ToShortTimeString()),
                        referencedItem.ItemStatus == CommentStatus.Deleted ? language.GetString("MessageForumRemove") : referencedItem.Description)));
                }
            }

            if (item.ItemStatus == CommentStatus.Deleted)
                topicItemContent.Controls.Add(new LiteralControl(language.GetString("MessageForumRemove")));
            else if (item.ShowState == ObjectShowState.Published || isCommunityOwner || item.UserID == udc.UserID)
                topicItemContent.Controls.Add(new LiteralControl(item.Description));
            else
                e.Item.Visible = false;
        }

        private void OnRemoveItemClick(object sender, EventArgs e)
        {
            Guid objectId = ((LinkButton)sender).CommandArgument.ToGuid();
            DataObjectForumTopicItem item = DataObject.Load<DataObjectForumTopicItem>(objectId, null, true);
            item.ItemStatus = CommentStatus.Deleted;
            item.Update(UserDataContext.GetUserDataContext());

            Reload();
        }

        private void OnPublishItemClick(object sender, EventArgs e)
        {
            Guid objectId = ((LinkButton)sender).CommandArgument.ToGuid();
            DataObjectForumTopicItem item = DataObject.Load<DataObjectForumTopicItem>(objectId, null, true);
            item.ShowState = ObjectShowState.Published;
            item.Update(UserDataContext.GetUserDataContext());

            Reload();
        }
    }
}
