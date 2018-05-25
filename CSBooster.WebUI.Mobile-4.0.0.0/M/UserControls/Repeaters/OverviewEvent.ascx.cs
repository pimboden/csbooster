// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls.Repeaters
{
    public partial class OverviewEvent : System.Web.UI.UserControl
    {
        private int pageSize = 20;
        private int currentPage = 1;
        private QuickParameters quickParameters;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        protected void Page_Load(object sender, EventArgs e)
        {
            string queryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "DC", "PN", "FE" }, true);
            lnkToday1.NavigateUrl = string.Format("{0}?{1}&DC={2}&FE=1", Request.GetRawPath(), queryString, DateConstraint.Today);
            lnkToday1.ID = null;
            lnkToday2.NavigateUrl = string.Format("{0}?{1}&DC={2}&FE=2", Request.GetRawPath(), queryString, DateConstraint.Today);
            lnkToday2.ID = null;
            lnkFromTomorrow.NavigateUrl = string.Format("{0}?{1}&DC={2}", Request.GetRawPath(), queryString, DateConstraint.FromTomorrow);
            lnkFromTomorrow.ID = null;
            lnkUntilYesterday.NavigateUrl = string.Format("{0}?{1}&DC={2}", Request.GetRawPath(), queryString, DateConstraint.UntilYesterday);
            lnkUntilYesterday.ID = null;

            pager.PageSize = pageSize;

            quickParameters = new QuickParameters();
            quickParameters.SortBy = QuickSort.StartDate;
            quickParameters.Direction = QuickSortDirection.Asc;
            quickParameters.FromNameValueCollection(Request.QueryString);
            quickParameters.PageSize = pageSize;
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.ShowState = ObjectShowState.Published;

            DateConstraint dateConstraint = (DateConstraint)Enum.Parse(typeof(DateConstraint), Request.QueryString["DC"] ?? "Today");
            switch (dateConstraint)
            {
                case DateConstraint.UntilYesterday:
                    quickParameters.FromStartDate = SqlDateTime.MinValue.Value;
                    quickParameters.ToStartDate = DateTime.Today.GetEndOfDay() - new TimeSpan(1, 0, 0, 0);
                    quickParameters.FromEndDate = SqlDateTime.MinValue.Value;
                    quickParameters.ToEndDate = DateTime.Today.GetEndOfDay() - new TimeSpan(1, 0, 0, 0);
                    quickParameters.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    quickParameters.Direction = QuickSortDirection.Desc;
                    LitTitle.Text = language.GetString("TitleEvent" + DateConstraint.UntilYesterday);
                    lnkToday1.Visible = true;
                    lnkToday2.Visible = true;
                    lnkFromTomorrow.Visible = true;
                    break;
                case DateConstraint.Today:
                    quickParameters.FromStartDate = SqlDateTime.MinValue.Value;
                    quickParameters.ToStartDate = DateTime.Today.GetEndOfDay();
                    quickParameters.FromEndDate = DateTime.Today.GetStartOfDay();
                    quickParameters.ToEndDate = SqlDateTime.MaxValue.Value;
                    quickParameters.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    if (quickParameters.Featured == 1 || !quickParameters.Featured.HasValue)
                    {
                        quickParameters.Featured = 1;
                        LitTitle.Text = language.GetString("TitleEvent" + DateConstraint.Today);
                        lnkToday2.Visible = true;
                    }
                    else
                    {
                        LitTitle.Text = language.GetString("TitleEventTodayMultiDay");
                        lnkToday1.Visible = true;
                    }
                    lnkUntilYesterday.Visible = true;
                    lnkFromTomorrow.Visible = true;
                    break;
                case DateConstraint.FromTomorrow:
                    quickParameters.FromStartDate = DateTime.Today.GetStartOfDay() + new TimeSpan(1, 0, 0, 0);
                    quickParameters.ToStartDate = SqlDateTime.MaxValue.Value;
                    quickParameters.FromEndDate = DateTime.Today.GetStartOfDay() + new TimeSpan(1, 0, 0, 0);
                    quickParameters.ToEndDate = SqlDateTime.MaxValue.Value;
                    quickParameters.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    LitTitle.Text = language.GetString("TitleEvent" + DateConstraint.FromTomorrow);
                    lnkToday1.Visible = true;
                    lnkToday2.Visible = true;
                    lnkUntilYesterday.Visible = true;
                    break;
            }

            currentPage = quickParameters.PageNumber;

            if (Request.IsAuthenticated)
            {
                lnkCreate.Visible = true;
                lnkCreate.NavigateUrl = "/M/Admin/EditEvent.aspx";
                lnkCreate.ID = null;
            }

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
            DataObjectList<DataObjectEvent> itemList = DataObjects.Load<DataObjectEvent>(quickParameters);
            repObj.DataSource = itemList;
            repObj.DataBind();
            pager.InitPager(currentPage, itemList.ItemTotal);
        }
    }
}