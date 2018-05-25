//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.20  1.0.0.4  AW  Initial release
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
   public partial class MonitorControlAdWordForm : DevExpress.XtraEditors.XtraForm
   {
      private MonitorControl parentControl;
      private CSBAdCampaign[] adCampaigns;

      public MonitorControlAdWordForm(MonitorControl parentControl)
      {
         InitializeComponent();
         this.parentControl = parentControl;

         foreach (string action in Enum.GetNames(typeof(AdWordFilterActions)))
         {
            this.actionComboBox.Properties.Items.Add(action);
         }
         try
         {
            Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlAdCampaignsProperties)parentControl.GetProperties()).ServiceLocation]);
            adCampaigns = service.GetAdCampaigns();
            foreach (CSBAdCampaign adCampaign in adCampaigns)
            {
               this.campaignComboBox.Properties.Items.Add(adCampaign.Description);
            }
         }
         catch { }
      }

      public void Init(CSBAdWord adWord)
      {
         if (adWord == null)
         {
            this.wordIdTextBox.Text = Guid.NewGuid().ToString();
         }
         else
         {
            this.wordIdTextBox.Text = adWord.AdWordId.ToString();
            this.wordTextBox.Text = adWord.Word;
            this.isExactCheckBox.Checked = adWord.IsExact;
            foreach (string action in Enum.GetNames(typeof(AdWordFilterActions)))
            {
               if (action == adWord.Action.ToString())
               {
                  this.actionComboBox.SelectedItem = adWord.Action.ToString();
                  continue;
               }
            }
            foreach (CSBAdCampaign adCampaign in adCampaigns)
            {
               if (adCampaign.CampaignId == adWord.CampaignId)
               {
                  this.campaignComboBox.SelectedItem = adCampaign.Description;
                  continue;
               }
            }
         }
      }

      public CSBAdWord GetAdWord()
      {
         CSBAdWord adWord = new CSBAdWord();
         try
         {
            adWord.AdWordId = new Guid(this.wordIdTextBox.Text);
            adWord.Word = string.IsNullOrEmpty(this.wordTextBox.Text) ? null : this.wordTextBox.Text;
            adWord.IsExact = this.isExactCheckBox.Checked;
            foreach (AdWordFilterActions action in Enum.GetValues(typeof(AdWordFilterActions)))
            {
               if (action.ToString() == this.actionComboBox.SelectedItem.ToString())
               {
                  adWord.Action = action;
                  continue;
               }
            }
            foreach (CSBAdCampaign adCampaign in adCampaigns)
            {
               if (adCampaign.Description == this.campaignComboBox.SelectedItem.ToString())
               {
                  adWord.CampaignId = adCampaign.CampaignId;
                  continue;
               }
            }
         }
         catch { }
         return adWord;
      }

      private void OnSaveButtonClick(object sender, EventArgs e)
      {
         CSBAdWord adWord = this.GetAdWord();
         Service service = ServiceHelper.GetService(Program.GetProperties().GetServiceUrls()[((MonitorControlAdCampaignsProperties)parentControl.GetProperties()).ServiceLocation]);
         if (service.SaveAdWord(adWord))
         {
            this.Close();
            this.DialogResult = DialogResult.OK;
         }
      }
   }
}
