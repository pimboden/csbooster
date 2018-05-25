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

namespace _4screen.CSB.Notification.Data
{

   internal class User
   {
      public void Update(Business.User item)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_User_Update";

            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier,item.UserID));

            if (item.Nickname.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Nickname", SqlDbType.NVarChar, item.Nickname));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Nickname", SqlDbType.NVarChar));

            if (item.Name.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Name", SqlDbType.NVarChar, item.Name));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Name", SqlDbType.NVarChar));

            if (item.Firstname.Length > 0)
               GetData.Parameters.Add(SqlHelper.AddParameter("@Firstname", SqlDbType.NVarChar, item.Firstname));
            else
               GetData.Parameters.Add(SqlHelper.AddParameter("@Firstname", SqlDbType.NVarChar));

            foreach (Business.Carrier carrier in item.Carriers)
            {
               string strName = string.Format("@Carrier_{0}", (int)carrier.Type);
               if (carrier.Address.Length > 0)
                  GetData.Parameters.Add(SqlHelper.AddParameter(strName, SqlDbType.NVarChar, carrier.Address));
               else
                  GetData.Parameters.Add(SqlHelper.AddParameter(strName, SqlDbType.NVarChar));
            }

            Conn.Open();
            GetData.ExecuteNonQuery();
            item.Loaded = true;
         }
         finally
         {
            Conn.Close();
         }
      }

      public void Load(Business.User item)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_User_Load";
            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, item.UserID));

            Conn.Open();
            SqlDataReader sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlReader.Read())
               FillObject(item, sqlReader);
            else
               item.Loaded = false;
         }
         finally
         {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
               Conn.Close();
            }
         }

      }

      public void Delete(string userID)
      {
         SqlConnection Conn = new SqlConnection(Helper.GetSiemeConnectionString());
         try
         {
            SqlCommand GetData = new SqlCommand();

            GetData.Connection = Conn;
            GetData.CommandType = CommandType.StoredProcedure;
            GetData.CommandText = "hisp_Notification_User_Delete";
            GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, new Guid(userID)));

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

      private void FillObject(Business.User item, SqlDataReader sqlReader)
      {
         item.UserID = sqlReader["NUS_USR_ID"].ToString().ToGuid();
         item.Nickname = sqlReader["NUS_Nickname"].ToString();
         item.Name = sqlReader["NUS_Name"].ToString();
         item.Firstname = sqlReader["NUS_Firstname"].ToString();
         for (int i = 1; i <= 2; i++)
         {
            try
            {
               string strValue = sqlReader[string.Format("NUS_Carrier_{0}", i)].ToString();
               Business.Carrier objCarrier = item.Carriers.Item((CarrierType)i);
               if (objCarrier != null)
                  objCarrier.Address = strValue;
            }
            catch
            {
               break;
            }
         }
         item.Loaded = true;
      }
   }





}
