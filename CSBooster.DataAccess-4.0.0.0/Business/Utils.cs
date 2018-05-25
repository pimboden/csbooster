// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.GeoTagging;

namespace _4screen.CSB.DataAccess.Business
{
    public class Utils
    {
        public static void SetPictureRelationsFromContent(string content, DataObject dataObject, string relationType, bool copyFirstImage)
        {
            DataObject.RelDelete(new RelationParams() { ParentObjectID = dataObject.ObjectID, ParentObjectType = Helper.GetObjectTypeNumericID("Article"), ChildObjectType = Helper.GetObjectTypeNumericID("Picture") });

            MatchCollection images = Regex.Matches(content, @"<img(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            int insertNumber = 0;
            foreach (Match match in images)
            {
                Match idMatch = Regex.Match(match.Groups[1].Value, @"id="".*?_(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                //Match pvMatch = Regex.Match(match.Groups[1].Value, @"pv=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                //Match pvpMatch = Regex.Match(match.Groups[1].Value, @"pvp=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                if (idMatch.Success)
                {
                    Guid objectId = idMatch.Groups[1].Value.ToGuid();
                    //PictureVersion pictureVersion = (PictureVersion)Enum.Parse(typeof(PictureVersion), pvMatch.Groups[1].Value);
                    //string popupVersion = pvpMatch.Groups[1].Value;

                    DataObject.RelInsert(new RelationParams()
                    {
                        ParentObjectID = dataObject.ObjectID,
                        ParentObjectType = Helper.GetObjectTypeNumericID("Article"),
                        ChildObjectID = objectId,
                        ChildObjectType = Helper.GetObjectTypeNumericID("Picture"),
                        RelationType = relationType
                    }, insertNumber++);
                }
            }

            if (copyFirstImage)
            {
                DataObjectList<DataObjectPicture> pictures = DataObjects.Load<DataObjectPicture>(new QuickParameters
                        {
                            Udc = UserDataContext.GetUserDataContext(),
                            ShowState = null,
                            RelationParams = new RelationParams
                            {
                                ParentObjectID = dataObject.ObjectID.Value
                            }
                        }
                    );
                if (pictures.Count > 0)
                {
                    string mediaSource = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], pictures[0].UserID, pictures[0].ObjectID);
                    string mediaTarget = string.Format(@"{0}\{1}\P\{{0}}\{2}.jpg", ConfigurationManager.AppSettings["ConverterRootPathMedia"], dataObject.UserID, dataObject.ObjectID);

                    foreach (var pictureFormat in pictures[0].PictureFormats)
                    {
                        if (!string.IsNullOrEmpty(pictureFormat.Value))
                        {
                            dataObject.SetImageType(pictureFormat.Key, (PictureFormat)Enum.Parse(typeof(PictureFormat), pictureFormat.Value));
                            File.Copy(string.Format(mediaSource, pictureFormat.Key), string.Format(mediaTarget, pictureFormat.Key), true);
                        }
                    }

                    dataObject.Image = dataObject.ObjectID.Value.ToString();
                }
            }
        }

        public static Dictionary<Guid, PictureVersion> GetPicturesFromContent(string content, out string idPrefix)
        {
            idPrefix = string.Empty;
            Dictionary<Guid, PictureVersion> pictures = new Dictionary<Guid, PictureVersion>();

            MatchCollection images = Regex.Matches(content, @"<img(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match match in images)
            {
                Match idMatch = Regex.Match(match.Groups[1].Value, @"id=""(.*?)_(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                //Match pvMatch = Regex.Match(match.Groups[1].Value, @"pv=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match pvpMatch = Regex.Match(match.Groups[1].Value, @"pvp=""(.*?)""", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                Guid? objectId = null;
                PictureVersion popupVersion = PictureVersion.L;
                if (idMatch.Success)
                {
                    idPrefix = idMatch.Groups[1].Value;
                    objectId = idMatch.Groups[2].Value.ToGuid();
                }
                if (pvpMatch.Success)
                {
                    popupVersion = string.IsNullOrEmpty(pvpMatch.Groups[1].Value) ? PictureVersion.L : (PictureVersion)Enum.Parse(typeof(PictureVersion), pvpMatch.Groups[1].Value);
                }
                if (objectId.HasValue && popupVersion != PictureVersion.None)
                {
                    pictures.Add(objectId.Value, popupVersion);
                }
            }

            return pictures;
        }

        public static void StoreGeoPointInDB(GeoPoint geoPoint)
        {
            Data.CSBooster_DataContext csbDAC = new _4screen.CSB.DataAccess.Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
            csbDAC.hisp_GeoKoordinates_Save(geoPoint.StreetAndNumber, geoPoint.ZipCode, geoPoint.City, geoPoint.CountryCode, geoPoint.Region, geoPoint.Lat, geoPoint.Long);
        }

        public static GeoPoint GetGeoPointFromDB(string streetAndNumber, string zipCode, string city, string countryCode)
        {
            Data.CSBooster_DataContext csbDAC = new _4screen.CSB.DataAccess.Data.CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var cachedGeoPoint = csbDAC.hisp_GeoKoordinates_Get(streetAndNumber, zipCode, city, countryCode).ElementAtOrDefault(0);
            if (cachedGeoPoint != null)
            {
                GeoPoint geoPoint = new GeoPoint() { StreetAndNumber = cachedGeoPoint.GEO_Street, ZipCode = cachedGeoPoint.GEO_PLZ, City = cachedGeoPoint.GEO_City, Region = cachedGeoPoint.GEO_Kanton, CountryCode = cachedGeoPoint.GEO_Land, Lat = cachedGeoPoint.GEO_Breite, Long = cachedGeoPoint.GEO_Laenge, };
                return geoPoint;
            }
            else
            {
                return null;
            }
        }

        public static XmlDocument LoadWidgetInstanceSettings(Guid instanceId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            string xml = wdc.hisp_Widget_LoadInstanceData(instanceId).ElementAtOrDefault(0).INS_XmlStateData;
            XmlDocument xmlDocument = new XmlDocument();
            if (!string.IsNullOrEmpty(xml))
                xmlDocument.LoadXml(xml);
            else
                XmlHelper.CreateRoot(xmlDocument, "root");
            return xmlDocument;
        }

        public static bool SaveWidgetInstanceSettings(Guid instanceId, XmlDocument xmlDocument)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            int status = wdc.hisp_Widget_SaveInstanceData(instanceId, xmlDocument.OuterXml);
            return status == 0 ? true : false;
        }

        public static string LoadWidgetTitle(Guid instanceId)
        {
            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstanceText_WITs.Where(x => x.INS_ID == instanceId) select widgInstances).FirstOrDefault();
            return widgetInstance.WIT_Title;
        }

        public static bool SaveWidgetTitle(Guid instanceId, string title)
        {
            try
            {
                CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstanceText_WITs.Where(x => x.INS_ID == instanceId) select widgInstances).FirstOrDefault();
                widgetInstance.WIT_Title = title;
                dataContext.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }

        public static Guid GetCommunityIdFromWidgetInstance(Guid widgetInstanceId)
        {
            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            Guid communityId = (from widgetInstance in dataContext.hitbl_WidgetInstance_INs
                                join widgetPage in dataContext.hitbl_Page_PAGs on widgetInstance.INS_PAG_ID equals widgetPage.PAG_ID
                                where widgetInstance.INS_ID == widgetInstanceId
                                select widgetPage.CTY_ID).SingleOrDefault();
            return communityId;
        }

        public static Guid GetCommunityIdFromPage(Guid pageId)
        {
            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            Guid communityId = (from widgetPage in dataContext.hitbl_Page_PAGs where widgetPage.PAG_ID == pageId select widgetPage.CTY_ID).SingleOrDefault();
            return communityId;
        }
    }
}
