//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Template SearchGeo
//  Updated:   
//******************************************************************************

using System;
using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class SearchGeoSettings_Step20 : _4screen.CSB.Common.StepsASCX
    {
        private Control widgetTemplate;

        protected void Page_Load(object sender, EventArgs e)
        {
            widgetTemplate = LoadControl("~/WidgetControls/WidgetTemplate.ascx");
            PhWT.Controls.Add(widgetTemplate);
        }

        public override bool SaveStep(int NextStep)
        {
            base.SaveStep(NextStep);
            try
            {
                return ((IWidgetTemplate) widgetTemplate).Save();
            }
            catch
            {
                return false;
            }
        }
    }
}