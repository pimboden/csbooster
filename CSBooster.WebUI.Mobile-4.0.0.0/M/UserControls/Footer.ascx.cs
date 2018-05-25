// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;

namespace _4screen.CSB.WebUI.M.UserControls
{
    public partial class Footer : System.Web.UI.UserControl
    {
        public string Text { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
                this.Controls.Add(new LiteralControl(Text));
        }
    }
}