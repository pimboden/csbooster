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
using Telerik.Web.UI;
using DataObjectSurvey=_4screen.CSB.DataObj.Business.DataObjectSurvey;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class SurveyStep03 : StepsASCX
    {
        private DataObjectSurvey survey;
        private SurveyDataClassDataContext surveyDataClassDataContext;
        private int currentPageTabIndex = 0;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string currentTab = Request.Form[hfCT.UniqueID];
            if (!string.IsNullOrEmpty(currentTab))
            {
                currentPageTabIndex = int.Parse(currentTab);
            }
            survey = DataObject.Load<DataObjectSurvey>(ObjectID, null, true);
            surveyDataClassDataContext = new SurveyDataClassDataContext();
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
            FillHiddenFields();

            //Add the "AddNewPage" Tab
            RadPageView rpAddNew = new RadPageView
                                       {
                                           ID = "NewPage"
                                       };
            radMultiPage.PageViews.Add(rpAddNew);
            radTab.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("TabAddNewPage"), "NewPage"));

            var allSurveyPages = from allPages in surveyDataClassDataContext.hitbl_Survey_Page_SPGs.Where(x => x.OBJ_ID == survey.ObjectID)
                                 orderby allPages.SortNumber ascending
                                 select allPages;

            if (allSurveyPages.Count() == 0)
            {
                //One Page schould allways exits.. so createit
                hitbl_Survey_Page_SPG firstPage = new hitbl_Survey_Page_SPG
                                                      {
                                                          OBJ_ID = survey.ObjectID.Value,
                                                          Title = GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("StartPageName"),
                                                          SortNumber = 0,
                                                          SPG_ID = Guid.NewGuid()
                                                      };
                surveyDataClassDataContext.hitbl_Survey_Page_SPGs.InsertOnSubmit(firstPage);
                surveyDataClassDataContext.SubmitChanges();
                allSurveyPages = from allPages in surveyDataClassDataContext.hitbl_Survey_Page_SPGs.Where(x => x.OBJ_ID == survey.ObjectID)
                                 orderby allPages.SortNumber ascending
                                 select allPages;

            }
            int i = 0;
            foreach (hitbl_Survey_Page_SPG surveyPage in allSurveyPages)
            {
                CreateTabContext(i++, surveyPage);
            }

        }

        private void CreateTabContext(int tabIndex, hitbl_Survey_Page_SPG surveyPage)
        {
            string idPostFix = surveyPage.SPG_ID.ToString().Replace("_", "_");
            radTab.Tabs.Insert(tabIndex, new RadTab(surveyPage.Title, surveyPage.SPG_ID.ToString()));
            RadPageView rpvw = new RadPageView
                                   {
                                       ID = string.Format("RPW_{0}", idPostFix)
                                   };
            radMultiPage.PageViews.Insert(tabIndex, rpvw);
            if (currentPageTabIndex == tabIndex)
            {
                InsertControlInPageView(tabIndex, surveyPage);

            }


        }

        private void InsertControlInPageView(int pageIndex, hitbl_Survey_Page_SPG surveyPage)
        {
            string idPostFix = surveyPage.SPG_ID.ToString().Replace("_", "_");
            radMultiPage.PageViews[pageIndex].Controls.Clear();
            Control ucSurveyPageInfo = LoadControl("/UserControls/Wizards/SurveyPageInfo.ascx");
            ISurveyWizardPage iSurveyPageInfo = ucSurveyPageInfo as ISurveyWizardPage;
            iSurveyPageInfo.survey = survey;
            iSurveyPageInfo.Settings = new Dictionary<string, object>
                                               {
                                                   {"SurveyPage", surveyPage},
                                                   {"surveyDataClassDataContext", surveyDataClassDataContext},
                                                   {"currentPageTab", radTab.Tabs[pageIndex]}

                                               };
            ucSurveyPageInfo.ID = string.Format("GDP_{0}", idPostFix);
            radMultiPage.PageViews[pageIndex].Controls.Add(ucSurveyPageInfo);
        }


        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                GetHiddenFields();
                if (AccessMode == AccessMode.Insert)
                    survey.ShowState = ObjectShowState.Draft;
                survey.Update(UserDataContext.GetUserDataContext());
                return true;
            }
            catch (Exception ex)
            {
                this.litMsg.Text = "Fehler beim Speichern: " + ex.Message;
                return false;
            }
        }


        protected void radTab_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Value.Equals("NewPage", StringComparison.InvariantCultureIgnoreCase))
            {
                int tabIndex = e.Tab.Index;
                //Create the Page
                hitbl_Survey_Page_SPG surveyPageSpg = new hitbl_Survey_Page_SPG();
                surveyPageSpg.SPG_ID = Guid.NewGuid();
                surveyPageSpg.OBJ_ID = survey.ObjectID.Value;
                surveyPageSpg.SortNumber = (tabIndex) * 10;
                surveyPageSpg.Title = string.Format("{0} {1}", GuiLanguage.GetGuiLanguage("DataObjectSurvey").GetString("DefaultPageName"), tabIndex);
                surveyDataClassDataContext.hitbl_Survey_Page_SPGs.InsertOnSubmit(surveyPageSpg);
                surveyDataClassDataContext.SubmitChanges();
                CreateTabContext(tabIndex, surveyPageSpg);
                radTab.SelectedIndex = tabIndex;
            }
            else
            {
                var surveyPage = (from allPages in surveyDataClassDataContext.hitbl_Survey_Page_SPGs.Where(x => x.OBJ_ID == survey.ObjectID)
                                     orderby allPages.SortNumber ascending
                                     select allPages).ToList().ElementAtOrDefault(e.Tab.Index);
                if(surveyPage!=null)
                    InsertControlInPageView(e.Tab.Index, surveyPage);
            }
            radMultiPage.SelectedIndex = radTab.SelectedIndex;
            hfCT.Value = e.Tab.Index.ToString();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            radTab.SelectedIndex = currentPageTabIndex;
            radMultiPage.SelectedIndex = currentPageTabIndex;
        }
    }
}