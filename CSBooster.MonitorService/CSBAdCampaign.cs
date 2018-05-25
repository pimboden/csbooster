//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.4  AW  Initial release
//  2007.11.26  1.0.0.5  AW  New cost attributes
//*****************************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _4screen.CSB.MonitorService
{
   public class CSBAdCampaign
   {
      private Guid campaignId;
      private string description;
      private string banner;
      private string url;
      private string content;
      private float credits;
      private float creditsUsed;
      private float costPerLinkClick;
      private float costPerPopupLinkClick;
      private float costPerBannerClick;
      private float costPerPopupView;
      private float costPerBannerView;

      public Guid CampaignId
      {
         get { return campaignId; }
         set { campaignId = value; }
      }

      public string Description
      {
         get { return description; }
         set { description = value; }
      }

      public string Banner
      {
         get { return banner; }
         set { banner = value; }
      }

      public string Url
      {
         get { return url; }
         set { url = value; }
      }

      public string Content
      {
         get { return content; }
         set { content = value; }
      }

      public float Credits
      {
         get { return credits; }
         set { credits = value; }
      }

      public float CreditsUsed
      {
         get { return creditsUsed; }
         set { creditsUsed = value; }
      }

      public float CostPerLinkClick
      {
         get { return costPerLinkClick; }
         set { costPerLinkClick = value; }
      }

      public float CostPerPopupLinkClick
      {
         get { return costPerPopupLinkClick; }
         set { costPerPopupLinkClick = value; }
      }

      public float CostPerBannerClick
      {
         get { return costPerBannerClick; }
         set { costPerBannerClick = value; }
      }

      public float CostPerPopupView
      {
         get { return costPerPopupView; }
         set { costPerPopupView = value; }
      }

      public float CostPerBannerView
      {
         get { return costPerBannerView; }
         set { costPerBannerView = value; }
      }

      public CSBAdCampaign()
      {
      }
   }
}
