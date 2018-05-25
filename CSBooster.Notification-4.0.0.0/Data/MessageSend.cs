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
   internal class MessageSend
   {
      private string strID = string.Empty;
      private CarrierType enuCarrierType = CarrierType.None;
      private List<string> listAddress = new List<string>();
      private string strSubject = string.Empty;
      private string strBody = string.Empty;
      private bool blnTestMode = true;
      private string strConn;

      public MessageSend(DataRow dr, bool testMode, string conn)
      {
         blnTestMode = testMode;
         strConn = conn;

         strID = dr["NNS_ID"].ToString();
         enuCarrierType = (CarrierType)Convert.ToInt32(dr["NNS_Carrier"]);
         string[] strAddress = dr["NNS_Address"].ToString().Split('|');
         foreach (string strItem in strAddress)
         {
            listAddress.Add(strItem);
         }
         strSubject = dr["NNS_Subject"].ToString();
         strBody = dr["NNS_Body"].ToString();
      }

      public MessageSend(SqlDataReader sqlReader, bool testMode, string conn)
      {
         blnTestMode = testMode;
         strConn = conn;

         strID = sqlReader["NNS_ID"].ToString();
         enuCarrierType = (CarrierType)Convert.ToInt32(sqlReader["NNS_Carrier"]);
         string[] strAddress = sqlReader["NNS_Address"].ToString().Split('|');
         foreach (string strItem in strAddress)
         {
            listAddress.Add(strItem);
         }
         strSubject = sqlReader["NNS_Subject"].ToString();
         strBody = sqlReader["NNS_Body"].ToString();
      }

      public string Body
      {
         get { return strBody; }
      }

      public string Subject
      {
         get { return strSubject; }
      }

      public List<string> Address
      {
         get { return listAddress; }
      }

      public string ID
      {
         get { return strID; }
      }

      public CarrierType CarrierType
      {
         get { return enuCarrierType; }
      }

      public bool Send()
      {
         try
         {
            IMessageSender objSender;
            if (blnTestMode)
               objSender = (IMessageSender)new MessageSendTest();
            else
            {
               if (CarrierType == CarrierType.EMail)
                  objSender = (IMessageSender)new MessageSendEmail();
               else if (CarrierType == CarrierType.CSBMessage)
                  objSender = (IMessageSender)new MessageSendCSBMessage();
               else
                  return false;
            }
            objSender.Send(this);
            SetSuccess();
            return true;
         }
         catch (Exception exc)
         {
            SetError(exc.ToString());
            return false;
         }
      }

      private void SetSuccess()
      {
         SqlConnection Conn = new SqlConnection(strConn);
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_NotificationSend_Success";

            GetData.Parameters.Add(SqlHelper.AddParameter("@ID", SqlDbType.UniqueIdentifier, new Guid(this.ID)));

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

      private void SetError(string error)
      {
         SqlConnection Conn = new SqlConnection(strConn);
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_NotificationSend_Error";

            GetData.Parameters.Add(SqlHelper.AddParameter("@ID", SqlDbType.UniqueIdentifier, new Guid(this.ID)));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Error", SqlDbType.NVarChar, error));

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
