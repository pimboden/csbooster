// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Xml;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Service
{
    public class Jobs
    {
        private List<Job> listJob = null;
        private int checkIntervalSecond;
        private string strCachePath = string.Empty;
        private bool running = false;

        public System.Diagnostics.EventLog EventLog { get; set; }

        public Jobs()
        {
            strCachePath = ConfigurationManager.AppSettings["CachingPath"];
            int.TryParse(ConfigurationManager.AppSettings["CheckIntervalSecond"], out this.checkIntervalSecond);
            if (checkIntervalSecond < 1)
                checkIntervalSecond = 1;
            else if (checkIntervalSecond > 3600)
                checkIntervalSecond = 3600;

        }

        public void Start()
        {
            if (listJob == null)
                LoadJobs();

            running = true;
            while (running)
            {
                RunJob();

                if (running)
                    System.Threading.Thread.Sleep(1000 * this.checkIntervalSecond);
            }
            listJob = null;
        }

        public void Stop()
        {
            running = false;

        }

        private void RunJob()
        {
            DateTime nowDate = DateTime.Now;

            foreach (Job job in listJob)
            {
                if (!running)
                    break;

                if (job.IsRunning)
                    continue;

                if (job.StartType == 1) //Täglich
                {
                    if (job.LastRunning.AddDays(1) <= nowDate)
                    {
                        DateTime datStart = nowDate;
                        if (job.StartTime > nowDate.TimeOfDay)
                            datStart = nowDate.AddDays(-1);

                        job.LastRunning = new DateTime(datStart.Year, datStart.Month, datStart.Day, job.StartTime.Hours, job.StartTime.Minutes, job.StartTime.Seconds);
                        job.ExpirationDate = nowDate.AddHours(1);
                        try
                        {
                            string strMsg = job.Start();
                            if (!string.IsNullOrEmpty(strMsg))
                                EventLog.WriteEntry(strMsg, EventLogEntryType.Information);
                        }
                        catch (Exception ex)
                        {
                            StringBuilder strMsg = new StringBuilder();
                            GetException(strMsg, ex);
                            string strCheckMessage = string.Format("Error: in job '{0}'\r\nSystem.Exception Message: {1}", job.ToString(), strMsg.ToString());

                            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], strCheckMessage);
                            EventLog.WriteEntry(strCheckMessage, System.Diagnostics.EventLogEntryType.Error);
                        }
                    }
                    continue;
                }

                if (job.StartType == 2) //Fliessend
                {
                    if (job.LastRunning.AddMinutes(job.WaitMinutes) < nowDate)
                    {
                        job.LastRunning = nowDate;
                        job.ExpirationDate = nowDate.AddMinutes(job.WaitMinutes + 10);
                        try
                        {
                            string strMsg = job.Start();
                            if (!string.IsNullOrEmpty(strMsg))
                                EventLog.WriteEntry(strMsg, EventLogEntryType.Information);
                        }
                        catch (Exception ex)
                        {
                            StringBuilder strMsg = new StringBuilder();
                            GetException(strMsg, ex);
                            string strCheckMessage = string.Format("Error: in job '{0}'\r\nSystem.Exception Message: {1}", job.ToString(), strMsg.ToString());

                            Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], strCheckMessage);
                            EventLog.WriteEntry(strCheckMessage, System.Diagnostics.EventLogEntryType.Error);
                        }
                        continue;
                    }
                }
            }

        }

        private void LoadJobs()
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                listJob = new List<Job>();

                XmlDocument xmlJobs = new XmlDocument();
                xmlJobs.Load(string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Jobs.config"));
                foreach (XmlElement xmlJob in xmlJobs.SelectNodes("//Jobs/Job"))
                {
                    Job job = new Job(xmlJob, strCachePath);
                    job.EventLog = EventLog;
                    job.StartType = Convert.ToInt32(xmlJob.GetAttribute("startType"));
                    if (job.StartType == 1)
                    {
                        DateTime datStart = nowDate;
                        job.StartTime = ConvertToTimeSpan(xmlJob.GetAttribute("startTime"));
                        if (job.StartTime > datStart.TimeOfDay)
                            datStart = nowDate.AddDays(-1);

                        job.LastRunning = new DateTime(datStart.Year, datStart.Month, datStart.Day, job.StartTime.Hours, job.StartTime.Minutes, job.StartTime.Seconds);
                    }
                    else if (job.StartType == 2)
                    {
                        job.WaitMinutes = Convert.ToInt32(xmlJob.GetAttribute("waitMinutes"));
                        job.LastRunning = nowDate.AddMinutes((job.WaitMinutes * -1));
                    }

                    listJob.Add(job);
                }
            }
            catch (Exception ex)
            {
                StringBuilder strMsg = new StringBuilder();
                GetException(strMsg, ex);

                Message.SendEmail(ConfigurationManager.AppSettings["BugReportMail"], ConfigurationManager.AppSettings["ServiceName"], strMsg.ToString());
                EventLog.WriteEntry(strMsg.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private TimeSpan ConvertToTimeSpan(string time)
        {
            string[] strTime = time.Split(':');
            if (strTime.Length == 1)
                return new TimeSpan(Convert.ToInt32(strTime[0]), 0, 0);
            else if (strTime.Length == 2)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), 0);
            if (strTime.Length == 3)
                return new TimeSpan(Convert.ToInt32(strTime[0]), Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            else
                return new TimeSpan(24, 0, 0);
        }

        private void GetException(StringBuilder msg, Exception exc)
        {
            msg.AppendLine(string.Format("source: '{0}' message: '{1}'", exc.Source, exc.ToString()));
            msg.AppendLine();
            if (exc.InnerException != null)
                GetException(msg, exc.InnerException);
        }
    }
}
