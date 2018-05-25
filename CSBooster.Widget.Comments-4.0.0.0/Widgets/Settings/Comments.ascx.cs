using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.Settings
{
    public partial class Comments : UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetComments");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FillControls();
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            CbxAllowAnonymous.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAllowAnonymous", false);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxAllowAnonymous", CbxAllowAnonymous.Checked);
                return DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }
    }
}