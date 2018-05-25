// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Collections.Generic; 

//using _4screen.CSB.Notification.Business;

namespace _4screen.CSB.Common
{
    /// <summary>
    /// Summary description for UserProfile
    /// </summary>
    [Serializable]
    public class UserProfile : System.Web.Profile.ProfileBase
    {
        public static UserProfile Current
        {
            get { return (UserProfile) HttpContext.Current.Profile; }
        }

        public static UserProfile GetProfile(string username)
        {
            return (UserProfile) System.Web.Profile.ProfileBase.Create(username);
        }

       [SettingsAllowAnonymous(true)]
        public virtual string Nickname
        {
            get { return ((string) (GetPropertyValue("Nickname"))); }
            set { SetPropertyValue("Nickname", value); }
        }

        [SettingsAllowAnonymous(true)]
        public Guid UserId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated && (string.IsNullOrEmpty(GetPropertyValue("UserId").ToString()) || new Guid(GetPropertyValue("UserId").ToString()) == Guid.Empty))
                {
                    MembershipUser mUser = Membership.GetUser(HttpContext.Current.User.Identity.Name, false);
                    if (mUser != null)
                    {
                        SetPropertyValue("UserId", new Guid(mUser.ProviderUserKey.ToString()));
                        Save();
                    }
                }
                return new Guid(GetPropertyValue("UserId").ToString());
            }
            set { SetPropertyValue("UserId", value); }
        }

        [SettingsAllowAnonymous(true)]
        public Guid ProfileCommunityID
        {
            get { return ((Guid) (GetPropertyValue("ProfileCommunityID"))); }
            set { SetPropertyValue("ProfileCommunityID", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string UserPictureURL
        {
            get
            {
                string strFilePath = GetPropertyValue("UserPictureURL") as string;
                if (string.IsNullOrEmpty(strFilePath))
                {
                    strFilePath = string.Format("{0}", Helper.GetObjectType("User").DefaultImageURL);
                    SetPropertyValue("UserPictureURL", strFilePath);
                }
                return strFilePath;
            }
            set { SetPropertyValue("UserPictureURL", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string OpenID
        {
            get { return (string) GetPropertyValue("OpenID"); }
            set { SetPropertyValue("OpenID", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string MyContentStyle
        {
            get { return (string)GetPropertyValue("MyContentStyle"); }
            set { SetPropertyValue("MyContentStyle", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string PrefferedCulture
        {
            get { return (string) GetPropertyValue("PrefferedCulture"); }
            set { SetPropertyValue("PrefferedCulture", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string Setting1
        {
            get { return (string)GetPropertyValue("Setting1"); }
            set { SetPropertyValue("Setting1", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string Setting2
        {
            get { return (string)GetPropertyValue("Setting2"); }
            set { SetPropertyValue("Setting2", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string Setting3
        {
            get { return (string)GetPropertyValue("Setting3"); }
            set { SetPropertyValue("Setting3", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string Setting4
        {
            get { return (string)GetPropertyValue("Setting4"); }
            set { SetPropertyValue("Setting4", value); }
        }

        [SettingsAllowAnonymous(true)]
        public string Setting5
        {
            get { return (string)GetPropertyValue("Setting5"); }
            set { SetPropertyValue("Setting5", value); }
        }
    }
}
