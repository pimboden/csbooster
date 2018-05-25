// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace _4screen.CSB.Common
{
    public class SiteConfig
    {
        private static Dictionary<string, string> _Languages = null;
        private static Dictionary<string, string> _NeutralLanguages = null;
        private static string _DefaultNeutralLanguage = string.Empty;
        private static string _SiteName = string.Empty;
        private static string _HostName = string.Empty;
        private static string _SiteUrl = string.Empty;
        private static string _SiteImagesPath = string.Empty;
        private static string _MediaDomainName = string.Empty;
        private static bool? _UsePopupWindows = null;

        public SiteConfig()
        {
        }

        public static Dictionary<string, string> NeutralLanguages
        {
            get
            {
                if (_NeutralLanguages == null)
                {
                    _NeutralLanguages = new Dictionary<string, string>();
                    string strLangs = GetSetting("AvailableLanguages");
                    string[] AvailableLangs = strLangs.Split(',');
                    System.Globalization.CultureInfo cu;
                    foreach (string strCurLang in AvailableLangs)
                    {
                        cu = new System.Globalization.CultureInfo(strCurLang);
                        if (cu.Parent != null && !_NeutralLanguages.ContainsKey(cu.Parent.Name))
                        {
                            _NeutralLanguages.Add(cu.Parent.Name, cu.Parent.NativeName);
                        }
                    }
                }
                return _NeutralLanguages;
            }
        }
        public static Dictionary<string, string> Languages
        {
            get
            {
                if (_Languages == null)
                {
                    _Languages = new Dictionary<string, string>();
                    string strLangs = GetSetting("AvailableLanguages");
                    string[] AvailableLangs = strLangs.Split(',');
                    System.Globalization.CultureInfo cu;
                    foreach (string strCurLang in AvailableLangs)
                    {
                        cu = new System.Globalization.CultureInfo(strCurLang);
                        _Languages.Add(cu.Name, cu.NativeName);
                    }
                }
                return _Languages;
            }
        }

        public static string DefaultLanguage
        {
            get { return GetSetting("DefaultLanguage"); }
        }

        public static string DefaultNeutralLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(_DefaultNeutralLanguage))
                {
                    System.Globalization.CultureInfo cu = new System.Globalization.CultureInfo(DefaultLanguage);
                      if (cu.Parent != null)
                      {
                          _DefaultNeutralLanguage = cu.Parent.Name;
                      }
                }
                return _DefaultNeutralLanguage;
            }
        }

        public static string GetSetting(string key)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            catch
            {
                throw new Exception(string.Format("No {0} setting in the web.config.", key));
            }
        }

        public static string SiteName
        {
            get
            {
                if (string.IsNullOrEmpty(_SiteName))
                {
                    _SiteName = ConfigurationManager.AppSettings["SiteName"];
                }
                return _SiteName;
            }
        }

        public static string HostName
        {
            get
            {
                if (_HostName.Length == 0)
                {
                    _HostName = ConfigurationManager.AppSettings["HostName"];
                }
                return _HostName;
            }
        }

        public static string SiteURL
        {
            get
            {
                if (_SiteUrl.Length == 0)
                {
                    _SiteUrl = string.Format("{0}", ConfigurationManager.AppSettings["HostName"]);
                }
                return _SiteUrl;
            }
        }

        public static string MediaDomainName
        {
            get
            {
                if (_MediaDomainName.Length == 0)
                {
                    _MediaDomainName = ConfigurationManager.AppSettings["MediaDomainName"];
                }
                return _MediaDomainName;
            }
        }

        public static string SiteImagesPath
        {
            get
            {
                if (_SiteImagesPath.Length == 0)
                {
                    _SiteImagesPath = "/Library/Images/Layout";
                }
                return _SiteImagesPath;
            }
        }

        public static bool UsePopupWindows
        {
            get
            {
                if (!_UsePopupWindows.HasValue)
                {
                    _UsePopupWindows = bool.Parse(ConfigurationManager.AppSettings["UsePopupWindows"]);
                }
                return _UsePopupWindows.Value;
            }
        }



        public static SiteContext GetSiteContext(UserProfile profile)
        {
            SiteContext SiteContext = new SiteContext();
            SiteContext.MediaDomainName = SiteConfig.MediaDomainName;
            SiteContext.SiteName = SiteConfig.SiteName;
            SiteContext.SiteURL = SiteConfig.SiteURL;
            SiteContext.Udc = UserDataContext.GetUserDataContext();
            SiteContext.UserProfile = profile;
            return SiteContext;
        }
    }
}