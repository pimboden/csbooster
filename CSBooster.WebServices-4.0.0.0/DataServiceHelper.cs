// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Extensions.Business;

namespace _4screen.CSB.WebServices
{
    public sealed class DataServiceHelper
    {
        private static IDataObjectFilter filter;

        private DataServiceHelper()
        {
        }

        static DataServiceHelper()
        {
            Assembly filterAssembly = Assembly.Load("CSBooster.DataAccess.Filter");
            Type filterType = filterAssembly.GetType("_4screen.CSB.DataAccess.Filter.DataObjectFilter");
            filter = (IDataObjectFilter)Activator.CreateInstance(filterType);
        }

        public static DataObject Get(WebServiceLogEntry log, string externalObjectId, int objectType)
        {
            Partner partner = Helper.GetCurrentPartner(log);

            DataObject dataObject = GetInstance(partner.CurrentUser.Nickname, objectType, externalObjectId, partner.PartnerID);
            if (dataObject.State != ObjectState.Added)
            {
                log.ObjectID = dataObject.ObjectID;
                return dataObject;
            }
            else
            {
                throw new RESTException(HttpStatusCode.NotFound, "Not Found");
            }
        }

        public static void Create(WebServiceLogEntry log, DataObject receivedDataObject, int objectType)
        {
            Partner partner = Helper.GetCurrentPartner(log);

            receivedDataObject.Status = ObjectStatus.Public;
            receivedDataObject.UserID = partner.CurrentUser.UserID;
            receivedDataObject.Nickname = partner.CurrentUser.Nickname;
            receivedDataObject.PartnerID = partner.PartnerID;
            if (!receivedDataObject.CommunityID.HasValue)
                receivedDataObject.CommunityID = partner.CommunityID;
            else
                receivedDataObject.CommunityID = receivedDataObject.CommunityID;

            receivedDataObject.TagList = receivedDataObject.TagList;

            filter.InsertFilter(receivedDataObject);

            receivedDataObject.Insert(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
            if (receivedDataObject.State == ObjectState.Saved)
            {
                log.ObjectID = receivedDataObject.ObjectID;
                try
                {
                    Helper.GeoTag(log, receivedDataObject, partner);
                }
                catch (Exception geoTaggingException)
                {
                    Helper.SetResponseStatus(log, HttpStatusCode.OK, string.Format("Updated but not GeoTagged -> {0}", geoTaggingException.Message));
                }
            }
            else
            {
                throw new RESTException(HttpStatusCode.NotFound, "Not Created (generic)");
            }
        }

        public static DataObject Update(WebServiceLogEntry log, DataObject receivedDataObject, int objectType)
        {
            Partner partner = Helper.GetCurrentPartner(log);

            DataObject dataObject = GetInstance(partner.CurrentUser.Nickname, objectType, receivedDataObject.ExternalObjectID, partner.PartnerID);
            if (dataObject.State != ObjectState.Added)
            {
                log.ObjectID = dataObject.ObjectID;
                receivedDataObject.UserID = dataObject.UserID;

                dataObject.Status = ObjectStatus.Public;
                if (!receivedDataObject.CommunityID.HasValue)
                    dataObject.CommunityID = partner.CommunityID;
                else
                    dataObject.CommunityID = receivedDataObject.CommunityID;

                filter.UpdateFilter(dataObject, receivedDataObject);

                dataObject.Update(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (dataObject.State == ObjectState.Saved)
                {
                    try
                    {
                        Helper.GeoTag(log, dataObject, partner);
                        return dataObject;
                    }
                    catch (Exception geoTaggingException)
                    {
                        Helper.SetResponseStatus(log, HttpStatusCode.OK, string.Format("Updated but not GeoTagged -> {0}", geoTaggingException.Message));
                        return dataObject;
                    }
                }
                else
                {
                    throw new RESTException(HttpStatusCode.InternalServerError, "Not Updated (generic)");
                }
            }
            else
            {
                throw new RESTException(HttpStatusCode.NotFound, "Not Found");
            }
        }

        public static DataObject Delete(WebServiceLogEntry log, string externalObjectId, int objectType)
        {
            Partner partner = Helper.GetCurrentPartner(log);

            DataObject dataObject = GetInstance(partner.CurrentUser.Nickname, objectType, externalObjectId, partner.PartnerID);
            if (dataObject.State != ObjectState.Added)
            {
                log.ObjectID = dataObject.ObjectID;

                DataObjectList<DataObject> relatedItems = DataObjects.Load<DataObject>(new QuickParameters()
                {
                    Udc = UserDataContext.GetUserDataContext(partner.CurrentUser.Nickname),
                    RelationParams = new RelationParams()
                    {
                        ParentObjectID = dataObject.ObjectID,
                        ParentObjectType = dataObject.ObjectType,
                    },
                    IgnoreCache = true,
                    DisablePaging = true
                });
                foreach (DataObject relatedItem in relatedItems)
                {
                    relatedItem.Delete(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                }

                dataObject.Delete(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (dataObject.State == ObjectState.Deleted)
                {
                    return dataObject;
                }
                else
                {
                    throw new RESTException(HttpStatusCode.InternalServerError, "Not Deleted");
                }
            }
            else
            {
                throw new RESTException(HttpStatusCode.NotFound, "Not Found");
            }
        }

        private static DataObject GetInstance(string username, int objectType, string externalObjectId, Guid partnerID)
        {
            DataObject dataObject = null;
            if (objectType == Common.Helper.GetObjectType("Generic").NumericId)
                dataObject = DataObject.Load<DataObjectGeneric>(UserDataContext.GetUserDataContext(username), partnerID, externalObjectId);
            else if (objectType == Common.Helper.GetObjectType("News").NumericId)
                dataObject = DataObject.Load<DataObjectNews>(UserDataContext.GetUserDataContext(username), partnerID, externalObjectId);
            return dataObject;
        }
    }
}