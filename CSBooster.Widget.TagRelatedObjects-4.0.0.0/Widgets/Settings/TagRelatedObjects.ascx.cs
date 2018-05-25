// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.Widget.Settings
{
    public partial class TagRelatedObjects : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetTagRelatedObjects");
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Title.ToString()), QuickSort.Title.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Viewed.ToString()), QuickSort.Viewed.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.StartDate.ToString()), QuickSort.StartDate.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.ModifiedDate.ToString()), QuickSort.ModifiedDate.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Commented.ToString()), QuickSort.Commented.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedAverage.ToString()), QuickSort.RatedAverage.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedConsolidated.ToString()), QuickSort.RatedConsolidated.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Random.ToString()), QuickSort.Random.ToString("D")));
            
            this.RcbOrderDir.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSortDirection" + QuickSortDirection.Desc.ToString()), QuickSortDirection.Desc.ToString("D")));
            this.RcbOrderDir.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSortDirection" + QuickSortDirection.Asc.ToString()), QuickSortDirection.Asc.ToString("D")));

            this.CbxOrderBy.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.Title).ToString();
            this.RcbOrderDir.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortDirection", (int)QuickSortDirection.Desc).ToString();
            RntbMaxNumber.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "NumberRelations", 5);
            this.CbxPagerTop.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", false);
            this.CbxPagerBottom.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "NumberRelations", (int)RntbMaxNumber.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "SortBy", CbxOrderBy.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "SortDirection", RcbOrderDir.SelectedValue);
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
