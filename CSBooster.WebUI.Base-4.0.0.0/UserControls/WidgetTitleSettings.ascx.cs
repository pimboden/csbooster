// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class WidgetTitleSettings : System.Web.UI.UserControl, IWidgetSettings
    {
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            TxtTitle.Text = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetTitle(InstanceId);
        }

        public bool Save()
        {
            return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetTitle(InstanceId, TxtTitle.Text);
        }
    }
}
