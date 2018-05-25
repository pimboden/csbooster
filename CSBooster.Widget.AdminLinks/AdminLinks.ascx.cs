using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4screen.CSB.Widget
{
    public partial class AdminLinks : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Control ctrl = LoadControl("~/UserControls/AdministrationLinks.ascx");
            ctrl.ID = "AL";
            IMinimalControl minimal = ctrl as IMinimalControl;
            phC.Controls.Add(ctrl);
            if (!Request.IsAuthenticated)
            {
                //hide the widget
                Control wgt = WidgetHelper.GetWidgetHost(this, 0, 6);
                if (wgt != null)
                {
                    wgt.Visible = false;
                    return false;
                }

            }
            return minimal.HasContent;
        }
    }
}