// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserDataInfo
    {
        private int alertCount;
        private int unreadMessagesCount;
        private Guid userId;
        private int friendRequestCount;
        public int AlertCount
        {
            get { return alertCount; }

        }

        public int UnreadMessagesCount
        {
            get { return unreadMessagesCount; }
        }

        public int FriendRequestCount
        {
            get { return friendRequestCount; }
        }

        public UserDataInfo(Guid userId)
        {
            this.userId = userId;
            alertCount = Messages.GetAlertsUnreadCount(this.userId);
            unreadMessagesCount = Messages.GetInboxUnreadCount(this.userId, null);
            friendRequestCount = Messages.GetRequestUnreadCount(this.userId);
        }
    }
}