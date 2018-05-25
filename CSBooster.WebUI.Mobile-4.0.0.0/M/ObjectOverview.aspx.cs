// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.M
{
    public partial class ObjectOverview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteObjectType objectType = Helper.GetObjectType(Request.QueryString["OT"]);

            Extensions.Business.TrackingManager.TrackEventPage(null, objectType.NumericId, IsPostBack, LogSitePageType.Overview);

            string overviewControl = !string.IsNullOrEmpty(objectType.MobileOverviewCtrl) ? objectType.MobileOverviewCtrl : "/M/UserControls/Repeaters/OverviewObject.ascx";
            Control control = LoadControl(overviewControl);
            Ph.Controls.Add(control);
        }
    }
}