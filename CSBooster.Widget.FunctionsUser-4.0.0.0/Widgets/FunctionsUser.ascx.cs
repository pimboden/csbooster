// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class FunctionsUser : WidgetBase
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        DataObject dataObject = null;
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            try
            {
                if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
                {
                    PageType pageType = this.WidgetHost.ParentPageType;
                    if (pageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                    {
                        dataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                    }
                    else if (pageType == PageType.Overview && (!string.IsNullOrEmpty(Request.QueryString["XUI"]) || !string.IsNullOrEmpty(Request.QueryString["XCN"])))
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                        {
                            dataObject = DataObject.Load<DataObject>(Request.QueryString["XUI"].ToGuid());
                        }
                        else if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                        {
                            dataObject = DataObject.Load<DataObject>(Request.QueryString["XCN"].ToGuid());
                        }
                    }
                    else
                    {
                        dataObject = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                    }
                }
                else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
                {
                    dataObject = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                }
                else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    dataObject = DataObject.Load<DataObject>(PageInfo.UserId);
                }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(settingsXml);
                ShowLinks(xmlDocument);
            }
            catch
            {
                hasContent = false;
            }


            return hasContent;
        }

        private void ShowLinks(XmlDocument xmlDocument)
        {
            CustomizationSection customization = CustomizationSection.CachedInstance;

            this.REPLINK.Visible = false;
            this.RECLINK.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Message", true) && customization.Modules["Messaging"].Enabled;
            this.LnkInvite.Visible = false;
            this.LnkFriendRequest.Visible = false;
            this.LnkJoin.Visible = false;
            this.ALELINK.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Notifications", true) && customization.Modules["Alerts"].Enabled;

            this.RECLINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=rec&URL={0}', '{1}', 450, 450, true)", System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)), languageShared.GetString("CommandRecommend").StripForScript());
            if (dataObject != null && dataObject.UserID.Value != UserDataContext.GetUserDataContext().UserID)
            {
                this.REPLINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=rep&RecType=report&ObjType={0}&URL={1}', '{2}', 450, 450, true)", dataObject.ObjectType, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)), languageShared.GetString("CommandContentReport").StripForScript());
                this.REPLINK.Visible = true;
            }
            if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Map", true) && dataObject != null && dataObject.Geo_Lat > double.MinValue && CustomizationSection.CachedInstance.Modules["Geotagging"].Enabled)
            {
                this.MAPLINK.Visible = true;
                this.MAPLINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/DetailMap.aspx?OID={0}', '{1}', 640, 480, true)", dataObject.ObjectID, languageShared.GetString("TitleMap").StripForScript());
            }

            if (dataObject != null && dataObject.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                StringBuilder queryString = new StringBuilder();
                if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]))
                    queryString.AppendFormat("&TGL1={0}", Request.QueryString["TGL1"]);
                if (!string.IsNullOrEmpty(Request.QueryString["TGL2"]))
                    queryString.AppendFormat("&TGL2={0}", Request.QueryString["TGL2"]);
                if (!string.IsNullOrEmpty(Request.QueryString["TGL3"]))
                    queryString.AppendFormat("&TGL3={0}", Request.QueryString["TGL3"]);
                if (!string.IsNullOrEmpty(Request.QueryString["XOTS"]))
                    queryString.AppendFormat("&OTS={0}", Request.QueryString["XOTS"]);
                if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
                    queryString.AppendFormat("&OTS={0}", Request.QueryString["OT"]);

                this.ALELINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/Notifications.aspx?OT={0}{1}', '{2}', 600, 450, true)", dataObject.ObjectType, queryString, languageShared.GetString("CommandAlerts").StripForScript());
            }
            else if (dataObject != null && dataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                string queryString = string.Empty;
                if (!string.IsNullOrEmpty(queryString))
                    queryString = string.Format("&OTS={0}", Request.QueryString["XOTS"]);
                this.ALELINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/Notifications.aspx?CN={0}&OT={1}{2}', '{3}', 600, 450, true)", dataObject.ObjectID, dataObject.ObjectType, queryString, languageShared.GetString("CommandAlerts").StripForScript());
            }
            else if (dataObject != null && dataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
            {
                string queryString = string.Empty;
                if (!string.IsNullOrEmpty(queryString))
                    queryString = string.Format("&OTS={0}", Request.QueryString["XOTS"]);
                this.ALELINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/Notifications.aspx?UI={0}&OT={1}{2}', '{3}', 600, 450, true)", dataObject.ObjectID, dataObject.ObjectType, queryString, languageShared.GetString("CommandAlerts").StripForScript());
                if (Request.IsAuthenticated && dataObject.UserID.Value != UserDataContext.GetUserDataContext().UserID && !FriendHandler.IsFriend(UserDataContext.GetUserDataContext().UserID, dataObject.UserID.Value))
                {
                    if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Friends", true) && customization.Modules["Friends"].Enabled)
                    {
                        LnkFriendRequest.Visible = true;
                        LnkFriendRequest.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=ymr&recid={0}', '{1}', 510, 490)", dataObject.ObjectID.Value, languageShared.GetString("CommandFriendshipQuery").StripForScript());
                        LnkFriendRequest.ID = null;
                    }
                }
            }
            else if (dataObject != null)
            {
                this.ALELINK.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/Notifications.aspx?OID={0}&OT={1}', '{2}', 600, 450, true)", dataObject.ObjectID.Value, dataObject.ObjectType, languageShared.GetString("CommandAlerts").StripForScript());
            }

            if (dataObject != null && dataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                Community community = new Community(dataObject.ObjectID.Value);
                if (!community.IsUserMember && !(community.IsUserOwner || community.IsUserCreator) && community.ProfileOrCommunity.Status == ObjectStatus.Public)
                {
                    if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Membership", true) && customization.Modules["Memberships"].Enabled)
                    {
                        this.LnkJoin.Visible = true;
                        this.LnkJoin.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/Participate.aspx?CTYID={0}', '{1}', 450, 160, true);", community.CommunityId, languageShared.GetString("CommandBecomeAMember").StripForScript());
                    }
                }
                if ((community.IsUserOwner || community.IsUserCreator) && community.ProfileOrCommunity.Status == ObjectStatus.Private)
                {
                    if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Friends", true) && customization.Modules["Friends"].Enabled)
                    {
                        this.LnkInvite.Visible = true;
                        this.LnkInvite.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/MessageSend.aspx?MsgType=invite&ObjType={0}&ObjID={1}', '{2}', 450, 450, true);", Helper.GetObjectType("Community").Id, community.CommunityId, languageShared.GetString("CommandInviteFriend").StripForScript());
                    }
                }
            }

            if (UserProfile.Current.IsAnonymous)
            {
                this.FUNCP2.Visible = false;
                hasContent = (RECLINK.Visible || MAPLINK.Visible || REPLINK.Visible);
            }
            else
            {
                hasContent = (RECLINK.Visible || MAPLINK.Visible || REPLINK.Visible || ALELINK.Visible || LnkJoin.Visible || LnkInvite.Visible || LnkFriendRequest.Visible);
            }
        }
    }

}