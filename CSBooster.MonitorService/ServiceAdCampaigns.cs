//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.20  1.0.0.4  AW  Initial release
//  2007.11.26  1.0.0.5  AW  New cost attributes
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
   public class ServiceAdCampaigns
   {
      public List<CSBAdCampaign> GetAdCampaigns()
      {
         List<CSBAdCampaign> adCampaigns = new List<CSBAdCampaign>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT * FROM hitbl_FilterAdCampaigns_FAC ORDER BY FAC_Company";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               CSBAdCampaign adCampaign = new CSBAdCampaign();
               adCampaign.CampaignId = (Guid)sqlDataReader["FAC_ID"];
               try { adCampaign.Description = (string)sqlDataReader["FAC_Company"]; }
               catch { }
               try { adCampaign.Banner = (string)sqlDataReader["FAC_BannerPage"]; }
               catch { }
               try { adCampaign.Url = (string)sqlDataReader["FAC_Url"]; }
               catch { }
               try { adCampaign.Content = (string)sqlDataReader["FAC_Content"]; }
               catch { }
               adCampaign.Credits = float.Parse(sqlDataReader["FAC_Credits"].ToString());
               adCampaign.CreditsUsed = float.Parse(sqlDataReader["FAC_CreditsUsed"].ToString());
               adCampaign.CostPerLinkClick = float.Parse(sqlDataReader["FAC_CostPerLinkClick"].ToString());
               adCampaign.CostPerPopupLinkClick = float.Parse(sqlDataReader["FAC_CostPerPopupLinkClick"].ToString());
               adCampaign.CostPerBannerClick = float.Parse(sqlDataReader["FAC_CostPerBannerClick"].ToString());
               adCampaign.CostPerPopupView = float.Parse(sqlDataReader["FAC_CostPerPopupView"].ToString());
               adCampaign.CostPerBannerView = float.Parse(sqlDataReader["FAC_CostPerBannerView"].ToString());
               adCampaigns.Add(adCampaign);
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

         return adCampaigns;
      }

      public bool SaveAdCampaign(CSBAdCampaign adCampaign)
      {
         SqlConnection sqlConnection = new SqlConnection();
         try
         {
            sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_Save";
            sqlConnection.Command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Command.Parameters.Add("@CampaignId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@CampaignId"].Value = adCampaign.CampaignId;
            sqlConnection.Command.Parameters.Add("@Company", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Company"].Value = adCampaign.Description;
            sqlConnection.Command.Parameters.Add("@BannerPage", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@BannerPage"].Value = adCampaign.Banner;
            sqlConnection.Command.Parameters.Add("@Url", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Url"].Value = adCampaign.Url;
            sqlConnection.Command.Parameters.Add("@Content", SqlDbType.Text);
            sqlConnection.Command.Parameters["@Content"].Value = adCampaign.Content;
            sqlConnection.Command.Parameters.Add("@Credits", SqlDbType.Float);
            sqlConnection.Command.Parameters["@Credits"].Value = adCampaign.Credits;
            sqlConnection.Command.Parameters.Add("@CreditsUsed", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CreditsUsed"].Value = adCampaign.CreditsUsed;
            sqlConnection.Command.Parameters.Add("@CostPerLinkClick", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CostPerLinkClick"].Value = adCampaign.CostPerLinkClick;
            sqlConnection.Command.Parameters.Add("@CostPerPopupLinkClick", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CostPerPopupLinkClick"].Value = adCampaign.CostPerPopupLinkClick;
            sqlConnection.Command.Parameters.Add("@CostPerBannerClick", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CostPerBannerClick"].Value = adCampaign.CostPerBannerClick;
            sqlConnection.Command.Parameters.Add("@CostPerPopupView", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CostPerPopupView"].Value = adCampaign.CostPerPopupView;
            sqlConnection.Command.Parameters.Add("@CostPerBannerView", SqlDbType.Float);
            sqlConnection.Command.Parameters["@CostPerBannerView"].Value = adCampaign.CostPerBannerView;
            sqlConnection.Command.ExecuteNonQuery();
            return true;
         }
         catch
         {
            return false;
         }
         finally { sqlConnection.Close(); }
      }

      public List<CSBAdWord> GetAdWords()
      {
         List<CSBAdWord> adWords = new List<CSBAdWord>();

         SqlConnection sqlConnection = new SqlConnection();
         System.Data.SqlClient.SqlDataReader sqlDataReader = null;
         try
         {
            sqlConnection.Command.CommandText = "SELECT hitbl_FilterAdWords_FAW.*, hitbl_FilterAdCampaigns_FAC.FAC_Company FROM hitbl_FilterAdWords_FAW JOIN hitbl_FilterAdCampaigns_FAC ON hitbl_FilterAdWords_FAW.FAW_CampaignId = hitbl_FilterAdCampaigns_FAC.FAC_ID ORDER BY FAW_Word";
            sqlDataReader = sqlConnection.Command.ExecuteReader();
            while (sqlDataReader.Read())
            {
               CSBAdWord adWord = new CSBAdWord();
               adWord.AdWordId = (Guid)sqlDataReader["FAW_ID"];
               adWord.Word = (string)sqlDataReader["FAW_Word"];
               adWord.IsExact = (bool)sqlDataReader["FAW_IsExactMatch"];
               adWord.Action = (AdWordFilterActions)Enum.Parse(typeof(AdWordFilterActions), sqlDataReader["FAW_Action"].ToString());
               adWord.CampaignId = (Guid)sqlDataReader["FAW_CampaignId"];
               adWord.CampaignDescription = (string)sqlDataReader["FAC_Company"];
               adWords.Add(adWord);
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

         return adWords;
      }

      public bool SaveAdWord(CSBAdWord adWord)
      {
         SqlConnection sqlConnection = new SqlConnection();
         try
         {
            sqlConnection.Command.CommandText = "hisp_Filter_AdWords_Save";
            sqlConnection.Command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Command.Parameters.Add("@WordId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@WordId"].Value = adWord.AdWordId;
            sqlConnection.Command.Parameters.Add("@Word", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Word"].Value = adWord.Word;
            sqlConnection.Command.Parameters.Add("@IsExact", SqlDbType.Bit);
            sqlConnection.Command.Parameters["@IsExact"].Value = adWord.IsExact;
            sqlConnection.Command.Parameters.Add("@Action", SqlDbType.NVarChar);
            sqlConnection.Command.Parameters["@Action"].Value = adWord.Action.ToString();
            sqlConnection.Command.Parameters.Add("@CampaignId", SqlDbType.UniqueIdentifier);
            sqlConnection.Command.Parameters["@CampaignId"].Value = adWord.CampaignId;
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
