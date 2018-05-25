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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Notification;

namespace _4screen.CSB.Notification.Data
{
   internal class CSBMessage
   {
      public void Send(string userID, string subject, string body)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Message_Insert";

            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, new Guid(userID)));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.NVarChar, subject));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.NVarChar, body));
            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }
   }
}
