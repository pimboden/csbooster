// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class SettingsInterests : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CustomizationSection.CachedInstance.Profile["Interests"].Enabled)
            {
                ProfileInterestTopic profileInterestTopics = this.LoadControl("~/UserControls/Dashboard/ProfileInterestTopic.ascx") as ProfileInterestTopic;
                profileInterestTopics.ID = "PTOP";
                PhInterests.Controls.Add(profileInterestTopics);

                ProfileIntresse profileInterests = this.LoadControl("~/UserControls/Dashboard/ProfileIntresse.ascx") as ProfileIntresse;
                profileInterests.ID = "PIN";
                PhInterests.Controls.Add(profileInterests);
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            ProfileInterestTopic profileIntresse1 = PhInterests.FindControl("PTOP") as ProfileInterestTopic;
            if (profileIntresse1 != null) profileIntresse1.Save();
            ProfileIntresse profileIntresse2 = PhInterests.FindControl("PIN") as ProfileIntresse;
            if (profileIntresse2 != null) profileIntresse2.Save();
        }
    }
}