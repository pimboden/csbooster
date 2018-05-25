using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Net;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class LinkFacebookAccount : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        private string id;
        private string email;
        private string gender;
        private string firstname;
        private string lastname;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
                ScriptManager.RegisterStartupScript(UpnlLogin, UpnlLogin.GetType(), "SetFocus", String.Format("document.getElementById('{0}').focus();", this.TxtLogin.ClientID), true);*/

            var cookie = Authentication.GetSignedCookie("fbs_" + ConfigurationManager.AppSettings["FacebookApplicationId"], ConfigurationManager.AppSettings["FacebookApplicationSecret"]);
            if (cookie != null)
            {
                string jsonProfile = Http.DownloadContent((HttpWebRequest)WebRequest.Create("https://graph.facebook.com/me?locale=en_US&access_token=" + cookie["access_token"]), null);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var profile = (Dictionary<string, object>)serializer.DeserializeObject(jsonProfile);
                id = profile["id"].ToString();
                if (profile.ContainsKey("email"))
                    email = profile["email"] != null ? profile["email"].ToString() : string.Empty;
                if (profile.ContainsKey("gender"))
                    gender = profile["gender"] != null ? profile["gender"].ToString() : string.Empty;
                if (profile.ContainsKey("first_name"))
                    firstname = profile["first_name"] != null ? profile["first_name"].ToString() : string.Empty;
                if (profile.ContainsKey("last_name"))
                    lastname = profile["last_name"] != null ? profile["last_name"].ToString() : string.Empty;
            }
        }

        protected void OnCreateClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                string username = TxtLogin.Text.StripHTMLTags().Trim();

                if (!string.IsNullOrEmpty(email))
                {
                    DataObjectUser.CreateUser(AuthenticationType.FacebookConnect, id, username, email, firstname, lastname, gender);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", "$telerik.$(function() { RefreshParentPage();CloseWindow(); } );", true);
                }
            }
        }

        protected void ValidateLogin(object sender, EventArgs e)
        {
            Page.Validate("Login");
        }

        protected void ValidateLogin(object sender, ServerValidateEventArgs e)
        {
            TxtLogin.Text = Common.Extensions.StripHTMLTags(TxtLogin.Text).Trim();

            MembershipUserCollection members = Membership.FindUsersByName(TxtLogin.Text);
            if (members.Count > 0)
            {
                e.IsValid = false;
                ScriptManager.RegisterStartupScript(UpnlLogin, UpnlLogin.GetType(), "SetFocus", String.Format("document.getElementById('{0}').focus();", this.TxtLogin.ClientID), true);
            }
        }
    }
}