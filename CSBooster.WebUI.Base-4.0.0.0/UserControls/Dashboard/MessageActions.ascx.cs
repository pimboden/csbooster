// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MessageActions : System.Web.UI.UserControl
    {
        public MessageBoxType Type { get; set; }
        public Message Message { get; set; }
        public Msgbox MessageBox { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

            if (Type == MessageBoxType.Inbox || (Type == MessageBoxType.Flagged && Message.UserId == UserProfile.Current.UserId))
            {
                Literal literal = new Literal();
                literal.Text = string.Format("<a class=\"messageBoxReplyButton\" href=\"javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=Msg&MsgMode=Reply&RecId={0}&MsgId={1}', '{2}', 450, 450);\" title=\"{2}\"></a>", Message.FromUserID, Message.MsgID, language.GetString("CommandAnswerMessage").StripForScript());
                PH.Controls.Add(literal);
            }
            if (Type == MessageBoxType.Inbox || Type == MessageBoxType.Outbox || Type == MessageBoxType.Flagged)
            {
                Literal literal = new Literal();
                literal.Text = string.Format("<a class=\"messageBoxForwardButton\" href=\"javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=Msg&MsgMode=Forward&MsgId={0}', '{1}', 450, 450);\" title=\"{1}\"></a>", Message.MsgID, language.GetString("CommandForwardMessage").StripForScript());
                PH.Controls.Add(literal);

                LinkButton flagLinkButton = new LinkButton();
                flagLinkButton.ID = "flagButton";
                if (Message.Flagged)
                {
                    flagLinkButton.ToolTip = language.GetString("TooltipUnselectMarked");
                    flagLinkButton.CssClass = "messageBoxFlagButtonActive";
                }
                else
                {
                    flagLinkButton.ToolTip = language.GetString("TooltipMark");
                    flagLinkButton.CssClass = "messageBoxFlagButtonInactive";
                }
                flagLinkButton.CommandArgument = Message.MsgID.ToString();
                flagLinkButton.Click += new EventHandler(FlagLinkButtonClick);
                PH.Controls.Add(flagLinkButton);

                LinkButton deleteButton = new LinkButton();
                deleteButton.ID = "deleteButton";
                deleteButton.ToolTip = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandDelete");
                deleteButton.CssClass = "messageBoxDeleteButton";
                deleteButton.Click += new EventHandler(OnMessageDeleteClick);
                deleteButton.CommandArgument = Message.MsgID.ToString();
                PH.Controls.Add(deleteButton);
            }
        }

        private void FlagLinkButtonClick(object sender, EventArgs e)
        {
            Message.ToggleMessageFlag(new Guid(((LinkButton)sender).CommandArgument));
            ((IReloadable)MessageBox).Reload();
        }

        protected void OnMessageDeleteClick(object sender, EventArgs e)
        {
            Message.DeleteMessage(new Guid(((LinkButton)sender).CommandArgument), UserProfile.Current.UserId);
            ((IReloadable)MessageBox).Reload();
        }
    }
}