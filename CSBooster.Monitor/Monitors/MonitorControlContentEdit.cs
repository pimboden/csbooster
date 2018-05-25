//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.3  AW  Initial release
//*****************************************************************************************

using System;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace _4screen.CSB.Monitor
{
   public partial class MonitorControlContentEdit : MonitorControl
   {
      public MonitorControlContentEdit(MonitorControlContentEditProperties properties)
         : base(properties)
      {
         InitializeComponent();

         this.Text = "Inhalt Editor";
      }

      public void Reload()
      {
         // Load data from via service
         ContentData[] contentData = null;
         try
         {
            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlContentEditProperties)this.properties).ServiceLocation]);
            contentData = service.GetContentData();
            foreach (ContentData contentDataEntry in contentData)
            {
               contentDataEntry.Content = contentDataEntry.Content.Replace("\n", "\r\n");
            }
            CSBoosterMonitor.GetServiceForm().AppendMessage("Daten aktualisiert -> " + this.Text);
         }
         catch (SoapException ex)
         {
            CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message);
         }
         catch (WebException exc)
         {
            CSBoosterMonitor.GetServiceForm().AppendMessage(exc.Message);
         }

         // Bind the data to the grid control
         this.bindingSource1.DataSource = contentData;
      }

      private void OnReloadItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         this.Reload();
      }

      private void OnPropertyItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         base.OnPropertyItemClickBase(sender, e);
      }

      private void OnCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
      {
         if (e.Column.FieldName == "Content")
         {
            if (DialogResult.Yes == MessageBox.Show("Möchten sie diesen Inhalt speichern?", "Änderung speichern", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
               try
               {
                  DevExpress.XtraGrid.Views.Grid.GridView gridView = ((DevExpress.XtraGrid.Views.Grid.GridView)sender);
                  ContentData contentData = (ContentData)gridView.GetRow(e.RowHandle);

                  CSBoosterMonitor.GetServiceForm().AppendMessage("Ändere Inhalt mit Schlüssel " + contentData.Key + " ...");

                  Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlContentEditProperties)this.properties).ServiceLocation]);
                  service.SetContentData(contentData.Key, contentData.Content);
               }
               catch (SoapException ex)
               {
                  CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
               }
            }
            this.Reload();
         }
      }
   }
}
