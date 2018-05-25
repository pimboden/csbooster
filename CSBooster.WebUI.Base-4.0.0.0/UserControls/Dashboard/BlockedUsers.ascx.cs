//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.5.0.0		05.08.2008 / AW
//******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class BlockedUsers : UserControl, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private const int PageSize = 25;
        private int currentPage = 1;
        private int numberItems;
        private string generealSearchParam;

        public string Title { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitFriends();
            RestoreState();
            Reload();
            SaveState();
            SetSearchButtons();
        }

        protected override void OnInit(EventArgs e)
        {
            pager1.ItemNamePlural = language.GetString("LableBlockedUserPlural");
            pager1.ItemNameSingular = language.GetString("LableBlockedUserSingular");
            pager1.BrowsableControl = this;
            pager2.ItemNamePlural = language.GetString("LableBlockedUserPlural");
            pager2.ItemNameSingular = language.GetString("LableBlockedUserSingular");
            pager2.BrowsableControl = this;
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = UniqueID + "$";

            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGenSearchParam")))
                generealSearchParam = Request.Params.Get(idPrefix + "PBGenSearchParam");
        }

        private void SaveState()
        {
            if (currentPage != 0)
                PBPageNum.Value = "" + currentPage;
            if (!string.IsNullOrEmpty(generealSearchParam))
                PBGenSearchParam.Text = generealSearchParam;
            if (!string.IsNullOrEmpty(generealSearchParam))
                PBGenSearchParam.Text = generealSearchParam;
        }

        private void ClearState()
        {
            PBSearchOptions.Value = string.Empty;
            PBGenSearchParam.Text = string.Empty;

            generealSearchParam = null;
        }

        private void SetSearchButtons()
        {
            if (!string.IsNullOrEmpty(generealSearchParam))
            {
                resetButton.Enabled = true;
            }
            else
            {
                resetButton.Enabled = false;
            }
        }

        private void InitFriends()
        {
            PBGenSearchParam.Attributes.Add("OnKeyPress", String.Format("return DoPostbackOnEnterKey(event, '{0}');", (object)findButton.UniqueID));
        }

        protected void OnBlockedUserItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectUser blockedUser = (DataObjectUser)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("UD");
            Control ctrl = LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;
            SetUserOutput(userOutput, blockedUser.ObjectID.Value);
            panel.Controls.Add(userOutput);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("ACT");
            BlockedUsersActions actions = (BlockedUsersActions)LoadControl("/UserControls/Dashboard/BlockedUsersActions.ascx");
            actions.Friend = blockedUser;
            actions.ReloadableControl = this;
            panel.Controls.Add(actions);
            panel.ID = null;
        }

        private static void SetUserOutput(SmallOutputUser2 userOutput, Guid userId)
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
            resetButton.Enabled = false;
            SaveState();
            Reload();
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return numberItems;
        }

        public void SetCurrentPage(int paramCurrentPage)
        {
            currentPage = paramCurrentPage;
            SaveState();
            Reload();
        }

        // Interface IReloadable
        public void Reload()
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();
            QuickParametersFriends qp = new QuickParametersFriends
                                            {
                                                CurrentUserID = UserProfile.Current.UserId,
                                                OnlyNotBlocked = false,
                                                FriendType = null,
                                                FriendSearchParam = generealSearchParam,
                                                Udc = udc,
                                                SortBy = QuickSort.StartDate,
                                                PageNumber = currentPage,
                                                PageSize = PageSize,
                                                IgnoreCache = true
                                            };
            DataObjectList<DataObjectFriend> lstFriends = DataObjects.Load<DataObjectFriend>(qp);
            numberItems = lstFriends.ItemTotal;
            lstFriends.Sort(new NicknameSorterFriend());
            blockedUserRepeater.DataSource = lstFriends;
            blockedUserRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = pager1.CheckPageRange(currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                currentPage = checkedPage;
                PBPageNum.Value = "" + checkedPage;
                lstFriends = DataObjects.Load<DataObjectFriend>(qp);
                numberItems = lstFriends.ItemTotal;
                lstFriends.Sort(new NicknameSorterFriend());
                blockedUserRepeater.DataSource = lstFriends;
                blockedUserRepeater.DataBind();
            }
            pager1.InitPager(currentPage, numberItems);
            pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                noitem.Visible = false;
        }
    }
}