// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI
{
    public partial class MasterPages_Static : System.Web.UI.MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
            this.head.Controls.Add(new LiteralControl(string.Format("<meta name=\"generator\" content=\"{0}\" />", Constants.META_GENERATOR.Replace("%VERSION%", typeof(Default).Assembly.GetName().Version.ToString()))));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            head.ID = null;
        }
    }
}
