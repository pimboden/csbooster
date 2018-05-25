// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SearchResultForumTopicItem : System.Web.UI.UserControl, ISettings
    {
        private Dictionary<string, object> settings = new Dictionary<string, object>();

        public Dictionary<string, object> Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObject dataObject = (DataObject)Settings["DataObject"];
            DataObjectForumTopicItem post = DataObject.Load<DataObjectForumTopicItem>(dataObject.ObjectID);

            List<DataObjectForumTopic> forumTopics = DataObjects.Load<DataObjectForumTopic>(new QuickParameters()
            {
                Udc = UserDataContext.GetUserDataContext(),
                Amount = 1,
                DisablePaging = true,
                RelationParams = new RelationParams()
                {
                    ChildObjectID = dataObject.ObjectID
                }
            });

            if (forumTopics.Count == 1 && post.ItemStatus != CommentStatus.Deleted)
            {
                LnkTitle.Text = string.Format(GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base").GetString("MessageForumSearchResult"), dataObject.Nickname, dataObject.Inserted.ToString("dd. MMM yyyy"), forumTopics[0].Title);
                LnkTitle.NavigateUrl =  Helper.GetDetailLink(forumTopics[0].objectType, forumTopics[0].ObjectID.ToString());
                LnkDesc.Text = dataObject.Description.StripHTMLTags().CropString(400);
                LnkDesc.NavigateUrl = LnkTitle.NavigateUrl;
                LnkTitle.ID = null;
                LnkDesc.ID = null;
            }
            else
            {
                this.Parent.Parent.Visible = false;
            }
        }
    }
}