// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class Comments : WidgetBase
    {
        private bool hasContent = true;
        private bool allowAnonymous = false;
        private UserDataContext udc;
        private DataObject commentedDataObject;

        public override bool ShowObject(string settingsXml)
        {
            udc = UserDataContext.GetUserDataContext();
            GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetComments");
            RfvName.ErrorMessage = language.GetString("MessageUsernameNeeded");
            RevName.ErrorMessage = language.GetString("MessageUsernameWrongChar");
            RfvEmail.ErrorMessage = language.GetString("MessageValidEmail");
            RevEmail.ErrorMessage = language.GetString("MessageValidEmail");
            RevCom.ErrorMessage = language.GetString("MessageCommentNeeded");
            RevEmail.ValidationExpression = Constants.REGEX_EMAIL;
            RevName.ValidationExpression = Constants.REGEX_USERNAME;
            phResult.Controls.Clear();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            allowAnonymous = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxAllowAnonymous", false);

            PnlInputAnonymous.Visible = allowAnonymous && !udc.IsAuthenticated;
            lbtnAddComment.Visible = allowAnonymous || udc.IsAuthenticated;
            Reload(false);
            return hasContent;
        }

        private void Reload(bool ignoreCache)
        {
            udc = UserDataContext.GetUserDataContext();
            GetCommentedDataObject();

            phResult.Controls.Clear();

            string template = "SmallOutputComment.ascx";
            string repeater = "comments.ascx";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            Control control = LoadControl("~/UserControls/Repeaters/" + repeater);
            ISettings objectComments = (ISettings)control;
            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("CommentedObject", commentedDataObject);
            settings.Add("OutputTemplateControl", template);
            settings.Add("IgnoreCache", ignoreCache);
            objectComments.Settings = settings;
            phResult.Controls.Add(control);
            hasContent = true;
        }

        private void GetCommentedDataObject()
        {
            DataObject profilePageOrCommunity = DataObject.Load<DataObject>(WidgetHost.ParentCommunityID);
            if (profilePageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                commentedDataObject = DataObject.Load<DataObjectUser>(profilePageOrCommunity.UserID);
            }
            else if (profilePageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                commentedDataObject = profilePageOrCommunity;
            }
            else if (profilePageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                if (WidgetHost.ParentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    commentedDataObject = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                }
                else if (WidgetHost.ParentPageType == PageType.None)
                {
                    commentedDataObject = profilePageOrCommunity;
                }
            }
        }

        protected void OnSaveClick(object sender, System.EventArgs e)
        {
            pnlInput.Visible = true;
            pnlInput2.Visible = false;

            Page.Validate();
            
            if (!string.IsNullOrEmpty(Request.Form["codeBox"]))
            {
                PnlError.Visible = true;
                LitError.Text = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("MessageInvisibleTextBoxCaptcha");
            }
            else if (Page.IsValid)
            {
                GetCommentedDataObject();
                udc = UserDataContext.GetUserDataContext();
                DataObjectComment doComment = new DataObjectComment(udc);
                doComment.Status = ObjectStatus.Public;
                doComment.Description = TxtCom.Content;
                doComment.IP = udc.UserIP;
                doComment.ShowState = ObjectShowState.Published;
                if (allowAnonymous && !udc.IsAuthenticated)
                {
                    doComment.Nickname = TxtName.Text.CropString(32);
                    doComment.Email = TxtEmail.Text;
                }
                MembershipUser membershipUser = Membership.GetUser(commentedDataObject.UserID, false);
                doComment.CommunityID = UserProfile.GetProfile(membershipUser.UserName).ProfileCommunityID;
                doComment.Insert(udc, commentedDataObject.ObjectID.Value, commentedDataObject.ObjectType);
                commentedDataObject.AddCommented();
                TxtCom.Content = string.Empty;

                pnlInput.Visible = false;
                pnlInput2.Visible = true;

                Reload(true);

                up2.Update();
            }
        }

        protected void OnAddCommentClick(object sender, EventArgs e)
        {
            pnlInput.Visible = true;
            pnlInput2.Visible = false;
        }
    }
}