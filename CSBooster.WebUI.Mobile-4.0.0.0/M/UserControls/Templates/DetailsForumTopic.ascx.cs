using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls.Templates
{
    public partial class DetailsForumTopic : System.Web.UI.UserControl, IDataObjectWorker
    {
        private UserDataContext udc;
        private DataObjectForumTopic forumTopic;
        protected int currentPage = 1;
        private int pageSize = 10;
        private bool isCommunityMember = false;
        private bool isCommunityOwner = false;
        private bool isRoleAllowed = false;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            var forum = DataObject.GetParents<DataObjectForum>(udc);
            lnkBack.Text = Helper.GetObjectName(forum[0].ObjectType, true);
            lnkBack.NavigateUrl = Helper.GetMobileDetailLink(forum[0].ObjectType, forum[0].ObjectID.ToString());
            lnkBack.ID = null;

            if (!string.IsNullOrEmpty(Request.QueryString["PN"]))
                currentPage = Convert.ToInt32(Request.QueryString["PN"]);
            pager.PageSize = pageSize;

            forumTopic = (DataObjectForumTopic)DataObject;

            SiteObjectType objectType = Helper.GetObjectType("ForumTopicItem");
            isRoleAllowed = Array.Exists(objectType.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y));
            isCommunityOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forumTopic.CommunityID.Value, out isCommunityMember);
            if ((forumTopic.TopicItemCreationUsers == CommunityUsersType.Members && isCommunityMember) ||
                (forumTopic.TopicItemCreationUsers == CommunityUsersType.Authenticated && udc.IsAuthenticated) ||
                 isRoleAllowed)
            {
                lnkCreate.NavigateUrl = string.Format("/M/Admin/EditForumTopicItem.aspx?FTID={0}&ReturnUrl={1}", forumTopic.ObjectID, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
            }
            else
            {
                lnkCreate.Enabled = false;
            }
            lnkCreate.ID = null;

            LitTitle.Text = _4screen.Utils.Extensions.EscapeForXHTML(forumTopic.Title);
            LitDesc.Text = _4screen.Utils.Extensions.EscapeForXHTML(forumTopic.Description);

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
                                                                                                                   PageSize = pageSize,
                                                                                                                   IgnoreCache = true,
                                                                                                                   SortBy = QuickSort.InsertedDate,
                                                                                                                   Direction = QuickSortDirection.Asc
                                                                                                               });

            pager.InitPager(currentPage, itemList.ItemTotal);

            RepForumTopic.DataSource = itemList;
            RepForumTopic.DataBind();
        }

        protected void OnForumTopicItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectForumTopicItem item = (DataObjectForumTopicItem)e.Item.DataItem;
            Literal postDate = (Literal)e.Item.FindControl("LitPosterInfo");
            postDate.Text = string.Format("{0} {1}", item.Inserted.ToShortDateString(), item.Inserted.ToShortTimeString());

            Control posterControl = e.Item.FindControl("PhPoster");
            Control poster = this.LoadControl("~/UserControls/Templates/SmallOutputUser2.ascx");
            DataObjectUser posterUser = DataObject.Load<DataObjectUser>(item.UserID, ObjectShowState.Published, false);
            if (item.UserID != Constants.ANONYMOUS_USERID.ToGuid())
            {
                ((ISmallOutputUser)poster).DataObjectUser = DataObject.Load<DataObjectUser>(item.UserID);
                posterControl.Controls.Add(poster);
                bool isPosterCommunityMember;
                bool isPosterCommunityOwner = Community.GetIsUserOwner(item.UserID.Value, forumTopic.CommunityID.Value, out isPosterCommunityMember);
                if (isPosterCommunityOwner)
                    posterControl.Controls.Add(new LiteralControl("<div class=\"forumModerator\">" + language.GetString("LabelModerator") + "</div>"));
            }
            else
            {
                ((ISmallOutputUser)poster).UserName = item.Nickname;
                ((ISmallOutputUser)poster).UserDetailURL = string.Empty;
                ((ISmallOutputUser)poster).UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                ((ISmallOutputUser)poster).PrimaryColor = "4";
                ((ISmallOutputUser)poster).SecondaryColor = "8";
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
                replyButton.CssClass = "button";
                replyButton.Text = languageShared.GetString("CommandComment");
                replyButton.NavigateUrl = string.Format("/M/Admin/EditForumTopicItem.aspx?FTID={0}&RefOID={1}&ReturnUrl={2}", forumTopic.ObjectID.Value, item.ObjectID.Value, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
                placeHolderfunctions.Controls.Add(replyButton);
            }
            if (isCommunityOwner && item.ShowState == ObjectShowState.Draft)
            {
                LinkButton publishButton = new LinkButton();
                publishButton.CssClass = "buttonSecondary";
                publishButton.Text = language.GetString("CommandForumReleasing");
                publishButton.CommandArgument = item.ObjectID.ToString();
                publishButton.Click += new EventHandler(OnPublishItemClick);
                placeHolderfunctions.Controls.Add(publishButton);
            }
            if (isCommunityOwner && item.ItemStatus == CommentStatus.None)
            {
                LinkButton removeButton = new LinkButton();
                removeButton.CssClass = "buttonSecondary";
                removeButton.Text = language.GetString("CommandForumRemove");
                removeButton.CommandArgument = item.ObjectID.ToString();
                removeButton.Click += new EventHandler(OnRemoveItemClick);
                placeHolderfunctions.Controls.Add(removeButton);
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

        protected void OnRemoveItemClick(object sender, EventArgs e)
        {
            Guid objectId = ((LinkButton)sender).CommandArgument.ToGuid();
            DataObjectForumTopicItem item = DataObject.Load<DataObjectForumTopicItem>(objectId, null, true);
            item.ItemStatus = CommentStatus.Deleted;
            item.Update(UserDataContext.GetUserDataContext());

            Reload();
        }

        protected void OnPublishItemClick(object sender, EventArgs e)
        {
            Guid objectId = ((LinkButton)sender).CommandArgument.ToGuid();
            DataObjectForumTopicItem item = DataObject.Load<DataObjectForumTopicItem>(objectId, null, true);
            item.ShowState = ObjectShowState.Published;
            item.Update(UserDataContext.GetUserDataContext());

            Reload();
        }
    }
}