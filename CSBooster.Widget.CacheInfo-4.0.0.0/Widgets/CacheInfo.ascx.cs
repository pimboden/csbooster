// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class CacheInfo : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetCacheInfo");

        public override bool ShowObject(string settingsXml)
        {

            string template = "CacheInfo.ascx";
            string repeater = "CacheInfo.ascx";
            if (this.WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(this.WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = this.WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(this.WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = this.WidgetHost.OutputTemplate.RepeaterControl;
            }

            Control ctrl = LoadControl("~/UserControls/Repeaters/" + repeater);
            IRepeater rep = ctrl as IRepeater;
            rep.OutputTemplate = template;  
            this.Ph.Controls.Add(ctrl);
            return true;
        }
    }
}