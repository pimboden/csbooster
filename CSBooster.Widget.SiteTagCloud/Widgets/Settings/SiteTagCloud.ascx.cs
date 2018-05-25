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
using System.Web.UI.WebControls;
using System.Configuration;

namespace _4screen.CSB.Widget.Settings
{
    public partial class SiteTagCloud : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetSiteTagCloud");
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

            RntbMaxNumber.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagWordCloudSize", int.Parse(ConfigurationManager.AppSettings["TagWordCloudSize"]));
            int relevance = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Relevance", 0);
            int relevanceType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RelevanceType", 1);

            foreach (ListItem item in RblRelevance.Items)
            {
                item.Text = language.GetString("TextRelevance" + item.Value);
                item.Selected = (item.Value == relevance.ToString());
            }

            foreach (ListItem item in RblRelevanceType.Items)
            {
                item.Text = language.GetString("TextRelevanceType" + item.Value);
                item.Selected = (item.Value == relevanceType.ToString());
            }
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);

                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "TagWordCloudSize", (int)RntbMaxNumber.Value.Value);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Relevance", this.RblRelevance.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "RelevanceType", this.RblRelevanceType.SelectedValue);  

                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }
    }
}
