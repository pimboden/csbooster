// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class NavigationTranslate : Page
    {
        string currentNeutralLanguage = string.Empty;
        string translationNeutralLanguage = string.Empty;
        Guid navId = Guid.Empty;
        string nodeValue = string.Empty;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            currentNeutralLanguage = CultureHandler.GetCurrentNeutralLanguageCode();
            ci = new CultureInfo(currentNeutralLanguage);
            LoadDdlLangs(currentNeutralLanguage);
            navId = new Guid(Request.QueryString["NAV"]);
            nodeValue = Request.QueryString["NID"];
            LoadCurrent();
            if (!Page.IsPostBack)
            {
                LoadTranslation();
            }
        }
        protected void ddlLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            translationNeutralLanguage = ddlLangs.SelectedValue;
            LoadTranslation();
        }

        protected void lbtnS_Click(object sender, EventArgs e)
        {
            SaveTranslation();
        }


        private void LoadDdlLangs(string excludeLanguage)
        {
            ddlLangs.Items.Clear();
            foreach (string langCode in SiteConfig.NeutralLanguages.Keys)
            {
                if (langCode != excludeLanguage)
                    ddlLangs.Items.Add(new ListItem(SiteConfig.NeutralLanguages[langCode], langCode));
            }
            ddlLangs.SelectedIndex = 0;
        }

        private void LoadTranslation()
        {
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XElement xmlLanguageNode = null;
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(navId).SingleOrDefault();
            var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(navId, translationNeutralLanguage).SingleOrDefault();

            XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
            XElement xmlStructNode = (from xmlStructNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                      select xmlStructNodes).SingleOrDefault();

            if (navLanguageRecord != null)
            {
                XElement xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                xmlLanguageNode = (from xmlLanguageNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                   select xmlLanguageNodes).SingleOrDefault();
            }
            if (xmlLanguageNode != null)
            {
                txtText.Text = xmlLanguageNode.Attribute("Text") != null ? xmlLanguageNode.Attribute("Text").Value : string.Empty;
                txtTooltip.Text = xmlLanguageNode.Attribute("Tooltip") != null ? xmlLanguageNode.Attribute("Tooltip").Value : string.Empty;



                if ((xmlStructNode.Attribute("PredefinedUrl") == null || xmlStructNode.Attribute("PredefinedUrl").Value == "") &&
                    (xmlStructNode.Attribute("ObjectUrl") == null || string.IsNullOrEmpty(xmlStructNode.Attribute("ObjectUrl").Value)))
                {
                    //Neither Predeifined nor link to an object
                    txtLink.Text = xmlLanguageNode.Attribute("NavigateUrl") != null ? xmlLanguageNode.Attribute("NavigateUrl").Value : string.Empty;
                    txtLink.Enabled = true;
                }
                else if (xmlStructNode.Attribute("ObjectUrl") != null && !string.IsNullOrEmpty(xmlStructNode.Attribute("ObjectUrl").Value))
                {
                    //link to an object
                    txtLink.Text = xmlLanguageNode.Attribute("NavigateUrl") != null ? xmlLanguageNode.Attribute("NavigateUrl").Value : string.Empty;
                    txtLink.Enabled = true;
                }
                else
                {
                    txtLink.Text = " Vordefiniert ";
                    txtLink.Enabled = false;
                    var xPredefinedNavi = Constants.Links[xmlStructNode.Attribute("PredefinedUrl").Value];
                    string localizationBaseFileName = "Navigation";
                    if (!string.IsNullOrEmpty(xPredefinedNavi.LocalizationBaseFileName))
                    {
                        localizationBaseFileName = xPredefinedNavi.LocalizationBaseFileName;
                    }
                    txtText.Text = GuiLanguage.GetGuiLanguage(localizationBaseFileName, translationNeutralLanguage).GetString(xPredefinedNavi.UrlTextKey) ?? string.Empty;
                }
            }
            else
            {
                txtText.Text = string.Empty;
                txtTooltip.Text = string.Empty;
                if (xmlStructNode.Attribute("PredefinedUrl") == null || string.IsNullOrEmpty(xmlStructNode.Attribute("PredefinedUrl").Value))
                {
                    txtLink.Text = string.Empty;
                    txtLink.Enabled = true;
                }
                else
                {
                    txtLink.Text = " Vordefiniert ";
                    txtLink.Enabled = false;
                    var xPredefinedNavi = Constants.Links[xmlStructNode.Attribute("PredefinedUrl").Value];

                    string localizationBaseFileName = "Navigation";
                    if (!string.IsNullOrEmpty(xPredefinedNavi.LocalizationBaseFileName))
                    {
                        localizationBaseFileName = xPredefinedNavi.LocalizationBaseFileName;
                    }
                    txtText.Text = GuiLanguage.GetGuiLanguage(localizationBaseFileName, translationNeutralLanguage).GetString(xPredefinedNavi.UrlTextKey) ?? string.Empty;                    
                }
            }
        }

        private void LoadCurrent()
        {
            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XElement xmlLanguageNode = null;
            litCurrLang.Text = ci.NativeName;
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(navId).SingleOrDefault();
            var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(navId, CultureHandler.GetCurrentNeutralLanguageCode()).SingleOrDefault();

            XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
            XElement xmlStructNode = (from xmlStructNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                      select xmlStructNodes).SingleOrDefault();

            if (navLanguageRecord != null)
            {
                XElement xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                xmlLanguageNode = (from xmlLanguageNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                   select xmlLanguageNodes).SingleOrDefault();
            }
            if (xmlLanguageNode != null)
            {
                litText.Text = xmlLanguageNode.Attribute("Text") != null ? xmlLanguageNode.Attribute("Text").Value : string.Empty;
                litTooltip.Text = xmlLanguageNode.Attribute("Tooltip") != null ? xmlLanguageNode.Attribute("Tooltip").Value : string.Empty;
                if (xmlStructNode.Attribute("PredefinedUrl") == null || string.IsNullOrEmpty(xmlStructNode.Attribute("PredefinedUrl").Value))
                {
                    litLink.Text = xmlLanguageNode.Attribute("NavigateUrl") != null ? xmlLanguageNode.Attribute("NavigateUrl").Value : string.Empty;
                }
                else
                {
                    litLink.Text = " Vordefiniert ";
                }
            }
        }

        private void SaveTranslation()
        {


            CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            XElement xmlLanguageNode;
            var navStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(navId).SingleOrDefault();
            var navLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(navId, translationNeutralLanguage).SingleOrDefault();

            XElement xmlStruct = XElement.Parse(navStructureRecord.NST_XMLStruct);
            XElement xmlStructNode = (from xmlStructNodes in xmlStruct.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                      select xmlStructNodes).SingleOrDefault();
            XElement xmlLanguage;
            if (navLanguageRecord != null)
            {
                xmlLanguage = XElement.Parse(navLanguageRecord.NAV_XMLNames);
                xmlLanguageNode = (from xmlLanguageNodes in xmlLanguage.Descendants("Node").Where(x => x.Attribute("Value").Value == nodeValue)
                                   select xmlLanguageNodes).SingleOrDefault();

                if (xmlLanguageNode == null)
                {
                    xmlLanguageNode = XElement.Parse(string.Format(@"<Node Text='' Value='{0}'/>", nodeValue));
                    xmlLanguage.Add(xmlLanguageNode);
                }
            }
            else
            {
                xmlLanguage = XElement.Parse(string.Format(@"<Tree><Node Text='' Value='{0}'/></Tree>", nodeValue));
                xmlLanguageNode = xmlLanguage.Nodes().OfType<XElement>().FirstOrDefault();
            }

            xmlLanguageNode.SetAttributeValue("Text", txtText.Text);
            xmlLanguageNode.SetAttributeValue("Tooltip", txtTooltip.Text);

            if (xmlStructNode.Attribute("PredefinedUrl") == null || string.IsNullOrEmpty(xmlStructNode.Attribute("PredefinedUrl").Value))
            {
                xmlLanguageNode.SetAttributeValue("NavigateUrl", txtLink.Text);
            }
            else
            {

                var xPredefinedNavi = Constants.Links[xmlStructNode.Attribute("PredefinedUrl").Value];

                if (xPredefinedNavi != null)
                {
                    string url = xPredefinedNavi.Url;
                    xmlLanguageNode.SetAttributeValue("NavigateUrl", url);
                    string baseFileName = "Navigation";
                    if (!string.IsNullOrEmpty(xPredefinedNavi.LocalizationBaseFileName))
                    {
                        baseFileName = xPredefinedNavi.LocalizationBaseFileName;

                    }

                    xmlLanguageNode.SetAttributeValue("Text", GuiLanguage.GetGuiLanguage(baseFileName, translationNeutralLanguage).GetString(xPredefinedNavi.UrlTextKey) ?? Request.Form[txtText.UniqueID]);

                    txtText.Text = xmlLanguageNode.Attribute("Text").Value;
                }
                else
                {
                    xmlLanguageNode.SetAttributeValue("NavigateUrl", null);
                }

            }
            csb.hisp_Navigation_SaveNavigationLanguage(navId, translationNeutralLanguage, xmlLanguage.ToString(), string.Empty);

        }
    }
}
