// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Xsl;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebServices
{
    [WebService(Namespace = "http://www.4screen.ch/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class GoogleMapServices : System.Web.Services.WebService
    {
        public GoogleMapServices()
        {
        }

        [WebMethod]
        public string LoadMarkers(Dictionary<string, string> objectTypesAndTags)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var objectTypeAndTags in objectTypesAndTags)
            {
                QuickParameters quickParameters = new QuickParameters();
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.ObjectType = Common.Helper.GetObjectTypeNumericID(objectTypeAndTags.Key);
                quickParameters.Tags1 = QuickParameters.GetDelimitedTagIds(objectTypeAndTags.Value, ',');
                quickParameters.Amount = 500;
                quickParameters.DisablePaging = true;
                quickParameters.OnlyGeoTagged = true;
                quickParameters.ShowState = ObjectShowState.Published;
                quickParameters.QuerySourceType = QuerySourceType.Page;
                var dataObjects = DataObjects.Load<DataObject>(quickParameters);
                foreach (var dataObject in dataObjects.DistinctBy((x, y) => x.Geo_Lat + x.Geo_Long == y.Geo_Lat + y.Geo_Long))
                {
                    string tag = string.Empty;
                    foreach (var currentTag in objectTypeAndTags.Value.Split(','))
                    {
                        if (!string.IsNullOrEmpty(currentTag) && dataObject.TagList.ToLower().Contains(currentTag.ToLower()))
                            tag = "_" + currentTag.ToLower();
                    }
                    sb.AppendLine(string.Format("AddMapMarker({0}, {1}, '{2}', '{3}', '{4}');", dataObject.Geo_Lat, dataObject.Geo_Long, tag, dataObject.ObjectID, dataObject.ObjectType));
                }
            }

            return sb.ToString();
        }

        [WebMethod]
        public object LoadMarkerDetails(string objectId, string latitude, string longitude, List<string> objectTypes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var objectType in objectTypes)
            {
                QuickParameters quickParameters = new QuickParameters();
                quickParameters.Udc = UserDataContext.GetUserDataContext();
                quickParameters.ObjectType = Common.Helper.GetObjectTypeNumericID(objectType);
                quickParameters.Amount = 10;
                quickParameters.DisablePaging = true;
                quickParameters.GeoLat = float.Parse(latitude);
                quickParameters.GeoLong = float.Parse(longitude);
                quickParameters.DistanceKm = 0.001f;
                quickParameters.ShowState = ObjectShowState.Published;
                quickParameters.QuerySourceType = QuerySourceType.Page;
                var dataObjects = DataObjects.LoadByReflection(quickParameters);
                foreach (var dataObject in dataObjects)
                {
                    XsltArgumentList argumentList = new XsltArgumentList();
                    argumentList.AddParam("SiteUrl", string.Empty, SiteConfig.SiteURL);
                    argumentList.AddParam("MediaUrl", string.Empty, SiteConfig.MediaDomainName);
                    argumentList.AddParam("DetailLink", string.Empty, Common.Helper.GetDetailLink(dataObject.ObjectType, dataObject.ObjectID.ToString()));
                    sb.Append(dataObject.GetOutput("Geo", SiteConfig.SiteURL + ConfigurationManager.AppSettings["XsltTemplateURLS"], argumentList));
                }
            }

            if (sb.Length > 0)
                return new {ObjectId = objectId, Content = sb.ToString()};
            else
                return new {ObjectId = objectId, Content = "Loading error"};
        }
    }
}