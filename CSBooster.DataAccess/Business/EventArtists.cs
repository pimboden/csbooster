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
    public static class EventArtists
    {
        /// <summary>
        /// Load Artist thatmacthes the Parameters... All Parameters are nullable execpt Amount
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
        public static List<EventArtist> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount)
        {
            return Data.EventArtists.Load(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
        }

        public static List<EventArtist> Load(Guid? Id, string Name, int? Type, string CountryISOCode, string Region, string ZIP, string City, int amount, int pageNumber, int pageSize, out int pageTotal)
        {
            List<EventArtist> list = Data.EventArtists.Load(Id, Name, Type, CountryISOCode, Region, ZIP, City, amount);
            return DoPaging(list, pageNumber, pageSize, out pageTotal);
        }

        public static List<Business.EventArtist> LoadAttached(Guid EventId, int amount)
        {
            return Data.EventArtists.LoadAttached(EventId, amount);
        }

        private static List<EventArtist> DoPaging(List<EventArtist> list, int pageNumber, int pageSize, out int pageTotal)
        {
            List<EventArtist> newList = new List<EventArtist>(pageSize);

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

    public class EventArtist
    {
        public EventArtist()
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

        private Nullable<Guid> artID;

        public Nullable<Guid> ID
        {
            get { return artID; }
            set { artID = value; }
        }

        private string artName;

        public string Name
        {
            get { return artName; }
            set { artName = value; }
        }

        private int artType;

        public int Type
        {
            get { return artType; }
            set { artType = value; }
        }

        private string artTypeName;

        public string TypeName
        {
            get { return artTypeName; }
            set { artTypeName = value; }
        }

        private string countryISOCode;

        public string CountryISOCode
        {
            get { return countryISOCode; }
            set { countryISOCode = value; }
        }

        private string artRegion;

        public string Region
        {
            get { return artRegion; }
            set { artRegion = value; }
        }

        private string artZip;

        public string ZIP
        {
            get { return artZip; }
            set { artZip = value; }
        }

        private string artCity;

        public string City
        {
            get { return artCity; }
            set { artCity = value; }
        }

        private Nullable<double> artGeoLat;

        public Nullable<double> GeoLat
        {
            get { return artGeoLat; }
            set { artGeoLat = value; }
        }

        private Nullable<double> artGeoLong;

        public Nullable<double> GeoLong
        {
            get { return artGeoLong; }
            set { artGeoLong = value; }
        }

        private string artURL;

        public string URL
        {
            get { return artURL; }
            set { artURL = value; }
        }

        private string artCommunityURL;

        public string CommunityURL
        {
            get { return artCommunityURL; }
            set { artCommunityURL = value; }
        }

        public string FormatedArtist
        {
            get
            {
                string strRet = string.Empty;
                if (!string.IsNullOrEmpty(artName))
                    strRet = string.Concat(artName, ", ");

                strRet += string.Concat(artZip, " ", artCity).Trim();
                strRet = strRet.TrimEnd(new char[2] {',', ' '});

                if (!string.IsNullOrEmpty(artURL))
                    return string.Format("<a target='_blank' href='{0}'>{1}</a>", artURL, strRet);
                else
                    return strRet;
            }
        }

        /// <summary>
        /// Returns null if not found
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static EventArtist Load(Guid ID)
        {
            return Data.EventArtist.Load(ID);
        }

        public static void AttachToEvent(Guid ArtistId, Guid EventId)
        {
            Data.EventArtist.AttachToEvent(ArtistId, EventId);
        }

        public static void DetachFromEvent(Guid ArtistId, Guid EventId)
        {
            Data.EventArtist.DetachFromEvent(ArtistId, EventId);
        }

        public void AttachToEvent(Guid EventId)
        {
            Data.EventArtist.AttachToEvent(ID.Value, EventId);
        }

        public void DetachFromEvent(Guid EventId)
        {
            Data.EventArtist.DetachFromEvent(ID.Value, EventId);
        }

        public void Insert()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Name must be set");
            }
            Data.EventArtist.Insert(this);
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
            Data.EventArtist.Update(this);
        }
    }
}