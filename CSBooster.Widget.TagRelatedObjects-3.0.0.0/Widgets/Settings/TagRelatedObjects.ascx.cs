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
    public partial class TagRelatedObjects : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetTagRelatedObjects");
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Title.ToString()), QuickSort.Title.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Viewed.ToString()), QuickSort.Viewed.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.StartDate.ToString()), QuickSort.StartDate.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.Commented.ToString()), QuickSort.Commented.ToString("D")));
            this.CbxOrderBy.Items.Add(new RadComboBoxItem(languageDataAccess.GetString("EnumQuickSort" + QuickSort.RatedAverage.ToString()), QuickSort.RatedAverage.ToString("D")));

            this.CbxOrderBy.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.Title).ToString();
            RntbMaxNumber.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "NumberRelations", 5);
            this.CbxPagerTop.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", false);
            this.CbxPagerBottom.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "NumberRelations", (int)RntbMaxNumber.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "SortBy", CbxOrderBy.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerTop", CbxPagerTop.Checked);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "ShowPagerBottom", CbxPagerBottom.Checked);

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }
    }
}
