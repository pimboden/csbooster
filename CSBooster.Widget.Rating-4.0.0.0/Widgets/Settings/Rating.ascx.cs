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
    public partial class Rating : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetRating");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxShowInfo.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowInfo", true);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowInfo", CbxShowInfo.Checked);

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}