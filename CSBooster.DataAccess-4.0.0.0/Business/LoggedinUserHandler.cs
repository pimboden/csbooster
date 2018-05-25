// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class LoggedinUserHandler
    {
        private static object _SyncRoot = new object();
        private static TimeSpan TimeSpanUserOnline = DataAccessConfiguration.UserOnlineTimeGap();
        private static TimeSpan TimeSpanLastActivityDateUpdate = DataAccessConfiguration.LastActivityDateUpdateTimeGap();


        public static void DeleteUserLoggedIn(string userId)
        {
            lock (_SyncRoot)
            {
                Dictionary<string, DateTime> dicLoggedInUsers = HttpRuntime.Cache[Constants.LOGGEDINUSER_CACHE_KEY] as Dictionary<string, DateTime>;
                if (dicLoggedInUsers == null)
                {
                    dicLoggedInUsers = new Dictionary<string, DateTime>();
                }
                if (dicLoggedInUsers.Any(x => x.Key == userId))
                {
                    dicLoggedInUsers.Remove(userId);
                    Data.CSBooster_DataContext csb = new Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
                    DateTime expireDate = DateTime.Now.Subtract(TimeSpanUserOnline);
                    csb.hisp_aspnet_Profile_UpdateLastActivityDate(new Guid(userId), expireDate);
                }
                CheckUpdate(userId, dicLoggedInUsers);
                HttpRuntime.Cache[Constants.LOGGEDINUSER_CACHE_KEY] = dicLoggedInUsers;
            }
        }

        public static void InsertUpdateUserLoggedIn(string userId)
        {
            lock (_SyncRoot)
            {
                Dictionary<string, DateTime> dicLoggedInUsers = HttpRuntime.Cache[Constants.LOGGEDINUSER_CACHE_KEY] as Dictionary<string, DateTime>;
                if (dicLoggedInUsers == null)
                {
                    dicLoggedInUsers = new Dictionary<string, DateTime>();
                }
                if (!dicLoggedInUsers.Any(x => x.Key == userId))
                {
                    DateTime now = DateTime.Now;
                    dicLoggedInUsers.Add(userId, now);
                    //Update The DB
                    Data.CSBooster_DataContext csb = new Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
                    csb.hisp_aspnet_Profile_UpdateLastActivityDate(new Guid(userId), now);
                }
                CheckUpdate(userId, dicLoggedInUsers);
                HttpRuntime.Cache[Constants.LOGGEDINUSER_CACHE_KEY] = dicLoggedInUsers;
            }
        }

        private static void CheckUpdate(string userId, Dictionary<string, DateTime> dicLoggedInUsers)
        {
            // TODO: Find reason for exception
            try
            {
                //Check when was the Last Insert made to the 
                var dt = (from a in dicLoggedInUsers.Where(x => x.Key == userId) select a.Value).SingleOrDefault();

                if (dt != null && dt.Add(TimeSpanLastActivityDateUpdate) <= DateTime.Now)
                {
                    Data.CSBooster_DataContext csb = new Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
                    DateTime now = DateTime.Now;
                    csb.hisp_aspnet_Profile_UpdateLastActivityDate(new Guid(userId), now);
                    dicLoggedInUsers[userId] = now;
                }
                //By the way Delete all expired Users
                var allToDelete = from allUsers in dicLoggedInUsers.Where(x => x.Value.Add(TimeSpanUserOnline) <= DateTime.Now) select allUsers.Key;

                foreach (string userToDelete in allToDelete)
                {
                    dicLoggedInUsers.Remove(userToDelete);
                }
            }
            catch
            {
            }
        }
    }
}