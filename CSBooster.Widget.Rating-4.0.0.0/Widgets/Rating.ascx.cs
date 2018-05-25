// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class Rating : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            string template = "ObjectVoting.ascx";
            string repeater = string.Empty;
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            DataObject dataObject = null;
            if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                PageType pageType = this.WidgetHost.ParentPageType;
                if (pageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    dataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                }
                else if (pageType == PageType.Overview && (!string.IsNullOrEmpty(Request.QueryString["XUI"]) || !string.IsNullOrEmpty(Request.QueryString["XCN"])))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                    {
                        dataObject = DataObject.Load<DataObject>(Request.QueryString["XUI"].ToGuid());
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                    {
                        dataObject = DataObject.Load<DataObject>(Request.QueryString["XCN"].ToGuid());
                    }
                }
                else
                {
                    dataObject = DataObject.Load<DataObject>(WidgetHost.ParentCommunityID);
                }
            }
            else
            {
                dataObject = DataObject.Load<DataObject>(WidgetHost.ParentCommunityID);
                if (dataObject.ObjectType == 19)
                    dataObject = DataObject.Load<DataObjectUser>(dataObject.UserID);
            }

            if (dataObject != null && dataObject.State != ObjectState.Added)
            {
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.LoadXml(settingsXml);

                bool showInfo = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowInfo", true);
                xmlDom = null;

                Control control = LoadControl("~/UserControls/Templates/" + template);
                IObjectVoting objectRating = (IObjectVoting)control;
                objectRating.DataObject = dataObject;
                objectRating.ShowInfo = showInfo;
                PnlCnt.Controls.Add(control);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}