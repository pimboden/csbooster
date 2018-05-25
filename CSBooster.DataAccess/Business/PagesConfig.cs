//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;

namespace _4screen.CSB.DataAccess.Business
{
    public class PagesConfig
    {
        public PagesConfig()
        {
        }

        public static HitblPagePag CreateNewPage(Guid communityID, string pageType, string page, string pageTitle)
        {
            int newPageOrder = (int)SPs.HispCommunityGetMaxPageOrder(communityID).ExecuteScalar() + 1;

            HitblPagePag NewPage = new HitblPagePag();
            NewPage.CtyId = communityID;
            NewPage.PagCreatedDate = DateTime.Now;
            NewPage.PagId = Guid.NewGuid();
            NewPage.PagLastUpdate = DateTime.Now;
            NewPage.PagTitle = pageTitle;
            NewPage.PagOrderNr = newPageOrder;
            NewPage.Save();
            Community.CreateDefaultWidgets(NewPage.PagId, pageType, page, "de-CH");

            return NewPage;
        }

        public static void DeletePage(Guid PageId)
        {
            SPs.HispCommunityDeletePage(PageId).Execute();
        }

        /// <summary>
        /// Gets the current Page of the community and set it in the user settings
        /// If the user has already visitetd the page it will be read form his settings, else page id with order 1 will come 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static HitblPagePag GetCurrPage(Guid communityID, Guid userId)
        {
            Guid fisrtPageID = (Guid)SPs.HispCommunityGetCurrPageID(communityID, userId).ExecuteScalar();
            return HitblPagePag.FetchByID(fisrtPageID);
        }

        /// <summary>
        /// Gets the first Page of the community (The non deletable page with sort order 1)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static HitblPagePag GetFirstPage(Guid communityID)
        {
            Guid fisrtPageID = (Guid)SPs.HispCommunityGetFirstPageID(communityID).ExecuteScalar();
            return HitblPagePag.FetchByID(fisrtPageID);
        }
    }
}