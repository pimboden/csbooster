namespace _4screen.CSB.Monitor
{
  partial class PropertyForm
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
      this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
      ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // propertyGridControl1
      // 
      this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
      this.propertyGridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.propertyGridControl1.Name = "propertyGridControl1";
      this.propertyGridControl1.RecordWidth = 110;
      this.propertyGridControl1.RowHeaderWidth = 90;
      this.propertyGridControl1.ServiceProvider = null;
      this.propertyGridControl1.Size = new System.Drawing.Size(565, 327);
      this.propertyGridControl1.TabIndex = 0;
      // 
      // PropertyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(565, 327);
      this.Controls.Add(this.propertyGridControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "PropertyForm";
      this.Text = "Eigenschaften";
      ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
  }
}