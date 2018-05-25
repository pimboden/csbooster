// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Data;
using _4screen.CSB.DataObj.SurveyCommon;
using _4screen.Utils.Web;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveyStep02 : StepsASCX
    {
        private DataObjectSurvey survey;
        private SurveyDataClassDataContext surveyDataClassDataContext;
        private int numberOfResultTexts = 0;
        private string headerText =  string.Format(@"<div class=""CSB_Survey_TestResultItem""><div class='col1'>{0}</div><div class='col2'>{1}</div><div class='col3'>{2}</div><div class='col4'>&nbsp;</div></div><div class='clearBoth'></div>", GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("TestauswertungText"), GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("FromPoints"), GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("ToPoints"));
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            survey = DataAccess.Business.DataObject.Load<DataObjectSurvey>(ObjectID, null, true);
            if (survey.State == ObjectState.Added)
            {
                survey.ObjectID = ObjectID;
                survey.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                survey.CommunityID = CommunityID;
                survey.ShowState = ObjectShowState.InProgress;
                survey.PunkteGelb = 0;
                survey.PunkteGruen = 0;
                survey.PunkteRot = 0;
                survey.Insert(UserDataContext.GetUserDataContext());
                survey.Title = string.Empty;
            }
            survey.SetValuesFromQuerySting();
            FillEditForm();
        }

        private void FillEditForm()
        {
            txtG.Text = survey.PunkteGruen.ToString();
            txtY.Text = survey.PunkteGelb.ToString();
            txtR.Text = survey.PunkteRot.ToString();
            LoadTestResults();
            FillHiddenFields();
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
               double value = 0;
                survey.PunkteGruen = double.TryParse(txtG.Text, out value) ? value : 0;
                survey.PunkteGelb = double.TryParse(txtY.Text, out value) ? value : 0;
                survey.PunkteRot = double.TryParse(txtR.Text, out value) ? value : 0;
                if (AccessMode == AccessMode.Insert)
                    survey.StartDate = DateTime.Now;
                GetHiddenFields();
                survey.Update(UserDataContext.GetUserDataContext());
                var allResults = phTextResults.Controls.OfType<ISurveyWizardPage>();
                foreach (ISurveyWizardPage surveyWizardPage in allResults)
                {
                    surveyWizardPage.SaveSubStep();
                }
                return true;
            }
            catch (Exception ex)
            {
                this.litMsg.Text = "Fehler beim Speichern: " + ex.Message;
                return false;
            }
        }


        private void LoadTestResults()
        {
            surveyDataClassDataContext = new SurveyDataClassDataContext();
            var allTestResults = from allResults in surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.Where(x=>x.OBJ_ID == survey.ObjectID)
                                 select allResults;
            numberOfResultTexts = allTestResults.Count();
            if (numberOfResultTexts > 0)
            {
                phTextResults.Controls.Add(new LiteralControl(headerText));

                foreach (hitbl_Survey_TestResult_STR testRes in allTestResults)
                {
                    LoadTestResultsControl(testRes);

                }

            }
        }

        private void LoadTestResultsControl(hitbl_Survey_TestResult_STR testResultObject)
        {
            Control testResultControl = LoadControl("/UserControls/Wizards/SurveyTestResults.ascx");
            ISurveyWizardPage iTestResultControl = testResultControl as ISurveyWizardPage;
            iTestResultControl.Settings = new Dictionary<string, object>
                                                  {
                                                      {"TestResult", testResultObject},
                                                      {"surveyDataClassDataContext", surveyDataClassDataContext},
                                                  };
            iTestResultControl.survey = survey;
            phTextResults.Controls.Add(testResultControl);

        }

        private void FillHiddenFields()
        {
            HFTagWords.Value = survey.TagList.Replace(Constants.TAG_DELIMITER, ',');
            HFStatus.Value = ((int)survey.Status).ToString();
            HFShowState.Value = ((int)survey.ShowState).ToString();
            HFCopyright.Value = survey.Copyright.ToString();
            if (survey.Geo_Lat != double.MinValue && survey.Geo_Long != double.MinValue)
            {
                HFGeoLat.Value = survey.Geo_Lat.ToString();
                HFGeoLong.Value = survey.Geo_Long.ToString();
            }
            HFZip.Value = survey.Zip;
            HFCity.Value = survey.City;
            HFRegion.Value = survey.Region;
            HFCountry.Value = survey.CountryCode;
        }

        private void GetHiddenFields()
        {
            survey.TagList = HFTagWords.Value.StripHTMLTags();
            survey.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), HFStatus.Value);
            survey.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), HFShowState.Value);
            survey.Copyright = int.Parse(HFCopyright.Value);
            double geoLat;
            if (double.TryParse(HFGeoLat.Value, out geoLat))
                survey.Geo_Lat = geoLat;
            double geoLong;
            if (double.TryParse(HFGeoLong.Value, out geoLong))
                survey.Geo_Long = geoLong;
            survey.Zip = HFZip.Value;
            survey.City = HFCity.Value;
            survey.Region = HFRegion.Value;
            survey.CountryCode = HFCountry.Value;
        }

        protected void lbtnNT_OnClick(object sender, EventArgs e)
        {
            if (numberOfResultTexts == 0)
            {
                phTextResults.Controls.Add(new LiteralControl(headerText));
            }
            hitbl_Survey_TestResult_STR testResult = new hitbl_Survey_TestResult_STR
                                                         {
                                                             OBJ_ID = survey.ObjectID.Value,
                                                             STR_ID = Guid.NewGuid()
                                                         };
            surveyDataClassDataContext.hitbl_Survey_TestResult_STRs.InsertOnSubmit(testResult);
            surveyDataClassDataContext.SubmitChanges();
            LoadTestResultsControl(testResult);
        }
    }
}