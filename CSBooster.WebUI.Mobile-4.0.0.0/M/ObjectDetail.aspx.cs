// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.CSB.WebUI.M.UserControls;

namespace _4screen.CSB.WebUI.M
{
    public partial class ObjectDetail : System.Web.UI.Page
    {
        private DataObject dataObject;

        protected void Page_Load(object sender, EventArgs e)
        {
            var objectType = Helper.GetObjectType(Request.QueryString["OT"]);
            dataObject = DataObject.LoadByReflection(Request.QueryString["OID"].ToGuid(), objectType.NumericId);

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(dataObject.CommunityID, dataObject.objectType, IsPostBack, LogSitePageType.Detail);
            dataObject.AddViewed(UserDataContext.GetUserDataContext());

            if (UserProfile.Current.UserId != dataObject.UserID.Value)
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("OBJECT_VIEWED_MOBILE_DEVICE", UserDataContext.GetUserDataContext(), dataObject.ObjectID.Value.ToString());

            Control control = !string.IsNullOrEmpty(objectType.MobileDetailsCtrl) ? LoadControl(objectType.MobileDetailsCtrl) : LoadControl("/M/UserControls/Templates/DetailsObject.ascx");
            ((IDataObjectWorker)control).DataObject = dataObject;
            PhCnt.Controls.Add(control);

            if (dataObject.Title != null)
                Master.Page.Title = SiteConfig.SiteName + " - " + _4screen.Utils.Extensions.EscapeForXHTML(dataObject.Title);
        }
    }
}