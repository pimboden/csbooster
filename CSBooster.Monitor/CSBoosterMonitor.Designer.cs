namespace _4screen.CSB.Monitor
{
  partial class CSBoosterMonitor
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
       this.components = new System.ComponentModel.Container();
       System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSBoosterMonitor));
       this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
       this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
       this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
       this.mainMenuBar = new DevExpress.XtraBars.Bar();
       this.fileMenu = new DevExpress.XtraBars.BarSubItem();
       this.exitItem = new DevExpress.XtraBars.BarButtonItem();
       this.viewMenu = new DevExpress.XtraBars.BarSubItem();
       this.addMonitorSubMenu = new DevExpress.XtraBars.BarSubItem();
       this.newStatisticsMonitorItem = new DevExpress.XtraBars.BarButtonItem();
       this.newUserMonitorItem = new DevExpress.XtraBars.BarButtonItem();
       this.newContentEditorItem = new DevExpress.XtraBars.BarButtonItem();
       this.newFeaturedContentEditorItem = new DevExpress.XtraBars.BarButtonItem();
       this.newAdCampaignEditorItem = new DevExpress.XtraBars.BarButtonItem();
       this.settingsMenu = new DevExpress.XtraBars.BarSubItem();
       this.monitorSettingsItem = new DevExpress.XtraBars.BarButtonItem();
       this.loadSettingsItem = new DevExpress.XtraBars.BarButtonItem();
       this.saveSettingsItem = new DevExpress.XtraBars.BarButtonItem();
       this.helpMenu = new DevExpress.XtraBars.BarSubItem();
       this.aboutItem = new DevExpress.XtraBars.BarButtonItem();
       this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
       this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
       ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
       this.SuspendLayout();
       // 
       // dockManager1
       // 
       this.dockManager1.Form = this;
       this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl"});
       this.dockManager1.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.OnClosedPanel);
       // 
       // defaultLookAndFeel1
       // 
       this.defaultLookAndFeel1.LookAndFeel.SkinName = "The Asphalt World";
       // 
       // barManager1
       // 
       this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.mainMenuBar});
       this.barManager1.DockControls.Add(this.barDockControlTop);
       this.barManager1.DockControls.Add(this.barDockControlBottom);
       this.barManager1.DockControls.Add(this.barDockControlLeft);
       this.barManager1.DockControls.Add(this.barDockControlRight);
       this.barManager1.DockManager = this.dockManager1;
       this.barManager1.Form = this;
       this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.monitorSettingsItem,
            this.addMonitorSubMenu,
            this.newStatisticsMonitorItem,
            this.newUserMonitorItem,
            this.saveSettingsItem,
            this.loadSettingsItem,
            this.fileMenu,
            this.viewMenu,
            this.settingsMenu,
            this.exitItem,
            this.helpMenu,
            this.aboutItem,
            this.newContentEditorItem,
            this.newFeaturedContentEditorItem,
            this.newAdCampaignEditorItem});
       this.barManager1.MainMenu = this.mainMenuBar;
       this.barManager1.MaxItemId = 20;
       // 
       // mainMenuBar
       // 
       this.mainMenuBar.BarName = "Main Menu";
       this.mainMenuBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
       this.mainMenuBar.DockCol = 0;
       this.mainMenuBar.DockRow = 0;
       this.mainMenuBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
       this.mainMenuBar.FloatLocation = new System.Drawing.Point(269, 129);
       this.mainMenuBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.fileMenu),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewMenu),
            new DevExpress.XtraBars.LinkPersistInfo(this.settingsMenu),
            new DevExpress.XtraBars.LinkPersistInfo(this.helpMenu)});
       this.mainMenuBar.OptionsBar.AllowQuickCustomization = false;
       this.mainMenuBar.OptionsBar.DisableCustomization = true;
       this.mainMenuBar.OptionsBar.MultiLine = true;
       this.mainMenuBar.OptionsBar.UseWholeRow = true;
       this.mainMenuBar.Text = "Hauptmenü";
       // 
       // fileMenu
       // 
       this.fileMenu.Caption = "Datei";
       this.fileMenu.Id = 11;
       this.fileMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.exitItem)});
       this.fileMenu.Name = "fileMenu";
       // 
       // exitItem
       // 
       this.exitItem.Caption = "Beenden";
       this.exitItem.Id = 14;
       this.exitItem.Name = "exitItem";
       this.exitItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnExitItemClick);
       // 
       // viewMenu
       // 
       this.viewMenu.Caption = "Ansicht";
       this.viewMenu.Id = 12;
       this.viewMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.addMonitorSubMenu)});
       this.viewMenu.Name = "viewMenu";
       // 
       // addMonitorSubMenu
       // 
       this.addMonitorSubMenu.Caption = "Monitor hinzufügen";
       this.addMonitorSubMenu.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.monitor;
       this.addMonitorSubMenu.Id = 6;
       this.addMonitorSubMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.newStatisticsMonitorItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.newUserMonitorItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.newContentEditorItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.newFeaturedContentEditorItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.newAdCampaignEditorItem)});
       this.addMonitorSubMenu.Name = "addMonitorSubMenu";
       // 
       // newStatisticsMonitorItem
       // 
       this.newStatisticsMonitorItem.Caption = "Statistik Monitor";
       this.newStatisticsMonitorItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.chart_pie;
       this.newStatisticsMonitorItem.Id = 7;
       this.newStatisticsMonitorItem.Name = "newStatisticsMonitorItem";
       this.newStatisticsMonitorItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewStatisticsMonitorItemClick);
       // 
       // newUserMonitorItem
       // 
       this.newUserMonitorItem.Caption = "Benutzer Monitor";
       this.newUserMonitorItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.user;
       this.newUserMonitorItem.Id = 8;
       this.newUserMonitorItem.Name = "newUserMonitorItem";
       this.newUserMonitorItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewUserMonitorItemClick);
       // 
       // newContentEditorItem
       // 
       this.newContentEditorItem.Caption = "Inhalt Editor";
       this.newContentEditorItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.page_edit;
       this.newContentEditorItem.Id = 17;
       this.newContentEditorItem.Name = "newContentEditorItem";
       this.newContentEditorItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewContentEditorItemClick);
       // 
       // newFeaturedContentEditorItem
       // 
       this.newFeaturedContentEditorItem.Caption = "Featured Content";
       this.newFeaturedContentEditorItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.page_lightning;
       this.newFeaturedContentEditorItem.Id = 18;
       this.newFeaturedContentEditorItem.Name = "newFeaturedContentEditorItem";
       this.newFeaturedContentEditorItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewFeaturedContentEditorItemClick);
       // 
       // newAdCampaignEditorItem
       // 
       this.newAdCampaignEditorItem.Caption = "Werbekampagnen";
       this.newAdCampaignEditorItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.report_edit;
       this.newAdCampaignEditorItem.Id = 19;
       this.newAdCampaignEditorItem.Name = "newAdCampaignEditorItem";
       this.newAdCampaignEditorItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnNewAdCampaignEditorItemClick);
       // 
       // settingsMenu
       // 
       this.settingsMenu.Caption = "Einstellungen";
       this.settingsMenu.Id = 13;
       this.settingsMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.monitorSettingsItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.loadSettingsItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.saveSettingsItem)});
       this.settingsMenu.Name = "settingsMenu";
       // 
       // monitorSettingsItem
       // 
       this.monitorSettingsItem.Caption = "Monitor Einstellungen...";
       this.monitorSettingsItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.table_gear;
       this.monitorSettingsItem.Id = 4;
       this.monitorSettingsItem.Name = "monitorSettingsItem";
       this.monitorSettingsItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnMonitorSettingsItemClick);
       // 
       // loadSettingsItem
       // 
       this.loadSettingsItem.Caption = "Monitor Einstellungen laden";
       this.loadSettingsItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.folder_table;
       this.loadSettingsItem.Id = 10;
       this.loadSettingsItem.Name = "loadSettingsItem";
       this.loadSettingsItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnLoadSettingsItemClick);
       // 
       // saveSettingsItem
       // 
       this.saveSettingsItem.Caption = "Monitor Einstellungen speichern";
       this.saveSettingsItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.table_save;
       this.saveSettingsItem.Id = 9;
       this.saveSettingsItem.Name = "saveSettingsItem";
       this.saveSettingsItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnSaveSettingsItemClick);
       // 
       // helpMenu
       // 
       this.helpMenu.Caption = "Hilfe";
       this.helpMenu.Id = 15;
       this.helpMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.aboutItem)});
       this.helpMenu.Name = "helpMenu";
       // 
       // aboutItem
       // 
       this.aboutItem.Caption = "Über CSBooster Monitor...";
       this.aboutItem.Id = 16;
       this.aboutItem.Name = "aboutItem";
       this.aboutItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnAboutItemClick);
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
       // xtraTabbedMdiManager1
       // 
       this.xtraTabbedMdiManager1.MdiParent = this;
       // 
       // CSBoosterMonitor
       // 
       this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
       this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
       this.ClientSize = new System.Drawing.Size(1327, 795);
       this.Controls.Add(this.barDockControlLeft);
       this.Controls.Add(this.barDockControlRight);
       this.Controls.Add(this.barDockControlBottom);
       this.Controls.Add(this.barDockControlTop);
       this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
       this.IsMdiContainer = true;
       this.Margin = new System.Windows.Forms.Padding(4);
       this.Name = "CSBoosterMonitor";
       this.Text = "CSBooster Monitor";
       this.Shown += new System.EventHandler(this.OnShown);
       ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
       this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraBars.Docking.DockManager dockManager1;
    private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarManager barManager1;
    private DevExpress.XtraBars.Bar mainMenuBar;
    private DevExpress.XtraBars.BarButtonItem monitorSettingsItem;
    private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
    private DevExpress.XtraBars.BarSubItem addMonitorSubMenu;
    private DevExpress.XtraBars.BarButtonItem newStatisticsMonitorItem;
    private DevExpress.XtraBars.BarButtonItem newUserMonitorItem;
    private DevExpress.XtraBars.BarButtonItem saveSettingsItem;
    private DevExpress.XtraBars.BarButtonItem loadSettingsItem;
    private DevExpress.XtraBars.BarSubItem fileMenu;
    private DevExpress.XtraBars.BarSubItem viewMenu;
    private DevExpress.XtraBars.BarSubItem settingsMenu;
    private DevExpress.XtraBars.BarButtonItem exitItem;
    private DevExpress.XtraBars.BarSubItem helpMenu;
    private DevExpress.XtraBars.BarButtonItem aboutItem;
     private DevExpress.XtraBars.BarButtonItem newContentEditorItem;
     private DevExpress.XtraBars.BarButtonItem newFeaturedContentEditorItem;
     private DevExpress.XtraBars.BarButtonItem newAdCampaignEditorItem;

  }
}

