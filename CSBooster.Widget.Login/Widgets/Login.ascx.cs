using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.DataAccess;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class Login : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            string template = "Login.ascx";
            if (_Host.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                    template = _Host.OutputTemplate.OutputTemplateControl;
            }

            Control control = this.LoadControl("~/UserControls/Templates/" + template);
            IMinimalControl minimal = control as IMinimalControl;
            control.ID = "Login";
            PnlCnt.Controls.Add(control);
            if (Request.IsAuthenticated && !UserDataContext.GetUserDataContext().IsAdmin)
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
