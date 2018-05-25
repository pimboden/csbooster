//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class ObjectSiblings : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            if (_Host.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                //// TODO implementieren

                ////string template = "SmallOutputObject.ascx";
                ////string repeater = "DataObjectLists.ascx";
                ////if (_Host.OutputTemplate != null)
                ////{
                ////    if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                ////        template = _Host.OutputTemplate.OutputTemplateControl;

                ////    if (!string.IsNullOrEmpty(_Host.OutputTemplate.RepeaterControl))
                ////        repeater = _Host.OutputTemplate.RepeaterControl;
                ////}

                string template = "ObjectDetailsSiblings.ascx";
                if (_Host.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                        template = _Host.OutputTemplate.OutputTemplateControl;
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
