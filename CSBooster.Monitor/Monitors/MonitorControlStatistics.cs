//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace _4screen.CSB.Monitor
{
  public partial class MonitorControlStatistics : MonitorControl
  {
    private delegate void ChartCallback();
    private delegate void ServiceFormCallback(string text);

    public MonitorControlStatistics(MonitorControlStatisticsProperties properties)
      : base(properties)
    {
      InitializeComponent();
    }

    public override string Text
    {
      get
      {
        if (this.properties != null && ((MonitorControlStatisticsProperties)this.properties).ServiceName != null)
          return "Statistik (" + ((MonitorControlStatisticsProperties)this.properties).ServiceName + ")";
        else
          return "Statistik (inaktiv)";
      }
    }

    public DevExpress.XtraCharts.ChartControl ChartControl
    {
      get { return this.chartControl1; }
      set { this.chartControl1 = value; }
    }

    public void Reload(bool callAsync)
    {
      try
      {
        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
        chartTitle.Antialiasing = true;
        chartTitle.Font = new Font("Tahoma", 10);
        chartTitle.TextColor = Color.Black;
        chartTitle.Text = "lade Daten...";
        this.chartControl1.Titles.Clear();
        this.chartControl1.Titles.Add(chartTitle);

        // Wait until the chart control is ready
        IntPtr handle = this.chartControl1.Handle;

        if (callAsync)
          ReloadAsync();
        else
          ReloadSync();
      }
      catch (SoapException ex)
      {
        CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message);
      }
      catch (WebException exc)
      {
        CSBoosterMonitor.GetServiceForm().AppendMessage(exc.Message);
      }
    }

    protected override void OnTimerTick(object sender, System.EventArgs e)
    {
      this.Reload(true);
    }

    private void OnReloadItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.Reload(true);
    }

    private void OnPropertyItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      base.OnPropertyItemClickBase(sender, e);
    }

    private void ReloadSync()
    {
      MethodInfo methodInfo = ServiceHelper.GetMethodInfo(((MonitorControlStatisticsProperties)this.properties).ServiceName, "", "", new Type[] { });
      Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlStatisticsProperties)this.properties).ServiceLocation]);
      ChartDataPair[] chartDataPairList = (ChartDataPair[])methodInfo.Invoke(service, new object[] { });

      this.chartDataPairBindingSource.DataSource = chartDataPairList;
      this.chartControl1.Titles.Clear();
      this.chartControl1.Refresh();
    }

    private void ReloadAsync()
    {
      MethodInfo methodInfo = ServiceHelper.GetMethodInfo(((MonitorControlStatisticsProperties)this.properties).ServiceName, "Begin", "", new Type[] { typeof(AsyncCallback), typeof(object) });
      Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlStatisticsProperties)this.properties).ServiceLocation]);
      methodInfo.Invoke(service, new object[] { new AsyncCallback(DataLoaded), null });
    }

    private void DataLoaded(IAsyncResult asyncResult)
    {
      try
      {
        MethodInfo methodInfo = ServiceHelper.GetMethodInfo(((MonitorControlStatisticsProperties)this.properties).ServiceName, "End", "", new Type[] { typeof(IAsyncResult) });
        Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlStatisticsProperties)this.properties).ServiceLocation]);
        ChartDataPair[] chartDataPairList = (ChartDataPair[])methodInfo.Invoke(service, new object[] { asyncResult });
        this.chartDataPairBindingSource.DataSource = chartDataPairList;
        this.chartControl1.Invoke(new ChartCallback(this.chartControl1.Titles.Clear));
        this.chartControl1.Invoke(new ChartCallback(this.chartControl1.Refresh));
        CSBoosterMonitor.GetServiceForm().Invoke(new ServiceFormCallback(CSBoosterMonitor.GetServiceForm().AppendMessage), new object[] { "Daten aktualisiert -> " + this.Text });
      }
      catch (Exception ex1)
      {
        try
        {
          CSBoosterMonitor.GetServiceForm().Invoke(new ServiceFormCallback(CSBoosterMonitor.GetServiceForm().AppendMessage), new object[] { ex1.InnerException.Message + " -> " + this.Text });
          this.chartDataPairBindingSource.Clear();
          this.chartControl1.Titles[0].Text = "fehlende Berechtigung oder Fehler aufgetreten";
          this.chartControl1.Invoke(new ChartCallback(this.chartControl1.Refresh));
        }
        catch (Exception ex2)
        {
          CSBoosterMonitor.GetServiceForm().Invoke(new ServiceFormCallback(CSBoosterMonitor.GetServiceForm().AppendMessage), new object[] { ex2.Message });
        }
      }
    }
  }
}
