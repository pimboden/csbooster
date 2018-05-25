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
using System.Data;
using System.Text;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization;
using System.Reflection;

namespace _4screen.CSB.Monitor
{
  public class MonitorControl : DevExpress.XtraEditors.XtraUserControl
  {
    private Timer timer;
    protected MonitorControlProperties properties;

    public MonitorControl()
    {
    }

    public MonitorControl(MonitorControlProperties properties)
    {
      this.properties = properties;

      timer = new Timer();
      timer.Interval = 5000;
      timer.Tick += new EventHandler(OnTimerTick);
      timer.Start();
      timer.Enabled = false;
    }

    public Timer Timer
    {
      get { return this.timer; }
      set { this.timer = value; }
    }

    public MonitorControlProperties GetProperties()
    {
      return properties;
    }

    protected void OnPropertyItemClickBase(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      PropertyForm propertyForm = PropertyForm.GetInstance();
      propertyForm.SetSelectedObject(this.properties);
      propertyForm.Show();
    }

    protected virtual void OnTimerTick(object sender, System.EventArgs e)
    {
    }
  }
}
