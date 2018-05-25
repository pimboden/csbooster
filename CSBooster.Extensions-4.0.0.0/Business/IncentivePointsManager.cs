using System;
using System.Collections.Generic;
using System.Data;
using _4screen.CSB.Common;
using _4screen.CSB.Extensions.Data;

namespace _4screen.CSB.Extensions.Business
{
    public class IncentivePointsManager
    {
        //Synchronous Operations
        public static void AddIncentivePointEvent(string rule, string description, UserDataContext userDataContext)
        {
            AddIncentivePointEvent(rule, description, userDataContext, string.Empty);
        }

        public static void AddIncentivePointEvent(string rule, string description, UserDataContext userDataContext, string objectID)
        {
            if (userDataContext.CurrentContext != null)
            {
                string pageURL = userDataContext.CurrentContext.Request.Url.AbsoluteUri;
                if (rule.Length > 0)
                {
                    string outDesc;
                    string outTimeSpan;
                    string outPointType;
                    int intPointsToAdd = CacheData.TrackIncentivePoints(rule, pageURL, userDataContext.UserRole, out outDesc, out outTimeSpan, out outPointType);
                    if (intPointsToAdd != 0)
                        DoAddIncentivePoints(intPointsToAdd, pageURL, rule, description, outTimeSpan, outPointType, userDataContext, objectID);
                }
            }
        }

        public static void AddIncentivePointEvent(string rule, UserDataContext userDataContext)
        {
            AddIncentivePointEvent(rule, userDataContext, string.Empty);
        }

        public static void AddIncentivePointEvent(string rule, UserDataContext userDataContext, string objectID)
        {
            if (userDataContext.CurrentContext != null)
            {
                string pageURL = userDataContext.CurrentContext.Request.Url.AbsoluteUri;
                if (rule.Length > 0)
                {
                    string outDesc;
                    string outTimeSpan;
                    string outPointType;
                    int intPointsToAdd = CacheData.TrackIncentivePoints(rule, pageURL, userDataContext.UserRole, out outDesc, out outTimeSpan, out outPointType);
                    if (intPointsToAdd != 0)
                        DoAddIncentivePoints(intPointsToAdd, pageURL, rule, outDesc, outTimeSpan, outPointType, userDataContext, objectID);
                }
            }
        }

        public static List<IncentivePoint> GetIncentivePointsHistory(Guid UserId)
        {
            List<IncentivePoint> ListIP = new List<IncentivePoint>();
            IDataReader idr = null;
            try
            {
                DLIncentivePointsManager objData = new DLIncentivePointsManager();
                idr = objData.GetIncentivePointsHistory(UserId);
                while (idr.Read())
                {
                    ListIP.Add(IncentivePoint.GetIncentivePoint(idr));
                }
                return ListIP;
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                {
                    idr.Close();
                }
            }
        }

        public static int GetIncentiveTotalPoints(Guid UserId)
        {
            DLIncentivePointsManager objData = new DLIncentivePointsManager();
            return objData.GetIncentiveTotalPoints(UserId);
        }

        public static void ClearHistory(Guid UserId)
        {
            DLIncentivePointsManager objData = new DLIncentivePointsManager();
            objData.ClearHistory(UserId);
        }

        //Add TheIncentivePoints
        private static void DoAddIncentivePoints(int pointsToAdd, string url, string rule, string description, string timestamp, string pointType, UserDataContext userDataContext, string objectID)
        {
            DLIncentivePointsManager objData = new DLIncentivePointsManager();
            objData.DoAddIncentivePoints(pointsToAdd, url, rule, description, timestamp, pointType, userDataContext.UserID, objectID);
        }
    }

    public class IncentivePoint
    {
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            internal set { _ID = value; }
        }

        private Guid _UserID;

        public Guid UserID
        {
            get { return _UserID; }
            internal set { _UserID = value; }
        }

        private int _TotalPoints;

        public int TotalPoints
        {
            get { return _TotalPoints; }
            internal set { _TotalPoints = value; }
        }

        private int _PointsAdded;

        public int PointsAdded
        {
            get { return _PointsAdded; }
            internal set { _PointsAdded = value; }
        }

        private DateTime _DateInserted;

        public DateTime DateInserted
        {
            get { return _DateInserted; }
            internal set { _DateInserted = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            internal set { _Description = value; }
        }

        private string _PointType;

        public string PointType
        {
            get { return _PointType; }
            internal set { _PointType = value; }
        }

        private string _URL;

        public string URL
        {
            get { return _URL; }
            internal set { _URL = value; }
        }

        private string _RuleName;

        public string RuleName
        {
            get { return _RuleName; }
            internal set { _RuleName = value; }
        }

        internal static IncentivePoint GetIncentivePoint(IDataReader idr)
        {
            IncentivePoint newIncPoint = new IncentivePoint();
            newIncPoint.DateInserted = Convert.ToDateTime(idr["IPN_Inserted"]);
            newIncPoint.Description = idr["IPN_Description"] != DBNull.Value ? idr["IPN_Description"].ToString() : string.Empty;
            newIncPoint.ID = new Guid(idr["IPN_PointsId"].ToString());
            newIncPoint.PointsAdded = Convert.ToInt32(idr["IPN_PointsLastAdded"]);
            newIncPoint.RuleName = idr["IPN_Rule"] != DBNull.Value ? idr["IPN_Rule"].ToString() : string.Empty;
            newIncPoint.TotalPoints = Convert.ToInt32(idr["IPN_PointsTotal"]);
            newIncPoint.PointType = idr["IPN_PointsTypeLastAdded"].ToString();
            newIncPoint.URL = idr["IPN_URL"] != DBNull.Value ? idr["IPN_URL"].ToString() : string.Empty;
            newIncPoint.UserID = new Guid(idr["UserID"].ToString());
            return newIncPoint;
        }
    }
}