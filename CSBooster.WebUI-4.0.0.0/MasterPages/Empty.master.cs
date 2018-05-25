// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using _4screen.CSB.Common;

// #2.0.0.0

namespace _4screen.CSB.WebUI
{
    public partial class MasterPages_Empty : System.Web.UI.MasterPage
    {
        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);

        #region move the viewstate to the end of the form --> SEO optimization / REMOVE WHITESPACES

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // Obtain the HTML rendered by the instance.
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            base.Render(hw);
            string html = sw.ToString();

            // Hose the writers we don't need anymore.
            hw.Close();
            sw.Close();

            // Find the viewstate. Hope M$ doesn't decide to change the case of these tags anytime soon.
            int start = html.IndexOf(@"<input type=""hidden"" name=""__VIEWSTATE""");
            // If we find it, then move it.
            if (start > -1)
            {
                int end = html.IndexOf("/>", start) + 2;

                // Strip out the viewstate.
                string viewstate = html.Substring(start, end - start);
                html = html.Remove(start, end - start);

                // Find the end of the form and insert it there, since we can't put it any lower in the response stream.
                int formend = html.IndexOf("</form>") - 1;
                if (formend >= 0)
                    html = html.Insert(formend, viewstate);
            }

            html = REGEX_BETWEEN_TAGS.Replace(html, "> <");          // #2.0.0.0
            html = REGEX_LINE_BREAKS.Replace(html, string.Empty);    // #2.0.0.0

            // Send the results back into the writer provided.
            writer.Write(html);
        }

        #endregion move the viewstate to the end of the form --> SEO optimization

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