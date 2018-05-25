// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class SearchWithGeo : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected global::System.Web.UI.WebControls.TextBox TxtSearch;
        protected global::System.Web.UI.WebControls.TextBox TxtLoc;
        protected global::System.Web.UI.WebControls.DropDownList DDRadius;
        protected global::System.Web.UI.WebControls.DropDownList DDCountry;
        protected global::System.Web.UI.WebControls.HiddenField HidCoords;
        protected global::System.Web.UI.WebControls.LinkButton LbtnFind;
        protected global::System.Web.UI.WebControls.HyperLink LnkReset;
        protected global::System.Web.UI.WebControls.HyperLink LnkFind;

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetSearch");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private string currentUrlPath;

        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LbtnFind.Click += new EventHandler(this.OnFind);
            this.LnkFind.Attributes.Add("OnClick", "DoFind('" + this.ClientID + "_TxtLoc', '" + this.ClientID + "_DDCountry', '" + this.ClientID + "_HidCoords', '" + this.LbtnFind.UniqueID + "');");
            this.TxtLoc.Attributes.Add("OnKeyPress", "return DoMapFindOnEnterKey(event, '" + this.ClientID + "_TxtLoc', '" + this.ClientID + "_DDCountry', '" + this.ClientID + "_HidCoords', '" + this.LbtnFind.UniqueID + "');");
            this.TxtSearch.Attributes.Add("OnKeyPress", "return DoMapFindOnEnterKey(event, '" + this.ClientID + "_TxtLoc', '" + this.ClientID + "_DDCountry', '" + this.ClientID + "_HidCoords', '" + this.LbtnFind.UniqueID + "');");

            currentUrlPath = Request.RawUrl.Split('?')[0];

            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "LOC", "CO", "GC", "DI", "SG", "IC" }, true);
            this.LnkReset.NavigateUrl = currentUrlPath + "?" + filteredQueryString;

            ListItem ch = null;
            ListItem de = null;
            ListItem at = null;

            List<CountryName> countries = CountryNames.Load();
            foreach (CountryName county in countries)
            {
                if (county.CountryCode == "CH")
                    ch = new ListItem(county.Name, county.CountryCode);
                else if (county.CountryCode == "DE")
                    de = new ListItem(county.Name, county.CountryCode);
                else if (county.CountryCode == "AT")
                    at = new ListItem(county.Name, county.CountryCode);
                else
                    this.DDCountry.Items.Add(new ListItem(county.Name, county.CountryCode));
            }
            if (ch != null && de != null && at != null)
            {
                this.DDCountry.Items.Insert(0, ch);
                this.DDCountry.Items.Insert(1, de);
                this.DDCountry.Items.Insert(2, at);
                this.DDCountry.Items.Insert(3, new ListItem("---------------------------------", ""));
            }

            if (!string.IsNullOrEmpty(Request.Params.Get("SG")))
            {
                try
                {
                    Guid guid = new Guid(Request.Params.Get("SG"));
                }
                catch
                {
                    this.TxtSearch.Text = Server.UrlDecode(Request.Params.Get("SG"));
                }
            }
            if (!string.IsNullOrEmpty(Request.Params.Get("LOC")))
            {
                this.TxtLoc.Text = Server.UrlDecode(Request.Params.Get("LOC"));
            }
            if (!string.IsNullOrEmpty(Request.Params.Get("DI")))
            {
                foreach (ListItem item in this.DDRadius.Items)
                {
                    item.Selected = false;
                    if (item.Value == Request.Params.Get("DI"))
                        item.Selected = true;
                }
            }
            if (!string.IsNullOrEmpty(Request.Params.Get("CO")))
            {
                foreach (ListItem item in this.DDCountry.Items)
                {
                    if (item.Value == Request.Params.Get("CO"))
                        item.Selected = true;
                }
            }
        }

        protected void OnFind(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> requestParams = new List<KeyValuePair<string, string>>();
            this.HidCoords.Value = this.HidCoords.Value.Replace(" ", "");
            if (!string.IsNullOrEmpty(this.TxtLoc.Text))
            {
                requestParams.Add(new KeyValuePair<string, string>("LOC", this.TxtLoc.Text));
                if (!string.IsNullOrEmpty(this.DDCountry.Text) && !string.IsNullOrEmpty(this.DDCountry.SelectedValue))
                    requestParams.Add(new KeyValuePair<string, string>("CO", this.DDCountry.SelectedValue));
                if (!string.IsNullOrEmpty(this.DDRadius.SelectedValue))
                    requestParams.Add(new KeyValuePair<string, string>("DI", this.DDRadius.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.HidCoords.Value))
                requestParams.Add(new KeyValuePair<string, string>("GC", this.HidCoords.Value));
            if (!string.IsNullOrEmpty(this.TxtSearch.Text))
                requestParams.Add(new KeyValuePair<string, string>("SG", this.TxtSearch.Text));

            if (requestParams.Count > 0)
                requestParams.Add(new KeyValuePair<string, string>("IC", "true"));

            string url = currentUrlPath + "?";
            for (int i = 0; i < requestParams.Count; i++)
            {
                url += requestParams[i].Key + "=" + HttpUtility.UrlEncodeUnicode(requestParams[i].Value);
                if (i < requestParams.Count - 1)
                    url += "&";
            }
            bool needAmp = (requestParams.Count > 0) ? true : false;
            url += Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "LOC", "CO", "GC", "DI", "SG", "IC" }, !needAmp);

            Response.Redirect(url);
        }



    }
}