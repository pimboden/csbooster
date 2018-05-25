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
using _4screen.CSB.Notification;
using _4screen.CSB.Common.Notification;

namespace _4screen.CSB.Notification.Data
{
   internal class Event
   {
      public void ReportEvent(EventIdentifier identifier, Guid userIDLogedIn, string subject, string body, string address, bool confident)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_NotificationSend_Insert";

            GetData.Parameters.Add(SqlHelper.AddParameter("@Identifier", SqlDbType.Int, (int)identifier));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Carrier", SqlDbType.Int, (int)CarrierType.EMail));

            if (address.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Address", SqlDbType.UniqueIdentifier, address));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Address", SqlDbType.UniqueIdentifier));

            if (subject.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier, subject));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier));

            if (body.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier, body));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier));
            
            GetData.Parameters.Add(SqlHelper.AddParameter("@DeleteAfterSuccess", SqlDbType.Bit, confident ? 0 : 1));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }

      public void ReportEvent(EventIdentifier identifier, Guid userIDLogedIn, Guid? objectID, int objectType, string title, 
          Guid? communityID, Guid? userID, Guid? parentID, DateTime? birthday, string subject, string body, bool confident)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Event_Insert";

            GetData.Parameters.Add(SqlHelper.AddParameter("@Identifier", SqlDbType.Int, (int)identifier));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Confident", SqlDbType.Bit, confident ? 1 : 0));
            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userIDLogedIn));

            if (objectID.HasValue)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier));

            if (objectType != 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, objectType));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int));

            if (userID.HasValue)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_USR_ID", SqlDbType.UniqueIdentifier, userID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_USR_ID", SqlDbType.UniqueIdentifier));

            if (communityID.HasValue)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_CTY_ID", SqlDbType.UniqueIdentifier, communityID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_CTY_ID", SqlDbType.UniqueIdentifier));

            if (parentID.HasValue)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_ID", SqlDbType.UniqueIdentifier, parentID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_ID", SqlDbType.UniqueIdentifier));

            if (birthday != null)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Birthday", SqlDbType.DateTime, birthday.Value.Date));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Birthday", SqlDbType.UniqueIdentifier));

            if (subject.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier, subject));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier));

            if (body.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier, body));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }

      public void ReportEvent(EventIdentifier identifier, Guid userIDLogedIn, Guid? objectID, Guid? parentID, string subject, string body, bool confident)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Event_Insert";

            GetData.Parameters.Add(SqlHelper.AddParameter("@Identifier", SqlDbType.Int, (int)identifier));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Confident", SqlDbType.Bit, confident ? 1 : 0));
            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userIDLogedIn));

            if (objectID.HasValue)
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier));

            GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int));

            GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_USR_ID", SqlDbType.UniqueIdentifier));

            GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_CTY_ID", SqlDbType.UniqueIdentifier));

            if (parentID.HasValue)
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_ID", SqlDbType.UniqueIdentifier, parentID.Value));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_ID", SqlDbType.UniqueIdentifier));

            if (subject.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier, subject));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.UniqueIdentifier));

            if (body.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier, body));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.UniqueIdentifier));

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
