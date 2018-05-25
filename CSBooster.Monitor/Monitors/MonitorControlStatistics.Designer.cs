namespace _4screen.CSB.Monitor
{
  partial class MonitorControlStatistics
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
      DevExpress.XtraCharts.PiePointOptions piePointOptions1 = new DevExpress.XtraCharts.PiePointOptions();
      DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
      DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
      DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
      this.chartDataPairBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
      this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
      this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
      this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
      this.showPropertiesItem = new DevExpress.XtraBars.BarButtonItem();
      this.reloadItem = new DevExpress.XtraBars.BarButtonItem();
      this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.chartDataPairBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
      this.SuspendLayout();
      // 
      // chartDataPairBindingSource
      // 
      this.chartDataPairBindingSource.DataSource = typeof(ChartDataPair);
      // 
      // chartControl1
      // 
      this.chartControl1.AppearanceName = "Pastel Kit";
      this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.chartControl1.Legend.Visible = false;
      this.chartControl1.Location = new System.Drawing.Point(0, 0);
      this.chartControl1.Margin = new System.Windows.Forms.Padding(4);
      this.chartControl1.Name = "chartControl1";
      piePointOptions1.HiddenSerializableString = "to be serialized";
      piePointOptions1.PercentOptions.ValueAsPercent = false;
      piePointOptions1.PointView = DevExpress.XtraCharts.PointView.ArgumentAndValues;
      series1.PointOptions = piePointOptions1;
      series1.PointOptionsTypeName = "PiePointOptions";
      pieSeriesLabel1.LineLength = 15;
      pieSeriesLabel1.HiddenSerializableString = "to be serialized";
      series1.Label = pieSeriesLabel1;
      series1.ArgumentDataMember = "Id";
      series1.View = pieSeriesView1;
      series1.ValueDataMembersSerializable = "Value";
      series1.Name = "Series 1";
      series1.DataSource = this.chartDataPairBindingSource;
      this.chartControl1.Series.AddRange(new DevExpress.XtraCharts.Series[] {
            series1});
      this.chartControl1.SeriesTemplate.PointOptionsTypeName = "PiePointOptions";
      this.chartControl1.SeriesTemplate.View = pieSeriesView2;
      this.chartControl1.Size = new System.Drawing.Size(400, 369);
      this.chartControl1.TabIndex = 0;
      // 
      // barManager1
      // 
      this.barManager1.DockControls.Add(this.barDockControlTop);
      this.barManager1.DockControls.Add(this.barDockControlBottom);
      this.barManager1.DockControls.Add(this.barDockControlLeft);
      this.barManager1.DockControls.Add(this.barDockControlRight);
      this.barManager1.Form = this;
      this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.showPropertiesItem,
            this.reloadItem});
      this.barManager1.MaxItemId = 4;
      // 
      // barDockControlTop
      // 
      this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
      // 
      // barDockControlBottom
      // 
      this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
      // 
      // barDockControlLeft
      // 
      this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
      // 
      // barDockControlRight
      // 
      this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
      // 
      // showPropertiesItem
      // 
      this.showPropertiesItem.Caption = "Eigenschaften...";
      this.showPropertiesItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.chart_pie_edit;
      this.showPropertiesItem.Id = 0;
      this.showPropertiesItem.Name = "showPropertiesItem";
      this.showPropertiesItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPropertyItemClick);
      // 
      // reloadItem
      // 
      this.reloadItem.Caption = "Aktualisieren";
      this.reloadItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.arrow_refresh;
      this.reloadItem.Id = 3;
      this.reloadItem.Name = "reloadItem";
      this.reloadItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnReloadItemClick);
      // 
      // popupMenu1
      // 
      this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.showPropertiesItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.reloadItem)});
      this.popupMenu1.Manager = this.barManager1;
      this.popupMenu1.Name = "popupMenu1";
      // 
      // MonitorControlStatistics
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.AutoScrollMinSize = new System.Drawing.Size(200, 200);
      this.Controls.Add(this.chartControl1);
      this.Controls.Add(this.barDockControlLeft);
      this.Controls.Add(this.barDockControlRight);
      this.Controls.Add(this.barDockControlBottom);
      this.Controls.Add(this.barDockControlTop);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "MonitorControlStatistics";
      this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
      this.Size = new System.Drawing.Size(400, 369);
      ((System.ComponentModel.ISupportInitialize)(this.chartDataPairBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraCharts.ChartControl chartControl1;
    private System.Windows.Forms.BindingSource chartDataPairBindingSource;
    private DevExpress.XtraBars.BarManager barManager1;
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraBars.BarButtonItem showPropertiesItem;
    private DevExpress.XtraBars.BarButtonItem reloadItem;


  }
}
