using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarContent : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAdmin = UserDataContext.GetUserDataContext().IsAdmin;

            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "tab" }, false);

            LnkWidgetSelect.CssClass = "cBarTabInactive";
            LnkWidgetSelect.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Widgets"].Enabled;
            LnkWidgetSelect.NavigateUrl = string.Format("{0}?tab=widgets{1}", Request.GetRawPath(), filteredQueryString);

            LnkContent.CssClass = "cBarTabInactive";
            LnkContent.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Content"].Enabled;
            LnkContent.NavigateUrl = string.Format("{0}?tab=content{1}", Request.GetRawPath(), filteredQueryString);

            if (Request.QueryString["tab"] == "content" && LnkContent.Visible)
            {
                MVCustBar.ActiveViewIndex = 1;
                LnkContent.CssClass = "cBarTab";
            }
            else if (LnkWidgetSelect.Visible)
            {
                MVCustBar.ActiveViewIndex = 0;
                LnkWidgetSelect.CssClass = "cBarTab";
            }

            WT.CommunityID = CommunityID;
            WT.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Widgets"].Enabled;
            CH.CommunityID = CommunityID;
            CH.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Content"].Enabled;

            LnkContent.ID = null;
            LnkWidgetSelect.ID = null;
        }
    }
}