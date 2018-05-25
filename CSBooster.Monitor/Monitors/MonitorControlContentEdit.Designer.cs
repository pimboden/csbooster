namespace _4screen.CSB.Monitor
{
  partial class MonitorControlContentEdit
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
       this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
       this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
       this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
       this.showPropertiesItem = new DevExpress.XtraBars.BarButtonItem();
       this.reloadItem = new DevExpress.XtraBars.BarButtonItem();
       this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
       this.gridControl1 = new DevExpress.XtraGrid.GridControl();
       this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
       this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
       this.keyName = new DevExpress.XtraGrid.Columns.GridColumn();
       this.content = new DevExpress.XtraGrid.Columns.GridColumn();
       this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
       ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
       ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
       this.SuspendLayout();
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
       // gridControl1
       // 
       this.gridControl1.DataSource = this.bindingSource1;
       this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
       this.gridControl1.EmbeddedNavigator.Name = "";
       this.gridControl1.Location = new System.Drawing.Point(0, 0);
       this.gridControl1.MainView = this.gridView1;
       this.gridControl1.Name = "gridControl1";
       this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
       this.gridControl1.Size = new System.Drawing.Size(900, 300);
       this.gridControl1.TabIndex = 4;
       this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
       // 
       // gridView1
       // 
       this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.keyName,
            this.content});
       this.gridView1.GridControl = this.gridControl1;
       this.gridView1.Name = "gridView1";
       this.gridView1.OptionsCustomization.AllowColumnMoving = false;
       this.gridView1.OptionsCustomization.AllowGroup = false;
       this.gridView1.OptionsView.RowAutoHeight = true;
       this.gridView1.OptionsView.ShowGroupPanel = false;
       this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnCellValueChanged);
       // 
       // keyName
       // 
       this.keyName.Caption = "Schlüsselname";
       this.keyName.FieldName = "Key";
       this.keyName.MinWidth = 200;
       this.keyName.Name = "keyName";
       this.keyName.OptionsColumn.AllowEdit = false;
       this.keyName.Visible = true;
       this.keyName.VisibleIndex = 0;
       this.keyName.Width = 200;
       // 
       // content
       // 
       this.content.Caption = "Inhalt";
       this.content.ColumnEdit = this.repositoryItemMemoEdit1;
       this.content.FieldName = "Content";
       this.content.MinWidth = 200;
       this.content.Name = "content";
       this.content.Visible = true;
       this.content.VisibleIndex = 1;
       this.content.Width = 679;
       // 
       // repositoryItemMemoEdit1
       // 
       this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
       // 
       // MonitorControlTableEdit
       // 
       this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
       this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
       this.AutoScroll = true;
       this.AutoScrollMinSize = new System.Drawing.Size(200, 200);
       this.Controls.Add(this.gridControl1);
       this.Controls.Add(this.barDockControlLeft);
       this.Controls.Add(this.barDockControlRight);
       this.Controls.Add(this.barDockControlBottom);
       this.Controls.Add(this.barDockControlTop);
       this.Margin = new System.Windows.Forms.Padding(4);
       this.Name = "MonitorControlTableEdit";
       this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
       this.Size = new System.Drawing.Size(900, 300);
       ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
       ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
       this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraBars.BarManager barManager1;
    private DevExpress.XtraBars.BarDockControl barDockControlTop;
    private DevExpress.XtraBars.BarDockControl barDockControlBottom;
    private DevExpress.XtraBars.BarDockControl barDockControlLeft;
    private DevExpress.XtraBars.BarDockControl barDockControlRight;
    private DevExpress.XtraBars.PopupMenu popupMenu1;
    private DevExpress.XtraBars.BarButtonItem showPropertiesItem;
    private DevExpress.XtraBars.BarButtonItem reloadItem;
     private DevExpress.XtraGrid.GridControl gridControl1;
     private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
     private System.Windows.Forms.BindingSource bindingSource1;
     private DevExpress.XtraGrid.Columns.GridColumn keyName;
     private DevExpress.XtraGrid.Columns.GridColumn content;
     private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;


  }
}
