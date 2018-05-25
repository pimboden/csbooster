using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class FastRSS : WidgetBase
    {
        protected bool showDesc = true;


        public override bool ShowObject(string strXml)
        {
            ViewState["XMLSettings"] = strXml;
            FastRSSLoad();
            return true;
        }


        protected void FastRSSLoad()
        {
            string strXml = ViewState["XMLSettings"] as string;
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(strXml);
            string url = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtURL", string.Empty);
            int feedCount = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlFC", 3);
            showDesc = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxDesc", true);
            ScriptManager.RegisterClientScriptInclude(this, GetType(), "FastRssWidget", ResolveClientUrl(SiteContext.VRoot + "/Library/Scripts/FastRssWidget.js"));
            ScriptManager.RegisterStartupScript(this, GetType(), "LoadRSS", string.Format("var rssLoaderWidget{0} = new FastRssWidget( '{1}', '{2}', {3}, {4} ); rssLoaderWidget{0}.load();", InstanceID.ToString().Replace("-", "_"), url, RssContainer.ClientID, feedCount, Convert.ToInt32(showDesc)), true);
        }
    }
}