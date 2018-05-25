//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.09.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class ObjectLinking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.MyContent); 

                DataObject dataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                this.OBJLINK.CloseWindowJSFunction = @"GetRadWindow().Close();";
                this.OBJLINK.DataObject = dataObject;
            }
        }
    }
}