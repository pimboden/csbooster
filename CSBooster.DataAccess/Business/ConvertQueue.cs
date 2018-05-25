//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class ConvertQueues
    {
        private static Data.ConvertQueue convertQueue;

        static ConvertQueues()
        {
            convertQueue = new Data.ConvertQueue();
        }

        public static List<ConvertQueue> LoadRunningJobs()
        {
            return convertQueue.LoadRunningJobs();
        }

        public static ConvertQueue LoadLastTimestamp(string ServerName)
        {
            return convertQueue.LoadLastTimestamp(ServerName);
        }

        public static List<ConvertQueue> LoadWaitingJobs()
        {
            return convertQueue.LoadWaitingJobs();
        }
    }

    public class ConvertQueue
    {
        private Data.ConvertQueue convertQueue;

        public ConvertQueue()
        {
            convertQueue = new Data.ConvertQueue();
        }

        //COQ_ID	uniqueidentifier	Unchecked
        private Guid? id = null;

        public Guid? ID
        {
            get { return id; }
            set { id = value; }
        }

        //COQ_Status	int	Unchecked
        //    public enum MediaConvertedState
        //    {
        //        NotConvertet = 0,
        //        InProgress = 1,
        //        Convertet = 2,
        //        ConvertError = 99
        //    }
        private MediaConvertedState status = MediaConvertedState.NotConvertet;

        public MediaConvertedState Status
        {
            get { return status; }
            set { status = value; }
        }

        //COQ_InsertedDate	datetime	Unchecked
        private DateTime insertedDate = DateTime.MinValue;

        public DateTime InsertedDate
        {
            get { return insertedDate; }
            set { insertedDate = value; }
        }

        //COQ_LookID	uniqueidentifier	Checked
        //    GUID
        private Guid? lookID = null;

        public Guid? LookID
        {
            get { return lookID; }
            set { lookID = value; }
        }

        //OBJ_ID	uniqueidentifier	Unchecked
        //    hitbl_DataObject_OBJ.OBJ_ID
        private Guid? objectID = null;

        public Guid? ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }

        //OBJ_Type	int	Unchecked
        //    public enum ObjectType
        //    {
        //            None = 0,
        //            Community = 1,
        //            User = 2,
        //        Picture = 3,
        //        Video = 4,
        //    }
        private int objectType = 0;

        public int ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        //COQ_LastTimeStamp	datetime	Checkd
        //    converter feedback
        private DateTime lastTimeStamp = DateTime.MinValue;

        public DateTime LastTimeStamp
        {
            get { return lastTimeStamp; }
            set { lastTimeStamp = value; }
        }

        //COQ_TryingCount	int	Unchecked
        //    Converter ++
        //    Monitoring Prozess, wenn angenommen nicht mehr am Ausführen, dann Status, GUID, COQ_LastTimeStamp resetten
        private int tryingCount = 0;

        public int TryingCount
        {
            get { return tryingCount; }
            set { tryingCount = value; }
        }

        //COQ_ServerName	varchar(250)	Checked
        //    Converter Server, der denn Job übernommen hat
        private string serverName = string.Empty;

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        //COQ_UserEmail	nvarchar(250)	Checked
        private string userEmail = string.Empty;

        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }

        //COQ_VideoPreviewPictureTimepointSec	float	Checked
        private double videoPreviewPictureTimepointSec = 0d;

        public double VideoPreviewPictureTimepointSec
        {
            get { return videoPreviewPictureTimepointSec; }
            set { videoPreviewPictureTimepointSec = value; }
        }

        //COQ_EstimatedWorkTimeSec	int	Checked
        private int estimatedWorkTimeSec = 0;

        public int EstimatedWorkTimeSec
        {
            get { return estimatedWorkTimeSec; }
            set { estimatedWorkTimeSec = value; }
        }

        //COQ_ConvertMessage	nvarchar(500)	Checked
        private string convertMessage = string.Empty;

        public string ConvertMessage
        {
            get { return convertMessage; }
            set { convertMessage = value; }
        }

        //COQ_StatisticFileExtension	nvarchar(50)	Checked
        //File Extension, z.B ".avi"
        private string statisticFileExtension = string.Empty;

        public string StatisticFileExtension
        {
            get { return statisticFileExtension; }
            set { statisticFileExtension = value; }
        }

        //COQ_StatisticFileSizeByte	int	Checked
        //Original File Grösse in Kb, z.B "5312"
        private int statisticFileSizeByte = 0;

        public int StatisticFileSizeByte
        {
            get { return statisticFileSizeByte; }
            set { statisticFileSizeByte = value; }
        }

        //COQ_StatisticWorkTimeSec	int	Checked
        //Für die Konvertierung benötigte Ausführungszeit, z.b "150"
        private int statisticWorkTimeSec = 0;

        public int StatisticWorkTimeSec
        {
            get { return statisticWorkTimeSec; }
            set { statisticWorkTimeSec = value; }
        }

        #region Methods

        #endregion

        public bool LoadNextJob(string ServerName)
        {
            //Data.ConvertQueue convertQueue = new Data.ConvertQueue();
            return convertQueue.LoadNextJob(this, ServerName);
        }

        public void Insert()
        {
            convertQueue.Insert(this);
        }

        public void Delete()
        {
            convertQueue.Delete(this);
        }

        public void Update()
        {
            convertQueue.Update(this);
        }
    }
}