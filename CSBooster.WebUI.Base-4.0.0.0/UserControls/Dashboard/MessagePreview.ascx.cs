// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MessagePreview : System.Web.UI.UserControl
    {
        private Message message;

        public Message Message
        {
            get { return message; }
            set { message = value; }
        }

        public IReloadable ReloadableControl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (message.TypOfMessage != (int)MessageTypes.InviteToCommunity)
            {
                GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
                string messageWindowTitle = (message.UserId == UserProfile.Current.UserId) ? language.GetString("LableMessageFrom") + " " + message.FromUserName : language.GetString("LableMessageTo") + " " + message.UserName;
                this.MSGOPEN.OnClientClick = string.Format("javascript:radWinOpen('/Pages/Popups/Message.aspx?MsgType=Msg&MsgId={0}', '{1}', 510, 490)", message.MsgID, messageWindowTitle);
            }
            if (message.UserId == UserProfile.Current.UserId) // Inbox
            {
                this.MSGOPEN.Click += new EventHandler(OnClick);
                this.MSGOPEN.CommandArgument = message.MsgID.ToString();
            }
            this.MSGOPEN.EnableViewState = false;

            StringBuilder sb = new StringBuilder(500);
            sb.AppendFormat("{0}<br/><br/>", message.DateSent);
            sb.AppendFormat("<b>{0}</b><br/>", message.Subject.CropString(50));
            sb.AppendFormat("{0}", message.MsgText.StripHTMLTags().CropString(200));
            this.LIT.Text = sb.ToString();
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Message.SetRead(new Guid(((LinkButton)sender).CommandArgument)); // Set isRead flag only for received mails
            if (message.TypOfMessage != (int)MessageTypes.InviteToCommunity)
            {
                ReloadableControl.Reload();
            }
            else
            {
                Response.Redirect("~/UserControls/Dashboard/MyCommunityInvitations.aspx");
            }
        }
    }
}