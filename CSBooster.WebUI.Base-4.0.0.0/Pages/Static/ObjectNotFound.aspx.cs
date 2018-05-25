//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.5.0		19.12.2007 / AW
//  Updated:   #1.1.0.0    28.12.2007 / AW
//                         - New object types added
//******************************************************************************

using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Static
{
    public partial class ObjectNotFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Static); 

            Response.Status = "404 Not Found";

            if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                this.MSG.Text = string.Format("<strong>" + GuiLanguage.GetGuiLanguage("Pages.Static.WebUI.Base").GetString("MessageThisObjectNotFound") + "</strong>", Helper.GetObjectName(Request.QueryString["OT"], true));
            }
            else
            {
                this.MSG.Text = "<strong>" + GuiLanguage.GetGuiLanguage("Pages.Static.WebUI.Base").GetString("MessageObjectNotFound") + "</strong>";
            }
        }
    }
}