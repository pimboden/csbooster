// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Net;
using System.ServiceModel.Web;

namespace _4screen.CSB.WebServices
{
    public class RESTException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public RESTException(HttpStatusCode statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription.Replace("\r\n", " ");
            WebOperationContext.Current.OutgoingResponse.StatusCode = statusCode;
            WebOperationContext.Current.OutgoingResponse.StatusDescription = StatusDescription;
        }
    }
}