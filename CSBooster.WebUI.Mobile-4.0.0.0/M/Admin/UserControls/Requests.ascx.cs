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
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.M.Admin.UserControls
{
    public partial class Requests : System.Web.UI.UserControl, IReloadable
    {
        private string title;
        private string requestType;
        private int currentPage = 1;
        private int numberItems;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public string RequestType
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

        protected void OnRepRequestsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            _4screen.CSB.DataAccess.Business.Message message = (_4screen.CSB.DataAccess.Business.Message)e.Item.DataItem;

            PlaceHolder placeHolder = (PlaceHolder)e.Item.FindControl("FT");
            Literal literal = new Literal();
            literal.Text = message.DateSent.ToShortDateString();
            placeHolder.Controls.Add(literal);

            DataObjectUser user = null;
            placeHolder = (PlaceHolder)e.Item.FindControl("UD");
            string linkMsgeUrl = string.Empty;
            if (requestType == "YouMe")
            {
                user = DataObject.Load<DataObjectUser>(message.FromUserID);
                linkMsgeUrl = string.Format("/M/Admin/MessageSend.aspx?MsgType=Msg&RecId={0}&ReturnUrl={1}", message.FromUserID, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
            }
            else if (requestType == "MeYou")
            {
                user = DataObject.Load<DataObjectUser>(message.UserId);
                linkMsgeUrl = string.Format("/M/Admin/MessageSend.aspx?MsgType=Msg&RecId={0}&ReturnUrl={1}", message.UserId, Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Request.RawUrl)));
            }
            Control control = LoadControl("/UserControls/Templates/SmallOutputUser2.ascx");
            ((ISmallOutputUser)control).DataObjectUser = user;
            placeHolder.Controls.Add(control);

            placeHolder = (PlaceHolder)e.Item.FindControl("FPAN");

            HyperLink linkMsg = new HyperLink();
            linkMsg.Target = "_self";
            linkMsg.NavigateUrl = linkMsgeUrl;
            linkMsg.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandSendMessage");
            linkMsg.CssClass = "button";
            placeHolder.Controls.Add(linkMsg);

            if (requestType == "YouMe")
            {
                LinkButton linkAccept = new LinkButton();
                linkAccept.CommandArgument = message.MsgID.ToString();
                linkAccept.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandAcceptUser");
                linkAccept.CssClass = "button";
                linkAccept.Click += new EventHandler(OnAcceptClick);
                placeHolder.Controls.Add(linkAccept);


                LinkButton linkReject = new LinkButton();
                linkReject.CommandArgument = message.MsgID.ToString();
                linkReject.Text = GuiLanguage.GetGuiLanguage("WebUI.Mobile").GetString("CommandRejectUser");
                linkReject.CssClass = "button";
                linkReject.Click += new EventHandler(OnRejectClick);
                placeHolder.Controls.Add(linkReject);
            }
        }

        void OnAcceptClick(object sender, EventArgs e)
        {
            _4screen.CSB.DataAccess.Business.Message message = new _4screen.CSB.DataAccess.Business.Message(SiteConfig.GetSiteContext(UserProfile.Current));
            message.Load(new Guid(((LinkButton)sender).CommandArgument));
            FriendHandler.Save(UserProfile.Current.UserId, message.FromUserID, false, 0, 0);
            _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("FRIEND_ACCEPT_MOBILE_DEVICE", UserDataContext.GetUserDataContext());
            _4screen.CSB.DataAccess.Business.Message.DeleteMessage(message.MsgID, message.UserId);
            _4screen.CSB.DataAccess.Business.Message.DeleteMessage(message.MsgID, message.FromUserID);
            Reload();
        }

        void OnRejectClick(object sender, EventArgs e)
        {
            _4screen.CSB.DataAccess.Business.Message message = new _4screen.CSB.DataAccess.Business.Message(SiteConfig.GetSiteContext(UserProfile.Current));
            message.Load(new Guid(((LinkButton)sender).CommandArgument));
            _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("FRIEND_REJECT_MOBILE_DEVICE", UserDataContext.GetUserDataContext());
            _4screen.CSB.DataAccess.Business.Message.DeleteMessage(message.MsgID, message.UserId);
            _4screen.CSB.DataAccess.Business.Message.DeleteMessage(message.MsgID, message.FromUserID);
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
            if (requestType == "YouMe")
                this.repRequests.DataSource = Messages.GetRequestInbox(UserProfile.Current.UserId, null, null, out numberItems, SiteConfig.GetSiteContext(UserProfile.Current));
            else if (requestType == "MeYou")
                this.repRequests.DataSource = Messages.GetRequestOutbox(UserProfile.Current.UserId, null, null, out numberItems, SiteConfig.GetSiteContext(UserProfile.Current));
            this.repRequests.DataBind();
            // Check if the number of messages got smaller and the page would be out of bounce

            if (numberItems > 0)
                this.pnlNoItems.Visible = false;
        }
    }
}