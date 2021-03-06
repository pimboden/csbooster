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
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class PasswordChange : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.UserProfileData);

            ChangePassword.ChangePasswordFailureText = language.GetString("MessageChangePasswordFailure");

            if (!Request.IsAuthenticated)
            {
                pnlLoggedIn.Visible = false;
            }
        }
    }
}