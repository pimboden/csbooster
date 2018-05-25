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
//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		13.01.2009 / AW
//******************************************************************************

namespace _4screen.CSB.WindowServices
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
            xml.Load("CSBooster.VideoEncodingService.exe.config");
            string serviceName = ((XmlElement)xml.SelectSingleNode("configuration/appSettings/add[@key='ServiceName']")).GetAttribute("value");
            service.ServiceName = serviceName;

            Installers.Add(process);
            Installers.Add(service);
        }
    }

    public class VideoEncodingService : ServiceBase
    {
        private System.Diagnostics.EventLog eventLog1;
        private VideoEncoder videoEncoder;
        private Thread thread;

        public VideoEncodingService()
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
            videoEncoder = new VideoEncoder();
            videoEncoder.EventLog = eventLog1;
            thread = new Thread(new ThreadStart(videoEncoder.Start));
            thread.Start();

            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been started");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " started");
        }

        protected override void OnStop()
        {
            videoEncoder.Stop();
            thread.Abort();

            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], "The service has been stopped");
            eventLog1.WriteEntry(ConfigurationManager.AppSettings["ServiceName"] + " stopped");
        }
    }
}
