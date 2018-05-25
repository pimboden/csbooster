//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace _4screen.CSB.Monitor
{
  static class Program
  {
    private static CSBoosterMonitorProperties csboosterMonitorProperties;

    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(CSBoosterMonitorProperties));
        csboosterMonitorProperties = (CSBoosterMonitorProperties)serializer.Deserialize(new StreamReader(Application.StartupPath + @"\settings.config", false));
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't load settings: " + ex.Message, "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      Application.Run(csboosterMonitorProperties.GetCSBoosterMonitor());
    }

    public static CSBoosterMonitorProperties GetProperties()
    {
      return csboosterMonitorProperties;
    }
  }
}
