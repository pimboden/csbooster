// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Xml;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.Settings
{
    public partial class Sharing : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetSharing");  
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack) 
                FillControls();
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowExtSharing", CbxShowExtSharing.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowILike", CbxILike.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowSendUrl", CbxShowSendUrl.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowEmbedAndCopy", CbxShowEmbedAndCopy.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "EmbedVideoWidth", (int)RntbEmbedWidth.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "EmbedVideoHeight", (int)RntbEmbedHeight.Value);

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxShowExtSharing.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowExtSharing", true);
            this.CbxILike.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowILike", false);
            this.CbxShowSendUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowSendUrl", true);
            this.CbxShowEmbedAndCopy.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowEmbedAndCopy", true);
            this.RntbEmbedWidth.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "EmbedVideoWidth", 400);
            this.RntbEmbedHeight.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "EmbedVideoHeight", 300);
        }
    }
}
