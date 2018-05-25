// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Extensions.Business;
using _4screen.CSB.GeoTagging;
using _4screen.Utils.Net;

namespace _4screen.CSB.WebServices
{
    public static class Helper
    {
        public static void SetResponseStatus(WebServiceLogEntry log, HttpStatusCode statusCode, string statusDescription)
        {
            statusDescription = statusDescription.Replace("\r\n", " ");
            WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
            WebOperationContext.Current.OutgoingResponse.StatusDescription = statusDescription;
            log.Result = statusCode.ToString();
            log.Message = statusDescription;
        }

        public static Partner GetCurrentPartner(WebServiceLogEntry log)
        {
            log.IP = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
            if (OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.IsAuthenticated)
            {
                MembershipUser partnerUser = Membership.GetUser(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name);
                Partner partner = Partner.Get(null, new Guid(partnerUser.ProviderUserKey.ToString()));
                if (partner != null)
                {
                    log.PartnerID = partner.PartnerID;
                    log.UserID = partner.CurrentUser.UserID;
                    DateTime now = DateTime.Now;
                    if (partner.RESTCalls < partner.MonthlyRESTCredits || now > partner.LastResetDate.GetEndOfMonth())
                    {
                        partner.RESTCalls++;
                        partner.Update();
                        return partner;
                    }
                    else
                    {
                        throw new RESTException(HttpStatusCode.Forbidden, "Too many rest service calls this month");
                    }
                }
                log.UserID = new Guid(partnerUser.ProviderUserKey.ToString());
            }
            throw new RESTException(HttpStatusCode.Forbidden, "Not an authorized partner");
        }

        public static bool GetImage(WebServiceLogEntry log, string url, int objectType, Guid userID, string imageName, out int width, out int height, out decimal aspectRatio)
        {
            log.FilesToDownload++;

            width = 0;
            height = 0;
            aspectRatio = 1.0m;

            try
            {
                string uploadPath = string.Format(@"{0}\{1}\P", ConfigurationManager.AppSettings["ConverterRootPathUpload"], userID.ToString());
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string fileExtension = string.Empty;
                Match match = Regex.Match(url, @"\.(jpeg|jpg|gif|png)$", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    fileExtension = match.Groups[1].Captures[0].Value;
                }
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    string imageID = Guid.NewGuid().ToString();
                    string filenameUploadOriginal = string.Format(@"{0}\{1}.{2}", uploadPath, imageID, fileExtension);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    Http.DownloadFile(request, filenameUploadOriginal);

                    FileInfo assembly = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(ConfigurationManager.AppSettings["MediaDomainName"], ConfigurationManager.AppSettings["ConverterRootPath"], userID.ToString(), imageName, true, assembly.DirectoryName + "/Configurations/");

                    imageHandler.DoConvert(filenameUploadOriginal, "ExtraSmallCroppedJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(filenameUploadOriginal, "SmallCroppedJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(filenameUploadOriginal, "LargeResizedJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    width = imageHandler.ImageInfo.Width;
                    height = imageHandler.ImageInfo.Height;
                    if (height > 0)
                        aspectRatio = Math.Round((decimal)width / (decimal)height, 3);
                    imageHandler.DoConvert(filenameUploadOriginal, "CopyArchive", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

                    return true;
                }
            }
            catch (Exception e)
            {
                log.Message += string.Format("URL={0},Error={1},", url, e.Message.Replace("\r\n", " "));
                return false;
            }
            return false;
        }

        public static void GetImages(WebServiceLogEntry log, DataObjectNews receivedNews, DataObject news)
        {
            DataObject dataObject = news ?? receivedNews;

            DataObjectList<DataObjectPicture> newsPictures = DataObjects.Load<DataObjectPicture>(new QuickParameters()
                {
                    Udc = UserDataContext.GetUserDataContext(dataObject.Nickname),
                    RelationParams = new RelationParams()
                    {
                        ParentObjectID = dataObject.ObjectID,
                        ParentObjectType = dataObject.ObjectType,
                        ChildObjectType = Common.Helper.GetObjectType("Picture").NumericId
                    },
                    IgnoreCache = true,
                    DisablePaging = true
                });
            foreach (DataObjectPicture newsPicture in newsPictures)
            {
                newsPicture.Delete(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
            }
            DataObject.RelDelete(new RelationParams() { ParentObjectID = dataObject.ObjectID, ParentObjectType = dataObject.ObjectType, ChildObjectType = Common.Helper.GetObjectType("Picture").NumericId });

            if (!string.IsNullOrEmpty(receivedNews.Image))
            {
                string imageName = dataObject.ObjectID.ToString();
                int width, height;
                decimal aspectRatio;
                if (GetImage(log, receivedNews.Image, Common.Helper.GetObjectType("News").NumericId, dataObject.UserID.Value, imageName, out width, out height, out aspectRatio))
                {
                    dataObject.Image = imageName;
                    dataObject.SetImageType(PictureVersion.XS, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.S, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.L, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.A, PictureFormat.Jpg);
                    log.FilesDownload++;
                }
            }

            if (receivedNews.LargePictures.Count > 0)
            {
                List<string> newsImages = receivedNews.LargePictures;
                List<string> newsCaptions = receivedNews.PictureCaptions;
                for (int i = 0; i < newsImages.Count; i++)
                {
                    Guid newsPictureId = Guid.NewGuid();
                    int width, height;
                    decimal aspectRatio;
                    if (GetImage(log, newsImages[i], Common.Helper.GetObjectType("News").NumericId, dataObject.UserID.Value, newsPictureId.ToString(), out width, out height, out aspectRatio))
                    {
                        DataObjectPicture newsPicture = new DataObjectPicture(UserDataContext.GetUserDataContext(dataObject.Nickname));
                        newsPicture.ObjectID = newsPictureId;
                        newsPicture.Title = !string.IsNullOrEmpty(newsCaptions[i]) ? newsCaptions[i].CropString(100) : " ";
                        newsPicture.PartnerID = dataObject.PartnerID;
                        newsPicture.Description = newsCaptions[i];
                        newsPicture.TagList = dataObject.TagList;
                        newsPicture.StartDate = dataObject.StartDate;
                        newsPicture.EndDate = dataObject.EndDate;
                        newsPicture.CommunityID = dataObject.CommunityID;
                        newsPicture.Status = dataObject.Status;
                        newsPicture.Copyright = dataObject.Copyright;
                        newsPicture.Image = newsPictureId.ToString();
                        newsPicture.Width = width;
                        newsPicture.Height = height;
                        newsPicture.AspectRatio = aspectRatio;
                        newsPicture.SetImageType(PictureVersion.XS, PictureFormat.Jpg);
                        newsPicture.SetImageType(PictureVersion.S, PictureFormat.Jpg);
                        newsPicture.SetImageType(PictureVersion.L, PictureFormat.Jpg);
                        newsPicture.SetImageType(PictureVersion.A, PictureFormat.Jpg);
                        newsPicture.Insert(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));

                        DataObject.RelInsert(new RelationParams() { ParentObjectID = dataObject.ObjectID, ParentObjectType = dataObject.ObjectType, ChildObjectID = newsPicture.ObjectID, ChildObjectType = newsPicture.ObjectType }, i);

                        log.FilesDownload++;
                    }
                }
            }
        }

        public static void GetImages(WebServiceLogEntry log, DataObjectGeneric receivedGeneric, DataObject generic)
        {
            DataObject dataObject = generic ?? receivedGeneric;

            if (!string.IsNullOrEmpty(receivedGeneric.Image))
            {
                string imageName = dataObject.ObjectID.ToString();
                int width, height;
                decimal aspectRatio;
                if (GetImage(log, receivedGeneric.Image, Common.Helper.GetObjectType("Generic").NumericId, dataObject.UserID.Value, imageName, out width, out height, out aspectRatio))
                {
                    dataObject.Image = imageName;
                    dataObject.SetImageType(PictureVersion.XS, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.S, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.L, PictureFormat.Jpg);
                    dataObject.SetImageType(PictureVersion.A, PictureFormat.Jpg);
                    log.FilesDownload++;
                }
            }
        }

        public static void GeoTag(WebServiceLogEntry log, DataObject dataObject, Partner partner)
        {
            if (!string.IsNullOrEmpty(dataObject.CountryCode) && (!string.IsNullOrEmpty(dataObject.City) || !string.IsNullOrEmpty(dataObject.Zip)) && (dataObject.Geo_Lat == double.MinValue || dataObject.Geo_Long == double.MinValue))
            {
                string queryStreet = string.IsNullOrEmpty(dataObject.Street) ? null : dataObject.Street;
                string queryZip = string.IsNullOrEmpty(dataObject.Zip) ? null : dataObject.Zip;
                string queryCity = string.IsNullOrEmpty(dataObject.City) ? null : dataObject.City;
                string queryCountry = dataObject.CountryCode;

                GeoPoint geoPoint = _4screen.CSB.DataAccess.Business.Utils.GetGeoPointFromDB(queryStreet, queryZip, queryCity, queryCountry);
                if (geoPoint != null) // Geo info from cache
                {
                    dataObject.Geo_Lat = geoPoint.Lat;
                    dataObject.Geo_Long = geoPoint.Long;
                    log.GeoService = "LocalCache";
                }
                else // Get geo info from service
                {
                    bool autoGeoTagging = bool.Parse(ConfigurationManager.AppSettings["AutoGeoTagging"]);
                    if (autoGeoTagging)
                    {
                        if (partner.GeoServiceCalls < partner.MonthlyGeoServiceCredits)
                        {
                            partner.GeoServiceCalls++;
                            partner.Update();

                            geoPoint = GeoTagging.GeoTagging.GetGeoPosition(queryStreet, queryZip, queryCity, null, queryCountry);
                            log.GeoService = "VirtualEarth";
                            if (geoPoint != null)
                            {
                                if (!string.IsNullOrEmpty(queryStreet))
                                    geoPoint.StreetAndNumber = queryStreet;
                                if (!string.IsNullOrEmpty(queryCity))
                                    geoPoint.City = queryCity;
                                geoPoint.CountryCode = queryCountry;
                                if (!string.IsNullOrEmpty(geoPoint.ZipCode) || !string.IsNullOrEmpty(geoPoint.City))
                                {
                                    dataObject.Geo_Lat = geoPoint.Lat;
                                    dataObject.Geo_Long = geoPoint.Long;
                                    dataObject.Zip = geoPoint.ZipCode ?? string.Empty;
                                    _4screen.CSB.DataAccess.Business.Utils.StoreGeoPointInDB(geoPoint);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Too many geo service calls this month");
                        }
                    }
                }
            }
        }
    }
}