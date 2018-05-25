// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileInterestTopic : ProfileQuestionsControl
    {
        protected GuiLanguage languageProfile = GuiLanguage.GetGuiLanguage("ProfileData");
        private DataObjectUser user;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            rptTopics.DataSource = ProfileDataHelper.InterestTopics;
            rptTopics.DataBind();
        }

        protected void rptTopics_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            InterestTopic interestTopic = e.Item.DataItem as InterestTopic;
            e.Item.ID = string.Format("rpit{0}", interestTopic.ID);

            Label lblTopic = e.Item.FindControl("lblTopic") as Label;

            CheckBoxList cblSubTopic = e.Item.FindControl("cblSubTopic") as CheckBoxList;
            cblSubTopic.ID = string.Format("ST{0}", interestTopic.ID);

            CheckBox cbxGrp1 = e.Item.FindControl("cbxGrp1") as CheckBox;
            cbxGrp1.ID = string.Format("GRP{0}", interestTopic.ID);

            lblTopic.Text = interestTopic.DisplayName;
            LoadQuestions(cblSubTopic, interestTopic.DBField);
            LoadAnswers(interestTopic.DBField, cblSubTopic, cbxGrp1);
        }

        private void LoadQuestions(CheckBoxList cblSubTopic, string dBFielName)
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            cblSubTopic.Items.Clear();
            cblSubTopic.DataSource = ProfileDataHelper.GetInterestSubTopic(dBFielName);
            cblSubTopic.DataTextField = "value";
            cblSubTopic.DataValueField = "key";
            cblSubTopic.DataBind();
        }

        private void LoadAnswers(string dBFieldName, CheckBoxList cblSubTopic, CheckBox cbxGrp1)
        {
            try
            {
                PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(dBFieldName);
                string values = propertyInfo.GetValue(user, null).ToString();

                PropertyInfo propertyInfo2 = typeof(DataObjectUser).GetProperty(dBFieldName + "Show");
                bool valuesShow = (bool)propertyInfo2.GetValue(user, null);

                cbxGrp1.Checked = valuesShow;

                FillCheckBoxListAnswer(cblSubTopic, values, FillListBy.Value);
            }
            catch
            {
            }
        }

        public void Save()
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);

            int iRptItemCount = 0;
            List<InterestTopic> interestTopics = rptTopics.DataSource as List<InterestTopic>;
            foreach (InterestTopic interestTopic in interestTopics)
            {
                CheckBoxList cblSubTopic = rptTopics.Items[iRptItemCount].FindControl(string.Format("ST{0}", interestTopic.ID)) as CheckBoxList;
                CheckBox cbxGrp1 = rptTopics.Items[iRptItemCount].FindControl(string.Format("GRP{0}", interestTopic.ID)) as CheckBox;
                if (cblSubTopic != null && cbxGrp1 != null)
                {
                    PropertyInfo propertyInfo = typeof(DataObjectUser).GetProperty(interestTopic.DBField);
                    propertyInfo.SetValue(user, GetComaSeparatetdValues(cblSubTopic.Items), null);

                    PropertyInfo propertyInfo2 = typeof(DataObjectUser).GetProperty(interestTopic.DBField + "Show");
                    propertyInfo2.SetValue(user, cbxGrp1.Checked, null);

                    /*if (!string.IsNullOrEmpty(GetComaSeparatetdValues(cblSubTopic.Items)))
                    {
                        _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(string.Format("PROFILE_{0}", interestTopic.DBField.ToUpper()), UserDataContext.GetUserDataContext());
                    }*/
                }
                iRptItemCount++;
            }

            user.Update(UserDataContext.GetUserDataContext());
        }
    }
}