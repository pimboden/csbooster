// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class Partner
    {
        public Guid PartnerID { get; set; }
        public string PartnerName { get; set; }
        public Guid CommunityID { get; set; }
        public string BaseUrlXSLT { get; set; }
        public string MapConfig { get; set; }
        public int GeoServiceCalls { get; set; }
        public int RESTCalls { get; set; }
        public int MonthlyGeoServiceCredits { get; set; }
        public int MonthlyRESTCredits { get; set; }
        public DateTime LastResetDate { get; set; }
        public string MobileHeader { get; set; }
        public string MobileFooter { get; set; }
        public Dictionary<Guid, DataObjectUser> Users { get; set; }
        public DataObjectUser CurrentUser { get; set; }

        /// <summary>
        /// Get a partner for a specific user (specify userID only)
        /// Get a partner with all users (specify partnerID only)
        /// </summary>
        public static Partner Get(Guid? partnerID, Guid? userID)
        {
            Data.CSBooster_DataContext dc = new Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var partnerResult = dc.hisp_Partners_Load(partnerID, userID).ToList();
            if (partnerResult.Count > 0)
            {
                Partner partner = new Partner();
                partner.PartnerID = partnerResult[0].PAR_ID;
                partner.PartnerName = partnerResult[0].PAR_Name;
                partner.CommunityID = partnerResult[0].CTY_ID;
                partner.BaseUrlXSLT = partnerResult[0].PAR_BaseUrlXSLT;
                partner.MapConfig = partnerResult[0].PAR_MapConfig;
                partner.GeoServiceCalls = partnerResult[0].PAR_GeoServiceCalls;
                partner.RESTCalls = partnerResult[0].PAR_RESTCalls;
                partner.MonthlyGeoServiceCredits = partnerResult[0].PAR_MonthlyGeoServiceCredits;
                partner.MonthlyRESTCredits = partnerResult[0].PAR_MonthlyRESTCredits;
                partner.LastResetDate = partnerResult[0].PAR_LastResetDate;
                partner.MobileHeader = partnerResult[0].PAR_MobileHeader;
                partner.MobileFooter = partnerResult[0].PAR_MobileFooter;
                partner.Users = new Dictionary<Guid, DataObjectUser>();
                for (int i = 0; i < partnerResult.Count; i++)
                {
                    DataObjectUser user = DataObject.Load<DataObjectUser>(partnerResult[i].USR_ID);
                    if (user.State != ObjectState.Added)
                    {
                        partner.Users.Add(user.ObjectID.Value, user);
                        if (i == 0)
                            partner.CurrentUser = user;
                    }
                }
                return partner;
            }
            else
            {
                return null;
            }
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            if (now > LastResetDate.GetEndOfMonth())
            {
                GeoServiceCalls = 0;
                RESTCalls = 0;
                LastResetDate = now;
            }

            Data.CSBooster_DataContext dc = new Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
            dc.hisp_Partners_Update(PartnerID, PartnerName, CommunityID, BaseUrlXSLT, MapConfig, GeoServiceCalls, RESTCalls, MonthlyGeoServiceCredits, MonthlyRESTCredits, LastResetDate, MobileHeader, MobileFooter);
        }
    }
}