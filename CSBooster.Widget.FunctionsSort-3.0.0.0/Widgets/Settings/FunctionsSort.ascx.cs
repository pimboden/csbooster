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
    public partial class FunctionsSort : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetFunctionsSort");
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

            CbxNoSort.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxNoSort", false);
            CbxByTitle.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByTitle", true);
            CbxByActivity.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByActivity", false);
            CbxByDate.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByDate", true);
            CbxByVisits.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByVisits", true);
            CbxByRatings.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatings", true);
            CbxByRatingConsolidated.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatingConsolidated", false);
            CbxByComments.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByComments", true);
            CbxByLinks.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByLinks", false);
            CbxByMember.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByMember", false);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxNoSort", CbxNoSort.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByTitle", CbxByTitle.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByActivity", CbxByActivity.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByDate", CbxByDate.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByVisits", CbxByVisits.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByRatings", CbxByRatings.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByRatingConsolidated", CbxByRatingConsolidated.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByComments", CbxByComments.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByLinks", CbxByLinks.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "CbxByMember", CbxByMember.Checked);

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}