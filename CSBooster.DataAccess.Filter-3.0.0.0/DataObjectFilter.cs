// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Filter
{
    public class DataObjectFilter : IDataObjectFilter
    {
        public void InsertFilter(DataObject dataObject)
        {

        }

        public void UpdateFilter(DataObject dataObject, DataObject newDataObject)
        {
            dataObject.Title = newDataObject.Title;
            dataObject.StartDate = newDataObject.StartDate;
            if (newDataObject.EndDate != DateTime.MinValue)
                dataObject.EndDate = newDataObject.EndDate;
            dataObject.Description = newDataObject.Description;
            dataObject.Copyright = newDataObject.Copyright;
            dataObject.TagList = newDataObject.TagList;
            if (newDataObject.Featured < dataObject.Featured)
                dataObject.Featured = newDataObject.Featured;
            dataObject.Street = newDataObject.Street;
            dataObject.Zip = newDataObject.Zip;
            dataObject.City = newDataObject.City;
            dataObject.CountryCode = newDataObject.CountryCode;
            dataObject.Geo_Lat = newDataObject.Geo_Lat;
            dataObject.Geo_Long = newDataObject.Geo_Long;
            dataObject.UrlXSLT = newDataObject.UrlXSLT;
        }
    }
}
