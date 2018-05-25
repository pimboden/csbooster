namespace _4screen.CSB.Monitor
{
    partial class MonitorControlUsers
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
           DevExpress.XtraGrid.Columns.GridColumn colEmail;
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
           this.lockedLabel = new DevExpress.XtraEditors.LabelControl();
           this.emailTextBox = new DevExpress.XtraEditors.TextEdit();
           this.emailLabel = new DevExpress.XtraEditors.LabelControl();
           this.usernameLabel = new DevExpress.XtraEditors.LabelControl();
           this.usernameTextBox = new DevExpress.XtraEditors.TextEdit();
           this.findButton = new DevExpress.XtraEditors.SimpleButton();
           this.lockedCheckBox = new DevExpress.XtraEditors.CheckEdit();
           this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
           this.gridControl1 = new DevExpress.XtraGrid.GridControl();
           this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
           this.colUsername = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colFullname = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colLastLoginDate = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colLastLockoutDate = new DevExpress.XtraGrid.Columns.GridColumn();
           this.colIsLocked = new DevExpress.XtraGrid.Columns.GridColumn();
           this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
           colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
           this.panelControl1.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.emailTextBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.lockedCheckBox.Properties)).BeginInit();
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
           this.panelControl1.Controls.Add(this.lockedLabel);
           this.panelControl1.Controls.Add(this.emailTextBox);
           this.panelControl1.Controls.Add(this.emailLabel);
           this.panelControl1.Controls.Add(this.usernameLabel);
           this.panelControl1.Controls.Add(this.usernameTextBox);
           this.panelControl1.Controls.Add(this.findButton);
           this.panelControl1.Controls.Add(this.lockedCheckBox);
           this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
           this.panelControl1.Location = new System.Drawing.Point(0, 0);
           this.panelControl1.Name = "panelControl1";
           this.panelControl1.Size = new System.Drawing.Size(900, 40);
           this.panelControl1.TabIndex = 4;
           this.panelControl1.Text = "panelControl1";
           // 
           // lockedLabel
           // 
           this.lockedLabel.Location = new System.Drawing.Point(463, 12);
           this.lockedLabel.Name = "lockedLabel";
           this.lockedLabel.Size = new System.Drawing.Size(91, 16);
           this.lockedLabel.TabIndex = 6;
           this.lockedLabel.Text = "Gesperrte User:";
           // 
           // emailTextBox
           // 
           this.emailTextBox.Location = new System.Drawing.Point(304, 10);
           this.emailTextBox.Name = "emailTextBox";
           this.emailTextBox.Size = new System.Drawing.Size(140, 22);
           this.emailTextBox.TabIndex = 5;
           this.emailTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchFieldKeyPress);
           // 
           // emailLabel
           // 
           this.emailLabel.Location = new System.Drawing.Point(260, 12);
           this.emailLabel.Name = "emailLabel";
           this.emailLabel.Size = new System.Drawing.Size(36, 16);
           this.emailLabel.TabIndex = 4;
           this.emailLabel.Text = "Email:";
           // 
           // usernameLabel
           // 
           this.usernameLabel.Location = new System.Drawing.Point(5, 12);
           this.usernameLabel.Name = "usernameLabel";
           this.usernameLabel.Size = new System.Drawing.Size(87, 16);
           this.usernameLabel.TabIndex = 3;
           this.usernameLabel.Text = "Username:";
           // 
           // usernameTextBox
           // 
           this.usernameTextBox.Location = new System.Drawing.Point(100, 10);
           this.usernameTextBox.Name = "usernameTextBox";
           this.usernameTextBox.Size = new System.Drawing.Size(140, 22);
           this.usernameTextBox.TabIndex = 2;
           this.usernameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchFieldKeyPress);
           // 
           // findButton
           // 
           this.findButton.Location = new System.Drawing.Point(597, 9);
           this.findButton.Name = "findButton";
           this.findButton.Size = new System.Drawing.Size(75, 24);
           this.findButton.TabIndex = 1;
           this.findButton.Text = "Finden";
           this.findButton.Click += new System.EventHandler(this.OnFindButtonClick);
           // 
           // lockedCheckBox
           // 
           this.lockedCheckBox.Location = new System.Drawing.Point(560, 11);
           this.lockedCheckBox.Name = "lockedCheckBox";
           this.lockedCheckBox.Properties.Caption = "";
           this.lockedCheckBox.Size = new System.Drawing.Size(75, 21);
           this.lockedCheckBox.TabIndex = 0;
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
            this.colUsername,
            colEmail,
            this.colFullname,
            this.colLastLoginDate,
            this.colLastLockoutDate,
            this.colIsLocked});
           this.gridView1.GridControl = this.gridControl1;
           this.gridView1.Name = "gridView1";
           this.gridView1.OptionsCustomization.AllowColumnMoving = false;
           this.gridView1.OptionsCustomization.AllowGroup = false;
           this.gridView1.OptionsView.ShowGroupPanel = false;
           this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnCellValueChanged);
           this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnCellValueChanging);
           // 
           // colUsername
           // 
           this.colUsername.Caption = "Username";
           this.colUsername.FieldName = "Username";
           this.colUsername.Name = "colUsername";
           this.colUsername.OptionsColumn.AllowEdit = false;
           this.colUsername.Visible = true;
           this.colUsername.VisibleIndex = 0;
           this.colUsername.Width = 150;
           // 
           // colEmail
           // 
           colEmail.Caption = "Email";
           colEmail.FieldName = "Email";
           colEmail.Name = "colEmail";
           colEmail.Visible = true;
           colEmail.VisibleIndex = 1;
           colEmail.Width = 195;
           // 
           // colFullname
           // 
           this.colFullname.Caption = "Name";
           this.colFullname.FieldName = "Fullname";
           this.colFullname.Name = "colFullname";
           this.colFullname.OptionsColumn.AllowEdit = false;
           this.colFullname.Visible = true;
           this.colFullname.VisibleIndex = 2;
           this.colFullname.Width = 150;
           // 
           // colLastLoginDate
           // 
           this.colLastLoginDate.Caption = "Letzte Anmeldung";
           this.colLastLoginDate.FieldName = "LastLoginDate";
           this.colLastLoginDate.Name = "colLastLoginDate";
           this.colLastLoginDate.OptionsColumn.AllowEdit = false;
           this.colLastLoginDate.Visible = true;
           this.colLastLoginDate.VisibleIndex = 3;
           this.colLastLoginDate.Width = 120;
           // 
           // colLastLockoutDate
           // 
           this.colLastLockoutDate.Caption = "Letzte Sperrung";
           this.colLastLockoutDate.FieldName = "LastLockoutDate";
           this.colLastLockoutDate.Name = "colLastLockoutDate";
           this.colLastLockoutDate.OptionsColumn.AllowEdit = false;
           this.colLastLockoutDate.Visible = true;
           this.colLastLockoutDate.VisibleIndex = 4;
           this.colLastLockoutDate.Width = 120;
           // 
           // colIsLocked
           // 
           this.colIsLocked.Caption = "Sperrung aktiv";
           this.colIsLocked.FieldName = "IsLocked";
           this.colIsLocked.Name = "colIsLocked";
           this.colIsLocked.Visible = true;
           this.colIsLocked.VisibleIndex = 5;
           this.colIsLocked.Width = 120;
           // 
           // repositoryItemMemoExEdit1
           // 
           this.repositoryItemMemoExEdit1.AutoHeight = false;
           this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
           this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
           // 
           // MonitorControlUsers
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
           this.Name = "MonitorControlUsers";
           this.barManager1.SetPopupContextMenu(this, this.popupMenu1);
           this.Size = new System.Drawing.Size(900, 300);
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
           this.panelControl1.ResumeLayout(false);
           this.panelControl1.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.emailTextBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.usernameTextBox.Properties)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.lockedCheckBox.Properties)).EndInit();
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
       private DevExpress.XtraGrid.Columns.GridColumn colUsername;
      private DevExpress.XtraGrid.Columns.GridColumn colFullname;
      private DevExpress.XtraGrid.Columns.GridColumn colLastLoginDate;
      private DevExpress.XtraGrid.Columns.GridColumn colLastLockoutDate;
      private DevExpress.XtraGrid.Columns.GridColumn colIsLocked;
      private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
      private DevExpress.XtraEditors.LabelControl emailLabel;
      private DevExpress.XtraEditors.LabelControl usernameLabel;
      private DevExpress.XtraEditors.TextEdit usernameTextBox;
      private DevExpress.XtraEditors.SimpleButton findButton;
      private DevExpress.XtraEditors.CheckEdit lockedCheckBox;
      private DevExpress.XtraEditors.LabelControl lockedLabel;
      private DevExpress.XtraEditors.TextEdit emailTextBox;



    }
}
