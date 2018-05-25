// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class PermaLink : WidgetBase
    {
        private bool hasContent = false;
        public override bool ShowObject(string settingsXml)
        {
            if (_Host.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                DataObject dataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                Control control = LoadControl("~/UserControls/Templates/Permalink.ascx");
                IDataObjectWorker dataObjectWorker = control as IDataObjectWorker;
                if (dataObjectWorker != null)
                {
                    dataObjectWorker.DataObject = dataObject;
                    Ph.Controls.Add(control);
                    hasContent = true;
                }

                if (dataObject.ObjectType == Helper.GetObjectTypeNumericID("Picture"))
                {
                    dataObject = DataObject.Load<DataObjectPicture>(Request.QueryString["OID"].ToGuid());
                }
                else if (dataObject.ObjectType == Helper.GetObjectTypeNumericID("Video"))
                {
                    dataObject = DataObject.Load<DataObjectVideo>(Request.QueryString["OID"].ToGuid());
                }
                else
                {
                    return hasContent;
                }

                control = LoadControl("~/UserControls/Templates/EmbedCode.ascx");
                dataObjectWorker = control as IDataObjectWorker;
                if (dataObjectWorker != null)
                {
                    dataObjectWorker.DataObject = dataObject;
                    Ph.Controls.Add(control);
                    hasContent = true;
                }
            }
            return hasContent;
        }
    }
}