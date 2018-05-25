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
    public partial class EmbedCode : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            if (_Host.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]) && !string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                DataObject dataObject = null;

                int objectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]);
                if (objectType == Helper.GetObjectTypeNumericID("Picture"))
                    dataObject = DataObject.Load<DataObjectPicture>(Request.QueryString["OID"].ToGuid());
                else if (objectType == Helper.GetObjectTypeNumericID("Video"))
                    dataObject = DataObject.Load<DataObjectVideo>(Request.QueryString["OID"].ToGuid());
                else
                    return false;

                Control control = LoadControl("~/UserControls/EmbedCode.ascx");
                ((ISettings)control).Settings = new Dictionary<string, object>();
                ((ISettings)control).Settings.Add("DataObject", dataObject);
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