//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//  2007.08.02  1.0.0.1  AW  GetObjectViewCountPerType for all objects
//  2007.08.17  1.0.0.2  AW  Try Catch Finally fixed
//  2007.11.06  1.0.0.3  AW  Query fixed because of db changes
//  2008.01.07  1.0.0.6  AW  Query get view count fixed
//*****************************************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Security;
using System.Configuration;

namespace _4screen.CSB.MonitorService
{
   public class ServiceStatistics
   {
      Dictionary<string, string> stringTranslations = new Dictionary<string, string>();

      public ServiceStatistics()
      {
         Hashtable hashtable = (Hashtable)ConfigurationManager.GetSection("stringTranslations");
         foreach (DictionaryEntry translation in hashtable)
         {
            stringTranslations.Add((string)translation.Key, (string)translation.Value);
         }
      }

      public List<ChartDataPair> GetObjectCountPerType(SqlDateTime fromDate)
      {
         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         Dictionary<string, int> objectCount = new Dictionary<string, int>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            // Count objects in data objects table
            sqlConnection.Command.CommandText = "SELECT COUNT(*) AS OBJ_Count, OBJ_Type FROM hitbl_DataObject_OBJ WHERE OBJ_InsertedDate > @FromDate GROUP BY OBJ_Type ORDER BY OBJ_Count DESC";
            sqlConnection.Command.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
            sqlConnection.Command.Parameters["@FromDate"].Value = fromDate;
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               objectCount.Add(sqlDataReader["OBJ_Type"].ToString(), int.Parse(sqlDataReader["OBJ_Count"].ToString()));
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

         int userCount = 0;
         try
         {
            // Count users in user table
            sqlConnection = new SqlConnection();
            sqlConnection.Command.CommandText = "SELECT Count(dbo.aspnet_Membership.UserId) AS User_Count FROM dbo.aspnet_Membership where dbo.aspnet_Membership.CreateDate > @FromDate";
            sqlConnection.Command.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
            sqlConnection.Command.Parameters["@FromDate"].Value = fromDate;
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            if (sqlDataReader.Read())
            {
               userCount = int.Parse(sqlDataReader["User_Count"].ToString());
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

         // Loop through whole enumeration and merge with data from above
         foreach (int value in Enum.GetValues(typeof(ObjectType)))
         {
            ObjectType currentType = (ObjectType)Enum.Parse(typeof(ObjectType), "" + value);
            if (currentType != ObjectType.User)
            {
               if (objectCount.ContainsKey("" + value))
                  chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], objectCount["" + value]));
               else
                  chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], 0));
            }
            else
            {
               chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], userCount));
            }
         }

         return chartDataPairList;
      }

      public List<ChartDataPair> GetObjectViewCountPerType()
      {
         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         Dictionary<string, int> objectCount = new Dictionary<string, int>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            // Count objects in db
            sqlConnection.Command.CommandText = "SELECT OBJ_Type, SUM(OBJ_ViewCount) AS OBJ_ViewCount " +
                                                "FROM hitbl_DataObject_OBJ " +
                                                "GROUP BY OBJ_Type " +
                                                "ORDER BY OBJ_ViewCount DESC";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               objectCount.Add(sqlDataReader["OBJ_Type"].ToString(), int.Parse(sqlDataReader["OBJ_ViewCount"].ToString()));
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

         // Loop through whole enumeration and merge with data from above
         foreach (int value in Enum.GetValues(typeof(ObjectType)))
         {
            ObjectType currentType = (ObjectType)Enum.Parse(typeof(ObjectType), "" + value);
            if (objectCount.ContainsKey("" + value))
               chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], objectCount["" + value]));
            else
               chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], 0));
         }

         return chartDataPairList;
      }

      public List<ChartDataPair> GetObjectViewCountPerTypeToday()
      {
         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         Dictionary<string, int> objectCount = new Dictionary<string, int>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            // Count objects in db
            sqlConnection.Command.CommandText = "SELECT hitbl_DataObject_OBJ.OBJ_Type, COUNT(*) AS OBJ_ViewCount " +
                                                "FROM hitbl_LogObjectActions_LOG INNER JOIN " +
                                                "     hitbl_DataObject_OBJ ON hitbl_LogObjectActions_LOG.LOG_ObjectID = hitbl_DataObject_OBJ.OBJ_ID " +
                                                "WHERE (hitbl_LogObjectActions_LOG.LOG_Rule LIKE '%Viewed') AND (hitbl_LogObjectActions_LOG.LOG_Action = 'LOAD') AND (hitbl_LogObjectActions_LOG.LOG_Date >= @FromDate) " +
                                                "GROUP BY hitbl_DataObject_OBJ.OBJ_Type " +
                                                "ORDER BY OBJ_ViewCount DESC";
            sqlConnection.Command.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
            sqlConnection.Command.Parameters["@FromDate"].Value = DateTime.Today;
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               objectCount.Add(sqlDataReader["OBJ_Type"].ToString(), int.Parse(sqlDataReader["OBJ_ViewCount"].ToString()));
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

         // Loop through whole enumeration and merge with data from above
         foreach (int value in Enum.GetValues(typeof(ObjectType)))
         {
            ObjectType currentType = (ObjectType)Enum.Parse(typeof(ObjectType), "" + value);
            if (objectCount.ContainsKey("" + value))
               chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], objectCount["" + value]));
            else
               chartDataPairList.Add(new ChartDataPair(stringTranslations[currentType.ToString()], 0));
         }

         return chartDataPairList;
      }

      public List<ChartDataPair> GetUserTagWordCount()
      {
         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT TOP 20 dbo.hitbl_Tagword_TGW.TGW_WordLower, Count(dbo.hitbl_Tagword_TGW.TGW_WordLower) AS TagWordCount " +
                                                "FROM dbo.hitbl_TagLog_TGL INNER JOIN dbo.hitbl_Tagword_TGW ON dbo.hitbl_TagLog_TGL.TGW_ID = dbo.hitbl_Tagword_TGW.TGW_ID " +
                                                "GROUP BY dbo.hitbl_Tagword_TGW.TGW_WordLower, dbo.hitbl_TagLog_TGL.TGW_Type " +
                                                "HAVING (((dbo.hitbl_TagLog_TGL.TGW_Type)>1)) " +
                                                "ORDER BY Count(dbo.hitbl_Tagword_TGW.TGW_WordLower) DESC";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               // Get tag name and add to list
               chartDataPairList.Add(new ChartDataPair(sqlDataReader["TGW_WordLower"].ToString(), int.Parse(sqlDataReader["TagWordCount"].ToString())));
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

         return chartDataPairList;
      }

      public List<ChartDataPair> GetInterestByType(string intrestType)
      {
         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         Dictionary<string, string> nameLookUpTable = new Dictionary<string, string>();
         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            // Get names
            sqlConnection.Command.CommandText = "SELECT MAN_ID, MAN_Title FROM hitbl_Main_MAN";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               nameLookUpTable.Add(sqlDataReader["MAN_ID"].ToString(), sqlDataReader["MAN_Title"].ToString());
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

         Dictionary<string, ChartDataPair> chartDataPairHashtable = new Dictionary<string, ChartDataPair>();
         try
         {
            // Get interests
            sqlConnection = new SqlConnection();
            sqlConnection.Command.CommandText = "SELECT " + intrestType + " FROM hitbl_UserProfileData_UPD";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               string[] answerIdList = sqlDataReader[intrestType].ToString().Split(new char[] { ';' });
               foreach (string answerId in answerIdList)
               {
                  if (answerId != "")
                  {
                     if (!chartDataPairHashtable.ContainsKey(answerId))
                     {
                        chartDataPairHashtable.Add(answerId, new ChartDataPair(nameLookUpTable[answerId], 1));
                     }
                     else
                     {
                        chartDataPairHashtable[answerId].Value++;
                     }
                  }
               }
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

         foreach (ChartDataPair chartDataPair in chartDataPairHashtable.Values)
         {
            chartDataPairList.Add(chartDataPair);
         }
         chartDataPairList.Sort();


         return chartDataPairList;
      }

      public List<ChartDataPair> GetTopEmailProvider()
      {
         Dictionary<string, int> emailTable = new Dictionary<string, int>();
         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT Email FROM aspnet_Membership";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               string[] email = sqlDataReader["Email"].ToString().Split(new char[] { '@' });
               if (email.Length == 2)
               {
                  if (!emailTable.ContainsKey(email[1]))
                  {
                     emailTable[email[1]] = 1;
                  }
                  else
                  {
                     emailTable[email[1]]++;
                  }
               }
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


         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         foreach (KeyValuePair<string, int> emailProvider in emailTable)
         {
            chartDataPairList.Add(new ChartDataPair(emailProvider.Key, emailProvider.Value));
         }
         chartDataPairList.Sort();
         return chartDataPairList.GetRange(0, 15);
      }

      public List<ChartDataPair> GetTopHomeRegion()
      {
         Dictionary<Regions, int> zipTable = new Dictionary<Regions, int>();
         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT UPD_Zip FROM hitbl_UserProfileData_UPD";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               string zip = sqlDataReader["UPD_Zip"].ToString();
               int dummyResult;
               if (zip.Length == 4 && int.TryParse(zip, out dummyResult))
               {
                  zip = zip.Substring(0, 1);
                  Regions region = (Regions)Enum.Parse(typeof(Regions), zip);
                  if (!zipTable.ContainsKey(region))
                  {
                     zipTable[region] = 1;
                  }
                  else
                  {
                     zipTable[region]++;
                  }
               }
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

         List<ChartDataPair> chartDataPairList = new List<ChartDataPair>();
         foreach (KeyValuePair<Regions, int> zip in zipTable)
         {
            chartDataPairList.Add(new ChartDataPair(zip.Key.ToString(), zip.Value));
         }
         chartDataPairList.Sort();
         return chartDataPairList;
      }
   }
}
