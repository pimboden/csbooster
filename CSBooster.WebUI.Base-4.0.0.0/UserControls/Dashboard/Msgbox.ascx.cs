// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Templates;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class Msgbox : System.Web.UI.UserControl, IMessageBox, IReloadable, IBrowsable
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private string title;
        private MessageBoxType messageBoxType;
        private int pageSize = 20;
        private int currentPage = 1;
        private int numberMessages;
        private string sortAttr = "DateSent";
        private string sortDir = "Desc";
        private string generealSearchParam = null;
        private bool searchOptions = false;
        private string subject = null;
        private string messageContent = null;
        private string userName = null;
        private DateTime? dateSentFrom = null;
        private DateTime? dateSentTo = null;
        private bool? isRead = null;
        private bool? flagged = null;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public MessageBoxType MessageBoxType
        {
            get { return messageBoxType; }
            set { messageBoxType = value; }
        }

        private MessageGroupTypes GroupType
        {
            get
            {
                if (messageBoxType == MessageBoxType.Inbox)
                    return MessageGroupTypes.Inbox;
                else
                    return MessageGroupTypes.Outbox;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMsgbox();
            RestoreState();
            Reload();
            SetSortButtons();
            SaveState();
            SetSearchButtons();
        }

        protected override void OnInit(EventArgs e)
        {
            FillGroupActions();
        }

        // Restore page state without using viewstates
        private void RestoreState()
        {
            string idPrefix = UniqueID + "$";
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

            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBSubject")))
                subject = Request.Params.Get(idPrefix + "PBSubject");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBMessage")))
                messageContent = Request.Params.Get(idPrefix + "PBMessage");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBUserName")))
                userName = Request.Params.Get(idPrefix + "PBUserName");
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBDateSentFrom")))
                dateSentFrom = DateTime.Parse(Request.Params.Get(idPrefix + "PBDateSentFrom"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBDateSentTo")))
                dateSentTo = DateTime.Parse(Request.Params.Get(idPrefix + "PBDateSentTo"));
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBIsRead")))
                isRead = Request.Params.Get(idPrefix + "PBIsRead") == "on" ? true : false;
            if (!string.IsNullOrEmpty(Request.Params.Get(idPrefix + "PBFlagged")))
                flagged = Request.Params.Get(idPrefix + "PBFlagged") == "on" ? true : false;
        }

        private void SaveState()
        {
            if (currentPage != 0)
                PBPageNum.Value = "" + currentPage;
            if (!string.IsNullOrEmpty(sortAttr))
                PBSortAttr.Value = sortAttr;
            if (!string.IsNullOrEmpty(sortDir))
                PBSortDir.Value = sortDir;
            if (!string.IsNullOrEmpty(generealSearchParam))
                PBGenSearchParam.Text = generealSearchParam;
            if (!string.IsNullOrEmpty(generealSearchParam))
                PBGenSearchParam.Text = generealSearchParam;
            PBSearchOptions.Value = "" + searchOptions;

            if (searchOptions)
            {
                search.Visible = true;

                if (!string.IsNullOrEmpty(subject))
                    PBSubject.Text = subject;
                if (!string.IsNullOrEmpty(messageContent))
                    PBMessage.Text = messageContent;
                if (!string.IsNullOrEmpty(userName))
                    PBUserName.Text = userName;
                if (dateSentFrom.HasValue)
                    PBDateSentFrom.SelectedDate = dateSentFrom;
                if (dateSentTo.HasValue)
                    PBDateSentTo.SelectedDate = dateSentTo;
                if (isRead.HasValue)
                    PBIsRead.Checked = (bool)isRead;
                if (flagged.HasValue)
                    PBFlagged.Checked = (bool)flagged;
            }
        }

        private void ClearState()
        {
            search.Visible = false;
            PBSearchOptions.Value = string.Empty;
            PBGenSearchParam.Text = string.Empty;

            generealSearchParam = null;
            searchOptions = false;
            subject = null;
            messageContent = null;
            userName = null;
            dateSentFrom = null;
            dateSentTo = null;
            isRead = null;
            flagged = null;
        }

        private void SetSortButtons()
        {
            userAscButton.CssClass = "dashboardDownButtonInactive";
            userDescButton.CssClass = "dashboardUpButtonInactive";
            msgAscButton.CssClass = "dashboardDownButtonInactive";
            msgDescButton.CssClass = "dashboardUpButtonInactive";

            if (sortAttr == "UserName" && sortDir == "Asc")
                userAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "UserName" && sortDir == "Desc")
                userDescButton.CssClass = "dashboardUpButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Asc")
                msgAscButton.CssClass = "dashboardDownButtonActive";
            if (sortAttr == "DateSent" && sortDir == "Desc")
                msgDescButton.CssClass = "dashboardUpButtonActive";
        }

        private void SetSearchButtons()
        {
            if (search.Visible == true)
            {
                hideOptButton.Visible = true;
                showOptButton.Visible = false;
            }
            else
            {
                hideOptButton.Visible = false;
                showOptButton.Visible = true;
            }
            if (search.Visible == true || !string.IsNullOrEmpty(generealSearchParam))
            {
                resetButton.Enabled = true;
            }
            else
            {
                resetButton.Enabled = false;
            }
        }

        private void InitMsgbox()
        {
            PBGenSearchParam.Attributes.Add("OnKeyPress", "return DoPostbackOnEnterKey(event, '" + findButton.UniqueID + "')");
            pager1.ItemNameSingular = language.GetString("LableMessageSingular");
            pager1.ItemNamePlural = language.GetString("LableMessagePlural");
            pager1.BrowsableControl = this;
            pager2.ItemNameSingular = language.GetString("LableMessageSingular");
            pager2.ItemNamePlural = language.GetString("LableMessagePlural");
            pager2.BrowsableControl = this;

            if (messageBoxType == MessageBoxType.Inbox)
            {
                LAB1.Text = language.GetString("LableFrom") + ":";
                USRLAB.Text = language.GetString("LableFrom");
            }
            else if (messageBoxType == MessageBoxType.Outbox)
            {
                LAB1.Text = language.GetString("LableTo") + ":";
                USRLAB.Text = language.GetString("LableTo");
            }
            else if (messageBoxType == MessageBoxType.Flagged)
            {
                LAB1.Text = string.Format("{0}/{1}:", language.GetString("LableFrom"), language.GetString("LableTo"));
                USRLAB.Text = string.Format("{0}/{1}", language.GetString("LableFrom"), language.GetString("LableTo"));
            }
        }

        private void FillGroupActions()
        {
            int currentSelectedIndex = ddlActions.SelectedIndex;
            ddlActions.Items.Clear();
            ddlActions.Items.Add(new ListItem(language.GetString("TextAction"), "None"));
            ddlActions.Items.Add(new ListItem(language.GetString("TextDeleteSelected"), "Delete"));
            ddlActions.Items.Add(new ListItem(language.GetString("TextSelectedToggle"), "ToggleFlag"));

            if (messageBoxType == MessageBoxType.Flagged)
            {
                ddlActions.SelectedIndex = currentSelectedIndex;
                return;
            }

            int intGroupID = 0;
            if (Request.QueryString["grpid"] != null)
            {
                PBGrpID.Value = Request.QueryString["grpid"];
                intGroupID = int.Parse(PBGrpID.Value);
            }

            bool blnFirst = true;

            List<DataAccess.Business.MessageGroup> list = DataAccess.Business.MessageGroup.Load(UserProfile.Current.UserId.ToString(), GroupType);
            foreach (DataAccess.Business.MessageGroup item in list)
            {
                if (item.GroupID != intGroupID)
                {
                    if (blnFirst)
                    {
                        ListItem li = new ListItem(language.GetString("TextSelectedMoveTo"), "None");
                        li.Attributes.CssStyle.Add("color", "green");
                        ddlActions.Items.Add(li);
                        blnFirst = false;
                    }
                    ddlActions.Items.Add(new ListItem(string.Format(">>> {0}", item.Title), string.Format("move{0}", item.GroupID)));
                }
            }
            ddlActions.SelectedIndex = currentSelectedIndex;
        }

        protected void OnMessageItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Message message = (Message)e.Item.DataItem;

            HtmlTableRow tableRow = (HtmlTableRow)e.Item.FindControl("messageRow");
            if (message.UserId == UserProfile.Current.UserId && !message.IsRead) // Check isRead flag only for received mails
                tableRow.Attributes.Add("class", "messageBoxUnread");
            tableRow.ID = null;

            PlaceHolder userPlaceHolder = (PlaceHolder)e.Item.FindControl("UD");
            Control ctrl = LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            SmallOutputUser2 userOutput = ctrl as SmallOutputUser2;
            if (messageBoxType == MessageBoxType.Inbox)
            {
                SetUserOutput(userOutput, message.FromUserID);
            }
            else if (messageBoxType == MessageBoxType.Outbox)
            {
                SetUserOutput(userOutput, message.UserId);
            }
            else if (messageBoxType == MessageBoxType.Flagged)
            {
                if (message.UserId == UserProfile.Current.UserId) // Inbox
                    SetUserOutput(userOutput, message.FromUserID);
                else // Outbox
                    SetUserOutput(userOutput, message.UserId);
            }
            userPlaceHolder.Controls.Add(userOutput);

            PlaceHolder messagePlaceHolder = (PlaceHolder)e.Item.FindControl("MP");
            MessagePreview messagePreview = (MessagePreview)LoadControl("/UserControls/Dashboard/MessagePreview.ascx");
            messagePreview.Message = message;
            messagePreview.ReloadableControl = this;
            messagePlaceHolder.Controls.Add(messagePreview);

            if (message.TypOfMessage != (int)MessageTypes.InviteToCommunity)
            {
                PlaceHolder actionPlaceHolder = (PlaceHolder)e.Item.FindControl("ACT");
                MessageActions actions = (MessageActions)LoadControl("/UserControls/Dashboard/MessageActions.ascx");
                actions.Message = message;
                actions.Type = messageBoxType;
                actions.MessageBox = this;
                actionPlaceHolder.Controls.Add(actions);
            }

            Panel statePanel = (Panel)e.Item.FindControl("STATE");
            Image image = new Image();
            if (message.IsRead)
                image.ImageUrl = "~/Library/Images/Layout/icon_msg_read_" + message.MessageState.ToString().ToLower() + ".png";
            else
                image.ImageUrl = "~/Library/Images/Layout/icon_msg_unread_" + message.MessageState.ToString().ToLower() + ".png";
            statePanel.Controls.Add(image);
            if (messageBoxType == MessageBoxType.Flagged)
            {
                image = new Image();
                if (message.UserId == UserProfile.Current.UserId)
                    image.ImageUrl = "~/Library/Images/Layout/icon_msg_inbox.png";
                else
                    image.ImageUrl = "~/Library/Images/Layout/icon_msg_outbox.png";
                statePanel.Controls.Add(image);
            }
            statePanel.ID = null;
        }

        private void SetUserOutput(SmallOutputUser2 userOutput, Guid userId)
        {
            userOutput.DataObjectUser = DataObject.Load<DataObjectUser>(userId);
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
            resetButton.Enabled = false;
            hideOptButton.Visible = false;
            showOptButton.Visible = true;
            SaveState();
            Reload();
        }

        protected void OnShowSearchOptionsClick(object sender, EventArgs e)
        {
            search.Visible = true;
            searchOptions = true;
            resetButton.Enabled = true;
            hideOptButton.Visible = true;
            showOptButton.Visible = false;
            SaveState();
        }

        protected void OnHideSearchOptionsClick(object sender, EventArgs e)
        {
            ClearState();
            search.Visible = false;
            searchOptions = false;
            resetButton.Enabled = false;
            hideOptButton.Visible = false;
            showOptButton.Visible = true;
            SaveState();
            Reload();
        }

        protected void OnActionSelected(object sender, EventArgs e)
        {
            String[] messageIds = Request.Form.GetValues("SEL");
            if (messageIds != null)
            {
                string strValue = ((DropDownList)sender).SelectedValue;
                if (strValue == "Delete")
                {
                    foreach (string messageId in messageIds)
                        Message.DeleteMessage(new Guid(messageId), UserProfile.Current.UserId);
                    Reload();
                }
                else if (strValue == "ToggleFlag")
                {
                    foreach (string messageId in messageIds)
                        Message.ToggleMessageFlag(new Guid(messageId));
                    Reload();
                }
                else if (strValue.StartsWith("move"))
                {
                    int intGroupID = int.Parse(strValue.Substring(4));
                    foreach (string messageId in messageIds)
                        Messages.SetGroup(messageId, GroupType, intGroupID);
                    Reload();
                }
            }
            ddlActions.SelectedIndex = 0;
        }

        // Interface IBrowsable
        public int GetNumberItems()
        {
            return numberMessages;
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
            FillGroupActions();
            int? intGroupID = null;

            if (PBGrpID.Value.Length > 0)
                intGroupID = int.Parse(PBGrpID.Value);

            if (messageBoxType == MessageBoxType.Inbox)
                msgbox.DataSource = Messages.GetInbox(UserProfile.Current.UserId, intGroupID ?? 0, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, null, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            else if (messageBoxType == MessageBoxType.Outbox)
                msgbox.DataSource = Messages.GetOutbox(UserProfile.Current.UserId, intGroupID, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            else if (messageBoxType == MessageBoxType.Flagged)
                msgbox.DataSource = Messages.GetFlagged(UserProfile.Current.UserId, dateSentFrom, dateSentTo, isRead, generealSearchParam, userName, subject, messageContent, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
            msgbox.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce
            int checkedPage = pager1.CheckPageRange(currentPage, numberMessages);
            if (checkedPage != currentPage)
            {
                currentPage = checkedPage;
                PBPageNum.Value = "" + checkedPage;
                if (messageBoxType == MessageBoxType.Inbox)
                    msgbox.DataSource = Messages.GetInbox(UserProfile.Current.UserId, intGroupID, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, null, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                else if (messageBoxType == MessageBoxType.Outbox)
                    msgbox.DataSource = Messages.GetOutbox(UserProfile.Current.UserId, intGroupID, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                else if (messageBoxType == MessageBoxType.Flagged)
                    msgbox.DataSource = Messages.GetFlagged(UserProfile.Current.UserId, dateSentFrom, dateSentTo, isRead, generealSearchParam, userName, subject, messageContent, currentPage, pageSize, sortAttr, sortDir, out numberMessages, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                msgbox.DataBind();
            }
            pager1.InitPager(currentPage, numberMessages);
            pager2.InitPager(currentPage, numberMessages);

            if (numberMessages > 0)
                noitem.Visible = false;
        }
    }
}