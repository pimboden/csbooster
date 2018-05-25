using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace _4screen.CSB.Extensions.Data
{
    internal static class CacheData
    {
        #region Page Tracking

        #endregion

        private static string PageTrackingFile = string.Format(@"{0}Configurations\NotTrackPageEvents.rule", HttpContext.Current.Request.PhysicalApplicationPath);

        // verification, if this Page Event for the given Role should be tracked
        public static bool TrackThisPageEvent(string pageUrl, string trackEvent, string userRoles)
        {
            Dictionary<string, string> dicNotTrackPageEvents = HttpRuntime.Cache["NotTrackPageEvents"] as Dictionary<string, string>;
            if (dicNotTrackPageEvents == null)
            {
                dicNotTrackPageEvents = LoadCacheNotTrackPageEvents(false);
            }

            return TrackPage(dicNotTrackPageEvents, pageUrl, trackEvent, userRoles);
        }

        private static bool TrackPage(Dictionary<string, string> dicNotTrackPageEvents, string pageUrl, string trackEvent, string userRoles)
        {
            pageUrl = pageUrl.ToLower();
            trackEvent = trackEvent.ToLower();
            userRoles = userRoles.ToLower();

            bool blnRet = true;
            //Prepare Domain Info
            //-----

            // http://rk-moss2007-sta/Pages/ProfileEditing.aspx
            string strDomain = HttpContext.Current.Request.Url.AbsoluteUri;

            int intSlash = strDomain.IndexOf("/", 8);
            if (intSlash > -1)
                // http://rk-moss2007-sta
                // http://rk-moss2007-sta:1234
                strDomain = strDomain.Substring(0, intSlash);
            strDomain = string.Concat("http://", HttpContext.Current.Request.Url.Host);

            strDomain += HttpContext.Current.Request.ApplicationPath;
            strDomain = strDomain.ToLower();
            if (pageUrl.IndexOf("#") > -1)
                pageUrl = pageUrl.Remove(pageUrl.IndexOf("#"));

            string BestKey = string.Empty;
            int intBestConformenceLength = 0;
            foreach (string key in dicNotTrackPageEvents.Keys)
            {
                //Prepare Url
                string strURL = key.Substring(0, key.IndexOf('|')).ToLower();
                string strTrackEvent = key.Substring(key.IndexOf('|') + 1);
                if ((strTrackEvent == "*" || strTrackEvent == trackEvent) && (userRoles.IndexOf(dicNotTrackPageEvents[key]) > -1 || dicNotTrackPageEvents[key].IndexOf(userRoles) > -1 || dicNotTrackPageEvents[key] == "*"))
                {
                    if (strURL.StartsWith("/"))
                        strURL = strDomain + strURL;

                    //Index Counter

                    if (pageUrl.Equals(strURL))
                    {
                        BestKey = key;
                        break;
                    }
                    else if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(pageUrl, strURL, Microsoft.VisualBasic.CompareMethod.Text))
                    {
                        if (intBestConformenceLength < strURL.Length)
                        {
                            intBestConformenceLength = strURL.Length;
                            BestKey = key;
                        }
                    }
                }
            }
            if (BestKey.Length > 0)
            {
                blnRet = false;
            }
            else
            {
                blnRet = true;
            }
            return blnRet;
        }

        // load the cache object for NotTrackPageEvents
        private static Dictionary<string, string> LoadCacheNotTrackPageEvents(bool forceLoad)
        {
            if (forceLoad && HttpRuntime.Cache["NotTrackPageEvents"] != null)
                HttpRuntime.Cache.Remove("NotTrackPageEvents");

            Dictionary<string, string> collRules = new Dictionary<string, string>();
            // load tracking rules from config file
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.IgnoreWhitespace = true;

            string strFile = PageTrackingFile;
            XmlReader xmlr = null;
            try
            {
                xmlr = XmlReader.Create(strFile, xmlSet);
                while (xmlr.Read())
                {
                    if (xmlr.Depth == 1 && xmlr.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            collRules.Add(String.Concat(xmlr.GetAttribute("url").ToLower(), "|", xmlr.GetAttribute("event").ToLower()), xmlr.GetAttribute("roles").ToLower());
                        }
                        catch
                        {
                            //ignore... if the Xml has 2 times the same entry
                        }
                    }
                }
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                HttpRuntime.Cache.Insert("NotTrackPageEvents", collRules, dep);
                return collRules;
            }
            finally
            {
                if (xmlr != null)
                {
                    xmlr.Close();
                }
            }
        }

        #region Object Tracking

        #endregion

        // verification, if this object Event for the given Role should be tracked
        public static bool TrackThisObjectEvent(string trackingRule, string userRoles)
        {
            trackingRule = trackingRule.ToLower();
            userRoles = userRoles.ToLower();

            Dictionary<string, string> dicNotTrackObjectEvents = HttpRuntime.Cache["NotTrackObjectEvents"] as Dictionary<string, string>;
            if (dicNotTrackObjectEvents == null)
                dicNotTrackObjectEvents = LoadCacheNotTrackObjectEvents(false);

            return TrackObject(dicNotTrackObjectEvents, trackingRule, userRoles);
        }

        private static bool TrackObject(Dictionary<string, string> dicNotTrackObjectEvents, string trackingRule, string userRoles)
        {
            bool blnRet = true;
            foreach (string key in dicNotTrackObjectEvents.Keys)
            {
                if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(trackingRule, key, Microsoft.VisualBasic.CompareMethod.Text))
                {
                    if (userRoles.IndexOf(dicNotTrackObjectEvents[key]) > -1 || dicNotTrackObjectEvents[key].IndexOf(userRoles) > -1 || dicNotTrackObjectEvents[key] == "*")
                    {
                        blnRet = false;
                        break;
                    }
                }
            }

            return blnRet;
        }

        // load the cache object for NotTrackObjectEvents
        private static Dictionary<string, string> LoadCacheNotTrackObjectEvents(bool forceLoad)
        {
            if (forceLoad && HttpRuntime.Cache["NotTrackObjectEvents"] != null)
                HttpRuntime.Cache.Remove("NotTrackObjectEvents");

            // load tracking rules from config file
            Dictionary<string, string> collRules = new Dictionary<string, string>();
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.IgnoreWhitespace = true;

            string strFile = string.Format(@"{0}Configurations\NotTrackObjectEvents.rule", HttpContext.Current.Request.PhysicalApplicationPath);
            XmlReader xmlr = null;
            try
            {
                xmlr = XmlReader.Create(strFile, xmlSet);
                while (xmlr.Read())
                {
                    if (xmlr.Depth == 1 && xmlr.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            collRules.Add(xmlr.GetAttribute("rule").ToLower(), xmlr.GetAttribute("roles").ToLower());
                        }
                        catch
                        {
                            //ignore... if the Xml has 2 times the same entry
                        }
                    }
                }
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                HttpRuntime.Cache.Insert("NotTrackObjectEvents", collRules, dep);
                return collRules;
            }
            finally
            {
                if (xmlr != null)
                {
                    xmlr.Close();
                }
            }
        }

        public static bool TrackThisSession(string ip, string system)
        {
            List<LogSession> list = HttpRuntime.Cache["NotTrackSessions"] as List<LogSession>;
            if (list == null)
                list = LoadChacheNotTrackSessions(false);

            foreach (LogSession item in list)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.IP) && string.IsNullOrEmpty(item.System))
                    {
                        if (!string.IsNullOrEmpty(ip) && Regex.IsMatch(ip, item.IP))
                            return false;
                    }
                    else if (string.IsNullOrEmpty(item.IP) && !string.IsNullOrEmpty(item.System))
                    {
                        if (!string.IsNullOrEmpty(system) && Regex.IsMatch(system, item.System))
                            return false;
                    }
                    else if (!string.IsNullOrEmpty(item.IP) && !string.IsNullOrEmpty(item.System))
                    {
                        if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(system) && Regex.IsMatch(ip, item.System) && Regex.IsMatch(system, item.System))
                            return false;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return true;
        }

        private static List<LogSession> LoadChacheNotTrackSessions(bool forceLoad)
        {
            if (forceLoad && HttpRuntime.Cache["NotTrackSessions"] != null)
                HttpRuntime.Cache.Remove("NotTrackSessions");

            // load tracking rules from config file
            List<LogSession> collRules = new List<LogSession>();
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.IgnoreWhitespace = true;

            string strFile = string.Format(@"{0}Configurations\NotTrackSessions.rule", HttpContext.Current.Request.PhysicalApplicationPath);
            if (File.Exists(strFile))
            {
                XmlReader xmlr = null;
                try
                {
                    xmlr = XmlReader.Create(strFile, xmlSet);
                    while (xmlr.Read())
                    {
                        if (xmlr.Depth == 1 && xmlr.NodeType == XmlNodeType.Element)
                        {
                            collRules.Add(new LogSession { IP = xmlr.GetAttribute("ip"), System = xmlr.GetAttribute("system") });    
                        }
                    }
                    System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                    HttpRuntime.Cache.Insert("NotTrackSessions", collRules, dep);
                    return collRules;
                }
                finally
                {
                    if (xmlr != null)
                    {
                        xmlr.Close();
                    }
                }
            }
            return collRules;
        }

        #region IncentivePoints

        #endregion

        private static string IncentivePointsFile = string.Format(@"{0}Configurations\IncentivePoints.rule", HttpContext.Current.Request.PhysicalApplicationPath);

        // verification, if this Page Event for the given Role should be tracked
        public static int TrackIncentivePoints(string incentivePointRules, string pageUrl, string userRoles, out string description, out string timeSpan, out string pointType)
        {
            Dictionary<string, string> dicIncentivePointsEvents = HttpRuntime.Cache["IncentivePointsEvents"] as Dictionary<string, string>;
            if (dicIncentivePointsEvents == null)
                dicIncentivePointsEvents = LoadCacheIncentivePointsEvents(false);

            return AddIncentivePoint(dicIncentivePointsEvents, incentivePointRules, pageUrl, userRoles, out description, out timeSpan, out pointType);
        }

        private static int AddIncentivePoint(Dictionary<string, string> dicIncentivePointsEvents, string incentivePointRules, string pageUrl, string userRoles, out string description, out string timeSpan, out string pointType)
        {
            int intRet = 0;
            description = string.Empty;
            timeSpan = string.Empty;
            pointType = string.Empty;
            pageUrl = pageUrl.ToLower();
            userRoles = userRoles.ToLower();
            incentivePointRules = incentivePointRules.ToLower();
            string BestKey = string.Empty;

            string strKeyToMatch = String.Format("{0}|{1}|{2}", incentivePointRules, pageUrl, userRoles);

            if (dicIncentivePointsEvents.ContainsKey(strKeyToMatch))
            {
                BestKey = strKeyToMatch;
            }
            else
            {
                //Prepare Domain Info
                //-----

                // http://rk-moss2007-sta/Pages/ProfileEditing.aspx
                string strDomain = HttpContext.Current.Request.Url.AbsoluteUri;

                int intSlash = strDomain.IndexOf("/", 8);
                if (intSlash > -1)
                    // http://rk-moss2007-sta
                    // http://rk-moss2007-sta:1234
                    strDomain = strDomain.Substring(0, intSlash);
                strDomain = string.Concat("http://", HttpContext.Current.Request.Url.Host);

                strDomain += HttpContext.Current.Request.ApplicationPath;
                strDomain = strDomain.ToLower();

                ArrayList alBestKeys = new ArrayList();
                //First find Keys that have roles that match
                foreach (string key in dicIncentivePointsEvents.Keys)
                {
                    ///key.Split('|')[2]; --> roles
                    string strRoles = key.Split('|')[2].ToLower();
                    if (strRoles.IndexOf(userRoles) != -1 || userRoles.IndexOf(strRoles) != -1 || strRoles == "*")
                    {
                        alBestKeys.Add(key);
                    }
                }
                //From this subset find the ones where the IncentivePointRules also matches
                ArrayList alBestKeys2 = new ArrayList();
                foreach (string key in alBestKeys)
                {
                    ///key.Split('|')[0]; --> IncentivePointRules
                    if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(incentivePointRules, key.Split('|')[0], Microsoft.VisualBasic.CompareMethod.Text))
                    {
                        alBestKeys2.Add(key);
                    }
                }
                //From this subset find  url that match

                ArrayList alBestKeys3 = new ArrayList();
                if (pageUrl.IndexOf("#") > -1)
                    pageUrl = pageUrl.Remove(pageUrl.IndexOf("#"));
                foreach (string key in alBestKeys2)
                {
                    ///key.Split('|')[1]; --> URL
                    string strURL = key.Split('|')[1].ToLower();
                    if (strURL.StartsWith("/"))
                        strURL = strDomain + strURL;


                    if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(pageUrl, strURL, Microsoft.VisualBasic.CompareMethod.Text))
                    {
                        alBestKeys3.Add(key);
                    }
                }
                //now the best rule macth is the winner

                int intBestConformenceLength = 0;
                foreach (string key in alBestKeys2)
                {
                    //key.Split('|')[0]; --> IncentivePointRules
                    string strIncentivePointRules = key.Split('|')[0].ToLower();

                    if (incentivePointRules.Equals(strIncentivePointRules))
                    {
                        BestKey = key;
                        break;
                    }
                    else if (Microsoft.VisualBasic.CompilerServices.StringType.StrLike(incentivePointRules, strIncentivePointRules, Microsoft.VisualBasic.CompareMethod.Text))
                    {
                        if (intBestConformenceLength < strIncentivePointRules.Length)
                        {
                            BestKey = key;
                            intBestConformenceLength = strIncentivePointRules.Length;
                        }
                    }
                }
                if (BestKey.Length > 0 && BestKey != strKeyToMatch)
                {
                    //Insert the value of the best key as the new key's value... so the next time the same key appears, the matching takes less time;
                    lock (dicIncentivePointsEvents)
                    {
                        try
                        {
                            dicIncentivePointsEvents.Add(strKeyToMatch, dicIncentivePointsEvents[BestKey]);
                        }
                        catch
                        {
                            //the key may have just been inserted by an other parallel call... then ignore the error
                        }
                    }
                    BestKey = strKeyToMatch;
                }
            }

            if (BestKey.Length > 0)
            {
                string[] strValues = dicIncentivePointsEvents[BestKey].Split('|');
                timeSpan = strValues[1];
                pointType = strValues[2];
                description = strValues[3];
                intRet = Convert.ToInt32(strValues[0]);
            }
            return intRet;
        }

        // load the cache object for NotTrackPageEvents
        private static Dictionary<string, string> LoadCacheIncentivePointsEvents(bool forceLoad)
        {
            if (forceLoad && HttpRuntime.Cache["IncentivePointsEvents"] != null)
                HttpRuntime.Cache.Remove("IncentivePointsEvents");

            Dictionary<string, string> collRules = new Dictionary<string, string>();
            // load tracking rules from config file
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.IgnoreWhitespace = true;

            string strFile = IncentivePointsFile;
            XmlReader xmlr = null;
            bool nodeRead = false;
            ;
            try
            {
                xmlr = XmlReader.Create(strFile, xmlSet);
                while (true)
                {
                    if (!nodeRead)
                    {
                        if (!xmlr.Read())
                        {
                            break;
                        }
                    }
                    if (xmlr.Depth == 1 && xmlr.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            string strKey = String.Format("{0}|{1}|{2}", xmlr.GetAttribute("rule").ToLower(), xmlr.GetAttribute("url").ToLower(), xmlr.GetAttribute("roles").ToLower());
                            string strValue = string.Format("{0}|{1}|{2}|{3}", xmlr.GetAttribute("PointsToAdd"), xmlr.GetAttribute("TimespanMinutes"), xmlr.GetAttribute("PointType"), xmlr.ReadElementContentAsString());
                            collRules.Add(strKey, strValue);
                            //ReadElementContentAsString reads past the node... goes to next node!
                            nodeRead = true;
                        }
                        catch
                        {
                            //ignore... if the Xml has 2 times the same entry
                        }
                    }
                    else
                    {
                        nodeRead = false;
                    }
                }
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                HttpRuntime.Cache.Insert("IncentivePointsEvents", collRules, dep);
                return collRules;
            }
            finally
            {
                if (xmlr != null)
                {
                    xmlr.Close();
                }
            }
        }
    }

    internal class LogSession
    {
        internal string IP
        {
            get;
            set;
        }

        internal string System
        {
            get;
            set;
        }
    }

}