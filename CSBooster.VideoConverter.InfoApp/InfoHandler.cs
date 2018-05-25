//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#0.1.0.0		26.03.2007 / TS
//	Updated:	#0.5.2.0		04.07.2007 / TS
//									- entfernen des Drive-Mappings und Umbau auf UNC
//									- stark erweitertes loghandling
//	Updated:	#0.5.2.1		12.07.2007 / TS
//									- setzen der JEPG-Kompression
//									- setzen der Resolution
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;
using Leadtools.Codecs;
using _4screen.CSB.DataAccess.Business;

namespace CSBooster.VideoConverter.InfoApp.Business
{
	public class InfoHandler
	{
		private Turbine.TVE3 tve;

		public void GenerateSnapshot(string inputFile, string outputFile, string objectID, int snapshotAfterSec)
		{
			_4screen.CSB.DataAccess.Business.MonitoringLog monLog = new MonitoringLog();		// #0.5.2.0

			try
			{
				Turbine.TVE3 tve = new Turbine.TVE3();

				// set license keys:
				tve.Key1 = 198021903;
				tve.Key2 = 1582164514;

				tve.InfoOpen(inputFile);
				tve.InfoSaveInputFrame(snapshotAfterSec, outputFile, 90);
				int intDuration = (int)(tve.InfoGet("totalDurationMs") / 1000d);

				WriteMonitoringLog("", objectID, "", 7, "generate image snapshot (Step 7 / 14)", _4screen.CSB.Common.MonitoringLogState.OK, "");
			}
			catch (System.Exception ex)
			{
				WriteMonitoringLog("", objectID, "", 7, "generate image snapshot (Step 7 / 14)", _4screen.CSB.Common.MonitoringLogState.OKWithWarning, ex.Message);
			}
			finally
			{
				try
				{
					tve.Dispose();
					if (tve != null)
						tve = null;
				}
				catch { }
			}
		}

		internal void WriteMonitoringLog(string transactionID, string objectID, string baseAction, int step, string stepDescription, _4screen.CSB.Common.MonitoringLogState state, string message)
		{
			try
			{
				_4screen.CSB.DataAccess.Business.MonitoringLog monLog = new MonitoringLog();		// #0.5.2.0

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

	}	// END CLASS
}	// END NAMESPACE
