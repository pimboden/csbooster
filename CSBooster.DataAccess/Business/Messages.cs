//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//******************************************************************************


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class Messages
    {
        private static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        public static List<Message> GetOutbox(Guid userId, int? groupID, SiteContext siteContext)
        {
            int numberMessages;
            return GetOutbox(userId, groupID, null, null, null, null, null, null, null, null, null, null, null, null, out numberMessages, siteContext);
        }

        public static List<Message> GetOutbox(Guid userId, int? groupID, DateTime? dateSentFrom, DateTime? dateSentTo, bool? flagged, bool? isRead, string generalSearchParam, string toUserName, string subject, string message, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberMessages, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();

            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetMessagesUserOutbox(userId, groupID, dateSentFrom, dateSentTo, flagged, isRead, "%" + toUserName + "%", "%" + subject + "%", "%" + message + "%", "%" + generalSearchParam + "%", pageNum ?? 1, pageSize ?? 10, sortAttr, sortDir, ref NumberMessages);
            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }

            numberMessages = NumberMessages.Value;

            return listMsg;
        }

        public static int GetInboxUnreadCount(Guid userId, int? groupID)
        {
            int numberMessages = 0;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            var result = wdc.hisp_UserMessages_GetMessagesUnreadCount(userId, groupID).ElementAtOrDefault(0);
            if (result != null)
                numberMessages = result.NumberMessages ?? 0;
            return numberMessages;
        }

        public static List<Message> GetInbox(Guid userId, int? groupID, SiteContext siteContext)
        {
            int numberMessages;
            return GetInbox(userId, groupID, null, null, null, null, null, null, null, null, null, null, null, null, null, out numberMessages, siteContext);
        }

        public static List<Message> GetInbox(Guid userId, int? groupID, DateTime? dateSentFrom, DateTime? dateSentTo, bool? flagged, bool? isRead, string generalSearchParam, string fromUserName, string subject, string message, int? msgType, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberMessages, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();
            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetMessagesUserInbox(userId, groupID, dateSentFrom, dateSentTo, flagged, isRead, "%" + fromUserName + "%", "%" + subject + "%", "%" + message + "%", msgType, "%" + generalSearchParam + "%", pageNum ?? 1, pageSize ?? 10, sortAttr, sortDir, ref NumberMessages);

            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }

            numberMessages = NumberMessages ?? 0;

            return listMsg;
        }

        public static List<Message> GetFlagged(Guid userId, DateTime? dateSentFrom, DateTime? dateSentTo, bool? isRead, string generalSearchParam, string userName, string subject, string message, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberMessages, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();
            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetMessagesUserFlagged(userId, dateSentFrom, dateSentTo, null, isRead, "%" + userName + "%", "%" + subject + "%", "%" + message + "%", "%" + generalSearchParam + "%", pageNum ?? 1, pageSize ?? 10, sortAttr, sortDir, ref NumberMessages);
            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }

            numberMessages = NumberMessages ?? 0;

            return listMsg;
        }


        public static int GetRequestUnreadCount(Guid userId)
        {
            int numberMessages = 0;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            var result = wdc.hisp_UserMessages_GetRequestUnreadCount(userId).ElementAtOrDefault(0);
            if (result != null)
                numberMessages = result.NumberMessages ?? 0;
            return numberMessages;
        }

        public static List<Message> GetRequestInbox(Guid userId, int? pageNum, int? pageSize, out int numberItems, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();
            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetRequestUserInbox(userId, pageNum ?? 1, pageSize ?? 10, ref NumberMessages);
            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }
            numberItems = NumberMessages ?? 0;
            return listMsg;
        }

        public static List<Message> GetRequestOutbox(Guid userId, int? pageNum, int? pageSize, out int numberItems, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();
            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetRequestUserOutbox(userId, pageNum ?? 1, pageSize ?? 10, ref NumberMessages);
            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }
            numberItems = NumberMessages ?? 0;
            return listMsg;
        }

        public static List<Message> GetAlerts(Guid userId, DateTime? dateSentFrom, DateTime? dateSentTo, bool? flagged, bool? isRead, string generalSearchParam, string fromUserName, string subject, string message, int? pageNum, int? pageSize, string sortAttr, string sortDir, out int numberMessages, SiteContext siteContext)
        {
            List<Message> listMsg = new List<Message>();
            int? NumberMessages = null;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            var results = wdc.hisp_UserMessages_GetAlerts(userId, dateSentFrom, dateSentTo, flagged, isRead, "%" + fromUserName + "%", "%" + subject + "%", "%" + message + "%", "%" + generalSearchParam + "%", pageNum ?? 1, pageSize ?? 10, sortAttr, sortDir, ref NumberMessages);
            foreach (MessageResult result in results)
            {
                Message newMessage = new Message(siteContext);
                Message.FillMessage(newMessage, result);
                listMsg.Add(newMessage);
            }
            numberMessages = NumberMessages ?? 0;
            return listMsg;
        }

        public static int GetAlertsUnreadCount(Guid userId)
        {
            int numberMessages = 0;
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            var result = wdc.hisp_UserMessages_GetAlertsUnreadCount(userId).ElementAtOrDefault(0);
            if (result != null)
                numberMessages = result.NumberMessages ?? 0;
            return numberMessages;
        }

        public static void SetGroup(string msgID, MessageGroupTypes groupType, int groupID)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            wdc.hisp_UserMessages_SetGroup(new Guid(msgID), (int) groupType, groupID);
        }
    }
}