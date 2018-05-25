using System;
using System.Linq;
using System.Web;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class ProfileFriendSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.Friends);

            try
            {
                Guid userID = new Guid(Request.QueryString["UID"]);
                Guid friendID = new Guid(Request.QueryString["FID"]);

                if (FriendHandler.IsFriend(userID, friendID))
                {
                    PFS.UserID = userID;
                    PFS.FriendID = friendID;
                    if (!IsPostBack)
                        PFS.LoadSetting();
                }
                else
                {
                    Response.Redirect("/pages/static/AccessDenied.aspx", true);
                }
            }
            catch
            {
                Response.Redirect("/pages/static/AccessDenied.aspx", true);
            }
        }
    }
}