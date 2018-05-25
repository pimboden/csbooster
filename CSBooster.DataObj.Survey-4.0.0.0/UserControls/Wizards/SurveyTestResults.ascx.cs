// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataObj.Data;
using _4screen.CSB.DataObj.SurveyCommon;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveyTestResults : System.Web.UI.UserControl, ISurveyWizardPage
    {
        private SurveyDataClassDataContext surveyDataClassDataContext;
        private hitbl_Survey_TestResult_STR currentTestResult;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Settings != null && Settings.ContainsKey("TestResult"))
            {
                currentTestResult = Settings["TestResult"] as hitbl_Survey_TestResult_STR;
                surveyDataClassDataContext = Settings["surveyDataClassDataContext"] as SurveyDataClassDataContext;
                MakeValidatorGroup();
                FillEditForm();
            }
            else
            {
                throw new SiemeArgumentException("_4screen.CSB.DataObj.UserControls.Wizards.SurveyTestResults", "OnInit", "TestResult", "TestResult was not send with the Settings Dictionary");
            }
        }

        private void MakeValidatorGroup()
        {

            string validationGroup = string.Format("VG_{0}", currentTestResult.STR_ID);
            txtT.ValidationGroup = validationGroup;
            txtPE.ValidationGroup = validationGroup;
            txtPS.ValidationGroup = validationGroup;
            rfvPE.ValidationGroup = validationGroup;
            rfvPS.ValidationGroup = validationGroup;
            rngPE.ValidationGroup = validationGroup;
            rngPS.ValidationGroup = validationGroup;
            lbOK.ValidationGroup = validationGroup;
            rngPS.MinimumValue = Int32.MinValue.ToString();
            rngPS.MaximumValue = Int32.MaxValue.ToString();
            rngPE.MinimumValue = Int32.MinValue.ToString();
            rngPE.MaximumValue = Int32.MaxValue.ToString();
            lbOK.CausesValidation = true;
            lbDel.CausesValidation = false;

        }

        protected void lbOK_Click(object sender, EventArgs e)
        {
            if (currentTestResult == null)
            {
                currentTestResult = new hitbl_Survey_TestResult_STR
                                 {
                                     STR_ID = Guid.NewGuid()
                                 };
                currentTestResult.OBJ_ID = survey.ObjectID.Value;
                surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.InsertOnSubmit(currentTestResult);
            }
            currentTestResult.ResultText = txtT.Text;
            double value;
            currentTestResult.ValueFrom = double.TryParse(txtPS.Text, out value) ? value : 0;
            currentTestResult.ValueTo = double.TryParse(txtPE.Text, out value) ? value : 0;
            surveyDataClassDataContext.SubmitChanges();
        }


        protected void lbDel_Click(object sender, EventArgs e)
        {
            surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.DeleteOnSubmit(currentTestResult);
            surveyDataClassDataContext.SubmitChanges();
            Visible = false;
        }

        #region Implementation of ISurveyWizardPage

        public void FillEditForm()
        {
            if (currentTestResult == null)
            {
                currentTestResult = new hitbl_Survey_TestResult_STR
                                 {
                                     STR_ID = Guid.NewGuid(),
                                     OBJ_ID = survey.ObjectID.Value,
                                 };
                surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.InsertOnSubmit(currentTestResult);
                surveyDataClassDataContext.SubmitChanges();
            }
            txtT.Text = currentTestResult.ResultText ?? string.Empty;
            txtPS.Text = currentTestResult.ValueFrom.HasValue ? currentTestResult.ValueFrom.Value.ToString() : string.Empty;
            txtPE.Text = currentTestResult.ValueTo.HasValue ? currentTestResult.ValueTo.Value.ToString() : string.Empty;

        }
        public Dictionary<string, object> Settings { get; set; }

        public DataObjectSurvey survey { get; set; }

        public bool SaveSubStep()
        {
            currentTestResult.ResultText = txtT.Text;
            double value;
            currentTestResult.ValueFrom = double.TryParse(txtPS.Text, out value) ? value : 0;
            currentTestResult.ValueTo = double.TryParse(txtPE.Text, out value) ? value : 0;
            surveyDataClassDataContext.SubmitChanges();
            return true;
        }

        #endregion
    }
}