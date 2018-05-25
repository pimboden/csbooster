// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web;

namespace _4screen.CSB.Common
{
    /// <summary>
    /// Summary description for ActionValidator
    /// </summary>
    public static class ActionValidator
    {
        private const int DURATION = 10; // 10 min period

        /*
       * Type of actions and their maximum value per period
       * 
       */

        public enum ActionTypeEnum
        {
            FirstVisit = 100, //Not Needed since user has to lo gin to creat a Page. 
            ReVisit = 5000, // Welcome to revisit as many times as user likes
            Postback = 5000, // Not must of a problem for us
            AddNewWidget = 100,
            // This check is mad twice... one on the Webservice and one on the psotback event.. 100 =50 Posible AddNewWidgets Actions 
            AddNewPage = 50
        }

        public class HitInfo
        {
            public int Hits = 0;
            public DateTime ExpiresAt = DateTime.Now.AddMinutes(DURATION);
        }

        public static bool IsValid(ActionTypeEnum actionType)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Browser.Crawler)
                return false;

            string key = actionType.ToString() + context.Request.UserHostAddress;

            var hit = (HitInfo) (context.Cache[key] ?? new HitInfo());

            if (hit.Hits > (int) actionType)
                return false;
            else
                hit.Hits++;

            if (hit.Hits == 1)
                context.Cache.Add(key, hit, null, DateTime.Now.AddMinutes(DURATION), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);

            return true;
        }
    }
}