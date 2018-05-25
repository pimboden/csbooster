// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class Wizard : WidgetBase
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public override bool ShowObject(string settingsXml)
        {
            var wizard = WizardSection.CachedInstance.Wizards[Request.QueryString["WizardID"]];
            string localizationBaseFileName = "UserControls.Wizards.WebUI.Base";
            if (!string.IsNullOrEmpty(wizard.LocalizationBaseFileName))
                localizationBaseFileName = wizard.LocalizationBaseFileName;
            GuiLanguage resMan = GuiLanguage.GetGuiLanguage(localizationBaseFileName);
            string wizardTitle = resMan.GetString(wizard.ResourceKey);

            ((IWidgetPageMaster)this.Page.Master).BreadCrumb.RenderAdminPageBreadCrumbs(wizardTitle);

            return true;
        }
    }
}