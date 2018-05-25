using System;

namespace _4screen.CSB.Extensions.Business
{
    public enum WebServiceType
    {
        REST,
        SOAP
    }

    public class WebServiceLogEntry
    {
        public Guid LogID { get; set; }
        public string IP { get; set; }
        public Guid? UserID { get; set; }
        public Guid? PartnerID { get; set; }
        public Guid? ObjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public WebServiceType ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
        public string Result { get; set; }
        public string GeoService { get; set; }
        public int FilesToDownload { get; set; }
        public int FilesDownload { get; set; }
        public string Message { get; set; }
        public string ExtendedMessage { get; set; }

        public WebServiceLogEntry()
        {
            StartDate = DateTime.Now;
        }

        public void Write(string result)
        {
            Result = result;
            Write();
        }

        public void Write()
        {
            EndDate = DateTime.Now;

            Data.CSBooster_ExtensionsDataContext dc = new Data.CSBooster_ExtensionsDataContext();
            dc.hisp_WebServicesLog_Insert(IP, UserID, PartnerID, ObjectID, StartDate, EndDate, ServiceType.ToString(), ServiceName, Method, Parameters, Result, GeoService, FilesToDownload, FilesDownload, Message, ExtendedMessage);
        }
    }
}