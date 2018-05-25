using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public partial class BoardCommunity : System.Web.UI.UserControl
    {
        protected string VRoot;
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
            VRoot = SiteConfig.SiteVRoot;

            string communityId = Request.QueryString["CN"];

            LnkCty1.NavigateUrl = string.Format("Javascript:radWinOpen('{0}/Pages/popups/CommunityMemberOwner.aspx?CN={1}', '{2}', 450, 440, false, null)", SiteConfig.SiteVRoot, communityId, language.GetString("LabelManageMembers").StripForScript());
            LnkCty2.NavigateUrl = string.Format("Javascript:radWinOpen('{0}/Pages/popups/MessageSend.aspx?MsgType=msg&RecType=member&ObjType=Community&ObjId={1}', '{2}', 510, 430, false, null)", SiteConfig.SiteVRoot, communityId, language.GetString("LabelNewMsgToMembers").StripForScript());

            LnkCty1.Attributes.Add("rel", "nofollow");
            LnkCty2.Attributes.Add("rel", "nofollow");

            LnkCty1.ID = null;
            LnkCty2.ID = null;
        }
    }
}
