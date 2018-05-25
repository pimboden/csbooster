// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
namespace _4screen.CSB.Common
{
    public class SiteContext
    {
        public UserDataContext Udc { get; set; }

        public string MediaDomainName { get; set; }

        public string SiteName { get; set; }

        public string SiteURL { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}