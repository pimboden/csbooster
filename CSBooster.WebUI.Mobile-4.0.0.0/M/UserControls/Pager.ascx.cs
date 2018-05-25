using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls
{
    public partial class Pager : System.Web.UI.UserControl
    {
        private int currentPage;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        public Control BrowsableControl { get; set; }
        public int PageSize { get; set; }

        public void InitPager(int page, int numberItems)
        {
            string queryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "PN" }, true);

            currentPage = page;
            Visible = numberItems > PageSize;
            lnkPrevious.Enabled = page != 1;
            lnkPrevious.NavigateUrl = lnkPrevious.Enabled ? string.Format("{0}?{1}&PN={2}", Request.GetRawPath(), queryString, currentPage - 1) : null;
            lnkNext.Enabled = page != (int)Math.Ceiling((double)numberItems / PageSize);
            lnkNext.NavigateUrl = lnkNext.Enabled ? string.Format("{0}?{1}&PN={2}", Request.GetRawPath(), queryString, currentPage + 1) : null;
        }
    }
}
