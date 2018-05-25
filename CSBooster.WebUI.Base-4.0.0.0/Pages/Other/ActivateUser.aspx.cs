//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #0.0.0.0    26.03.2007 / PI
//  Updated:   #0.4.0.0    20.04.2007 / TS
//									- scrambled activation routine replaced by a guid (was to buggy)
//  Updated:   #0.5.2.0    09.07.2007 / TS
//									- fixer Aktivierungscode für Promo's. Kann im Web.config definiert werden (blank = nicht beachtet) 
//  Updated:   #1.0.5.0    19.12.2007 / AW
//									- Set profile defaults (ads on)
//******************************************************************************
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
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public partial class ActivateUser : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Other.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.QueryString["U"];
            string activationCode = Request.QueryString["C"];
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(activationCode))
            {
                username = Server.UrlDecode(username);
                activationCode = Server.UrlDecode(activationCode);

                if (DoActivate(username, activationCode, false))
                {
                    MembershipUser membershipUser = Membership.GetUser(username, false);
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (!string.IsNullOrEmpty(membershipUser.Comment))
                    {
                        Response.Redirect(string.Format("{0}?U={1}", membershipUser.Comment, Request.QueryString["U"]));
                    }
                    else
                    {
                        Response.Redirect(Helper.GetDetailLink("User", username, false) + "&P=Dashboard");
                    }
                }

            }
            else
            {
                pnlError.Visible = false;
                pnlActivate.Visible = true;
            }
        }

        private static void SetProfileDefaults(Guid userId)
        {
            try
            {
                DataObjectUser user = DataObject.Load<DataObjectUser>(userId);
                user.DisplayAds = true;
                user.Update(UserDataContext.GetUserDataContext());
            }
            catch
            { }
        }

        private bool DoActivate(string username, string activationCode, bool manualLogin)
        {
            bool userCreated = false;

            MembershipUser membershipUser = Membership.GetUser(username, false);

            if (membershipUser != null)
            {
                string userId = membershipUser.ProviderUserKey.ToString();
                AspnetMembership membership = AspnetMembership.FetchByID(new Guid(userId));

                if (membership.PasswordQuestion == activationCode || (manualLogin && ConfigurationManager.AppSettings["PromoActivationCode"].ToLower().Trim() == activationCode.ToLower().Trim()))
                {
                    membership.PasswordQuestion = "-";
                    membership.Save();
                    Roles.RemoveUserFromRole(username, "NotActivated");
                    Roles.AddUserToRole(username, "Basic");
                    DataObjectUser.CreateUser(UserDataContext.GetUserDataContext(), username);
                    UserProfile userProfile = UserProfile.GetProfile(username);
                    if (userProfile == null)
                    {
                        userProfile = (UserProfile)UserProfile.Create(username);
                        userProfile.UserId = new Guid(membershipUser.ProviderUserKey.ToString());
                        userProfile.Nickname = username;
                    }
                    Community.CreateUserProfileCommunity(UserDataContext.GetUserDataContext(), userProfile);
                    DataObjectUser.JoinCommunities(userId, _4screen.CSB.Common.SiteConfig.GetSiteContext(UserProfile.Current));
                    DataObjectUser.AddDefaultFriends(userId);
                    _4screen.CSB.Extensions.Business.IncentivePointsManager.AddIncentivePointEvent("USER_ACTIVATE", UserDataContext.GetUserDataContext());
                    SetProfileDefaults(new Guid(userId));
                    userCreated = true;
                }
                else if (membership.PasswordQuestion != activationCode)
                {
                    lblInfo.Text += language.GetString("MessageActivatCodeInvalid");
                    pnlError.Visible = true;
                    pnlActivate.Visible = false;
                }
            }
            else
            {
                lblInfo.Text += language.GetString("MessageActivatUserInvalid");
                pnlError.Visible = true;
                pnlActivate.Visible = false;
            }

            return userCreated;
        }

        protected void BtnActivate_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;

            string username = Server.UrlDecode(Request.QueryString["U"]);

            if (DoActivate(username, TxtActivateCode.Text.TrimEnd(' ').TrimStart(' '), true))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect(Helper.GetDetailLink("User", username, false) + "&P=Dashboard");
            }
            else
            {
                pnlError.Visible = true;
            }
        }
    }
}