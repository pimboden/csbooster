// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Net;
using System.ServiceModel;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Extensions.Business;

namespace _4screen.CSB.WebServices
{
    public sealed class DataServiceGeneric
    {
        private DataServiceGeneric()
        {
        }

        public static DataObjectGeneric GetGeneric(string externalObjectId)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "GetGeneric", Parameters = externalObjectId};
            try
            {
                DataObjectGeneric dataObject = (DataObjectGeneric) DataServiceHelper.Get(log, externalObjectId, Common.Helper.GetObjectType("Generic").NumericId);
                log.Write(HttpStatusCode.OK.ToString());
                return dataObject;
            }
            catch (RESTException e)
            {
                log.Message = e.StatusDescription;
                log.Write(e.StatusCode.ToString());
                return null;
            }
            catch (Exception e)
            {
                Helper.SetResponseStatus(log, HttpStatusCode.InternalServerError, string.Format("Not Found -> {0}", e.Message));
                log.ExtendedMessage = e.StackTrace;
                log.Write();
                return null;
            }
        }

        public static DataObjectGeneric CreateGeneric(DataObjectGeneric receivedDataObject)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "CreateGeneric", Parameters = "DataObjectGeneric"};
            try
            {
                DataServiceHelper.Create(log, receivedDataObject, Common.Helper.GetObjectType("Generic").NumericId);
                Helper.GetImages(log, receivedDataObject, null);
                receivedDataObject.Update(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (receivedDataObject.State == ObjectState.Saved)
                {
                    log.Write(HttpStatusCode.OK.ToString());
                    return receivedDataObject;
                }
                else
                {
                    throw new RESTException(HttpStatusCode.NotFound, "Not Updated (specific)");
                }
            }
            catch (RESTException e)
            {
                log.Message = e.StatusDescription;
                log.Write(e.StatusCode.ToString());
                return null;
            }
            catch (Exception e)
            {
                Helper.SetResponseStatus(log, HttpStatusCode.InternalServerError, string.Format("Not Created -> {0}", e.Message));
                log.ExtendedMessage = e.StackTrace;
                log.Write();
                return null;
            }
        }

        public static DataObjectGeneric UpdateGeneric(DataObjectGeneric receivedDataObject)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "UpdateGeneric", Parameters = "DataObjectGeneric"};
            try
            {
                DataObjectGeneric dataObject = (DataObjectGeneric) DataServiceHelper.Update(log, receivedDataObject, Common.Helper.GetObjectType("Generic").NumericId);
                Helper.GetImages(log, receivedDataObject, dataObject);
                dataObject.GenericData = receivedDataObject.GenericData;

                dataObject.Update(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (receivedDataObject.State == ObjectState.Changed)
                {
                    log.Write(HttpStatusCode.OK.ToString());
                    return dataObject;
                }
                else
                {
                    throw new RESTException(HttpStatusCode.NotFound, "Not Updated (specific)");
                }
            }
            catch (RESTException e)
            {
                log.Message = e.StatusDescription;
                log.Write(e.StatusCode.ToString());
                return null;
            }
            catch (Exception e)
            {
                Helper.SetResponseStatus(log, HttpStatusCode.InternalServerError, string.Format("Not Updated -> {0}", e.Message));
                log.ExtendedMessage = e.StackTrace;
                log.Write();
                return null;
            }
        }

        public static DataObjectGeneric DeleteGeneric(string externalObjectId)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "DeleteGeneric", Parameters = externalObjectId};
            try
            {
                DataObjectGeneric dataObject = (DataObjectGeneric) DataServiceHelper.Delete(log, externalObjectId, Common.Helper.GetObjectType("Generic").NumericId);
                log.Write(HttpStatusCode.OK.ToString());
                return dataObject;
            }
            catch (RESTException e)
            {
                log.Message = e.StatusDescription;
                log.Write(e.StatusCode.ToString());
                return null;
            }
            catch (Exception e)
            {
                Helper.SetResponseStatus(log, HttpStatusCode.InternalServerError, string.Format("Not Deleted -> {0}", e.Message));
                log.ExtendedMessage = e.StackTrace;
                log.Write();
                return null;
            }
        }
    }
}