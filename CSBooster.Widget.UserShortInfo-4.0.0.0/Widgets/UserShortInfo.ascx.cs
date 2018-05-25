// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class UserShortInfo : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Guid? userId = null;

            try
            {
                if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    DataObject profileCommunity = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                    userId = profileCommunity.UserID.Value;
                }
                else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
                {
                    if (WidgetHost.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                    {
                        DataObject detail = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                        userId = detail.UserID;
                    }
                    else if (WidgetHost.ParentPageType == PageType.Overview && !string.IsNullOrEmpty(Request.QueryString["XUI"]))
                    {
                        userId = Request.QueryString["XUI"].ToGuid();
                    }
                }
            }
            catch
            {
                return false;
            }

            if (userId.HasValue)
            {
                string template = "UserShortInfo.ascx";
                if (WidgetHost.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                        template = WidgetHost.OutputTemplate.OutputTemplateControl;
                }

                Control control = LoadControl("~/UserControls/Templates/" + template);
                control.ID = "USI";
                DataObjectUser user = DataObject.Load<DataObjectUser>(userId);
                ((IDataObjectWorker)control).DataObject = user;
                Ph.Controls.Add(control);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
