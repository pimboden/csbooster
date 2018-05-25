// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public class SystemSettings
    {
        private Dictionary<string, string> LoadFromDB()
        {
            CSBooster_DataContext cdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            Dictionary<string, string> settings = new Dictionary<string, string>();
            foreach (var settingsEntry in cdc.hitbl_Settings_SETs)
            {
                settings.Add(settingsEntry.SET_Key, settingsEntry.SET_Value);
            }
            return settings;
        }

        private string GetCachedValue(string key)
        {
            Dictionary<string, string> settings = (Dictionary<string, string>)HttpRuntime.Cache["CSBoosterSettings"];
            if (settings == null)
            {
                settings = LoadFromDB();
                HttpRuntime.Cache.Insert("CSBoosterSettings", settings);
            }
            return settings[key];
        }

        public void SetValue(string key, object value)
        {
            CSBooster_DataContext cdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            cdc.hitbl_Settings_SETs.Single(x => x.SET_Key == key).SET_Value = value.ToString();
            cdc.SubmitChanges();

            Dictionary<string, string> settings = (Dictionary<string, string>)HttpRuntime.Cache["CSBoosterSettings"];
            if (settings != null)
            {
                settings[key] = value.ToString();
            }
        }

        public string GetString(string key)
        {
            return GetCachedValue(key);
        }

        public bool GetBoolean(string key)
        {
            return bool.Parse(GetCachedValue(key));
        }

        public DateTime GetDateTime(string key)
        {
            return DateTime.Parse(GetCachedValue(key));
        }

        public int GetInt(string key)
        {
            return int.Parse(GetCachedValue(key));
        }

        public float GetFloat(string key)
        {
            return float.Parse(GetCachedValue(key));
        }
    }
}
