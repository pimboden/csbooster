using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace _4screen.CSB.Monitor
{
  public partial class AboutForm : DevExpress.XtraEditors.XtraForm
  {
    public AboutForm()
    {
      InitializeComponent();

      this._4screenLinkLabel.Links.Add(0, this._4screenLinkLabel.Text.Length, "http://www.4screen.ch/");
    }

    public void SetInfo(string info)
    {
      this.memoEdit1.Text = info;
    }

    private void OnOkButtonClick(object sender, EventArgs e)
    {
      this.Close();
    }

    private void On4screenLinkLabelClick(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo(e.Link.LinkData.ToString());
      System.Diagnostics.Process.Start(processStartInfo);
    }
  }
}
