//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.MonitorService
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.11.06  1.0.0.3  AW  Initial release
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
   public class ContentData
   {
      private string key;
      private string content;

      public string Key
      {
         get { return key; }
         set { key = value; }
      }

      public string Content
      {
         get { return content; }
         set { content = value; }
      }

      public ContentData()
      {
      }

      public ContentData(string key, string content)
      {
         this.key = key;
         this.content = content;
      }
   }
}
