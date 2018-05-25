//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		18.09.2007 / PI
//  Updated:   #1.0.5.0    20.12.2007 / AW
//                         - Tab bar style fixed
//                         - Tab bar title edit style changed
//******************************************************************************

using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class TabBar : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public CSBooster_DataContext WDC { get; set; }
        public hitbl_Community_CTY Community { get; set; }
        public DataObject PageOrCommunity { get; set; }
        public bool IsMember { get; set; }
        public bool IsOwner { get; set; }
        public List<hitbl_Page_PAG> Pages { get; set; }
        public hitbl_Page_PAG CurrentPage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTabBar();
        }

        private void LoadTabBar()
        {
            tabList.Controls.Clear();

            bool addPageEnabled = CustomizationSection.CachedInstance.CustomizationBar.PageAddEnabled;
            bool updatePageEnabled = CustomizationSection.CachedInstance.CustomizationBar.PageUpdateEnabled;

            if (IsOwner)
            {
                if (addPageEnabled && !updatePageEnabled)
                    updatePageEnabled = true;
            }
            else if (UserDataContext.GetUserDataContext().IsAdmin)
            {
                addPageEnabled = true;
                updatePageEnabled = true;
            }
            else
            {
                addPageEnabled = false;
                updatePageEnabled = false;
            }

            int pagesStartIndex = IsOwner ? 0 : 1;
            for (int i = pagesStartIndex; i < Pages.Count; i++)
            {
                var page = Pages[i];

                HtmlGenericControl li = new HtmlGenericControl("li");
                li.ID = "Tab" + page.PAG_ID.ToString();
                li.Attributes["class"] = (page.PAG_ID == CurrentPage.PAG_ID ? "pageTabActive" : "pageTabInactive");

                if (page.PAG_OrderNr == 1 && IsOwner)
                    li.Attributes["class"] += " pageTabPrivate";

                if (Request.Form["__EVENTTARGET"] != this.UniqueID + "$Edit_" + page.PAG_ID.ToString())
                {
                    HyperLink gotoButton = new HyperLink();
                    gotoButton.Text = page.PAG_Title;
                    gotoButton.CssClass = "pageTabGoto";
                    gotoButton.NavigateUrl = GetFriendlyPageLink(i);
                    li.Controls.Add(gotoButton);
                }

                if (IsOwner && updatePageEnabled)
                {
                    LinkButton okButton = new LinkButton();
                    okButton.ID = "Ok_" + page.PAG_ID.ToString();
                    okButton.Attributes.Add("class", "pageTabButton pageTabSave");
                    okButton.CommandArgument = page.PAG_ID.ToString();
                    okButton.Click += new EventHandler(OnSaveTitleClick);
                    okButton.ToolTip = language.GetString("TooltipTabSaveTitle");
                    okButton.Visible = false;

                    TextBox editTextBox = new TextBox();
                    editTextBox.Width = 65;
                    editTextBox.ID = "TxtPageTitle_" + page.PAG_ID.ToString();
                    editTextBox.Text = page.PAG_Title;
                    editTextBox.Visible = false;
                    editTextBox.Attributes.Add("onkeypress", "DoPostbackOnEnterKey(event, '" + this.UniqueID + "$" + okButton.UniqueID + "')");
                    li.Controls.Add(editTextBox);
                    li.Controls.Add(okButton);

                    LinkButton editPageTitle = new LinkButton();
                    editPageTitle.ID = "Edit_" + page.PAG_ID.ToString();
                    editPageTitle.Attributes.Add("class", "pageTabButton pageTabEdit");
                    editPageTitle.CommandArgument = page.PAG_ID.ToString();
                    editPageTitle.ToolTip = language.GetString("TooltipTabEditTitle");
                    editPageTitle.Visible = false;
                    li.Controls.Add(editPageTitle);

                    if (Request.Form["__EVENTTARGET"] == this.UniqueID + "$Edit_" + page.PAG_ID.ToString())
                    {
                        okButton.Visible = true;
                        editTextBox.Visible = true;
                        ScriptManager.RegisterStartupScript((Control)upnl, upnl.GetType(), "SetFocus", "SelectTextBox('" + this.ClientID + "_" + editTextBox.UniqueID + "')", true);
                    }
                    else if (CustomizationSection.CachedInstance.CustomizationBar.Enabled)
                    {
                        editPageTitle.Visible = true;
                    }
                }

                if (page.PAG_OrderNr > 2 && addPageEnabled)
                {
                    LinkButton deletePageButton = new LinkButton();
                    deletePageButton.ID = "Del_" + page.PAG_ID.ToString();
                    deletePageButton.Attributes.Add("class", "pageTabButton pageTabDelete");
                    deletePageButton.CommandArgument = page.PAG_ID.ToString();
                    deletePageButton.OnClientClick = string.Format("return confirm('{0}');", language.GetString("MessageTabDeletePageConfirm").StripForScript());
                    deletePageButton.Click += new EventHandler(OnPageDeleteClick);
                    deletePageButton.ToolTip = language.GetString("TooltipTabDeletePage");
                    li.Controls.Add(deletePageButton);
                }
                tabList.Controls.Add(li);
            }
            if (IsOwner && CustomizationSection.CachedInstance.CustomizationBar.Enabled && addPageEnabled)
            {
                LinkButton addNewTabLinkButton = new LinkButton();
                addNewTabLinkButton.Text = language.GetString("CommandTabNewPage");
                addNewTabLinkButton.ID = "Insert";
                addNewTabLinkButton.Click += new EventHandler(OnNewPageClick);
                HtmlGenericControl li2 = new HtmlGenericControl("li");
                li2.Attributes["class"] = "pageTabNew";
                li2.Controls.Add(addNewTabLinkButton);
                tabList.Controls.Add(li2);
            }

            upnl.Update();

            tabList.ID = null;
        }

        private string GetFriendlyPageLink(int index)
        {
            string friendlyPagename = index == 0 ? "dashboard" : index.ToString();
            if (PageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                return Helper.GetDetailLink(PageOrCommunity.ObjectType, Community.CTY_VirtualUrl) + "/" + friendlyPagename;
            else
                return Helper.GetDetailLink("User", PageOrCommunity.Nickname) + "/" + friendlyPagename;
        }

        private void OnPageDeleteClick(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            Guid pageId = new Guid(linkButton.CommandArgument);
            var page = Pages.Find(x => x.PAG_ID == pageId);
            WDC.hitbl_Page_PAGs.DeleteOnSubmit(page);
            WDC.SubmitChanges();

            Response.Redirect(GetFriendlyPageLink(1));
        }

        private void OnNewPageClick(object sender, EventArgs e)
        {
            string pageType = "Community";
            if (PageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                pageType = "Profile";
            else if (PageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
                pageType = "Page";

            var subSonicPage = PagesConfig.CreateNewPage(PageOrCommunity.ObjectID.Value, pageType, "NewPage", language.GetString("TextTabUnnamed"));
            var page = WDC.hitbl_Page_PAGs.Where(x => x.PAG_ID == subSonicPage.PagId).Single();
            Pages.Add(page);

            LoadTabBar();
        }

        private void OnSaveTitleClick(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            Guid pageId = new Guid(linkButton.CommandArgument);
            var page = Pages.Find(x => x.PAG_ID == pageId);
            page.PAG_Title = ((TextBox)this.FindControl("TxtPageTitle_" + pageId)).Text;
            WDC.SubmitChanges();

            LoadTabBar();
        }
    }
}