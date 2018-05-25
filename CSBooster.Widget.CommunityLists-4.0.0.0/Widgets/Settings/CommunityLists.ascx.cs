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
    public partial class CommunityLists : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetCommunityLists");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            // fill the controls
            RntbUsers.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "PageSize", 5);
            this.CbxPagerTop.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", false);
            this.CbxPagerBottom.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "PageSize", (int)RntbUsers.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerTop", CbxPagerTop.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerBottom", CbxPagerBottom.Checked);

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}