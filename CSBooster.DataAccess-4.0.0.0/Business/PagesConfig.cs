// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public class PagesConfig
    {
        public static HitblPagePag CreateNewPage(Guid communityId, string pageType, string page, string pageTitle)
        {
            int newPageOrder = (int)SPs.HispCommunityGetMaxPageOrder(communityId).ExecuteScalar() + 1;

            HitblPagePag newPage = new HitblPagePag();
            newPage.CtyId = communityId;
            newPage.PagCreatedDate = DateTime.Now;
            newPage.PagId = Guid.NewGuid();
            newPage.PagLastUpdate = DateTime.Now;
            newPage.PagTitle = pageTitle;
            newPage.PagOrderNr = newPageOrder;
            newPage.Save();

            Community.CreateDefaultWidgets(newPage.PagId, pageType, page, "de-CH");

            return newPage;
        }

        public static void DeletePage(Guid pageId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());

            wdc.hisp_Community_DeletePage(pageId);
        }
    }
}