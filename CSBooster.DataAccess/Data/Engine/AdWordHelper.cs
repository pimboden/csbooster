//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    public class AdWordHelper
    {
        internal static string GetBanner(string pageName, UserDataContext udc)
        {
            List<string> bannerContent = new List<string>();
            List<Guid> campaignIds = new List<Guid>();
            float costPerBannerView = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_LoadByBannerPage";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@BannerPage", SqlDbType.VarChar));
                sqlConnection.Command.Parameters["@BannerPage"].Value = pageName;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    if (float.Parse(sqlDataReader["FAC_Credits"].ToString()) > float.Parse(sqlDataReader["FAC_CreditsUsed"].ToString()) + float.Parse(sqlDataReader["FAC_CostPerBannerClick"].ToString()))
                    {
                        bannerContent.Add(sqlDataReader["FAC_Content"].ToString());
                        campaignIds.Add((Guid)sqlDataReader["FAC_ID"]);
                        costPerBannerView = float.Parse(sqlDataReader["FAC_CostPerBannerView"].ToString());
                    }
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }

            if (bannerContent.Count == 1)
            {
                if (AddView(udc.IsAuthenticated ? udc.UserID : udc.AnonymousUserId, udc.UserIP, campaignIds[0], "Banner") > 0)
                {
                    IncreaseCreditsUsed(campaignIds[0], costPerBannerView);
                    AddLog(campaignIds[0], null, udc.UserID, pageName, "Banner");
                }
                return bannerContent[0];
            }
            else if (bannerContent.Count > 1)
            {
                Random random = new Random();
                int bannerIndex = random.Next(0, bannerContent.Count);
                if (AddView(udc.IsAuthenticated ? udc.UserID : udc.AnonymousUserId, udc.UserIP, campaignIds[bannerIndex], "Banner") > 0)
                {
                    IncreaseCreditsUsed(campaignIds[bannerIndex], costPerBannerView);
                    AddLog(campaignIds[bannerIndex], null, udc.UserID, pageName, "Banner");
                }
                return bannerContent[bannerIndex];
            }
            else
            {
                return "";
            }
        }

        internal static string GetCampaignUrl(Guid campaignId, Guid? objectId, UserDataContext udc, string word, string type)
        {
            string campaignUrl = "";
            float costPerLinkClick = 0;
            float costPerPopupLinkClick = 0;
            float costPerBannerLinkClick = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_LoadById";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                {
                    campaignUrl = sqlDataReader["FAC_Url"].ToString();
                    costPerLinkClick = float.Parse(sqlDataReader["FAC_CostPerLinkClick"].ToString());
                    costPerPopupLinkClick = float.Parse(sqlDataReader["FAC_CostPerPopupLinkClick"].ToString());
                    costPerBannerLinkClick = float.Parse(sqlDataReader["FAC_CostPerBannerClick"].ToString());
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }

            if (AddView(udc.IsAuthenticated ? udc.UserID : udc.AnonymousUserId, udc.UserIP, campaignId, type) > 0)
            {
                if (type == "BannerLink")
                    IncreaseCreditsUsed(campaignId, costPerBannerLinkClick);
                else if (type == "PopupLink")
                    IncreaseCreditsUsed(campaignId, costPerPopupLinkClick);
                else
                    IncreaseCreditsUsed(campaignId, costPerLinkClick);
                AddLog(campaignId, objectId, udc.UserID, word, type);
            }

            return campaignUrl;
        }

        internal static string GetCampaignContent(Guid campaignId, Guid objectId, UserDataContext udc, string word, string type)
        {
            string campaignContent = "";
            float costPerPopupView = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_LoadById";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                {
                    campaignContent = sqlDataReader["FAC_Content"].ToString();
                    costPerPopupView = float.Parse(sqlDataReader["FAC_CostPerPopupView"].ToString());
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }

            if (AddView(udc.IsAuthenticated ? udc.UserID : udc.AnonymousUserId, udc.UserIP, campaignId, type) > 0)
            {
                IncreaseCreditsUsed(campaignId, costPerPopupView);
                AddLog(campaignId, objectId, udc.UserID, word, type);
            }

            return campaignContent;
        }

        internal static List<string> GetCampaignObjectIds(Guid objectId)
        {
            List<string> campaignObjectIds = new List<string>();
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_CampaignObjects_LoadIds";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@ObjectId"].Value = objectId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    for (int i = 1; i <= int.Parse(sqlDataReader["FAO_NumberPopupLinks"].ToString()); i++)
                    {
                        campaignObjectIds.Add(sqlDataReader["FAC_ID"].ToString() + "_" + sqlDataReader["OBJ_ID"].ToString() + "_" + sqlDataReader["FAO_Word"].ToString() + "_" + i);
                    }
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }
            return campaignObjectIds;
        }

        /// <summary>
        /// Delete all campaign object entries for a given object id
        /// </summary>
        /// <param name="objectId">A unique object id</param>
        internal static void ResetCampaignObjects(Guid objectId)
        {
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_CampaignObjects_Delete";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@ObjectId"].Value = objectId;
                sqlConnection.Command.ExecuteNonQuery();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Add campaign object entry
        /// </summary>
        /// <param name="campaignId">A unique campaign id</param>
        /// <param name="objectId">A unique object id</param>
        /// <param name="word">The filter word</param>
        /// <param name="numberPopupLinks">Number of popup links for this campaign object</param>
        internal static void AddToCampaignObjects(Guid campaignId, Guid objectId, string word, int numberPopupLinks)
        {
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_CampaignObjects_Insert";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@ObjectId"].Value = objectId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Word", SqlDbType.NVarChar));
                sqlConnection.Command.Parameters["@Word"].Value = word;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@NumberPopupLinks", SqlDbType.Int));
                sqlConnection.Command.Parameters["@NumberPopupLinks"].Value = numberPopupLinks;
                sqlConnection.Command.ExecuteNonQuery();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static int AddView(Guid userId, string ip, Guid campaignId, string type)
        {
            int viewAdded = 0;
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_AddView";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@USR_ID", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@USR_ID"].Value = userId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@FAC_ID", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@FAC_ID"].Value = campaignId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar));
                sqlConnection.Command.Parameters["@Type"].Value = type;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserTimeSpanSecond", SqlDbType.Int));
                sqlConnection.Command.Parameters["@UserTimeSpanSecond"].Value = FilterEngine.GetFilterEngineConfig().UserViewTimeSpan == 0 ? 990000000 : FilterEngine.GetFilterEngineConfig().UserViewTimeSpan;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@IPTimeSpanSecond", SqlDbType.Int));
                sqlConnection.Command.Parameters["@IPTimeSpanSecond"].Value = FilterEngine.GetFilterEngineConfig().IPViewTimeSpan == 0 ? 990000000 : FilterEngine.GetFilterEngineConfig().IPViewTimeSpan;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@IP", SqlDbType.Char));
                sqlConnection.Command.Parameters["@IP"].Value = ip;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ViewAdded", SqlDbType.Bit));
                sqlConnection.Command.Parameters["@ViewAdded"].Direction = ParameterDirection.ReturnValue;
                sqlConnection.Command.ExecuteNonQuery();
                viewAdded = (int)sqlConnection.Command.Parameters["@ViewAdded"].Value;
            }
            finally
            {
                sqlConnection.Close();
            }
            return viewAdded;
        }

        internal static void AddLog(Guid campaignId, Guid? objectId, Guid userId, string word, string type)
        {
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_AddLog";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@ObjectId"].Value = objectId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Word", SqlDbType.NVarChar));
                sqlConnection.Command.Parameters["@Word"].Value = word;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar));
                sqlConnection.Command.Parameters["@Type"].Value = type;
                sqlConnection.Command.ExecuteNonQuery();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal static void IncreaseCreditsUsed(Guid campaignId, float creditsUsed)
        {
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_IncreaseCreditsUsed";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@FAC_ID", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@FAC_ID"].Value = campaignId;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@Credits", SqlDbType.Float));
                sqlConnection.Command.Parameters["@Credits"].Value = creditsUsed;
                sqlConnection.Command.Parameters.Add(new SqlParameter("@AvailableCredits", SqlDbType.Float));
                sqlConnection.Command.Parameters["@AvailableCredits"].Direction = ParameterDirection.Output;
                sqlConnection.Command.ExecuteNonQuery();
                float availableCredits = float.Parse(sqlConnection.Command.Parameters["@AvailableCredits"].Value.ToString());
                if (availableCredits < 0)
                {
                    AdWordFilterJob adWordFilterJob = new AdWordFilterJob();
                    adWordFilterJob.ProcessDataObjectsInCampaign(campaignId);
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal static bool CreditsLeft(Guid campaignId, AdWordFilterActions action)
        {
            float availableCredits = 0;
            float costPerLinkClick = 0;
            float costPerPopupLinkClick = 0;

            List<string> campaignObjectIds = new List<string>();
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdCampaigns_LoadById";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@CampaignId"].Value = campaignId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                {
                    availableCredits = float.Parse(sqlDataReader["FAC_Credits"].ToString()) - float.Parse(sqlDataReader["FAC_CreditsUsed"].ToString());
                    costPerLinkClick = float.Parse(sqlDataReader["FAC_CostPerLinkClick"].ToString());
                    costPerPopupLinkClick = float.Parse(sqlDataReader["FAC_CostPerPopupLinkClick"].ToString());
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }

            if (action == AdWordFilterActions.Link && availableCredits > 0)
                return true;
            else if (action == AdWordFilterActions.Popup && availableCredits > 0)
                return true;
            else
                return false;
        }

        internal static bool UserWantsAds(Guid userId)
        {
            bool userWantsAds = false;

            List<string> campaignObjectIds = new List<string>();
            SqlConnectionHelper sqlConnection = new SqlConnectionHelper();
            System.Data.SqlClient.SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Command.CommandText = "hisp_Filter_AdWords_CheckUser";
                sqlConnection.Command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier));
                sqlConnection.Command.Parameters["@UserId"].Value = userId;
                sqlDataReader = sqlConnection.Command.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader["UPD_DisplayAds"] != DBNull.Value)
                        userWantsAds = (bool)sqlDataReader["UPD_DisplayAds"];
                }
                sqlDataReader.Close();
            }
            finally
            {
                sqlConnection.Close();
            }

            return userWantsAds;
        }
    }
}