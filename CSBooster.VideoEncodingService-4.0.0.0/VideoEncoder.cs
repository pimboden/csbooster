// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using Affirma.ThreeSharp;
using Affirma.ThreeSharp.Model;
using Affirma.ThreeSharp.Query;

namespace _4screen.CSB.WindowServices
{
    public class VideoEncoder
    {
        private enum BaseActions
        {
            VideoConvert = 0
        }

        private MonitoringLog monitoringLog;
        private bool running = false;
        private string videoActionProfile;
        private string temporaryConversionFolder;
        private string ffmpegExecutable;
        private bool generateMultipleThumbnails;
        private int? numberThumbnails;
        private int? maxThumbnailGenerationTime;
        private bool archiveUpload = false;
        private string mediaFolder = string.Empty;
        private FileInfo watermarkFileInfo = null;
        private bool useAmazoneS3 = false;
        private string amazoneS3AKey = string.Empty;
        private string amazoneS3SAKey = string.Empty;
        private string amazoneS3Bucket = string.Empty;
        private string amazoneS3BucketLocation = string.Empty;
        private string transactionId;
        private int checkIntervalSecond = 10;
        private DateTime conversionStart;
        private UserDataContext adminUdc;

        public System.Diagnostics.EventLog EventLog { get; set; }

        public VideoEncoder()
        {
            ffmpegExecutable = ConfigurationManager.AppSettings["FFMpegExecutable"];
            temporaryConversionFolder = ConfigurationManager.AppSettings["TemporaryConversionFolder"];

            int.TryParse(ConfigurationManager.AppSettings["CheckIntervalSecond"], out this.checkIntervalSecond);
            if (checkIntervalSecond < 1)
                checkIntervalSecond = 1;
            else if (checkIntervalSecond > 3600)
                checkIntervalSecond = 3600;

            generateMultipleThumbnails = bool.Parse(ConfigurationManager.AppSettings["GenerateMultipleThumbnails"]);
            int numberThumbnails;
            if (int.TryParse(ConfigurationManager.AppSettings["NumberThumbnails"], out numberThumbnails))
                this.numberThumbnails = numberThumbnails;
            maxThumbnailGenerationTime = int.Parse(ConfigurationManager.AppSettings["MaxThumbnailGenerationTime"]);
            videoActionProfile = ConfigurationManager.AppSettings["ActionProfile"];
            archiveUpload = bool.Parse(ConfigurationManager.AppSettings["ArchiveUpload"]);
            mediaFolder = ConfigurationManager.AppSettings["ConverterRootPathMedia"];

            useAmazoneS3 = bool.Parse(ConfigurationManager.AppSettings["UseAmazoneS3"]);
            amazoneS3AKey = ConfigurationManager.AppSettings["AmazoneS3AKey"];
            amazoneS3SAKey = ConfigurationManager.AppSettings["AmazoneS3SAKey"];
            amazoneS3Bucket = ConfigurationManager.AppSettings["AmazoneS3Bucket"].ToLower();
            amazoneS3BucketLocation = ConfigurationManager.AppSettings["AmazoneS3BucketLocation"].ToUpper();

            monitoringLog = new MonitoringLog();

            adminUdc = UserDataContext.GetUserDataContext("admin");
        }

        public void Start()
        {
            running = true;
            while (running)
            {
                bool jobInQueue = false;
                ConvertQueue convertQueue = new ConvertQueue();
                try
                {
                    jobInQueue = convertQueue.LoadNextJob(System.Environment.MachineName);
                }
                catch (Exception e)
                {
                    EventLog.WriteEntry("CSBooster Video Load Job Error: Msg=" + e.Message + ", Trace=" + e.StackTrace, System.Diagnostics.EventLogEntryType.Error);
                }
                if (jobInQueue)
                {
                    conversionStart = DateTime.Now;

                    if (convertQueue.ObjectType == Helper.GetObjectType("Video").NumericId)
                    {
                        transactionId = Guid.NewGuid().ToString();

                        DataObjectVideo video = null;

                        try
                        {
                            video = DataObject.Load<DataObjectVideo>(adminUdc, convertQueue.ObjectID, null, true);
                            video.ShowState = ObjectShowState.InProgress;
                            video.VideoPreviewPictureTimepointSec = convertQueue.VideoPreviewPictureTimepointSec;
                            video.UpdateBackground();

                            EncodeVideos(video);

                            WriteMonitoringLog(transactionId, video.ObjectID, BaseActions.VideoConvert, 0, "Encoding video", _4screen.CSB.Common.MonitoringLogState.OK, string.Format("Video encoding took {0} seconds", (DateTime.Now - conversionStart).TotalSeconds));

                            convertQueue.StatisticWorkTimeSec = (int)(DateTime.Now - conversionStart).TotalSeconds;
                            convertQueue.StatisticFileSizeByte = video.SizeByte;
                            convertQueue.StatisticFileExtension = video.OriginalLocation.Substring(video.OriginalLocation.LastIndexOf('.') + 1);
                            convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.Convertet;
                            convertQueue.Update();
                        }
                        catch (Exception e)
                        {
                            EventLog.WriteEntry("CSBooster Video Encoding Error: Oid=" + convertQueue.ObjectID + ", Msg=" + e.Message + ", Trace=" + e.StackTrace, System.Diagnostics.EventLogEntryType.Error);

                            try
                            {
                                if (video != null)
                                {
                                    WriteMonitoringLog(transactionId, video.ObjectID, BaseActions.VideoConvert, 0, "Encoding video", _4screen.CSB.Common.MonitoringLogState.AbortedMissionCritical, e.Message + " " + e.StackTrace);
                                }

                                convertQueue.ConvertMessage = e.Message;
                                convertQueue.Status = _4screen.CSB.Common.MediaConvertedState.ConvertError;
                                convertQueue.LastTimeStamp = DateTime.Now;
                                convertQueue.StatisticWorkTimeSec = (int)(DateTime.Now - conversionStart).TotalSeconds;
                                if (video != null)
                                {
                                    convertQueue.StatisticFileExtension = video.OriginalLocation.Substring(video.OriginalLocation.LastIndexOf('.') + 1);
                                }
                                convertQueue.Update();

                                if (video != null)
                                {
                                    video.ConvertMessage = e.Message;
                                    video.ShowState = ObjectShowState.ConversionFailed;
                                    video.UpdateBackground();
                                }
                            }
                            catch (Exception e2)
                            {
                                EventLog.WriteEntry("CSBooster Video Encoding Error 2: Oid=" + convertQueue.ObjectID + ", Msg=" + e2.Message + ", Trace=" + e.StackTrace, System.Diagnostics.EventLogEntryType.Error);
                            }
                        }
                    }
                }

                if (running)
                    System.Threading.Thread.Sleep(1000 * this.checkIntervalSecond);
            }
        }

        public void Stop()
        {
            running = false;
        }

        private void EncodeVideos(DataObjectVideo video)
        {
            //Read Default ConfigSettings
            XDocument videoEoncodingConfig = XDocument.Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\configurations\CSBVideoEoncoding.config");
            var root = videoEoncodingConfig.Elements("root").SingleOrDefault();
            if (root != null)
            {
                VideoVersion thumbnailVideoVersion = VideoVersion.None;
                int thumbnailGenerationErrorCode = 0;
                string thumbnailGenerationVideoFormat = string.Empty;
                var actionProfile = (from allActionProfiles in root.Elements("actionProfile")
                                     where allActionProfiles.Attribute("Id").Value == videoActionProfile
                                     select allActionProfiles).SingleOrDefault();
                var thumbnailActionId = actionProfile.Attribute("ThumbnailActionId").Value;
                if (actionProfile != null)
                {
                    Dictionary<string, string> defaultSettings = ReadDefaultSettings(actionProfile);
                    IEnumerable<XElement> actionProfilesConvertActions = GetAllActions(actionProfile);
                    foreach (var action in actionProfilesConvertActions)
                    {
                        string format = action.Elements("format").SingleOrDefault().Value;
                        VideoVersion videoVersion = VideoVersion.None;
                        if (action.Elements("format").SingleOrDefault().Attribute("VideoVersion") != null)
                        {
                            string videoVersionAttribute = action.Elements("format").SingleOrDefault().Attribute("VideoVersion").Value;
                            if (!string.IsNullOrEmpty(videoVersionAttribute))
                            {
                                switch (videoVersionAttribute.ToLower())
                                {
                                    case "xs":
                                        videoVersion = VideoVersion.XS;
                                        break;
                                    case "s":
                                        videoVersion = VideoVersion.S;
                                        break;
                                    case "m":
                                        videoVersion = VideoVersion.M;
                                        break;
                                    case "l":
                                        videoVersion = VideoVersion.L;
                                        break;
                                    default:
                                        videoVersion = VideoVersion.None;
                                        break;
                                }
                            }
                        }
                        var actionSettings = (from allActionSetting in action.Elements("settings").Elements("add")
                                              select new
                                                  {
                                                      key = allActionSetting.Attribute("key").Value,
                                                      value = allActionSetting.Attribute("value").Value
                                                  }).ToDictionary(x => x.key, x => x.value);
                        int errorCode = EncodeVideo(video, format.ToLower(), videoVersion, defaultSettings, actionSettings, action.Attribute("Id").Value != thumbnailActionId);
                        if (action.Attribute("Id").Value == thumbnailActionId)
                        {
                            thumbnailGenerationErrorCode = errorCode;
                            thumbnailGenerationVideoFormat = format.ToLower();
                            thumbnailVideoVersion = videoVersion;
                        }

                    }
                    if (thumbnailGenerationErrorCode == 0)
                    {
                        //The video convertion succeded so the thumbnail can be generated form it
                        GenerateThumbNails(video, thumbnailGenerationVideoFormat, thumbnailVideoVersion);
                    }
                    else
                    {
                        throw new Exception("Video processing failed: Error code " + thumbnailGenerationErrorCode);
                    }
                }
            }
        }


        private IEnumerable<XElement> GetAllActions(XElement actionProfile)
        {
            var actions = from allactions in actionProfile.Elements("action")
                          select allactions;
            return actions;
        }


        private Dictionary<string, string> ReadDefaultSettings(XElement actionProfile)
        {

            Dictionary<string, string> allDefaultSettings =
                (from defaultsettings in actionProfile.Elements("defaultSettings").Elements("add")
                 select new
                            {
                                key = defaultsettings.Attribute("key").Value,
                                value = defaultsettings.Attribute("value").Value
                            }).ToDictionary(x => x.key, x => x.value);
            return allDefaultSettings;
        }

        private int EncodeVideo(DataObjectVideo video, string format, VideoVersion videoVersion, Dictionary<string, string> defaultSettings, Dictionary<string, string> actionSettings, bool deleteEncodedVideo)
        {
            //Console.WriteLine("Encoding '" + video.Title + "' -> " + video.OriginalLocation);

            FileInfo sourceVideoFileInfo = new FileInfo(video.OriginalLocation);
            VideoInfo encodedVideoInfo = new VideoInfo();

            MediaHandler mediaHandler = new MediaHandler();
            mediaHandler.FFMPEGPath = ffmpegExecutable;
            mediaHandler.InputPath = sourceVideoFileInfo.DirectoryName;
            mediaHandler.OutputPath = temporaryConversionFolder;
            mediaHandler.FileName = sourceVideoFileInfo.Name;
            mediaHandler.OutputFileName = string.Format("{2}{0}.{1}", video.ObjectID, format, videoVersion);

            VideoInfo sourceVideoInfo = mediaHandler.Get_Info();

            if (!video.OriginalLocation.ToLower().EndsWith(string.Format(".{0}", format)))
            {
                try
                {
                    if (actionSettings.ContainsKey("VideoWidth"))
                    {
                        //An empty VideoWidth overides the default setting width
                        if (!string.IsNullOrEmpty(actionSettings["VideoWidth"]))
                        {
                            mediaHandler.Width = Convert.ToInt32(actionSettings["VideoWidth"]);
                            if (actionSettings.ContainsKey("VideoHeight") && !string.IsNullOrEmpty(actionSettings["VideoHeight"]) && actionSettings["VideoHeight"] != "auto")
                            {
                                mediaHandler.Height = Convert.ToInt32(actionSettings["VideoHeight"]);
                            }
                            else
                            {
                                int calculatedHeight = (int)(mediaHandler.Width * (double)sourceVideoInfo.Height / (double)sourceVideoInfo.Width);
                                if (calculatedHeight % 2 != 0)
                                    calculatedHeight += 1;
                                mediaHandler.Height = calculatedHeight;
                            }
                        }
                    }
                    else if (defaultSettings.ContainsKey("VideoWidth") && !string.IsNullOrEmpty(defaultSettings["VideoWidth"]))
                    {
                        mediaHandler.Width = Convert.ToInt32(defaultSettings["VideoWidth"]);
                        if (defaultSettings.ContainsKey("VideoHeight") && !string.IsNullOrEmpty(defaultSettings["VideoHeight"]) && defaultSettings["VideoHeight"] != "auto")
                        {
                            mediaHandler.Height = Convert.ToInt32(defaultSettings["VideoHeight"]);
                        }
                        else
                        {
                            int calculatedHeight = (int)(mediaHandler.Width * (double)sourceVideoInfo.Height / (double)sourceVideoInfo.Width);
                            if (calculatedHeight % 2 != 0)
                                calculatedHeight += 1;
                            mediaHandler.Height = calculatedHeight;
                        }
                    }
                }
                catch
                {

                }
                try
                {
                    if (actionSettings.ContainsKey("VideoFrameRate"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["VideoFrameRate"]) && actionSettings["VideoFrameRate"] != "auto")
                        {
                            mediaHandler.FrameRate = Convert.ToDouble(actionSettings["VideoFrameRate"]);
                        }
                    }
                    else if (defaultSettings.ContainsKey("VideoFrameRate") && !string.IsNullOrEmpty(defaultSettings["VideoFrameRate"]) && defaultSettings["VideoFrameRate"] != "auto")
                    {
                        mediaHandler.FrameRate = Convert.ToInt32(defaultSettings["VideoFrameRate"]);
                    }
                }
                catch
                {

                }
                try
                {
                    if (actionSettings.ContainsKey("StartPosition"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["StartPosition"]))
                        {
                            mediaHandler.Start_Position = actionSettings["StartPosition"];
                        }
                    }
                    else if (defaultSettings.ContainsKey("StartPosition") && !string.IsNullOrEmpty(defaultSettings["StartPosition"]))
                    {
                        mediaHandler.Start_Position = defaultSettings["StartPosition"];
                    }
                }
                catch
                {

                }
                try
                {
                    if (actionSettings.ContainsKey("Duration"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["Duration"]))
                        {
                            mediaHandler.Duration = actionSettings["Duration"];
                        }
                    }
                    else if (defaultSettings.ContainsKey("Duration") && !string.IsNullOrEmpty(defaultSettings["Duration"]))
                    {
                        mediaHandler.Duration = defaultSettings["Duration"];
                    }
                }
                catch
                {

                }
                try
                {
                    double sourceVideoBitrate;
                    double.TryParse(sourceVideoInfo.Video_Bitrate.Replace(" kb/s", ""), out sourceVideoBitrate);
                    mediaHandler.Video_Bitrate = sourceVideoBitrate;
                    if (actionSettings.ContainsKey("VideoBitrate"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["VideoBitrate"]) && actionSettings["VideoBitrate"] != "auto")
                        {
                            if (sourceVideoBitrate > Convert.ToDouble(actionSettings["VideoBitrate"]) || sourceVideoBitrate == 0)
                                mediaHandler.Video_Bitrate = Convert.ToDouble(actionSettings["VideoBitrate"]);
                        }
                    }
                    else if (defaultSettings.ContainsKey("VideoBitrate") && !string.IsNullOrEmpty(defaultSettings["VideoBitrate"]) && defaultSettings["VideoBitrate"] != "auto")
                    {
                        if (sourceVideoBitrate > Convert.ToDouble(defaultSettings["VideoBitrate"]) || sourceVideoBitrate == 0)
                            mediaHandler.Video_Bitrate = Convert.ToInt32(defaultSettings["VideoBitrate"]);
                    }
                }
                catch
                {

                }
                try
                {
                    double sourceAudioBitrate;
                    double.TryParse(sourceVideoInfo.Audio_Bitrate.Replace(" kb/s", ""), out sourceAudioBitrate);
                    mediaHandler.Audio_Bitrate = sourceAudioBitrate;
                    if (actionSettings.ContainsKey("AudioBitrate"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["AudioBitrate"]) && actionSettings["AudioBitrate"] != "auto")
                        {
                            if (sourceAudioBitrate > Convert.ToDouble(actionSettings["AudioBitrate"]) || sourceAudioBitrate == 0)
                                mediaHandler.Audio_Bitrate = Convert.ToDouble(actionSettings["AudioBitrate"]);
                        }
                    }
                    else if (defaultSettings.ContainsKey("AudioBitrate") && !string.IsNullOrEmpty(defaultSettings["AudioBitrate"]) && defaultSettings["AudioBitrate"] != "auto")
                    {
                        if (sourceAudioBitrate > Convert.ToDouble(defaultSettings["VideoBitrate"]) || sourceAudioBitrate == 0)
                            mediaHandler.Audio_Bitrate = Convert.ToInt32(defaultSettings["AudioBitrate"]);
                    }
                }
                catch
                {

                }
                try
                {
                    int sourceAudioSamplingRate;
                    int.TryParse(sourceVideoInfo.SamplingRate.Replace(" kb/s", ""), out sourceAudioSamplingRate);
                    mediaHandler.Audio_SamplingRate = sourceAudioSamplingRate == 32000 ? 44100 : sourceAudioSamplingRate;
                    if (actionSettings.ContainsKey("AudioSamplingRate"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["AudioSamplingRate"]) && actionSettings["AudioSamplingRate"] != "auto")
                        {
                            if (sourceAudioSamplingRate > Convert.ToDouble(actionSettings["AudioSamplingRate"]) || sourceAudioSamplingRate == 0)
                                mediaHandler.Audio_SamplingRate = Convert.ToInt32(actionSettings["AudioSamplingRate"]);
                        }
                    }
                    else if (defaultSettings.ContainsKey("AudioSamplingRate") && !string.IsNullOrEmpty(defaultSettings["AudioSamplingRate"]) && defaultSettings["AudioSamplingRate"] != "auto")
                    {
                        if (sourceAudioSamplingRate > Convert.ToDouble(defaultSettings["VideoBitrate"]) || sourceAudioSamplingRate == 0)
                            mediaHandler.Audio_SamplingRate = Convert.ToInt32(defaultSettings["AudioSamplingRate"]);
                    }
                }
                catch
                {

                }

                // Force mono or stereo, no 5:1 supported
                mediaHandler.Channel = sourceVideoInfo.Channel.Contains("mono") ? 1 : 2;

                mediaHandler.ExitProcess = 100000;
                try
                {
                    if (actionSettings.ContainsKey("MaxDecodingTime"))
                    {
                        if (!string.IsNullOrEmpty(actionSettings["MaxDecodingTime"]))
                        {

                            mediaHandler.ExitProcess = Convert.ToInt32(actionSettings["MaxDecodingTime"]);
                        }
                    }
                    else if (defaultSettings.ContainsKey("MaxDecodingTime"))
                    {
                        mediaHandler.ExitProcess = Convert.ToInt32(defaultSettings["MaxDecodingTime"]);
                    }
                }
                catch
                {

                }
                try
                {
                    bool embeddedWatermark = false;
                    if (actionSettings.ContainsKey("EmbeddedWatermark"))
                    {
                        embeddedWatermark = bool.Parse(actionSettings["EmbeddedWatermark"]);
                    }
                    else if (defaultSettings.ContainsKey("EmbeddedWatermark"))
                    {
                        embeddedWatermark = bool.Parse(defaultSettings["EmbeddedWatermark"]);
                    }

                    if (embeddedWatermark)
                    {
                        string watermarkImage = string.Empty;
                        if (actionSettings.ContainsKey("WatermarkImage"))
                        {
                            watermarkImage = actionSettings["WatermarkImage"];
                        }
                        else if (defaultSettings.ContainsKey("WatermarkImage"))
                        {
                            watermarkImage = defaultSettings["WatermarkImage"];
                        }

                        watermarkFileInfo = CreateWatermarkFrame(video, sourceVideoInfo, mediaHandler, watermarkImage);
                        if (watermarkFileInfo != null)
                        {
                            mediaHandler.WaterMarkPath = watermarkFileInfo.DirectoryName;
                            mediaHandler.WaterMarkImage = watermarkFileInfo.Name;
                        }
                    }
                }
                catch
                {

                }
                switch (format)
                {
                    case "wmv":
                        encodedVideoInfo = mediaHandler.Encode_WMV();
                        break;
                    case "mpg":
                        encodedVideoInfo = mediaHandler.Encode_MPG();
                        break;
                    case "mp4":
                        encodedVideoInfo = mediaHandler.Encode_MP4();
                        break;
                    case "mov":
                        encodedVideoInfo = mediaHandler.Encode_MOV();
                        break;
                    case "m4v":
                        encodedVideoInfo = mediaHandler.Encode_M4V();
                        break;
                    case "flv":
                        encodedVideoInfo = mediaHandler.Encode_FLV();
                        break;
                    case "avi":
                        encodedVideoInfo = mediaHandler.Encode_AVI();
                        break;
                    case "3gp":
                        encodedVideoInfo = mediaHandler.Encode_3GP();
                        break;

                }

                if (encodedVideoInfo.ErrorCode == 0)
                {
                    FileInfo encodedVideoFileInfo = new FileInfo(string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion));
                    if (encodedVideoFileInfo.Length == 0)
                    {
                        throw new Exception("Video processing failed: Filesize 0 bytes");
                    }
                }

            }
            else // Already a video in the given format
            {

                File.Copy(video.OriginalLocation, string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion));
            }
            //Move generated Video

            string encodedVideoFile = string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion);
            if (useAmazoneS3)
                UploadToAmazonS3(encodedVideoFile, video.ObjectID.Value, video.UserID.Value, format, videoVersion);
            else
                UploadToMediaFolder(encodedVideoFile, video.ObjectID.Value, video.UserID.Value, format, videoVersion);
            if (deleteEncodedVideo)
            {
                DeleteFile(string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion));
            }
            return encodedVideoInfo.ErrorCode;
        }

        private void GenerateThumbNails(DataObjectVideo video, string format, VideoVersion videoVersion)
        {
            FileInfo sourceVideoFileInfo = new FileInfo(video.OriginalLocation);
            VideoInfo encodedVideoInfo = new VideoInfo();

            MediaHandler mediaHandler = new MediaHandler();
            mediaHandler.FFMPEGPath = ffmpegExecutable;
            mediaHandler.InputPath = sourceVideoFileInfo.DirectoryName;
            mediaHandler.OutputPath = temporaryConversionFolder;
            mediaHandler.FileName = sourceVideoFileInfo.Name;


            mediaHandler.OutputFileName = string.Format("{2}{0}.{1}", video.ObjectID, format, videoVersion);
            VideoInfo sourceVideoInfo = mediaHandler.Get_Info();

            MediaHandler thumbnailMediaHandler = new MediaHandler();

            thumbnailMediaHandler.FFMPEGPath = ffmpegExecutable;
            thumbnailMediaHandler.InputPath = temporaryConversionFolder;
            thumbnailMediaHandler.OutputPath = temporaryConversionFolder;
            thumbnailMediaHandler.FileName = string.Format("{2}{0}.{1}", video.ObjectID, format, videoVersion);
            thumbnailMediaHandler.Image_Format = "jpg";

            // The exit process timeout probably doesn't work
            if (maxThumbnailGenerationTime.HasValue)
                thumbnailMediaHandler.ExitProcess = maxThumbnailGenerationTime.Value;

            if (generateMultipleThumbnails) // TODO: Check how many files have been written
            {
                thumbnailMediaHandler.Multiple_Thumbs = true;
                thumbnailMediaHandler.ImageName = video.ObjectID + "_";
                thumbnailMediaHandler.Auto_Transition_Time = true;
                thumbnailMediaHandler.No_Of_Thumbs = numberThumbnails.Value;
                thumbnailMediaHandler.Thumb_Start_Position = (int)(sourceVideoInfo.Duration_Sec / numberThumbnails);
            }
            else
            {
                thumbnailMediaHandler.ImageName = video.ObjectID.ToString();
                TimeSpan frameTime;
                if (video.VideoPreviewPictureTimepointSec > 0 && video.VideoPreviewPictureTimepointSec < sourceVideoInfo.Duration_Sec)
                    frameTime = new TimeSpan(0, 0, (int)(video.VideoPreviewPictureTimepointSec));
                else
                    frameTime = new TimeSpan(0, 0, (int)(sourceVideoInfo.Duration_Sec * 0.3));
                thumbnailMediaHandler.Frame_Time = frameTime.ToString();
            }

            VideoInfo thumbnailInfo = thumbnailMediaHandler.Grab_Thumb();

            if (thumbnailInfo.ErrorCode == 0)
            {
                string videoThumbnail = string.Format(@"{0}\{1}.jpg", temporaryConversionFolder, video.ObjectID);
                _4screen.CSB.ImageHandler.Business.ImageHandler imgHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(ConfigurationManager.AppSettings["MediaDomainName"], ConfigurationManager.AppSettings["ConverterRootPath"], video.UserID.ToString(), video.ObjectID.ToString(), true, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\configurations");

                Size largePreview = new Size();
                try
                {
                    imgHandler.DoConvert(videoThumbnail, "ExtraSmallJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imgHandler.DoConvert(videoThumbnail, "SmallJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imgHandler.DoConvert(videoThumbnail, "MediumJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imgHandler.DoConvert(videoThumbnail, "LargeJpg", _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    largePreview.Width = imgHandler.ImageInfo.Width;
                    largePreview.Height = imgHandler.ImageInfo.Height;
                }
                catch (Exception e)
                {
                    EventLog.WriteEntry("CSBooster Video Encoding Error - generate Thumnails: " + e.Message + " " + e.StackTrace, System.Diagnostics.EventLogEntryType.Warning);
                }

                string encodedVideoFile = string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion);

                if (archiveUpload)
                    UploadToArchiveFolder(sourceVideoFileInfo.FullName, video.ObjectID.Value, video.UserID.Value);

                UpdateDataObject(video, sourceVideoInfo, largePreview, format, videoVersion);

                DeleteFile(videoThumbnail);
                DeleteFile(encodedVideoFile);
                if (watermarkFileInfo != null)
                    DeleteFile(watermarkFileInfo.FullName);
            }
            else
            {
                throw new Exception("Video thumbnail generation failed: Error code " + thumbnailInfo.ErrorCode + " " + thumbnailInfo.ErrorMessage);
            }
        }

        private static void DeleteFile(string filename)
        {
            try { File.Delete(filename); }
            catch { } // No need for exception handling, temp files only
        }

        private void UploadToArchiveFolder(string videoFile, Guid objectId, Guid userId)
        {
            try
            {
                string targetFolder = string.Format(@"{0}\{1}\{2}\a", mediaFolder, userId, Helper.GetMediaFolder(Helper.GetObjectType("Video").NumericId));
                Directory.CreateDirectory(targetFolder);
                string videoFileExtenison = videoFile.Substring(videoFile.LastIndexOf('.') + 1);
                string targetVideoFile = string.Format(@"{0}\{1}\{2}\a\{3}.{4}", mediaFolder, userId, Helper.GetMediaFolder(Helper.GetObjectType("Video").NumericId), objectId, videoFileExtenison);
                File.Copy(videoFile, targetVideoFile, true);
            }
            catch (Exception e)
            {
                throw new Exception("Archive upload failed: " + e.Message);
            }
        }

        private void UploadToMediaFolder(string videoFile, Guid objectId, Guid userId, string format, VideoVersion videoVersion)
        {
            try
            {
                string targetFolder = string.Format(@"{0}\{1}\{2}\{3}", mediaFolder, userId, Helper.GetMediaFolder(Helper.GetObjectType("Video").NumericId), format);
                if (videoVersion != VideoVersion.None)
                {
                    targetFolder += string.Format(@"\{0}", videoVersion);
                }
                Directory.CreateDirectory(targetFolder);
                string targetVideoFile = string.Format(@"{0}\{1}.{2}", targetFolder, objectId, format);
                File.Copy(videoFile, targetVideoFile, true);
            }
            catch (Exception e)
            {
                throw new Exception("Media upload failed: " + e.Message);
            }
        }

        private void UploadToAmazonS3(string videoFile, Guid objectId, Guid userId, string format, VideoVersion videoVersion)
        {
            try
            {
                string keyNamePath = string.Format("{0}/{1}/{2}", userId, Helper.GetMediaFolder(Helper.GetObjectType("Video").NumericId), format);
                if (videoVersion != VideoVersion.None)
                {
                    keyNamePath += string.Format(@"/{0}", videoVersion);
                }
                string keyName = string.Format(@"{0}/{1}.{2}", keyNamePath, objectId, format).ToLower();

                ThreeSharpConfig config = new ThreeSharpConfig();
                config.AwsAccessKeyID = amazoneS3AKey;
                config.AwsSecretAccessKey = amazoneS3SAKey;
                config.Format = amazoneS3BucketLocation == "EU" ? CallingFormat.SUBDOMAIN : CallingFormat.REGULAR;

                IThreeSharp service = new ThreeSharpQuery(config);
                ObjectAddRequest objectAddRequest = new ObjectAddRequest(amazoneS3Bucket, keyName);
                objectAddRequest.LoadStreamWithFile(videoFile);
                objectAddRequest.Headers.Add("x-amz-acl", "public-read");

                ObjectAddResponse objectAddResponse = service.ObjectAdd(objectAddRequest);
                objectAddResponse.DataStream.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Amazon S3 upload failed: " + e.Message);
            }
        }

        private void UpdateDataObject(DataObjectVideo video, VideoInfo videoInfo, Size largePreview, string format, VideoVersion videoVersion)
        {
            video.SetImageType(_4screen.CSB.Common.PictureVersion.L, _4screen.CSB.Common.PictureFormat.Jpg);
            video.SetImageType(_4screen.CSB.Common.PictureVersion.M, _4screen.CSB.Common.PictureFormat.Jpg);
            video.SetImageType(_4screen.CSB.Common.PictureVersion.S, _4screen.CSB.Common.PictureFormat.Jpg);
            video.SetImageType(_4screen.CSB.Common.PictureVersion.XS, _4screen.CSB.Common.PictureFormat.Jpg);
            video.Image = video.ObjectID.ToString();
            Size videoSize = (videoInfo.Width > 0 && videoInfo.Height > 0) ? new Size(videoInfo.Width, videoInfo.Height) : largePreview;
            video.Width = videoSize.Width;
            video.Height = videoSize.Height;
            video.AspectRatio = (decimal)videoSize.Width / (decimal)videoSize.Height;
            video.ConvertMessage = string.Format("Video Encoding dauerte {0} Sekunden", (DateTime.Now - conversionStart).TotalSeconds);
            video.DurationSecond = (int)videoInfo.Duration_Sec;
            if (archiveUpload)
            {
                string videoFileExtenison = video.OriginalLocation.Substring(video.OriginalLocation.LastIndexOf('.') + 1).ToLower();
                switch (videoFileExtenison)
                {
                    case "wmv":
                        video.OriginalFormat = VideoFormat.Wmv;
                        break;
                    case "mpg":
                        video.OriginalFormat = VideoFormat.Mpg;
                        break;
                    case "mp4":
                        video.OriginalFormat = VideoFormat.Mp4;
                        break;
                    case "mov":
                        video.OriginalFormat = VideoFormat.Mov;
                        break;
                    case "m4v":
                        video.OriginalFormat = VideoFormat.M4v;
                        break;
                    case "flv":
                        video.OriginalFormat = VideoFormat.Flv;
                        break;
                    case "avi":
                        video.OriginalFormat = VideoFormat.Avi;
                        break;
                    case "3gp":
                        video.OriginalFormat = VideoFormat._3gp;
                        break;
                    default:
                        video.OriginalFormat = VideoFormat.Unknow;
                        break;
                }
                video.OriginalLocation = string.Format(@"\{0}\{1}\a\{2}.{3}", video.UserID, Helper.GetMediaFolder(Helper.GetObjectType("Video").NumericId), video.ObjectID, videoFileExtenison);
            }
            else
            {
                video.OriginalLocation = string.Empty;
            }
            FileInfo encodedVideoFileInfo = new FileInfo(string.Format(@"{0}\{3}{1}.{2}", temporaryConversionFolder, video.ObjectID, format, videoVersion));
            video.SizeByte = (int)encodedVideoFileInfo.Length;

            DataObject profileOrCommunity = DataObject.Load<DataObject>(adminUdc, video.CommunityID, null, true);
            if (profileOrCommunity.State != _4screen.CSB.Common.ObjectState.Added)
            {
                if (profileOrCommunity.ObjectType == Helper.GetObjectType("Community").NumericId)
                {
                    DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(adminUdc, video.CommunityID, null, true);
                    if (community.Managed)
                    {
                        video.ShowState = DataObjectCommunity.IsUserOwner(video.CommunityID.Value, video.UserID.Value) ? _4screen.CSB.Common.ObjectShowState.Published : _4screen.CSB.Common.ObjectShowState.Draft;
                    }
                    else
                    {
                        video.ShowState = _4screen.CSB.Common.ObjectShowState.Published;
                    }
                }
                else
                {
                    video.ShowState = _4screen.CSB.Common.ObjectShowState.Published;
                }
            }
            video.UpdateBackground();
        }

        private FileInfo CreateWatermarkFrame(DataObjectVideo video, VideoInfo videoInfo, MediaHandler mediaHandler, string watermarkImage)
        {
            try
            {
                Bitmap watermark = new Bitmap(watermarkImage);
                int watermarkFullWidth = mediaHandler.Width != 0 ? mediaHandler.Width : videoInfo.Width;
                int watermarkFullHeight = mediaHandler.Height != 0 ? mediaHandler.Height : videoInfo.Height;
                Bitmap watermarkFullframe = new Bitmap(watermarkFullWidth, watermarkFullHeight);
                Graphics g = Graphics.FromImage(watermarkFullframe);
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), 0, 0, watermarkFullWidth, watermarkFullHeight);

                // Check if the watermark is larger than the video
                if (watermark.Width > watermarkFullWidth && watermark.Height > watermarkFullHeight)
                    g.DrawImage(watermark, 0, 0, watermarkFullWidth, watermarkFullHeight);
                else if (watermark.Width > watermarkFullWidth)
                    g.DrawImage(watermark, 0, watermarkFullHeight - watermark.Height, watermarkFullWidth, watermark.Height);
                else if (watermark.Height > watermarkFullHeight)
                    g.DrawImage(watermark, watermarkFullWidth - watermark.Width, 0, watermark.Width, watermarkFullHeight);
                else
                    g.DrawImage(watermark, watermarkFullWidth - watermark.Width, watermarkFullHeight - watermark.Height, watermark.Width, watermark.Height);
                g.Dispose();

                string watermarkFrame = string.Format(@"{0}\watermark_{1}.png", temporaryConversionFolder, video.ObjectID);
                watermarkFullframe.Save(watermarkFrame, System.Drawing.Imaging.ImageFormat.Png);

                return new FileInfo(watermarkFrame);
            }
            catch
            {
                return null;
            }
        }

        private void WriteMonitoringLog(string transactionId, Guid? objectId, BaseActions baseAction, int step, string stepDescription, _4screen.CSB.Common.MonitoringLogState state, string message)
        {
            if (BaseActions.VideoConvert == baseAction)
                monitoringLog.BaseAction = "Video Conversion";

            monitoringLog.TransactionID = transactionId;
            monitoringLog.ObjectID = objectId;
            monitoringLog.Step = step;
            monitoringLog.StepDescription = stepDescription;
            monitoringLog.Message = message;
            monitoringLog.State = state;
            monitoringLog.Insert();
        }
    }
}
