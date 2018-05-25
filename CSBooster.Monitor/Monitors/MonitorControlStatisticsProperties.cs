//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  [Serializable]
  [System.Xml.Serialization.XmlInclude(typeof(MonitorControlStatisticsProperties))]
  public class MonitorControlStatisticsProperties : MonitorControlProperties, IServiceSwitchable
  {
    private MonitorControlStatistics control;
    private Type serviceReturnDataType;
    private string serviceName;
    private string serviceLocation;

    public MonitorControlStatisticsProperties()
      : base()
    {
      this.control = new MonitorControlStatistics(this);
      this.serviceReturnDataType = typeof(ChartDataPair[]);
    }

    [Category("Service")]
    [DisplayName("Service Standort")]
    [TypeConverter(typeof(ServiceLocationConverter))]
    public string ServiceLocation
    {
      get { return this.serviceLocation; }
      set
      {
        this.serviceLocation = value;
        if (this.serviceName != null)
        {
          this.control.Reload(true);
        }
      }
    }

    [Category("Statistik")]
    [DisplayName("Typ")]
    [TypeConverter(typeof(ServiceNameTypeConverter))]
    public string ServiceName
    {
      get { return this.serviceName; }
      set
      {
        this.serviceName = value;

        Control parent = this.control.Parent;
        while (parent != null && !(parent is DevExpress.XtraBars.Docking.DockPanel))
          parent = parent.Parent;
        if (parent != null)
          parent.Text = "Statistik (" + this.serviceName + ")";

        control.Reload(true);
      }
    }

    [Category("Statistik")]
    [DisplayName("Autom. Aktualisierung")]
    [TypeConverter(typeof(TypeConverter))]
    public bool AutoUpdate
    {
      get { return this.control.Timer.Enabled; }
      set { this.control.Timer.Enabled = value; }
    }

    [Category("Statistik")]
    [DisplayName("Aktualisierungsintervall [s]")]
    [Editor(typeof(DevExpress.XtraEditors.SpinEdit), typeof(DevExpress.XtraEditors.BaseEdit))]
    public int UpdateInterval
    {
      get { return this.control.Timer.Interval / 1000; }
      set
      {
        if (value >= 5 && value <= 3600)
          this.control.Timer.Interval = value * 1000;
      }
    }

    [Category("Diagramm")]
    [DisplayName("Legende")]
    [TypeConverter(typeof(TypeConverter))]
    public bool Legend
    {
      get { return this.control.ChartControl.Legend.Visible; }
      set { this.control.ChartControl.Legend.Visible = value; }
    }

    [Category("Diagramm")]
    [DisplayName("Beschriftungen")]
    [TypeConverter(typeof(TypeConverter))]
    public bool Labels
    {
      get { return this.control.ChartControl.Series[0].Label.Visible; }
      set { this.control.ChartControl.Series[0].Label.Visible = value; }
    }

    [Category("Diagramm")]
    [DisplayName("Werte prozentual")]
    [TypeConverter(typeof(TypeConverter))]
    public bool ValueAsPercent
    {
      get { return ((DevExpress.XtraCharts.PiePointOptions)this.control.ChartControl.Series[0].PointOptions).PercentOptions.ValueAsPercent; }
      set
      {
        ((DevExpress.XtraCharts.PiePointOptions)this.control.ChartControl.Series[0].PointOptions).PercentOptions.ValueAsPercent = value;
        if (value == true) ((DevExpress.XtraCharts.PiePointOptions)this.control.ChartControl.Series[0].PointOptions).ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
        else ((DevExpress.XtraCharts.PiePointOptions)this.control.ChartControl.Series[0].PointOptions).ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.General;
      }
    }

    public override Control GetControl()
    {
      return control;
    }

    // Implementation of IServiceSwitchable
    public Type GetServiceReturnType()
    {
      return this.serviceReturnDataType;
    }
  }
}
