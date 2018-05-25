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
    public partial class SurveyPageInfo : System.Web.UI.UserControl, ISurveyWizardPage
    {
        private Data.SurveyDataClassDataContext surveyDataClassDataContext;
        private hitbl_Survey_Page_SPG currentPage;
        private int currentPageInfoTab = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            radTab.SelectedIndex = currentPageInfoTab;
            radMP.SelectedIndex =currentPageInfoTab;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string currentTab = Request.Form[hfCT.UniqueID];
            if (!string.IsNullOrEmpty(currentTab))
            {
                currentPageInfoTab = Convert.ToInt32(currentTab);
            }
            if (survey != null && Settings != null && Settings.ContainsKey("SurveyPage") && Settings.ContainsKey("surveyDataClassDataContext"))
            {
                currentPage = Settings["SurveyPage"] as hitbl_Survey_Page_SPG;
                surveyDataClassDataContext = Settings["surveyDataClassDataContext"] as SurveyDataClassDataContext;
                FillEditForm();
            }
            else
            {
                throw new SiemeArgumentException("_4screen.CSB.DataObj.UserControls.Wizards.SurveyPageInfo", "OnInit", "SurveyPage", "SurveyPage was not send with the Settings Dictionary");
            }
        }

        #region Implementation of ISurveyWizardPage

        public DataObjectSurvey survey { get; set; }

        public void FillEditForm()
        {
            if (currentPage != null)
            {
                txtTitle.Text = currentPage.Title;
                txtDesc.Content = currentPage.Description;
            }

            var allSurveyQuestions = from allPages in surveyDataClassDataContext.hitbl_Survey_Question_SQUs.Where(x => x.SPG_ID == currentPage.SPG_ID)
                                     orderby allPages.SortNumber ascending
                                     select allPages;


            int i = 0;
            foreach (hitbl_Survey_Question_SQU surveyQuestion in allSurveyQuestions)
            {
                LoadQuestionOutput(surveyQuestion, false, null);
                i++;
            }
            radMP.PageViews[1].Controls.Add(new LiteralControl("<div style='float:left'>"));
            LinkButton lbtnNQ = new LinkButton
            {
                ID = string.Format("lbtnNQ{0}", currentPage.SPG_ID),
                Text = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("LabelSurveyAddQuestion"),
                CommandArgument = (i * 10).ToString(),
                CssClass = "inputButton"
            };
            lbtnNQ.Click += new EventHandler(lbtnNQ_Click);
            radMP.PageViews[1].Controls.Add(lbtnNQ);
            radMP.PageViews[1].Controls.Add(new LiteralControl("</div><div class='clearBoth'></div>"));
            

        }

        private void LoadQuestionOutput(hitbl_Survey_Question_SQU surveyQuestion, bool editMode, LinkButton buttonNew)
        {
            string idPostFix = surveyQuestion.SQU_ID.ToString().Replace("_", "_");
            Control ucSurveyQuestion = LoadControl("/UserControls/Wizards/SurveyQuestion.ascx");
            ISurveyWizardPage iSurveyQuestion = ucSurveyQuestion as ISurveyWizardPage;
            iSurveyQuestion.survey = survey;
            iSurveyQuestion.Settings = new Dictionary<string, object>
                                           {
                                               {"SurveyQuestion", surveyQuestion},
                                               {"surveyDataClassDataContext", surveyDataClassDataContext}
                                           };
            if(editMode)
            {
                iSurveyQuestion.Settings.Add("EditMode", editMode);
                iSurveyQuestion.Settings.Add("ButtonNew", buttonNew);
                
            }
            ucSurveyQuestion.ID = string.Format("GDP_{0}", idPostFix);
            radMP.PageViews[1].Controls.Add(ucSurveyQuestion);

        }

        public bool SaveSubStep()
        {
            currentPage.Title = txtTitle.Text;
            currentPage.Description = txtDesc.Content;
            surveyDataClassDataContext.SubmitChanges();
            var surveyQuestions = radMP.Controls.OfType<ISurveyWizardPage>();
            foreach (ISurveyWizardPage surveyQuestion in surveyQuestions)
            {
                surveyQuestion.SaveSubStep();
            }
            return true;
        }


        public Dictionary<string, object> Settings { get; set; }

        #endregion

        protected void lbtnNQ_Click(object sender, EventArgs e)
        {

            hitbl_Survey_Question_SQU newQuestion = new hitbl_Survey_Question_SQU();
            newQuestion.SQU_ID = Guid.NewGuid();
            newQuestion.QuestionText = string.Format(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("DefaultQuestionText"), newQuestion.SortNumber);
            newQuestion.QuestionType = (int) SurveyAnswersType.NotSelected;
            newQuestion.SPG_ID = currentPage.SPG_ID;
            newQuestion.SortNumber = Convert.ToInt32(((LinkButton) sender).CommandArgument);
            surveyDataClassDataContext.hitbl_Survey_Question_SQUs.InsertOnSubmit(newQuestion);
            surveyDataClassDataContext.SubmitChanges();
            LinkButton lbtnNQ =  sender as LinkButton;
            lbtnNQ.Visible = false;
            LoadQuestionOutput(newQuestion, true, lbtnNQ);

        }

        protected void lbtnS_Click(object sender, EventArgs e)
        {
            SaveSubStep();
        }

        protected void lbtnD_Click(object sender, EventArgs e)
        {
            surveyDataClassDataContext.hitbl_Survey_Page_SPGs.DeleteOnSubmit(currentPage);
            surveyDataClassDataContext.SubmitChanges();
            if (Settings.ContainsKey("currentPageTab"))
            {
                //Set the Parent's AddNew Button visibilirty to true
                RadTab currentPageTab = Settings["currentPageTab"] as RadTab;
                RadTabStrip rts= currentPageTab.TabStrip;
                rts.Tabs.Remove(currentPageTab);
            }

        }

        protected void radTab_TabClick(object sender, RadTabStripEventArgs e)
        {
            radMP.SelectedIndex = e.Tab.Index;
            hfCT.Value = e.Tab.Index.ToString();
        }
    }
}