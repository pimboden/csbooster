// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class SettingsBasic : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            ProfileBasisInfo profileBasisInfo = this.LoadControl("~/UserControls/Dashboard/ProfileBasisInfo.ascx") as ProfileBasisInfo;
            profileBasisInfo.ID = "PBI";
            PhProperties.Controls.Add(profileBasisInfo);

            ProfileCommunication profileCommunication = this.LoadControl("~/UserControls/Dashboard/ProfileCommunication.ascx") as ProfileCommunication;
            profileCommunication.ID = "PCO";
            PhProperties.Controls.Add(profileCommunication);

            ProfileTalente profileTalents = this.LoadControl("~/UserControls/Dashboard/ProfileTalente.ascx") as ProfileTalente;
            profileTalents.ID = "PTA";
            PhProperties.Controls.Add(profileTalents);
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            ProfileBasisInfo profileBasisInfo = PhProperties.FindControl("PBI") as ProfileBasisInfo;
            if (profileBasisInfo != null) profileBasisInfo.Save();
            ProfileCommunication profileCommunication = PhProperties.FindControl("PCO") as ProfileCommunication;
            if (profileCommunication != null) profileCommunication.Save();
            ProfileTalente profileTalente = PhProperties.FindControl("PTA") as ProfileTalente;
            if (profileTalente != null) profileTalente.Save();
        }
    }
}