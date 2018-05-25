//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   #1.1.0.0    17.01.2007 / AW
//                         - User icon in comments posted fixed
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class Comments : System.Web.UI.UserControl, IReloadable, IComments, IBrowsable
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private string title;
        private CommentsType commentsType;
        private int pageSize = 10;
        private int currentPage = 1;
        private int numberItems;
        private string sortAttr = "DateCreated";
        private string sortDir = "Desc";
        private string generealSearchParam = null;
        private bool searchOptions = false;
        private string text = null;
        private string userName = null;
        private DateTime? createdFrom = null;
        private DateTime? createdTo = null;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public CommentsType CommentsType
        {
            get { return commentsType; }
            set { commentsType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitComments();
            RestoreState();
            Reload();
            SetSortButtons();
            SaveState();
            SetSearchButtons();
        }

        protected override void OnInit(EventArgs e)
        {
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = this.UniqueID + "$";
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBPageNum")))
                currentPage = int.Parse(Request.Params.Get(idPrefix + "PBPageNum"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSortAttr")))
                sortAttr = Request.Params.Get(idPrefix + "PBSortAttr");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSortDir")))
                sortDir = Request.Params.Get(idPrefix + "PBSortDir");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBGenSearchParam")))
                generealSearchParam = Request.Params.Get(idPrefix + "PBGenSearchParam");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSearchOptions")))
                searchOptions = bool.Parse(Request.Params.Get(idPrefix + "PBSearchOptions"));

            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBText")))
                text = Request.Params.Get(idPrefix + "PBText");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBUserName")))
                userName = Request.Params.Get(idPrefix + "PBUserName");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBCreatedFrom")))
                createdFrom = DateTime.Parse(Request.Params.Get(idPrefix + "PBCreatedFrom"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBCreatedTo")))
                createdTo = DateTime.Parse(Request.Params.Get(idPrefix + "PBCreatedTo"));
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
            if (!string.IsNullOrEmpty(this.sortAttr))
                this.PBSortAttr.Value = sortAttr;
            if (!string.IsNullOrEmpty(this.sortDir))
                this.PBSortDir.Value = sortDir;
            if (!string.IsNullOrEmpty(this.generealSearchParam))
                this.PBGenSearchParam.Text = generealSearchParam;
            if (!string.IsNullOrEmpty(this.generealSearchParam))
                this.PBGenSearchParam.Text = generealSearchParam;
            this.PBSearchOptions.Value = "" + searchOptions;

            if (searchOptions)
            {
                this.search.Visible = true;

                if (!string.IsNullOrEmpty(this.text))
                    this.PBText.Text = this.text;
                if (!string.IsNullOrEmpty(this.userName))
                    this.PBUserName.Text = this.userName;
                if (this.createdFrom.HasValue)
                    this.PBCreatedFrom.SelectedDate = this.createdFrom;
                if (this.createdTo.HasValue)
                    this.PBCreatedTo.SelectedDate = this.createdTo;
            }
        }

        private void ClearState()
        {
            this.search.Visible = false;
            this.PBSearchOptions.Value = string.Empty;
            this.PBGenSearchParam.Text = string.Empty;

            generealSearchParam = null;
            searchOptions = false;
            text = null;
            userName = null;
            createdFrom = null;
            createdTo = null;
        }

        private void SetSortButtons()
        {
            userAscButton.CssClass = "dashboardDownButtonInactive";
            userDescButton.CssClass = "dashboardUpButtonInactive";
            commAscButton.CssClass = "dashboardDownButtonInactive";
            commDescButton.CssClass = "dashboardUpButtonInactive";

            if (sortAttr == "UserName" && sortDir == "Asc")
                userAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "UserName" && sortDir == "Desc")
                userDescButton.CssClass = "dashboardUpButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Asc")
                commAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Desc")
                commDescButton.CssClass = "dashboardUpButtonActive";
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

        private void InitComments()
        {
            pager1.ItemNameSingular = languageShared.GetString("LableCommentSigular");
            pager1.ItemNamePlural = languageShared.GetString("LableCommentPlural");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = languageShared.GetString("LableCommentSigular");
            pager2.ItemNamePlural = languageShared.GetString("LableCommentPlural");
            pager2.BrowsableControl = this;

            this.PBGenSearchParam.Attributes.Add("OnKeyPress", "return DoPostbackOnEnterKey(event, '" + this.findButton.UniqueID + "');");

            if (commentsType == CommentsType.CommentsReceived)
            {
                this.LAB1.Text = language.GetString("LableFrom") + ":";
                this.USRLAB.Text = language.GetString("LableFrom");
            }
            else if (commentsType == CommentsType.CommentsPosted)
            {
                this.LAB1.Text = language.GetString("LableTo") + ":";
                this.USRLAB.Text = language.GetString("LableTo");
            }
        }

        protected void OnCommentItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataObjectComment comment = (DataObjectComment)e.Item.DataItem;

            Panel panel = (Panel)e.Item.FindControl("UD");
            Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;

            SetUserOutput(userOutput, comment.UserID.Value, comment.Nickname);

            panel.Controls.Add(userOutput);

            panel = (Panel)e.Item.FindControl("CP");
            CommentPreview commentPreview = (CommentPreview)LoadControl("/UserControls/Dashboard/CommentPreview.ascx");
            commentPreview.Comment = comment;
            commentPreview.Type = "Comment";
            commentPreview.StripHtml = true;
            panel.Controls.Add(commentPreview);

            panel = (Panel)e.Item.FindControl("COP");
            commentPreview = (CommentPreview)LoadControl("/UserControls/Dashboard/CommentPreview.ascx");
            commentPreview.Comment = comment;
            commentPreview.Type = "Object";
            panel.Controls.Add(commentPreview);
        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId, string userName)
        {
            if (userId != Constants.ANONYMOUS_USERID.ToGuid())
            {
                userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
                userOutput.LinkActive = true;
            }
            else
            {
                userOutput.UserName = userName;
                userOutput.UserDetailURL = string.Empty;
                userOutput.UserPictureURL = _4screen.CSB.Common.SiteConfig.MediaDomainName + Helper.GetObjectType("User").DefaultImageURL;
                userOutput.PrimaryColor = Helper.GetDefaultUserPrimaryColor();
                userOutput.SecondaryColor = Helper.GetDefaultUserSecondaryColor();
            }
        }

        protected void OnSortClick(object sender, EventArgs e)
        {
            LinkButton sortButton = (LinkButton)sender;
            string[] sortOptions = sortButton.CommandArgument.Split(new char[] { ' ' });
            sortAttr = sortOptions[0];
            sortDir = sortOptions[1];
            SaveState();
            SetSortButtons();
            Reload();
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

            QuickParameters quickParams = new QuickParameters
                                              {
                                                  Udc = UserDataContext.GetUserDataContext(),
                                                  ObjectType = Helper.GetObjectTypeNumericID("Comment"),
                                                  FromInserted = createdFrom,
                                                  ToInserted = createdTo,
                                                  PageSize = pageSize,
                                                  PageNumber = currentPage,
                                                  GeneralSearch = generealSearchParam,
                                                  Nickname = userName,
                                                  Description = text
                                              };
            if (sortAttr == "UserName" && sortDir == "Asc")
            {
                quickParams.SortBy = QuickSort.Nickname;
                quickParams.Direction = QuickSortDirection.Asc;
            }
            else if (sortAttr == "UserName" && sortDir == "Desc")
            {
                quickParams.SortBy = QuickSort.Nickname;
                quickParams.Direction = QuickSortDirection.Desc;
            }
            else if (sortAttr == "DateSent" && sortDir == "Asc")
            {
                quickParams.SortBy = QuickSort.InsertedDate;
                quickParams.Direction = QuickSortDirection.Asc;
            }
            else if (sortAttr == "DateSent" && sortDir == "Desc")
            {
                quickParams.SortBy = QuickSort.InsertedDate;
                quickParams.Direction = QuickSortDirection.Desc;
            }

            if (commentsType == CommentsType.CommentsReceived)
            {
                quickParams.CommunityID = UserProfile.Current.ProfileCommunityID;

            }
            else if (commentsType == CommentsType.CommentsPosted)
            {
                quickParams.UserID = UserProfile.Current.UserId;
            }
            DataObjectList<DataObjectComment> comments = DataObjects.Load<DataObjectComment>(quickParams);
            this.comments.DataSource = comments;
            this.comments.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = this.pager1.CheckPageRange(this.currentPage, numberItems);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                quickParams.PageNumber = currentPage;
                comments = DataObjects.Load<DataObjectComment>(quickParams);
                this.comments.DataSource = comments;
                this.comments.DataBind();
            }
            numberItems = comments.ItemTotal;
            this.pager1.InitPager(currentPage, numberItems);
            this.pager2.InitPager(currentPage, numberItems);

            if (numberItems > 0)
                this.noitem.Visible = false;
        }
    }
}