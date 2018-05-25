// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Xml.Linq;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class NaviOutputOutlook : System.Web.UI.UserControl, IMinimalControl
    {
        public bool HasContent { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!string.IsNullOrEmpty(Prop1) && XElement.Parse(Prop1).Nodes().Count() > 0)
            {
                HasContent = true;
            }
            else
            {
                HasContent = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HasContent)
            {
                nav.LoadXml(Prop1);
            }
        }

    }
}