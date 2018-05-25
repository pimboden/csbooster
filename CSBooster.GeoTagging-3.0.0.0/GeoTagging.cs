using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;
using System.Net;
using _4screen.CSB.GeoTagging.MapPointService;

namespace _4screen.CSB.GeoTagging
{
    public class GeoPoint
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public string StreetAndNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
    }

    public static class GeoTagging
    {
        /// <summary>
        /// Returns a Filed GeoPintClass  or  null if the address could not be resolved
        /// </summary>
        /// <param name="address"></param>
        /// <param name="plz"></param>
        /// <param name="city"></param>
        /// <param name="region"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static GeoPoint GetGeoPosition(string address, string plz, string city, string region, string country)
        {
            if (ConfigurationManager.AppSettings["GeoTaggingProvider"] == "Google")
            {
                return GetGeoPositionGoogle(address, plz, city, region, country);
            }
            else if (ConfigurationManager.AppSettings["GeoTaggingProvider"] == "Microsoft")
            {
                return GetGeoPositionMicrosoft(address, plz, city, region, country);
            }
            else
            {
                return null;
            }
        }

        private static GeoPoint GetGeoPositionGoogle(string address, string plz, string city, string region, string country)
        {
            GeoPoint geoPoint = null;

            if (string.IsNullOrEmpty(country))
                country = "Schweiz";

            List<string> addressString = new List<string>();
            if (!string.IsNullOrEmpty(address)) addressString.Add(address);
            if (!string.IsNullOrEmpty(plz) && !string.IsNullOrEmpty(city)) addressString.Add(plz + " " + city);
            else if (!string.IsNullOrEmpty(plz)) addressString.Add(plz);
            else if (!string.IsNullOrEmpty(city)) addressString.Add(city);
            if (!string.IsNullOrEmpty(region)) addressString.Add(region);
            if (!string.IsNullOrEmpty(country)) addressString.Add(country);
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://maps.google.com/maps/geo?q={0}&output=xml&key={1}", string.Join(", ", addressString.ToArray()), ConfigurationManager.AppSettings["GoogleMapKey"]));
                string responseXml = DownloadContent(request);
                if (!string.IsNullOrEmpty(responseXml))
                {
                    responseXml = System.Text.RegularExpressions.Regex.Replace(responseXml, "xmlns=\".*?\"", string.Empty);
                    XDocument response = XDocument.Parse(responseXml);
                    string statusCode = response.XPathSelectElement("//Status/code").Value;
                    var placemarks = response.Element("kml").Element("Response").Elements("Placemark");
                    if (placemarks.Count() == 1)
                    {
                        int accuracy = int.Parse(response.XPathSelectElement("//Placemark/AddressDetails").Attribute("Accuracy").Value);
                        if (statusCode == "200" && accuracy > 0)
                        {
                            geoPoint = new GeoPoint();

                            if (response.XPathSelectElement("//Placemark/AddressDetails/Country/CountryNameCode") != null)
                                geoPoint.CountryCode = response.XPathSelectElement("//Placemark/AddressDetails/Country/CountryNameCode").Value;

                            if (response.XPathSelectElement("//AdministrativeArea/AdministrativeAreaName") != null)
                                geoPoint.Region = response.XPathSelectElement("//AdministrativeArea/AdministrativeAreaName").Value;

                            if (response.XPathSelectElement("//Locality/LocalityName") != null)
                                geoPoint.City = response.XPathSelectElement("//Locality/LocalityName").Value;

                            if (response.XPathSelectElement("//Thoroughfare/ThoroughfareName") != null)
                                geoPoint.StreetAndNumber = response.XPathSelectElement("//Thoroughfare/ThoroughfareName").Value;

                            if (response.XPathSelectElement("//Locality/PostalCode/PostalCodeNumber") != null)
                                geoPoint.ZipCode = response.XPathSelectElement("//Locality/PostalCode/PostalCodeNumber").Value;

                            if (response.XPathSelectElement("//Placemark/Point/coordinates") != null)
                            {
                                string[] longLat = response.XPathSelectElement("//Placemark/Point/coordinates").Value.Split(',');
                                geoPoint.Long = double.Parse(longLat[0]);
                                geoPoint.Lat = double.Parse(longLat[1]);
                            }

                            return geoPoint;
                        }
                    }
                }
            }
            catch
            {
                return geoPoint;
            }
            return geoPoint;
        }

        private static GeoPoint GetGeoPositionMicrosoft(string address, string plz, string city, string region, string country)
        {
            GeoPoint geoPoint = null;
            FindServiceSoap findService = new FindServiceSoap();

            string myUserID = Properties.Settings.Default.MPUser;
            string myPassword = Properties.Settings.Default.MPPass;

            NetworkCredential myCredentials = new NetworkCredential(myUserID, myPassword);

            findService.Credentials = myCredentials;
            findService.PreAuthenticate = true;
            string addressLine = (address != null) ? address.Trim() : string.Empty;
            string zipCode = (plz != null) ? plz.Trim() : string.Empty;
            string stadt = (city != null) ? city.Trim() : string.Empty;
            string kanton = (region != null) ? region.Trim() : string.Empty;
            string land = (country != null) ? country.Trim() : string.Empty;
            string dataSourceName = "MapPoint.EU";

            if (land.Length == 0)
                land = "switzerland";
            if (kanton.Length == 0)
                kanton = stadt;

            FindOptions myFindOptions = new FindOptions();
            myFindOptions.ThresholdScore = 0.5;
            myFindOptions.Range = new FindRange();
            myFindOptions.Range.StartIndex = 0;
            myFindOptions.Range.Count = 1;
            //Try find exact address

            Address myAddress = new Address();
            myAddress.AddressLine = addressLine;
            myAddress.PrimaryCity = stadt;
            myAddress.SecondaryCity = string.Empty;
            myAddress.Subdivision = kanton;
            myAddress.PostalCode = zipCode;
            myAddress.CountryRegion = land;

            FindAddressSpecification findAddressSpec = new FindAddressSpecification();
            findAddressSpec.InputAddress = myAddress;
            findAddressSpec.Options = myFindOptions;

            FindSpecification findSpec = new FindSpecification();
            string inputPlace = string.Format("{0},{1},{2}", city, kanton, land);
            inputPlace = inputPlace.Replace(",,", "").TrimStart(',');
            findSpec.InputPlace = inputPlace;

            FindResults myFindResults = null;
            FindResult[] myResults = null;
            try
            {
                findAddressSpec.DataSourceName = dataSourceName;
                myFindResults = findService.FindAddress(findAddressSpec);
            }
            catch
            {
            }
            if (myFindResults != null && myFindResults.Results != null && myFindResults.Results.Count() > 0)
            {
                myResults = myFindResults.Results;
                if (myResults.Count() > 0)
                {
                    geoPoint = FillGeoPoint(myResults[0]);
                }
            }
            else
            {
                //Try finding by Location

                try
                {
                    findSpec.DataSourceName = dataSourceName;
                    myFindResults = findService.Find(findSpec);
                }
                catch
                {
                }
                if (myFindResults != null && myFindResults.Results != null && myFindResults.Results.Count() > 0)
                {
                    myResults = myFindResults.Results;
                    geoPoint = FillGeoPoint(myResults[0]);
                }
            }
            if (geoPoint == null)
            {
                //still nothing 
                //try it again with new datasource
                myFindResults = null;
                myResults = null;
                dataSourceName = "MapPoint.NA";
                try
                {
                    findAddressSpec.DataSourceName = dataSourceName;
                    myFindResults = findService.FindAddress(findAddressSpec);
                }
                catch
                {
                }
                if (myFindResults != null && myFindResults.Results != null && myFindResults.Results.Count() > 0)
                {
                    myResults = myFindResults.Results;
                    if (myResults.Count() > 0)
                    {
                    }
                }
                else
                {
                    //Try finding by Location

                    try
                    {
                        findSpec.DataSourceName = dataSourceName;
                        myFindResults = findService.Find(findSpec);
                    }
                    catch
                    {
                    }
                    if (myFindResults != null && myFindResults.Results != null && myFindResults.Results.Count() > 0)
                    {
                        myResults = myFindResults.Results;
                        geoPoint = FillGeoPoint(myResults[0]);
                    }
                }
            }
            return geoPoint;
        }

        private static GeoPoint FillGeoPoint(FindResult findResult)
        {
            GeoPoint geoPoint = new GeoPoint();
            geoPoint.Lat = findResult.FoundLocation.LatLong.Latitude;
            geoPoint.Long = findResult.FoundLocation.LatLong.Longitude;
            if (findResult.FoundLocation.Address != null)
            {
                geoPoint.StreetAndNumber = findResult.FoundLocation.Address.AddressLine;
                geoPoint.ZipCode = findResult.FoundLocation.Address.PostalCode;
                geoPoint.City = findResult.FoundLocation.Address.PrimaryCity;
                geoPoint.CountryCode = findResult.FoundLocation.Address.CountryRegion;
            }
            return geoPoint;
        }

        // DownloadContent hierher kopiert, um nicht mehr von Common abhängig zu sein
        private static string DownloadContent(HttpWebRequest request)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[8192];
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            try
            {
                string tempString = null;
                int count = 0;
                do
                {
                    count = responseStream.Read(buffer, 0, buffer.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.GetEncoding("UTF-8").GetString(buffer, 0, count);
                        sb.Append(tempString);
                    }
                }
                while (count > 0);
            }
            finally
            {
                responseStream.Close();
            }
            return sb.ToString();
        }
    }
}