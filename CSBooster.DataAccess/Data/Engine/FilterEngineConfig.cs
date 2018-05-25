//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace _4screen.CSB.DataAccess.Data
{
    internal class FilterEngineConfig
    {
        private int userViewTimeSpan;
        private int ipViewTimeSpan;
        private Dictionary<string, string> adminEmailList = new Dictionary<string, string>();
        private Dictionary<string, string> objectLinks = new Dictionary<string, string>();

        internal FilterEngineConfig()
        {
            try
            {
                string configFile = string.Format(FilterEngine.GetApplicationPath() + @"Configurations\FilterEngine.config");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(configFile);
                XmlNode objectViewNode = xmlDocument.SelectSingleNode("//filterEngine/View");
                Business.ViewConfig viewConfig = new Business.ViewConfig(objectViewNode);
                userViewTimeSpan = viewConfig.UserTimeSpanSecond;
                ipViewTimeSpan = viewConfig.IPTimeSpanSecond;

                XmlNodeList adminEmailNodes = xmlDocument.SelectNodes("//filterEngine/badWordFilterReporting/adminEmail");
                foreach (XmlNode adminEmailNode in adminEmailNodes)
                {
                    adminEmailList.Add(adminEmailNode.Attributes["email"].Value, adminEmailNode.Attributes["name"].Value);
                }

                XmlNode objectLinkUrlPrefixNode = xmlDocument.SelectSingleNode("//filterEngine/badWordFilterReporting/urlPrefix");

                XmlNodeList objectLinkNodes = xmlDocument.SelectNodes("//filterEngine/badWordFilterReporting/objectLink");
                foreach (XmlNode objectLinkNode in objectLinkNodes)
                {
                    Type constants = typeof (_4screen.CSB.Common.Constants);
                    FieldInfo fieldInfo = constants.GetField(objectLinkNode.Attributes["const"].Value);
                    string value = (string) fieldInfo.GetValue(null);
                    objectLinks.Add(objectLinkNode.Attributes["type"].Value, objectLinkUrlPrefixNode.InnerText + value);
                }
            }
            catch
            {
            }
        }

        internal int UserViewTimeSpan
        {
            get { return userViewTimeSpan; }
            set { userViewTimeSpan = value; }
        }

        internal int IPViewTimeSpan
        {
            get { return ipViewTimeSpan; }
            set { ipViewTimeSpan = value; }
        }

        internal Dictionary<string, string> AdminEmailList
        {
            get { return adminEmailList; }
            set { adminEmailList = value; }
        }

        internal Dictionary<string, string> ObjectLinks
        {
            get { return objectLinks; }
            set { objectLinks = value; }
        }

        internal static void ReadFilterObjectsConfig(string nodeName, Dictionary<string, FilterObject> filterObjects)
        {
            try
            {
                string configFile = string.Format(FilterEngine.GetApplicationPath() + @"Configurations\FilterEngine.config");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(configFile);
                XmlNodeList filterObjectNodes = xmlDocument.SelectNodes("//filterEngine/" + nodeName + "/object");
                foreach (XmlNode filterObjectNode in filterObjectNodes)
                {
                    if (filterObjectNode.Attributes["type"] != null)
                    {
                        FilterObject filterObject = new FilterObject(filterObjectNode.Attributes["type"].Value);
                        if (filterObjectNode.Attributes["objectTypeId"] != null)
                        {
                            filterObject.ObjectTypeId = int.Parse(filterObjectNode.Attributes["objectTypeId"].Value);
                        }
                        XmlNodeList filterObjectProperties = filterObjectNode.SelectNodes("property");
                        foreach (XmlNode filterObjectProperty in filterObjectProperties)
                        {
                            if (filterObjectProperty.Attributes["name"] != null)
                            {
                                string propertyCopyToName = "";
                                if (filterObjectProperty.Attributes["linkedName"] != null)
                                    propertyCopyToName = filterObjectProperty.Attributes["linkedName"].Value;
                                filterObject.Properties.Add(new FilterObjectProperty(filterObjectProperty.Attributes["name"].Value, propertyCopyToName));
                            }
                        }
                        filterObjects.Add(filterObjectNode.Attributes["type"].Value, filterObject);
                    }
                }
            }
            catch
            {
            }
        }
    }
}