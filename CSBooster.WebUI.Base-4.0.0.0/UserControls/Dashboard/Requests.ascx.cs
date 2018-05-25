// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
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
    public partial class Requests : System.Web.UI.UserControl, IReloadable, IFriendsRequests, IBrowsable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private string title;
        private FriendsActionType requestType;
        private int pageSize = 25;
        private int currentPage = 1;
        private int numberItems;

        public FriendsActionType FriendsActionType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitFriends();
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

        private void InitFriends()
        {
            pager1.ItemNameSingular = language.GetString("LableRequestSingular");
            pager1.ItemNamePlural = language.GetString("LableRequestPlural");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = language.GetString("LableRequestSingular");
            pager2.ItemNamePlural = language.GetString("LableRequestPlural");
            pager2.BrowsableControl = this;
        }

        protected void OnFriendRequestItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Message message = (Message)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("FT");
            Literal literal = new Literal();
            literal.Text = message.DateSent.ToShortDateString();
            panel.Controls.Add(literal);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("UD");
            Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;
            if (requestType == FriendsActionType.RequestReceived)
            {
                SetUserOutput(userOutput, message.FromUserID);
            }
            else if (requestType == FriendsActionType.RequestSent)
            {
                SetUserOutput(userOutput, message.UserId);
            }
            panel.Controls.Add(userOutput);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("ACT");
            FriendActions actions = (FriendActions)LoadControl("/UserControls/Dashboard/FriendActions.ascx");
            actions.Message = message;
            actions.FriendsActionType = requestType;
            actions.ReloadableControl = this;
            panel.ID = null;
            panel.Controls.Add(actions);

            panel = (Panel)e.Item.FindControl("FPAN");
            if (requestType == FriendsActionType.RequestReceived)
            {
                HyperLink link = new HyperLink();
                link.CssClass = "inputButton";
                link.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/FriendRequest.aspx?MsgType=Msg&MsgId={0}&MsgMode=Accept', '{1}', 550, 620)", message.MsgID, language.GetString("TitleRequestAccept").StripForScript());
                link.Text = language.GetString("CommandRequestAccept");
                link.ID = null;
                Panel fpan2 = new Panel();
                fpan2.CssClass = "friendDecision";
                fpan2.Controls.Add(link);
                panel.Controls.Add(fpan2);

                link = new HyperLink();
                link.CssClass = "inputButtonSecondary";
                link.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/FriendRequest.aspx?MsgType=Msg&MsgId={0}&MsgMode=Deny', '{1}', 550, 620)", message.MsgID, language.GetString("TitleRequestDeny").StripForScript());
                link.Text = language.GetString("CommandRequestDeny");
                link.ID = null;
                Panel fpan3 = new Panel();
                fpan3.CssClass = "friendDecision";
                fpan3.Controls.Add(link);
                panel.Controls.Add(fpan3);
            }
            panel.ID = null;
        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId)
        {
            userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
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
            if (requestType == FriendsActionType.RequestReceived)
                this.friendRepeater.DataSource = Messages.GetRequestInbox(UserProfile.Current.UserId, currentPage, pageSize, out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            else if (requestType == FriendsActionType.RequestSent)
                this.friendRepeater.DataSource = Messages.GetRequestOutbox(UserProfile.Current.UserId, currentPage, pageSize, out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            this.friendRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = this.pager1.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                if (requestType == FriendsActionType.RequestReceived)
                    this.friendRepeater.DataSource = Messages.GetRequestInbox(UserProfile.Current.UserId, currentPage, pageSize, out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                else if (requestType == FriendsActionType.RequestSent)
                    this.friendRepeater.DataSource = Messages.GetRequestOutbox(UserProfile.Current.UserId, currentPage, pageSize, out numberItems, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                this.friendRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, numberItems);
            this.pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                this.noitem.Visible = false;
        }
    }
}