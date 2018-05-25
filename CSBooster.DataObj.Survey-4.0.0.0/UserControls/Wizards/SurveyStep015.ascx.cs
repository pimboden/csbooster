// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveyStep015 : StepsASCX
    {
        private DataObjectSurvey survey;


        protected override void  OnInit(EventArgs e)
        {
            base.OnInit(e);
            revTxtMT.ValidationExpression = Constants.REGEX_EMAIL;
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
                survey.IsContest = false;
                survey.Insert(UserDataContext.GetUserDataContext());
                survey.Title = string.Empty;
            }
            survey.SetValuesFromQuerySting();
            FillEditForm();
        }

        private void FillEditForm()
        {
            txtMT.Text = survey.MailTo;
            rblIsContest.SelectedIndex = rblIsContest.Items.IndexOf(rblIsContest.Items.FindByValue(Convert.ToInt16(survey.IsContest).ToString()));
            rblShowForm.SelectedIndex = rblShowForm.Items.IndexOf(rblShowForm.Items.FindByValue(Convert.ToInt16(survey.ShowForm).ToString()));
            rdtpActiveFrom.SelectedDate = survey.StartDate;
            if(survey.EndDate != DateTime.MaxValue)
            {
                rdtpActiveTo.SelectedDate = survey.EndDate;
            }
            FillHiddenFields();
        }

         public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                survey.MailTo = txtMT.Text;
                survey.IsContest = Convert.ToBoolean(Convert.ToInt32(rblIsContest.SelectedValue));
                survey.ShowForm = Convert.ToBoolean(Convert.ToInt32(rblShowForm.SelectedValue));
                survey.StartDate = AccessMode == AccessMode.Insert && !rdtpActiveFrom.SelectedDate.HasValue ? DateTime.Now : rdtpActiveFrom.SelectedDate.Value;
                if (rdtpActiveTo.SelectedDate.HasValue)
                {
                    survey.EndDate = rdtpActiveTo.SelectedDate.Value;
                }
                GetHiddenFields();
                survey.Update(UserDataContext.GetUserDataContext());
                return true;
            }
             catch(Exception ex)
             {
                 this.litMsg.Text = "Fehler beim Speichern: " + ex.Message;
                 return false;

             }
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
    }
}