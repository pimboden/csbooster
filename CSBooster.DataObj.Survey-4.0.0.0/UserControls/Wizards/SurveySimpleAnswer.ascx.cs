// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataObj.Data;
using _4screen.CSB.DataObj.SurveyCommon;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveySimpleAnswer : System.Web.UI.UserControl, ISurveyWizardPage
    {

        private hitbl_Survey_Answer_Row_SAR currentRowAnswer;
        private SurveyAnswersType CurrentAnswerType;
        private SurveyDataClassDataContext surveyDataClassDataContext;
        private RadMultiPage surveyQuestionMultiPage;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (survey != null && Settings != null && Settings.ContainsKey("SurveyAnswerRow") && Settings.ContainsKey("SurveyAnswersType") && Settings.ContainsKey("surveyDataClassDataContext"))
            {
                currentRowAnswer = Settings["SurveyAnswerRow"] as hitbl_Survey_Answer_Row_SAR;
                CurrentAnswerType = (SurveyAnswersType)Settings["SurveyAnswersType"];
                surveyDataClassDataContext = Settings["surveyDataClassDataContext"] as SurveyDataClassDataContext;
                surveyQuestionMultiPage = Settings["SurveyQuestionMultiPage"] as RadMultiPage;
                
                FillEditForm();
            }
            else
            {
                throw new SiemeArgumentException("_4screen.CSB.DataObj.UserControls.Wizards.SurveySimpleQuestion", "OnInit", "SurveyAnswerRow", "SurveyAnswerRow was not send with the Settings Dictionary");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Implementation of ISurveyWizardPage
#endregion

        public Dictionary<string, object> Settings { get; set; }

        public DataObjectSurvey survey { get; set; }

        public bool SaveSubStep()
        {
            throw new NotImplementedException();
        }

        public void FillEditForm()
        {
            switch (CurrentAnswerType)
            {
                case SurveyAnswersType.SingleTextbox:
                    litQ.Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("AnswerTextbox");
                    litAnswerTitle.Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("AnswerTextbox");
                    txtQ.Visible = false;
                    txtWeigth.Text = currentRowAnswer.AnswerWeight.HasValue ? currentRowAnswer.AnswerWeight.Value.ToString() : "0";
                    break;
                case SurveyAnswersType.Textarea:
                    litQ.Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("AnswerTextarea");
                    litAnswerTitle.Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("AnswerTextbox");
                    txtQ.Visible = false;
                    txtWeigth.Text = currentRowAnswer.AnswerWeight.HasValue ? currentRowAnswer.AnswerWeight.Value.ToString() : "0";
                    break;
                case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                    litQ.Text = currentRowAnswer.AnswerText;
                    litAnswerTitle.Text = currentRowAnswer.AnswerText;
                    txtQ.Content = currentRowAnswer.AnswerText;
                    txtWeigth.Text = currentRowAnswer.AnswerWeight.HasValue ? currentRowAnswer.AnswerWeight.Value.ToString() : "0";
                    break;
                default:
                    break;

            }
            if (Settings.ContainsKey("EditMode"))
            {
                bool editMode = (bool)Settings["EditMode"];
                if (editMode)
                {
                    radMP.SelectedIndex = 1;
                }
            }
        }

        #region Eventhandlers
        #endregion

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            radMP.SelectedIndex = 1;
            surveyQuestionMultiPage.SelectedIndex = 1;
        }

        protected void lbDel_Click(object sender, EventArgs e)
        {
            surveyQuestionMultiPage.SelectedIndex = 1;
            surveyDataClassDataContext.hitbl_Survey_Answer_Row_SARs.DeleteOnSubmit(currentRowAnswer);
            surveyDataClassDataContext.SubmitChanges();
            Visible = false;
        }

        protected void lbtnES_Click(object sender, EventArgs e)
        {
            surveyQuestionMultiPage.SelectedIndex = 1;
            radMP.SelectedIndex = 0;
            if (Settings.ContainsKey("ButtonNew"))
            {
                //Set the Parent's AddNew Button visibilirty to true
                LinkButton lbtn = Settings["ButtonNew"] as LinkButton;
                lbtn.Visible = true;
            }
            switch (CurrentAnswerType)
            {
                case SurveyAnswersType.SingleTextbox:
                case SurveyAnswersType.Textarea:
                    currentRowAnswer.AnswerText = string.Empty;
                    break;
                case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                    currentRowAnswer.AnswerText = txtQ.Content;
                    litQ.Text = currentRowAnswer.AnswerText;
                    break;
                default:
                    break;

            }
            double value;
            currentRowAnswer.AnswerWeight = double.TryParse(txtWeigth.Text, out value) ? value : 0;
            surveyDataClassDataContext.SubmitChanges();
        }

        protected void lbtnEC_Click(object sender, EventArgs e)
        {
            surveyQuestionMultiPage.SelectedIndex = 1;
            radMP.SelectedIndex = 0;
            if (Settings.ContainsKey("ButtonNew"))
            {
                //Set the Parent's AddNew Button visibilirty to true
                LinkButton lbtn = Settings["ButtonNew"] as LinkButton;
                lbtn.Visible = true;
            }
        }

    }
}