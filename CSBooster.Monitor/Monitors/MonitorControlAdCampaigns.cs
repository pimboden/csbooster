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
using DevExpress.XtraGrid.Columns;

namespace _4screen.CSB.Monitor
{
   public partial class MonitorControlAdCampaigns : MonitorControl
   {
      public MonitorControlAdCampaigns(MonitorControlAdCampaignsProperties properties)
         : base(properties)
      {
         InitializeComponent();

         this.Text = "Werbekampagnen";
      }

      public void Reload()
      {
         // Load ads from via service
         CSBAdCampaign[] adCampaigns = null;
         CSBAdWord[] adWords = null;
         try
         {
            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlAdCampaignsProperties)this.properties).ServiceLocation]);
            adCampaigns = service.GetAdCampaigns();
            adWords = service.GetAdWords();
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

         // Bind ads to the grid control
         this.bindingSource1.DataSource = adCampaigns;
         this.bindingSource2.DataSource = adWords;
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

      private void OnNewCampaignButtonClick(object sender, EventArgs e)
      {
         MonitorControlAdCampaignForm adCampaignForm = new MonitorControlAdCampaignForm(this);
         adCampaignForm.Init(null);
         if (adCampaignForm.ShowDialog(this) == DialogResult.OK)
            this.Reload();
      }

      private void OnNewWordButtonClick(object sender, EventArgs e)
      {
         MonitorControlAdWordForm adWordForm = new MonitorControlAdWordForm(this);
         adWordForm.Init(null);
         if (adWordForm.ShowDialog(this) == DialogResult.OK)
            this.Reload();
      }

      private void OnAdCampaignGridDoubleClick(object sender, MouseEventArgs e)
      {
         MonitorControlAdCampaignForm adCampaignForm = new MonitorControlAdCampaignForm(this);
         int[] selectedRows = this.gridView1.GetSelectedRows();
         if (selectedRows.Length == 1)
         {
            CSBAdCampaign adCampaign = (CSBAdCampaign)this.gridView1.GetRow(selectedRows[0]);
            adCampaignForm.Init(adCampaign);
            if (adCampaignForm.ShowDialog(this) == DialogResult.OK)
               this.Reload();
         }
      }

      private void OnAdWordGridDoubleClick(object sender, MouseEventArgs e)
      {
         MonitorControlAdWordForm adWordForm = new MonitorControlAdWordForm(this);
         int[] selectedRows = this.gridView2.GetSelectedRows();
         if (selectedRows.Length == 1)
         {
            CSBAdWord adWord = (CSBAdWord)this.gridView2.GetRow(selectedRows[0]);
            adWordForm.Init(adWord);
            if (adWordForm.ShowDialog(this) == DialogResult.OK)
               this.Reload();
         }
      }
   }
}
