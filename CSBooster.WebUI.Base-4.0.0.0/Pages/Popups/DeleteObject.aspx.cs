using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Extensions.Business;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class DeleteObject : System.Web.UI.Page
    {
        protected UserDataContext udc;
        bool hasCallback = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            udc = UserDataContext.GetUserDataContext();

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.MyContent);

            int objectType = Convert.ToInt32(Request.QueryString["type"]);

            DataObject dataObject = DataObject.Load<DataObject>(Request.QueryString["Id"].ToGuid(), null, true);
            if (dataObject.State != ObjectState.Added)
            {
                if (CustomizationSection.CachedInstance.MyContent.DeleteEvent == DeleteEvents.Delete)
                {
                    dataObject.Delete(udc);
                }
                else if (CustomizationSection.CachedInstance.MyContent.DeleteEvent == DeleteEvents.StatusDeleted)
                {
                    dataObject.MarkAsDeleted(udc);
                }
                _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent(string.Format("{0}_DELETE", objectType.ToString().ToUpper()), udc, dataObject.ObjectID.Value.ToString());
                TrackingManager.TrackObjectEvent(dataObject.ObjectType, dataObject.ObjectID, TrackRule.Deleted, string.Empty);
            }

            litScript.Text = "<script type ='text/javascript'>$telerik.$(function() { CloseWindow(); } );</script>";
        }
    }
}