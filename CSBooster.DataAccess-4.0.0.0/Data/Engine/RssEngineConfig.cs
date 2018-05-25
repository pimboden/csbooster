// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web;
using System.Xml;

namespace _4screen.CSB.DataAccess.Data
{
    internal class RssEngineConfig
    {
        public int Days { get; set; }
        public int MaxDescriptionLength { get; set; }
        public string ObjectTypes { get; set; }
        public int MaxItems { get; set; }

        internal RssEngineConfig()
        {
            string configFile = string.Format(HttpContext.Current.Request.PhysicalApplicationPath + @"Configurations\RssEngine.config");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configFile);

            XmlNode settingsNode = xmlDocument.SelectSingleNode("//rssEngine/days");
            Days = int.Parse(settingsNode.InnerText);

            settingsNode = xmlDocument.SelectSingleNode("//rssEngine/maxDescriptionLength");
            MaxDescriptionLength = int.Parse(settingsNode.InnerText);

            settingsNode = xmlDocument.SelectSingleNode("//rssEngine/objectTypes");
            ObjectTypes = settingsNode.InnerText;

            settingsNode = xmlDocument.SelectSingleNode("//rssEngine/maxItems");
            MaxItems = int.Parse(settingsNode.InnerText);
        }
    }
}