// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot("Styles")]
    public class StyleSettingsWidget
    {
        [XmlElement("Header")]
        public StyleSettings Header { get; set; }

        [XmlElement("Content")]
        public StyleSettings Content { get; set; }

        [XmlElement("Footer")]
        public StyleSettings Footer { get; set; }

        [XmlElement("CustomStyle")]
        public string CustomStyle { get; set; }

        public string GetXml()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            serializer.Serialize(writer, this);
            return Regex.Replace(sb.ToString(), "<Styles.*?>", "<Styles>");
        }

        public static StyleSettingsWidget ParseXml(string xml)
        {
            StringReader reader = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(typeof(StyleSettingsWidget));
            return (StyleSettingsWidget)serializer.Deserialize(reader);
        }
    }
}
