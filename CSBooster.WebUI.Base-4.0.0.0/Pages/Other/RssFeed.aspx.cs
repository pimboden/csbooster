//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		30.12.2008 / AW
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public partial class RssFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Overview); 

            QuickParameters quickParameters = new QuickParameters();
            quickParameters.FromNameValueCollection(Request.QueryString);

            Response.Clear();
            Response.ContentType = "application/rss+xml";
            Response.Write(_4screen.CSB.DataAccess.Business.RssEngine.GetFeed(quickParameters, "rss"));
            Response.End();
        }
    }
}