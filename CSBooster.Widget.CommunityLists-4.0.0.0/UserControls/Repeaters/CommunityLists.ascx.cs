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
    public partial class CommunityLists : System.Web.UI.UserControl, IReloadable, IBrowsable, IRepeater, ISettings
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private bool bottomPagerVisible = true;
        private bool topPagerVisible = true;
        private int pagerBreak = 4;
        private int numberItems;
        private IPager PAGTOP;
        private IPager PAGBOT;
        
        public QuickParameters QuickParameters{ get; set; }

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
            Control ctrlPagTop = this.LoadControl("/UserControls/Pager.ascx");
            PAGTOP = ctrlPagTop as IPager;
            PAGTOP.BrowsableControl = this;
            PAGTOP.PageSize = QuickParameters.PageSize;
            PAGTOP.CustomText = TopPagerCustomText;
            PAGTOP.PagerBreak = pagerBreak;
            PAGTOP.RenderHref = this.RenderHtml;  
            ctrlPagTop.Visible = topPagerVisible;
            
            if (string.IsNullOrEmpty(this.ItemNameSingular)) 
                PAGTOP.ItemNameSingular = Helper.GetObjectName(QuickParameters.ObjectType, true);
            else
                PAGTOP.ItemNameSingular = this.ItemNameSingular;

            if (string.IsNullOrEmpty(this.ItemNamePlural))  
                PAGTOP.ItemNamePlural = Helper.GetObjectName(QuickParameters.ObjectType, false);
            else
                PAGTOP.ItemNamePlural = this.ItemNamePlural;

            PhPagTop.Controls.Add(ctrlPagTop);

            Control ctrlPagBot = this.LoadControl("/UserControls/Pager.ascx");
            PAGBOT = ctrlPagBot as IPager;
            PAGBOT.CustomText = BottomPagerCustomText;
            ctrlPagBot.Visible = bottomPagerVisible;
            PAGBOT.BrowsableControl = this;
            PAGBOT.PageSize = QuickParameters.PageSize;
            PAGBOT.PagerBreak = pagerBreak;
            PAGBOT.RenderHref = this.RenderHtml;  

            if (string.IsNullOrEmpty(this.ItemNameSingular))
                PAGBOT.ItemNameSingular = Helper.GetObjectName(QuickParameters.ObjectType, true);
            else
                PAGBOT.ItemNameSingular = this.ItemNameSingular;

            if (string.IsNullOrEmpty(this.ItemNamePlural))
                PAGBOT.ItemNamePlural = Helper.GetObjectName(QuickParameters.ObjectType, false);
            else
                PAGBOT.ItemNamePlural = this.ItemNamePlural;

            PhPagBot.Controls.Add(ctrlPagBot);

            RestoreState();

            Reload();
            SaveState();
        }

        // Restore page state without using viewstates
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

        protected void OnOverviewItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectCommunity dataObjectUser = (DataObjectCommunity)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");

            Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", this.OutputTemplate));

            IDataObjectWorker dataObjectWorker = ctrl as IDataObjectWorker;
            if (dataObjectWorker != null)
            {
                dataObjectWorker.DataObject = dataObjectUser;
            }

            ph.Controls.Add(ctrl);
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return this.numberItems;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.QuickParameters.PageNumber = currentPage;
            SaveState();
            Reload();
        }

        // Interface IReloadable
        public void Reload()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            this.OBJOVW.DataSource = DataObjects.Load<DataObjectCommunity>(QuickParameters);
            numberItems = ((DataObjectList<DataObjectCommunity>)this.OBJOVW.DataSource).ItemTotal;
            this.OBJOVW.DataBind();
            this.PAGTOP.InitPager(QuickParameters.PageNumber, numberItems);
            this.PAGBOT.InitPager(QuickParameters.PageNumber, numberItems);
            if (numberItems > 0)
            {
                HasContent = true;
                this.NOITEMPH.Visible = false;
            }
            else
            {
                HasContent = false;
            }
        }
    }
}