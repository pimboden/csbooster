//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.3  AW  Initial release
//*****************************************************************************************

using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Security;

namespace _4screen.CSB.MonitorService
{
   public class ServiceData
   {
      public List<CSBDataObject> GetDataObjects(string objectId, string communityId, string userId, bool featured)
      {
         List<CSBDataObject> dataObjects = new List<CSBDataObject>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            Guid? objectGuid = null;
            if (!string.IsNullOrEmpty(objectId))
               objectGuid = new Guid(objectId);
            Guid? communityGuid = null;
            if (!string.IsNullOrEmpty(communityId))
               communityGuid = new Guid(communityId);
            Guid? userGuid = null;
            if (!string.IsNullOrEmpty(userId))
               userGuid = new Guid(userId);

            sqlConnection.Command.CommandText = "hisp_DataObject_Search";
            sqlConnection.Command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Command.Parameters.Add("@ObjectId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@ObjectId"].Value = objectGuid;
            sqlConnection.Command.Parameters.Add("@CommunityId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@CommunityId"].Value = communityGuid;
            sqlConnection.Command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@UserId"].Value = userGuid;
            sqlConnection.Command.Parameters.Add("@Featured", SqlDbType.Bit);
            if (featured)
               sqlConnection.Command.Parameters["@Featured"].Value = featured;
            else
               sqlConnection.Command.Parameters["@Featured"].Value = null;
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               CSBDataObject dataObject = new CSBDataObject();
               dataObject.ObjectId = (Guid)sqlDataReader["OBJ_ID"];
               dataObject.CommunityId = (Guid)sqlDataReader["CTY_ID"];
               dataObject.UserId = (Guid)sqlDataReader["USR_ID"];
               dataObject.Featured = (int)sqlDataReader["OBJ_Featured"] == 0 ? false : true;
               dataObject.Title = (string)sqlDataReader["OBJ_Title"];
               dataObject.Nickname = (string)sqlDataReader["USR_Nickname"];
               dataObject.ObjectType = (ObjectType)Enum.Parse(typeof(ObjectType), sqlDataReader["OBJ_Type"].ToString());
               dataObjects.Add(dataObject);
            }
         }
         catch (Exception e)
         {
            throw new SoapException("Anfrage konnte nicht bearbeitet werden -> " + e.Message, SoapException.ServerFaultCode);
         }
         finally
         {
            try { sqlDataReader.Close(); }
            finally { sqlConnection.Close(); }
         }

         return dataObjects;
      }

      public bool FeatureDataObject(string objectId, bool featured)
      {
         SqlConnection sqlConnection = new SqlConnection();
         try
         {
            Guid objectGuid = new Guid(objectId);

            sqlConnection.Command.CommandText = "UPDATE hitbl_DataObject_OBJ SET OBJ_Featured = @Featured WHERE OBJ_ID = @ObjectId";
            sqlConnection.Command.Parameters.Add("@Featured", SqlDbType.Bit);
            sqlConnection.Command.Parameters["@Featured"].Value = featured;
            sqlConnection.Command.Parameters.Add("@ObjectId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@ObjectId"].Value = objectGuid;
            sqlConnection.Command.ExecuteNonQuery();
            return true;
         }
         catch
         {
            return false;
         }
         finally { sqlConnection.Close(); }
      }

      public List<ContentData> GetContentData()
      {
         List<ContentData> contentData = new List<ContentData>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT CNT_Name, CNT_Text FROM hitbl_Content_CNT WHERE CNT_Name LIKE 'Home%'";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               contentData.Add(new ContentData((string)sqlDataReader["CNT_Name"], (string)sqlDataReader["CNT_Text"]));
            }
         }
         catch (Exception e)
         {
            throw new SoapException("Anfrage konnte nicht bearbeitet werden -> " + e.Message, SoapException.ServerFaultCode);
         }
         finally
         {
            try { sqlDataReader.Close(); }
            finally { sqlConnection.Close(); }
         }

         return contentData;
      }

      public bool SetContentData(string key, string content)
      {
         SqlConnection sqlConnection = new SqlConnection();
         try
         {
            sqlConnection.Command.CommandText = "UPDATE hitbl_Content_CNT SET CNT_Text = @Content WHERE CNT_Name = @Key";
            sqlConnection.Command.Parameters.Add("@Key", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Key"].Value = key;
            sqlConnection.Command.Parameters.Add("@Content", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Content"].Value = content;
            sqlConnection.Command.ExecuteNonQuery();
            return true;
         }
         catch
         {
            return false;
         }
         finally { sqlConnection.Close(); }
      }
   }
}
