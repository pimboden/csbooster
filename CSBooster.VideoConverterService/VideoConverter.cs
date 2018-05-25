//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	
//******************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;
using vbio = Microsoft.VisualBasic.FileIO.FileSystem;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Query;
using Affirma.ThreeSharp.Model;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace CSBooster.VideoConverterService
{
   public partial class VideoConverter : ServiceBase
   {
      private enum BaseActions
      {
         VideoConvert = 0
      }

      #region FIELDS
      private System.Timers.Timer timer;
      private System.Diagnostics.EventLog eventLog;
      private _4screen.CSB.DataAccess.Business.MonitoringLog monLog;
      private _4screen.CSB.Common.UserDataContext udc;

      private bool blnIsEncoding = false;
      private string strTransactionID;
      private int maxRetries = 1;
      private int intTimerInSec = 5;
      private string strLocalPathTarget;
      private string strLocalPathSource;
      private string strConverterEngine;
      private string strVideoWidth;
      private string strVideoHeight;
      private string strFLVProfil;
      private bool blnShowConverterProgressOnEncoding = false;
      private string strSnapShotEngine;
      private string strImgLActionProfile = "LargeResizedJpg";
      private string strImgSActionProfile = "SmallCroppedJpg";
      private string strImgXSActionProfile = "ExtraSmallCroppedJpg";
      private bool blnDoOptimizeSpeed = false;
      private string strConverterRootPathUpload;
      private string strConverterRootPathMedia;
      private string strConverterRootPathArchive;
      private string strConverterMailFromAddress;
      private string strConverterMailFromDisplayName;
      private string strConverterMailToOperator;
      private int intSnapshotAfterSec = 5;
      private bool useAmazoneS3 = false;
      private string amazoneS3AKey = string.Empty;
      private string amazoneS3SAKey = string.Empty;
      private string amazoneS3Bucket = string.Empty;
      private string amazoneS3BucketLocation = string.Empty;
      #endregion FIELDS

      public VideoConverter()
      {
         InitializeComponent();
      }

      #region WINDOWS SERVICE EVENTS
      protected override void OnStart(string[] args)
      {
         LoadSettings();
         InitializeService();

         timer = new System.Timers.Timer();
         timer.Interval = intTimerInSec * 1000;
         timer.Enabled = true;
         timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
         udc = _4screen.CSB.Common.UserDataContext.GetUserDataContext();
      }

      protected override void OnStop()
      {
         this.timer.Enabled = false;

         try
         {
            Process[] localByName = Process.GetProcessesByName("convert");
            for (int i = 0; i < localByName.Length; i++)
               localByName[i].Kill();

            blnIsEncoding = false;
         }
         catch { }
      }

      private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
      {
         if (blnIsEncoding)
            return;

         // ENABLE FOR DEBUGGING
         //timer.Interval = 600 * 1000;

         CheckConvertQueue();
      }
      #endregion SERVICE EVENTS

      #region PRIVATE INIT METHODES
      private void LoadSettings()
      {
         // TODO: Get from config
         intSnapshotAfterSec = 5 * 1000;

         intTimerInSec = Convert.ToInt32(ConfigurationSettings.AppSettings["ConverterJobQueueCheckIntervallSec"]);
         strConverterRootPathUpload = ConfigurationSettings.AppSettings["ConverterRootPathUpload"];
         strConverterRootPathMedia = ConfigurationSettings.AppSettings["ConverterRootPathMedia"];
         strConverterRootPathArchive = ConfigurationSettings.AppSettings["ConverterRootPathArchive"];
         strLocalPathTarget = ConfigurationSettings.AppSettings["ConverterLocalPathTarget"];
         strLocalPathSource = ConfigurationSettings.AppSettings["ConverterLocalPathSource"];
         strConverterEngine = ConfigurationSettings.AppSettings["ConverterVideoConverterEngine"];
         strVideoWidth = ConfigurationSettings.AppSettings["ConverterVideoWidth"];
         strVideoHeight = ConfigurationSettings.AppSettings["ConverterVideoHeight"];
         strFLVProfil = ConfigurationSettings.AppSettings["ConverterVideoConvertFlvProfil"];
         strSnapShotEngine = ConfigurationSettings.AppSettings["ConverterVideoSnapShotEngine"];
         strImgLActionProfile = ConfigurationSettings.AppSettings["ConverterImgLActionProfile"];
         strImgSActionProfile = ConfigurationSettings.AppSettings["ConverterImgSActionProfile"];
         strImgXSActionProfile = ConfigurationSettings.AppSettings["ConverterImgXSActionProfile"];
         blnDoOptimizeSpeed = Convert.ToBoolean(ConfigurationSettings.AppSettings["ConverterVideoOptimizeSpeed"]);
         blnShowConverterProgressOnEncoding = Convert.ToBoolean(ConfigurationSettings.AppSettings["ShowConverterProgressOnEncoding"]);
         strConverterMailFromAddress = ConfigurationSettings.AppSettings["ConverterMailFromAddress"];
         strConverterMailFromDisplayName = ConfigurationSettings.AppSettings["ConverterMailFromDisplayName"];
         strConverterMailToOperator = ConfigurationSettings.AppSettings["ConverterMailToOperator"];
         useAmazoneS3 = ConfigurationSettings.AppSettings["UseAmazoneS3"] == "True" ? true : false;
         amazoneS3AKey = ConfigurationSettings.AppSettings["AmazoneS3AKey"];
         amazoneS3SAKey = ConfigurationSettings.AppSettings["AmazoneS3SAKey"];
         amazoneS3Bucket = ConfigurationSettings.AppSettings["AmazoneS3Bucket"].ToLower();
         amazoneS3BucketLocation = ConfigurationSettings.AppSettings["AmazoneS3BucketLocation"].ToUpper();
      }

      private void InitializeService()
      {
         eventLog = new EventLog();
         InitializeEventLog();

         try
         {
            monLog = new MonitoringLog();
            blnIsEncoding = false;
         }
         catch (Exception ex)
         {
            eventLog.WriteEntry("Service Initialization aborted. Service is not running stable!\r\nError: " + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, EventLogEntryType.Error);
         }
      }

      private void InitializeEventLog()
      {
         if (!System.Diagnostics.EventLog.SourceExists("VideoConverter"))
         {
            System.Diagnostics.EventLog.CreateEventSource("VideoConverter", "CSBooster");
         }
         eventLog.Source = "VideoConverter";
         eventLog.Log = "CSBooster";
      }
      #endregion PRIVATE INIT METHODES

      #region PRIVATE SYSTEM METHODES
      private void WriteMonitoringLog(string transactionID, string objectID, BaseActions baseAction, int step, string stepDescription, _4screen.CSB.Common.MonitoringLogState state, string message)
      {
         try
         {
            if (BaseActions.VideoConvert == baseAction)
               monLog.BaseAction = "Video Convert";

            monLog.TransactionID = transactionID;
            monLog.ObjectID = objectID;
            monLog.Step = step;
            monLog.StepDescription = stepDescription;
            monLog.Message = message;
            monLog.State = state;
            monLog.Insert();
         }
         catch
         { }
      }
      #endregion PRIVATE SYSTEM METHODES

      #region PRIVATE CONVERT QUEUE METHODES
      private void CheckConvertQueue()
      {
         try
         {
            string strServerName = System.Environment.MachineName;
            ConvertQueue convertQueue = new ConvertQueue();
            if (convertQueue.LoadNextJob(strServerName))
            {
               bool blnIsOK = true;
               strTransactionID = Guid.NewGuid().ToString();

               if (convertQueue.ObjectType == _4screen.CSB.Common.ObjectType.Picture)
               {
                  // TODO: Convert Picture
               }
               else if (convertQueue.ObjectType == _4screen.CSB.Common.ObjectType.Video)
               {
                  DataObjectVideo dov = null;
                  try
                  {
                     dov = new _4screen.CSB.DataAccess.Business.DataObjectVideo(udc);
                     dov.ObjectID = convertQueue.ObjectID;
                     dov.Load(null, true);
                     dov.ShowState = ObjectShowState.InProgress;
                     dov.UpdateBackground();

                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 1, "Load DO (Step 1 / 15)", _4screen.CSB.Common.MonitoringLogState.OK, "");
                  }
                  catch (System.Exception ex)
                  {
                     blnIsOK = false;
                     string strCheckMessage = string.Format("Error: Can't load DataObjectVideo<br/>DataObjectVideo.ObjectID = {{{0}}}<br/>System.Exception Message: {1}", convertQueue.ObjectID, ex.Message);
                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 1, "Load DO (Step 1 / 15)", _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, strCheckMessage);
                     convertQueue.TryingCount++;
                     convertQueue.ConvertMessage = strCheckMessage;
                     convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.ConvertError;
                     convertQueue.LastTimeStamp = DateTime.Now;
                     convertQueue.Update();
                  }

                  if (blnIsOK)
                  {
                     if (DoEncoding(convertQueue, dov)) // succeeded
                     {
                        WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 14, "Convert SUCCEEDED (Step 14 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");
                        // TODO: SEND MAIL
                     }
                     else // aborted
                     {
                        WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 14, "Convert ABORTED (Step 14 / 14)", _4screen.CSB.Common.MonitoringLogState.Aborted, "please check detail process steps");
                        // TODO: SEND MAIL
                     }
                     blnIsEncoding = false;
                  }
               }
            }
         }
         catch (System.Exception ex)
         {
            string strCheckMessage = string.Format("System.Exception<br/><br/>{0}", ex.Message);
            eventLog.WriteEntry(strCheckMessage, EventLogEntryType.Error);
            WriteMonitoringLog("", "", BaseActions.VideoConvert, 0, "Get Convert Jobs from Convert Queue", _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, strCheckMessage);
         }
      }
      #endregion PRIVATE CONVERT QUEUE METHODES

      #region PRIVATE ENCODING METHODES
      private bool DoEncoding(_4screen.CSB.DataAccess.Business.ConvertQueue convertQueue, _4screen.CSB.DataAccess.Business.DataObjectVideo dov)
      {
         blnIsEncoding = true;
         bool blnIsOK = true;
         string strCheckMessage = string.Empty;
         string strStatisticFileExtension = string.Empty;
         string strNewGUIDFilename = string.Empty;
         string strNewGUIDFilenameWithoutExt = string.Empty;
         string strTargetOutPath = string.Empty;
         string strSourcePathFile = string.Empty;
         string strConverterLocalSourcePathFile = string.Empty;
         string strOriginalLocation = string.Empty;
         int intStatisticFileSizeByte = -1;
         DateTime dtmStart = DateTime.Now;
         int intSizeBite = -1;

         #region DO SOME FILE MANIPULATIONS AND SET THE RIGHT PATH

         // check if the source folder and file exists
         if (blnIsOK)
         {
            try
            {
               //The ConverterRootPathUpload in Web.Config may differ from the one in the app.config
               //Im WebConfig  =  key="ConverterRootPathUpload" value="\\csbmedia\csbooster\Upload"
               //Im AppConfig  =   key="ConverterRootPathUpload" value="Z:\Upload"
               //Sample Upload Path = \\csbmedia\csbooster\Upload\ec572c72-e3fc-4486-82ec-0b2fe0a6d60b\V\20c81a3eb0bc43ac8016f84383893908PA084165.MOV
               //Convert to = Z:\Upload\ec572c72-e3fc-4486-82ec-0b2fe0a6d60b\V\20c81a3eb0bc43ac8016f84383893908PA084165.MOV
               string strPartialPath = dov.OriginalLocation.Substring(dov.OriginalLocation.IndexOf(dov.UserID));

               strSourcePathFile = Path.Combine(strConverterRootPathUpload, strPartialPath);
               intSnapshotAfterSec = Convert.ToInt32(convertQueue.VideoPreviewPictureTimepointSec);

               if (blnIsOK)
               {
                  if (!vbio.FileExists(strSourcePathFile))
                  {
                     strCheckMessage = string.Format("Uploaded file doesn't exist.<br/>Can't find uploaded media '{0}'", strSourcePathFile);
                     blnIsOK = false;
                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 2, "Check if source folder and file exists (Step 2 / 15)", _4screen.CSB.Common.MonitoringLogState.Aborted, strCheckMessage);
                  }
                  else
                  {
                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 2, "Check if source folder and file exists (Step 2 / 15)", _4screen.CSB.Common.MonitoringLogState.OK, "");
                  }
               }
            }
            catch (Exception ex)
            {
               blnIsOK = false;
               strCheckMessage = string.Format("Base error occurred<br/>DataObjectVideo.ObjectID = {{{0}}}<br/>System.Exception Message: {1}", dov.ObjectID, ex.Message);
               WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 2, "Check if source folder and file exists (Step 2 / 15)", _4screen.CSB.Common.MonitoringLogState.Aborted, strCheckMessage);
            }
         }

         // rename source file to a GUID file
         if (blnIsOK)
         {
            try
            {
               FileInfo fi = new FileInfo(strSourcePathFile);
               strStatisticFileExtension = fi.Extension;
               strNewGUIDFilename = dov.ObjectID + fi.Extension;
               strConverterLocalSourcePathFile = Path.Combine(strLocalPathSource, strNewGUIDFilename);

               strTargetOutPath = strSourcePathFile.Replace(strConverterRootPathUpload, strConverterRootPathMedia).Replace(fi.Name, "");
               if (!Directory.Exists(strTargetOutPath))
                  Directory.CreateDirectory(strTargetOutPath);

               try
               {
                  // copy the file to a GUID File
                  vbio.CopyFile(strSourcePathFile, strConverterLocalSourcePathFile, true);
                  WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 3, "copy source file to a GUID file (Step 3 / 15)", _4screen.CSB.Common.MonitoringLogState.OK, "");
               }
               catch (Exception ex)
               {
                  blnIsOK = false;
                  strCheckMessage = string.Format("Error: Can't copy original media to a new GUID file<br/>DataObjectVideo.ObjectID = {{{0}}}<br/>System.Exception Message: {1}", dov.ObjectID, ex.Message);
                  WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 3, "copy source file to a GUID file (Step 3 / 15)", _4screen.CSB.Common.MonitoringLogState.Aborted, strCheckMessage);
               }

               if (blnIsOK)
               {
                  try
                  {
                     // move original to the archive
                     string strTmpArchivePath = strTargetOutPath + @"A\"; //-->X:\CSBooster\Media\ec572c72-e3fc-4486-82ec-0b2fe0a6d60b\V\A
                     if (!Directory.Exists(strTmpArchivePath))
                        Directory.CreateDirectory(strTmpArchivePath);

                     strOriginalLocation = strSourcePathFile;		// set the original location to the upload dir; needed if move would crash
                     vbio.MoveFile(strSourcePathFile, Path.Combine(strTmpArchivePath, strNewGUIDFilename), true);
                     strOriginalLocation = Path.Combine(strTmpArchivePath, strNewGUIDFilename);

                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 4, "move source to archive (Step 4 / 15)", _4screen.CSB.Common.MonitoringLogState.OK, "");
                  }
                  catch (Exception ex)
                  {
                     strCheckMessage = string.Format("Error: Can't move original media to the archive folder<br/>DataObjectVideo.ObjectID = {{{0}}}<br/>System.Exception Message: {1}", dov.ObjectID, ex.Message);
                     WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 4, "move source to archive (Step 4 / 15)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, strCheckMessage);
                  }
               }

               fi = null;
            }
            catch (Exception ex)
            {
               blnIsOK = false;
               strCheckMessage = string.Format("Base error in copy media to a GUID file<br/>DataObjectVideo.ObjectID = {{{0}}}<br/>System.Exception Message: {1}", dov.ObjectID, ex.Message);
               WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 3, "copy source file to a GUID file (Step 3 / 15)", _4screen.CSB.Common.MonitoringLogState.Aborted, strCheckMessage);
            }
         }
         #endregion DO SOME FILE MANIPULATIONS AND SET THE RIGHT PATH


         #region DO THE ENCODING
         if (blnIsOK)
         {
            if (!strConverterLocalSourcePathFile.EndsWith(".flv"))
            {
               ConvertVideo(strConverterLocalSourcePathFile, strLocalPathTarget, dov.ObjectID);
            }
            else
            {
               try
               {
                  System.IO.File.Copy(strConverterLocalSourcePathFile, System.IO.Path.Combine(strLocalPathTarget, dov.ObjectID + ".flv"));
               }
               catch (Exception ex)
               {
                  WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 9, "couldn't copy to " + strLocalPathTarget, _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, ex.Message);
                  blnIsOK = false;
               }
            }

            // check if movie FLV exists
            // if yes, convert has succeed (99%)
            // generate thumbnails if FLV file exists
            if (vbio.FileExists(System.IO.Path.Combine(strLocalPathTarget, dov.ObjectID + ".flv")))
            {
               WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 6, "video converted (Step 6 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");

               // do the preview snapshots
               GenerateSnapshot(dov.ObjectID, dov.UserID);

               // move the files from local convert server to the media server
               try
               {
                  //Check if The video Should be send to Amazone or moved to the Mediaserver
                  if (!useAmazoneS3)
                  {
                     if (!Directory.Exists(strTargetOutPath + @"FLV\"))
                        Directory.CreateDirectory(strTargetOutPath + @"FLV\");
                     vbio.MoveFile(System.IO.Path.Combine(strLocalPathTarget, dov.ObjectID + ".flv"), System.IO.Path.Combine(strTargetOutPath + @"FLV\", dov.ObjectID + ".flv"), true);
                  }
                  else
                  {
                     intSizeBite = (int)UploadToAmazone(System.IO.Path.Combine(strLocalPathTarget, dov.ObjectID + ".flv"), dov.ObjectID, dov.UserID);
                     if (intSizeBite == -1)
                        blnIsOK = false;
                     vbio.DeleteFile(System.IO.Path.Combine(strLocalPathTarget, dov.ObjectID + ".flv"));
                  }

                  WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 9, "move converted video to the media path (Step 9 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");
               }
               catch (Exception ex)
               {
                  blnIsOK = false;
                  WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 9, "move converted video to the media path (Step 9 / 14)", _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, ex.Message);
               }
            }
            else
            {
               blnIsOK = false;
               strCheckMessage = string.Format("CRITICAL ERROR: converted FLV movie does not exist! Please check this: " + System.IO.Path.Combine(strTargetOutPath, dov.ObjectID + ".flv"));
               WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 6, "video converted (Step 6 / 14)", _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, strCheckMessage);
            }

            // finale check for running convert processes
            try
            {
               Process[] localByName = Process.GetProcessesByName("convert");
               for (int i = 0; i < localByName.Length; i++)
                  localByName[i].Kill();
            }
            catch
            { }

            blnIsEncoding = false;

            // Get File Size statistics
            try
            {
               FileInfo fiUpload = new FileInfo(strSourcePathFile);
               intStatisticFileSizeByte = (int)fiUpload.Length;
               fiUpload = null;
            }
            catch
            { }
         }
         #endregion DO THE ENCODING

         #region WRITE To THE DATABASE
         // Update Queue Item
         int intConvertTryCount = convertQueue.TryingCount;
         intConvertTryCount++;

         try
         {
            if (blnIsOK)
            {
               // succeded
               TimeSpan ts = (TimeSpan)(DateTime.Now - dtmStart);
               int intStatisticWorkTimeSec = (int)ts.TotalSeconds;

               convertQueue.TryingCount = intConvertTryCount;
               convertQueue.ConvertMessage = strCheckMessage;
               convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.Convertet;
               convertQueue.LastTimeStamp = DateTime.Now;
               convertQueue.StatisticFileExtension = strStatisticFileExtension;
               convertQueue.StatisticFileSizeByte = intStatisticFileSizeByte;
               convertQueue.StatisticWorkTimeSec = intStatisticWorkTimeSec;
               convertQueue.Update();

               try
               {
                  // delete the source file in the upload folder
                  File.Delete(strSourcePathFile);
                  // delete temporary convert source file
                  File.Delete(strConverterLocalSourcePathFile);
               }
               catch
               { }
            }
            else
            {
               // aborted
               convertQueue.TryingCount = intConvertTryCount;
               if (intConvertTryCount >= maxRetries)
                  convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.ConvertError;
               else
               {
                  convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.NotConvertet;
                  convertQueue.LookID = "";
               }

               convertQueue.ConvertMessage = strCheckMessage;
               convertQueue.LastTimeStamp = DateTime.Now;
               convertQueue.StatisticFileExtension = strStatisticFileExtension;
               convertQueue.StatisticFileSizeByte = intStatisticFileSizeByte;
               convertQueue.StatisticWorkTimeSec = -1;
               convertQueue.Update();
            }

            WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 12, "update convert queue in database (Step 12 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");
         }
         catch
         {
            //Error: update convert queue crashed
            strCheckMessage = string.Format("ConvertQueue final update crashed! Please check the convert queue in the database");
            WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 12, "update convert queue in database (Step 12 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, strCheckMessage);
         }

         // Update Object Item
         try
         {
            if (blnIsOK)
            {
               // succeded
               string strHTTPPathOutput = string.Empty;
               string strHTTPImageOutput = string.Empty;
               string strHTTPImageLargeOutput = string.Empty;
               string strTmpOutPathAndFile = strTargetOutPath + @"FLV\" + dov.ObjectID + ".flv";

               //File Size
               try
               {
                  FileInfo fiUpload = new FileInfo(strTmpOutPathAndFile);
                  intSizeBite = (int)fiUpload.Length;
                  fiUpload = null;
               }
               catch
               { }

               if (strTmpOutPathAndFile.StartsWith(strConverterRootPathMedia))
                  strHTTPPathOutput = strTmpOutPathAndFile.Substring(strConverterRootPathMedia.Length).Replace("\\", "/");

               dov.SetImageType(_4screen.CSB.Common.PictureVersion.L, _4screen.CSB.Common.PictureFormat.Jpg);
               dov.SetImageType(_4screen.CSB.Common.PictureVersion.S, _4screen.CSB.Common.PictureFormat.Jpg);
               dov.SetImageType(_4screen.CSB.Common.PictureVersion.XS, _4screen.CSB.Common.PictureFormat.Jpg);
               dov.Image = dov.ObjectID;
               dov.ConvertMessage = string.Empty;
               dov.DurationSecond = -1;	// TODO kann mit dieser Engine nicht ausgelesen werden
               dov.Location = strHTTPPathOutput;
               dov.OriginalLocation = strOriginalLocation;
               dov.SizeByte = intSizeBite;
               //Depending on the Community Settings set the default Showstate to this object
               DataObject doCont = new DataObject(udc);
               doCont.ObjectID = dov.CommunityID;
               doCont.Load(null, true);
               if (doCont.State != _4screen.CSB.Common.ObjectState.Added)
               {
                  if (doCont.ObjectType == _4screen.CSB.Common.ObjectType.Community)
                  {

                     if (!string.IsNullOrEmpty(doCont.GetXMLValue("Managed")) && doCont.GetXMLValue("Managed") == "1")
                     {
                        if (DataObjectCommunity.IsUserOwner(dov.CommunityID, dov.UserID))
                        {
                           dov.ShowState = _4screen.CSB.Common.ObjectShowState.Published;
                        }
                        else
                        {
                           dov.ShowState = _4screen.CSB.Common.ObjectShowState.Draft;
                        }
                     }
                     else
                     {
                        dov.ShowState = _4screen.CSB.Common.ObjectShowState.Published;
                     }
                  }
                  else
                  {
                     dov.ShowState = _4screen.CSB.Common.ObjectShowState.Published;
                  }
               }
               dov.UpdateBackground();
            }
            else
            {
               dov.ShowState = ObjectShowState.ConversionFailed;
               dov.ConvertMessage = strCheckMessage;
               dov.DurationSecond = -1;
               dov.Location = "";
               dov.OriginalLocation = strOriginalLocation;
               dov.SizeByte = -1;
               dov.Image = "";
               dov.UpdateBackground();
            }

            WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 13, "update object item in database (Step 13 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");
         }
         catch
         {
            //Error: update convert queue crashed
            strCheckMessage = string.Format("Object item final database update crashed! Please check the Object item in the database");
            WriteMonitoringLog(strTransactionID, dov.ObjectID, BaseActions.VideoConvert, 13, "update object item in database (Step 13 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, strCheckMessage);
         }

         #endregion WRITE TO DATABASE

         if (strCheckMessage.Length > 0)
            eventLog.WriteEntry(strCheckMessage, EventLogEntryType.Error);

         if (blnIsOK)
            return true;
         else
            return false;
      }

      private long UploadToAmazone(string ConvertedFile, string objectID, string userID)
      {
         long StreamLength = -1;
         try
         {
            //Attention: Key Is case sensitive
            string keyName = userID + "/" + Helper.GetMediaFolder(ObjectType.Video) + "/flv/" + objectID + ".flv";
            keyName = keyName.ToLower();
            ThreeSharpConfig config = new ThreeSharpConfig();
            config.AwsAccessKeyID = amazoneS3AKey;
            config.AwsSecretAccessKey = amazoneS3SAKey;
            if (amazoneS3BucketLocation == "EU")
               config.Format = CallingFormat.SUBDOMAIN;
            else
               config.Format = CallingFormat.REGULAR;
            IThreeSharp service = new ThreeSharpQuery(config);
            ObjectAddRequest objectAddRequest = new ObjectAddRequest(amazoneS3Bucket, keyName);
            objectAddRequest.LoadStreamWithFile(ConvertedFile);
            objectAddRequest.Headers.Add("x-amz-acl", "public-read");
            ObjectAddResponse objectAddResponse = service.ObjectAdd(objectAddRequest);
            objectAddResponse.DataStream.Close();
            FileInfo fiUpload = new FileInfo(ConvertedFile);
            StreamLength = (int)fiUpload.Length;
            fiUpload = null;
         }
         catch (Exception e)
         {
            WriteMonitoringLog(strTransactionID, objectID, BaseActions.VideoConvert, 13, "Amazone S3 upload failed", _4screen.CSB.Common.MonitoringLogState.Aborted, e.Message);
         }
         return StreamLength;
      }

      // make the image snapshot
      private int GenerateSnapshot(string outObjectID, string UserID)
      {
         try
         {
            Process process = new Process();
            process.StartInfo.FileName = strSnapShotEngine;
            process.StartInfo.Arguments = UserID + " " + outObjectID + " " + intSnapshotAfterSec.ToString() + " " + strImgLActionProfile + " " + strImgSActionProfile + " " + strImgXSActionProfile;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            while (true)
            {
               System.Threading.Thread.Sleep(1000);
               if (process.HasExited)
               {
                  break;
               }
            }

            return 0;
         }
         catch
         {
            return 1;
         }

      }

      // do the real video encoding
      private int ConvertVideo(string sourcePathAndFilename, string targetOutPath, string objectID)
      {
         try
         {
            // copy base convert settings to be sure, that the whole video will be converted
            //File.Copy(strInitCOMPLETEFilePathName, strInitFilePathName, true);
            //System.Threading.Thread.Sleep(500);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = strConverterEngine;
            process.StartInfo.Arguments = " /flash /flashp " + strFLVProfil + " /outputwidth " + strVideoWidth + " /outputheight " + strVideoHeight + " /file " + sourcePathAndFilename + " /odir " + targetOutPath;
            if (blnShowConverterProgressOnEncoding)
               process.StartInfo.CreateNoWindow = false;
            else
               process.StartInfo.CreateNoWindow = true;

            process.Start();

            while (true)
            {
               System.Threading.Thread.Sleep(2000);
               if (process.HasExited)
               {
                  break;
               }
            }

            WriteMonitoringLog(strTransactionID, objectID, BaseActions.VideoConvert, 5, "video converting (Step 5 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");

            return 0;
         }
         catch
         {
            WriteMonitoringLog(strTransactionID, objectID, BaseActions.VideoConvert, 5, "video converting (Step 5 / 14)", _4screen.CSB.Common.MonitoringLogState.Aborted, "");

            Process[] localByName = Process.GetProcessesByName("convert");
            for (int i = 0; i < localByName.Length; i++)
               localByName[i].Kill();

            return 1;
         }

      }
      #endregion PRIVATE ENCODING METHODES
   }
}
