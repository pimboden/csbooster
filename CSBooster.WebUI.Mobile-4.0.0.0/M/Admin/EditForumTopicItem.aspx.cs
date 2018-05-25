using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class EditForumTopicItem : System.Web.UI.Page
    {
        private bool objectExisting = true;
        private DataObjectForumTopic forumTopic;
        private DataObjectForumTopicItem forumTopicItem;
        private bool isRoleAllowed;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SiteObjectType siteObjectType = Helper.GetObjectType("ForumTopicItem");
            isRoleAllowed = Array.Exists(siteObjectType.AllowedRoles.Split(','), y => UserDataContext.GetUserDataContext().UserRoles.Contains(y));
            PnlInputAnonymous.Visible = isRoleAllowed && !Request.IsAuthenticated;

            forumTopic = DataObject.Load<DataObjectForumTopic>(Request.QueryString["FTID"].ToNullableGuid());
            bool isCommunityMember;
            bool isCommunityOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forumTopic.CommunityID.Value, out isCommunityMember);

            forumTopicItem = DataObject.Load<DataObjectForumTopicItem>(Request.QueryString["OID"].ToNullableGuid());
            if (forumTopicItem.State == ObjectState.Added)
            {
                LitTitle.Text = language.GetString("LabelAddForumTopicItem");
                lbtnSave.Text = languageShared.GetString("CommandCreate");
                objectExisting = false;
                forumTopicItem.ObjectID = Guid.NewGuid();
                forumTopicItem.CommunityID = forumTopic.CommunityID;
                forumTopicItem.ShowState = forumTopic.IsModerated && !isCommunityOwner ? ObjectShowState.Draft : ObjectShowState.Published;
                forumTopicItem.Status = ObjectStatus.Public;
                forumTopicItem.Title = "ForumTopicItem";
                forumTopicItem.ReferencedItemId = Request.QueryString["RefOID"].ToNullableGuid();
            }
            else
            {
                LitTitle.Text = language.GetString("LabelEditForumTopic");
                lbtnSave.Text = languageShared.GetString("CommandSave");
                TxtDesc.Text = forumTopicItem.Description;
            }
        }

        private bool Save()
        {
            try
            {
                bool isCommunityMember;
                bool isCommunityOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, forumTopic.CommunityID.Value, out isCommunityMember);

                Regex nameRegex = new Regex(Constants.REGEX_USERNAME);
                Regex emailRegex = new Regex(Constants.REGEX_EMAIL);

                if (!string.IsNullOrEmpty(Request.Form["codeBox"]))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageInvisibleTextBoxCaptcha");
                }
                else if (!Request.IsAuthenticated && string.IsNullOrEmpty(TxtName.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageNameRequired");
                }
                else if (!Request.IsAuthenticated && !nameRegex.IsMatch(TxtName.Text))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageNameNotValid");
                }
                else if (!Request.IsAuthenticated && string.IsNullOrEmpty(TxtEmail.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageEmailRequired");
                }
                else if (!Request.IsAuthenticated && !emailRegex.IsMatch(TxtEmail.Text))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageEmailNotValid");
                }
                else if (string.IsNullOrEmpty(TxtDesc.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageForumTopicItemTextRequired");
                }
                else if (((forumTopic.TopicItemCreationUsers == CommunityUsersType.Members && isCommunityMember) ||
                    (forumTopic.TopicItemCreationUsers == CommunityUsersType.Authenticated && Request.IsAuthenticated) ||
                    isRoleAllowed))
                {
                    if (!Request.IsAuthenticated)
                    {
                        forumTopicItem.Nickname = TxtName.Text.StripHTMLTags();
                        forumTopicItem.Email = TxtEmail.Text.StripHTMLTags();
                    }
                    forumTopicItem.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);

                    if (objectExisting)
                        forumTopicItem.Update(UserDataContext.GetUserDataContext());
                    else
                        forumTopicItem.Insert(UserDataContext.GetUserDataContext(), forumTopic.ObjectID.Value, forumTopic.ObjectType);

                    return true;
                }
            }
            catch (Exception e)
            {
                pnlStatus.Visible = true;
                litStatus.Text = "Forum Topic Item konnte nicht gespeichert werden: " + e.Message;
            }
            return false;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (Save())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                    Response.Redirect(System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                else
                    Response.Redirect(Helper.GetMobileDetailLink(forumTopicItem.ObjectType, forumTopicItem.ObjectID.ToString()));
            }
        }
    }
}
