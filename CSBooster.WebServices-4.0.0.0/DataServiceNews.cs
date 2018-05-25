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
    public static class DataServiceNews
    {
        public static DataObjectNews GetNews(string externalObjectId)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "GetNews", Parameters = externalObjectId};
            try
            {
                DataObjectNews dataObject = (DataObjectNews) DataServiceHelper.Get(log, externalObjectId, Common.Helper.GetObjectType("News").NumericId);
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

        public static DataObjectNews CreateNews(DataObjectNews receivedDataObject)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "CreateNews", Parameters = "DataObjectNews"};
            try
            {
                DataServiceHelper.Create(log, receivedDataObject, Common.Helper.GetObjectType("News").NumericId);
                Helper.GetImages(log, receivedDataObject, null);
                receivedDataObject.Update(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (receivedDataObject.State == ObjectState.Saved)
                {
                    log.Write(HttpStatusCode.OK.ToString());
                    return receivedDataObject;
                }
                throw new RESTException(HttpStatusCode.NotFound, "Not Updated (specific)");
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

        public static DataObjectNews UpdateNews(DataObjectNews receivedDataObject)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "UpdateNews", Parameters = "DataObjectNews"};
            try
            {
                DataObjectNews dataObject = (DataObjectNews) DataServiceHelper.Update(log, receivedDataObject, Common.Helper.GetObjectType("News").NumericId);
                Helper.GetImages(log, receivedDataObject, dataObject);
                dataObject.NewsText = receivedDataObject.NewsText;
                dataObject.ReferenceURL = receivedDataObject.ReferenceURL;
                dataObject.Links = receivedDataObject.Links;

                dataObject.Update(UserDataContext.GetUserDataContext(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name));
                if (receivedDataObject.State == ObjectState.Changed)
                {
                    log.Write(HttpStatusCode.OK.ToString());
                    return dataObject;
                }
                throw new RESTException(HttpStatusCode.NotFound, "Not Updated (specific)");
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

        public static DataObjectNews DeleteNews(string externalObjectId)
        {
            WebServiceLogEntry log = new WebServiceLogEntry() {ServiceType = WebServiceType.REST, ServiceName = "IDataService", Method = "DeleteNews", Parameters = externalObjectId};
            try
            {
                DataObjectNews dataObject = (DataObjectNews) DataServiceHelper.Delete(log, externalObjectId, Common.Helper.GetObjectType("News").NumericId);
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
                Helper.SetResponseStatus(log, HttpStatusCode.InternalServerError, string.Format("Not Delete -> {0}", e.Message));
                log.ExtendedMessage = e.StackTrace;
                log.Write();
                return null;
            }
        }
    }
}