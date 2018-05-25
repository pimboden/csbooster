// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class Info : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            DataObject dataObject = DataObject.LoadByReflection(PageInfo.UserId.HasValue ? PageInfo.UserId.Value : PageInfo.CommunityId.Value);

            string template = "InfoObject.ascx";
            if (WidgetHost.OutputTemplate != null && !string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                template = WidgetHost.OutputTemplate.OutputTemplateControl;

            Control control = LoadControl("~/UserControls/Templates/" + template);
            ((IDataObjectWorker)control).DataObject = dataObject;
            Ph.Controls.Add(control);

            return true;
        }
    }
}
