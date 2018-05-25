// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin.UserControls
{
    public partial class Friends : System.Web.UI.UserControl, IReloadable
    {
        private string title;
        private string friendsType;
        private int currentPage = 1;
        private int numberItems;
        private string generealSearchParam = null;
        private bool searchOptions = false;
        private string userName = null;
        private FriendType? friendTypeId = null;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string FriendsType
        {
            get { return friendsType; }
            set { friendsType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGenSearchParam")))
                generealSearchParam = Request.Params.Get(idPrefix + "PBGenSearchParam");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSearchOptions")))
                searchOptions = bool.Parse(Request.Params.Get(idPrefix + "PBSearchOptions"));

            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBUserName")))
                userName = Request.Params.Get(idPrefix + "PBUserName");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBFriendTypeId")))
                friendTypeId = (FriendType)int.Parse(Request.Params.Get(idPrefix + "PBFriendTypeId"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
            this.PBSearchOptions.Value = "" + searchOptions;

        }

        private void ClearState()
        {
            this.PBSearchOptions.Value = string.Empty;

            generealSearchParam = null;
            searchOptions = false;
            userName = null;
            friendTypeId = null;
        }

        protected void OnRepFriendsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectFriend friend = (DataObjectFriend)e.Item.DataItem;

            PlaceHolder ph = (PlaceHolder)e.Item.FindControl("UD");
            Control control = LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            ((ISmallOutputUser)control).DataObjectUser = friend;
            ph.Controls.Add(control);

            ph = (PlaceHolder)e.Item.FindControl("ACT");

            HyperLink linkMsg = new HyperLink();
            linkMsg.Target = "_self";
            linkMsg.NavigateUrl = string.Format("/M/Admin/MessageSend.aspx?MsgType=Msg&RecId={0}&ReturnUrl={1}", friend.UserID, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
            linkMsg.CssClass = "button";
            linkMsg.Text = language.GetString("CommandSendMessage");
            ph.Controls.Add(linkMsg);
        }


        protected void OnSearchClick(object sender, EventArgs e)
        {
            currentPage = 1;
            SaveState();
            Reload();
        }

        protected void OnResetSearchClick(object sender, EventArgs e)
        {
            ClearState();
            currentPage = 1;
            SaveState();
            Reload();
        }

        protected void OnShowSearchOptionsClick(object sender, EventArgs e)
        {
            searchOptions = true;
            SaveState();
        }

        protected void OnHideSearchOptionsClick(object sender, EventArgs e)
        {
            ClearState();
            searchOptions = false;
            SaveState();
            Reload();
        }

        // Interface IBrowsable
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
            DataObjectList<DataObjectFriend> friends = null;
            if (friendsType == "AllFriends")
            {
                friends = DataObjects.Load<DataObjectFriend>(new QuickParametersFriends()
                                                                 {
                                                                     Udc = UserDataContext.GetUserDataContext(),
                                                                     CurrentUserID = UserProfile.Current.UserId,
                                                                     FriendType = friendTypeId,
                                                                     OnlyNotBlocked = true,
                                                                     GeneralSearch = generealSearchParam,
                                                                     Nickname = userName,
                                                                     SortBy = QuickSort.Title,
                                                                     Direction = QuickSortDirection.Asc,
                                                                     PageNumber = 1,
                                                                     PageSize = 1000,
                                                                     IgnoreCache = true
                                                                 });
            }
            else if (friendsType == "BlockedUsers")
            {
                friends = DataObjects.Load<DataObjectFriend>(new QuickParametersFriends()
                                                                 {
                                                                     Udc = UserDataContext.GetUserDataContext(),
                                                                     CurrentUserID = UserProfile.Current.UserId,
                                                                     FriendType = friendTypeId,
                                                                     OnlyNotBlocked = false,
                                                                     GeneralSearch = generealSearchParam,
                                                                     Nickname = userName,
                                                                     SortBy = QuickSort.Title,
                                                                     PageNumber = 1,
                                                                     PageSize = 1000,
                                                                     IgnoreCache = true
                                                                 });
            }
            numberItems = friends.ItemTotal;

            this.repFriends.DataSource = friends;
            this.repFriends.DataBind();

            if (numberItems > 0)
                this.pnlNoItems.Visible = false;
        }
    }
}