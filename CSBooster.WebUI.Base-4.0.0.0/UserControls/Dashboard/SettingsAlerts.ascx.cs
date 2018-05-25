// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class SettingsAlerts : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CustomizationSection.CachedInstance.Modules["Alerts"].Enabled)
            {
                ProfileNotification profileNotification = this.LoadControl("~/UserControls/Dashboard/ProfileNotification.ascx") as ProfileNotification;
                profileNotification.ID = "PNO";
                PhMessaging.Controls.Add(profileNotification);
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            ProfileNotification profileNotification = PhMessaging.FindControl("PNO") as ProfileNotification;
            if (profileNotification != null) profileNotification.Save();
        }
    }
}