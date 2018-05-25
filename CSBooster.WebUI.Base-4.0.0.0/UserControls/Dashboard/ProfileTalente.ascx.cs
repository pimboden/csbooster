// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileTalente : ProfileQuestionsControl
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");

        private List<TextBox> talentTextboxes;
        private List<ProfileDataGeneralItem> profileDataGeneralItems;
        private int tabIndexCounter = 303;
        private DataObjectUser user;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadAnswers();

            this.PnlJobTal.Visible = CustomizationSection.CachedInstance.Profile["JobTalents"].Enabled;
            this.PnlJob.Visible = CustomizationSection.CachedInstance.Profile["Job"].Enabled;
            this.PnlSlo.Visible = CustomizationSection.CachedInstance.Profile["Slogan"].Enabled;
        }

        private void LoadAnswers()
        {
            try
            {
                user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);
                cbxGrp1.Checked = user.BerufShow;
                FillTextboxAnswer(txtBeruf, user.Beruf, AnswerType.String);
                FillTextboxAnswer(txtMotto, user.Lebensmoto, AnswerType.String);

                talentTextboxes = new List<TextBox>();
                profileDataGeneralItems = ProfileDataHelper.GetProfileDataGeneralItems(string.Empty);
                this.RepTalents.DataSource = profileDataGeneralItems;
                this.RepTalents.DataBind();
            }
            catch
            {
            }
        }

        public void Save()
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            StringBuilder userInput = new StringBuilder();
            user.Beruf = txtBeruf.Text;
            user.BerufShow = cbxGrp1.Checked;
            user.Lebensmoto = txtMotto.Text;
            user.LebensmotoShow = cbxGrp1.Checked;

            for (int i = 0; i < talentTextboxes.Count; i++)
            {
                PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(profileDataGeneralItems[i].DBField);
                propertyInfo.SetValue(user, talentTextboxes[i].Text, null);

                PropertyInfo propertyInfo2 = typeof(DataObjectUser).GetProperty(profileDataGeneralItems[i].DBField + "Show");
                propertyInfo2.SetValue(user, cbxGrp1.Checked, null);

                userInput.Append(talentTextboxes[i].Text);
            }

            user.Update(UserDataContext.GetUserDataContext());
            if (user.State == ObjectState.Saved)
            {
                if (userInput.Length > 0)
                {
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_WHO_AM_I", UserDataContext.GetUserDataContext());
                }
            }
        }

        protected void OnRepTalentsBinding(object sender, RepeaterItemEventArgs e)
        {
            ProfileDataGeneralItem generalItem = (ProfileDataGeneralItem)e.Item.DataItem;

            Literal title = (Literal)e.Item.FindControl("LitTalent");
            title.Text = generalItem.FriendlyName + ":";

            TextBox textbox = (TextBox)e.Item.FindControl("TxtTalent");
            textbox.TabIndex = (short)(tabIndexCounter++);
            talentTextboxes.Add(textbox);

            PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(generalItem.DBField);
            if (propertyInfo.GetValue(user, null) != null)
                textbox.Text = propertyInfo.GetValue(user, null).ToString();
        }
    }
}