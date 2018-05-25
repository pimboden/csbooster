// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.WebUI.UserControls;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.MasterPages
{
    public partial class Default : System.Web.UI.MasterPage, IWidgetPageMaster
    {
        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+<", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);

        public string HeaderStyle { get; set; }
        public string BodyStyle { get; set; }
        public string FooterStyle { get; set; }

        public HtmlMeta MetaDescription
        {
            get { return metadesc; }
            set { metadesc = value; }
        }

        public HtmlMeta MetaKeywords
        {
            get { return metatags; }
            set { metatags = value; }
        }

        public HtmlMeta MetaModifiedDate
        {
            get { return metadate; }
            set { metadate = value; }
        }

        public HtmlMeta MetaOgTitle
        {
            get { return metaOgTitle; }
            set { metaOgTitle = value; }
        }

        public HtmlMeta MetaOgType
        {
            get { return metaOgType; }
            set { metaOgType = value; }
        }

        public HtmlMeta MetaOgUrl
        {
            get { return metaOgUrl; }
            set { metaOgUrl = value; }
        }

        public HtmlMeta MetaOgImage
        {
            get { return metaOgImage; }
            set { metaOgImage = value; }
        }

        public HtmlMeta MetaOgSiteName
        {
            get { return metaOgSiteName; }
            set { metaOgSiteName = value; }
        }

        public HtmlMeta MetaOgDescription
        {
            get { return metaOgDescription; }
            set { metaOgDescription = value; }
        }

        public HtmlMeta MetaOgLatitude
        {
            get { return metaOgLatitude; }
            set { metaOgLatitude = value; }
        }

        public HtmlMeta MetaOgLongitude
        {
            get { return metaOgLongitude; }
            set { metaOgLongitude = value; }
        }

        public HtmlMeta MetaOgStreet
        {
            get { return metaOgStreet; }
            set { metaOgStreet = value; }
        }

        public HtmlMeta MetaOgCity
        {
            get { return metaOgCity; }
            set { metaOgCity = value; }
        }

        public HtmlMeta MetaOgZipCode
        {
            get { return metaOgZipCode; }
            set { metaOgZipCode = value; }
        }

        public HtmlMeta MetaOgCountryCode
        {
            get { return metaOgCountryCode; }
            set { metaOgCountryCode = value; }
        }

        public HtmlGenericControl BodyTag
        {
            get { return body; }
            set { body = value; }
        }

        public IBreadCrumb BreadCrumb
        {
            get { return (IBreadCrumb)breadcrumbs; }
            set { breadcrumbs = (BreadCrumb)value; }
        }

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

            head.Controls.Add(new LiteralControl(string.Format("<meta name=\"generator\" content=\"{0}\" />", Constants.META_GENERATOR.Replace("%VERSION%", typeof(Default).Assembly.GetName().Version.ToString()))));

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["UseCustomSkin"]))
                head.Controls.Add(new LiteralControl(string.Format("<link href=\"/Library/Skins/Custom/All.Custom.css\" rel=\"stylesheet\" type=\"text/css\" />")));

            metadesc.ID = null;
            metatags.ID = null;
            metadate.ID = null;

            metaOgTitle.ID = null;
            metaOgType.ID = null;
            metaOgUrl.ID = null;
            metaOgImage.ID = null;
            metaOgSiteName.ID = null;
            metaOgDescription.ID = null;
            metaOgLatitude.ID = null;
            metaOgLongitude.ID = null;
            metaOgStreet.ID = null;
            metaOgCity.ID = null;
            metaOgZipCode.ID = null;
            metaOgCountryCode.ID = null;

            head.ID = null;
            body.ID = null;
        }
    }
}