// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class Sharing : WidgetBase
    {
        private DataObject dataObject;

        public override bool ShowObject(string settingsXml)
        {
            bool showWidget = false;
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            if (this.WidgetHost.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                dataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                if (dataObject.State != ObjectState.Added)
                {
                    Control control = LoadControl("~/UserControls/Templates/Sharing.ascx");
                    ((ISettings)control).Settings = new Dictionary<string, object>();
                    ((ISettings)control).Settings.Add("DataObject", dataObject);
                    ((ISettings)control).Settings.Add("ShowExtSharing", XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowExtSharing", true));
                    ((ISettings)control).Settings.Add("ShowILike", XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowILike", false));
                    ((ISettings)control).Settings.Add("ShowSendUrl", XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowSendUrl", true));
                    ((ISettings)control).Settings.Add("ShowEmbedAndCopy", XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowEmbedAndCopy", true));
                    ((ISettings)control).Settings.Add("EmbedVideoWidth", XmlHelper.GetElementValue(xmlDom.DocumentElement, "EmbedVideoWidth", 400));
                    ((ISettings)control).Settings.Add("EmbedVideoHeight", XmlHelper.GetElementValue(xmlDom.DocumentElement, "EmbedVideoHeight", 300));
                    ((ISettings)control).Settings.Add("Width", WidgetHost.ColumnWidth - WidgetHost.ContentPadding);
                    Cnt.Controls.Add(control);

                    showWidget = true;
                }
            }
            return showWidget;
        }
    }
}
