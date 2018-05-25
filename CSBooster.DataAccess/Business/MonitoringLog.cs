//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//	Created:	#0.5.2.0		04.07.2007 / TS
//									- Handling zur Überwachung von Action-Prozessen (zB. Video-Konvertierung)
//******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class MonitoringLog
    {
        private Data.MonitoringLog monitoringLog;

        public MonitoringLog()
        {
            monitoringLog = new Data.MonitoringLog();
        }

        //MOL_Transaction_ID	NVARCHAR(50)
        private string transactionID = string.Empty;

        public string TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }

        //MOL_BaseAction	NVARCHAR(250)
        private string baseAction = string.Empty;

        public string BaseAction
        {
            get { return baseAction; }
            set { baseAction = value; }
        }

        //MOL_Step	INT
        private int step = -1;

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        //MOL_StepDescription	NVARCHAR(50)
        private string stepDescription = string.Empty;

        public string StepDescription
        {
            get { return stepDescription; }
            set { stepDescription = value; }
        }

        //MOL_Message	NTEXT
        private string message = string.Empty;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //MOL_State	int
        //    public enum MonitoringLogState
        //    {
        //		OK = 0,
        //		OKWithInformation = 1,
        //		OKWithWarning = 2,
        //		Aborted = 3,
        //		AbortedMissionCritical = 4
        //    }
        private MonitoringLogState state = MonitoringLogState.OK;

        public MonitoringLogState State
        {
            get { return state; }
            set { state = value; }
        }

        //OBJ_ID	uniqueidentified
        //    GUID
        private Guid? objectID = null;

        public Guid? ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }

        #region Methods

        #endregion

        public void Insert()
        {
            monitoringLog.Insert(this);
        }
    }
}