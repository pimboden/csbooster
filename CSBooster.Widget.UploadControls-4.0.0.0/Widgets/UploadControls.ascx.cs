// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class UploadControls : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserActivities");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc.IsAuthenticated)
            {
                try
                {
                    string template = "UploadControls.ascx";
                    if (WidgetHost.OutputTemplate != null)
                    {
                        if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                            template = WidgetHost.OutputTemplate.OutputTemplateControl;
                    }

                    Control control = LoadControl("~/UserControls/Templates/" + template);

                    ISettings setting = control as ISettings;
                    if (setting != null)
                    {
                        setting.Settings = new System.Collections.Generic.Dictionary<string, object>();
                        setting.Settings.Add("ParentPageType", WidgetHost.ParentPageType);
                        setting.Settings.Add("ParentObjectType", WidgetHost.ParentObjectType);
                        setting.Settings.Add("ParentCommunityID", WidgetHost.ParentCommunityID);
                    }

                    phC.Controls.Add(control);
                    hasContent = true;
                }
                catch
                {
                }
            }

            return hasContent;
        }
    }
}