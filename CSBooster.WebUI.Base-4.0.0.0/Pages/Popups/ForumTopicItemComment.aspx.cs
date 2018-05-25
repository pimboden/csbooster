//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		25.09.2007 / AW
//  Updated:   
//******************************************************************************
using System;
using System.Linq;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Extensions.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class ForumTopicItemComment : System.Web.UI.Page
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        private UserDataContext udc;
        private Guid? objectId;
        private Guid? referencedItemId;
        private string objectType;
        private bool isRoleAllowed;

        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext(UserProfile.Current.UserName);

            SiteObjectType siteObjectType = Helper.GetObjectType("ForumTopicItem");
            isRoleAllowed = Array.Exists(siteObjectType.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y));
            PnlInputAnonymous.Visible = isRoleAllowed && !udc.IsAuthenticated;
            
            RfvName.ErrorMessage = language.GetString("MessageUsernameRequired");
            RevName.ErrorMessage = language.GetString("MessageUsernameWrongChar");
            RfvEmail.ErrorMessage = language.GetString("MessageValidEmail");
            RevEmail.ErrorMessage = language.GetString("MessageValidEmail");
            RevEmail.ValidationExpression = Constants.REGEX_EMAIL;
            RevName.ValidationExpression = Constants.REGEX_USERNAME;
            RevCom.ErrorMessage = language.GetString("MessageForumTopicItemTextRequired");

            if (Request.QueryString["ObjId"] != null)
                objectId = Request.QueryString["ObjId"].ToGuid();
            if (Request.QueryString["ObjIdRef"] != null)
                referencedItemId = Request.QueryString["ObjIdRef"].ToGuid();
            objectType = Request.QueryString["ObjType"] ?? string.Empty;

            TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Detail);
            COMTIT.Text = language.GetString("LableAnswer") + ":";
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (!string.IsNullOrEmpty(Request.Form["codeBox"]))
            {
                PnlError.Visible = true;
                LitError.Text = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("MessageInvisibleTextBoxCaptcha");
            }
            else if (Page.IsValid)
            {
                DataObjectForumTopic forumTopic = DataObject.Load<DataObjectForumTopic>(objectId);

                bool isCommunityMember;
                bool isCommunityOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forumTopic.CommunityID.Value, out isCommunityMember);

                if (((forumTopic.TopicItemCreationUsers == CommunityUsersType.Members && isCommunityMember) ||
                    (forumTopic.TopicItemCreationUsers == CommunityUsersType.Authenticated && udc.IsAuthenticated) ||
                    isRoleAllowed) && forumTopic.State == ObjectState.Saved)
                {
                    DataObjectForumTopicItem forumTopicItem = new DataObjectForumTopicItem(udc);
                    forumTopicItem.ObjectID = Guid.NewGuid();
                    forumTopicItem.CommunityID = forumTopic.CommunityID;
                    forumTopicItem.Status = forumTopic.Status;
                    forumTopicItem.ShowState = forumTopic.IsModerated && !isCommunityOwner ? ObjectShowState.Draft : ObjectShowState.Published;
                    forumTopicItem.Title = "ForumTopicItem";
                    forumTopicItem.Description = TxtCom.Content;
                    forumTopicItem.ReferencedItemId = referencedItemId;
                    if (!udc.IsAuthenticated)
                    {
                        forumTopicItem.Nickname = TxtName.Text.CropString(32);
                        forumTopicItem.Email = TxtEmail.Text;
                    }
                    forumTopicItem.Insert(udc, forumTopic.ObjectID.Value, Helper.GetObjectTypeNumericID("ForumTopic"));
                    IncentivePointsManager.AddIncentivePointEvent("FORUMTOPICITEM_UPLOAD", udc, forumTopicItem.CommunityID.Value.ToString());
                }
                ClientScript.RegisterStartupScript(GetType(), "CloseWindow", "$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );", true);
            }
        }
    }
}