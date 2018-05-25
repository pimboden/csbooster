// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class MessageReceiver
    {
        public Guid? UserId { get; set; }
        public DataObjectUser User { get; set; }
        public FriendType? FriendType { get; set; }
        public string EmailAddress { get; set; }
    }
}
