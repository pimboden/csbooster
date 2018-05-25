// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI
{
    public partial class Admin_Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);
        }
    }
}
