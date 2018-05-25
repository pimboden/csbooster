//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.20  1.0.0.4  AW  Initial release
//*****************************************************************************************

using System;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace _4screen.CSB.Monitor
{
   public partial class MonitorControlFeaturedContentEdit : MonitorControl
   {
      public MonitorControlFeaturedContentEdit(MonitorControlFeaturedContentEditProperties properties)
         : base(properties)
      {
         InitializeComponent();

         this.Text = "Featured Content";
      }

      public void Reload()
      {
         // Load users from via service
         CSBDataObject[] dataObjects = null;
         try
         {
            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlFeaturedContentEditProperties)this.properties).ServiceLocation]);
            dataObjects = service.GetDataObjects(this.objectIdTextBox.Text, this.communityIdTextBox.Text, this.userIdTextBox.Text, this.featuredCheckBox.Checked);
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
         this.bindingSource1.DataSource = dataObjects;
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
         if (e.Column.FieldName == "Featured")
         {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = ((DevExpress.XtraGrid.Views.Grid.GridView)sender);
            CSBDataObject dataObject = (CSBDataObject)gridView.GetRow(e.RowHandle);

            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlFeaturedContentEditProperties)this.properties).ServiceLocation]);

            if ((bool)e.Value == true)
            {
               CSBoosterMonitor.GetServiceForm().AppendMessage("Feature " + dataObject.Title + " ...");
               try
               {
                  service.FeatureDataObject(dataObject.ObjectId.ToString(), true);
               }
               catch (SoapException ex)
               {
                  CSBoosterMonitor.GetServiceForm().AppendMessage(ex.Message + " -> " + this.Text);
               }
            }
            else
            {
               CSBoosterMonitor.GetServiceForm().AppendMessage("Stufe " + dataObject.Title + " zurück ...");
               try
               {
                  service.FeatureDataObject(dataObject.ObjectId.ToString(), false);
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
