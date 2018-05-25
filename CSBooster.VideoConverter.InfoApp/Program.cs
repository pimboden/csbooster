//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#0.1.0.0		26.03.2007 / TS
//	Updated:	#0.5.2.0		04.07.2007 / TS
//									- entfernen des Drive-Mappings und Umbau auf UNC
//									- stark erweitertes loghandling
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using CSBooster.VideoConverter.InfoApp.Business;
using _4screen.CSB.Common;
using _4screen.CSB.ImageHandler.Business;
using System.Threading;
using System.Reflection;

namespace CSBooster.VideoConverter.InfoApp
{
	class Program
	{
		private static string strOutputPath;
		private static string strUserID;
		private static string strObjectID;
		private static string strVideoFile;
		private static int intSnapShotAfterSec = 5000;
		private static string strImgLActionProfile = "LargeResizedJpg";
		private static string strImgSActionProfile = "SmallCroppedJpg";
		private static string strImgXSActionProfile = "ExtraSmallCroppedJpg";
		private static string strOutputFileSnap = string.Empty;

		static void Main(string[] args)
		{
			//ARGUMENTS: 	
			//0	= UserID 
			//1	= outObjectID 
			//2	= SnapshotAfterSec 
			//3	= ImgLActionProfile
			//4	= ImgSActionProfile
			//5	= ImgXSActionProfile
			// ENABLE FOR DEBUGGING
			//InfoHandler obj1 = new InfoHandler();
			//
			//obj1.WriteMonitoringLog("", strObjectID, "", 7, "generate image snapshot (Step 7 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, "Anzahl argumente: " + args.Length.ToString());

			if (args.Length == 6)
			{
				//Read Arguments
				strOutputPath = ConfigurationSettings.AppSettings["ConverterLocalPathTarget"];
				strUserID = args[0];
				strObjectID = args[1];
				intSnapShotAfterSec = Convert.ToInt32(args[2]) * 1000;
				strImgLActionProfile = args[3];
				strImgSActionProfile = args[4];
				strImgXSActionProfile = args[5];


				string[] strArchiveFile = Directory.GetFiles(ConfigurationSettings.AppSettings["ConverterRootPath"] + "\\Media\\" + strUserID+ "\\" + Helper.GetMediaFolder(ObjectType.Video) + "\\a", "*" + strObjectID + ".*");
				if (strArchiveFile.Length == 1)
				{
					strVideoFile =  strArchiveFile[0];
				}

				if (File.Exists(strVideoFile))
				{
					// set outputfiles
					strOutputFileSnap = System.IO.Path.Combine(strOutputPath, strObjectID + "_Snap.jpg");
					try
					{
						// generate the thumbnails
						GenerateSnapshot();


					}
					catch (Exception ex)
					{
						InfoHandler obj2 = new InfoHandler();
						
						obj2.WriteMonitoringLog("", strObjectID, "", 7, "generate image snapshot (Step 7 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, "System exception: " + ex.Message);
					}
				}
				else
				{
					InfoHandler obj3 = new InfoHandler();
					
					obj3.WriteMonitoringLog("", strObjectID, "", 7, "generate image snapshot (Step 7 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, "input FLV file not found: " + strVideoFile);
				}
			}
		}

		private static void GenerateSnapshot()
		{
			InfoApp.Business.InfoHandler obj = new InfoApp.Business.InfoHandler();
			obj.GenerateSnapshot(strVideoFile, strOutputFileSnap, strObjectID, intSnapShotAfterSec);
			ImageHandler imgHandler = new ImageHandler(ConfigurationSettings.AppSettings["MediaDomainName"], ConfigurationSettings.AppSettings["ConverterRootPath"], strUserID, strObjectID, true, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\configurations");
			imgHandler.DoConvert(strOutputFileSnap, strImgLActionProfile, ImageHandler.ReturnPath.Url);
			imgHandler.DoConvert(strOutputFileSnap, strImgSActionProfile, ImageHandler.ReturnPath.Url);
			imgHandler.DoConvert(strOutputFileSnap, strImgXSActionProfile, ImageHandler.ReturnPath.Url);
			try
			{
				//Now copy the snapshot file to the archive
				string strArchivePath = ConfigurationSettings.AppSettings["ConverterRootPath"] + "\\MEDIA\\" + strUserID + "\\" + Helper.GetMediaFolder(ObjectType.Picture) + "\\A\\";
				if (!Directory.Exists(strArchivePath))
				{
					Directory.CreateDirectory(strArchivePath);
				}
				File.Move(strOutputFileSnap, strArchivePath + strObjectID + ".jpg");
			}
			catch
			{
				//ignore if snapshot could not be moved
			}
		}


	}
}	
