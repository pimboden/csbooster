// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using Telerik.Web.UI;

namespace _4screen.CSB.Widget.Settings
{
    public partial class RSS : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetRSS");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (RadComboBoxItem item in this.CbxFR.Items)
            {
                item.Text = language.GetString("TextTimeSec" + item.Value);   
            }

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            txtURL.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "txtURL", string.Empty);
            RntbFC.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ddlFC", 10);
            cbxDesc.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "cbxDesc", true);
            CbxFR.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ddlFR", "0");

        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "txtURL", txtURL.Text);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ddlFC", (int)RntbFC.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "cbxDesc", cbxDesc.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ddlFR", CbxFR.SelectedValue);


                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}