//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.4.0		11.12.2007 / AW
//  Updated:   
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Configuration;
using System.Net.Mail;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class DetailMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Detail);

            IMap map = (IMap)LoadControl("~/UserControls/GoogleMap.ascx");
            map.ObjectId = Request.QueryString["OID"].ToGuid();
            map.MapLayout = MapLayout.MapOnly;
            map.MapNaviation = MapNavigation.Normal;
            map.MapStyle = MapStyle.Aerial;
            map.Zoom = 10;
            map.Width = 600;
            map.Height = 400;
            Ph.Controls.Add((UserControl)map);
        }
    }
}