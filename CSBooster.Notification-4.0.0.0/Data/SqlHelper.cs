//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//  Updated:   
//******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;


namespace _4screen.CSB.Notification.Data
{
   internal static class SqlHelper
   {

      public static string GetNullString(string content)
      {
         if (content.Trim().Length == 0)
            return "NULL";
         else
            return string.Format("'{0}'", content.Replace("'", "''"));
      }

      public static string GetString(string content)
      {
         if (content.Trim().Length == 0)
            return "''";
         else
            return string.Format("'{0}'", content.Replace("'", "''"));
      }

      public static SqlParameter AddParameter(string name, SqlDbType fieldType, int size, object Value)
      {
         SqlParameter para = new SqlParameter(name, fieldType, size);
         para.Value = Value;
         return para;
      }

      public static SqlParameter AddParameter(string name, SqlDbType fieldType, object Value)
      {
         SqlParameter para = new SqlParameter(name, fieldType);
         para.Value = Value;
         return para;
      }

      public static SqlParameter AddParameter(string name, SqlDbType fieldType)
      {
         SqlParameter para = new SqlParameter(name, fieldType);
         para.Value = DBNull.Value;
         return para;
      }


   }
}
