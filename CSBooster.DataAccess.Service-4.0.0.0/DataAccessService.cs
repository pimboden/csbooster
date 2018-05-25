// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Threading;
using System.Xml;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Service
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
            xml.Load("CSBooster.DataAccess.Service.exe.config");
            string serviceName = ((XmlElement)xml.SelectSingleNode("configuration/appSettings/add[@key='ServiceName']")).GetAttribute("value");
            service.ServiceName = serviceName;

            Installers.Add(process);
            Installers.Add(service);
        }
    }

    public class DataAccessService : ServiceBase
    {
        private System.Diagnostics.EventLog eventLog1;
        private Jobs jobs = null;
        private Thread thread;

        public DataAccessService()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            this.CanPauseAndContinue = false;
            this.AutoLog = false;
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
            jobs = new Jobs();
            jobs.EventLog = this.eventLog1;
            thread = new Thread(new ThreadStart(jobs.Start));
            thread.Start();

            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been started");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " started");
        }

        protected override void OnStop()
        {
            jobs.Stop();
            thread.Abort();

            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been stopped");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " stopped");
        }

    }
}
