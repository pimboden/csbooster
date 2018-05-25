//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class Participate : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Memberships); 
        }

        protected void OnYesClick(object sender, EventArgs e)
        {
            Guid? communtyId = Request.QueryString["CTYID"].ToNullableGuid();
            if (communtyId.HasValue)
            {
                try
                {
                    UserDataContext udc = UserDataContext.GetUserDataContext();
                    
                    HirelCommunityUserCur.Insert(communtyId.Value, UserProfile.Current.UserId, false, 0, DateTime.Now, Guid.Empty);
                    SPs.HispDataObjectAddMemberCount(communtyId, 1).Execute();
                    UserActivities.InsertMembership(udc, communtyId.Value);
                    
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("COMMUNITY_PARTICIPATE", udc, communtyId.ToString());
                    _4screen.CSB.Notification.Business.Event.ReportNewMember(UserProfile.Current.UserId, UserProfile.Current.UserId, communtyId);
                    
                    try
                    {
                        Community community = new Community(communtyId.Value);
                        if (community.ProfileOrCommunity.ParentObjectID.HasValue) // Join parent community too
                        {
                            HirelCommunityUserCur.Insert(community.ProfileOrCommunity.ParentObjectID.Value, UserProfile.Current.UserId, false, 0, DateTime.Now, Guid.Empty);
                            SPs.HispDataObjectAddMemberCount(community.ProfileOrCommunity.ParentObjectID.Value, 1).Execute();
                            UserActivities.InsertMembership(udc, community.ProfileOrCommunity.ParentObjectID.Value);
                        }
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }
            }
            LitScript.Text = "<script type=\"text/javascript\">$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );</script>";
        }
    }
}