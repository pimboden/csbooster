//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//  Updated:   #1.1.0.0    11.01.2008 / PT   Lesen alternatives Rating pro Community
//             #1.1.0.0    14.01.2008 / PT   Einbau Factor (RatedConsolidatedFactor)
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class DataAccessConfiguration
    {
        public static QuickSort GetCommunityRatedQuickSort(string communityID)
        {
            try
            {
                XmlDocument xmlDoc = LoadDataAccessConfig();
                XmlNode xmlCommunity = xmlDoc.SelectSingleNode(string.Format("root/RatingQuickSort/QuickSort[@Cty='{0}']", communityID));
                if (xmlCommunity == null)
                    xmlCommunity = xmlDoc.SelectSingleNode("root/RatingQuickSort/QuickSort[@Cty='*']");

                if (xmlCommunity != null)
                    return (QuickSort)Enum.Parse(typeof(QuickSort), xmlCommunity.InnerText);
                else
                    return QuickSort.RatedAverage;
            }
            catch
            {
                return QuickSort.RatedAverage;
            }
        }

        public static RatingConfig GetRatingConfig(RatingType ratingType)
        {
            RatingConfig item = new RatingConfig(LoadDataAccessConfig().SelectSingleNode(string.Format("root/Rating[@Type='{0}']", ratingType)));
            return item;
        }

        public static ViewConfig GetViewConfig()
        {
            ViewConfig item = new ViewConfig(LoadDataAccessConfig().SelectSingleNode("root/View"));
            return item;
        }

        public static DefaultImages GetDefaultImages()
        {
            DefaultImages item = new DefaultImages(LoadDataAccessConfig().SelectSingleNode("root/DefaultImages"));
            return item;
        }

        public static string GetURLImageSmall(int objectType)
        {
            DefaultImages item = GetDefaultImages();
            return item.GetURLImageSmall(objectType);
        }

        public static List<string> GetExcludeUsers()
        {
            List<string> list = new List<string>();
            foreach (XmlNode item in LoadDataAccessConfig().SelectNodes("root/ExcludeUser/User"))
            {
                list.Add(item.InnerText.ToLower());
            }
            return list;
        }

        public static List<string> CharacterDieresis(string charachter)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(charachter) && charachter.Length == 1)
            {
                XmlElement item = LoadDataAccessConfig().SelectSingleNode(string.Format("root/CharacterDieresis/Char[@Key='{0}']", charachter.ToLower())) as XmlElement;
                if (item != null)
                {
                    foreach (string c in item.GetAttribute("Value").Split(','))
                    {
                        list.Add(c);
                    }
                }
            }
            return list;
        }

        public static Dictionary<string, UserAddressFields> GetUserAddressFields()
        {
            Dictionary<string, UserAddressFields> list = new Dictionary<string, UserAddressFields>();
            foreach (XmlElement item in LoadDataAccessConfig().SelectNodes("root/UserAddressFields/Field"))
            {
                UserAddressFields uaf = new UserAddressFields();
                uaf.Name = item.GetAttribute("Name");
                uaf.Must = Convert.ToBoolean(item.GetAttribute("Must"));
                uaf.Active = Convert.ToBoolean(item.GetAttribute("Active"));
                if (!string.IsNullOrEmpty(item.GetAttribute("Overrule"))
) uaf.Overrule = Convert.ToBoolean(item.GetAttribute("Overrule"));
                else
                    uaf.Overrule = false;
                if (!list.ContainsKey(uaf.Name))
                    list.Add(uaf.Name, uaf);
            }
            return list;
        }

        public static string GetDefaultCachingTime()
        {
            return LoadDataAccessConfig().SelectSingleNode("root/DefaultCachingTiem").InnerText;
        }

        public static TimeSpan LastActivityDateUpdateTimeGap()
        {
            return Helper.ConvertFromString(LoadDataAccessConfig().SelectSingleNode("root/LastActivityDateUpdateTimeGap").InnerText);
        }

        public static TimeSpan UserOnlineTimeGap()
        {
            return Helper.ConvertFromString(LoadDataAccessConfig().SelectSingleNode("root/UserOnlineTimeGap").InnerText);
        }

        public static bool IsDBCacheActiv
        {
            get { return bool.Parse(LoadDataAccessConfig().SelectSingleNode("root/IsDBCacheActiv").InnerText); }
        }

        private static XmlDocument LoadDataAccessConfig()
        {
            XmlDocument xmlDoc = (XmlDocument)HttpRuntime.Cache["DataAccess.config"];
            if (xmlDoc == null)
            {
                string strFile = string.Format(@"{0}Configurations\DataAccess.config", WebRootPath.Instance.ToString());
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strFile);
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                HttpRuntime.Cache.Insert("DataAccess.config", xmlDoc, dep, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
            }
            return xmlDoc;
        }

        public static bool UserActivityIsActivityActiv(UserActivityWhat activityWhat)
        {
            UserActivitySettings settings = UserActivityLoadSettings();
            return (settings.ActivitySetting.ContainsKey(activityWhat));
        }

        public static bool UserActivityIsActiv()
        {
            return (UserActivityLoadSettings().ActivitySetting.Count > 0);
        }

        public static int UserActivityMaximalAmount()
        {
            return UserActivityLoadSettings().MaximalAmount;
        }

        public static int UserActivityValidUntil(UserActivityWhat activityWhat)
        {
            UserActivitySettings settings = UserActivityLoadSettings();
            if (settings.ActivitySetting.ContainsKey(activityWhat))
                return settings.ActivitySetting[activityWhat];
            else
                return -1;
        }

        private static UserActivitySettings UserActivityLoadSettings()
        {
            UserActivitySettings settings = HttpRuntime.Cache["UserActivitySettings"] as UserActivitySettings;

            if (settings == null)
            {
                settings = new UserActivitySettings();
                settings.ActivitySetting = new Dictionary<UserActivityWhat, int>(10);
                settings.MaximalAmount = 0;

                try
                {
                    settings.MaximalAmount = LoadDataAccessConfig().SelectSingleNode("root/UserActivitySettings/MaximalAmount").InnerText.ToInt32(25);

                    XmlNode xmlValid = LoadDataAccessConfig().SelectSingleNode("root/UserActivitySettings/Default/ValidUntil");
                    if (xmlValid != null)
                    {
                        TimeSpan tisDefault = xmlValid.InnerText.ToTimeSpan();
                        if (tisDefault.TotalMinutes >= 1)
                        {
                            foreach (UserActivityWhat what in Enum.GetValues(typeof(UserActivityWhat)))
                            {
                                xmlValid = LoadDataAccessConfig().SelectSingleNode(string.Format("root/UserActivitySettings/{0}/ValidUntil", what.ToString()));
                                if (xmlValid != null)
                                {
                                    TimeSpan timeSpan = xmlValid.InnerText.ToTimeSpan();
                                    if (timeSpan.TotalMinutes >= 1)
                                        settings.ActivitySetting.Add(what, Convert.ToInt32(timeSpan.TotalMinutes));
                                }
                                else
                                {
                                    settings.ActivitySetting.Add(what, Convert.ToInt32(tisDefault.TotalMinutes));
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // do nothing
                }
                HttpRuntime.Cache.Insert("UserActivitySettings", settings);
            }

            return settings;
        }

        public static Dictionary<string, bool> LoadRoleRight(int objectRoleRight)
        {
            XmlNodeList listRoles = LoadDataAccessConfig().SelectNodes("root/ObjectRoleRightMapping/Role[@Name != 'not set']");
            Dictionary<string, bool> dicRoles = new Dictionary<string, bool>(listRoles.Count);
            foreach (XmlElement itemRole in listRoles)
            {
                dicRoles.Add(itemRole.GetAttribute("Name"), IsRoleInRight(objectRoleRight, Convert.ToInt32(itemRole.InnerText)));
            }
            return dicRoles;
        }

        public static Dictionary<int, string> LoadObjectFeaturedValues()
        {
            XmlNodeList featuredValues = LoadDataAccessConfig().SelectNodes("root/ObjectFeaturedValues/ObjectFeaturedValue");
            GuiLanguage language = GuiLanguage.GetGuiLanguage("Shared");   
            Dictionary<int, string> featuredValuesTable = new Dictionary<int, string>(featuredValues.Count);
            foreach (XmlElement featuredValue in featuredValues)
            {
                featuredValuesTable.Add(int.Parse(featuredValue.GetAttribute("Value")), language.GetString("ObjectFeaturedValue" + featuredValue.GetAttribute("Value")));
            }
            return featuredValuesTable;
        }

        public static int GetRoleInt(string role)
        {
            return Convert.ToInt32(LoadDataAccessConfig().SelectSingleNode(string.Format("root/ObjectRoleRightMapping/Role[@Name != '{0}']", role)).InnerText);
        }

        private static bool IsRoleInRight(int objectRoleRight, int roleValue)
        {
            return (roleValue == (roleValue & objectRoleRight));
        }

        public static int GetRoleRightValue(Dictionary<string, bool> roleRight)
        {
            int i = 1;
            int r = 0;
            foreach (KeyValuePair<string, bool> kvp in roleRight)
            {
                if (kvp.Value)
                    r += i;
                i = i * 2;
            }
            return r;
        }

        public static List<Guid> GetDefaultUserFriends()
        {
            List<Guid> list = new List<Guid>();
            XmlNodeList users = LoadDataAccessConfig().SelectNodes("root/UserDefaultFriends/UserId");
            foreach (XmlElement user in users)
            {
                if (!string.IsNullOrEmpty(user.InnerText) && user.InnerText.IsGuid())
                    list.Add(user.InnerText.ToGuid());  
            }
            return list;
        }
    }

    public class UserAddressFields
    {
        public string Name { get; set; }
        public bool Must { get; set; }
        public bool Active { get; set; }
        public bool Overrule { get; set; }
    }

    internal class UserActivitySettings
    {
        public Dictionary<UserActivityWhat, int> ActivitySetting
        {
            get;
            set;
        }

        public int MaximalAmount
        {
            get;
            set;
        }
    }

    public class DefaultImages
    {
        private XmlNode xmlImages = null;
        private string strURLImageSmall = string.Empty;

        internal DefaultImages(XmlNode xmlParent)
        {
            if (xmlParent != null)
            {
                xmlImages = xmlParent;
                strURLImageSmall = xmlImages.SelectSingleNode("URLImageSmall").InnerText;
            }
        }

        public string GetURLImageSmall(int objectType)
        {
            string strFile = strURLImageSmall;
            if (xmlImages != null)
            {
                XmlNode xmlTemp = xmlImages.SelectSingleNode(objectType.ToString());
                if (xmlTemp != null && xmlTemp.InnerText.Length > 0)
                    strFile = xmlTemp.InnerText;
            }
            return strFile;
        }
    }

    public class ViewConfig
    {
        public int UserTimeSpanSecond { get; internal set; }
        public int IPTimeSpanSecond { get; internal set; }

        public ViewConfig(XmlNode xmlParent)
        {
            if (xmlParent != null)
            {
                XmlElement xmlNode = (XmlElement)xmlParent;
                TimeSpan tsp = xmlNode.SelectSingleNode("UserTimeSpan").InnerText.ToTimeSpan();
                UserTimeSpanSecond = (tsp.Hours * 3600) + (tsp.Minutes * 60) + tsp.Seconds;

                tsp = xmlNode.SelectSingleNode("IPTimeSpan").InnerText.ToTimeSpan();
                IPTimeSpanSecond = (tsp.Hours * 3600) + (tsp.Minutes * 60) + tsp.Seconds;
            }
        }
    }

    public class RatingConfig
    {
        private RatingType enuRatingType = RatingType.Standard;
        private int intMinPoint = 0;
        private int intMaxPoint = 0;
        private int intTimeSpanSecond = 0;
        private Dictionary<string, int> lstRoleMultiplier = new Dictionary<string, int>();
        private decimal decConsolidatedFactor = 1.1m;

        internal RatingConfig(XmlNode xmlParent)
        {
            if (xmlParent != null)
            {
                XmlElement xmlNode = (XmlElement)xmlParent;

                if (xmlNode.GetAttribute("Type") == RatingType.Standard.ToString())
                    enuRatingType = RatingType.Standard;
                else if (xmlNode.GetAttribute("Type") == RatingType.OneVersusOne.ToString())
                    enuRatingType = RatingType.OneVersusOne;
                else if (xmlNode.GetAttribute("Type") == RatingType.BestOfMany.ToString())
                    enuRatingType = RatingType.BestOfMany;

                intMinPoint = Convert.ToInt32(xmlNode.SelectSingleNode("MinPoint").InnerText);
                intMaxPoint = Convert.ToInt32(xmlNode.SelectSingleNode("MaxPoint").InnerText);
                TimeSpan tsp = xmlNode.SelectSingleNode("TimeSpan").InnerText.ToTimeSpan();
                intTimeSpanSecond = (tsp.Hours * 3600) + (tsp.Minutes * 60) + tsp.Seconds;

                foreach (XmlElement xmlItem in xmlNode.SelectNodes("RoleMultiplier"))
                {
                    string strKey = string.Format("{0}|{1}", xmlItem.GetAttribute("Role").ToLower().Trim(), xmlItem.GetAttribute("url").ToLower().Trim());
                    if (!lstRoleMultiplier.ContainsKey(strKey))
                    {
                        lstRoleMultiplier.Add(strKey, Convert.ToInt32(xmlItem.InnerText));
                    }
                }

                //#1.1.0.0
                XmlNode xmlFactor = xmlNode.SelectSingleNode("RatedConsolidatedFactor");
                if (xmlFactor != null)
                    decConsolidatedFactor = decimal.Parse(xmlFactor.InnerText, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("de-CH"));
            }
        }

        public RatingType RatingType
        {
            get { return enuRatingType; }
        }

        public int MinPoint
        {
            get { return intMinPoint; }
        }

        public int MaxPoint
        {
            get { return intMaxPoint; }
        }

        public int TimeSpanSecond
        {
            get { return intTimeSpanSecond; }
        }

        public int GetRoleMultiplier(string userRoles, string url)
        {
            int intMax = -1;
            userRoles = userRoles.ToLower();
            foreach (string key in lstRoleMultiplier.Keys)
            {
                string[] strPart = key.Split('|');
                if (userRoles.IndexOf(strPart[0]) > -1)
                {
                    if (lstRoleMultiplier[key] > intMax)
                    {
                        if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(url, strPart[1], Microsoft.VisualBasic.CompareMethod.Text))
                        {
                            intMax = lstRoleMultiplier[key];
                        }
                    }
                }
            }

            if (intMax < 0)
                return 1;
            else
                return intMax;
        }

        //#1.1.0.0
        public decimal GetConsolidatedFactor()
        {
            return decConsolidatedFactor;
        }
    }
}