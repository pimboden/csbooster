using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class HTMLContent : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            bool HasContent = false;
            try
            {
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.LoadXml(settingsXml);

                litHTMLContent.Text = XmlHelper.GetElementValueCDATA(xmlDom.DocumentElement, "HTMLContent", string.Empty);
                HasContent = litHTMLContent.Text.Length > 0;

            }
            catch
            {
            }
            return HasContent;
        }
    }
}