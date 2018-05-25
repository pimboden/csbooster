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
    public partial class HTMLContent : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetHTMLContent");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            RadEditor1.Content = XmlHelper.GetElementValueCDATA(xmlDocument.DocumentElement, "HTMLContent", string.Empty);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerTextCDATA(xmlDocument.DocumentElement, "HTMLContent", RadEditor1.Content);

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}