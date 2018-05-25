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
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataObj.UserControls.Repeaters
{
    public partial class SurveyOverview : System.Web.UI.UserControl, IReloadable, IBrowsable, IObjectOverview, ISettings
    {
        private bool bottomPagerVisible = true;
        private bool topPagerVisible = true;
        private int itemsPerRow = 4;
        private int pagerBreak = 15;
        private int numberItems;
        private RepeatLayout repeaterLayout = RepeatLayout.Table;
        private IPager pagerTop;
        private IPager pagerBottom;

        public QuickParameters QuickParameters { get; set; }

        public bool RenderHtml { get; set; }

        public Dictionary<string, object> Settings { get; set; }

        public int PagerBreak
        {
            get { return  pagerBreak; }
            set { pagerBreak = value; }
        }

        public RepeatLayout RepeaterLayout
        {
            get { return repeaterLayout; }
            set { repeaterLayout = value; }
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

        public int ItemsPerRow
        {
            get { return itemsPerRow; }
            set { itemsPerRow = value; }
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
            Control ctrlPagTop = LoadControl("/UserControls/Pager.ascx");
            pagerTop = ctrlPagTop as IPager;
            pagerTop.BrowsableControl = this;
            pagerTop.PageSize = QuickParameters.PageSize;
            pagerTop.CustomText = TopPagerCustomText;
            pagerTop.PagerBreak = pagerBreak;
            ctrlPagTop.Visible = topPagerVisible;
            pagerTop.ItemNameSingular = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("Survey");
            pagerTop.ItemNamePlural = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("Surveys");
            PhPagTop.Controls.Add(ctrlPagTop);

            Control ctrlPagBot = LoadControl("/UserControls/Pager.ascx");
            pagerBottom = ctrlPagBot as IPager;
            pagerBottom.CustomText = BottomPagerCustomText;
            ctrlPagBot.Visible = bottomPagerVisible;
            pagerBottom.BrowsableControl = this;
            pagerBottom.PageSize = QuickParameters.PageSize;
            pagerBottom.PagerBreak = pagerBreak;
            pagerBottom.ItemNameSingular = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("Survey");
            pagerBottom.ItemNamePlural = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("Surveys");
            PhPagBot.Controls.Add(ctrlPagBot);

            if (RepeaterLayout == RepeatLayout.Table && itemsPerRow > 0)
            {
                OBJOVW.RepeatLayout = RepeaterLayout;
                OBJOVW.RepeatColumns = itemsPerRow;
                OBJOVW.ItemStyle.Width = new Unit((int)Math.Round(100.0f / itemsPerRow, 0) + "%");
            }
            else
            {
                OBJOVW.RepeatLayout = RepeatLayout.Flow;
            }


            RestoreState();
            if ((PageType)Settings["ParentPageType"] == PageType.Overview)
            {
                ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderOverviewPageBreadCrumbs(QuickParameters);
            }

            Reload();
            SaveState();
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
                this.PBTagWord.Value = QuickParameters.TagID.ToString();
            PBSearchParam.Value = QuickParameters.GeneralSearch;
            PBUserSearchParam.Value = QuickParameters.UserSearch;
            if (QuickParameters.GeoLat.HasValue)
                PBGeoCoordsLat.Value = QuickParameters.GeoLat.ToString();
            if (QuickParameters.GeoLong.HasValue)
                PBGeoCoordsLong.Value = QuickParameters.GeoLong.ToString();
            if (QuickParameters.DistanceKm.HasValue)
                PBGeoRadius.Value = QuickParameters.DistanceKm.ToString();
        }

        protected void OnOverviewItemDataBound(object sender, DataListItemEventArgs e)
        {
            Business.DataObjectSurvey dataObjectUser = (Business.DataObjectSurvey)e.Item.DataItem;
            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("PhItem");

            string templateControl = this.OutputTemplate;
            Control outControl = LoadControl(string.Format("~/UserControls/Templates/{0}", templateControl));

            IDataObjectWorker outputControl = outControl as IDataObjectWorker;
            outputControl.DataObject = dataObjectUser;

            ISettings iCtrSett = outputControl as ISettings;
            iCtrSett.Settings = this.Settings;

            ph.Controls.Add(outControl);
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
            if (!Helper.IsObjectTypeEnabled(QuickParameters.ObjectType))
                return;
            UserDataContext udc = UserDataContext.GetUserDataContext();

            QuickParameters.Udc = udc;
            QuickParameters.WithCopy = false;
            QuickParameters.OnlyConverted = true;
            QuickParameters.ShowState = ObjectShowState.Published;

            QuickParametersDataObjectSurvey qpSurvey = new QuickParametersDataObjectSurvey();
            qpSurvey.FillFromQuickParameter(QuickParameters);
            qpSurvey.IsContest = false;
            if (Settings != null && Settings.ContainsKey("AllowUrlOverride"))
            {
                bool getFromUrl = (bool)Settings["AllowUrlOverride"];
                if (getFromUrl)
                {
                    qpSurvey.IsContest = !string.IsNullOrEmpty(Request.QueryString["IsContest"])
                                                ? Convert.ToBoolean(Request.QueryString["IsContest"])
                                                : false;
                }
            }

            qpSurvey.ToStartDate = DateTime.Now;
            qpSurvey.FromEndDate = DateTime.Now;
            qpSurvey.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;

            var dataSource = DataObjects.Load<Business.DataObjectSurvey>(qpSurvey);
            OBJOVW.DataSource = dataSource;
            numberItems = dataSource.ItemTotal;
            OBJOVW.DataBind();
            pagerTop.InitPager(QuickParameters.PageNumber, numberItems);
            pagerBottom.InitPager(QuickParameters.PageNumber, numberItems);
            if ((QuickParameters.DisablePaging.HasValue && QuickParameters.DisablePaging.Value) ? dataSource.Count > 0 : numberItems > 0)
            {
                HasContent = true;
                NOITEMPH.Visible = false;
            }
            else
            {
                HasContent = false;
            }
        }
    }
}