using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarStyle : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAdmin = UserDataContext.GetUserDataContext().IsAdmin;

            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "tab" }, false);

            LnkLayout.CssClass = "cBarTabInactive";
            LnkLayout.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Layout"].Enabled;
            LnkLayout.NavigateUrl = string.Format("{0}?tab=layout{1}", Request.GetRawPath(), filteredQueryString);

            LnkTheme.CssClass = "cBarTabInactive";
            LnkTheme.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Theme"].Enabled;
            LnkTheme.NavigateUrl = string.Format("{0}?tab=theme{1}", Request.GetRawPath(), filteredQueryString);

            LnkWidgetStyle.CssClass = "cBarTabInactive";
            LnkWidgetStyle.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Style"].Enabled;
            LnkWidgetStyle.NavigateUrl = string.Format("{0}?tab=style{1}", Request.GetRawPath(), filteredQueryString);

            if (Request.QueryString["tab"] == "layout" && LnkLayout.Visible)
            {
                this.MVCustBar.ActiveViewIndex = 1;
                LnkLayout.CssClass = "cBarTab";
            }
            else if (Request.QueryString["tab"] == "theme" && LnkTheme.Visible)
            {
                this.MVCustBar.ActiveViewIndex = 2;
                LnkTheme.CssClass = "cBarTab";
            }
            else if (LnkWidgetStyle.Visible)
            {
                this.MVCustBar.ActiveViewIndex = 0;
                LnkWidgetStyle.CssClass = "cBarTab";
            }

            LH.CommunityID = CommunityID;
            LH.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Layout"].Enabled;
            TH.CommunityID = CommunityID;
            TH.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Theme"].Enabled;
            CSH.CommunityID = CommunityID;
            CSH.Visible = isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Style"].Enabled;

            LnkWidgetStyle.ID = null;
            LnkLayout.ID = null;
            LnkTheme.ID = null;
        }
    }
}