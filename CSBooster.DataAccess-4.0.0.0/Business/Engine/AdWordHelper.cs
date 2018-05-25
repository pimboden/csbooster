// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class AdWordHelper
    {
        /// <summary>
        /// Get the banner for the given page name
        /// </summary>
        /// <param name="pageName">A unique page name</param>
        /// <returns>Banner HTML string</returns>
        public static string GetBanner(string pageName, UserDataContext udc)
        {
            return Data.AdWordHelper.GetBanner(pageName, udc);
        }

        /// <summary>
        /// Get the campaign url for a give campaign id
        /// Also logs clicks and stores credits
        /// </summary>
        /// <param name="campaignId">A unique campaign id</param>
        /// <param name="objectId">A unique object id</param>
        /// <param name="userId">A unique user id</param>
        /// <param name="word">Ad word</param>
        /// <param name="type">Link or Popup</param>
        /// <returns>Url string</returns>
        public static string GetCampaignUrl(Guid campaignId, Guid? objectId, UserDataContext udc, string word, string type)
        {
            return Data.AdWordHelper.GetCampaignUrl(campaignId, objectId, udc, word, type);
        }

        /// <summary>
        /// Get the content for a give campaign id
        /// </summary>
        /// <param name="campaignId">A unique campaign id</param>
        /// <returns>Content HTML string</returns>
        public static string GetCampaignContent(Guid campaignId, Guid objectId, UserDataContext udc, string word, string type)
        {
            return Data.AdWordHelper.GetCampaignContent(campaignId, objectId, udc, word, type);
        }

        /// <summary>
        /// Get a list of html node ids for the give object id (popup links)
        /// </summary>
        /// <param name="objectId">A unique object id</param>
        /// <returns>A list of strings in the format 'campaignId_linkId'</returns>
        public static List<string> GetCampaignObjectIds(Guid objectId)
        {
            return Data.AdWordHelper.GetCampaignObjectIds(objectId);
        }
    }
}