using System;
using _4screen.CSB.Common;
using _4screen.CSB.Extensions.Data;

namespace _4screen.CSB.Extensions.Business
{
    public class TrackingManager
    {
        public static void TrackObjectEvent(int objectType, Guid? objectID, TrackRule action, string actionParms)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc != null && udc.CurrentContext != null)
            {
                if (udc.IsAuthenticated || CacheData.TrackThisSession(udc.UserIP, udc.SystemVersion))
                {
                    string rule = string.Format("{0}_{1}", objectType, action.ToString());
                    if (CacheData.TrackThisObjectEvent(rule, udc.UserRole))
                    {
                        DLTrackingManager objData = new DLTrackingManager();
                        objData.DoTrackObjectEvent(new SessionLogParams(udc), objectType, objectID, action.ToString(), actionParms);
                    }
                }
            }
        }

        public static void TrackEventPage(Guid? communityID, int? objectType, bool isPostback, LogSitePageType logSitePageType)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc != null && udc.CurrentContext != null)
            {
                if (udc.IsAuthenticated || CacheData.TrackThisSession(udc.UserIP, udc.SystemVersion))
                {
                    string pageURL = udc.CurrentContext.Request.Url.LocalPath;
                    if (pageURL.Length > 0)
                    {
                        if (CacheData.TrackThisPageEvent(pageURL, TrackRule.Viewed.ToString(), udc.UserRole))
                        {
                            string refererURL = udc.CurrentContext.Request.ServerVariables["HTTP_REFERER"] ?? string.Empty;
                            string queryString = udc.CurrentContext.Request.QueryString.ToString();

                            DLTrackingManager objData = new DLTrackingManager();
                            objData.DoTrackPageEvent(new SessionLogParams(udc), pageURL.CropString(500), queryString.CropString(500), refererURL.CropString(500), isPostback, logSitePageType, communityID, objectType, null, null);
                        }
                    }
                }
            }
        }

        public static void TrackEventSearch(string searchWord, int searchResult, int objectType)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (udc != null && udc.CurrentContext != null)
            {
                if (udc.IsAuthenticated || CacheData.TrackThisSession(udc.UserIP, udc.SystemVersion))
                {
                    string pageURL = udc.CurrentContext.Request.Url.LocalPath;
                    if (pageURL.Length > 0)
                    {
                        if (CacheData.TrackThisPageEvent(pageURL, TrackRule.Viewed.ToString(), udc.UserRole))
                        {
                            string refererURL = udc.CurrentContext.Request.ServerVariables["HTTP_REFERER"] ?? string.Empty;
                            string queryString = udc.CurrentContext.Request.QueryString.ToString();

                            DLTrackingManager objData = new DLTrackingManager();
                            objData.DoTrackSearchEvent(new SessionLogParams(udc), searchWord.CropString(200), searchResult, objectType);
                        }
                    }
                }
            }
        }


    }
}