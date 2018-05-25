using System;
using System.Data;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.Extensions.Data
{
    internal class DLTrackingManager
    {
        private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        internal void DoTrackObjectEvent(_4screen.CSB.Extensions.Business.SessionLogParams sessionLogParams, int objectType, Guid? objectId, string action, string actionParams)
        {
            if (string.IsNullOrEmpty(sessionLogParams.SessionID))
                return;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlCommand SetData = new SqlCommand("hisp_LogObjectAction_Append", Conn);
            SetData.CommandType = CommandType.StoredProcedure;

            // Session Parameter
            SetData.Parameters.AddWithValue("@LSE_SessionID", sessionLogParams.SessionID);

            if (sessionLogParams.UserID.HasValue)
                SetData.Parameters.AddWithValue("@USR_ID", sessionLogParams.UserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.IP))
                SetData.Parameters.AddWithValue("@IP", sessionLogParams.IP);

            SetData.Parameters.AddWithValue("@IsMobileDevice", sessionLogParams.IsMobileDevice);
            SetData.Parameters.AddWithValue("@IsIPhone", sessionLogParams.IsIPhone);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserType))
                SetData.Parameters.AddWithValue("@BrowserType", sessionLogParams.BrowserType);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserVersion))
                SetData.Parameters.AddWithValue("@BrowserVersion", sessionLogParams.BrowserVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.System))
                SetData.Parameters.AddWithValue("@System", sessionLogParams.System);

            if (!string.IsNullOrEmpty(sessionLogParams.SystemVersion))
                SetData.Parameters.AddWithValue("@SystemVersion", sessionLogParams.SystemVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.UserLanguage))
                SetData.Parameters.AddWithValue("@UserLanguage", sessionLogParams.UserLanguage);

            if (sessionLogParams.BrowserID.HasValue)
                SetData.Parameters.AddWithValue("@BrowserID", sessionLogParams.BrowserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.Roles))
                SetData.Parameters.AddWithValue("@Roles", sessionLogParams.Roles);

            // SiteAction Parameter
            SetData.Parameters.AddWithValue("@OBJ_Type", objectType);

            if (objectId.HasValue)
                SetData.Parameters.AddWithValue("@OBJ_ID", objectId.Value);

            if (!string.IsNullOrEmpty(action))
                SetData.Parameters.AddWithValue("@Action", action);

            if (!string.IsNullOrEmpty(actionParams))
                SetData.Parameters.AddWithValue("@ActionParams", actionParams);


            try
            {
                Conn.Open();
                SetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        internal void DoTrackPageEvent(_4screen.CSB.Extensions.Business.SessionLogParams sessionLogParams, string pageUrl, string urlParams, string pageReferer, bool isPostback, LogSitePageType logSitePageType, Guid? communityId, int? objectType, Guid? addPara, int? addParaType)
        {
            if (string.IsNullOrEmpty(sessionLogParams.SessionID))
                return;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlCommand SetData = new SqlCommand("hisp_LogSiteAction_Append", Conn);
            SetData.CommandType = CommandType.StoredProcedure;

            // Session Parameter
            SetData.Parameters.AddWithValue("@LSE_SessionID", sessionLogParams.SessionID);

            if (sessionLogParams.UserID.HasValue)
                SetData.Parameters.AddWithValue("@USR_ID", sessionLogParams.UserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.IP))
                SetData.Parameters.AddWithValue("@IP", sessionLogParams.IP);

            SetData.Parameters.AddWithValue("@IsMobileDevice", sessionLogParams.IsMobileDevice);
            SetData.Parameters.AddWithValue("@IsIPhone", sessionLogParams.IsIPhone);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserType))
                SetData.Parameters.AddWithValue("@BrowserType", sessionLogParams.BrowserType);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserVersion))
                SetData.Parameters.AddWithValue("@BrowserVersion", sessionLogParams.BrowserVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.System))
                SetData.Parameters.AddWithValue("@System", sessionLogParams.System);

            if (!string.IsNullOrEmpty(sessionLogParams.SystemVersion))
                SetData.Parameters.AddWithValue("@SystemVersion", sessionLogParams.SystemVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.UserLanguage))
                SetData.Parameters.AddWithValue("@UserLanguage", sessionLogParams.UserLanguage);

            if (sessionLogParams.BrowserID.HasValue)
                SetData.Parameters.AddWithValue("@BrowserID", sessionLogParams.BrowserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.Roles))
                SetData.Parameters.AddWithValue("@Roles", sessionLogParams.Roles);

            // SiteAction Parameter
            if (!string.IsNullOrEmpty(pageUrl))
                SetData.Parameters.AddWithValue("@PageURL", pageUrl);

            if (!string.IsNullOrEmpty(urlParams))
                SetData.Parameters.AddWithValue("@URLParams", urlParams);

            if (!string.IsNullOrEmpty(pageReferer))
                SetData.Parameters.AddWithValue("@PageReferer", pageReferer);

            SetData.Parameters.AddWithValue("@IsPostback", isPostback);
            SetData.Parameters.AddWithValue("@PageType", (int)logSitePageType);

            if (communityId.HasValue)
                SetData.Parameters.AddWithValue("@CTY_ID", communityId.Value);

            if (objectType.HasValue)
                SetData.Parameters.AddWithValue("@OBJ_Type", objectType.Value);

            if (addPara.HasValue)
                SetData.Parameters.AddWithValue("@AddPara", addPara.Value);

            if (addParaType.HasValue)
                SetData.Parameters.AddWithValue("@AddParaType", addParaType.Value);

            try
            {
                Conn.Open();
                SetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        internal void DoTrackSearchEvent(_4screen.CSB.Extensions.Business.SessionLogParams sessionLogParams, string searchWord, int searchResult, int objectType)
        {
            if (string.IsNullOrEmpty(sessionLogParams.SessionID))
                return;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlCommand SetData = new SqlCommand("hisp_LogSearchResult_Append", Conn);
            SetData.CommandType = CommandType.StoredProcedure;

            // Session Parameter
            SetData.Parameters.AddWithValue("@LSE_SessionID", sessionLogParams.SessionID);

            if (sessionLogParams.UserID.HasValue)
                SetData.Parameters.AddWithValue("@USR_ID", sessionLogParams.UserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.IP))
                SetData.Parameters.AddWithValue("@IP", sessionLogParams.IP);

            SetData.Parameters.AddWithValue("@IsMobileDevice", sessionLogParams.IsMobileDevice);
            SetData.Parameters.AddWithValue("@IsIPhone", sessionLogParams.IsIPhone);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserType))
                SetData.Parameters.AddWithValue("@BrowserType", sessionLogParams.BrowserType);

            if (!string.IsNullOrEmpty(sessionLogParams.BrowserVersion))
                SetData.Parameters.AddWithValue("@BrowserVersion", sessionLogParams.BrowserVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.System))
                SetData.Parameters.AddWithValue("@System", sessionLogParams.System);

            if (!string.IsNullOrEmpty(sessionLogParams.SystemVersion))
                SetData.Parameters.AddWithValue("@SystemVersion", sessionLogParams.SystemVersion);

            if (!string.IsNullOrEmpty(sessionLogParams.UserLanguage))
                SetData.Parameters.AddWithValue("@UserLanguage", sessionLogParams.UserLanguage);

            if (sessionLogParams.BrowserID.HasValue)
                SetData.Parameters.AddWithValue("@BrowserID", sessionLogParams.BrowserID.Value);

            if (!string.IsNullOrEmpty(sessionLogParams.Roles))
                SetData.Parameters.AddWithValue("@Roles", sessionLogParams.Roles);

            // SearchAction Parameter
            if (!string.IsNullOrEmpty(searchWord))
                SetData.Parameters.AddWithValue("@SearchWord", searchWord);

            SetData.Parameters.AddWithValue("@SearchResult", searchResult);
            SetData.Parameters.AddWithValue("@OBJ_Type", objectType);

            try
            {
                Conn.Open();
                SetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }

        }
    }
}