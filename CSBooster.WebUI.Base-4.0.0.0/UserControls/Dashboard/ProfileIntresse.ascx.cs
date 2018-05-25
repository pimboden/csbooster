// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileIntresse : ProfileQuestionsControl
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        private DataObjectUser user;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            try
            {
                user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

                cbxGrp1.Checked = user.Interesst1Show;
                
                FillTextboxAnswer(txtIntress1, user.Interesst1, AnswerType.String);
                FillTextboxAnswer(txtIntress2, user.Interesst2, AnswerType.String);
                FillTextboxAnswer(txtIntress3, user.Interesst3, AnswerType.String);
                FillTextboxAnswer(txtIntress4, user.Interesst4, AnswerType.String);
                FillTextboxAnswer(txtIntress5, user.Interesst5, AnswerType.String);
                FillTextboxAnswer(txtIntress6, user.Interesst6, AnswerType.String);
                FillTextboxAnswer(txtIntress7, user.Interesst7, AnswerType.String);
                FillTextboxAnswer(txtIntress8, user.Interesst8, AnswerType.String);
                FillTextboxAnswer(txtIntress9, user.Interesst9, AnswerType.String);
                FillTextboxAnswer(txtIntress10, user.Interesst10, AnswerType.String);
            }
            catch
            {
                //ignore errors 
            }
        }

        public void Save()
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            user.Interesst1 = txtIntress1.Text;
            user.Interesst1Show = cbxGrp1.Checked;
            user.Interesst2 = txtIntress2.Text;
            user.Interesst2Show = cbxGrp1.Checked;
            user.Interesst3 = txtIntress3.Text;
            user.Interesst3Show = cbxGrp1.Checked;
            user.Interesst4 = txtIntress4.Text;
            user.Interesst4Show = cbxGrp1.Checked;
            user.Interesst5 = txtIntress5.Text;
            user.Interesst5Show = cbxGrp1.Checked;
            user.Interesst6 = txtIntress6.Text;
            user.Interesst6Show = cbxGrp1.Checked;
            user.Interesst7 = txtIntress7.Text;
            user.Interesst7Show = cbxGrp1.Checked;
            user.Interesst8 = txtIntress8.Text;
            user.Interesst8Show = cbxGrp1.Checked;
            user.Interesst9 = txtIntress9.Text;
            user.Interesst9Show = cbxGrp1.Checked;
            user.Interesst10 = txtIntress10.Text;
            user.Interesst10Show = cbxGrp1.Checked;

            user.Update(UserDataContext.GetUserDataContext());

            if (user.State == ObjectState.Saved)
            {
                // nur wenn etwas geändert hat
                string strValues = txtIntress1.Text + txtIntress2.Text + txtIntress3.Text + txtIntress4.Text + txtIntress5.Text + txtIntress6.Text + txtIntress7.Text + txtIntress8.Text + txtIntress9.Text + txtIntress10.Text;
                if (!string.IsNullOrEmpty(strValues.Trim()))
                {
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("PROFILE_OTHERINTERESSTS", UserDataContext.GetUserDataContext());
                }
            }
        }

    }
}