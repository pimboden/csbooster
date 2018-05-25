//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  [Serializable]
  public class CSBoosterMonitorProperties
  {
    private CSBoosterMonitor csboosterMonitor;
    private Dictionary<string, string> serviceUrls = new Dictionary<string, string>();

    public CSBoosterMonitorProperties()
    {
      this.csboosterMonitor = new CSBoosterMonitor(this);
    }

    [Category("Server")]
    [DisplayName("Datenbank Web Service")]
    public string DatabaseServiceUrl
    {
      get { return this.serviceUrls["Datenbank Web Service"]; }
      set { this.serviceUrls["Datenbank Web Service"] = value; }
    }

    [Category("Server")]
    [DisplayName("Video Konvertor Web Service")]
    public string VideoConverterServiceUrl
    {
      get { return this.serviceUrls["Video Konvertor Web Service"]; }
      set { this.serviceUrls["Video Konvertor Web Service"] = value; }
    }

    [Category("Server")]
    [DisplayName("Video Konvertor 2 Web Service")]
    public string VideoConverter2ServiceUrl
    {
      get { return this.serviceUrls["Video Konvertor 2 Web Service"]; }
      set { this.serviceUrls["Video Konvertor 2 Web Service"] = value; }
    }

    [Browsable(false)]
    public Point FormPosition
    {
      get { return new Point(this.csboosterMonitor.DesktopLocation.X, this.csboosterMonitor.DesktopLocation.Y); }
      set { this.csboosterMonitor.SetDesktopLocation(value.X, value.Y); }
    }

    [Browsable(false)]
    public Size FormSize
    {
      get { return new Size(this.csboosterMonitor.Width, this.csboosterMonitor.Height); }
      set { this.csboosterMonitor.Size = new Size(value.Width, value.Height); }
    }

    [Browsable(false)]
    public FormWindowState WindowState
    {
      get { return this.csboosterMonitor.WindowState; }
      set { this.csboosterMonitor.WindowState = value; }
    }

    public Dictionary<string, string> GetServiceUrls()
    {
      return this.serviceUrls;
    }

    public CSBoosterMonitor GetCSBoosterMonitor()
    {
      return this.csboosterMonitor;
    }
  }
}
