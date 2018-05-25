namespace _4screen.CSB.Monitor
{
   partial class MonitorControlAdCampaigns
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
           this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
           this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
           this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
           this.showPropertiesItem = new DevExpress.XtraBars.BarButtonItem();
           this.reloadItem = new DevExpress.XtraBars.BarButtonItem();
           this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
           this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
           this.newWordButton = new DevExpress.XtraEditors.SimpleButton();
           this.newCampaignButton = new DevExpress.XtraEditors.SimpleButton();
           this.splitContainer1 = new System.Windows.Forms.SplitContainer();
           this.gridControl1 = new DevExpress.XtraGrid.GridControl();
           this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
           this.colAdCampaignDescription = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdBannerPage = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdUrl = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdContent = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdCredits = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdCreditsUsed = new DevExpress.XtraGrid.Columns.GridColumn();
           this.gridControl2 = new DevExpress.XtraGrid.GridControl();
           this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
           this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
           this.colAdWord = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colIsExact = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAction = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colAdCampaign = new DevExpress.XtraGrid.Columns.GridColumn();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
           this.panelControl1.SuspendLayout();
           this.splitContainer1.Panel1.SuspendLayout();
           this.splitContainer1.Panel2.SuspendLayout();
           this.splitContainer1.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
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
           this.barManager1.MaxItemId = 2;
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
           this.showPropertiesItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.user_edit;
           this.showPropertiesItem.Id = 0;
           this.showPropertiesItem.Name = "showPropertiesItem";
           this.showPropertiesItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPropertyItemClick);
           // 
           // reloadItem
           // 
           this.reloadItem.Caption = "Aktualisieren";
           this.reloadItem.Glyph = global::_4screen.CSB.Monitor.Properties.Resources.arrow_refresh;
           this.reloadItem.Id = 1;
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
           // panelControl1
           // 
           this.panelControl1.Controls.Add(this.newWordButton);
           this.panelControl1.Controls.Add(this.newCampaignButton);
           this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
           this.panelControl1.Location = new System.Drawing.Point(0, 0);
           this.panelControl1.Name = "panelControl1";
           this.panelControl1.Size = new System.Drawing.Size(900, 40);
           this.panelControl1.TabIndex = 4;
           this.panelControl1.Text = "panelControl1";
           // 
           // newWordButton
           // 
           this.newWordButton.Location = new System.Drawing.Point(134, 9);
           this.newWordButton.Name = "newWordButton";
           this.newWordButton.Size = new System.Drawing.Size(123, 24);
           this.newWordButton.TabIndex = 2;
           this.newWordButton.Text = "Neues Wort...";
           this.newWordButton.Click += new System.EventHandler(this.OnNewWordButtonClick);
           // 
           // newCampaignButton
           // 
           this.newCampaignButton.Location = new System.Drawing.Point(5, 9);
           this.newCampaignButton.Name = "newCampaignButton";
           this.newCampaignButton.Size = new System.Drawing.Size(123, 24);
           this.newCampaignButton.TabIndex = 1;
           this.newCampaignButton.Text = "Neue Kampagne...";
           this.newCampaignButton.Click += new System.EventHandler(this.OnNewCampaignButtonClick);
           // 
           // splitContainer1
           // 
           this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitContainer1.Location = new System.Drawing.Point(0, 40);
           this.splitContainer1.Name = "splitContainer1";
           // 
           // splitContainer1.Panel1
           // 
           this.splitContainer1.Panel1.Controls.Add(this.gridControl1);
           // 
           // splitContainer1.Panel2
           // 
           this.splitContainer1.Panel2.Controls.Add(this.gridControl2);
           this.splitContainer1.Size = new System.Drawing.Size(900, 260);
           this.splitContainer1.SplitterDistance = 550;
           this.splitContainer1.TabIndex = 7;
           // 
           // gridControl1
           // 
           this.gridControl1.DataSource = this.bindingSource1;
           this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl1.EmbeddedNavigator.Name = "";
           this.gridControl1.Location = new System.Drawing.Point(0, 0);
           this.gridControl1.MainView = this.gridView1;
           this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl1.Name = "gridControl1";
           this.gridControl1.Size = new System.Drawing.Size(550, 260);
           this.gridControl1.TabIndex = 5;
           this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
           this.gridControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnAdCampaignGridDoubleClick);
           // 
           // gridView1
           // 
           this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAdCampaignDescription,
            this.colAdBannerPage,
            this.colAdUrl,
            this.colAdContent,
            this.colAdCredits,
            this.colAdCreditsUsed});
           this.gridView1.GridControl = this.gridControl1;
           this.gridView1.Name = "gridView1";
           this.gridView1.OptionsCustomization.AllowColumnMoving = false;
           this.gridView1.OptionsCustomization.AllowGroup = false;
           this.gridView1.OptionsView.ShowGroupPanel = false;
           this.gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
           // 
           // colAdCampaignDescription
           // 
           this.colAdCampaignDescription.Caption = "Kampagne";
           this.colAdCampaignDescription.FieldName = "Description";
           this.colAdCampaignDescription.Name = "colAdCampaignDescription";
           this.colAdCampaignDescription.OptionsColumn.AllowEdit = false;
           this.colAdCampaignDescription.Visible = true;
           this.colAdCampaignDescription.VisibleIndex = 0;
           // 
           // colAdBannerPage
           // 
           this.colAdBannerPage.Caption = "Banner";
           this.colAdBannerPage.FieldName = "Banner";
           this.colAdBannerPage.Name = "colAdBannerPage";
           this.colAdBannerPage.OptionsColumn.AllowEdit = false;
           this.colAdBannerPage.Visible = true;
           this.colAdBannerPage.VisibleIndex = 1;
           // 
           // colAdUrl
           // 
           this.colAdUrl.Caption = "Url";
           this.colAdUrl.FieldName = "Url";
           this.colAdUrl.Name = "colAdUrl";
           this.colAdUrl.OptionsColumn.AllowEdit = false;
           this.colAdUrl.Visible = true;
           this.colAdUrl.VisibleIndex = 2;
           // 
           // colAdContent
           // 
           this.colAdContent.Caption = "Inhalt";
           this.colAdContent.FieldName = "Content";
           this.colAdContent.Name = "colAdContent";
           this.colAdContent.OptionsColumn.AllowEdit = false;
           this.colAdContent.Visible = true;
           this.colAdContent.VisibleIndex = 3;
           // 
           // colAdCredits
           // 
           this.colAdCredits.Caption = "Kredite";
           this.colAdCredits.FieldName = "Credits";
           this.colAdCredits.Name = "colAdCredits";
           this.colAdCredits.OptionsColumn.AllowEdit = false;
           this.colAdCredits.Visible = true;
           this.colAdCredits.VisibleIndex = 4;
           // 
           // colAdCreditsUsed
           // 
           this.colAdCreditsUsed.Caption = "Benutzte Kredite";
           this.colAdCreditsUsed.FieldName = "CreditsUsed";
           this.colAdCreditsUsed.Name = "colAdCreditsUsed";
           this.colAdCreditsUsed.OptionsColumn.AllowEdit = false;
           this.colAdCreditsUsed.Visible = true;
           this.colAdCreditsUsed.VisibleIndex = 5;
           // 
           // gridControl2
           // 
           this.gridControl2.DataSource = this.bindingSource2;
           this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
           this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl2.EmbeddedNavigator.Name = "";
           this.gridControl2.Location = new System.Drawing.Point(0, 0);
           this.gridControl2.MainView = this.gridView2;
           this.gridControl2.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl2.Name = "gridControl2";
           this.gridControl2.Size = new System.Drawing.Size(346, 260);
           this.gridControl2.TabIndex = 3;
           this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
           this.gridControl2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnAdWordGridDoubleClick);
           // 
           // gridView2
           // 
           this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAdWord,
            this.colIsExact,
            this.colAction,
            this.colAdCampaign});
           this.gridView2.GridControl = this.gridControl2;
           this.gridView2.Name = "gridView2";
           this.gridView2.OptionsCustomization.AllowColumnMoving = false;
           this.gridView2.OptionsCustomization.AllowGroup = false;
           this.gridView2.OptionsView.ShowGroupPanel = false;
           // 
           // colAdWord
           // 
           this.colAdWord.Caption = "Wort";
           this.colAdWord.FieldName = "Word";
           this.colAdWord.Name = "colAdWord";
           this.colAdWord.OptionsColumn.AllowEdit = false;
           this.colAdWord.Visible = true;
           this.colAdWord.VisibleIndex = 0;
           // 
           // colIsExact
           // 
           this.colIsExact.Caption = "Exakt";
           this.colIsExact.FieldName = "IsExact";
           this.colIsExact.Name = "colIsExact";
           this.colIsExact.OptionsColumn.AllowEdit = false;
           this.colIsExact.Visible = true;
           this.colIsExact.VisibleIndex = 1;
           // 
           // colAction
           // 
           this.colAction.Caption = "Aktion";
           this.colAction.FieldName = "Action";
           this.colAction.Name = "colAction";
           this.colAction.OptionsColumn.AllowEdit = false;
           this.colAction.Visible = true;
           this.colAction.VisibleIndex = 2;
           // 
           // colAdCampaign
           // 
           this.colAdCampaign.Caption = "Kampagne";
           this.colAdCampaign.FieldName = "CampaignDescription";
           this.colAdCampaign.Name = "colAdCampaign";
           this.colAdCampaign.OptionsColumn.AllowEdit = false;
           this.colAdCampaign.Visible = true;
           this.colAdCampaign.VisibleIndex = 3;
           // 
           // MonitorControlAdCampaigns
           // 
           this.Appearance.BackColor = System.Drawing.SystemColors.Control;
           this.Appearance.Options.UseBackColor = true;
           this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.splitContainer1);
           this.Controls.Add(this.panelControl1);
           this.Controls.Add(this.barDockControlLeft);
           this.Controls.Add(this.barDockControlRight);
           this.Controls.Add(this.barDockControlBottom);
           this.Controls.Add(this.barDockControlTop);
           this.Margin = new System.Windows.Forms.Padding(4);
           this.Name = "MonitorControlAdCampaigns";
           this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
           this.Size = new System.Drawing.Size(900, 300);
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
           this.panelControl1.ResumeLayout(false);
           this.splitContainer1.Panel1.ResumeLayout(false);
           this.splitContainer1.Panel2.ResumeLayout(false);
           this.splitContainer1.ResumeLayout(false);
           ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
           this.ResumeLayout(false);

        }

        #endregion

      private System.Windows.Forms.BindingSource bindingSource1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem showPropertiesItem;
      private DevExpress.XtraBars.PopupMenu popupMenu1;
      private DevExpress.XtraBars.BarButtonItem reloadItem;
      private DevExpress.XtraEditors.PanelControl panelControl1;
      private DevExpress.XtraEditors.SimpleButton newCampaignButton;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private DevExpress.XtraGrid.GridControl gridControl1;
      private DevExpress.XtraGrid.GridControl gridControl2;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
      private DevExpress.XtraGrid.Columns.GridColumn colAdWord;
      private DevExpress.XtraGrid.Columns.GridColumn colIsExact;
      private DevExpress.XtraGrid.Columns.GridColumn colAction;
      private DevExpress.XtraGrid.Columns.GridColumn colAdCampaign;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraGrid.Columns.GridColumn colAdCampaignDescription;
      private DevExpress.XtraGrid.Columns.GridColumn colAdBannerPage;
      private DevExpress.XtraGrid.Columns.GridColumn colAdUrl;
      private DevExpress.XtraGrid.Columns.GridColumn colAdContent;
      private DevExpress.XtraGrid.Columns.GridColumn colAdCredits;
      private DevExpress.XtraGrid.Columns.GridColumn colAdCreditsUsed;
      private System.Windows.Forms.BindingSource bindingSource2;
      private DevExpress.XtraEditors.SimpleButton newWordButton;



    }
}
