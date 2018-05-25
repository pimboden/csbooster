// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public static class Navigation
    {
        public enum NavigationType
        {
            TreeView = 0,
            FlyOutMenu = 1,
            Outlook = 3
        }

        private static CSBooster_DataContext csb = new CSBooster_DataContext(Helper.GetSiemeConnectionString());

        public static XElement ReplaceData(XElement OriginalNode, XElement xmlLanguage, bool IgnoreNavigatUrl)
        {
            XElement newNode = null;
            if (OriginalNode != null)
            {
                string Value = OriginalNode.Attribute("Value").Value;

                newNode = new XElement("Node");
                foreach (var attribute in OriginalNode.Attributes())
                {
                    if (!(IgnoreNavigatUrl && attribute.Name == "NavigateUrl"))
                    {
                        newNode.SetAttributeValue(attribute.Name, attribute.Value);
                    }

                }

                var langNode = (from xmlLang in xmlLanguage.Descendants("Node")
                                where (xmlLang.Attribute("Value").Value == Value)
                                select xmlLang).SingleOrDefault();


                if (langNode != null)
                {
                    foreach (var attributeLng in langNode.Attributes())
                    {
                        if (!(IgnoreNavigatUrl && attributeLng.Name == "NavigateUrl"))
                        {
                            newNode.SetAttributeValue(attributeLng.Name, attributeLng.Value);
                        }
                    }
                }
                foreach (var xnode in OriginalNode.Elements("Node"))
                {
                    XElement xnewChild = ReplaceData(xnode, xmlLanguage, true);
                    if (xnewChild != null)
                        newNode.Add(xnewChild);
                }
            }
            return newNode;
        }

        //Merges the structure node with the Language... If the Text od the Node in the Translation  is not set 
        //Then null is retuned
        public static XElement MergeWithLangData(XElement OriginalNode, XElement xmlLanguage)
        {
            XElement newNode = null;
            bool NodeInvalid = false;
            if (OriginalNode != null)
            {
                string Value = OriginalNode.Attribute("Value").Value;

                newNode = new XElement("Node");
                foreach (var attribute in OriginalNode.Attributes())
                {
                    //Copy only attributes that are needed
                    if (attribute.Name != "Text"
                        && attribute.Name != "NavigateUrl"
                        && attribute.Name != "Tooltip"
                        && attribute.Name != "RolesVisibility"
                        && attribute.Name != "PredefinedUrl")
                    {
                        newNode.SetAttributeValue(attribute.Name, attribute.Value);
                    }

                }

                var langNode = (from xmlLang in xmlLanguage.Descendants("Node")
                                where (xmlLang.Attribute("Value").Value == Value)
                                select xmlLang).SingleOrDefault();


                if (langNode != null)
                {
                    foreach (var attributeLng in langNode.Attributes())
                    {
                        if (attributeLng.Name == "Text" && attributeLng.Value.Trim() == string.Empty)
                        {
                            NodeInvalid = true;
                            break;
                        }
                        if (attributeLng.Value.Trim() != string.Empty)
                        {
                            newNode.SetAttributeValue(attributeLng.Name, attributeLng.Value);
                        }

                    }
                }
                else
                {
                    NodeInvalid = true;
                }
                if (!NodeInvalid)
                {
                    foreach (var xnode in OriginalNode.Elements("Node"))
                    {
                        XElement xnewChild = MergeWithLangData(xnode, xmlLanguage);
                        if (xnewChild != null)
                            newNode.Add(xnewChild);
                    }
                }
                else
                {
                    newNode = null;
                }
            }
            return newNode;
        }

        public static void GeneratePreCacheNavigation(Guid navId, string langKey, string role)
        {
            XElement xRolNavi = XElement.Parse("<Tree/>");
            XElement xRolStruct = XElement.Parse("<Tree/>");
            try
            {
                var NavStructureRecord = csb.hisp_Navigation_GetNavigationStructureXML(navId).SingleOrDefault();
                XElement xmlStruct = XElement.Parse(NavStructureRecord.NST_XMLStruct);
                var NavLanguageRecord = csb.hisp_Navigation_GetNavigationLanguageXML(navId, langKey).SingleOrDefault();
                if (NavLanguageRecord != null)
                {
                    XElement xmlLanguage = XElement.Parse(NavLanguageRecord.NAV_XMLNames);
                    //Get only Struct Nodes that are for the CurrentRole

                    foreach (var xnode in xmlStruct.Elements("Node"))
                    {

                        XElement xnewChild = GetRoleNodes(xnode, role);
                        if (xnewChild != null)
                        {
                            xRolStruct.Add(xnewChild);
                        }
                    }
                    foreach (var xnode in xRolStruct.Elements("Node"))
                    {
                        XElement xnewChild = MergeWithLangData(xnode, xmlLanguage);
                        if (xnewChild != null)
                        {
                            xRolNavi.Add(xnewChild);
                        }
                    }
                }
                csb.hisp_Navigation_SavePreChache(navId, langKey, role, xRolNavi.ToString());
            }
            catch 
            {
            }

        }
        private static XElement GetRoleNodes(XElement currentElement, string RoleName)
        {
            XElement ReturnElement = XElement.Parse(currentElement.ToString());
            ReturnElement.RemoveNodes();
            if (currentElement.Attribute("RolesVisibility") == null
                     || currentElement.Attribute("RolesVisibility").Value == string.Empty
                     || currentElement.Attribute("RolesVisibility").Value.ToLower().Split(Constants.TAG_DELIMITER).Contains(RoleName.ToLower()))
            {
                if (currentElement.HasElements)
                {
                    foreach (var child in currentElement.Nodes().OfType<XElement>())
                    {
                        XElement retChild = GetRoleNodes(child, RoleName);
                        if (child != null)
                        {
                            ReturnElement.Add(child);
                        }
                    }
                }

            }
            else
            {
                ReturnElement = null;
            }
            return ReturnElement;
        }

        public static string TranformXML(string navXml, NavigationType NavType)
        {
            string NewNavXML = navXml;
            switch (NavType)
            {
                case _4screen.CSB.DataAccess.Business.Navigation.NavigationType.FlyOutMenu:
                    NewNavXML = TranformXML_Menu(navXml);
                    break;
                case _4screen.CSB.DataAccess.Business.Navigation.NavigationType.Outlook:
                    NewNavXML = TranformXML_Outlook(navXml);
                    break;
                default:
                    break;
            }
            return NewNavXML;
        }

        private static string TranformXML_Outlook(string navXml)
        {
            return navXml.Replace("<Tree>", "<PanelBar>").Replace("<Node", "<Item").Replace("</Tree>", "</PanelBar>").Replace("</Node>", "</Item>");
        }


        private static string TranformXML_Menu(string navXml)
        {
            string strTemp = navXml.Replace("<Tree>", "<Menu>").Replace("<Node", "<Item").Replace("</Tree>", "</Menu>").Replace("</Node>", "</Item>"); ;
            XElement xNavi = XElement.Parse(strTemp);
            XElement xRootGroup = new XElement("Group");
            XElement xNewNavi = new XElement("Menu", xRootGroup);
            foreach (XElement xelem in xNavi.Nodes().OfType<XElement>())
            {
                XElement newChild = GetMenuElement(xelem);
                if (newChild != null)
                {
                    xRootGroup.Add(newChild);
                }
            }
            return xNewNavi.ToString();
        }
        private static XElement GetMenuElement(XElement currNode)
        {
            XElement returnElem = null;
            if (currNode == null)
            {
                return null;
            }
            else
            {
                returnElem = new XElement("Item");
                foreach (XAttribute origAtt in currNode.Attributes())
                {
                    returnElem.SetAttributeValue(origAtt.Name, origAtt.Value);
                }
                if (currNode.HasElements)
                {
                    XElement newGroup = new XElement("Group");
                    foreach (XElement xelem in currNode.Nodes().OfType<XElement>())
                    {
                        XElement newChild = GetMenuElement(xelem);
                        if (newChild != null)
                        {
                            newGroup.Add(newChild);
                        }
                    }
                    returnElem.Add(newGroup);
                }
            }
            return returnElem;
        }
    }
}
