//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.5.0.0		05.08.2008 / AW
//******************************************************************************

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class BlockedUsersActions : UserControl
    {
        private DataObjectUser friend;

        public IReloadable ReloadableControl { get; set; }

        public DataObjectUser Friend
        {
            get { return friend; }
            set { friend = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton removeLinkButton = new LinkButton();
            removeLinkButton.ID = "removeButton";
            removeLinkButton.CssClass = "blockedUserRemoveButton";
            removeLinkButton.ToolTip = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base").GetString("LableUnblock");
            removeLinkButton.CommandArgument = friend.ObjectID.Value.ToString();
            removeLinkButton.Click += RemoveLinkButtonClick;
            Ph.Controls.Add(removeLinkButton);
        }

        private void RemoveLinkButtonClick(object sender, EventArgs e)
        {
            FriendHandler.UnBlockFriend(UserProfile.Current.UserId, ((LinkButton)sender).CommandArgument.ToGuid());
            ReloadableControl.Reload();
        }
    }
}