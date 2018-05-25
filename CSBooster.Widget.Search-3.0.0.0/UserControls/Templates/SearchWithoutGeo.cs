//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using System.Text;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class SearchWithoutGeo : System.Web.UI.UserControl, IDataObjectWorker
    {
        protected global::System.Web.UI.WebControls.TextBox TxtSearch;
        protected global::System.Web.UI.WebControls.LinkButton LbtnFind;
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
            this.LnkFind.Attributes.Add("OnClick", "DoFind('" + this.LbtnFind.UniqueID + "');");
            this.TxtSearch.Attributes.Add("OnKeyPress", "return DoFindOnEnterKey(event, '" + this.LbtnFind.UniqueID + "');");

            currentUrlPath = Request.RawUrl.Split('?')[0];

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
        }

        protected void OnFind(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> requestParams = new List<KeyValuePair<string, string>>();
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
            url += Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "SG", "IC" }, !needAmp);

            Response.Redirect(url);
        }



    }
}