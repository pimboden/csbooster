// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Xml;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class IFrame : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            string borderAttributes = string.Empty;
            if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameBorder", true))
                borderAttributes = "frameborder=\"0\" style=\"border:solid 1px black;\"";
            else
                borderAttributes = "frameborder=\"0\"";
            string Link = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameURL", string.Empty);

            LitIFrame.Text = string.Format("<iframe src=\"{0}\" width=\"100%\" height=\"{1}px\" {2}></iframe>", Link, XmlHelper.GetElementValue(xmlDocument.DocumentElement, "IFrameHeight", 100), borderAttributes);
            return !string.IsNullOrEmpty(Link.Trim());
        }
    }
}