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
    public partial class ObjectListsProp : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetObjectLists");
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        private UserDataContext udc = UserDataContext.GetUserDataContext();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
                FillControls();
        }

        private void FillControls()
        {
            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Title.ToString()), QuickSort.Title.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Viewed.ToString()), QuickSort.Viewed.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.StartDate.ToString()), QuickSort.StartDate.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Commented.ToString()), QuickSort.Commented.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedAverage.ToString()), QuickSort.RatedAverage.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedConsolidated.ToString()), QuickSort.RatedConsolidated.ToString("D")));

            RntbMaxNumber.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MaxNumber", 5);
            this.CbxOrderBy.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.StartDate).ToString();
            this.CbxPagerTop.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", false);
            this.CbxPagerBottom.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);

            this.PnlRenderHtml.Visible = (udc.IsAuthenticated && udc.IsAdmin);
            this.CbxRenderHtml.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RenderHtml", false);
            this.CbxAnonymous.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Anonymous", false); 
        }


        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "MaxNumber", (int)RntbMaxNumber.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "SortBy", CbxOrderBy.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerTop", CbxPagerTop.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerBottom", CbxPagerBottom.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Anonymous", CbxAnonymous.Checked);

                if (udc.IsAuthenticated && udc.IsAdmin)
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "RenderHtml", CbxRenderHtml.Checked);

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

    }
}
