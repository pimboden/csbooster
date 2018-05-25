// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyFavorites : System.Web.UI.UserControl, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private UserDataContext udc = UserDataContext.GetUserDataContext();
        private int pageSize = 20;
        private int currentPage = 1;
        private int numberItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMember();
            RestoreState();
            Reload();
            SaveState();
        }

        protected override void OnInit(EventArgs e)
        {
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
            string idPrefixAlt = this.ClientID + "_";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
        }

        private void ClearState()
        {
        }

        private void InitMember()
        {
            pager1.ItemNameSingular = language.GetString("LableFavoritesSingular");
            pager1.ItemNamePlural = language.GetString("LableFavoritesPlural");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = language.GetString("LableFavoritesSingular");
            pager2.ItemNamePlural = language.GetString("LableFavoritesPlural");
            pager2.BrowsableControl = this;
        }

        protected void OnFavoriteItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject item = (DataObject)e.Item.DataItem;

            Literal lit = e.Item.FindControl("litTit") as Literal;
            lit.Text = string.Format("<a href='{0}'>{1}</>", Helper.GetDetailLink(item.ObjectType, item.ObjectID.ToString()), item.Title);

            lit = e.Item.FindControl("LitUser") as Literal;
            lit.Text = string.Format("<a href='{0}{1}'>{1}</>", Constants.Links["NICE_LINK_TO_USER_DETAIL"], item.Nickname);

            lit = e.Item.FindControl("litTyp") as Literal;
            lit.Text = Helper.GetObjectName(item.ObjectType, true);

            PlaceHolder ph = e.Item.FindControl("phDel") as PlaceHolder;
            LinkButton btnDel = new LinkButton();
            btnDel.CommandArgument = item.ObjectID.ToString();
            btnDel.ToolTip = language.GetString("TooltipFavoritDelete");
            btnDel.CssClass = "favoriteDeleteButton"; 
            btnDel.Attributes.Add("onclick", string.Format("javascript: return confirm('{0}');", language.GetString("TextFavoritDeleteConfirm").StripForScript()));
            btnDel.Click += new EventHandler(btnDel_Click);

            ph.Controls.Add(btnDel);  
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
            {
                string strId = btn.CommandArgument;
                DataObject.RemoveFromFavorite(udc, strId.ToGuid());    
                //DataObject.RelDelete(new RelationParams() { Udc = udc, ChildObjectID = strId.ToGuid(), ParentObjectID = udc.UserID, RelationType = "Favorites" });
            }
            this.Reload();
        }

        public int GetNumberItems()
        {
            return this.numberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
            SaveState();
            Reload();
        }

        // Interface IReloadable
        public void Reload()
        {
            int checkedPage = 0;
            DataObjectList<DataObject> list = DataObjects.Load<DataObject>(new QuickParameters() { Udc = udc, PageNumber = currentPage, PageSize = pageSize, Amount = 0, RelationParams = new RelationParams() { Udc = udc, ParentObjectID = udc.UserID, SortType = RelationSortType.Child, RelationType = "Favorites" } });
            this.favRepeater.DataSource = list;
            this.favRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            checkedPage = this.pager1.CheckPageRange(this.currentPage, list.ItemTotal);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                list = DataObjects.Load<DataObject>(new QuickParameters() { IgnoreCache = true, Udc = udc, PageNumber = currentPage, PageSize = pageSize, Amount = 0, RelationParams = new RelationParams() { Udc = udc, ParentObjectID = udc.UserID, SortType = RelationSortType.Child, RelationType = "Favorites" } });
                this.favRepeater.DataSource = list;
                this.favRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, list.ItemTotal);
            this.pager2.InitPager(currentPage, list.ItemTotal);

            if (list.ItemTotal > 0)
                this.noitem.Visible = false;

        }
    }
}