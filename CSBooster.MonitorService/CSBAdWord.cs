//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.4  AW  Initial release
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
   public class CSBAdWord
   {
      private Guid adWordId;
      private string word;
      private bool isExact;
      private AdWordFilterActions action;
      private Guid campaignId;
      private string campaignDescription;

      public Guid AdWordId
      {
         get { return adWordId; }
         set { adWordId = value; }
      }

      public string Word
      {
         get { return word; }
         set { word = value; }
      }

      public bool IsExact
      {
         get { return isExact; }
         set { isExact = value; }
      }

      public AdWordFilterActions Action
      {
         get { return action; }
         set { action = value; }
      }

      public Guid CampaignId
      {
         get { return campaignId; }
         set { campaignId = value; }
      }

      public string CampaignDescription
      {
         get { return campaignDescription; }
         set { campaignDescription = value; }
      }

      public CSBAdWord()
      {
      }
   }
}
