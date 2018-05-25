// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace _4screen.CSB.Common
{
    public class ProfileDataHelper
    {
        private static List<InterestTopic> interestTopics = null;

        public static List<InterestTopic> InterestTopics
        {
            get
            {
                if (HttpRuntime.Cache["ProfileinterestTopic"] == null || interestTopics == null)
                {
                    interestTopics = new List<InterestTopic>();
                    LoadinterestTopicsFromXML(string.Empty, "de-ch");
                }
                return interestTopics;
            }
        }

        public static string GetInterestTopicDisplayName(string dbFieldName)
        {
            return interestTopics.Find(x => x.DBField.ToLower() == dbFieldName.ToLower()).DisplayName;
        }

        public static InterestTopic GetInterestTopic(string dbFieldName)
        {
            return interestTopics.Find(x => x.DBField.ToLower() == dbFieldName.ToLower());
        }

        public static Dictionary<string, string> GetInterestSubTopic(string dbFieldName)
        {
            List<InterestSubTopic> subTopics = interestTopics.Find(x => x.DBField.ToLower() == dbFieldName.ToLower()).InterestSubTopic;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (InterestSubTopic subTopic in subTopics)
            {
                dict.Add(subTopic.ID, subTopic.DisplayName);
            }
            return dict;
        }

        //Load thestatic Field
        private static void LoadinterestTopicsFromXML(string VRoot, string LangCode)
        {
            XmlDocument xmlConfig = new XmlDocument();
            HttpRuntime.Cache.Remove("ProfileinterestTopic");
            string configFile = HttpContext.Current.Server.MapPath(string.Format("{0}/configurations/ProfileData.config", VRoot));
            xmlConfig.Load(configFile);
            XmlNodeList xmlinterestTopics = xmlConfig.SelectNodes("/Root/InterestTopic");
            int i = 0;
            foreach (XmlNode xmlInterestTopic in xmlinterestTopics)
            {
                if (i < 10) //Only 10 DB Fields are available... throw exception if more that 10 are found in the DB
                {
                    InterestTopic InterestTopic = new InterestTopic();
                    InterestTopic.ID = xmlInterestTopic.Attributes["ID"].Value;
                    InterestTopic.DBField = xmlInterestTopic.Attributes["DBField"].Value;
                    if (InterestTopic.DBField.ToLower().IndexOf("interesttopic") == -1)
                    {
                        throw new SiemeException("ProfileInterestTopic", "LoadinterestTopicsFromXML()", "DBField in ProfileinterestTopics.config must  be InterestTopicx where x goes from 1 to 10");
                    }
                    InterestTopic.DisplayName = xmlInterestTopic.SelectSingleNode(string.Format("./FriendlyName[@Lang = '{0}']", LangCode.ToLower())).InnerText;
                    InterestTopic.NameSingular = xmlInterestTopic.SelectSingleNode(string.Format("./NameSingular[@Lang = '{0}']", LangCode.ToLower())).InnerText;
                    InterestTopic.NamePlural = xmlInterestTopic.SelectSingleNode(string.Format("./NamePlural[@Lang = '{0}']", LangCode.ToLower())).InnerText;
                    InterestTopic.InterestSubTopic = new List<InterestSubTopic>();
                    XmlNodeList xmlInterestSubTopics = xmlInterestTopic.SelectNodes("./InterestSubTopics/InterestSubTopic");
                    foreach (XmlNode xmlInterestSubTopic in xmlInterestSubTopics)
                    {
                        InterestSubTopic InterestSubTopic = new InterestSubTopic();
                        InterestSubTopic.ID = xmlInterestSubTopic.Attributes["ID"].Value;
                        InterestSubTopic.Value = xmlInterestSubTopic.SelectSingleNode("./Value").InnerText;
                        InterestSubTopic.DisplayName = xmlInterestSubTopic.SelectSingleNode(string.Format("./FriendlyName[@Lang = '{0}']", LangCode.ToLower())).InnerText;
                        InterestTopic.InterestSubTopic.Add(InterestSubTopic);
                    }
                    interestTopics.Add(InterestTopic);
                }
                else
                {
                    throw new SiemeException("ProfileInterestTopic", "LoadinterestTopicsFromXML()", "Too Many interestTopics defined in ProfileinterestTopics.config");
                }
                i++;
            }
            //The Cache is only set for the static field to get reloaded in case that the config file got changed
            System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(configFile);
            HttpRuntime.Cache.Insert("ProfileinterestTopic", string.Empty, dep);
        }

        public static string GetProfileDataTitle(string vRoot, UserProfileDataKey key)
        {
            XDocument doc = null;
            if (HttpRuntime.Cache["ProfileData"] != null)
            {
                doc = (XDocument)HttpRuntime.Cache["ProfileData"];
            }
            else
            {
                doc = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/ProfileData.config", vRoot)));
                HttpRuntime.Cache.Insert("ProfileData", doc);
            }

            var config = doc.Element("Root");
            var name = from personalItems in config.Elements("Personal").Where(p => p.Attribute("DBField").Value == key.ToString()) from friendlyName in personalItems.Elements("FriendlyName").Where(f => f.Attribute("Lang").Value == "de-ch") select friendlyName.Value;

            return name.SingleOrDefault();
        }

        public static Dictionary<string, string> GetProfileDataNames(string vRoot, UserProfileDataKey key)
        {
            XDocument doc = null;
            if (HttpRuntime.Cache["ProfileData"] != null)
            {
                doc = (XDocument)HttpRuntime.Cache["ProfileData"];
            }
            else
            {
                doc = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/ProfileData.config", vRoot)));
                HttpRuntime.Cache.Insert("ProfileData", doc);
            }

            var config = doc.Element("Root");
            var names = from personalItems in config.Elements("Personal").Where(p => p.Attribute("DBField").Value == key.ToString()) from personalItem in personalItems.Elements("PeronalItem").Where(p => p.Attribute("Condition").Value == "Update" || p.Attribute("Condition").Value == "None") from friendlyName in personalItem.Elements("FriendlyName").Where(f => f.Attribute("Lang").Value == "de-ch") select new { Name = friendlyName.Value, Value = personalItem.Element("Value").Value };

            return names.ToDictionary(k => k.Value, k => k.Name);
        }

        public static void FillProfileDataList(string vRoot, DropDownList ddl, UserProfileDataKey key, bool isSearch)
        {
            XDocument doc = null;
            if (HttpRuntime.Cache["ProfileData"] != null)
            {
                doc = (XDocument)HttpRuntime.Cache["ProfileData"];
            }
            else
            {
                doc = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/ProfileData.config", vRoot)));
                HttpRuntime.Cache.Insert("ProfileData", doc);
            }

            var config = doc.Element("Root");
            var dropDownList = from personalItems in config.Elements("Personal").Where(p => p.Attribute("DBField").Value == key.ToString()) from personalItem in personalItems.Elements("PeronalItem").Where(p => (isSearch && p.Attribute("Condition").Value == "Search") || (!isSearch && p.Attribute("Condition").Value == "Update") || p.Attribute("Condition").Value == "None") from friendlyName in personalItem.Elements("FriendlyName").Where(f => f.Attribute("Lang").Value == "de-ch") select new { Text = friendlyName.Value, Value = personalItem.Element("Value").Value };

            ddl.Items.Clear();
            foreach (var item in dropDownList)
            {
                ddl.Items.Add(new ListItem() { Text = item.Text, Value = item.Value });
            }
        }

        public static List<ProfileDataGeneralItem> GetProfileDataGeneralItems(string vRoot)
        {
            XDocument doc = null;
            if (HttpRuntime.Cache["ProfileData"] != null)
            {
                doc = (XDocument)HttpRuntime.Cache["ProfileData"];
            }
            else
            {
                doc = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/ProfileData.config", vRoot)));
                HttpRuntime.Cache.Insert("ProfileData", doc);
            }

            var config = doc.Element("Root");
            var names = from generalItems in config.Elements("General") from friendlyName in generalItems.Elements("FriendlyName").Where(f => f.Attribute("Lang").Value == "de-ch") select new ProfileDataGeneralItem() { DBField = generalItems.Attribute("DBField").Value, FriendlyName = friendlyName.Value };

            return names.ToList();
        }
    }

    public class ProfileDataGeneralItem
    {
        public string DBField { get; set; }
        public string FriendlyName { get; set; }
    }

    public class InterestTopic
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string DBField { get; set; }
        public string NameSingular { get; set; }
        public string NamePlural { get; set; }
        public List<InterestSubTopic> InterestSubTopic { get; set; }
    }

    public class InterestSubTopic
    {
        public Type type { get; set; }
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}