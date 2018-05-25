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
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputForumTopic : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LnkTitle.Text = DataObject.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString());
            LnkTitle.ID = null;

            List<DataObjectForumTopicItem> forumTopicItem = DataObjects.Load<DataObjectForumTopicItem>(new QuickParameters()
                                                                               {
                                                                                   RelationParams = new RelationParams
                                                                                                                   {
                                                                                                                       ParentObjectID = DataObject.ObjectID,
                                                                                                                       ParentObjectType = Helper.GetObjectTypeNumericID("ForumTopic")
                                                                                                                   },
                                                                                   Udc = UserDataContext.GetUserDataContext(),
                                                                                   PageNumber = 1,
                                                                                   PageSize = 1,
                                                                                   IgnoreCache = false,
                                                                                   SortBy = QuickSort.InsertedDate,
                                                                                   Direction = QuickSortDirection.Desc
                                                                               });
            if (forumTopicItem.Count == 1)
            {
                pnlInfo2.Visible = true;
                LitDesc.Text = string.Format("{0} / {1} {2}", forumTopicItem[0].Nickname, forumTopicItem[0].Inserted.ToShortDateString(), forumTopicItem[0].Inserted.ToShortTimeString());
                LitDesc2.Text = forumTopicItem[0].Description.StripHTMLTags().CropString(80);
            }
            else
            {
                pnlInfo.Visible = true;
            }
        }
    }
}