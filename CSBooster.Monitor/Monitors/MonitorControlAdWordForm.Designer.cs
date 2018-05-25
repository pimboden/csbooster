namespace _4screen.CSB.Monitor
{
   partial class MonitorControlAdWordForm
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
         this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.isExactCheckBox = new DevExpress.XtraEditors.CheckEdit();
         this.campaignComboBox = new DevExpress.XtraEditors.ComboBoxEdit();
         this.actionComboBox = new DevExpress.XtraEditors.ComboBoxEdit();
         this.wordTextBox = new DevExpress.XtraEditors.TextEdit();
         this.wordIdTextBox = new DevExpress.XtraEditors.TextEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.word = new DevExpress.XtraLayout.LayoutControlItem();
         this.wordId = new DevExpress.XtraLayout.LayoutControlItem();
         this.action = new DevExpress.XtraLayout.LayoutControlItem();
         this.adCampaign = new DevExpress.XtraLayout.LayoutControlItem();
         this.isExact = new DevExpress.XtraLayout.LayoutControlItem();
         this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
         this.saveButton = new DevExpress.XtraEditors.SimpleButton();
         this.cancelButton = new DevExpress.XtraEditors.SimpleButton();
         ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
         this.panelControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.isExactCheckBox.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.campaignComboBox.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.actionComboBox.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordTextBox.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordIdTextBox.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.word)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordId)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.action)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.adCampaign)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.isExact)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
         this.panelControl2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panelControl1
         // 
         this.panelControl1.Controls.Add(this.layoutControl1);
         this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelControl1.Location = new System.Drawing.Point(0, 0);
         this.panelControl1.Name = "panelControl1";
         this.panelControl1.Size = new System.Drawing.Size(456, 170);
         this.panelControl1.TabIndex = 1;
         this.panelControl1.Text = "panelControl1";
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.isExactCheckBox);
         this.layoutControl1.Controls.Add(this.campaignComboBox);
         this.layoutControl1.Controls.Add(this.actionComboBox);
         this.layoutControl1.Controls.Add(this.wordTextBox);
         this.layoutControl1.Controls.Add(this.wordIdTextBox);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(2, 2);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(452, 166);
         this.layoutControl1.TabIndex = 3;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // isExactCheckBox
         // 
         this.isExactCheckBox.Location = new System.Drawing.Point(77, 73);
         this.isExactCheckBox.Name = "isExactCheckBox";
         this.isExactCheckBox.Properties.Caption = "";
         this.isExactCheckBox.Size = new System.Drawing.Size(369, 21);
         this.isExactCheckBox.StyleController = this.layoutControl1;
         this.isExactCheckBox.TabIndex = 2;
         // 
         // campaignComboBox
         // 
         this.campaignComboBox.Location = new System.Drawing.Point(77, 138);
         this.campaignComboBox.Name = "campaignComboBox";
         this.campaignComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.campaignComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
         this.campaignComboBox.Size = new System.Drawing.Size(369, 22);
         this.campaignComboBox.StyleController = this.layoutControl1;
         this.campaignComboBox.TabIndex = 4;
         // 
         // actionComboBox
         // 
         this.actionComboBox.EditValue = "";
         this.actionComboBox.Location = new System.Drawing.Point(77, 105);
         this.actionComboBox.Name = "actionComboBox";
         this.actionComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.actionComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
         this.actionComboBox.Size = new System.Drawing.Size(369, 22);
         this.actionComboBox.StyleController = this.layoutControl1;
         this.actionComboBox.TabIndex = 3;
         // 
         // wordTextBox
         // 
         this.wordTextBox.Location = new System.Drawing.Point(77, 40);
         this.wordTextBox.Name = "wordTextBox";
         this.wordTextBox.Size = new System.Drawing.Size(369, 22);
         this.wordTextBox.StyleController = this.layoutControl1;
         this.wordTextBox.TabIndex = 1;
         // 
         // wordIdTextBox
         // 
         this.wordIdTextBox.Location = new System.Drawing.Point(77, 7);
         this.wordIdTextBox.Name = "wordIdTextBox";
         this.wordIdTextBox.Properties.ReadOnly = true;
         this.wordIdTextBox.Size = new System.Drawing.Size(369, 22);
         this.wordIdTextBox.StyleController = this.layoutControl1;
         this.wordIdTextBox.TabIndex = 0;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.word,
            this.wordId,
            this.action,
            this.adCampaign,
            this.isExact});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "layoutControlGroup1";
         this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.layoutControlGroup1.Size = new System.Drawing.Size(452, 166);
         this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.layoutControlGroup1.Text = "layoutControlGroup1";
         this.layoutControlGroup1.TextVisible = false;
         // 
         // word
         // 
         this.word.AllowHotTrack = false;
         this.word.Control = this.wordTextBox;
         this.word.CustomizationFormText = "Wort:";
         this.word.Location = new System.Drawing.Point(0, 33);
         this.word.Name = "word";
         this.word.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
         this.word.Size = new System.Drawing.Size(450, 33);
         this.word.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.word.Text = "Wort:";
         this.word.TextSize = new System.Drawing.Size(65, 20);
         // 
         // wordId
         // 
         this.wordId.AllowHotTrack = false;
         this.wordId.Control = this.wordIdTextBox;
         this.wordId.CustomizationFormText = "Wort-ID:";
         this.wordId.Location = new System.Drawing.Point(0, 0);
         this.wordId.Name = "wordId";
         this.wordId.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
         this.wordId.Size = new System.Drawing.Size(450, 33);
         this.wordId.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.wordId.Text = "Wort-ID:";
         this.wordId.TextSize = new System.Drawing.Size(65, 20);
         // 
         // action
         // 
         this.action.AllowHotTrack = false;
         this.action.Control = this.actionComboBox;
         this.action.CustomizationFormText = "Aktion:";
         this.action.Location = new System.Drawing.Point(0, 98);
         this.action.Name = "action";
         this.action.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
         this.action.Size = new System.Drawing.Size(450, 33);
         this.action.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.action.Text = "Aktion:";
         this.action.TextSize = new System.Drawing.Size(65, 20);
         // 
         // adCampaign
         // 
         this.adCampaign.AllowHotTrack = false;
         this.adCampaign.Control = this.campaignComboBox;
         this.adCampaign.CustomizationFormText = "Kampagne:";
         this.adCampaign.Location = new System.Drawing.Point(0, 131);
         this.adCampaign.Name = "adCampaign";
         this.adCampaign.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
         this.adCampaign.Size = new System.Drawing.Size(450, 33);
         this.adCampaign.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.adCampaign.Text = "Kampagne:";
         this.adCampaign.TextSize = new System.Drawing.Size(65, 20);
         // 
         // isExact
         // 
         this.isExact.AllowHotTrack = false;
         this.isExact.Control = this.isExactCheckBox;
         this.isExact.CustomizationFormText = "Exakt:";
         this.isExact.Location = new System.Drawing.Point(0, 66);
         this.isExact.Name = "isExact";
         this.isExact.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
         this.isExact.Size = new System.Drawing.Size(450, 32);
         this.isExact.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
         this.isExact.Text = "Exakt:";
         this.isExact.TextSize = new System.Drawing.Size(65, 20);
         // 
         // panelControl2
         // 
         this.panelControl2.Controls.Add(this.saveButton);
         this.panelControl2.Controls.Add(this.cancelButton);
         this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panelControl2.Location = new System.Drawing.Point(0, 170);
         this.panelControl2.Name = "panelControl2";
         this.panelControl2.Size = new System.Drawing.Size(456, 37);
         this.panelControl2.TabIndex = 2;
         this.panelControl2.Text = "panelControl2";
         // 
         // saveButton
         // 
         this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.saveButton.Location = new System.Drawing.Point(288, 7);
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size(75, 23);
         this.saveButton.TabIndex = 0;
         this.saveButton.Text = "Speichern";
         this.saveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
         // 
         // cancelButton
         // 
         this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point(369, 7);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(75, 23);
         this.cancelButton.TabIndex = 1;
         this.cancelButton.Text = "Abbrechen";
         // 
         // MonitorControlAdWordForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(456, 207);
         this.Controls.Add(this.panelControl2);
         this.Controls.Add(this.panelControl1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "MonitorControlAdWordForm";
         this.Text = "Werbekampagen Editor";
         ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
         this.panelControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.isExactCheckBox.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.campaignComboBox.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.actionComboBox.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordTextBox.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordIdTextBox.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.word)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.wordId)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.action)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.adCampaign)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.isExact)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
         this.panelControl2.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraEditors.PanelControl panelControl1;
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraEditors.TextEdit wordTextBox;
      private DevExpress.XtraEditors.TextEdit wordIdTextBox;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem word;
      private DevExpress.XtraLayout.LayoutControlItem wordId;
      private DevExpress.XtraEditors.PanelControl panelControl2;
      private DevExpress.XtraEditors.SimpleButton saveButton;
      private DevExpress.XtraEditors.SimpleButton cancelButton;
      private DevExpress.XtraEditors.ComboBoxEdit actionComboBox;
      private DevExpress.XtraLayout.LayoutControlItem action;
      private DevExpress.XtraEditors.ComboBoxEdit campaignComboBox;
      private DevExpress.XtraLayout.LayoutControlItem adCampaign;
      private DevExpress.XtraEditors.CheckEdit isExactCheckBox;
      private DevExpress.XtraLayout.LayoutControlItem isExact;


   }
}