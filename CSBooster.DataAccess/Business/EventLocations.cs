//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public static class EventLocations
    {
        /// <summary>
        /// Load Location thatmacthes the Parameters... All Parameters are nullable execpt Amount
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        /// <param name="CountryISOCode"></param>
        /// <param name="Region"></param>
        /// <param name="ZIP"></param>
        /// <param name="City"></param>
        /// <param name="Amount">0 = All</param>
        /// <returns></returns>
        public static List<EventLocation> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount)
        {
            return Data.EventLocations.Load(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
        }

        public static List<EventLocation> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount, int pageNumber, int pageSize, out int pageTotal)
        {
            List<EventLocation> list = Data.EventLocations.Load(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
            return DoPaging(list, pageNumber, pageSize, out pageTotal);
        }

        public static List<Business.EventLocation> LoadAttached(Guid EventId, int amount)
        {
            return Data.EventLocations.LoadAttached(EventId, amount);
        }

        private static List<EventLocation> DoPaging(List<EventLocation> list, int pageNumber, int pageSize, out int pageTotal)
        {
            List<EventLocation> newList = new List<EventLocation>(pageSize);

            pageTotal = list.Count/pageSize;
            if (list.Count > pageSize*pageTotal)
                pageTotal++;

            if (pageNumber > pageTotal)
                pageNumber = pageTotal;

            int intFrom = 0;
            if (list.Count > 0 && pageNumber > 0 && pageSize > 0)
                intFrom = (pageNumber - 1)*pageSize;

            for (int i = intFrom; i < list.Count; i++)
            {
                if (newList.Count == pageSize)
                    break;

                newList.Add(list[i]);
            }
            return newList;
        }
    }

    public class EventLocation
    {
        public EventLocation()
        {
            ID = null;
            Name = null;
            Type = 0;
            CountryISOCode = null;
            Region = null;
            ZIP = null;
            City = null;
            GeoLat = null;
            GeoLong = null;
            URL = null;
            CommunityURL = null;
        }

        private Nullable<Guid> locID;

        public Nullable<Guid> ID
        {
            get { return locID; }
            set { locID = value; }
        }

        private string locName;

        public string Name
        {
            get { return locName; }
            set { locName = value; }
        }

        private int locType;

        public int Type
        {
            get { return locType; }
            set { locType = value; }
        }

        private string locTypeName;

        public string TypeName
        {
            get { return locTypeName; }
            set { locTypeName = value; }
        }

        private string countryISOCode;

        public string CountryISOCode
        {
            get { return countryISOCode; }
            set { countryISOCode = value; }
        }

        private string locRegion;

        public string Region
        {
            get { return locRegion; }
            set { locRegion = value; }
        }

        private string locZip;

        public string ZIP
        {
            get { return locZip; }
            set { locZip = value; }
        }

        private string locCity;

        public string City
        {
            get { return locCity; }
            set { locCity = value; }
        }

        private Nullable<double> locGeoLat;

        public Nullable<double> GeoLat
        {
            get { return locGeoLat; }
            set { locGeoLat = value; }
        }

        private Nullable<double> locGeoLong;

        public Nullable<double> GeoLong
        {
            get { return locGeoLong; }
            set { locGeoLong = value; }
        }

        private string locURL;

        public string URL
        {
            get { return locURL; }
            set { locURL = value; }
        }

        private string locCommunityURL;

        public string CommunityURL
        {
            get { return locCommunityURL; }
            set { locCommunityURL = value; }
        }

        public string FormatedLocation
        {
            get
            {
                string strRet = string.Empty;
                if (!string.IsNullOrEmpty(locName))
                    strRet = string.Concat(locName, ", ");

                strRet += string.Concat(locZip, " ", locCity).Trim();
                strRet = strRet.TrimEnd(new char[1] {','});

                if (!string.IsNullOrEmpty(locURL))
                    return string.Format("<a target='_blank' href='{0}'>{1}</a>", locURL, strRet);
                else
                    return strRet;
            }
        }


        /// <summary>
        /// Returns null if not found
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static EventLocation Load(Guid ID)
        {
            return Data.EventLocation.Load(ID);
        }

        public static void AttachToEvent(Guid LocationId, Guid EventId)
        {
            Data.EventLocation.AttachToEvent(LocationId, EventId);
        }

        public static void DetachFromEvent(Guid LocationId, Guid EventId)
        {
            Data.EventLocation.DetachFromEvent(LocationId, EventId);
        }

        public void AttachToEvent(Guid EventId)
        {
            Data.EventLocation.AttachToEvent(ID.Value, EventId);
        }

        public void DetachFromEvent(Guid EventId)
        {
            Data.EventLocation.DetachFromEvent(ID.Value, EventId);
        }

        public void Insert()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Name must be set");
            }
            Data.EventLocation.Insert(this);
        }

        public void Update()
        {
            if (ID == null || ID == Guid.Empty)
            {
                throw new Exception("ID must be set");
            }

            else if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Name must be set");
            }
            Data.EventLocation.Update(this);
        }
    }
}