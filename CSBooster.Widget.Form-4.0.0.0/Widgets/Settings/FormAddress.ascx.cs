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
    public partial class FormAddress : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetForm");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FillControls();
        }

        private void FillControls()
        {
            if (this.DdlAdrSave.Items.Count == 0)
            {
                this.DdlAdrSave.Items.Add(new System.Web.UI.WebControls.ListItem(language.GetString("ItemAddressSave0"), "0"));
                this.DdlAdrSave.Items.Add(new System.Web.UI.WebControls.ListItem(language.GetString("ItemAddressSave1"), "1"));
                this.DdlAdrSave.Items.Add(new System.Web.UI.WebControls.ListItem(language.GetString("ItemAddressSave2"), "2"));
            }

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            CbxAdressShow.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAdressShow", false);
            CbxAdressCommentShow.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAdressCommentShow", false);
            Common.Helper.Ddl_SelectItem(DdlAdrSave, XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DdlAdressSave", 1));
            CbxMustAuth.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxMustAuth", false);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxAdressShow", CbxAdressShow.Checked);
                if (DdlAdrSave.SelectedItem != null)
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "DdlAdressSave", DdlAdrSave.SelectedItem.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxAdressCommentShow", CbxAdressCommentShow.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxMustAuth", CbxMustAuth.Checked);

                return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}