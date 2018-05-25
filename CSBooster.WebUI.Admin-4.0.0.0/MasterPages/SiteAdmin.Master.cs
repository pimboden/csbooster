// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI
{
    public partial class MasterPages_SiteAdmin : System.Web.UI.MasterPage
    {
        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);    // #2.0.0.0
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);     // #2.0.0.0
        protected string CSS;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CSS = string.Format(@"<link href=""/Library/Styles/csbooster.css"" rel=""stylesheet"" type=""text/css""/><link href=""/Library/Styles/csboosteradmin.css"" rel=""stylesheet"" type=""text/css""/>");
            Page.Header.DataBind();
            this.head.Controls.Add(new LiteralControl(string.Format("<meta name=\"generator\" content=\"{0}\" />", Constants.META_GENERATOR.Replace("%VERSION%", typeof(Admin_Default).Assembly.GetName().Version.ToString()))));
        }

    }
}
