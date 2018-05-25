// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web;

namespace _4screen.CSB.Common
{
    public class PageInfo
    {
        private Guid? userId;
        private Guid? communityId;
        private Guid? effectiveCommunityId;

        private static PageInfo GetInstance()
        {
            PageInfo pageInfo = HttpContext.Current.Items["PageInfo"] as PageInfo;
            if (pageInfo == null)
            {
                pageInfo = new PageInfo();
                HttpContext.Current.Items["PageInfo"] = pageInfo;
            }
            return pageInfo;
        }

        public static Guid? UserId
        {
            get { return GetInstance().userId; }
            set { GetInstance().userId = value; }
        }

        public static Guid? CommunityId
        {
            get { return GetInstance().communityId; }
            set { GetInstance().communityId = value; }
        }

        public static Guid? EffectiveCommunityId
        {
            get { return GetInstance().effectiveCommunityId; }
            set { GetInstance().effectiveCommunityId = value; }
        }
    }
}
