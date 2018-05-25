using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

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
                    if (_Host.OutputTemplate != null)
                    {
                        if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                            template = _Host.OutputTemplate.OutputTemplateControl;
                    }

                    Control control = LoadControl("~/UserControls/Templates/" + template);

                    ISettings setting = control as ISettings;
                    if (setting != null)
                    {
                        setting.Settings = new System.Collections.Generic.Dictionary<string, object>();
                        setting.Settings.Add("ParentPageType", _Host.ParentPageType);
                        setting.Settings.Add("ParentObjectType", _Host.ParentObjectType);
                        setting.Settings.Add("ParentCommunityID", _Host.ParentCommunityID);
                    }

                    ////IUserActivity objectActivities = (IUserActivity)control;
                    ////objectActivities.OutputTemplate = template;
                    ////objectActivities.UserActivityParameters = new UserActivityParameters { ObjectID = this._Host.ParentCommunityID, ObjectType = this._Host.ParentObjectType, IgnoreCache = ignoreCache };

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