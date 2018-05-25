using System;
using System.Web;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
            LogManager.WriteEntry();
        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["UserInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("UserInfo");
                cookie.Expires = DateTime.Now.AddYears(10);
            }

            UserDataContext udc = UserDataContext.GetUserDataContext();
            if (CustomizationSection.CachedInstance.Modules["MobileUI"].Enabled && udc.IsMobileDevice && string.IsNullOrEmpty(cookie["SM"]) && !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("mpdefault.aspx") && !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("/m/"))
            {
                cookie["SM"] = "Mobile";
                Response.Cookies.Add(cookie);
                Response.Redirect("/mpdefault.aspx");
            }
            else
            {
                cookie["BI"] = Guid.NewGuid().ToString();
                Response.Cookies.Add(cookie);
            }
        }

        void Session_End(object sender, EventArgs e)
        {

        }

        void Application_PostMapRequestHandler(object sender, EventArgs e)
        {
            try
            {
                string originalUrl = Request.ServerVariables["HTTP_X_REWRITE_URL"];
                string currentUrl = Request.Url.PathAndQuery;

                if (originalUrl != null && originalUrl != currentUrl && !originalUrl.EndsWith("/"))
                {
                    Context.RewritePath(originalUrl, true);
                }
            }
            catch { }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                LoggedinUserHandler.InsertUpdateUserLoggedIn(HttpContext.Current.Profile.GetPropertyValue("UserId").ToString());
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.RawUrl.Contains(".aspx"))
            {
                string preferredCulture = null;
                if (HttpContext.Current.Profile != null)
                    preferredCulture = ((UserProfile)HttpContext.Current.Profile).PrefferedCulture;
                string currentCulture = CultureHandler.SetCurrentCulture(preferredCulture);
                if (HttpContext.Current.Profile != null && ((UserProfile)HttpContext.Current.Profile).PrefferedCulture.ToLower() != currentCulture.ToLower())
                {
                    ((UserProfile)HttpContext.Current.Profile).PrefferedCulture = currentCulture;
                    ((UserProfile)HttpContext.Current.Profile).Save();
                }
            }
            if (Request.HttpMethod == "GET")
            {
                if (Request.AppRelativeCurrentExecutionFilePath.EndsWith(".aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("formshieldhttphandler.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("telerik.web.ui.dialoghandler.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("cropimage.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("wizard.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("widgetsettings.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("useradmin.aspx") &&
                    !Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("resourceeditdialog.aspx") &&
                    !Request.RawUrl.ToLower().Contains("edit=content") &&
                    !Request.RawUrl.ToLower().Contains("edit=style") &&
                    !Request.RawUrl.ToLower().Contains("cn=wizard") &&
                    !Request.RawUrl.ToLower().Contains("p=dashboard"))
                {
                    Response.Filter = new ScriptDeferFilter(Response);
                }
            }
        }
    }
}