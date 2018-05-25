// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.UserControls.Repeaters
{
    public partial class Comments : System.Web.UI.UserControl, ISettings, IBrowsable
    {
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetComments");
        private int currentPage = 1;
        private int numberItems;
        private int pageSize = 20;
        private DataObject commentedDataObject;
        private string outputTemplate;
        public Dictionary<string, object> Settings { get; set; }

        protected override void OnInit(EventArgs e)
        {
            pager.ItemNameSingular = Helper.GetObjectName("Comment", true);
            pager.ItemNamePlural = Helper.GetObjectName("Comment", false);
            pager.BrowsableControl = this;
            pager.PageSize = pageSize;

            bool ignoreCache = false;
            if (Settings != null)
            {

                if (!Settings.ContainsKey("HasContent"))
                    Settings.Add("HasContent", false);
                if (Settings.ContainsKey("CommentedObject"))
                    commentedDataObject = (DataObject)Settings["CommentedObject"];
                if (Settings.ContainsKey("OutputTemplateControl"))
                    outputTemplate = Settings["OutputTemplateControl"].ToString();
                if (Settings.ContainsKey("IgnoreCache"))
                    ignoreCache = Convert.ToBoolean(Settings["IgnoreCache"]);
            }
            Reload(ignoreCache);
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return this.numberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
            Reload(false);
        }

        public void Reload(bool ignoreCache)
        {
            DataObjectList<DataObjectComment> comments = DataObjects.Load<DataObjectComment>(new QuickParameters()
                                                                                                 {
                                                                                                     Udc = UserDataContext.GetUserDataContext(),
                                                                                                     PageNumber = currentPage,
                                                                                                     PageSize = pageSize,
                                                                                                     IgnoreCache = ignoreCache,
                                                                                                     RelationParams = new RelationParams
                                                                                                                          {
                                                                                                                              ExcludeSystemObjects = false,
                                                                                                                              ParentObjectID = commentedDataObject.ObjectID
                                                                                                                          }
                                                                                                 });
            this.repComments.DataSource = comments;
            this.repComments.DataBind();
            numberItems = comments.ItemTotal;
            this.pager.InitPager(currentPage, numberItems);
            if (numberItems > 0)
                Settings["HasContent"] = true;
        }

        protected void OnRepCommentsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectComment item = (DataObjectComment)e.Item.DataItem;
            PlaceHolder placeHolder = (PlaceHolder)e.Item.FindControl("Ph");
            Control control = LoadControl(string.Format("~/UserControls/Templates/{0}", outputTemplate));
            ((IDataObjectWorker)control).DataObject = item;
            placeHolder.Controls.Add(control);
        }
    }
}