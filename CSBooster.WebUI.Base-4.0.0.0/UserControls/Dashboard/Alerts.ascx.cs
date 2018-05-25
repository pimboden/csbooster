//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
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
    public partial class Alerts : System.Web.UI.UserControl, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private string msgboxType;
        private int pageSize = 5;
        private int currentPage = 1;
        private int numberMessages;
        private string sortAttr = "DateSent";
        private string sortDir = "Desc";

        public string MsgboxType
        {
            get { return msgboxType; }
            set { msgboxType = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RestoreState();
            Reload();
            SetSortButtons();
            SaveState();
        }

        protected override void OnInit(EventArgs e)
        {
            pager1.ItemNamePlural = language.GetString("LableAlertPlural");
            pager1.ItemNameSingular = language.GetString("LableAlertSingular");
            pager1.BrowsableControl = this;
            pager2.ItemNamePlural = language.GetString("LableAlertPlural");
            pager2.ItemNameSingular = language.GetString("LableAlertSingular");
            pager2.BrowsableControl = this;
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
        }

        private void SaveState()
        {
            if (this.currentPage != 0)
                this.PBPageNum.Value = "" + currentPage;
            if (!string.IsNullOrEmpty(this.sortAttr))
                this.PBSortAttr.Value = sortAttr;
            if (!string.IsNullOrEmpty(this.sortDir))
                this.PBSortDir.Value = sortDir;
        }

        private void ClearState()
        {
        }

        private void SetSortButtons()
        {
            userAscButton.CssClass = "dashboardDownButtonInactive";
            userDescButton.CssClass = "dashboardUpButtonInactive";
            dateAscButton.CssClass = "dashboardDownButtonInactive";
            dateDescButton.CssClass = "dashboardUpButtonInactive";

            if (sortAttr == "UserName" && sortDir == "Asc")
                userAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "UserName" && sortDir == "Desc")
                userDescButton.CssClass = "dashboardUpButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Asc")
                dateAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Desc")
                dateDescButton.CssClass = "dashboardUpButtonActive";
        }

        protected void OnAlertItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Message message = (Message)e.Item.DataItem;

            HtmlTableRow tableRow = (HtmlTableRow)e.Item.FindControl("alertRow");
            if (message.UserId == UserProfile.Current.UserId && !message.IsRead) // Check isRead flag only for received mails
                tableRow.Attributes.Add("class", "alertUnread");
            tableRow.ID = null;

            Panel panel = (Panel)e.Item.FindControl("UD");
            Control ctrl = this.LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;
            SetUserOutput(userOutput, message.FromUserID);
            panel.Controls.Add(userOutput);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("MP");
            AlertPreview messagePreview = (AlertPreview)LoadControl("/UserControls/Dashboard/AlertPreview.ascx");
            messagePreview.Message = message;
            panel.Controls.Add(messagePreview);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("DEL");
            LinkButton deleteButton = new LinkButton();
            deleteButton.ToolTip = languageShared.GetString("CommandDelete");
            deleteButton.CssClass = "alertDeleteButton";
            deleteButton.Click += new EventHandler(OnMessageDeleteClick);
            deleteButton.CommandArgument = message.MsgID.ToString();
            panel.Controls.Add(deleteButton);
            panel.ID = null;

            panel = (Panel)e.Item.FindControl("STATE");
            Image image = new Image();
            if (message.IsRead)
                image.ImageUrl = "~/Library/Images/Layout/icon_alert_read.png";
            else
                image.ImageUrl = "~/Library/Images/Layout/icon_alert_unread.png";
            panel.Controls.Add(image); panel.Controls.Add(image);
            panel.ID = null;
        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId)
        {
            userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
            userOutput.LinkActive = true;
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

        protected void OnMessageDeleteClick(object sender, EventArgs e)
        {
            Message.DeleteMessage(new Guid(((LinkButton)sender).CommandArgument), UserProfile.Current.UserId);
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
            SaveState();
            Reload();
        }

        protected void OnShowSearchOptionsClick(object sender, EventArgs e)
        {
            SaveState();
        }

        protected void OnHideSearchOptionsClick(object sender, EventArgs e)
        {
            ClearState();
            SaveState();
            Reload();
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return this.numberMessages;
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
            this.alertsRepeater.DataSource = Messages.GetAlerts(UserProfile.Current.UserId, null, null, null, null, null, null, null, null, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            this.alertsRepeater.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = this.pager1.CheckPageRange(this.currentPage, numberMessages);
            if (checkedPage != currentPage)
            {
                this.currentPage = checkedPage;
                this.PBPageNum.Value = "" + checkedPage;
                this.alertsRepeater.DataSource = Messages.GetAlerts(UserProfile.Current.UserId, null, null, null, null, null, null, null, null, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                this.alertsRepeater.DataBind();
            }
            this.pager1.InitPager(currentPage, numberMessages);
            this.pager2.InitPager(currentPage, numberMessages);

            if (numberMessages > 0)
                this.noitem.Visible = false;
        }
    }
}