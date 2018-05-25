//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Template Info
//  Updated:   
//******************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace _4screen.CSB.Widget
{
   public partial class TestWidgetSettings_Step20 : _4screen.CSB.Common.StepsASCX
   {
      Control widgetTemplate;

      protected override void OnInit(EventArgs e)
      {
         base.OnInit(e);
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         widgetTemplate = this.LoadControl("~/UserControls/WidgetTemplate.ascx");
         this.PhWT.Controls.Add(widgetTemplate);
      }

      public override bool SaveStep(int NextStep)
      {
         base.SaveStep(NextStep);
         try
         {
            return ((IWidgetTemplate)widgetTemplate).Save();
         }
         catch
         {

            return false;
         }
      }

   }
}
