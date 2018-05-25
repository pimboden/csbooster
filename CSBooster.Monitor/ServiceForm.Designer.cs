namespace _4screen.CSB.Monitor
{
  partial class ServiceForm
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
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
      this.SuspendLayout();
      // 
      // memoEdit1
      // 
      this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.memoEdit1.Location = new System.Drawing.Point(0, 0);
      this.memoEdit1.Name = "memoEdit1";
      this.memoEdit1.Size = new System.Drawing.Size(468, 304);
      this.memoEdit1.TabIndex = 0;
      // 
      // ServiceForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(468, 304);
      this.ControlBox = false;
      this.Controls.Add(this.memoEdit1);
      this.Name = "ServiceForm";
      this.Text = "ServiceForm";
      ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraEditors.MemoEdit memoEdit1;



  }
}