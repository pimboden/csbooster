//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public partial class AssemblyInfo : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetObjectLists");

        public override bool ShowObject(string settingsXml)
        {

            string template = "AssemblyInfo.ascx";
            string repeater = "AssemblyInfo.ascx";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            Control ctrl = LoadControl("~/UserControls/Repeaters/" + repeater);
            IRepeater rep = ctrl as IRepeater;
            rep.OutputTemplate = template;  
            this.Ph.Controls.Add(ctrl);
            return true;
        }
    }
}