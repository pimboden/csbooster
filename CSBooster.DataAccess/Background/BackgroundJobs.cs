// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Background
{
    public class BackgroundJobs
    {
        private string strCachingPath = string.Empty;
        private Data.DataObjectBackground objData = null;
        private Data.AdWordFilterJob adWordFilterJob;
        private DateTime datExpirationDate = DateTime.Now.AddDays(2);

        public DateTime ExpirationDate
        {
            get { return datExpirationDate; }
            set { datExpirationDate = value; }
        }

        public string CachingPath
        {
            get { return strCachingPath; }
            set { strCachingPath = value; }
        }

        public void Cancel()
        {
            if (objData != null)
                objData.Cancel = true;
            else if (adWordFilterJob != null)
                adWordFilterJob.Cancel = true;
        }

        public void SetAgility()
        {
            if (objData == null)
                objData = new _4screen.CSB.DataAccess.Data.DataObjectBackground();
            else
                return;

            try
            {
                objData.SetAgility();
            }
            finally
            {
                objData = null;
            }
        }

        public int SetEmphasis()
        {
            if (objData == null)
                objData = new _4screen.CSB.DataAccess.Data.DataObjectBackground();
            else
                return -1;

            try
            {
                return objData.SetEmphasis();
            }
            finally
            {
                objData = null;
            }
        }

        public void StartAdWordBatch()
        {
            try
            {
                adWordFilterJob = new Data.AdWordFilterJob();
                adWordFilterJob.ProcessDataObjectsAll();
            }
            finally
            {
                adWordFilterJob = null;
            }
        }

        public int StartBirthdayCheck(int dayCountBefore)
        {
            if (objData == null)
                objData = new _4screen.CSB.DataAccess.Data.DataObjectBackground();

            try
            {
                // zuerst einmal aller Registrationen aktuallisieren
                Notification.Business.Notification objNoti = new _4screen.CSB.Notification.Business.Notification();
                objNoti.CheckBirthdayRegistration();
                objNoti = null;

                // jetzt alle user mit geburtstag lesen und checken

                return objData.StartBirthdayCheck(dayCountBefore);
            }
            finally
            {
                objData = null;
            }
        }

        public void ExecuteStoreProcedure(string name)
        {
            Data.BackgroundJobs.ExecuteStoreProcedure(name);  
        }
    }
}