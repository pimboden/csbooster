//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public class Logout : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string siteMode = context.Request.QueryString["SM"] ?? "Full";
            string redirectUrl = siteMode == "Full" ? "/default.aspx" : "/mpdefault.aspx";
            MembershipUser membershipUser = Membership.GetUser(new Guid(context.Profile.GetPropertyValue("UserId").ToString()));
            if (membershipUser != null)
            {
                _4screen.CSB.DataAccess.Business.LoggedinUserHandler.DeleteUserLoggedIn(membershipUser.ProviderUserKey.ToString());

                if (!string.IsNullOrEmpty(membershipUser.Comment))
                {
                    redirectUrl = string.Format("{0}", membershipUser.Comment);
                }

                if (HttpContext.Current != null)
                {
                    string key = string.Format("UDC_{0}", membershipUser.UserName.ToLower());
                    HttpContext.Current.Items.Remove(key);
                }
            }

            List<string> cookiesToClear = new List<string>();
            foreach (string cookieName in context.Request.Cookies)
            {
                if (cookieName != "UserInfo")
                {
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    cookiesToClear.Add(cookie.Name);
                }
            }

            foreach (string name in cookiesToClear)
            {
                HttpCookie cookie = new HttpCookie(name, string.Empty);
                cookie.Expires = DateTime.Today.AddYears(-1);

                context.Response.Cookies.Set(cookie);
            }

            context.Response.Redirect(redirectUrl);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}