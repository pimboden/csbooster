// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Repeaters
{
    public partial class DataObjectLists : System.Web.UI.UserControl, IReloadable, IBrowsable, IRepeater, ISettings
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private bool bottomPagerVisible = true;
        private bool topPagerVisible = true;
        private int pagerBreak = 4;
        private int numberItems;
        private IPager pagerTop;
        private IPager pagerBottom;
        private bool autoSelectCurrentPage = true;

        public QuickParameters QuickParameters { get; set; }

        public Dictionary<string, object> Settings { get; set; }

        public bool RenderHtml { get; set; }

        public int PagerBreak
        {
            get { return pagerBreak; }
            set { pagerBreak = value; }
        }

        public bool BottomPagerVisible
        {
            get { return bottomPagerVisible; }
            set { bottomPagerVisible = value; }
        }

        public bool TopPagerVisible
        {
            get { return topPagerVisible; }
            set { topPagerVisible = value; }
        }

        public bool HasContent { get; set; }

        public string TopPagerCustomText { get; set; }

        public string BottomPagerCustomText { get; set; }

        public string Title { get; set; }

        public string ItemNameSingular { get; set; }

        public string ItemNamePlural { get; set; }

        public string OutputTemplate { get; set; }

        protected string NotItemFound
        {
            get
            {
                return string.Format(languageShared.GetString("MessageNoItemFound"), Title);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (QuickParameters.SortBy != QuickSort.Random)
            {
                Control ctrlPagTop = LoadControl("/UserControls/Pager.ascx");
                ctrlPagTop.Visible = topPagerVisible;
                pagerTop = ctrlPagTop as IPager;
                pagerTop.BrowsableControl = this;
                pagerTop.PageSize = QuickParameters.PageSize;
                pagerTop.CustomText = TopPagerCustomText;
                pagerTop.PagerBreak = pagerBreak;
                pagerTop.RenderHref = RenderHtml;
                pagerTop.ItemNameSingular = string.IsNullOrEmpty(ItemNameSingular) ? Helper.GetObjectName(QuickParameters.ObjectType, true) : ItemNameSingular;
                pagerTop.ItemNamePlural = string.IsNullOrEmpty(ItemNamePlural) ? Helper.GetObjectName(QuickParameters.ObjectType, false) : ItemNamePlural;
                PhPagTop.Controls.Add(ctrlPagTop);

                Control ctrlPagBot = LoadControl("/UserControls/Pager.ascx");
                ctrlPagBot.Visible = bottomPagerVisible;
                pagerBottom = ctrlPagBot as IPager;
                pagerBottom.CustomText = BottomPagerCustomText;
                pagerBottom.BrowsableControl = this;
                pagerBottom.PageSize = QuickParameters.PageSize;
                pagerBottom.PagerBreak = pagerBreak;
                pagerBottom.RenderHref = RenderHtml;
                pagerBottom.ItemNameSingular = string.IsNullOrEmpty(ItemNameSingular) ? Helper.GetObjectName(QuickParameters.ObjectType, true) : ItemNameSingular;
                pagerBottom.ItemNamePlural = string.IsNullOrEmpty(ItemNamePlural) ? Helper.GetObjectName(QuickParameters.ObjectType, false) : ItemNamePlural;
                PhPagBot.Controls.Add(ctrlPagBot);

                if (Settings != null && Settings.ContainsKey("SelectCurrentPage") && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    if (!string.IsNullOrEmpty(Request.Form["__EVENTTARGET"]) && (Request.Form["__EVENTTARGET"].Contains(ctrlPagTop.UniqueID) || Request.Form["__EVENTTARGET"].Contains(ctrlPagBot.UniqueID)))
                        autoSelectCurrentPage = false;
                }
                else
                {
                    autoSelectCurrentPage = false;
                }
            }
            else
            {
                IReloader reloaderTop = (IReloader)LoadControl("/UserControls/Reloader.ascx");
                ((Control)reloaderTop).Visible = topPagerVisible;
                reloaderTop.BrowsableControl = this;
                reloaderTop.FullReload = RenderHtml;
                reloaderTop.ObjectType = QuickParameters.ObjectType;
                PhPagTop.Controls.Add((Control)reloaderTop);

                IReloader reloaderBottom = (IReloader)LoadControl("/UserControls/Reloader.ascx");
                ((Control)reloaderBottom).Visible = bottomPagerVisible;
                reloaderBottom.BrowsableControl = this;
                reloaderBottom.FullReload = RenderHtml;
                reloaderBottom.ObjectType = QuickParameters.ObjectType;
                PhPagBot.Controls.Add((Control)reloaderBottom);

                autoSelectCurrentPage = false;
            }

            RestoreState();

            if ((PageType)Settings["ParentPageType"] == PageType.Overview && Settings.ContainsKey("HeadingTag") && (int)Settings["HeadingTag"] == 1)
            {
                ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderOverviewPageBreadCrumbs(QuickParameters);
            }

            Reload();
            SaveState();

            LitParams.Text = "<span style=\"display:none;\" id=\"" + Settings["WidgetInstanceId"] + "\">" + HttpUtility.HtmlEncode(QuickParameters.ToJSON()) + "</span>";
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = UniqueID + "$";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                QuickParameters.PageNumber = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSortAttr")))
                QuickParameters.SortBy = (QuickSort)Enum.Parse(typeof(QuickSort), Request.Params.Get(idPrefix + "PBSortAttr"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBObjectType")))
                QuickParameters.ObjectType = int.Parse(Request.Params.Get(idPrefix + "PBObjectType"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBAscCtyID")))
                QuickParameters.CommunityID = Request.Params.Get(idPrefix + "PBAscCtyID").ToGuid();
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBAscUserID")))
                QuickParameters.UserID = Request.Params.Get(idPrefix + "PBAscUserID").ToGuid();
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBTagWord")))
                QuickParameters.TagID = DataObjectTag.GetTagID(Request.Params.Get(idPrefix + "PBTagWord"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSearchParam")))
                QuickParameters.GeneralSearch = Request.Params.Get(idPrefix + "PBSearchParam");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBUserSearchParam")))
                QuickParameters.UserSearch = Request.Params.Get(idPrefix + "PBUserSearchParam");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGeoCoordsLat")))
                QuickParameters.GeoLat = float.Parse(Request.Params.Get(idPrefix + "PBGeoCoordsLat"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGeoCoordsLong")))
                QuickParameters.GeoLong = float.Parse(Request.Params.Get(idPrefix + "PBGeoCoordsLong"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGeoRadius")))
                QuickParameters.DistanceKm = float.Parse(Request.Params.Get(idPrefix + "PBGeoRadius"));
        }

        private void SaveState()
        {
            if (QuickParameters.PageNumber != 0)
                PBPageNum.Value = "" + QuickParameters.PageNumber;
            PBSortAttr.Value = QuickParameters.SortBy.ToString();
            PBObjectType.Value = QuickParameters.ObjectType.ToString();
            PBAscCtyID.Value = QuickParameters.CommunityID.HasValue ? QuickParameters.CommunityID.Value.ToString() : string.Empty;
            PBAscUserID.Value = QuickParameters.UserID.HasValue ? QuickParameters.UserID.Value.ToString() : string.Empty;
            if (QuickParameters.TagID.HasValue)
                PBTagWord.Value = QuickParameters.TagID.ToString();
            PBSearchParam.Value = QuickParameters.GeneralSearch;
            PBUserSearchParam.Value = QuickParameters.UserSearch;
            if (QuickParameters.GeoLat.HasValue)
                PBGeoCoordsLat.Value = QuickParameters.GeoLat.ToString();
            if (QuickParameters.GeoLong.HasValue)
                PBGeoCoordsLong.Value = QuickParameters.GeoLong.ToString();
            if (QuickParameters.DistanceKm.HasValue)
                PBGeoRadius.Value = QuickParameters.DistanceKm.ToString();
        }

        protected void OnOverviewItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");
            Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", OutputTemplate));
            ((IDataObjectWorker)ctrl).DataObject = dataObject;
            ((ISettings)ctrl).Settings = Settings;
            ph.Controls.Add(ctrl);
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return numberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            QuickParameters.PageNumber = currentPage;
            SaveState();
            Reload();
        }

        // Interface IReloadable
        public void Reload()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            if (autoSelectCurrentPage)
            {
                QuickParameters.CurrentObjectID = Request.QueryString["OID"].ToGuid();
                QuickParameters.IgnoreCache = true;
            }

            DataObjectList<DataObject> list = DataObjects.LoadByReflection(QuickParameters);
            RepObj.DataSource = list;
            numberItems = list.ItemTotal;
            if (autoSelectCurrentPage)
                QuickParameters.PageNumber = list.PageNumber;
            RepObj.DataBind();

            if (pagerTop != null)
                pagerTop.InitPager(QuickParameters.PageNumber, numberItems);
            if (pagerBottom != null)
                pagerBottom.InitPager(QuickParameters.PageNumber, numberItems);

            if (numberItems > 0)
            {
                HasContent = true;
                PnlNoItems.Visible = false;
            }
            else
            {
                HasContent = false;
            }
        }
    }
}