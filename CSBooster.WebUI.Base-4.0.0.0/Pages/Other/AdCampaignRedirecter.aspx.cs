//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public partial class AdCampaignRedirecter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.UserProfileData); 

            try
            {
                Guid? objectId = null;
                if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                    objectId = new Guid(Request.QueryString["OID"]);
                string url = _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignUrl(new Guid(Request.QueryString["CID"]), objectId, UserDataContext.GetUserDataContext(), Request.QueryString["Word"], Request.QueryString["Type"]);
                Response.Redirect(url);
            }
            catch
            {
                Label1.Text = GuiLanguage.GetGuiLanguage("Pages.Other.WebUI.Base").GetString("MessageAdCampaignNotFound");
                Label1.Visible = true;
            }
        }
    }
}