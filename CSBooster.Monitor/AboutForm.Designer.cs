namespace _4screen.CSB.Monitor
{
  partial class AboutForm
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
      this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
      this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
      this.okButton = new DevExpress.XtraEditors.SimpleButton();
      this._4screenLinkLabel = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // memoEdit1
      // 
      this.memoEdit1.Location = new System.Drawing.Point(12, 34);
      this.memoEdit1.Name = "memoEdit1";
      this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.memoEdit1.Properties.Appearance.Options.UseFont = true;
      this.memoEdit1.Properties.ReadOnly = true;
      this.memoEdit1.Size = new System.Drawing.Size(417, 142);
      this.memoEdit1.TabIndex = 0;
      // 
      // labelControl1
      // 
      this.labelControl1.Location = new System.Drawing.Point(12, 12);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(27, 16);
      this.labelControl1.TabIndex = 1;
      this.labelControl1.Text = "Info:";
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(354, 195);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 2;
      this.okButton.Text = "OK";
      this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
      // 
      // linkLabel1
      // 
      this._4screenLinkLabel.AutoSize = true;
      this._4screenLinkLabel.Location = new System.Drawing.Point(13, 195);
      this._4screenLinkLabel.Name = "linkLabel1";
      this._4screenLinkLabel.Size = new System.Drawing.Size(155, 17);
      this._4screenLinkLabel.TabIndex = 3;
      this._4screenLinkLabel.TabStop = true;
      this._4screenLinkLabel.Text = "http://www.4screen.ch/";
      this._4screenLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.On4screenLinkLabelClick);
      // 
      // AboutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(441, 233);
      this.Controls.Add(this._4screenLinkLabel);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.labelControl1);
      this.Controls.Add(this.memoEdit1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "AboutForm";
      this.Text = "Über CSBooster Monitor";
      this.TopMost = true;
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DevExpress.XtraEditors.MemoEdit memoEdit1;
    private DevExpress.XtraEditors.LabelControl labelControl1;
    private DevExpress.XtraEditors.SimpleButton okButton;
    private System.Windows.Forms.LinkLabel _4screenLinkLabel;
  }
}