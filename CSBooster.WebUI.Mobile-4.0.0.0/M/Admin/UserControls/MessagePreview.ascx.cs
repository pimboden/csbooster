// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.M.Admin.UserControls
{
    public partial class MessagePreview : System.Web.UI.UserControl
    {
        private DataAccess.Business.Message message;

        public DataAccess.Business.Message Message
        {
            get { return message; }
            set { message = value; }
        }

        private string msgUserName;

        public string MsgUserName
        {
            get { return msgUserName; }
            set { msgUserName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkOpen.Click += new EventHandler(OnClick);
            lnkOpen.CommandArgument = message.MsgID.ToString();
            lnkOpen.EnableViewState = false;
            litDate.Text = message.DateSent.ToShortDateString() + " " + message.DateSent.ToShortTimeString();
            litUser.Text = MsgUserName;
            litMsg.Text = message.Subject.StripHTMLTags().CropString(40);
        }

        protected void OnClick(object sender, EventArgs e)
        {
            if (message.UserId == UserProfile.Current.UserId)
                DataAccess.Business.Message.SetRead(new Guid(((LinkButton)sender).CommandArgument));

            if (message.TypOfMessage != (int)MessageTypes.InviteToCommunity)
                Response.Redirect(string.Format("/M/Admin/Message.aspx?MsgType=Msg&MsgId={0}", message.MsgID));
        }
    }
}