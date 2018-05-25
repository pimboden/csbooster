//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.Pages.Admin
{
    public partial class MyUserActivities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control control = LoadControl("~/UserControls/Repeaters/UserActivities.ascx");
            IUserActivity objectActivities = (IUserActivity)control;
            objectActivities.OutputTemplate = "UserActivities.ascx";
            objectActivities.UserActivityParameters = new UserActivityParameters { ObjectID = UserProfile.Current.ProfileCommunityID, ObjectType = Helper.GetObjectTypeNumericID("ProfileCommunity"), IgnoreCache = true };
            Cnt.Controls.Add(control);
        }
    }
}
