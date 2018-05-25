//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.20  1.0.0.4  AW  Initial release
//  2007.11.26  1.0.0.5  AW  New cost attributes
//*****************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraEditors;

namespace _4screen.CSB.Monitor
{
   public partial class MonitorControlAdCampaignForm : DevExpress.XtraEditors.XtraForm
   {
      private MonitorControl parentControl;
      private List<DictionaryEntry> bannerPageList = new List<DictionaryEntry>();

      public MonitorControlAdCampaignForm(MonitorControl parentControl)
      {
         InitializeComponent();
         this.parentControl = parentControl;

         Hashtable bannerPageHashtable = (Hashtable)ConfigurationManager.GetSection("adCampaignBannerPages");
         foreach (DictionaryEntry bannerPage in bannerPageHashtable)
            bannerPageList.Add(bannerPage);
         bannerPageList.Sort(CompareDictionaryEntryByValue);
         foreach (DictionaryEntry bannerPage in bannerPageList)
            this.bannerPageComboBox.Properties.Items.Add(bannerPage.Value);
      }

      public void Init(CSBAdCampaign adCampaign)
      {
         if (adCampaign == null)
         {
            this.camapaignIdTextBox.Text = Guid.NewGuid().ToString();
            this.creditsTextBox.Text = "1000.0";
            this.creditsUsedTextBox.Text = "0.0";
            this.costPerBannerClickTextBox.Text = "50.0";
            this.costPerBannerViewTextBox.Text = "2.0";
            this.costPerLinkClickTextBox.Text = "50.0";
            this.costPerPopupLinkClickTextBox.Text = "100.0";
            this.costPerPopupViewTextBox.Text = "5.0";
         }
         else
         {
            this.camapaignIdTextBox.Text = adCampaign.CampaignId.ToString();
            this.creditsTextBox.Text = adCampaign.Credits.ToString();
            this.creditsUsedTextBox.Text = adCampaign.CreditsUsed.ToString();
            this.costPerBannerClickTextBox.Text = adCampaign.CostPerBannerClick.ToString();
            this.costPerBannerViewTextBox.Text = adCampaign.CostPerBannerView.ToString();
            this.costPerLinkClickTextBox.Text = adCampaign.CostPerLinkClick.ToString();
            this.costPerPopupLinkClickTextBox.Text = adCampaign.CostPerPopupLinkClick.ToString();
            this.costPerPopupViewTextBox.Text = adCampaign.CostPerPopupView.ToString();
            this.contentEdit.Text = adCampaign.Content;
            this.urlTextBox.Text = adCampaign.Url;
            this.descriptionTextBox.Text = adCampaign.Description;
            this.bannerPageComboBox.Text = adCampaign.Banner;
         }
      }

      public CSBAdCampaign GetAdCampaign()
      {
         CSBAdCampaign adCampaign = new CSBAdCampaign();
         try
         {
            adCampaign.CampaignId = new Guid(this.camapaignIdTextBox.Text);
            adCampaign.Description = string.IsNullOrEmpty(this.descriptionTextBox.Text) ? null : this.descriptionTextBox.Text;
            try { adCampaign.Banner = this.bannerPageComboBox.Text; }
            catch { }
            adCampaign.Url = string.IsNullOrEmpty(this.urlTextBox.Text) ? null : this.urlTextBox.Text;
            adCampaign.Content = string.IsNullOrEmpty(this.contentEdit.Text) ? null : this.contentEdit.Text;
            adCampaign.Credits = float.Parse(this.creditsTextBox.Text);
            adCampaign.CreditsUsed = float.Parse(this.creditsUsedTextBox.Text);
            adCampaign.CostPerBannerClick = float.Parse(this.costPerBannerClickTextBox.Text);
            adCampaign.CostPerBannerView = float.Parse(this.costPerBannerViewTextBox.Text);
            adCampaign.CostPerLinkClick = float.Parse(this.costPerLinkClickTextBox.Text);
            adCampaign.CostPerPopupLinkClick = float.Parse(this.costPerPopupLinkClickTextBox.Text);
            adCampaign.CostPerPopupView = float.Parse(this.costPerPopupViewTextBox.Text);
         }
         catch { }
         return adCampaign;
      }

      private static int CompareDictionaryEntryByValue(DictionaryEntry x, DictionaryEntry y)
      {
         return x.Value.ToString().CompareTo(y.Value.ToString());
      }

      private Predicate<DictionaryEntry> ValueEquals(string value)
      {
         return delegate(DictionaryEntry dictionaryEntry) { return dictionaryEntry.Value.ToString() == value; };
      }

      private void OnSaveButtonClick(object sender, EventArgs e)
      {
         CSBAdCampaign adCampaign = this.GetAdCampaign();
         Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlAdCampaignsProperties)parentControl.GetProperties()).ServiceLocation]);
         if (service.SaveAdCampaign(adCampaign))
         {
            this.Close();
            this.DialogResult = DialogResult.OK;
         }
      }

      private void OnAddLinkButtonClick(object sender, EventArgs e)
      {
         this.contentEdit.Text = this.contentEdit.Text.Insert(this.contentEdit.SelectionStart, "<a href=\"../../AdCampaignRedirecter.aspx?CID=" + this.camapaignIdTextBox.Text + "\" target=\"_blank\"> </a>");
      }
   }
}
