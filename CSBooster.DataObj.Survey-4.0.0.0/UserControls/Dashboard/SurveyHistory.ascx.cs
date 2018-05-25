// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataObj.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataObj.UserControls.Dashboard
{
    public partial class SurveyHistory : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            hidUserId.Value = UserDataContext.GetUserDataContext().UserID.ToString();
            sqlDS.ConnectionString = Helper.GetSiemeConnectionString(); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetImage(object light)
        {
            return string.Format("/library/Images/layout/SurveyRes_{0}.jpg", light);
        }

        protected string GetLink(object objectId)
        {
            SiteObjectType siteObjectType = Helper.GetObjectType("Survey");
            string localizationBaseFileName = "Wizards";

            if (!string.IsNullOrEmpty(siteObjectType.LocalizationBaseFileName))
            {
                localizationBaseFileName = siteObjectType.LocalizationBaseFileName;
            }

            string returnValue =GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString("TestNoLongerAvailable");
            Guid dataObjectId = new Guid(objectId.ToString());
            DataObjectSurvey dataObjectSurvey = DataObject.Load<DataObjectSurvey>(dataObjectId);
            if(dataObjectSurvey.State != ObjectState.Added)
            {
                returnValue = string.Format("<a href='{0}' class='inputButton'>{1}</a>",Helper.GetDetailLink( Helper.GetObjectTypeNumericID("Survey"), dataObjectId.ToString()), GuiLanguage.GetGuiLanguage(localizationBaseFileName).GetString("GoToTestLinkText"));

            }
            return returnValue;
        }
    }
}