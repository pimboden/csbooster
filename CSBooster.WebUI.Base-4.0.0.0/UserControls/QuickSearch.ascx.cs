// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class QuickSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtSearch.Attributes.Add("onkeypress", "RedirectOnEnterKey(event, '" + Constants.Links["LINK_TO_SITE_SEARCH"].Url + "&SG=', '" + this.TxtSearch.ClientID + "')");
            this.LnkSearch.Attributes.Add("onclick", "RedirectOnClick('" + Constants.Links["LINK_TO_SITE_SEARCH"].Url + "&SG=', '" + this.TxtSearch.ClientID + "')");   
        }
    }
}