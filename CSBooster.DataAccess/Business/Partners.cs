using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class Partners
    {
        private List<hitbl_Partners_PAR> partners;

        private Partners()
        {
            CSBooster_DataContext cdc = new CSBooster_DataContext();
            partners = (from partner in cdc.hitbl_Partners_PARs select partner).ToList();
        }

        public static Partners GetInstance()
        {
            if (HttpRuntime.Cache["Partners"] == null)
            {
                HttpRuntime.Cache.Insert("Partners", new Partners());
            }
            return (Partners)HttpRuntime.Cache["Partners"];
        }

        public string GetPartnerName(string communityId)
        {
            hitbl_Partners_PAR partner = partners.Find(x => x.CTY_ID.ToString() == communityId);
            if (partner != null)
                return partner.PAR_Name;
            else
                return null;
        }

        public hitbl_Partners_PAR GetPartner(Guid communityId)
        {
            hitbl_Partners_PAR partner = partners.Find(x => x.CTY_ID == communityId);
            if (partner != null)
                return partner;
            else
                return null;
        }

        public List<hitbl_Partners_PAR> GetPartners()
        {
            return partners;
        }

        public List<Guid> GetPartnerCommunities()
        {
            List<Guid> communities = new List<Guid>();
            foreach (hitbl_Partners_PAR partner in partners)
            {
                communities.Add(partner.CTY_ID);
            }
            return communities;
        }
    }
}
