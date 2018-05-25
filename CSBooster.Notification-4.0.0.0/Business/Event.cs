//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;   


namespace _4screen.CSB.Notification.Business
{
   public class Event
   {

      internal static EventIdentifier GetEventIdentifierEnum(string value, EventIdentifier defaultValue)
      {
         foreach (EventIdentifier item in Enum.GetValues(typeof(EventIdentifier)))
         {
            if (item.ToString() == value)
               return item;
         }
         return defaultValue;
      }

      public static void ReportEvent(EventIdentifier identifier, Guid userIDLogedIn, Guid? objectID)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(identifier, userIDLogedIn, objectID,null, string.Empty, string.Empty, false);
      }

      public static void ReportEvent(EventIdentifier identifier, Guid userIDLogedIn, Guid? objectID, bool confident)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(identifier, userIDLogedIn, objectID, null, string.Empty, string.Empty, confident);
      }

      public static void ReportNewTopicItem(Guid userIDLogedIn, Guid? objectID, Guid? topicID)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(EventIdentifier.NewTopicItem, userIDLogedIn, objectID, topicID, string.Empty, string.Empty, false);
      }

      public static void ReportNewTopicItem(Guid userIDLogedIn, Guid? objectID, Guid? topicID, bool confident)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(EventIdentifier.NewTopicItem, userIDLogedIn, objectID, topicID, string.Empty, string.Empty, confident);
      }

      public static void ReportNewMember(Guid userIDLogedIn, Guid? userID, Guid? communityID)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(EventIdentifier.NewMember, userIDLogedIn, communityID, null, string.Empty, string.Empty, false);
      }

      public static void ReportNewMember(Guid userIDLogedIn, Guid? userID, Guid? communityID, bool confident)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(EventIdentifier.NewMember, userIDLogedIn, communityID,null, string.Empty, string.Empty, confident);
      }

      public static void ReportBirthdayNotification(Guid userID, Guid? friendID, string friendName, DateTime birthday)
      {
         Data.Event objData = new Data.Event();
         objData.ReportEvent(EventIdentifier.BirthdayNotification, userID, friendID, _4screen.CSB.Common.Helper.GetObjectTypeNumericID("User"), friendName, new Guid(Common.Constants.DEFAULT_COMMUNITY_ID), userID, userID, birthday, string.Empty, string.Empty, false);
      }
   }
}
