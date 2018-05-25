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
using System.Web.UI.WebControls;

namespace _4screen.CSB.Widget.Settings
{
    public partial class FunctionsFilter : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetFunctionsFilter");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
                FillControls();
        }

        private void FillControls()
        {

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            txtTGL.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagWords", string.Empty);
            CbxFilterType.Checked = (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "rcbOutput", "AutoFilter") == "AutoFilter");
            RntbMaxCount.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MaxCount", 20);
            int relevance = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Relevance", 3);
            DivTagwords.Visible = !CbxFilterType.Checked;
            DivMaxCount.Visible = !DivTagwords.Visible; 
            DivRelevance.Visible = !DivTagwords.Visible; 


            foreach (ListItem item in RblRelevance.Items)
            {
                item.Text = language.GetString("TextRelevance" + item.Value);    
                item.Selected = (item.Value == relevance.ToString()); 
            }

        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TagWords", txtTGL.Text.Replace(";", ","));
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "rcbOutput", CbxFilterType.Checked ? "AutoFilter": "ManualFilter");
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "MaxCount", (int)RntbMaxCount.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Relevance", RblRelevance.SelectedItem.Value); 

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        protected void CbxFilterType_CheckedChanged(object sender, EventArgs e)
        {
            DivTagwords.Visible = !CbxFilterType.Checked;
            DivMaxCount.Visible = !DivTagwords.Visible;
            DivRelevance.Visible = !DivTagwords.Visible; 
        }

    }
}