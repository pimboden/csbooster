// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class ObjectSiblings : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            if (WidgetHost.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                //// TODO implementieren

                ////string template = "SmallOutputObject.ascx";
                ////string repeater = "DataObjectLists.ascx";
                ////if (WidgetHost.OutputTemplate != null)
                ////{
                ////    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                ////        template = WidgetHost.OutputTemplate.OutputTemplateControl;

                ////    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                ////        repeater = WidgetHost.OutputTemplate.RepeaterControl;
                ////}

                string template = "ObjectDetailsSiblings.ascx";
                if (WidgetHost.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                        template = WidgetHost.OutputTemplate.OutputTemplateControl;
                }

                Control objectSiblings = LoadControl("~/UserControls/Templates/" + template);
                ((IDataObjectWorker)objectSiblings).DataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                this.Ph.Controls.Add(objectSiblings);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
