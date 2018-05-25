//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//*****************************************************************************************

using System;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
  public partial class MonitorControlUsers : MonitorControl
  {
    public MonitorControlUsers(MonitorControlUsersProperties properties)
      : base(properties)
    {
      InitializeComponent();

      this.Text = "Benutzer Monitor";
    }

    public void Reload()
    {
      // Load users from via service
      User[] users = null;
      try
      {
        Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlUsersProperties)this.properties).ServiceLocation]);
        users = service.GetUsers(this.usernameTextBox.Text, this.emailTextBox.Text, this.lockedCheckBox.Checked);
        CSBoosterMonitor.GetServiceForm().AppendMessage("Daten aktualisiert -> " + this.Text);
      }
      catch (SoapException ex)
      {
        CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
      }
      catch (WebException exc)
      {
        CSBoosterMonitor.GetServiceForm().AppendMessage(exc.Message + " -> " + this.Text);
      }

      // Bind the users to the grid control
      this.bindingSource1.DataSource = users;
    }

    private void OnReloadItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      this.Reload();
    }

    private void OnPropertyItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
      OnPropertyItemClickBase(sender, e);
    }

    private void OnFindButtonClick(object sender, EventArgs e)
    {
      this.Reload();
    }

    private void OnCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
      if (e.Column.FieldName == "IsLocked")
      {
        DevExpress.XtraGrid.Views.Grid.GridView gridView = ((DevExpress.XtraGrid.Views.Grid.GridView)sender);
        User user = (User)gridView.GetRow(e.RowHandle);

        Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlUsersProperties)this.properties).ServiceLocation]);

        if ((bool)e.Value == true)
        {
          CSBoosterMonitor.GetServiceForm().AppendMessage("Sperre " + user.Username + " ...");
          try
          {
            service.LockUser(user.Username);
          }
          catch (SoapException ex)
          {
            CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
          }
        }
        else
        {
          CSBoosterMonitor.GetServiceForm().AppendMessage("Entsperre " + user.Username + " ...");
          try
          {
            service.UnlockUser(user.Username);
          }
          catch (SoapException ex)
          {
            CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
          }
        }
        this.Reload();
      }
    }

    private void OnCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
      if (e.Column.FieldName == "Email")
      {
        if (DialogResult.Yes == MessageBox.Show("Möchten sie die Email Adresse speichern?", "Änderung speichern", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          try
          {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = ((DevExpress.XtraGrid.Views.Grid.GridView)sender);
            User user = (User)gridView.GetRow(e.RowHandle);

            CSBoosterMonitor.GetServiceForm().AppendMessage("Ändere Email von " + user.Username + " ...");

            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlUsersProperties)this.properties).ServiceLocation]);
            service.SetUserEmail(user.Username, user.Email);
          }
          catch (SoapException ex)
          {
            CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
          }
        }
        this.Reload();
      }
    }

    private void OnSearchFieldKeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Enter)
      {
        this.Reload();
      }
    }
  }
}
