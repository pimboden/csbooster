// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyContent : System.Web.UI.UserControl, IBrowsable, ISettings
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private int? objectType;
        private QuickSort sort;
        private QuickSortDirection sortDirection;
        private DataAccessType accessType;
        private ObjectShowState? showState;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private string communityIds = string.Empty;
        private string searchWord = string.Empty;
        private string tagList;
        private bool? showMyContentOnly;
        private int pageSize = 40;
        private int currentPage = 1;
        private int numberItems = 100;
        private int amount = 0;

        public MyContentMode MyContentMode { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        public int? ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        public string SearchWord
        {
            get { return searchWord; }
            set { searchWord = value; }
        }

        public string TagList
        {
            get { return tagList; }
            set { tagList = value; }
        }

        public string CommunityIds
        {
            get { return communityIds; }
            set { communityIds = value; }
        }

        public bool? ShowMyContentOnly
        {
            get { return showMyContentOnly; }
            set { showMyContentOnly = value; }
        }
        public QuickSort Sort
        {
            get { return sort; }
            set { sort = value; }
        }

        public QuickSortDirection SortDirection
        {
            get { return sortDirection; }
            set { sortDirection = value; }
        }

        public DataAccessType AccessType
        {
            get { return accessType; }
            set { accessType = value; }
        }

        public ObjectShowState? ShowState
        {
            get { return showState; }
            set { showState = value; }
        }

        public DateTime? DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        public DateTime? DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pager1.ItemNameSingular = language.GetString("LableContentSingular");
            pager1.ItemNamePlural = language.GetString("LableContentPlural");

            if (MyContentMode != Common.MyContentMode.Admin)
            {
                PnlTemplate.Visible = false;
            }
            if (MyContentMode == Common.MyContentMode.Related)
            {
                if (IsPostBack)
                    ScriptManager.RegisterStartupScript(upnl, upnl.GetType(), "initializeObjectRelator", "$(function() { initializeObjectRelator(); });", true);
                LitListStart.Text = "<ul id=\"dragContainer\">";
                LitListEnd.Text = "</ul>";
            }

            InitializeFromQueryString();
            RestoreState();

            pager1.PageSize = PageSize;
            pager1.BrowsableControl = this;
            if (!string.IsNullOrEmpty(Request.QueryString["CP"]))
                currentPage = Convert.ToInt32(Request.QueryString["CP"]);
            pager1.InitPager(currentPage, numberItems);

            Reload();
        }

        public void InitializeFromQueryString()
        {
            if (!objectType.HasValue)
                objectType = !String.IsNullOrEmpty(Request.QueryString["T"]) ? Helper.GetObjectType(Request.QueryString["T"]).NumericId : Helper.GetObjectType("Picture").NumericId;
            if (string.IsNullOrEmpty(communityIds))
                communityIds = Request.QueryString["C"] ?? string.Empty;
            if (string.IsNullOrEmpty(searchWord))
                searchWord = Request.QueryString["W"] ?? string.Empty;
            if (!showMyContentOnly.HasValue)
                showMyContentOnly = (!string.IsNullOrEmpty(Request.QueryString["I"]) && Request.QueryString["I"] == "true") ? true : false;
            if (string.IsNullOrEmpty(tagList))
                tagList = QuickParameters.GetDelimitedTagIds(Request.QueryString["TGL1"], ',');
        }

        private void RestoreState()
        {
            if (!string.IsNullOrEmpty(Request.Form[htObjType.UniqueID]))
                objectType = Convert.ToInt32((string)Request.Form[htObjType.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htSort.UniqueID]))
                sort = (QuickSort)Convert.ToInt32((string)Request.Form[htSort.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htSortDirection.UniqueID]))
                sortDirection = (QuickSortDirection)Convert.ToInt32((string)Request.Form[htSortDirection.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htAccesType.UniqueID]))
                accessType = (DataAccessType)Convert.ToInt32((string)Request.Form[htAccesType.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htShowState.UniqueID]))
                showState = (ObjectShowState)Convert.ToInt32((string)Request.Form[htShowState.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htNumberItems.UniqueID]))
                numberItems = Convert.ToInt32((string)Request.Form[htNumberItems.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htDateFrom.UniqueID]))
                dateFrom = Convert.ToDateTime((string)Request.Form[htDateFrom.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htDateTo.UniqueID]))
                dateTo = Convert.ToDateTime((string)Request.Form[htDateTo.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htCurrentPage.UniqueID]))
                currentPage = Convert.ToInt32((string)Request.Form[htCurrentPage.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htSearchWord.UniqueID]))
                searchWord = Request.Form[htSearchWord.UniqueID];
            if (!string.IsNullOrEmpty(Request.Form[htMyCntOnly.UniqueID]))
                showMyContentOnly = bool.Parse(Request.Form[htMyCntOnly.UniqueID]);
            if (!string.IsNullOrEmpty(Request.Form[htCommunityId.UniqueID]))
                communityIds = Request.Form[htCommunityId.UniqueID];
            if (!string.IsNullOrEmpty(Request.Form[htTagId.UniqueID]))
                tagList = Request.Form[htTagId.UniqueID];
        }

        private void SaveState()
        {
            htObjType.Value = string.Format("{0}", (int)objectType);
            htMyCntOnly.Value = string.Format("{0}", showMyContentOnly);
            htCommunityId.Value = string.Format("{0}", communityIds);
            htTagId.Value = string.Format("{0}", tagList);
            htSort.Value = string.Format("{0}", (int)sort);
            htSortDirection.Value = string.Format("{0}", (int)sortDirection);
            htAccesType.Value = string.Format("{0}", (int)accessType);
            htShowState.Value = string.Format("{0}", showState.HasValue ? Convert.ToString((int)showState.Value) : string.Empty);
            htDateFrom.Value = string.Format("{0}", dateFrom.HasValue ? dateFrom.Value.ToShortDateString() : string.Empty);
            htDateTo.Value = string.Format("{0}", dateTo.HasValue ? dateTo.Value.ToShortDateString() : string.Empty);
            htSearchWord.Value = string.Format("{0}", !string.IsNullOrEmpty(searchWord) ? searchWord : string.Empty);
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

            Reload();
        }

        public void Refresh()
        {
            pager1.ItemNameSingular = Helper.GetObjectName(ObjectType.Value, true);
            pager1.ItemNamePlural = Helper.GetObjectName(ObjectType.Value, false);

            upnl.Update();
        }

        public void Reload()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            DataObjectList<DataObject> qobjects = new DataObjectList<DataObject>();

            bool searchInCommunitiesValid = true;

            if (!SecuritySection.CachedInstance.CurrentUserHasAccess("MyContentSearchAllCommunities"))
            {
                DataObjectList<DataObject> ownerCommunities = DataObjects.Load<DataObject>(new QuickParameters { QuerySourceType = QuerySourceType.MyContent, Udc = UserDataContext.GetUserDataContext(), ShowState = null, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = true, IsCreator = null } });
                ownerCommunities.AddRange(DataObjects.Load<DataObject>(new QuickParameters { QuerySourceType = QuerySourceType.MyContent, Udc = UserDataContext.GetUserDataContext(), ShowState = ObjectShowState.InProgress, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = null, IsCreator = true } }));
                ownerCommunities.AddRange(DataObjects.Load<DataObject>(new QuickParameters { QuerySourceType = QuerySourceType.MyContent, Udc = UserDataContext.GetUserDataContext(), ShowState = ObjectShowState.ConversionFailed, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = null, IsCreator = true } }));
                DataObjectList<DataObject> allCommunities = DataObjects.Load<DataObject>(new QuickParameters { QuerySourceType = QuerySourceType.MyContent, Udc = UserDataContext.GetUserDataContext(), ShowState = ObjectShowState.Published, IgnoreCache = true, MembershipParams = new MembershipParams { UserID = UserProfile.Current.UserId, IsOwner = false, IsCreator = null } });

                allCommunities.Add(DataObject.Load<DataObject>(UserProfile.Current.ProfileCommunityID, null, true));
                allCommunities.AddRange(ownerCommunities);

                if (string.IsNullOrEmpty(communityIds) && !showMyContentOnly.Value)
                {
                    communityIds = string.Join("|", ownerCommunities.Select(x => x.CommunityID.ToString()).ToArray());
                    if (string.IsNullOrEmpty(communityIds))
                        searchInCommunitiesValid = false;
                }
                else
                {
                    communityIds = QuickParameters.GetDelimitedCommunityIDs(communityIds, Constants.TAG_DELIMITER);
                    string[] searchInCommunities = communityIds.Trim('|').Split('|');

                    foreach (string searchInCommunity in searchInCommunities)
                    {
                        if (showMyContentOnly.Value)
                        {
                            if (!string.IsNullOrEmpty(searchInCommunity) && !allCommunities.Exists(c => c.CommunityID == searchInCommunity.ToGuid()))
                            {
                                searchInCommunitiesValid = false;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(searchInCommunity) && !ownerCommunities.Exists(c => c.CommunityID == searchInCommunity.ToGuid()))
                            {
                                searchInCommunitiesValid = false;
                            }
                        }
                    }
                }
            }
            else
            {
                communityIds = communityIds.Replace(Constants.TAG_DELIMITER.ToString(), "|");
            }

            if (SecuritySection.CachedInstance.CurrentUserHasAccess("MyContentSearchAllCommunities") || searchInCommunitiesValid == true)
            {
                qobjects = DataObjects.Load<DataObject>(new QuickParameters()
                                                            {
                                                                QuerySourceType = QuerySourceType.MyContent,
                                                                Udc = udc,
                                                                ObjectType = objectType.Value,
                                                                ObjectID = searchWord.IsGuid() ? searchWord.ToGuid() : (Guid?)null,
                                                                CommunityID = null,
                                                                Communities = communityIds,
                                                                Tags1 = tagList,
                                                                UserID = showMyContentOnly.HasValue && showMyContentOnly.Value ? UserProfile.Current.UserId : (Guid?)null,
                                                                SortBy = sort,
                                                                Direction = sortDirection,
                                                                PageNumber = currentPage,
                                                                PageSize = PageSize,
                                                                ShowState = showState,
                                                                FromInserted = dateFrom,
                                                                ToInserted = dateTo,
                                                                GeneralSearch = searchWord,
                                                                IgnoreCache = true,
                                                                WithCopy = true,
                                                                Amount = amount,
                                                                OnlyConverted = false
                                                            });
            }

            numberItems = qobjects.ItemTotal;
            pager1.InitPager(currentPage, numberItems);
            SaveState();

            contentRepeater.DataSource = qobjects;
            contentRepeater.DataBind();
        }

        protected void OnContentItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObject dataObject = (DataObject)e.Item.DataItem;
            Control itemOutput = null;

            if (MyContentMode == Common.MyContentMode.Selection)
            {
                itemOutput = this.LoadControl("~/UserControls/Dashboard/MyContentSelectionBox.ascx");
                ((MyContentSelectionBox)itemOutput).DataObject = dataObject;
                ((MyContentSelectionBox)itemOutput).Settings = Settings;
            }
            else if (MyContentMode == Common.MyContentMode.Widgets)
            {
                itemOutput = this.LoadControl("~/UserControls/Dashboard/MyContentWidgetBox.ascx");
                ((MyContentWidgetBox)itemOutput).DataObject = dataObject;
                ((MyContentWidgetBox)itemOutput).Settings = Settings;
            }
            else if (MyContentMode == Common.MyContentMode.Related)
            {
                itemOutput = this.LoadControl("~/UserControls/Dashboard/MyContentRelatedBox.ascx");
                ((MyContentRelatedBox)itemOutput).DataObject = dataObject;
                ((MyContentRelatedBox)itemOutput).Settings = Settings;
                ((MyContentRelatedBox)itemOutput).IsSource = true;
            }
            else
            {
                if (UserProfile.Current.MyContentStyle == "Table" || UserProfile.Current.MyContentStyle == "")
                {
                    itemOutput = this.LoadControl("~/UserControls/Dashboard/MyContentItemRow.ascx");
                    ((MyContentItemRow)itemOutput).DataObject = dataObject;
                    ((MyContentItemRow)itemOutput).DoSearch = Reload;
                    LbtnStyle1.CssClass = "active";
                    LbtnStyle2.CssClass = "";
                }
                else
                {
                    itemOutput = this.LoadControl("~/UserControls/Dashboard/MyContentItemBox.ascx");
                    ((MyContentItemRow)itemOutput).DataObject = dataObject;
                    ((MyContentItemRow)itemOutput).DoSearch = Reload;
                    LbtnStyle1.CssClass = "";
                    LbtnStyle2.CssClass = "active";
                }
            }

            e.Item.FindControl("PhItem").Controls.Add(itemOutput);
        }

        protected void OnSelectStyleClick(object sender, EventArgs e)
        {
            UserProfile.Current.MyContentStyle = ((LinkButton)sender).CommandArgument;
            UserProfile.Current.Save();

            Reload();
        }
    }
}