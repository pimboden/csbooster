//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		06.09.2007 / AW
//  Updated:   
//******************************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class AlertPreview : System.Web.UI.UserControl
    {
        private Message message;

        public Message Message
        {
            get { return message; }
            set { message = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.MSGOPEN.OnClientClick = string.Format("javascript:radWinOpen('/Pages/Popups/Message.aspx?MsgType=Msg&MsgId={0}', '{1}', 510, 490)", message.MsgID, GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base").GetString("TitleAlert").StripForScript());
            if (message.UserId == UserProfile.Current.UserId) // Inbox
            {
                this.MSGOPEN.Click += new EventHandler(OnClick);
                this.MSGOPEN.CommandArgument = message.MsgID.ToString();
            }

            StringBuilder sb = new StringBuilder(500);
            sb.Append("<div>");
            sb.AppendFormat("<div>{0}</div>", message.DateSent);
            sb.AppendFormat("<div><b>{0}</b></div>", message.Subject.CropString( 30));
            sb.AppendFormat("<div>{0}</div>", message.MsgText.StripHTMLTags().CropString( 200));
            sb.Append("</div>");
            this.LIT.Text = sb.ToString();
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Message.SetRead(new Guid(((LinkButton)sender).CommandArgument)); // Set isRead flag only for received mails
            ((IReloadable)this.Page).Reload();
        }
    }
}