// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataObj.Data;
using _4screen.CSB.DataObj.SurveyCommon;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveyQuestion : System.Web.UI.UserControl, ISurveyWizardPage
    {
        private hitbl_Survey_Question_SQU currentQuestion;
        private SurveyDataClassDataContext surveyDataClassDataContext;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (survey != null && Settings != null && Settings.ContainsKey("SurveyQuestion") && Settings.ContainsKey("surveyDataClassDataContext"))
            {
                currentQuestion = Settings["SurveyQuestion"] as hitbl_Survey_Question_SQU;
                surveyDataClassDataContext = Settings["surveyDataClassDataContext"] as SurveyDataClassDataContext;
                FillEditForm();
            }
            else
            {
                throw new SiemeArgumentException("_4screen.CSB.DataObj.UserControls.Wizards.SurveyQuestion", "OnInit", "SurveyQuestion", "SurveyQuestion was not send with the Settings Dictionary");
            }
        }

        #region Implementation of ISurveyWizardPage
        #endregion

        public Dictionary<string, object> Settings { get; set; }

        public DataObjectSurvey survey { get; set; }

        public bool SaveSubStep()
        {
            currentQuestion.QuestionText = txtQ.Content;
            litQ.Text = txtQ.Content;
            surveyDataClassDataContext.SubmitChanges();
            return true;
        }

        public void FillEditForm()
        {
            FillAnswerTypes();
            try
            {
                rcbAnswerType.SelectedIndex = rcbAnswerType.Items.IndexOf(rcbAnswerType.Items.FindItemByValue(Convert.ToInt32( currentQuestion.QuestionType).ToString()));
            }
            catch
            {
                rcbAnswerType.SelectedIndex = 0;
            }
            litQ.Text = currentQuestion.QuestionText;
            txtQ.Content = currentQuestion.QuestionText;

            if (Settings.ContainsKey("EditMode"))
            {
                bool editMode = (bool)Settings["EditMode"];
                if (editMode)
                {
                    radMP.SelectedIndex = 1;
                }
            }
            LoadQuestions();
        }

        #region Local Methods
        #endregion

        private void LoadQuestions()
        {
            switch (currentQuestion.QuestionType)
            {
                case SurveyAnswersType.NotSelected:
                    phAnsList.Controls.Add(new LiteralControl(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("NotSelected")));
                    break;
                case SurveyAnswersType.NoAnswers:
                    phAnsList.Controls.Add(new LiteralControl(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("NoAnswers")));
                    break;
                case SurveyAnswersType.SingleTextbox:
                case SurveyAnswersType.Textarea:
                case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                    LoadSimpleAnswers();
                    break;
            }
        }

        private void LoadSimpleAnswers()
        {
            var allSurveyAnswers = from allAnswers in surveyDataClassDataContext.hitbl_Survey_Answer_Row_SARs.Where(x => x.SQU_ID == currentQuestion.SQU_ID)
                                   orderby allAnswers.SortNumber ascending
                                   select allAnswers;


            int i = 0;
            foreach (hitbl_Survey_Answer_Row_SAR surveyAnswer in allSurveyAnswers)
            {
                LoadSurveySimpleQuestionOutput(surveyAnswer, false, null);
                i++;
            }
            if ((i == 0 || (currentQuestion.QuestionType != SurveyAnswersType.SingleTextbox && currentQuestion.QuestionType != SurveyAnswersType.Textarea)&&(currentQuestion.QuestionType != SurveyAnswersType.NoAnswers)))
            {
                phAnsList.Controls.Add(new LiteralControl("<div style='float:left'>"));
                LinkButton lbtnNSA = new LinkButton
                                         {
                                             ID = string.Format("lbtnNQ{0}", currentQuestion.SQU_ID),
                                             Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyAddAnswer"),
                                             CommandArgument = (i * 10).ToString(),
                                             CssClass = "inputButton"
                                         };
                lbtnNSA.Click += new EventHandler(lbtnNSA_Click);
                phAnsList.Controls.Add(lbtnNSA);
                phAnsList.Controls.Add(new LiteralControl("</div><div class='clearBoth'></div>"));
            }
        }

        private void LoadSurveySimpleQuestionOutput(hitbl_Survey_Answer_Row_SAR surveyAnswerRow, bool editMode, LinkButton buttonNew)
        {
            string idPostFix = surveyAnswerRow.SAR_ID.ToString().Replace("_", "_");
            Control ucSurveySimpleQuestion = LoadControl("/UserControls/Wizards/SurveySimpleAnswer.ascx");
            ISurveyWizardPage iSurveySimpleQuestion = ucSurveySimpleQuestion as ISurveyWizardPage;
            iSurveySimpleQuestion.survey = survey;
            iSurveySimpleQuestion.Settings = new Dictionary<string, object>
                                                 {
                                                     {"SurveyAnswerRow", surveyAnswerRow},
                                                     {"surveyDataClassDataContext", surveyDataClassDataContext},
                                                     {"SurveyAnswersType", currentQuestion.QuestionType},
                                                     {"SurveyQuestionMultiPage", radMP}
                                                 };
            if (editMode)
            {
                iSurveySimpleQuestion.Settings.Add("EditMode", editMode);

                if (buttonNew != null)
                {
                    iSurveySimpleQuestion.Settings.Add("ButtonNew", buttonNew);
                }

            }
            ucSurveySimpleQuestion.ID = string.Format("GDP_{0}", idPostFix);
            phAnsList.Controls.Add(ucSurveySimpleQuestion);

        }

        private void FillAnswerTypes()
        {
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.NotSelected.ToString()), ((int)SurveyAnswersType.NotSelected).ToString()));
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.NoAnswers.ToString()), ((int)SurveyAnswersType.NoAnswers).ToString()));
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.MultipleChoiceMultipleAnswers.ToString()), ((int)SurveyAnswersType.MultipleChoiceMultipleAnswers).ToString()));
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.MultipleChoiceOnlyOneAnswer.ToString()), ((int)SurveyAnswersType.MultipleChoiceOnlyOneAnswer).ToString()));
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.SingleTextbox.ToString()), ((int)SurveyAnswersType.SingleTextbox).ToString()));
            rcbAnswerType.Items.Add(new RadComboBoxItem(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString(SurveyAnswersType.Textarea.ToString()), ((int)SurveyAnswersType.Textarea).ToString()));
        }

        #region EventHabndlers
        #endregion

        private void lbtnNSA_Click(object sender, EventArgs e)
        {
            //Add New SImple Answer
            hitbl_Survey_Answer_Row_SAR newAnswer = new hitbl_Survey_Answer_Row_SAR();
            newAnswer.SAR_ID = Guid.NewGuid();
            newAnswer.AnswerWeight = 0;
            newAnswer.SQU_ID = currentQuestion.SQU_ID;
            newAnswer.SortNumber = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            surveyDataClassDataContext.hitbl_Survey_Answer_Row_SARs.InsertOnSubmit(newAnswer);
            surveyDataClassDataContext.SubmitChanges();
            LinkButton lbtnNSA = sender as LinkButton;
            lbtnNSA.Visible = false;

            switch (currentQuestion.QuestionType)
            {
                case SurveyAnswersType.SingleTextbox:
                case SurveyAnswersType.Textarea:
                    LoadSurveySimpleQuestionOutput(newAnswer, true, null);
                    break;
                case SurveyAnswersType.MultipleChoiceOnlyOneAnswer:
                case SurveyAnswersType.MultipleChoiceMultipleAnswers:
                    LoadSurveySimpleQuestionOutput(newAnswer, true, lbtnNSA);
                    break;
                default:
                    break;
            }
            radMP.SelectedIndex = 1;
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            radMP.SelectedIndex = 1;
        }

        protected void lbDel_Click(object sender, EventArgs e)
        {
            surveyDataClassDataContext.hitbl_Survey_Question_SQUs.DeleteOnSubmit(currentQuestion);
            surveyDataClassDataContext.SubmitChanges();
            Visible = false;
        }

        protected void lbtnES_Click(object sender, EventArgs e)
        {
            currentQuestion.QuestionText = txtQ.Content;
            litQ.Text = currentQuestion.QuestionText;
            surveyDataClassDataContext.SubmitChanges();
            radMP.SelectedIndex = 0;
            if (Settings.ContainsKey("ButtonNew"))
            {
                //Set the Parent's AddNew Button visibilirty to true
                LinkButton lbtn = Settings["ButtonNew"] as LinkButton;
                lbtn.Visible = true;
            }

        }

        protected void lbtnEC_Click(object sender, EventArgs e)
        {
            radMP.SelectedIndex = 0;
            if (Settings.ContainsKey("ButtonNew"))
            {
                //Set the Parent's AddNew Button visibilirty to true
                LinkButton lbtn = Settings["ButtonNew"] as LinkButton;
                lbtn.Visible = true;
            }
        }

        protected void rcbAnswerType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            radMP.SelectedIndex = 1;
            currentQuestion.QuestionType = (SurveyAnswersType)Convert.ToInt32(e.Value);
            currentQuestion.QuestionText = txtQ.Content;
            litQ.Text = txtQ.Content;
            surveyDataClassDataContext.SubmitChanges();
            phAnsList.Controls.Clear();
            LoadQuestions();
        }
    }
}