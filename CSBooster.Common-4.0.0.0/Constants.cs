// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.Common
{
    public static class Constants
    {
        public const char TAG_DELIMITER = '¦';
        public const int ROLE_RIGHT_MAX_VALUE = 2147483647;
        public const string DEFAULT_USER_PASSWORD = "1-csb_delete_user%";
        public const string WIDGET_CONTAINER = "/UserControls/WidgetContainer.ascx";
        public const string LANGUAGEFILES_CONTAINER = "/Library/Language";
        public const string APPLICATION_NAME = "CSBooster";

        public const string META_GENERATOR = "sieme.net (%VERSION%) social network software / 4 screen AG /  www.sieme.net";

        public const string ANONYMOUS_USERID = "{84177e98-9f22-4827-b8d5-e09101a54837}";
        public const string ANONYMOUS_USERNAME = "ANONYMOUS";
        public const string ANONYMOUS_PROFILE_COMMUNITY_ID = "{edaee205-a0b4-4e69-a966-97a92b755e12}";
        public const string ADMIN_USERID = "{7b1be42f-3306-4f90-a2d7-ae9690bea7df}";
        public const string DEFAULT_COMMUNITY_ID = "{65498787-4a55-4fbf-9b02-621065192a73}";
        public const string DEFAULT_LAYOUTID = "{6775d09c-26ad-492d-8331-15ed8115e2dd}";
        public const string DEFAULT_THEME = "Default";
        public const string DEFAULT_LAYOUT = "Default";
        public const string LOGGEDINUSER_CACHE_KEY = "LOGGEDINUSERS";

        public const string SITEOBJECT_SECTION_CACHE_KEY = "SiteObjectSection";
        public const string SITELINK_SECTION_CACHE_KEY = "SiteLinkSection";
        public const string CUSTOMIZATION_SECTION_CACHE_KEY = "CustomizationSection";
        public const string WIDGET_SECTION_CACHE_KEY = "WidgetSection";
        public const string WIZARD_SECTION_CACHE_KEY = "WizardSection";
        public const string SECURITY_SECTION_CACHE_KEY = "SecuritySection";
        public const string OUTPUTTEMPALTES_SECTION_CACHE_KEY = "OutputTemplates";
        public static string MAP_SECTION_CACHE_KEY = "MapSection";

        public const string REGEX_EMAIL = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public const string REGEX_USERNAME = @"^[\s0-9A-Za-z_-]{3,32}$";
        public const string REGEX_URL = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        public const string TEXT_REPLACING_SITEURL = "<%SITE_URL%>";

        private static SiteLinkElementCollection links = new SiteLinkElementCollection();

        public static SiteLinkElementCollection Links
        {
            get { return links; }
        }

        static Constants()
        {
            links = SiteLinkSection.CachedInstance.SiteLinks;
        }

        // Default images
        public const string DEFIMG = "/Defmedia/DataObjectImageSmall.gif";

        public const string PRIMARY_COLOR_ICONS_PATH = "/Library/Images/User/sm/pc/";
        public const string SECONDARY_COLOR_ICONS_PATH = "/Library/Images/User/sm/sc/";
    }
}