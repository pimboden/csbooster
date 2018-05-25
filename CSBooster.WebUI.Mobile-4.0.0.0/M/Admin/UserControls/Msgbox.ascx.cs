// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.Admin.UserControls
{
    public partial class Msgbox : System.Web.UI.UserControl, IReloadable
    {
        private string title;
        private string msgboxType;
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
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string MsgboxType
        {
            get { return msgboxType; }
            set { msgboxType = value; }
        }

        private MessageGroupTypes GroupType
        {
            get
            {
                if (msgboxType == "Inbox")
                    return MessageGroupTypes.Inbox;
                else
                    return MessageGroupTypes.Outbox;
            }
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
            this.PBSearchOptions.Value = "" + searchOptions;
        }

        private void ClearState()
        {
            this.PBSearchOptions.Value = string.Empty;

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


        protected void OnMsgboxItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            _4screen.CSB.DataAccess.Business.Message message = (_4screen.CSB.DataAccess.Business.Message)e.Item.DataItem;

            if (message.UserId == UserProfile.Current.UserId && !message.IsRead)
                ((HtmlTableRow)e.Item.FindControl("trMsg")).Attributes.Add("class", "messageUnread");
            ((HtmlTableRow)e.Item.FindControl("trMsg")).ID = null;

            string userOutput = string.Empty;
            if (msgboxType == "Inbox")
            {
                userOutput = message.FromUserName;
            }
            else if (msgboxType == "Outbox")
            {
                userOutput = message.UserName;
            }
            else if (msgboxType == "Flagged")
            {
                if (message.UserId == UserProfile.Current.UserId)  // Inbox
                    userOutput = message.FromUserName;
                else // Outbox
                    userOutput = message.UserName;
            }

            PlaceHolder panel = (PlaceHolder)e.Item.FindControl("phMsg");
            MessagePreview messagePreview = (MessagePreview)LoadControl("/M/Admin/UserControls/MessagePreview.ascx");
            messagePreview.Message = message;
            messagePreview.MsgUserName = userOutput;

            panel.Controls.Add(messagePreview);

            if (message.TypOfMessage != (int)MessageTypes.InviteToCommunity)
            {
                panel = (PlaceHolder)e.Item.FindControl("phAct");
                LiteralControl lit = new LiteralControl();
                if (msgboxType != "Outbox")
                {
                    lit.Text = string.Format(@"<a class='button' target='_self' href='/M/Admin/MessageSend.aspx?MsgType=Msg&amp;MsgMode=Reply&amp;RecId={0}&amp;MsgId={1}&amp;ReturnUrl={3}'>{2}</a><br/>", UserProfile.Current.UserId, message.MsgID, GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandRespond"), Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
                }
                lit.Text += string.Format(@"<a class='button' target='_self' href='/M/Admin/MessageSend.aspx?MsgType=Msg&amp;MsgMode=Forward&amp;MsgId={0}&amp;ReturnUrl={2}'>{1}</a><br/>", message.MsgID, GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandForward"), Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
                panel.Controls.Add(lit);
                LinkButton deleteButton = new LinkButton();
                deleteButton.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandDelete");
                deleteButton.CommandArgument = message.MsgID.ToString();
                deleteButton.Click += new EventHandler(OnDeleteClick);
                deleteButton.CssClass = "button";
                panel.Controls.Add(deleteButton);
            }

        }

        protected void OnDeleteClick(object sender, EventArgs e)
        {
            _4screen.CSB.DataAccess.Business.Message.DeleteMessage(new Guid(((LinkButton)sender).CommandArgument), UserProfile.Current.UserId);
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
            int? intGroupID = null;

            if (!string.IsNullOrEmpty(Request.QueryString["N"]))
            {
                isRead = true;
            }
            if (this.PBGrpID.Value.Length > 0)
                intGroupID = int.Parse(this.PBGrpID.Value);
            if (msgboxType == "Inbox")
                this.msgbox.DataSource = Messages.GetInbox(UserProfile.Current.UserId, intGroupID, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, null, null, null, sortAttr, sortDir, out numberMessages, SiteConfig.GetSiteContext(UserProfile.Current));
            else if (msgboxType == "Outbox")
                this.msgbox.DataSource = Messages.GetOutbox(UserProfile.Current.UserId, intGroupID, dateSentFrom, dateSentTo, flagged, isRead, generealSearchParam, userName, subject, messageContent, null, null, sortAttr, sortDir, out numberMessages, SiteConfig.GetSiteContext(UserProfile.Current));
            else if (msgboxType == "Flagged")
                this.msgbox.DataSource = Messages.GetFlagged(UserProfile.Current.UserId, dateSentFrom, dateSentTo, isRead, generealSearchParam, userName, subject, messageContent, null, null, sortAttr, sortDir, out numberMessages, SiteConfig.GetSiteContext(UserProfile.Current));
            this.msgbox.DataBind();

            if (numberMessages > 0)
                this.pnlNoItems.Visible = false;
        }
    }
}