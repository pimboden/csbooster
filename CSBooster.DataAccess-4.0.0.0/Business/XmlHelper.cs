// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Xml;

namespace _4screen.CSB.DataAccess
{
    public static class XmlHelper
    {
        public static XmlNode CreateRoot(XmlDocument xmlDoc, string name)
        {
            if (xmlDoc.DocumentElement == null)
                return xmlDoc.AppendChild(xmlDoc.CreateElement(name));
            else
                return xmlDoc.DocumentElement;
        }

        public static XmlNode AppendNode(XmlNode xmlNode, string name)
        {
            return xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
        }


        public static void SetElementInnerText(XmlNode xmlNode, string name, string content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.Replace("&#x0;", string.Empty).Replace("\0", string.Empty);
        }

        public static string GetElementValue(XmlNode xmlNode, string name, string defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null)
                return xmlNode.SelectSingleNode(name).InnerText;
            else
                return defaultValue;
        }


        public static void SetElementInnerTextCDATA(XmlNode xmlNode, string name, string content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = string.Format("<![CDATA[{0}]]>", content.Replace("&#x0;", string.Empty).Replace("\0", string.Empty));
        }

        public static string GetElementValueCDATA(XmlNode xmlNode, string name, string defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null)
            {
                string retVal = xmlNode.SelectSingleNode(name).InnerText;
                if (retVal.StartsWith("<![CDATA["))
                {
                    retVal = retVal.Substring(9);
                    retVal = retVal.Remove(retVal.Length - 3);
                }
                return retVal;
            }
            else
                return defaultValue;
        }


        public static void SetElementInnerText(XmlNode xmlNode, string name, DateTime content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.Ticks.ToString();
        }

        public static DateTime GetElementValue(XmlNode xmlNode, string name, DateTime defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return new DateTime(Convert.ToInt64(xmlNode.SelectSingleNode(name).InnerText));
            else
                return defaultValue;
        }

        public static void SetElementInnerText(XmlNode xmlNode, string name, Guid content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.ToString();
        }

        public static Guid GetElementValue(XmlNode xmlNode, string name, Guid defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return new Guid( xmlNode.SelectSingleNode(name).InnerText);
            else
                return defaultValue;
        }

        public static void SetElementInnerText(XmlNode xmlNode, string name, int content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.ToString();
        }

        public static int GetElementValue(XmlNode xmlNode, string name, int defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return Convert.ToInt32(xmlNode.SelectSingleNode(name).InnerText);
            else
                return defaultValue;
        }


        public static void SetElementInnerText(XmlNode xmlNode, string name, long content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.ToString();
        }

        public static long GetElementValue(XmlNode xmlNode, string name, long defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return Convert.ToInt64(xmlNode.SelectSingleNode(name).InnerText);
            else
                return defaultValue;
        }


        public static void SetElementInnerText(XmlNode xmlNode, string name, decimal content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.ToString(System.Globalization.CultureInfo.InvariantCulture);   
        }

        public static decimal GetElementValue(XmlNode xmlNode, string name, decimal defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return Convert.ToDecimal(xmlNode.SelectSingleNode(name).InnerText.Replace(',', '.'));
            else
                return defaultValue;
        }


        public static void SetElementInnerText(XmlNode xmlNode, string name, bool content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerText = content.ToString();
        }

        public static bool GetElementValue(XmlNode xmlNode, string name, bool defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && xmlNode.SelectSingleNode(name).InnerText.Length > 0)
                return Convert.ToBoolean(xmlNode.SelectSingleNode(name).InnerText);
            else
                return defaultValue;
        }

        public static void SetElementInnerXml(XmlNode xmlNode, string name, string content)
        {
            XmlNode xmlValue = xmlNode.SelectSingleNode(name);
            if (xmlValue == null)
                xmlValue = xmlNode.AppendChild(xmlNode.OwnerDocument.CreateElement(name));
            xmlValue.InnerXml = content.Replace("&#x0;", string.Empty).Replace("\0", string.Empty);
        }

        public static string GetElementInnerXml(XmlNode xmlNode, string name, string defaultValue)
        {
            if (xmlNode != null && xmlNode.SelectSingleNode(name) != null && !string.IsNullOrEmpty(xmlNode.SelectSingleNode(name).InnerXml))
                return xmlNode.SelectSingleNode(name).InnerXml;
            else
                return defaultValue;
        }

        public static int GetAttributeValue(XmlElement xmlElement, string name, int defaultValue)
        {
            if (xmlElement != null && xmlElement.HasAttribute(name) && xmlElement.GetAttribute(name).Length > 0)
                return Convert.ToInt32(xmlElement.GetAttribute(name));
            else
                return defaultValue;
        }

        public static decimal GetAttributeValue(XmlElement xmlElement, string name, decimal defaultValue)
        {
            if (xmlElement != null && xmlElement.HasAttribute(name) && xmlElement.GetAttribute(name).Length > 0)
                return Convert.ToDecimal(xmlElement.GetAttribute(name));
            else
                return defaultValue;
        }

        public static string GetAttributeValue(XmlElement xmlElement, string name, string defaultValue)
        {
            if (xmlElement != null && xmlElement.HasAttribute(name))
                return xmlElement.GetAttribute(name);
            else
                return defaultValue;
        }

        public static bool GetAttributeValue(XmlElement xmlElement, string name, bool defaultValue)
        {
            if (xmlElement != null && xmlElement.HasAttribute(name) && xmlElement.GetAttribute(name).Length > 0)
                return Convert.ToBoolean(xmlElement.GetAttribute(name));
            else
                return defaultValue;
        }
    }
}