// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;

namespace _4screen.CSB.Common
{
    public class UserDataContext
    {
        private Guid userId = new Guid(Constants.ANONYMOUS_USERID);
        private string[] userRoles = new string[] { "ANONYMOUS" };
        private int userRolesFlag = 0;
        private string nickname = string.Empty;

        public Guid AnonymousUserId { get; internal set; }
        public bool IsAuthenticated { get; internal set; }

        public static UserDataContext GetUserDataContext()
        {
            if (HttpContext.Current != null)
                return GetUserDataContext(HttpContext.Current.User.Identity.Name);
            else
                return GetUserDataContext("ANONYMOUS");
        }

        public static UserDataContext GetUserDataContext(string username)
        {
            if (HttpContext.Current != null)
            {
                string key = string.Format("UDC_{0}", username.ToLower());
                UserDataContext udc = HttpContext.Current.Items[key] as UserDataContext;
                if (udc == null)
                {
                    udc = new UserDataContext(username);
                    HttpContext.Current.Items[key] = udc;
                }
                return udc;
            }
            else
            {
                return new UserDataContext(username);
            }
        }

        private UserDataContext(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    MembershipUser memUser = Membership.GetUser(username);
                    if (memUser != null)
                    {
                        userId = memUser.ProviderUserKey.ToString().ToGuid();
                        nickname = username;
                        userRolesFlag = 1;
                        IsAuthenticated = true;
                    }
                }
                catch
                {
                    if (username.ToLower() == "admin")
                    {
                        GetAdminValues();
                    }
                    else
                    {
                        GetAnonymousValues();
                    }
                }
            }
            else
            {
                GetAnonymousValues();
            }
        }

        public HttpContext CurrentContext
        {
            get { return HttpContext.Current; }
        }

        public bool IsAdmin
        {
            get
            {
                foreach (string role in UserRoles)
                {
                    if (role.ToLower() == "admin")
                        return true;
                }
                return false;
            }
        }

        private void GetAdminValues()
        {
            userId = Constants.ADMIN_USERID.ToGuid();
            userRoles = new string[] { "ADMIN" };
            nickname = "Admin";
            userRolesFlag = -1;
            IsAuthenticated = true;
            if (HttpContext.Current != null && HttpContext.Current.Request.AnonymousID != null)
                AnonymousUserId = HttpContext.Current.Request.AnonymousID.ToGuid();
        }

        private void GetAnonymousValues()
        {
            userId = Constants.ANONYMOUS_USERID.ToGuid();
            userRoles = new string[] { "ANONYMOUS" };
            nickname = "ANONYMOUS";
            userRolesFlag = -1;
            IsAuthenticated = false;
            if (HttpContext.Current != null && HttpContext.Current.Request.AnonymousID != null)
                AnonymousUserId = HttpContext.Current.Request.AnonymousID.ToGuid();
        }

        public Guid UserID
        {
            get { return userId; }
        }

        public string Nickname
        {
            get { return nickname; }
        }

        public string[] UserRoles
        {
            get
            {
                if (userRolesFlag == 1)
                    InitUserRoles(nickname);
                return userRoles;
            }
        }

        public string UserRole
        {
            get
            {
                if (userRolesFlag == 1)
                    InitUserRoles(nickname);
                return String.Join(",", userRoles);
            }
        }

        public string UserSessionID
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                    return HttpContext.Current.Session.SessionID;
                else
                    return string.Empty;
            }
        }

        public Guid? BrowserID
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
                    if (cookie != null)
                    {
                        return cookie["BI"].ToNullableGuid();
                    }
                }

                return null;
            }
        }

        public string UserIP
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.UserHostAddress;
                else
                    return string.Empty;
            }
        }

        public bool IsMobileDevice
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.Browser.IsMobileDevice || IsIPhone || IsAndroid;
                else
                    return false;
            }
        }

        public bool IsIPhone
        {
            get
            {
                if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
                    return HttpContext.Current.Request.UserAgent.ToLower().Contains("iphone");
                else
                    return false;
            }
        }

        public bool IsIPad
        {
            get
            {
                if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
                    return HttpContext.Current.Request.UserAgent.ToLower().Contains("ipad");
                else
                    return false;
            }
        }

        public bool IsAndroid
        {
            get
            {
                if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
                    return HttpContext.Current.Request.UserAgent.ToLower().Contains("android");
                else
                    return false;
            }
        }

        public string BrowserType
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.Browser.Type;
                else
                    return string.Empty;
            }
        }

        public string BrowserVersion
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.Browser.Version;
                else
                    return string.Empty;
            }
        }

        public string SystemType
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request.Browser.Platform;
                else
                    return string.Empty;
            }
        }

        public string SystemVersion
        {
            get
            {
                if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
                {
                    string systemVersion = HttpContext.Current.Request.UserAgent;
                    try
                    {
                        systemVersion = systemVersion.Substring(systemVersion.IndexOf("(") + 1);
                        systemVersion = systemVersion.Substring(0, systemVersion.IndexOf(")"));
                    }
                    catch { }
                    return systemVersion.CropString(60);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string UserLanguages
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request.UserLanguages != null)
                {
                    try { return string.Join(",", HttpContext.Current.Request.UserLanguages); }
                    catch { return string.Empty; }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void InitUserRoles(string UserName)
        {
            userRolesFlag = -1;
            string currentRoles = string.Empty;
            SqlCommand command = new SqlCommand();
            SqlConnection connection = new SqlConnection(Helper.GetSiemeConnectionString());
            SqlDataReader dataReader = null;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "aspnet_UsersInRoles_GetRolesForUser";
            command.Parameters.Add(new SqlParameter("@ApplicationName", SqlDbType.NVarChar, 256));
            command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
            command.Parameters[0].Value = Constants.APPLICATION_NAME;
            command.Parameters[1].Value = UserName;

            try
            {
                connection.Open();
                dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    currentRoles += dataReader["RoleName"] + ",";
                }
                if (currentRoles.Length > 0)
                {
                    currentRoles = currentRoles.Substring(0, currentRoles.Length - 1);
                    userRoles = currentRoles.Split(',');
                }
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
        }
    }
}