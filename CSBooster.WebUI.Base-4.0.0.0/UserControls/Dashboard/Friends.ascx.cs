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
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class Friends : System.Web.UI.UserControl, IReloadable, IFriendsRequests, IBrowsable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private string title;
        private FriendsActionType friendsType;
        private int pageSize = 25;
        private int currentPage = 1;
        private int numberItems;
        private string generealSearchParam = null;
        private bool searchOptions = false;
        private string userName = null;
        private FriendType? friendType = null;
        Dictionary<FriendType, string> friendTypes = null;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public FriendsActionType FriendsActionType
        {
            get { return friendsType; }
            set { friendsType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            RestoreState();
            Reload();
            SaveState();
            SetSearchButtons();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitFriends();
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
                friendType = (FriendType)int.Parse(Request.Params.Get(idPrefix + "PBFriendTypeId"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
            if (!string.IsNullOrEmpty(this.generealSearchParam))
                this.PBGenSearchParam.Text = generealSearchParam;
            if (!string.IsNullOrEmpty(this.generealSearchParam))
                this.PBGenSearchParam.Text = generealSearchParam;
            this.PBSearchOptions.Value = "" + searchOptions;

            if (searchOptions)
            {
                this.search.Visible = true;

                if (!string.IsNullOrEmpty(this.userName))
                    this.PBUserName.Text = this.userName;
                if (this.friendType.HasValue)
                    this.PBFriendTypeId.Text = "" + this.friendType;
            }
        }

        private void ClearState()
        {
            this.search.Visible = false;
            this.PBSearchOptions.Value = string.Empty;
            this.PBGenSearchParam.Text = string.Empty;

            generealSearchParam = null;
            searchOptions = false;
            userName = null;
            friendType = null;
        }

        private void SetSearchButtons()
        {
            if (this.search.Visible == true)
            {
                this.hideOptButton.Visible = true;
                this.showOptButton.Visible = false;
            }
            else
            {
                this.hideOptButton.Visible = false;
                this.showOptButton.Visible = true;
            }
            if (this.search.Visible == true || !string.IsNullOrEmpty(this.generealSearchParam))
            {
                this.resetButton.Enabled = true;
            }
            else
            {
                this.resetButton.Enabled = false;
            }
        }

        private void InitFriends()
        {
            PBGenSearchParam.Attributes.Add("OnKeyPress", "return DoPostbackOnEnterKey(event, '" + this.findButton.UniqueID + "');");

            pager1.ItemNameSingular = language.GetString("LableFriendSingular");
            pager1.ItemNamePlural = language.GetString("LableFriendPlural");
            pager2.ItemNameSingular = language.GetString("LableFriendSingular");
            pager2.ItemNamePlural = language.GetString("LableFriendPlural");
            pager1.BrowsableControl = this;
            pager2.BrowsableControl = this;

            ddlActions.Items[0].Text = language.GetString("TextAction");
            ddlActions.Items[1].Text = language.GetString("TextDeleteSelected");
            ddlActions.Items[2].Text = language.GetString("TextFriendshipChandeType");

            friendTypes = FriendHandler.GetFriendTypes();
            foreach (KeyValuePair<FriendType, string> kvp in friendTypes)
            {
                ListItem item = new ListItem(string.Format(">>> {0}", kvp.Value), "FSS_" + kvp.Key.ToString("d"));
                this.ddlActions.Items.Add(item);

                item = new ListItem(kvp.Value, kvp.Key.ToString("d"));
                this.PBFriendTypeId.Items.Add(item);
            }
        }

        protected void OnFriendItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectFriend friend = (DataObjectFriend)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("FT");
            Literal literal = new Literal();
            if (friendTypes.ContainsKey(friend.FriendType))
                literal.Text = friendTypes[friend.FriendType];
            else
                literal.Text = "-";
            panel.Controls.Add(literal);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("UD");
            Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;
            SetUserOutput(userOutput, friend.ObjectID.Value);
            panel.ID = null;
            panel.Controls.Add(userOutput);

            panel = (Panel)e.Item.FindControl("ACT");
            FriendActions actions = (FriendActions)LoadControl("/UserControls/Dashboard/FriendActions.ascx");
            actions.Friend = friend;
            actions.FriendsActionType = friendsType;
            actions.ReloadableControl = this;
            panel.ID = null;
            panel.Controls.Add(actions);
        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId)
        {
            userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
            userOutput.LinkActive = true;
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
            this.resetButton.Enabled = false;
            this.hideOptButton.Visible = false;
            this.showOptButton.Visible = true;
            SaveState();
            Reload();
        }

        protected void OnShowSearchOptionsClick(object sender, EventArgs e)
        {
            this.search.Visible = true;
            searchOptions = true;
            this.resetButton.Enabled = true;
            this.hideOptButton.Visible = true;
            this.showOptButton.Visible = false;
            SaveState();
        }

        protected void OnHideSearchOptionsClick(object sender, EventArgs e)
        {
            ClearState();
            this.search.Visible = false;
            searchOptions = false;
            this.resetButton.Enabled = false;
            this.hideOptButton.Visible = false;
            this.showOptButton.Visible = true;
            SaveState();
            Reload();
        }

        protected void OnActionSelected(object sender, EventArgs e)
        {
            String[] friendIds = Request.Form.GetValues("YMSEL");
            if (friendIds != null)
            {
                string selectedValue = ((DropDownList)sender).SelectedValue;
                if (selectedValue == "Delete")
                {
                    foreach (string friendId in friendIds)
                        FriendHandler.DeleteFriend(UserProfile.Current.UserId, new Guid(friendId));
                    Reload();
                }
                else if (selectedValue.StartsWith("FSS_"))
                {
                    int friendshipTypeId = int.Parse(selectedValue.Substring(4));
                    foreach (string friendId in friendIds)
                        FriendHandler.Save(UserProfile.Current.UserId, new Guid(friendId), false, friendshipTypeId, 0);
                    Reload();
                }
            }
            this.ddlActions.SelectedIndex = 0;
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
            UserDataContext udc = UserDataContext.GetUserDataContext();
            DataObjectList<DataObjectFriend> lstFriends = null;
            QuickParametersFriends qp = new QuickParametersFriends
                                            {
                                                CurrentUserID = UserProfile.Current.UserId,
                                                FriendType = friendType,
                                                FriendSearchParam = generealSearchParam,
                                                Udc = udc,
                                                Nickname = userName,
                                                SortBy = QuickSort.StartDate,
                                                PageNumber = currentPage,
                                                PageSize = pageSize,
                                                IgnoreCache = true
                                            };
            qp.OnlyNotBlocked = true;
            lstFriends = DataObjects.Load<DataObjectFriend>(qp);

            numberItems = lstFriends.ItemTotal;
            lstFriends.Sort(new NicknameSorterFriend());
            this.friendRepeater.DataSource = lstFriends;
            this.friendRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = this.pager1.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                lstFriends = DataObjects.Load<DataObjectFriend>(qp);
                numberItems = lstFriends.ItemTotal;
                lstFriends.Sort(new NicknameSorterFriend());
                this.friendRepeater.DataSource = lstFriends;
                this.friendRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, numberItems);
            this.pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                this.noitem.Visible = false;
        }
    }
}