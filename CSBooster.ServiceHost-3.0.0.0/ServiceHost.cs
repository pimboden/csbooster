using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Xml;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebServices;

namespace _4screen.CSB.WebServices
{
    [RunInstaller(true)]
    public class ServiceHostInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ServiceHostInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();

            XmlDocument xml = new XmlDocument();
            xml.Load("CSBooster.ServiceHost.exe.config");
            string serviceName = ((XmlElement)xml.SelectSingleNode("configuration/appSettings/add[@key='ServiceName']")).GetAttribute("value");
            service.ServiceName = serviceName;

            Installers.Add(process);
            Installers.Add(service);
        }
    }

    public class ServiceHost : ServiceBase
    {
        public System.Diagnostics.EventLog eventLog1;

        public ServiceHost()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

            if (!System.Diagnostics.EventLog.SourceExists(ConfigurationManager.AppSettings["ServiceLogSourceName"]))
            {
                System.Diagnostics.EventLog.CreateEventSource(ConfigurationManager.AppSettings["ServiceLogSourceName"], ConfigurationManager.AppSettings["ServiceLogName"]);
            }
            eventLog1.Source = ConfigurationManager.AppSettings["ServiceLogSourceName"];
            eventLog1.Log = ConfigurationManager.AppSettings["ServiceLogName"];
        }

        protected override void OnStart(string[] args)
        {
            WebServiceHost host = new WebServiceHost(Type.GetType(ConfigurationManager.AppSettings["ServiceTypeName"]));
            host.Open();

            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been started");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " started");
        }

        protected override void OnStop()
        {
            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been stopped");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " stopped");
        }
    }
}
