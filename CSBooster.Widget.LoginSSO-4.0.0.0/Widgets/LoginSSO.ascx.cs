// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public partial class LoginSSO : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            string template = "LoginSSO.ascx";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;
            }

            Control control = this.LoadControl("~/UserControls/Templates/" + template);
            IMinimalControl minimal = control as IMinimalControl;
            control.ID = "LoginSSO";
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
