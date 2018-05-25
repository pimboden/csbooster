using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class EditForumTopic : System.Web.UI.Page
    {
        private bool objectExisting = true;
        private DataObjectForumTopic forumTopic;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            forumTopic = DataObject.Load<DataObjectForumTopic>(Request.QueryString["OID"].ToNullableGuid());
            if (forumTopic.State == ObjectState.Added)
            {
                LitTitle.Text = language.GetString("LabelAddForumTopic");
                lbtnSave.Text = languageShared.GetString("CommandCreate");
                objectExisting = false;
                forumTopic.ObjectID = Guid.NewGuid();
                forumTopic.CommunityID = UserProfile.Current.ProfileCommunityID;
                forumTopic.ShowState = ObjectShowState.Published;
                forumTopic.Status = ObjectStatus.Public;
                forumTopic.IsModerated = false;
                forumTopic.TopicItemCreationUsers = CommunityUsersType.Authenticated;
            }
            else
            {
                LitTitle.Text = language.GetString("LabelEditForumTopic");
                lbtnSave.Text = languageShared.GetString("CommandSave");
                TxtTitle.Text = forumTopic.Title;
                TxtDesc.Text = forumTopic.Description;
            }
        }

        private bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtTitle.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageTitleRequired");
                }
                else
                {
                    forumTopic.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                    forumTopic.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);

                    if (objectExisting)
                        forumTopic.Update(UserDataContext.GetUserDataContext());
                    else
                        forumTopic.Insert(UserDataContext.GetUserDataContext(), Request.QueryString["FID"].ToGuid(), Helper.GetObjectTypeNumericID("Forum"));

                    return true;
                }
            }
            catch (Exception e)
            {
                pnlStatus.Visible = true;
                litStatus.Text = "Forum Topic konnte nicht gespeichert werden: " + e.Message;
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
                    Response.Redirect(Helper.GetMobileDetailLink(forumTopic.ObjectType, forumTopic.ObjectID.ToString()));
            }
        }
    }
}
