//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class Search : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            string template = "SearchWithGeo.ascx";
            string repeater = "";
            if (_Host.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                    template = _Host.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(_Host.OutputTemplate.RepeaterControl))
                    repeater = _Host.OutputTemplate.RepeaterControl;
            }

            Control control = LoadControl("~/UserControls/Templates/" + template); 
            Ph.Controls.Add(control);

            return true;
        }
    }
}
