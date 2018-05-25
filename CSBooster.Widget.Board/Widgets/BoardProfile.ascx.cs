using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class BoardProfile : System.Web.UI.UserControl
    {
        protected string VRoot;
        protected UserDataContext udc;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetBoard");


        public bool ShowComments
        {
            get
            {
                return PnlComments.Visible;
            }
            set
            {
                PnlComments.Visible = value;
            }
        }

        public bool ShowContents
        {
            get
            {
                return PnlContents.Visible;
            }
            set
            {
                PnlContents.Visible = value;
            }
        }

        public bool ShowFavorites
        {
            get
            {
                return PnlFavorites.Visible;
            }
            set
            {
                PnlFavorites.Visible = value;
            }
        }

        public bool ShowFriends
        {
            get
            {
                return PnlFriends.Visible;
            }
            set
            {
                PnlFriends.Visible = value;
            }
        }

        public bool ShowMembership
        {
            get
            {
                return PnlMembership.Visible;
            }
            set
            {
                PnlMembership.Visible = value;
            }
        }

        public bool ShowMessage
        {
            get
            {
                return PnlMessage.Visible;
            }
            set
            {
                PnlMessage.Visible = value;
            }
        }

        public bool ShowNotifications
        {
            get
            {
                return PnlNotifications.Visible;
            }
            set
            {
                PnlNotifications.Visible = value;
            }
        }

        public bool ShowProperties
        {
            get
            {
                return PnlProperties.Visible;
            }
            set
            {
                PnlProperties.Visible = value;
            }
        }

        public bool ShowSurvey
        {
            get
            {
                return PnlSurvey.Visible;
            }
            set
            {
                PnlSurvey.Visible = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();
            VRoot = SiteConfig.SiteVRoot;

            int intMessageCount = Messages.GetInboxUnreadCount(UserProfile.Current.UserId, null);
            int intRequestCount = Messages.GetRequestUnreadCount(UserProfile.Current.UserId);
            int intCommentsCount;
            ObjectComments.GetCommentsReceived(UserProfile.Current.UserId, null, null, null, null, null, null, null, null, null, null, out intCommentsCount);
           
            if (intMessageCount == 0)
                this.LnkMsg1.Text = language.GetString("ReceivedMessagesNoUnread");
            else if (intMessageCount == 1)
                this.LnkMsg1.Text = language.GetString("ReceivedMessages1Unread");
            else
                this.LnkMsg1.Text = string.Format(language.GetString("ReceivedMessagesXUnread"), intMessageCount);
            this.LnkMsg1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYMESSAGESINBOX_PAGE"].Url;
            this.LnkMsg2.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYMESSAGESOUTBOX_PAGE"].Url;
            this.LnkMsg3.NavigateUrl = string.Format("javascript:radWinOpen('{0}/Pages/Popups/MessageSend.aspx?MsgType=Msg', '{1}', 700, 530, false, null)", VRoot, language.GetString("WriteMessage").StripForScript());

            this.LnkFre1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYYOUMEREQUESTS_PAGE"].Url;
            this.LnkFre2.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYFRIENDS_PAGE"].Url;
            this.LnkFre3.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYMEYOUREQUESTS_PAGE"].Url;
            this.LnkFre4.NavigateUrl = VRoot + Constants.Links["LINK_TO_BLOCKED_USER_PAGE"].Url;
            
            this.LnkTest1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYTEST_PAGE"].Url;

            this.LnkCom1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYCOMMENTSRECEIVED_PAGE"].Url;
            this.LnkCom2.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYCOMMENTSPOSTED_PAGE"].Url;

            this.LnkMem1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYCOMMUNITYMEMBERS_PAGE"].Url;
            this.LnkMem2.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYCOMMUNITYINVITATIONS_PAGE"].Url;

            this.LnkAlerts1.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYALERTS_PAGE"].Url;
            this.LnkAlerts2.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYALERTSETTINGS_PAGE"].Url;

            this.LnkFav.NavigateUrl = VRoot + Constants.Links["LINK_TO_MYFAVORITES_PAGE"].Url;

            this.LnkProf1.NavigateUrl = VRoot + Constants.Links["LINK_TO_PROFILE_PAGE"].Url;
            this.LnkProf3.NavigateUrl = VRoot + Constants.Links["LINK_TO_PROFILESETTINGS_PAGE"].Url;

            if (intRequestCount == 0)
                this.LnkFre1.Text = language.GetString("FriendRequestNoUnread");
            else if (intRequestCount == 1)
                this.LnkFre1.Text = language.GetString("FriendRequest1Unread");
            else
                this.LnkFre1.Text = string.Format(language.GetString("FriendRequestXUnread"), intRequestCount);


            List<SiteObjectType> siteObjects = Helper.GetObjectTypes();
            var contentObjectTypes = from allObjcets in siteObjects
                                     select new
                                     {
                                         ObjectType = allObjcets,
                                         MenuTitle = Helper.GetObjectName(allObjcets.NumericId, false)
                                     };


            List<HyperLink> contentLinks = new List<HyperLink>();
            List<InfoObject> infoObjects = InfoObjects.LoadForUser(udc, UserProfile.Current.UserId, null);
            foreach (var type in contentObjectTypes)
            {
                if (infoObjects.Exists(i => i.ObjectType == type.ObjectType.NumericId) && ((type.ObjectType.IsActive && type.ObjectType.IsUserContent) || UserDataContext.GetUserDataContext().IsAdmin))
                {
                    HyperLink link = new HyperLink();
                    link.Text = string.Format("{0} ({1})", type.MenuTitle, infoObjects.Find(x => x.ObjectType == type.ObjectType.NumericId).Count);
                    link.NavigateUrl = string.Format("{0}/Pages/Admin/MyContent.aspx?T={1}&I=true", VRoot, type.ObjectType.Id);
                    link.Attributes.Add("rel", "nofollow");
                    contentLinks.Add(link);
                }
            }
            if (contentLinks.Count > 0)
            {
                for (int i = 0; i < contentLinks.Count; i++)
                {
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.Controls.Add(contentLinks[i]);
                    this.CNTPH.Controls.Add(div);
                }
            }
            else
            {
                if (!udc.IsAdmin)
                {
                    this.CNTPH.Controls.Add(new LiteralControl(string.Format("<div>{0}</div>", language.GetString("NoContentYet"))));
                }
            }

            //if (udc.IsAdmin)
            //{
            //    HtmlGenericControl div = new HtmlGenericControl("div");
            //    if (contentLinks.Count > 0)
            //        div.Attributes.Add("style", "margin-top:5px");
            //    HyperLink link = new HyperLink();
            //    link.Text = language.GetString("LabelContents");
            //    link.NavigateUrl = string.Format("{0}/Pages/Admin/MyContent.aspx?T=Picture&I=false", VRoot);
            //    div.Controls.Add(link);
            //    this.CNTPH.Controls.Add(div);

            //    this.CNTPH.Controls.Add(new LiteralControl(string.Format("<div><a href=\"javascript:radWinOpen('{0}/Pages/Other/Wizard.aspx?WizardID=CommunityCreate&OID={1}', '{2}', 800, 500, false, null,'wizardWin')\">{3}</a></div>", VRoot, Guid.NewGuid().ToString(), language.GetString("TitleCreateCommunity").StripForScript(), language.GetString("TitleCreateCommunity"))));
            //    this.CNTPH.Controls.Add(new LiteralControl(string.Format("<div><a href=\"javascript:radWinOpen('{0}/Pages/Other/Wizard.aspx?WizardID=PageCreate&OID={1}', '{2}', 800, 500, false, null, 'wizardWin')\">{3}</a></div>", VRoot, Guid.NewGuid().ToString(), language.GetString("TitleCreatePage").StripForScript(), language.GetString("TitleCreatePage"))));
            //}

            LnkMsg1.Attributes.Add("rel", "nofollow");
            LnkMsg2.Attributes.Add("rel", "nofollow");
            LnkMsg3.Attributes.Add("rel", "nofollow");
            LnkFre1.Attributes.Add("rel", "nofollow");
            LnkFre2.Attributes.Add("rel", "nofollow");
            LnkFre3.Attributes.Add("rel", "nofollow");
            LnkFre4.Attributes.Add("rel", "nofollow");
            LnkCom1.Attributes.Add("rel", "nofollow");
            LnkCom2.Attributes.Add("rel", "nofollow");
            LnkMem1.Attributes.Add("rel", "nofollow");
            LnkMem2.Attributes.Add("rel", "nofollow");
            LnkAlerts1.Attributes.Add("rel", "nofollow");
            LnkAlerts2.Attributes.Add("rel", "nofollow");
            LnkFav.Attributes.Add("rel", "nofollow");
            LnkProf1.Attributes.Add("rel", "nofollow");
            LnkProf3.Attributes.Add("rel", "nofollow");

            LnkMsg1.ID = null;
            LnkMsg2.ID = null;
            LnkMsg3.ID = null;
            LnkFre1.ID = null;
            LnkFre2.ID = null;
            LnkFre3.ID = null;
            LnkFre4.ID = null;
            LnkCom1.ID = null;
            LnkCom2.ID = null;
            LnkMem1.ID = null;
            LnkMem2.ID = null;
            LnkAlerts1.ID = null;
            LnkAlerts2.ID = null;
            LnkFav.ID = null;
            LnkProf1.ID = null;
            LnkProf3.ID = null;
            PnlComments.ID = null;
            PnlContents.ID = null;
            PnlFavorites.ID = null;
            PnlFriends.ID = null;
            PnlMembership.ID = null;
            PnlMessage.ID = null;
            PnlNotifications.ID = null;
            PnlProperties.ID = null;
            PnlSurvey.ID = null;
        }
    }
}
