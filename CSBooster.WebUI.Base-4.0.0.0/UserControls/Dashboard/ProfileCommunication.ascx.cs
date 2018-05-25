// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.Security;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileCommunication : ProfileQuestionsControl
    {
        private DataObjectUser user;
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadAnswers();

            this.PnlEma.Visible = CustomizationSection.CachedInstance.Profile["Email"].Enabled;
            this.PnlPho.Visible = CustomizationSection.CachedInstance.Profile["Phone"].Enabled;
            this.PnlMob.Visible = CustomizationSection.CachedInstance.Profile["PhonenumberMobile"].Enabled;
            this.PnlLan.Visible = CustomizationSection.CachedInstance.Profile["PhonenumberLandline"].Enabled;
            this.PnlCom.Visible = CustomizationSection.CachedInstance.Profile["Communication"].Enabled;
            this.PnlMSN.Visible = CustomizationSection.CachedInstance.Profile["CommunicationMSN"].Enabled;
            this.PnlYah.Visible = CustomizationSection.CachedInstance.Profile["CommunicationYahoo"].Enabled;
            this.PnlSky.Visible = CustomizationSection.CachedInstance.Profile["CommunicationSkype"].Enabled;
            this.PnlICQ.Visible = CustomizationSection.CachedInstance.Profile["CommunicationICQ"].Enabled;
            this.PnlAIM.Visible = CustomizationSection.CachedInstance.Profile["CommunicationAIM"].Enabled;
            this.PnlWeb.Visible = CustomizationSection.CachedInstance.Profile["Web"].Enabled;
            this.PnlHP.Visible = CustomizationSection.CachedInstance.Profile["Homepage"].Enabled;
            this.PnlBlo.Visible = CustomizationSection.CachedInstance.Profile["Blog"].Enabled;
        }

        private void LoadAnswers()
        {
            MembershipUser membershipUser = Membership.GetUser(UserProfile.Current.UserId);
            FillTextboxAnswer(TxtEmail, membershipUser.Email, AnswerType.String);

            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            cbxGrp1.Checked = user.MobileShow;
            FillTextboxAnswer(txtMobile, user.Mobile, AnswerType.String);
            FillTextboxAnswer(txtPhone, user.Phone, AnswerType.String);

            cbxGrp2.Checked = user.MSNShow;
            FillTextboxAnswer(txtMSN, user.MSN, AnswerType.String);
            FillTextboxAnswer(txtYahoo, user.Yahoo, AnswerType.String);
            FillTextboxAnswer(txtSkype, user.Skype, AnswerType.String);
            FillTextboxAnswer(txtICQ, user.ICQ, AnswerType.String);
            FillTextboxAnswer(txtAIM, user.AIM, AnswerType.String);

            cbxGrp3.Checked = user.HomepageShow;
            FillTextboxAnswer(txtHomepage, user.Homepage, AnswerType.String);
            FillTextboxAnswer(txtBlog, user.Blog, AnswerType.String);
        }

        public void Save()
        {
            try
            {
                MembershipUser membershipUser = Membership.GetUser(UserProfile.Current.UserId);
                membershipUser.Email = TxtEmail.Text;
                Membership.UpdateUser(membershipUser);
            }
            catch
            {
                LitEmailMsg.Text = string.Format("<div style=\"margin-top:5px;\">{0}</div>", languageProfile.GetString("MessageEmailSave"));
            }

            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            user.Mobile = txtMobile.Text;
            user.MobileShow = cbxGrp1.Checked;
            user.Phone = txtPhone.Text;
            user.PhoneShow = cbxGrp1.Checked;

            user.MSN = txtMSN.Text;
            user.MSNShow = cbxGrp2.Checked;
            user.Yahoo = txtYahoo.Text;
            user.YahooShow = cbxGrp2.Checked;
            user.Skype = txtSkype.Text;
            user.SkypeShow = cbxGrp2.Checked;
            user.ICQ = txtICQ.Text;
            user.ICQShow = cbxGrp2.Checked;
            user.AIM = txtAIM.Text;
            user.AIMShow = cbxGrp2.Checked;

            user.Homepage = txtHomepage.Text;
            user.HomepageShow = cbxGrp3.Checked;
            user.Blog = txtBlog.Text;
            user.BlogShow = cbxGrp3.Checked;

            user.Update(UserDataContext.GetUserDataContext());

            if (user.State == ObjectState.Saved)
            {
                string strValues = txtAIM.Text + txtBlog.Text + txtHomepage.Text + txtICQ.Text + txtMobile.Text + txtMSN.Text + txtPhone.Text + txtSkype.Text + txtYahoo.Text;
                if (!string.IsNullOrEmpty(strValues.Trim()))
                {
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_ADDRESSES", UserDataContext.GetUserDataContext());
                }
            }
        }

    }
}