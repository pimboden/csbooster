//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Notification;

namespace _4screen.CSB.Notification.Data
{
   internal static class Constants
   {
      public const string K_LIST = "<%NOTIFICATION_LIST%>";
      public const string K_ITEM = "##ITEMTEMPLATE##";

      public const string K_FIELD_WEB_ROOT = "<%EVENT_WEB_ROOT%>";

      public const string K_FIELD_EVENT_LINK = "<%EVENT_LINK%>";
      public const string K_FIELD_EVENT_TITLE = "<%EVENT_TITLE%>";
      public const string K_FIELD_EVENT_DATE = "<%EVENT_DATE%>";
      public const string K_FIELD_EVENT_TIME = "<%EVENT_TIME%>";
      public const string K_FIELD_EVENT_OBJECT_TYPE = "<%EVENT_OBJECT_TYPE%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY_LONG = "<%EVENT_OBJECT_BIRTHDATE_DAY_LONG%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY = "<%EVENT_OBJECT_BIRTHDATE_DAY%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH = "<%EVENT_OBJECT_BIRTHDATE_MONTH%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH_LONG = "<%EVENT_OBJECT_BIRTHDATE_MONTH_LONG%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_YEAR = "<%EVENT_OBJECT_BIRTHDATE_YEAR%>";
      public const string K_FIELD_EVENT_OBJECT_BIRTHDATE_AGE = "<%EVENT_OBJECT_BIRTHDATE_AGE%>";

      public const string K_FIELD_COMMUNITY_LINK = "<%COMMUNITY_LINK%>";
      public const string K_FIELD_USER_LINK = "<%USER_LINK%>";
      public const string K_FIELD_USER_ID = "<%EVENT_USER_ID%>";
      public const string K_FIELD_USER_FIRSTNAME = "<%EVENT_USER_FIRSTNAME%>";
      public const string K_FIELD_USER_NAME = "<%EVENT_USER_NAME%>";
      public const string K_FIELD_USER_NICKNAME = "<%EVENT_USER_NICKNAME%>";
   }
}
