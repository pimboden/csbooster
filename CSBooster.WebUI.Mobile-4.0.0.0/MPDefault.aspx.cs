// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI
{
    public partial class MPDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Homepage);

            PhHome.Controls.Add(LoadControl("/M/UserControls/Home.ascx"));
        }
    }
}