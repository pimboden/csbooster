//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//             #1.2.0.0    23.01.2008 / PT   QuickLoad (SQL) anpassen / Objekttypen erweitert 
//******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using _4screen.CSB.Common;
using System.Text.RegularExpressions;

namespace _4screen.CSB.DataAccess.Data
{
    internal class QuickCacheHandler
    {
        private bool blnIgnoreCache = false;
        private string strKey = string.Empty;
        private CacheItemPriority enuItemPriority = CacheItemPriority.Default;
        private int? intAlternateCacheMinutes = null;

        public QuickCacheHandler(string CacheKey)
        {
            strKey = CacheKey;
            ItemPriority = System.Web.Caching.CacheItemPriority.BelowNormal;
        }

        public QuickCacheHandler(Business.QuickParameters paras, string spezKey)
        {
            if (paras.IgnoreCache == null)
                blnIgnoreCache = false;
            else
                blnIgnoreCache = paras.IgnoreCache.Value;

            strKey = paras.ToString();
            if (!string.IsNullOrEmpty(spezKey))
                strKey += string.Format("-SK{0}", spezKey);

            // Overwrite caching time
            if (paras.SortBy == QuickSort.Random)
                intAlternateCacheMinutes = 1;
            if (paras.CachingTimeInMinutes.HasValue)
                intAlternateCacheMinutes = paras.CachingTimeInMinutes.Value;

            if (!string.IsNullOrEmpty(paras.Nickname) || !string.IsNullOrEmpty(paras.Title) || !string.IsNullOrEmpty(paras.Description))
            {
                //The probability for the same strings searched is low..
                ItemPriority = System.Web.Caching.CacheItemPriority.Low;
            }
        }

        public CacheItemPriority ItemPriority
        {
            get { return enuItemPriority; }
            set { enuItemPriority = value; }
        }

        public int? AlternateCacheMinutes
        {
            get { return intAlternateCacheMinutes; }
            set { intAlternateCacheMinutes = value; }
        }

        public bool IgnoreCache
        {
            get { return blnIgnoreCache; }
        }

        public string Key
        {
            get { return strKey; }
        }

        public bool Insert(object obj)
        {
            return Insert(obj, false);
        }

        public bool Insert(object obj, bool withFileDependency)
        {
            TimeSpan tisLifeTime = GetCacheingTime(Business.DataAccessConfiguration.GetDefaultCachingTime());

            string strFile = null;
            if (withFileDependency)
                strFile = string.Format(@"{0}\{1}.csb", GetChachingPath(), strKey);

            if (!string.IsNullOrEmpty(strFile) && File.Exists(strFile))
                HttpRuntime.Cache.Insert(strKey, obj, new CacheDependency(strFile), Cache.NoAbsoluteExpiration, tisLifeTime, ItemPriority, null);
            else
                HttpRuntime.Cache.Insert(strKey, obj, null, DateTime.Now.Add(tisLifeTime), Cache.NoSlidingExpiration, ItemPriority, null);

            return true;
        }

        public object Get()
        {
            if (!blnIgnoreCache)
                return HttpRuntime.Cache[strKey];
            else
                return null;
        }

        public static void RemoveCache(int objectType, Guid? userIDLogedIn)
        {
            // wird aufgerufen wenn ein neues Objekt eingefügt wird
            if (HttpContext.Current != null)
            {
                string strUserID = "-XX";
                if (userIDLogedIn.HasValue && userIDLogedIn.Value != Constants.ANONYMOUS_USERID.ToGuid())
                    strUserID = string.Concat("-X", userIDLogedIn);

                string strStart = string.Format("OT{0}-", (int)objectType);
                List<string> listKey = new List<string>(20);
                foreach (DictionaryEntry item in HttpRuntime.Cache)
                {
                    if (item.Key.ToString().StartsWith(strStart))
                    {
                        if (item.Key.ToString().IndexOf(strUserID) > 3)
                            listKey.Add(item.Key.ToString());
                    }
                }

                foreach (string strKey in listKey)
                {
                    HttpRuntime.Cache.Remove(strKey);
                }
            }
        }

        public static void RemoveCache(string pattern)
        {
            if (!string.IsNullOrEmpty(pattern) && HttpContext.Current != null)
            {
                List<string> listKey = new List<string>(20);
                foreach (DictionaryEntry item in HttpRuntime.Cache)
                {
                    if (Regex.IsMatch(item.Key.ToString(), pattern))
                    {
                        listKey.Add(item.Key.ToString());
                    }
                }

                foreach (string strKey in listKey)
                {
                    HttpRuntime.Cache.Remove(strKey);
                }
            }
        }

        private TimeSpan GetCacheingTime(string time)
        {
            if (intAlternateCacheMinutes != null)
                return new TimeSpan(0, intAlternateCacheMinutes.Value, 0);
            else
                return Helper.ConvertFromString(time);
        }

        private string GetDataAccessCachePath()
        {
            return string.Format(@"{0}Configurations\DataAccessCache.config", WebRootPath.Instance.ToString());
        }

        public string GetChachingPath()
        {
            object objCachingPath = HttpRuntime.Cache["DataAccessCachingPath"];
            if (objCachingPath == null)
                return string.Empty;
            else
                return objCachingPath.ToString();
        }
    }
}