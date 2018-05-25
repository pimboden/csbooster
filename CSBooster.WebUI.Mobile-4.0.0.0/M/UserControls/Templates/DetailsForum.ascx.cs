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
    public partial class DetailsForum : System.Web.UI.UserControl, IDataObjectWorker
    {
        private UserDataContext udc;
        protected DataObject dataObject;
        private DataObjectForum forum;
        protected int currentPage = 1;
        private int pageSize = 10;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            if (!string.IsNullOrEmpty(Request.QueryString["PN"]))
                currentPage = Convert.ToInt32(Request.QueryString["PN"]);
            pager.PageSize = pageSize;

            forum = (DataObjectForum)dataObject;

            LitDesc.Text = forum.Description;

            bool isMember;
            bool isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forum.CommunityID.Value, out isMember);
            if ((forum.ThreadCreationUsers == CommunityUsersType.Owners && isOwner) ||
                (forum.ThreadCreationUsers == CommunityUsersType.Members && isMember) ||
                (forum.ThreadCreationUsers == CommunityUsersType.Authenticated && udc.IsAuthenticated) ||
                udc.IsAdmin)
            {
                lnkCreate.NavigateUrl = string.Format("/M/Admin/EditForumTopic.aspx?FID={0}", forum.ObjectID);
            }
            else
            {
                lnkCreate.Enabled = false;
            }
            lnkCreate.ID = null;

            Reload();
        }

        private void Reload()
        {
            DataObjectList<DataObjectForumTopic> forumTopics = DataObjects.Load<DataObjectForumTopic>(new QuickParameters
                                                                                                          {
                                                                                                              RelationParams = new RelationParams
                                                                                                                                   {
                                                                                                                                       ParentObjectID = forum.ObjectID,
                                                                                                                                       ParentObjectType = Helper.GetObjectTypeNumericID("Forum")
                                                                                                                                   },
                                                                                                              Udc = udc,
                                                                                                              PageNumber = currentPage,
                                                                                                              PageSize = pageSize,
                                                                                                              ShowState = ObjectShowState.Published,
                                                                                                              IgnoreCache = true
                                                                                                          });
            RepForum.DataSource = forumTopics;
            RepForum.DataBind();
            pager.InitPager(currentPage, forumTopics.ItemTotal);
        }

        protected void OnForumItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectForumTopic forumTopic = (DataObjectForumTopic)e.Item.DataItem;

            HyperLink forumTopicLink = (HyperLink)e.Item.FindControl("LnkTopicLink");
            forumTopicLink.NavigateUrl = Helper.GetMobileDetailLink("ForumTopic", forumTopic.ObjectID.Value.ToString());
            forumTopicLink.ID = null;
            Literal forumTopicTitle = (Literal)e.Item.FindControl("LitTopicTitle");
            forumTopicTitle.Text = forumTopic.Title;
            Literal forumTopicDesc = (Literal)e.Item.FindControl("LitTopicDesc");
            forumTopicDesc.Text = forumTopic.Description.CropString(200);

            Literal numberPosts = (Literal)e.Item.FindControl("LitNumberPosts");
            numberPosts.Text = forumTopic.TopicItemCount.ToString("N0");
            Literal numberViews = (Literal)e.Item.FindControl("LitNumberViews");
            numberViews.Text = forumTopic.ViewCount.ToString("N0");
        }
    }
}