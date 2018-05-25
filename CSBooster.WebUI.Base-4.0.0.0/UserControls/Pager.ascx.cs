//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		06.09.2007 / AW
//******************************************************************************

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using System.Collections.Generic;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Pager : System.Web.UI.UserControl, IPager
    {
        private GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private int pageSize;
        private string itemNameSingular;
        private string itemNamePlural;
        private string customText;
        private Control browsableControl;
        private int pagerBreak = 4;
        private string buttonNextUrlActive = "/Library/Images/Layout/cmd_next.png";
        private string buttonLastUrlActive = "/Library/Images/Layout/cmd_last.png";
        private string buttonPrevUrlActive = "/Library/Images/Layout/cmd_prev.png";
        private string buttonFirstUrlActive = "/Library/Images/Layout/cmd_first.png";
        private string buttonNextUrlInactive = "/Library/Images/Layout/cmd_next_i.png";
        private string buttonLastUrlInactive = "/Library/Images/Layout/cmd_last_i.png";
        private string buttonPrevUrlInactive = "/Library/Images/Layout/cmd_prev_i.png";
        private string buttonFirstUrlInactive = "/Library/Images/Layout/cmd_first_i.png";
        private string cssClassPager = "pager";
        private string cssClassButtons = "pagerButtons";
        private string cssClassGoToActive = "pagerGoToActive";
        private string cssClassGoToInactive = "pagerGoToInactive";
        private bool renderHref = false;

        public bool RenderHref
        {
            get { return renderHref; }
            set { renderHref = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public string ItemNameSingular
        {
            get { return itemNameSingular; }
            set { itemNameSingular = value; }
        }

        public string ItemNamePlural
        {
            get { return itemNamePlural; }
            set { itemNamePlural = value; }
        }

        public string CustomText
        {
            get { return customText; }
            set { customText = value; }
        }

        public Control BrowsableControl
        {
            get { return browsableControl; }
            set { browsableControl = value; }
        }

        public int PagerBreak
        {
            get { return pagerBreak; }
            set { pagerBreak = value; }
        }

        public string ButtonNextUrlActive
        {
            get { return buttonNextUrlActive; }
            set { buttonNextUrlActive = value; }
        }

        public string ButtonLastUrlActive
        {
            get { return buttonLastUrlActive; }
            set { buttonLastUrlActive = value; }
        }

        public string ButtonPrevUrlActive
        {
            get { return buttonPrevUrlActive; }
            set { buttonPrevUrlActive = value; }
        }

        public string ButtonFirstUrlActive
        {
            get { return buttonFirstUrlActive; }
            set { buttonFirstUrlActive = value; }
        }

        public string ButtonNextUrlInactive
        {
            get { return buttonNextUrlInactive; }
            set { buttonNextUrlInactive = value; }
        }

        public string ButtonLastUrlInactive
        {
            get { return buttonLastUrlInactive; }
            set { buttonLastUrlInactive = value; }
        }

        public string ButtonPrevUrlInactive
        {
            get { return buttonPrevUrlInactive; }
            set { buttonPrevUrlInactive = value; }
        }

        public string ButtonFirstUrlInactive
        {
            get { return buttonFirstUrlInactive; }
            set { buttonFirstUrlInactive = value; }
        }

        public string CssClassPager
        {
            get { return cssClassPager; }
            set { cssClassPager = value; }
        }

        public string CssClassButtons
        {
            get { return cssClassButtons; }
            set { cssClassButtons = value; }
        }

        public string CssClassGoToActive
        {
            get { return cssClassGoToActive; }
            set { cssClassGoToActive = value; }
        }

        public string CssClassGoToInactive
        {
            get { return cssClassGoToInactive; }
            set { cssClassGoToInactive = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Pag.CssClass = cssClassPager;
            this.GoToFirst.CssClass = cssClassButtons;
            this.GoToLast.CssClass = cssClassButtons;
            this.GoToNext.CssClass = cssClassButtons;
            this.GoToPrev.CssClass = cssClassButtons;

            this.LnkFirst.CssClass = cssClassButtons;
            this.LnkLast.CssClass = cssClassButtons;
            this.LnkNext.CssClass = cssClassButtons;
            this.LnkPrev.CssClass = cssClassButtons;

            ImgFirst.ID = null;
            ImgNext.ID = null;
            ImgPrev.ID = null;
            ImgLast.ID = null;

            if (this.RenderHref)
                DoCheckPage();
        }


        protected void OnChangePageClick(object sender, EventArgs e)
        {
            int numberItems = 0;
            if (browsableControl == null)
                numberItems = ((IBrowsable)this.Page).GetNumberItems();
            else
                numberItems = ((IBrowsable)browsableControl).GetNumberItems();
            int currentPage;
            if (sender is ImageButton)
                currentPage = int.Parse(((ImageButton)sender).CommandArgument);
            else
                currentPage = int.Parse(((LinkButton)sender).CommandArgument);
            this.InitPager(currentPage, numberItems);
            if (browsableControl == null)
                ((IBrowsable)this.Page).SetCurrentPage(currentPage);
            else
                ((IBrowsable)browsableControl).SetCurrentPage(currentPage);
        }

        public int CheckPageRange(int currentPage, int numberItems)
        {
            int lastPage = (int)Math.Ceiling((double)numberItems / pageSize);
            return Math.Min(currentPage, lastPage) > 0 ? Math.Min(currentPage, lastPage) : 1;
        }

        public void InitPager(int currentPage, int numberItems)
        {
            this.Ctrl.Visible = true;

            if (this.RenderHref)
                InitLinkPager(currentPage, numberItems);
            else
                InitButtonPager(currentPage, numberItems);
        }

        private void DoCheckPage()
        {
            int numberItems = 0;
            if (browsableControl == null)
                numberItems = ((IBrowsable)this.Page).GetNumberItems();
            else
                numberItems = ((IBrowsable)browsableControl).GetNumberItems();

            int currentPage = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["PN"]))
                currentPage = Convert.ToInt32(Request.QueryString["PN"]);
            else
                currentPage = 1;

            InitLinkPager(currentPage, numberItems);
            this.InitLinkPager(currentPage, numberItems);
            if (browsableControl == null)
                ((IBrowsable)this.Page).SetCurrentPage(currentPage);
            else
                ((IBrowsable)browsableControl).SetCurrentPage(currentPage);
        }

        private void InitLinkPager(int currentPage, int numberItems)
        {
            this.GoTo.Visible = false;
            this.GoToFirst.Visible = false;
            this.GoToPrev.Visible = false;
            this.GoToNext.Visible = false;
            this.GoToLast.Visible = false;

            string queryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "PN", "COID" }, true);
            if (!string.IsNullOrEmpty(queryString))
                queryString += "&";

            int lastPage = (int)Math.Ceiling((double)numberItems / pageSize);
            int itemFrom = ((currentPage - 1) * pageSize) + 1;
            int itemTo = itemFrom + pageSize - 1;

            if (string.IsNullOrEmpty(customText))
            {
                if (numberItems == 1)
                    this.LitText.Text = numberItems + " " + itemNameSingular;
                else if (numberItems > 1 && numberItems <= itemTo && currentPage == 1)
                    this.LitText.Text = numberItems + " " + itemNamePlural;
                else if (numberItems > 1)
                    this.LitText.Text = itemFrom + "-" + Math.Min(itemTo, numberItems) + string.Format(" {0} ", GuiLanguage.GetGuiLanguage("Shared").GetString("TextOf")) + numberItems + " " + itemNamePlural;
                else
                    this.LitText.Text = " ";
            }
            else
            {
                this.LitText.Text = customText;
            }

            if (numberItems <= pageSize)
            {
                this.Ctrl.Visible = false;
                if (customText == " " || string.IsNullOrEmpty(this.LitText.Text))
                    this.Visible = false;
            }

            if (currentPage == 1 || numberItems == 0)
            {
                this.LnkPrev.Enabled = false;
                this.ImgPrev.ImageUrl = buttonPrevUrlInactive;
                this.LnkFirst.Enabled = false;
                this.ImgFirst.ImageUrl = buttonFirstUrlInactive;
            }
            else
            {
                this.LnkPrev.Enabled = true;
                this.ImgPrev.ImageUrl = buttonPrevUrlActive;
                this.LnkFirst.Enabled = true;
                this.ImgFirst.ImageUrl = buttonFirstUrlActive;
            }
            if (currentPage == lastPage || numberItems == 0)
            {
                this.LnkNext.Enabled = false;
                this.ImgNext.ImageUrl = buttonNextUrlInactive;
                this.LnkLast.Enabled = false;
                this.ImgLast.ImageUrl = buttonLastUrlInactive;
            }
            else
            {
                this.LnkNext.Enabled = true;
                this.ImgNext.ImageUrl = buttonNextUrlActive;
                this.LnkLast.Enabled = true;
                this.ImgLast.ImageUrl = buttonLastUrlActive;
            }

            if (currentPage > 1)
            {
                this.LnkPrev.NavigateUrl = string.Format("{0}?{1}PN={2}", Request.GetRawPath(), queryString, currentPage - 1);
                this.LnkFirst.NavigateUrl = string.Format("{0}?{1}PN={2}", Request.GetRawPath(), queryString, 1);
            }
            if (currentPage < lastPage)
            {
                this.LnkNext.NavigateUrl = string.Format("{0}?{1}PN={2}", Request.GetRawPath(), queryString, currentPage + 1);
                this.LnkLast.NavigateUrl = string.Format("{0}?{1}PN={2}", Request.GetRawPath(), queryString, lastPage);
            }

            LnkFirst.Attributes.Add("rel", "nofollow");
            LnkPrev.Attributes.Add("rel", "nofollow");
            LnkNext.Attributes.Add("rel", "nofollow");
            LnkLast.Attributes.Add("rel", "nofollow");

            if (LnkFirst.Enabled)
            {
                LnkFirst.ToolTip = languageShared.GetString("LablePagerFirst");
            }

            if (LnkPrev.Enabled)
            {
                LnkPrev.ToolTip = languageShared.GetString("LablePagerPrev");
            }

            if (LnkNext.Enabled)
            {
                LnkNext.ToolTip = languageShared.GetString("LablePagerNext");
            }

            if (LnkLast.Enabled)
            {
                LnkLast.ToolTip = languageShared.GetString("LablePagerLast");
            }

            this.LnkGoTo.Controls.Clear();

            int fromPage = 1;
            int toPage = lastPage;

            if (currentPage - pagerBreak >= 1)
                fromPage = currentPage - pagerBreak;
            if (currentPage + pagerBreak <= lastPage)
                toPage = currentPage + pagerBreak;

            for (int i = fromPage; i <= toPage; i++)
            {
                HyperLink gotoLink = new HyperLink();

                gotoLink.Text = i.ToString();

                if (i == currentPage)
                {
                    gotoLink.NavigateUrl = "#";
                    gotoLink.Enabled = false;
                    gotoLink.CssClass = cssClassGoToInactive;
                }
                else
                {
                    gotoLink.NavigateUrl = string.Format("{0}?{1}PN={2}", Request.GetRawPath(), queryString, i);
                    gotoLink.CssClass = cssClassGoToActive;
                    gotoLink.ToolTip = string.Format(languageShared.GetString("LablePagerThis"), i);
                }
                this.LnkGoTo.Controls.Add(gotoLink);
            }
        }

        private void InitButtonPager(int currentPage, int numberItems)
        {
            this.LnkGoTo.Visible = false;
            this.LnkFirst.Visible = false;
            this.LnkPrev.Visible = false;
            this.LnkNext.Visible = false;
            this.LnkLast.Visible = false;

            int lastPage = (int)Math.Ceiling((double)numberItems / pageSize);
            int itemFrom = ((currentPage - 1) * pageSize) + 1;
            int itemTo = itemFrom + pageSize - 1;

            if (string.IsNullOrEmpty(customText))
            {
                if (numberItems == 1)
                    this.LitText.Text = numberItems + " " + itemNameSingular;
                else if (numberItems > 1 && numberItems <= itemTo && currentPage == 1)
                    this.LitText.Text = numberItems + " " + itemNamePlural;
                else if (numberItems > 1)
                    this.LitText.Text = itemFrom + "-" + Math.Min(itemTo, numberItems) + string.Format(" {0} ", GuiLanguage.GetGuiLanguage("Shared").GetString("TextOf")) + numberItems + " " + itemNamePlural;
                else
                    this.LitText.Text = " ";
            }
            else
            {
                this.LitText.Text = customText;
            }

            if (numberItems <= pageSize)
            {
                this.Ctrl.Visible = false;
                if (customText == " " || string.IsNullOrEmpty(this.LitText.Text))
                    this.Visible = false;
            }

            if (currentPage == 1 || numberItems == 0)
            {
                this.GoToPrev.Enabled = false;
                this.Prev.ImageUrl = buttonPrevUrlInactive;
                this.GoToFirst.Enabled = false;
                this.First.ImageUrl = buttonFirstUrlInactive;
            }
            else
            {
                this.GoToPrev.Enabled = true;
                this.Prev.ImageUrl = buttonPrevUrlActive;
                this.GoToFirst.Enabled = true;
                this.First.ImageUrl = buttonFirstUrlActive;
            }
            if (currentPage == lastPage || numberItems == 0)
            {
                this.GoToNext.Enabled = false;
                this.Next.ImageUrl = buttonNextUrlInactive;
                this.GoToLast.Enabled = false;
                this.Last.ImageUrl = buttonLastUrlInactive;
            }
            else
            {
                this.GoToNext.Enabled = true;
                this.Next.ImageUrl = buttonNextUrlActive;
                this.GoToLast.Enabled = true;
                this.Last.ImageUrl = buttonLastUrlActive;
            }

            if (currentPage > 1)
            {
                this.GoToPrev.CommandArgument = "" + (currentPage - 1);
                this.GoToFirst.CommandArgument = "" + 1;
            }
            if (currentPage < lastPage)
            {
                this.GoToNext.CommandArgument = "" + (currentPage + 1);
                this.GoToLast.CommandArgument = "" + lastPage;
            }

            if (GoToFirst.Enabled)
                GoToFirst.ToolTip = languageShared.GetString("LablePagerFirst");

            if (GoToPrev.Enabled)
                GoToPrev.ToolTip = languageShared.GetString("LablePagerPrev");

            if (GoToNext.Enabled)
                GoToNext.ToolTip = languageShared.GetString("LablePagerNext");

            if (GoToLast.Enabled)
                GoToLast.ToolTip = languageShared.GetString("LablePagerLast");

            this.GoTo.Controls.Clear();

            int fromPage = 1;
            int toPage = lastPage;

            if (currentPage - pagerBreak >= 1)
                fromPage = currentPage - pagerBreak;
            if (currentPage + pagerBreak <= lastPage)
                toPage = currentPage + pagerBreak;

            for (int i = fromPage; i <= toPage; i++)
            {
                LinkButton gotoPageButton = new LinkButton();
                gotoPageButton.ID = "GoTo" + i;
                gotoPageButton.Text = i.ToString();
                if (i == currentPage)
                {
                    gotoPageButton.Enabled = false;
                    gotoPageButton.CssClass = cssClassGoToInactive;
                }
                else
                {
                    gotoPageButton.CommandArgument = "" + i;
                    gotoPageButton.Click += new EventHandler(OnChangePageClick);
                    gotoPageButton.CssClass = cssClassGoToActive;
                    gotoPageButton.ToolTip = string.Format(languageShared.GetString("LablePagerThis"), i);
                }
                this.GoTo.Controls.Add(gotoPageButton);
            }
        }
    }
}