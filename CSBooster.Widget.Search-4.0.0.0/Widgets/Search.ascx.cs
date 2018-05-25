// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;

namespace _4screen.CSB.Widget
{
    public partial class Search : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            string template = "SearchWithGeo.ascx";
            string repeater = "";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            Control control = LoadControl("~/UserControls/Templates/" + template); 
            Ph.Controls.Add(control);

            return true;
        }
    }
}
