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
using _4screen.CSB.Notification;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;

namespace _4screen.CSB.Notification.Data
{
   internal class RegistrationDefault
   {
      public void Save(Business.RegistrationDefaultList list)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Default_Save";

            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, list.userId));
            GetData.Parameters.Add(SqlHelper.AddParameter("@Default", SqlDbType.Xml, GetXml(list)));

            Conn.Open();
            GetData.ExecuteNonQuery();
         }
         finally
         {
            Conn.Close();
         }
      }

      public void Load(Business.RegistrationDefaultList list)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_Default_Load";

            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, list.userId));

            Conn.Open();
            SqlDataReader sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlReader.Read())
            {
               XmlDocument xmlDefault = new XmlDocument();
               xmlDefault.LoadXml(sqlReader["NRD_Default"].ToString());
               foreach (XmlElement xmlItem in xmlDefault.SelectNodes("//Default"))
               {
                  FillObject(list, xmlItem);
               }
            }
            sqlReader.Close();
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }
      }

      private string GetXml(Business.RegistrationDefaultList list)
      {
         XmlDocument xmlDoc = new XmlDocument();
         xmlDoc.AppendChild(xmlDoc.CreateElement("root"));

         foreach (Business.RegistrationDefault item in list)
         {
            XmlElement xmlDefault = xmlDoc.DocumentElement.AppendChild(xmlDoc.CreateElement("Default")) as XmlElement;
            xmlDefault.SetAttribute("Identifier", item.Identifier.ToString("D"));
            foreach (Business.Carrier carrier in item.Carriers)
            {
               if (carrier.Checked && carrier.Availably)
               {
                  xmlDefault.SetAttribute("Carrier", carrier.Type.ToString("D"));
                  xmlDefault.SetAttribute("Collect", carrier.Collect.ToString("D"));
               }
               else
               {
                  xmlDefault.SetAttribute("Carrier", CarrierType.None.ToString("D"));
                  xmlDefault.SetAttribute("Collect", CarrierCollect.Immediately.ToString("D"));
               }
            }
         }

         return xmlDoc.OuterXml;
      }

      private void FillObject(Business.RegistrationDefaultList list, XmlElement xmlDefault)
      {
         EventIdentifier enuEventIdentifier = (EventIdentifier)Convert.ToInt32(xmlDefault.GetAttribute("Identifier"));
         Business.RegistrationDefault item = list.Item(enuEventIdentifier);

         if (item != null)
         {
            item.Identifier = enuEventIdentifier;
            CarrierType enuCarrierType = (CarrierType)Convert.ToInt32(xmlDefault.GetAttribute("Carrier"));
            Business.Carrier objCarrier = item.Carriers.Item(enuCarrierType);
            if (objCarrier != null)
            {
               objCarrier.Checked = true;
               objCarrier.Collect = (CarrierCollect)Convert.ToInt32(xmlDefault.GetAttribute("Collect"));
               objCarrier.CollectValue = 1;
            }
         }

      }

   }


}
