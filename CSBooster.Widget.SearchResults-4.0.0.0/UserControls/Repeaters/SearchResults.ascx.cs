// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.UserControls.Repeaters
{
    public partial class SearchResults : System.Web.UI.UserControl, IReloadable, IBrowsable, IRepeater, ISettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetSearchResults");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private bool bottomPagerVisible = true;
        private bool topPagerVisible = true;
        private int pagerBreak = 4;
        private int numberItems;
        private IPager PAGTOP;
        private IPager PAGBOT;

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ((IPager)this.Pag).BrowsableControl = this;
            ((IPager)this.Pag).PageSize = QuickParameters.PageSize;
            ((IPager)this.Pag).CustomText = string.Format("<a href=\"#top\" class=\"up\">{0}</a>", language.GetString("LabelToTop"));
            ((IPager)this.Pag).PagerBreak = 4;

            LitTitle.Text = string.Format("<a name=\"Results{0}\">{1}</a>", QuickParameters.ObjectType, Helper.GetObjectName(QuickParameters.ObjectType, false));

            RestoreState();
            Reload();
            SaveState();
        }

        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
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
            if (this.QuickParameters.PageNumber != 0)
                this.PBPageNum.Value = "" + QuickParameters.PageNumber;
            this.PBSortAttr.Value = QuickParameters.SortBy.ToString();
            this.PBObjectType.Value = QuickParameters.ObjectType.ToString();
            this.PBAscCtyID.Value = QuickParameters.CommunityID.HasValue ? QuickParameters.CommunityID.Value.ToString() : string.Empty;
            this.PBAscUserID.Value = QuickParameters.UserID.HasValue ? QuickParameters.UserID.Value.ToString() : string.Empty;
            if (QuickParameters.TagID.HasValue)
                this.PBTagWord.Value = QuickParameters.TagID.ToString();
            this.PBSearchParam.Value = QuickParameters.GeneralSearch;
            this.PBUserSearchParam.Value = QuickParameters.UserSearch;
            if (QuickParameters.GeoLat.HasValue)
                this.PBGeoCoordsLat.Value = QuickParameters.GeoLat.ToString();
            if (QuickParameters.GeoLong.HasValue)
                this.PBGeoCoordsLong.Value = QuickParameters.GeoLong.ToString();
            if (QuickParameters.DistanceKm.HasValue)
                this.PBGeoRadius.Value = QuickParameters.DistanceKm.ToString();
        }

        protected void OnSearchItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            PlaceHolder placeHolder = (PlaceHolder)e.Item.FindControl("Ph");

            Control control = LoadControl(string.Format("~/UserControls/Templates/{0}", Helper.GetObjectType(dataObject.ObjectType).SearchResultCtrl));

            IDataObjectWorker dataObjectWorker = control as IDataObjectWorker;
            if (dataObjectWorker != null)
            {
                dataObjectWorker.DataObject = dataObject;
            }
            else
            {
                ((ISettings)control).Settings.Add("DataObject", dataObject);
            }
            placeHolder.Controls.Add(control);
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return this.numberItems;
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
            DataObjectList<DataObject> list = DataObjects.LoadByReflection(QuickParameters);
            this.RepRes.DataSource = list;
            numberItems = list.ItemTotal;
            this.RepRes.DataBind();

            ((IPager)this.Pag).InitPager(QuickParameters.PageNumber, numberItems);
            if (numberItems == 0)
            {
                PnlResults.Visible = false;
            }
        }
    }
}