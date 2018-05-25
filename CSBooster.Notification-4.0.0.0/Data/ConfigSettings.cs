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
   internal static class ConfigSetting
   {
      public static string SiteURL()
      {
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(string.Format(@"{0}\configurations\notification.config", WebRootPath.Instance.ToString()));
         return xmlDoc.SelectSingleNode("//SiteURL").InnerText;
      }

      public static bool TestMode()
      {
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(string.Format(@"{0}\configurations\notification.config", WebRootPath.Instance.ToString()));
         return Convert.ToBoolean(xmlDoc.SelectSingleNode("//TestMode").InnerText);
      }

      public static string TestModeFolder()
      {
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.Load(string.Format(@"{0}\configurations\notification.config", WebRootPath.Instance.ToString()));
         return xmlDoc.SelectSingleNode("//TestModeFolder").InnerText;
      }
   }
}
