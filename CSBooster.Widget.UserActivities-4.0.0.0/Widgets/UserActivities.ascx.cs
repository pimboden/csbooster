// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class UserActivities : WidgetBase
    {
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
                    if (this.WidgetHost.ParentObjectType == 19) // Profile
                    {
                        DataObject profile = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
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
                if (WidgetHost.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                        template = WidgetHost.OutputTemplate.OutputTemplateControl;

                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                        repeater = WidgetHost.OutputTemplate.RepeaterControl;
                }

                Control control = LoadControl("~/UserControls/Repeaters/" + repeater);
                IUserActivity objectActivities = (IUserActivity)control;
                objectActivities.OutputTemplate = template;
                objectActivities.UserActivityParameters = new UserActivityParameters { ObjectID = this.WidgetHost.ParentCommunityID, ObjectType = this.WidgetHost.ParentObjectType, IgnoreCache = ignoreCache };

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