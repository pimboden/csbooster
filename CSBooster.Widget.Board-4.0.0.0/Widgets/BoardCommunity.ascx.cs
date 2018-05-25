// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class BoardCommunity : System.Web.UI.UserControl
    {
        protected UserDataContext udc = null;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetBoard");

        public bool ShowMembership
        {
            get
            {
                return LnkCty1.Visible;
            }
            set
            {
                LnkCty1.Visible = value;
            }
        }

        public bool ShowMessage
        {
            get
            {
                return LnkCty2.Visible;
            }
            set
            {
                LnkCty2.Visible = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            LnkCty1.NavigateUrl = string.Format("Javascript:radWinOpen('/Pages/popups/CommunityMemberOwner.aspx?CN={0}', '{1}', 450, 440, false, null)", PageInfo.EffectiveCommunityId, language.GetString("LabelManageMembers").StripForScript());
            LnkCty2.NavigateUrl = string.Format("Javascript:radWinOpen('/Pages/popups/MessageSend.aspx?MsgType=msg&RecType=member&ObjType=Community&ObjId={0}', '{1}', 510, 430, false, null)", PageInfo.EffectiveCommunityId, language.GetString("LabelNewMsgToMembers").StripForScript());

            LnkCty1.Attributes.Add("rel", "nofollow");
            LnkCty2.Attributes.Add("rel", "nofollow");

            LnkCty1.ID = null;
            LnkCty2.ID = null;
        }
    }
}
