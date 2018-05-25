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
    public partial class CommunityShortInfo : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Guid? communityId = null;

            try
            {
                if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
                {
                    communityId = this.WidgetHost.ParentCommunityID;
                }
                else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
                {
                    if (WidgetHost.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                    {
                        DataObject detail = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                        communityId = detail.CommunityID;
                    }
                    else if (WidgetHost.ParentPageType == PageType.Overview && !string.IsNullOrEmpty(Request.QueryString["XCN"]))
                    {
                        communityId = Request.QueryString["XCN"].ToGuid();
                    }
                }
            }
            catch
            {
                return false;
            }

            string template = string.Empty;
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;
            }


            if (communityId.HasValue && !string.IsNullOrEmpty(template))
            {
                DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(communityId);
                if (community.State != ObjectState.Added)
                {
                    Control control = LoadControl("~/UserControls/Templates/" + template);
                    control.ID = "USI";

                    IDataObjectWorker dataObjectWorker = control as IDataObjectWorker;
                    if (dataObjectWorker != null)
                        dataObjectWorker.DataObject = community;
                    else
                        return false;
                    Ph.Controls.Add(control);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
