namespace _4screen.CSB.Monitor
{
   partial class MonitorControlFeaturedContentEdit
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
           this.userIdLabel = new DevExpress.XtraEditors.LabelControl();
           this.userIdTextBox = new DevExpress.XtraEditors.TextEdit();
           this.featuredLabel = new DevExpress.XtraEditors.LabelControl();
           this.communityIdTextBox = new DevExpress.XtraEditors.TextEdit();
           this.communityIdLabel = new DevExpress.XtraEditors.LabelControl();
           this.objectIdLabel = new DevExpress.XtraEditors.LabelControl();
           this.objectIdTextBox = new DevExpress.XtraEditors.TextEdit();
           this.findButton = new DevExpress.XtraEditors.SimpleButton();
           this.featuredCheckBox = new DevExpress.XtraEditors.CheckEdit();
           this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
           this.gridControl1 = new DevExpress.XtraGrid.GridControl();
           this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
           this.colObjectId = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colCommunityId = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colUserId = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colNickname = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colObjectType = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colFeatured = new DevExpress.XtraGrid.Columns.GridColumn();
           this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
           this.panelControl1.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.userIdTextBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.communityIdTextBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.objectIdTextBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.featuredCheckBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
           this.panelControl2.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
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
           this.panelControl1.Controls.Add(this.userIdLabel);
           this.panelControl1.Controls.Add(this.userIdTextBox);
           this.panelControl1.Controls.Add(this.featuredLabel);
           this.panelControl1.Controls.Add(this.communityIdTextBox);
           this.panelControl1.Controls.Add(this.communityIdLabel);
           this.panelControl1.Controls.Add(this.objectIdLabel);
           this.panelControl1.Controls.Add(this.objectIdTextBox);
           this.panelControl1.Controls.Add(this.findButton);
           this.panelControl1.Controls.Add(this.featuredCheckBox);
           this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
           this.panelControl1.Location = new System.Drawing.Point(0, 0);
           this.panelControl1.Name = "panelControl1";
           this.panelControl1.Size = new System.Drawing.Size(900, 40);
           this.panelControl1.TabIndex = 4;
           this.panelControl1.Text = "panelControl1";
           // 
           // userIdLabel
           // 
           this.userIdLabel.Location = new System.Drawing.Point(373, 13);
           this.userIdLabel.Name = "userIdLabel";
           this.userIdLabel.Size = new System.Drawing.Size(72, 16);
           this.userIdLabel.TabIndex = 8;
           this.userIdLabel.Text = "Benutzer-ID:";
           // 
           // userIdTextBox
           // 
           this.userIdTextBox.Location = new System.Drawing.Point(451, 10);
           this.userIdTextBox.Name = "userIdTextBox";
           this.userIdTextBox.Size = new System.Drawing.Size(100, 22);
           this.userIdTextBox.TabIndex = 7;
           this.userIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchFieldKeyPress);
           // 
           // featuredLabel
           // 
           this.featuredLabel.Location = new System.Drawing.Point(569, 13);
           this.featuredLabel.Name = "featuredLabel";
           this.featuredLabel.Size = new System.Drawing.Size(56, 16);
           this.featuredLabel.TabIndex = 6;
           this.featuredLabel.Text = "Featured:";
           // 
           // communityIdTextBox
           // 
           this.communityIdTextBox.Location = new System.Drawing.Point(255, 10);
           this.communityIdTextBox.Name = "communityIdTextBox";
           this.communityIdTextBox.Size = new System.Drawing.Size(100, 22);
           this.communityIdTextBox.TabIndex = 5;
           this.communityIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchFieldKeyPress);
           // 
           // communityIdLabel
           // 
           this.communityIdLabel.Location = new System.Drawing.Point(186, 13);
           this.communityIdLabel.Name = "communityIdLabel";
           this.communityIdLabel.Size = new System.Drawing.Size(63, 16);
           this.communityIdLabel.TabIndex = 4;
           this.communityIdLabel.Text = "Comm.-ID:";
           // 
           // objectIdLabel
           // 
           this.objectIdLabel.Location = new System.Drawing.Point(5, 12);
           this.objectIdLabel.Name = "objectIdLabel";
           this.objectIdLabel.Size = new System.Drawing.Size(59, 16);
           this.objectIdLabel.TabIndex = 3;
           this.objectIdLabel.Text = "Object-ID:";
           // 
           // objectIdTextBox
           // 
           this.objectIdTextBox.Location = new System.Drawing.Point(70, 9);
           this.objectIdTextBox.Name = "objectIdTextBox";
           this.objectIdTextBox.Size = new System.Drawing.Size(100, 22);
           this.objectIdTextBox.TabIndex = 2;
           this.objectIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchFieldKeyPress);
           // 
           // findButton
           // 
           this.findButton.Location = new System.Drawing.Point(667, 10);
           this.findButton.Name = "findButton";
           this.findButton.Size = new System.Drawing.Size(75, 24);
           this.findButton.TabIndex = 1;
           this.findButton.Text = "Finden";
           this.findButton.Click += new System.EventHandler(this.OnFindButtonClick);
           // 
           // featuredCheckBox
           // 
           this.featuredCheckBox.EditValue = true;
           this.featuredCheckBox.Location = new System.Drawing.Point(631, 11);
           this.featuredCheckBox.Name = "featuredCheckBox";
           this.featuredCheckBox.Properties.Caption = "";
           this.featuredCheckBox.Size = new System.Drawing.Size(75, 21);
           this.featuredCheckBox.TabIndex = 0;
           // 
           // panelControl2
           // 
           this.panelControl2.Controls.Add(this.gridControl1);
           this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
           this.panelControl2.Location = new System.Drawing.Point(0, 40);
           this.panelControl2.Name = "panelControl2";
           this.panelControl2.Size = new System.Drawing.Size(900, 260);
           this.panelControl2.TabIndex = 5;
           this.panelControl2.Text = "panelControl2";
           // 
           // gridControl1
           // 
           this.gridControl1.DataSource = this.bindingSource1;
           this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl1.EmbeddedNavigator.Name = "";
           this.gridControl1.Location = new System.Drawing.Point(2, 2);
           this.gridControl1.MainView = this.gridView1;
           this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
           this.gridControl1.Name = "gridControl1";
           this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1});
           this.gridControl1.Size = new System.Drawing.Size(896, 256);
           this.gridControl1.TabIndex = 1;
           this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
           // 
           // gridView1
           // 
           this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colObjectId,
            this.colCommunityId,
            this.colUserId,
            this.colNickname,
            this.colTitle,
            this.colObjectType,
            this.colFeatured});
           this.gridView1.GridControl = this.gridControl1;
           this.gridView1.Name = "gridView1";
           this.gridView1.OptionsCustomization.AllowColumnMoving = false;
           this.gridView1.OptionsCustomization.AllowGroup = false;
           this.gridView1.OptionsView.ShowGroupPanel = false;
           this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnCellValueChanging);
           // 
           // colObjectId
           // 
           this.colObjectId.Caption = "Object-ID";
           this.colObjectId.FieldName = "ObjectId";
           this.colObjectId.Name = "colObjectId";
           this.colObjectId.OptionsColumn.AllowEdit = false;
           this.colObjectId.Visible = true;
           this.colObjectId.VisibleIndex = 0;
           this.colObjectId.Width = 150;
           // 
           // colCommunityId
           // 
           this.colCommunityId.Caption = "Community-ID";
           this.colCommunityId.FieldName = "CommunityId";
           this.colCommunityId.Name = "colCommunityId";
           this.colCommunityId.OptionsColumn.AllowEdit = false;
           this.colCommunityId.Visible = true;
           this.colCommunityId.VisibleIndex = 1;
           this.colCommunityId.Width = 150;
           // 
           // colUserId
           // 
           this.colUserId.Caption = "Benutzer-ID";
           this.colUserId.FieldName = "UserId";
           this.colUserId.Name = "colUserId";
           this.colUserId.OptionsColumn.AllowEdit = false;
           this.colUserId.Visible = true;
           this.colUserId.VisibleIndex = 2;
           this.colUserId.Width = 150;
           // 
           // colNickname
           // 
           this.colNickname.Caption = "Username";
           this.colNickname.FieldName = "Nickname";
           this.colNickname.Name = "colNickname";
           this.colNickname.OptionsColumn.AllowEdit = false;
           this.colNickname.Visible = true;
           this.colNickname.VisibleIndex = 3;
           this.colNickname.Width = 110;
           // 
           // colTitle
           // 
           this.colTitle.Caption = "Title";
           this.colTitle.FieldName = "Title";
           this.colTitle.Name = "colTitle";
           this.colTitle.OptionsColumn.AllowEdit = false;
           this.colTitle.Visible = true;
           this.colTitle.VisibleIndex = 4;
           this.colTitle.Width = 160;
           // 
           // colObjectType
           // 
           this.colObjectType.Caption = "Typ";
           this.colObjectType.FieldName = "ObjectType";
           this.colObjectType.Name = "colObjectType";
           this.colObjectType.OptionsColumn.AllowEdit = false;
           this.colObjectType.Visible = true;
           this.colObjectType.VisibleIndex = 5;
           this.colObjectType.Width = 80;
           // 
           // colFeatured
           // 
           this.colFeatured.Caption = "Featured";
           this.colFeatured.FieldName = "Featured";
           this.colFeatured.Name = "colFeatured";
           this.colFeatured.Visible = true;
           this.colFeatured.VisibleIndex = 6;
           this.colFeatured.Width = 80;
           // 
           // repositoryItemMemoExEdit1
           // 
           this.repositoryItemMemoExEdit1.AutoHeight = false;
           this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
           this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
           // 
           // MonitorControlFeaturedContentEdit
           // 
           this.Appearance.BackColor = System.Drawing.SystemColors.Control;
           this.Appearance.Options.UseBackColor = true;
           this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.panelControl2);
           this.Controls.Add(this.panelControl1);
           this.Controls.Add(this.barDockControlLeft);
           this.Controls.Add(this.barDockControlRight);
           this.Controls.Add(this.barDockControlBottom);
           this.Controls.Add(this.barDockControlTop);
           this.Margin = new System.Windows.Forms.Padding(4);
           this.Name = "MonitorControlFeaturedContentEdit";
           this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
           this.Size = new System.Drawing.Size(900, 300);
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
           this.panelControl1.ResumeLayout(false);
           this.panelControl1.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.userIdTextBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.communityIdTextBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.objectIdTextBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.featuredCheckBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
           this.panelControl2.ResumeLayout(false);
           ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
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
      private DevExpress.XtraEditors.PanelControl panelControl2;
      private DevExpress.XtraGrid.GridControl gridControl1;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraGrid.Columns.GridColumn colFeatured;
      private DevExpress.XtraGrid.Columns.GridColumn colObjectId;
      private DevExpress.XtraGrid.Columns.GridColumn colCommunityId;
      private DevExpress.XtraGrid.Columns.GridColumn colUserId;
      private DevExpress.XtraGrid.Columns.GridColumn colNickname;
      private DevExpress.XtraGrid.Columns.GridColumn colTitle;
      private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
      private DevExpress.XtraEditors.LabelControl communityIdLabel;
      private DevExpress.XtraEditors.LabelControl objectIdLabel;
      private DevExpress.XtraEditors.TextEdit objectIdTextBox;
      private DevExpress.XtraEditors.SimpleButton findButton;
      private DevExpress.XtraEditors.CheckEdit featuredCheckBox;
      private DevExpress.XtraEditors.LabelControl featuredLabel;
      private DevExpress.XtraEditors.TextEdit communityIdTextBox;
      private DevExpress.XtraGrid.Columns.GridColumn colObjectType;
      private DevExpress.XtraEditors.LabelControl userIdLabel;
      private DevExpress.XtraEditors.TextEdit userIdTextBox;



    }
}
