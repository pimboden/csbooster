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
   internal class Notification
   {
      string strWebRoot = ConfigSetting.SiteURL();
      string strRootFolder = string.Empty;

      public Notification(string rootFolder)
      {
         strRootFolder = rootFolder;
      }

      public void CheckBirthdayRegistration()
      {
          // DODO: An das neue Alerting anpassen

         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandTimeout = 600;
            GetData.CommandText = "hisp_Notification_RegisteredEvent_CheckBirthdayRegistration";

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }

      /// <summary>
      /// liest pro User und Carrier alle zugewiesenen Events und baut die eigentliche Messages auf.
      /// Speichert die ab und löscht die Relation zwischen Event / Registration
      /// </summary>
      /// <returns>Anzahl der aufbereiteten Messages</returns>
      public int GetPending()
      {
         int intCount = 0;
         string strLastUserID = string.Empty;
         CarrierType enuLastCarrierType = CarrierType.None;

         NotificationMessage objMessage = null;
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_RegisteredEvent_GetPending";

            Conn.Open();
            SqlDataReader sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            while (sqlReader.Read())
            {
               CarrierType enuCarrierType = (CarrierType)Convert.ToInt32(sqlReader["NRE_Carrier"]);
               string strUserID = sqlReader["NUS_USR_ID"].ToString();
               string strEventID = sqlReader["NEV_ID"].ToString();
               string strRegisterID = sqlReader["NRE_ID"].ToString();

               if (strLastUserID != strUserID || enuLastCarrierType != enuCarrierType)
               {
                  strLastUserID = strUserID;
                  enuLastCarrierType = enuCarrierType;

                  objMessage = InsertPending(objMessage); // TODO: Return value always null
                  objMessage = new NotificationMessage(enuCarrierType, strWebRoot, strRootFolder);
               }
               objMessage.AddEvent(sqlReader);
               SetGroupID(strEventID, strRegisterID, objMessage.GroupID);
               intCount++;
            }
            sqlReader.Close();
            objMessage = InsertPending(objMessage);
         }
         catch
         {
            if (objMessage != null)
               SetGroupID(string.Empty, string.Empty, objMessage.GroupID);

            throw;
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }

         return intCount;
      }

      private void SetNextSend(string groupID)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_RegisteredEvent_SetNextSend";

            GetData.Parameters.Add(SqlHelper.AddParameter("@GroupID", SqlDbType.UniqueIdentifier, new Guid(groupID)));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }

      private void DeleteGroup(string groupID)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Event_Registration_Delete";

            GetData.Parameters.Add(SqlHelper.AddParameter("@GroupID", SqlDbType.UniqueIdentifier, new Guid(groupID)));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }

      private void SetGroupID(string eventID, string registerID, string groupID)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Event_Registration_SetGroup";

            if (eventID.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@NEV_ID", SqlDbType.UniqueIdentifier, new Guid(eventID)));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@NEV_ID", SqlDbType.UniqueIdentifier));

            if (registerID.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@NRE_ID", SqlDbType.UniqueIdentifier, new Guid(registerID)));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@NRE_ID", SqlDbType.UniqueIdentifier));

            GetData.Parameters.Add(SqlHelper.AddParameter("@GroupID", SqlDbType.UniqueIdentifier, new Guid(groupID)));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }

      /// <summary>
      /// Speichert eine Message in der DB.
      /// Setzt das NextSend Datum in der Registration neu
      /// Löscht die Relationen die zu dieser Message geführt haben
      /// </summary>
      /// <param name="message"></param>
      /// <returns></returns>
      private NotificationMessage InsertPending(NotificationMessage message)
      {
         if (message == null)
            return null;

         message.Commit();

         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_NotificationSend_Insert";

            GetData.Parameters.Add(SqlHelper.AddParameter("@Identifier", SqlDbType.Int, 0));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Carrier", SqlDbType.Int, (int)message.Carrier));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Address", SqlDbType.NVarChar, message.Address));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Subject", SqlDbType.NVarChar, message.Subject));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Body", SqlDbType.NVarChar, message.Body));
            GetData.Parameters.Add(SqlHelper.AddParameter("@DeleteAfterSuccess", SqlDbType.Bit, message.Confident ? 0 : 1));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }

         SetNextSend(message.GroupID); //für alle registrationen dieser gruppe das 'NextSend' Datum setzen
         DeleteGroup(message.GroupID); //löschen aller relationen-records dieser gruppe

         return null;
      }

      /// <summary>
      /// Liest alle Messages die noch nicht gesendet wurden und sendet diese mit dem richtigen Carrier
      /// </summary>
      /// <returns>Anzahl der Messages die versendet wurden</returns>
      public int SendNotification()
      {
         bool blnTestMode = ConfigSetting.TestMode();
         int intCount = 0;

         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandTimeout = 300;
            GetData.CommandText = "hisp_Notification_NotificationSend_GetPending";

            SqlDataAdapter da = new SqlDataAdapter(GetData);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               MessageSend objMessageSend = new MessageSend(dr, blnTestMode, Helper.GetSiemeConnectionString());
               if (objMessageSend.Send())
               {
                  intCount++;
               }
            }

         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }

         return intCount;
      }

      /// <summary>
      /// Verteilt die eingegangenen Events zu den verschiedenen Registrationen
      /// </summary>
      public void AllocateNotification()
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandTimeout = 300;
            GetData.CommandText = "hisp_Notification_Event_Registration_Insert";

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }
   }
}
