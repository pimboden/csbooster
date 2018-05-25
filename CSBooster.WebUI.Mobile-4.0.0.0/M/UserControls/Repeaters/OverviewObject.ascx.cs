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
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls.Repeaters
{
    public partial class OverviewObject : System.Web.UI.UserControl
    {
        private int pageSize = 20;
        private int currentPage = 1;
        private QuickParameters quickParameters;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.PageSize = pageSize;

            quickParameters = new QuickParameters();
            quickParameters.SortBy = QuickSort.StartDate;
            quickParameters.Direction = QuickSortDirection.Desc;
            quickParameters.FromNameValueCollection(Request.QueryString);
            quickParameters.PageSize = pageSize;
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.ShowState = ObjectShowState.Published;
            currentPage = quickParameters.PageNumber;
            LitTitle.Text = Helper.GetObjectName(quickParameters.ObjectType, false);

            Reload();
        }

        protected void OnOverviewItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");
            string smallOutputControl = !string.IsNullOrEmpty(Helper.GetObjectType(quickParameters.ObjectType).MobileSmallOutputCtrl) ? Helper.GetObjectType(quickParameters.ObjectType).MobileSmallOutputCtrl : "/M/UserControls/Templates/SmallOutputObject.ascx";
            Control control = LoadControl(smallOutputControl);
            ((IDataObjectWorker)control).DataObject = dataObject;
            ph.Controls.Add(control);
        }

        // Interface IReloadable
        public void Reload()
        {
            DataObjectList<DataObject> itemList = DataObjects.Load<DataObject>(quickParameters);
            repObj.DataSource = itemList;
            repObj.DataBind();
            pager.InitPager(currentPage, itemList.ItemTotal);
        }
    }
}