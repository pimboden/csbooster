// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data;
using SubSonic;

namespace _4screen.CSB.DataAccess.Business{
    public partial class SPs{
        
        /// <summary>
        /// Creates an object wrapper for the hisp_aspnet_Profile_UpdateLastActivityDate Procedure
        /// </summary>
        public static StoredProcedure HispAspnetProfileUpdateLastActivityDate(Guid? UserId, DateTime? CurrentTimeUtc)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_aspnet_Profile_UpdateLastActivityDate", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@CurrentTimeUtc", CurrentTimeUtc, DbType.DateTime, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_DeletePage Procedure
        /// </summary>
        public static StoredProcedure HispCommunityDeletePage(Guid? PageId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_DeletePage", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@PageId", PageId, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_GetCurrPageID Procedure
        /// </summary>
        public static StoredProcedure HispCommunityGetCurrPageID(Guid? Community, Guid? UserId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_GetCurrPageID", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@Community", Community, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_GetFirstPageID Procedure
        /// </summary>
        public static StoredProcedure HispCommunityGetFirstPageID(Guid? Community)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_GetFirstPageID", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@Community", Community, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_GetMaxPageOrder Procedure
        /// </summary>
        public static StoredProcedure HispCommunityGetMaxPageOrder(Guid? Community)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_GetMaxPageOrder", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@Community", Community, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_IsUserMember Procedure
        /// </summary>
        public static StoredProcedure HispCommunityIsUserMember(Guid? CommunityId, Guid? UserId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_IsUserMember", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CommunityId", CommunityId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_User_Load Procedure
        /// </summary>
        public static StoredProcedure HispCommunityUserLoad(Guid? CommunityId, bool? WithAdmin)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_User_Load", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CommunityId", CommunityId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@WithAdmin", WithAdmin, DbType.Boolean, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_User_RemoveMember Procedure
        /// </summary>
        public static StoredProcedure HispCommunityUserRemoveMember(Guid? CommunityId, Guid? UserId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_User_RemoveMember", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CommunityId", CommunityId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Community_User_SetOwner Procedure
        /// </summary>
        public static StoredProcedure HispCommunityUserSetOwner(Guid? CommunityId, Guid? UserId, bool? Owner)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Community_User_SetOwner", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CommunityId", CommunityId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@Owner", Owner, DbType.Boolean, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Content_GetContent Procedure
        /// </summary>
        public static StoredProcedure HispContentGetContent(Guid? CTYID, string CNTName, string CNTLangCode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Content_GetContent", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CTY_ID", CTYID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@CNT_Name", CNTName, DbType.String, null, null);
        	
            sp.Command.AddParameter("@CNT_LangCode", CNTLangCode, DbType.String, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_Content_SaveContent Procedure
        /// </summary>
        public static StoredProcedure HispContentSaveContent(Guid? CTYID, string CNTName, string CNTLangCode, string CNTText)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_Content_SaveContent", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CTY_ID", CTYID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@CNT_Name", CNTName, DbType.String, null, null);
        	
            sp.Command.AddParameter("@CNT_LangCode", CNTLangCode, DbType.String, null, null);
        	
            sp.Command.AddParameter("@CNT_Text", CNTText, DbType.String, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_DataObject_AddMemberCount Procedure
        /// </summary>
        public static StoredProcedure HispDataObjectAddMemberCount(Guid? CTYID, int? Count)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_DataObject_AddMemberCount", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CTY_ID", CTYID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@Count", Count, DbType.Int32, 0, 10);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_DataObject_FindInCommunity Procedure
        /// </summary>
        public static StoredProcedure HispDataObjectFindInCommunity(Guid? OBJID, Guid? CTYID, bool? ObjectFound)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_DataObject_FindInCommunity", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@OBJ_ID", OBJID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@CTY_ID", CTYID, DbType.Guid, null, null);
        	
            sp.Command.AddOutputParameter("@ObjectFound", DbType.Boolean, null, null);
            
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_DataObject_GetUserIDByOpenID Procedure
        /// </summary>
        public static StoredProcedure HispDataObjectGetUserIDByOpenID(string OpenID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_DataObject_GetUserIDByOpenID", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@OpenID", OpenID, DbType.String, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_DataObject_GetUserIDByPPID Procedure
        /// </summary>
        public static StoredProcedure HispDataObjectGetUserIDByPPID(string PPID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_DataObject_GetUserIDByPPID", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@PPID", PPID, DbType.String, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_IncentivePoints_GetPointsWithColors Procedure
        /// </summary>
        public static StoredProcedure HispIncentivePointsGetPointsWithColors(Guid? UserId)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_IncentivePoints_GetPointsWithColors", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@UserId", UserId, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_MoveWidgetInstance Procedure
        /// </summary>
        public static StoredProcedure HispMoveWidgetInstance(Guid? InsID, int? ToColumnID, int? ToRow)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_MoveWidgetInstance", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@InsID", InsID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@ToColumnID", ToColumnID, DbType.Int32, 0, 10);
        	
            sp.Command.AddParameter("@ToRow", ToRow, DbType.Int32, 0, 10);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the HISP_USER_DELETEASPNETUSER Procedure
        /// </summary>
        public static StoredProcedure HispUserDeleteaspnetuser(Guid? UserID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("HISP_USER_DELETEASPNETUSER", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@UserId", UserID, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_UserFriendType_GetTypes Procedure
        /// </summary>
        public static StoredProcedure HispUserFriendTypeGetTypes(string LangCode)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_UserFriendType_GetTypes", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@LangCode", LangCode, DbType.AnsiString, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetInstance_ReorderByPageColumn Procedure
        /// </summary>
        public static StoredProcedure HispWidgetInstanceReorderByPageColumn(Guid? PageId, int? ColumnID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetInstance_ReorderByPageColumn", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@PageId", PageId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@ColumnID", ColumnID, DbType.Int32, 0, 10);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetInstance_ReorderColumns Procedure
        /// </summary>
        public static StoredProcedure HispWidgetInstanceReorderColumns(Guid? CtyId, int? MaxColumn)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetInstance_ReorderColumns", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CtyId", CtyId, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@MaxColumn", MaxColumn, DbType.Int32, 0, 10);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetTemplates_GetCommunityTemplates Procedure
        /// </summary>
        public static StoredProcedure HispWidgetTemplatesGetCommunityTemplates(Guid? CTYID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetTemplates_GetCommunityTemplates", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@CTY_ID", CTYID, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetTemplates_GetUserTemplates Procedure
        /// </summary>
        public static StoredProcedure HispWidgetTemplatesGetUserTemplates(Guid? UserID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetTemplates_GetUserTemplates", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@UserId", UserID, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetTemplates_IncreaseCount Procedure
        /// </summary>
        public static StoredProcedure HispWidgetTemplatesIncreaseCount(Guid? PAGID, Guid? WTPID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetTemplates_IncreaseCount", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@PAG_ID", PAGID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@WTP_ID", WTPID, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetTemplates_ReduceCount Procedure
        /// </summary>
        public static StoredProcedure HispWidgetTemplatesReduceCount(Guid? PAGID, Guid? WTPID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetTemplates_ReduceCount", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@PAG_ID", PAGID, DbType.Guid, null, null);
        	
            sp.Command.AddParameter("@WTP_ID", WTPID, DbType.Guid, null, null);
        	
            return sp;
        }
        
        /// <summary>
        /// Creates an object wrapper for the hisp_WidgetTemplates_RemoveUserTemplate Procedure
        /// </summary>
        public static StoredProcedure HispWidgetTemplatesRemoveUserTemplate(Guid? WTPID)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("hisp_WidgetTemplates_RemoveUserTemplate", DataService.GetInstance("SqlDataProvider"), "dbo");
        	
            sp.Command.AddParameter("@WTP_ID", WTPID, DbType.Guid, null, null);
        	
            return sp;
        }
        
    }
    
}
