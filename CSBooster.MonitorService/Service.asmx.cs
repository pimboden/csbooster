//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Data;
using System.Web;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Security;

namespace _4screen.CSB.MonitorService
{
   [WebService(Namespace = "http://localhost/")]
   public class Service : System.Web.Services.WebService
   {
      private ServiceStatistics statistics;
      private ServiceUsers users;
      private ServiceData serviceData;
      private ServiceAdCampaigns adCampaigns;
      public AuthenticationHeader authHeader;

      public Service()
      {
         statistics = new ServiceStatistics();
         users = new ServiceUsers();
         serviceData = new ServiceData();
         adCampaigns = new ServiceAdCampaigns();
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      public Authorization Authenticate()
      {
         if (authHeader != null) return new Authorization(authHeader.Username, authHeader.Password);
         else throw new Exception("No authentication header found!");
      }

      //////////////////////////////////////////////////////////////
      // Users
      //////////////////////////////////////////////////////////////

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<User> GetUsers(string username, string email, bool isLocked)
      {
         if (authHeader != null) return users.GetUsers(username, email, isLocked);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool LockUser(string username)
      {
         if (authHeader != null) return users.LockUser(username);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool UnlockUser(string username)
      {
         if (authHeader != null) return users.UnlockUser(username);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool SetUserEmail(string username, string email)
      {
         if (authHeader != null) return users.SetUserEmail(username, email);
         else throw new Exception("No authentication header found!");
      }

      //////////////////////////////////////////////////////////////
      // Data
      //////////////////////////////////////////////////////////////

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<CSBDataObject> GetDataObjects(string objectId, string communityId, string userId, bool featured)
      {
         if (authHeader != null) return serviceData.GetDataObjects(objectId, communityId, userId, featured);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool FeatureDataObject(string objectId, bool featured)
      {
         if (authHeader != null) return serviceData.FeatureDataObject(objectId, featured);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ContentData> GetContentData()
      {
         if (authHeader != null) return serviceData.GetContentData();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool SetContentData(string key, string content)
      {
         if (authHeader != null) return serviceData.SetContentData(key, content);
         else throw new Exception("No authentication header found!");
      }

      //////////////////////////////////////////////////////////////
      // Ad Campaigns
      //////////////////////////////////////////////////////////////

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<CSBAdCampaign> GetAdCampaigns()
      {
         if (authHeader != null) return adCampaigns.GetAdCampaigns();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool SaveAdCampaign(CSBAdCampaign adCampaign)
      {
         if (authHeader != null) return adCampaigns.SaveAdCampaign(adCampaign);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<CSBAdWord> GetAdWords()
      {
         if (authHeader != null) return adCampaigns.GetAdWords();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public bool SaveAdWord(CSBAdWord adWord)
      {
         if (authHeader != null) return adCampaigns.SaveAdWord(adWord);
         else throw new Exception("No authentication header found!");
      }

      //////////////////////////////////////////////////////////////
      // Statistics
      //////////////////////////////////////////////////////////////

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetObjectCountPerType()
      {
         if (authHeader != null) return statistics.GetObjectCountPerType(SqlDateTime.MinValue);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetObjectCountPerTypeToday()
      {
         if (authHeader != null) return statistics.GetObjectCountPerType(DateTime.Today);
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetObjectViewCountPerType()
      {
         if (authHeader != null) return statistics.GetObjectViewCountPerType();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetObjectViewCountPerTypeToday()
      {
         if (authHeader != null) return statistics.GetObjectViewCountPerTypeToday();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetInterestInArtCount()
      {
         if (authHeader != null) return statistics.GetInterestByType("UPD_KunstUnterhaltung");
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetInterestInPoliticsCount()
      {
         if (authHeader != null) return statistics.GetInterestByType("UPD_Politik");
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetInterestInLifestyleCount()
      {
         if (authHeader != null) return statistics.GetInterestByType("UPD_SportLifestyle");
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetInterestInCareerCount()
      {
         if (authHeader != null) return statistics.GetInterestByType("UPD_Karriere");
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetUserTagWordCount()
      {
         if (authHeader != null) return statistics.GetUserTagWordCount();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetTopEmailProvider()
      {
         if (authHeader != null) return statistics.GetTopEmailProvider();
         else throw new Exception("No authentication header found!");
      }

      [WebMethod]
      [SoapHeader("authHeader")]
      [AuthenticatonSoapExtensionAttribute]
      public List<ChartDataPair> GetTopHomeRegion()
      {
         if (authHeader != null) return statistics.GetTopHomeRegion();
         else throw new Exception("No authentication header found!");
      }
   }
}
