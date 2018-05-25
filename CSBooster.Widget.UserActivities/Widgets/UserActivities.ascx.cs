using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using System;

namespace _4screen.CSB.Widget
{
    public partial class UserActivities : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserActivities");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            Reload(false);
            return hasContent;    //have to be true
        }

        private void Reload(bool ignoreCache)
        {
            PnlCnt.Controls.Clear();
            PnlInput.Visible = false;
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc.IsAuthenticated)
            {
                if (_4screen.CSB.DataAccess.Business.DataAccessConfiguration.UserActivityIsActivityActiv(UserActivityWhat.DoNowThis))
                {
                    if (this._Host.ParentObjectType == 19) // Profile
                    {
                        DataObject profile = DataObject.Load<DataObject>(this._Host.ParentCommunityID);
                        if (udc.UserID == profile.UserID.Value)
                        {
                            hasContent = true;
                            PnlInput.Visible = true;
                        }
                    }
                    else
                    {
                        hasContent = true;
                        PnlInput.Visible = true;
                    }
                }
            }

            try
            {
                string template = "UserActivities.ascx";
                string repeater = "UserActivities.ascx";
                if (_Host.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                        template = _Host.OutputTemplate.OutputTemplateControl;

                    if (!string.IsNullOrEmpty(_Host.OutputTemplate.RepeaterControl))
                        repeater = _Host.OutputTemplate.RepeaterControl;
                }

                Control control = LoadControl("~/UserControls/Repeaters/UserActivities.ascx");
                IUserActivity objectActivities = (IUserActivity)control;
                objectActivities.OutputTemplate = template;
                objectActivities.UserActivityParameters = new UserActivityParameters { ObjectID = this._Host.ParentCommunityID, ObjectType = this._Host.ParentObjectType, IgnoreCache = ignoreCache };

                PnlCnt.Controls.Add(control);
                if (objectActivities.HasContent && !PnlInput.Visible)
                    hasContent = true;
            }
            catch
            {
            }

            PnlInput.ID = null;
        }

        protected void LbtnInput_Click1(object sender, EventArgs e)
        {
            if (TxtInput.Text.Trim().Length > 0)
            {
                _4screen.CSB.DataAccess.Business.UserActivities.InsertDoNowThis(UserDataContext.GetUserDataContext(), TxtInput.Text.Trim());
                TxtInput.Text = string.Empty;
                Reload(true);
            }
            hasContent = true;
        }
    }
}