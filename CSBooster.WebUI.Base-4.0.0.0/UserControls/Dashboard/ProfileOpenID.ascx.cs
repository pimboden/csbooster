// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;
using ExtremeSwank.OpenId;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileOpenID : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        private DataObjectUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);
            if (user.State != ObjectState.Added)
            {
                if (!string.IsNullOrEmpty(user.OpenID))
                    this.LitOpenIDCur.Text = user.OpenID;
                else
                    this.LitOpenIDCur.Text = language.GetString("MessageOpenIdNone");
            }

            if (!IsPostBack)
            {
                OpenIdClient openID = new OpenIdClient();

                switch (openID.RequestedMode)
                {
                    case RequestedMode.IdResolution:
                        openID.Identity = UserProfile.Current.OpenID;
                        if (openID.ValidateResponse())
                        {
                            OpenIdUser openIDUser = openID.RetrieveUser();
                            user.OpenID = openIDUser.Identity;
                            user.Update(UserDataContext.GetUserDataContext());
                            UserProfile.Current.OpenID = string.Empty;
                            UserProfile.Current.Save();
                            this.LitOpenIDMsg.Text = language.GetString("MessageOpenIdConfirmed");
                            this.LitOpenIDCur.Text = user.OpenID;
                        }
                        else
                        {
                            this.LitOpenIDMsg.Text = language.GetString("MessageOpenIdNotConfirmed");
                        }
                        break;
                    case RequestedMode.CanceledByUser:
                        this.LitOpenIDMsg.Text = language.GetString("MessageOpenIdCanceled");
                        break;
                }
            }
        }

        public void LbtnOpenIDAddClick(object sender, EventArgs e)
        {
            OpenIdClient openID = new OpenIdClient();
            openID.Identity = this.TxtOpenIDNew.Text;
            UserProfile.Current.OpenID = openID.Identity;
            UserProfile.Current.Save();
            //openID.AuthVersion = ProtocolVersion.V2_0;
            //openID.ReturnUrl = new Uri(string.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Request.Path));
            //openID.BeginAuth();
            openID.CreateRequest(false, true);
        }

        public void LbtnOpenIDRemoveClick(object sender, EventArgs e)
        {
            user.OpenID = string.Empty;
            user.Update(UserDataContext.GetUserDataContext());
            this.LitOpenIDMsg.Text = language.GetString("MessageOpenIdRemoved");
            this.LitOpenIDCur.Text = "";
        }
    }
}