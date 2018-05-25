using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public partial class ContextLinks : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            Control ctrl = LoadControl("~/UserControls/ContextLinks.ascx");
            ctrl.ID = "CL";
            IMinimalControl minimal = ctrl as IMinimalControl;
            phC.Controls.Add(ctrl);
            if (!Request.IsAuthenticated || 
                (!minimal.HasContent && !UserDataContext.GetUserDataContext().IsAdmin))
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