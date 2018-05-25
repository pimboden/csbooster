//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public static class FriendHandler
    {
        public static Dictionary<int, string> GetFriendTypes(string langCode)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            return fHanlder.GetFriendTypes(langCode);
        }


        public static void TransferFriendAsCommunityMember(Guid communityID, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.TransferFriendAsCommunityMember(friendId, communityID);

            //SPs.HispUserFriendTransferAsCommunityMember(friendId, communityID);
        }

        public static bool IsFriend(Guid userId, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            return fHanlder.IsFriend(userId, friendId);
            //return Convert.ToBoolean(SPs.HispUserFriendIsFriend(userId, friendId).ExecuteScalar());
        }

        public static bool IsBlocked(Guid userId, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            return fHanlder.IsBlocked(userId, friendId);
            //return Convert.ToBoolean(SPs.HispUserFriendIsBlocked(userId, friendId).ExecuteScalar());
        }

        public static void Save(Guid userId, Guid friendId, bool blockFriend, int friendType, int allowBirthdayNotification)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.Save(userId, friendId, Convert.ToInt32(blockFriend), friendType, allowBirthdayNotification);
            Business.UserActivities.InsertFriendship(userId, null, friendId);  
            //SPs.HispUserFriendSaveFriend(userId, friendId, Convert.ToInt32(blockFriend), friendType).ExecuteScalar();
        }

        public static void BlockFriend(Guid userId, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.BlockFriend(userId, friendId, 1);

            //  SPs.HispUserFriendBlock(userId, friendId, 1).Execute();
        }

        public static void UnBlockFriend(Guid userId, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.BlockFriend(userId, friendId, 0);
            //SPs.HispUserFriendBlock(userId, friendId, 0).Execute();
        }

        public static void BirthdayNotification(string userId, string friendId, int allow)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.BirthdayNotification(userId, friendId, allow);
        }

        public static void DeleteFriend(Guid userId, Guid friendId)
        {
            _4screen.CSB.DataAccess.Data.FriendHanlder fHanlder = new _4screen.CSB.DataAccess.Data.FriendHanlder();
            fHanlder.DeleteFriend(userId, friendId);
            //SPs.HispUserFriendRemoveFriend(userId, friendId).Execute();
        }
    }
}