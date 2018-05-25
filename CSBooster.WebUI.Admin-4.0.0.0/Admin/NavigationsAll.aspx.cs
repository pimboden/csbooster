// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class NavigationsAll : System.Web.UI.Page, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");
        private const int PAGESIZE = 10;
        private IPager IPagTop;
        private int numberItems = 100;
        private int currentPage = 1;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            RestoreState();
            IPagTop = PAGTOP as IPager;
            IPagTop.PageSize = PAGESIZE;
            IPagTop.BrowsableControl = this;
            IPagTop.ItemNameSingular = language.GetString("LableContentSingular");
            IPagTop.ItemNamePlural = language.GetString("LableContentPlural");
        }

        public void RestoreState()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form[htNumberItems.UniqueID]))
                {
                    numberItems = Convert.ToInt32(Request.Form[htNumberItems.UniqueID]);
                }

                if (!string.IsNullOrEmpty(Request.Form[htCurrentPage.UniqueID]))
                {
                    currentPage = Convert.ToInt32(Request.Form[htCurrentPage.UniqueID]);
                }

            }
            catch
            {
            }
        }

        private void SaveState()
        {
            htCurrentPage.Value = string.Format("{0}", currentPage);
            htNumberItems.Value = string.Format("{0}", numberItems);
        }

        public int GetNumberItems()
        {
            return numberItems;
        }

        public void SetCurrentPage(int currPage)
        {
            currentPage = currPage;
            DoSearch();
        }

        private void DoSearch()
        {
            CSBooster_DataAccessMRS csb = new CSBooster_DataAccessMRS();
            IMultipleResults results = csb.hisp_Navigation_GetNavigations(0, currentPage, PAGESIZE);

            var NavRSInfo = results.GetResult<_4screen.CSB.DataAccess.Data.CSBooster_DataAccessMRS.RecordNumerInfos>().FirstOrDefault();
            List<hitbl_NavigationStructure_NST> Navigations = results.GetResult<hitbl_NavigationStructure_NST>().ToList();

            numberItems = NavRSInfo.RowTotal;
            int checkedPage = IPagTop.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage) // Reload if the current and the checked page are different
            {
                this.currentPage = checkedPage;
                DoSearch();
            }
            else
            {
                IPagTop.InitPager(currentPage, numberItems);
                SaveState();
                if (numberItems > 0)
                {
                    RepNav.DataSource = Navigations;
                    RepNav.DataBind();
                }
                else
                {
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("NavigationsAll");
            DoSearch();
        }

        protected void OnRepNavItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                hitbl_NavigationStructure_NST nav = (hitbl_NavigationStructure_NST)e.Item.DataItem;

                Control itemOutput = e.Item;

                Literal litName = (Literal)itemOutput.FindControl("litName"); ;
                litName.Text = string.Format("<a class=\"title\" href=\"/admin/NavigationsEdit.aspx?NavID={0}&Src={1} \">{2}</a>", nav.NST_ID, Server.UrlEncode(Request.Url.PathAndQuery), nav.NST_KeyName);
               
               
                // Function icons
                PlaceHolder functions = (PlaceHolder)itemOutput.FindControl("PhFunc");

                //Tooltip with ID
                functions.Controls.Add(GetInfoTooltip(nav.NST_ID));
                Literal infoLink = new Literal();
                infoLink.Text = string.Format("<a id=\"ITTT_{0}\" class=\"icon popup\" href=\"javascript:void(0)\"></a>", nav.NST_ID);
                functions.Controls.Add(infoLink);

                //Tooltip with Generate
                LinkButton generateButton = new LinkButton();
                if (nav.NST_IsDirty)
                {
                    generateButton.CssClass = "icon navnotsynchro";
                    generateButton.ToolTip = language.GetString("TooltipNotSynchronized");
                }
                else
                {
                    generateButton.CssClass = "icon navsynchro";
                    generateButton.ToolTip = language.GetString("TooltipSynchronized");
                }
               
                generateButton.CommandArgument = nav.NST_ID.ToString();
                generateButton.Click += new EventHandler(OnGenerateButtonClick);
                functions.Controls.Add(generateButton);

                LiteralControl editLink = new LiteralControl();
                editLink.Text = string.Format("<a class=\"icon edit\" href=\"/admin/NavigationsEdit.aspx?NavID={0}&Src={1} \"></a>", nav.NST_ID, Server.UrlEncode(Request.Url.PathAndQuery));
                functions.Controls.Add(editLink);

                LinkButton deleteButton = new LinkButton();
                deleteButton.CssClass = "icon delete";
                deleteButton.ToolTip = "Löschen";
                deleteButton.CommandArgument = nav.NST_ID.ToString();
                deleteButton.Click += new EventHandler(OnDeleteButtonClick);
                functions.Controls.Add(deleteButton);
            }
        }

        private Telerik.Web.UI.RadToolTip GetInfoTooltip(Guid navID)
        {
            Telerik.Web.UI.RadToolTip tooltip = GetTooltip(string.Format(@"ITT_{0}", navID), string.Format(@"ITTT_{0}", navID));
            Literal literal = new Literal();
            literal.Text = language.GetString("TooltipInfoTitle"); ;
            tooltip.Controls.Add(literal);
            TextBox txtID = new TextBox();
            txtID.Text = navID.ToString();
            tooltip.Controls.Add(txtID);
            return tooltip;
        }


        private Telerik.Web.UI.RadToolTip GetTooltip(string tooltipId, string targetId)
        {
            Telerik.Web.UI.RadToolTip tooltip = new Telerik.Web.UI.RadToolTip();
            tooltip.Skin = "Custom";
            tooltip.EnableEmbeddedSkins = false;
            tooltip.ShowEvent = Telerik.Web.UI.ToolTipShowEvent.OnMouseOver;
            tooltip.Position = Telerik.Web.UI.ToolTipPosition.TopRight;
            tooltip.RelativeTo = Telerik.Web.UI.ToolTipRelativeDisplay.Element;
            tooltip.HideEvent = Telerik.Web.UI.ToolTipHideEvent.LeaveToolTip;
            tooltip.ID = tooltipId;
            tooltip.IsClientID = true;
            tooltip.TargetControlID = targetId;
            return tooltip;
        }

        protected void OnDeleteButtonClick(object sender, EventArgs e)
        {

            CSBooster_DataAccessMRS csb = new CSBooster_DataAccessMRS();
            csb.hisp_Navigation_DeleteNavigation(((LinkButton)sender).CommandArgument.ToGuid());
            DoSearch();
        }

        protected void OnGenerateButtonClick(object sender, EventArgs e)
        {
            string[] AllRoles = string.Format("{0},Anonymous", string.Join(",", Roles.GetAllRoles())).Split(',');
            if (htCurrentPage.Value == string.Empty)
                htCurrentPage.Value = Request.Form[htCurrentPage.UniqueID];

            foreach (string langCode in SiteConfig.NeutralLanguages.Keys)
            {
                foreach (string role in AllRoles)
                {
                    CSB.DataAccess.Business.Navigation.GeneratePreCacheNavigation(((LinkButton)sender).CommandArgument.ToGuid(), langCode, role);
                    foreach (int currNavT in Enum.GetValues(typeof(DataAccess.Business.Navigation.NavigationType)))
                    {
                        Cache.Remove(string.Format("{0}_{1}_{2}_{3}", ((LinkButton)sender).CommandArgument, langCode.ToLower(), role.ToLower(), currNavT));
                    }
                }
            }
            DoSearch();

        }
    }
}
