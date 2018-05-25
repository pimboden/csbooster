// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using OboutInc.ImageZoom;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Admin.UserControls
{
    public partial class SingleObjectSelection : System.Web.UI.UserControl, IBrowsable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        private string _strCommunityId;
        private string _SearchWord;
        private Guid? _UserId;
        private int currentPage = 1;
        private IPager IPagTop;
        private int pageSize = 50;
        private int numberItems = 100;
        private int Amount = 0;


        public int? ObjType { get; set; }
        public string CurrentSelected;
        public string UrlTextBoxId;
        //private Dictionary<int, string> featuredValues;

        public void StartSearch()
        {
            InitPage();
            DoSearch();
        }

        private void InitPage()
        {
            IPagTop = this.PAGTOP as IPager;
            RestoreState();
            IPagTop.PageSize = pageSize;
            IPagTop.BrowsableControl = this;
            IPagTop.ItemNameSingular = Helper.GetObjectName(ObjType.Value, true);
            IPagTop.ItemNamePlural = Helper.GetObjectName(ObjType.Value, false);
            //featuredValues = DataAccessConfiguration.LoadObjectFeaturedValues();
            this.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (ObjType.HasValue)
            {
                InitPage();
            }
            else
            {
                this.Visible = false;
            }

        }

        protected void lbtnSR_Click(object sender, EventArgs e)
        {
            DoSearch();
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

                if (!string.IsNullOrEmpty(Request.Form[htSearchWord.UniqueID]))
                {
                    _SearchWord = Request.Form[htSearchWord.UniqueID];
                }

                if (!string.IsNullOrEmpty(Request.Form[htUserId.UniqueID]))
                {
                    _UserId = Request.Form[htUserId.UniqueID].ToGuid();
                }
                else
                {
                    if (Roles.IsUserInRole("Admin"))
                    {
                        _UserId = null;
                    }
                }
            }
            catch
            {
            }
        }

        private void SaveState()
        {
            htObjType.Value = string.Format("{0}", (int)ObjType);
            htUserId.Value = string.Format("{0}", _UserId.HasValue ? _UserId.Value.ToString():string.Empty);
            htSearchWord.Value = string.Format("{0}", !string.IsNullOrEmpty(_SearchWord) ? _SearchWord : string.Empty);
            htCurrentPage.Value = string.Format("{0}", currentPage);
            htNumberItems.Value = string.Format("{0}", numberItems);
        }


        public void DoSearch()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            DataObjectList<DataObject> qobjects = new DataObjectList<DataObject>();

            _strCommunityId = string.Empty;
            _SearchWord = txtSR.Text;
            //Alway check for all content in my communities
            _UserId = null;
            DataObjectList<DataObject> ownerCommunities = DataObjects.Load<DataObject>(
new QuickParameters
{
    Udc = UserDataContext.GetUserDataContext(),
    ShowState = null,
    IgnoreCache = true,
    MembershipParams = new MembershipParams
    {
        UserID = UserProfile.Current.UserId,
        IsOwner = true,
        IsCreator = null
    }
});

            DataObjectList<DataObject> allCommunities = DataObjects.Load<DataObject>(
new QuickParameters
{
    Udc = UserDataContext.GetUserDataContext(),
    ShowState = ObjectShowState.Published,
    IgnoreCache = true,
    MembershipParams = new MembershipParams
    {
        UserID = UserProfile.Current.UserId,
        IsOwner = false,
        IsCreator = null
    }
});

            allCommunities.Add(DataObject.Load<DataObject>(UserProfile.Current.ProfileCommunityID, null, true));
            allCommunities.AddRange(ownerCommunities);

           

            Guid? OID = null;
            string SW = string.Empty;
            if (_SearchWord.IsGuid())
            {
                OID = _SearchWord.ToGuid();
            }
            else
            {
                SW = _SearchWord;
            }
            qobjects = DataObjects.Load <DataObject>(new QuickParameters()
            {
                Udc = udc,
                ObjectType = ObjType.Value,
                ObjectID = OID,
                CommunityID = null,
                Communities = _strCommunityId,
                UserID = _UserId,
                SortBy = QuickSort.InsertedDate,
                Direction = QuickSortDirection.Desc,
                PageNumber = currentPage,
                PageSize = 20,
                ShowState = ObjectShowState.Published,
                GeneralSearch = SW,
                IgnoreCache = true,
                WithCopy = true,
                Amount = Amount,
                OnlyConverted = true
            });


            numberItems = qobjects.ItemTotal;

            int checkedPage = IPagTop.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage) // Reload if the current and the checked page are different
            {
                this.currentPage = checkedPage;
                DoSearch();
            }

            IPagTop.InitPager(currentPage, numberItems);
            SaveState();
            if (numberItems > 0)
            {
                pnlSelection.Visible = true;
                lblNoData.Visible = false;
                RepObj.DataSource = qobjects;
                RepObj.DataBind();
            }
            else
            {
                pnlSelection.Visible = false;
                lblNoData.Visible = true;
                lblNoData.Text = string.Format(language.GetString("MessageNoObjectsFound"), IPagTop.ItemNamePlural);
            }
        }

        protected void OnRepObjItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataObject quickObject = (DataObject)e.Item.DataItem;

                Control itemOutput = e.Item;

                Panel pnlItem = (Panel)e.Item.FindControl("pnlItem");

                ImageZoom IZ1 = (ImageZoom)itemOutput.FindControl("IZ1");
              
                IZ1.ImageUrl = SiteConfig.MediaDomainName + quickObject.GetImage(PictureVersion.XS);
                IZ1.BigImageUrl = SiteConfig.MediaDomainName + quickObject.GetImage(PictureVersion.L);
                IZ1.Description = quickObject.Title;
                IZ1.StyleFolder = "/library/OboutControls/ImageZoom/styles/simple";

                string DetailLink = Helper.GetDetailLink(quickObject.ObjectType, quickObject.ObjectID.Value.ToString());

                Literal litItemPicker = (Literal)itemOutput.FindControl("litItemPicker");
                if (CurrentSelected.ToLower() == DetailLink.ToLower())
                {
                    pnlItem.CssClass = "CSB_admin_cnt_item CSB_admin_cnt_item_selected CSB_admin_cnt_item_row";
                    litItemPicker.Text = "<a href=\"javascript:void(0)\" class=\"picked\"></a>";

                }
                else
                {
                    litItemPicker.Text = string.Format("<a href=\"javascript:void(0)\" class=\"notpicked\" onclick=\"javascript:document.getElementById('{0}').value='{1}'\"></a>", UrlTextBoxId, DetailLink);
                }

                HyperLink link2 = (HyperLink)itemOutput.FindControl("LnkDet2");
                link2.NavigateUrl =  DetailLink;
                link2.Text = quickObject.Title.StripHTMLTags();
                link2.Target = "_blank";
                link2.ID = null;

                HyperLink authorLink = (HyperLink)itemOutput.FindControl("LnkAuthor");
                authorLink.NavigateUrl = Helper.GetDetailLink("User", quickObject.Nickname);
                authorLink.Text = quickObject.Nickname;
                authorLink.Target = "_blank";
                authorLink.ID = null;

                DataObject dataObject= DataObject.Load<DataObject>(quickObject.CommunityID, null, false);
                string communityCssClass = string.Empty;
                string communityTooltip = string.Empty;
                if (dataObject.State != ObjectState.Added)
                {
                    string communityName = string.Empty;
                    string communityUrl = string.Empty;
                    if (dataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                    {
                        DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(quickObject.CommunityID, null, false);
                        communityName = community.Title;
                        communityUrl = string.Format("{0}",Helper.GetDetailLink("Community", community.VirtualURL));
                        if (!community.Managed)
                        {
                            communityCssClass = "cty";
                            communityTooltip = language.GetString("TootltipCommunity");
                        }
                        else
                        {
                            communityCssClass = "mcty";
                            communityTooltip = language.GetString("TootltipCommunityManaged");
                        }
                    }
                    else
                    {
                        communityName = language.GetString("LableProfile");
                        communityUrl = string.Format("{0}",  Helper.GetDetailLink("User", dataObject.Nickname));
                        communityCssClass = "prof";
                        communityTooltip = language.GetString("TooltipProfile");
                    }
                    HyperLink communityLink = (HyperLink)itemOutput.FindControl("LnkCty");
                    communityLink.NavigateUrl = communityUrl;
                    communityLink.Text = communityName;
                    communityLink.Target = "_blank";
                    communityLink.ID = null;
                }
                else
                {
                    communityCssClass = "errcty";
                    communityTooltip = language.GetString("TooltipCommunityError"); 
                }
                Panel storagePanel = (Panel)itemOutput.FindControl("PnlLoc");
                storagePanel.CssClass = communityCssClass;
                storagePanel.ID = null;
                storagePanel.ToolTip = communityTooltip;
                HyperLink lnkMyCnt = (HyperLink)itemOutput.FindControl("lnkMyCnt");
                lnkMyCnt.Target = "_blank";
                lnkMyCnt.NavigateUrl = string.Format("/Pages/Admin/MyContent.aspx?T={0}&W={1}&C=&I=false", Helper.GetObjectType(quickObject.ObjectType).Id, quickObject.ObjectID.Value.ToString());
                lnkMyCnt.Text = language.GetString("CommandGoToMyContent");
            }
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
    }
}