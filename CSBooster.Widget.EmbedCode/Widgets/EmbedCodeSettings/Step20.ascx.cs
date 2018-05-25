// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class EmbedCodeSettings_Step20 : _4screen.CSB.Common.StepsASCX
    {
        private Control widgetTemplate;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

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