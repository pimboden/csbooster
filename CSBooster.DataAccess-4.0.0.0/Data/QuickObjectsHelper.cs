// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.Utils;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class DataObjectsHelper
    {
        private static string strConn = Helper.GetSiemeConnectionString();

        #region GetObject

        public static SqlDataReader GetReaderAll<T>(Business.QuickParameters paras, int? useThisObjectType) where T : Business.DataObject, new()
        {
            T listItem = new T();

            StringBuilder sql = new StringBuilder(1000);
            StringBuilder sqlWhere = new StringBuilder(1000);

            if (!string.IsNullOrEmpty(paras.Communities) && paras.Communities.IndexOf('|') == -1)
            {
                paras.CommunityID = Common.Extensions.ToGuid(paras.Communities);
                paras.Communities = string.Empty;
            }

            bool disablePaging = false;
            if (paras.DisablePaging.HasValue)
            {
                disablePaging = paras.DisablePaging.Value;
            }

            if (useThisObjectType != null)
            {
                paras.ObjectType = useThisObjectType.Value;
            }

            bool checkPage = paras.QuerySourceType == QuerySourceType.Page || !paras.Udc.IsAuthenticated;
            bool checkCommunityMember = (paras.QuerySourceType == QuerySourceType.Community && paras.CommunityID.HasValue && paras.Udc.IsAuthenticated);
            bool checkProfileFriendship = (paras.QuerySourceType == QuerySourceType.Profile && paras.UserID.HasValue && paras.Udc.IsAuthenticated);

            if (paras.Udc.IsAdmin)
            {
                checkPage = false;
                checkCommunityMember = false;
                checkProfileFriendship = false;
            }

            string queryChildRelation = null;
            string queryParentRelation = null;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            bool GroupInfoActive = (paras.RelationParams != null && (paras.RelationParams.ParentObjectType.HasValue || paras.RelationParams.ParentObjectType.HasValue));
            string topAmmount = paras.Amount > 0 ? string.Format("top {0}", paras.Amount) : string.Empty;
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;

                sql.Append("SET NOCOUNT ON\r\n");
                sql.Append("DECLARE @Amount          AS INT\r\n");

                sql.Append("DECLARE @FromID          AS INT\r\n");
                sql.Append("DECLARE @ToID            AS INT\r\n");
                sql.Append("DECLARE @RowCount        AS INT\r\n");
                sql.Append("DECLARE @RowTotal        AS INT\r\n");
                sql.Append("DECLARE @PageTotal       AS INT\r\n");
                sql.Append("DECLARE @PageNumber      AS INT\r\n");
                sql.Append("DECLARE @PageSize        AS INT\r\n");

                if (checkCommunityMember)
                {
                    sql.Append("DECLARE @IsCommunityMember          AS BIT\r\n");
                    sql.Append("SET @IsCommunityMember = 0\r\n");
                    sql.Append("IF EXISTS(SELECT * FROM hirel_Community_User_CUR WHERE CTY_ID = @CTY_ID AND USR_ID = @USR_ID)\r\n");
                    sql.Append("   BEGIN SET @IsCommunityMember = 1 END\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_ID", SqlDbType.UniqueIdentifier, paras.CommunityID.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, paras.Udc.UserID));
                }
                else if (checkProfileFriendship)
                {
                    sql.Append("DECLARE @IsUserFriend          AS INT\r\n");
                    sql.Append("SET @IsUserFriend = 0\r\n");
                    sql.Append("SELECT @IsUserFriend = UFR_TypeID FROM hitbl_UserFriends_FRI WHERE ASP_UserId = @USR_ID AND ASP_FriendId = @ASP_FriendId AND FRI_Blocked = 0\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, paras.UserID.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ASP_FriendId", SqlDbType.UniqueIdentifier, paras.Udc.UserID));
                }

                sql.AppendFormat("SET @PageNumber = {0}\r\n", paras.PageNumber > 0 ? paras.PageNumber.ToString() : "1");
                sql.AppendFormat("SET @PageSize = {0}\r\n", paras.PageSize > 0 ? paras.PageSize.ToString() : "99999999");


                if (!string.IsNullOrEmpty(paras.Tags1))
                {
                    sql.AppendFormat("SELECT returnValue INTO #tempTGWList1 FROM GuidListSplit(COALESCE('{0}', ''), DEFAULT)\r\n", paras.Tags1);
                }
                if (!string.IsNullOrEmpty(paras.Tags2))
                {
                    sql.AppendFormat("SELECT returnValue INTO #tempTGWList2 FROM GuidListSplit(COALESCE('{0}', ''), DEFAULT)\r\n", paras.Tags2);
                }
                if (!string.IsNullOrEmpty(paras.Tags3))
                {
                    sql.AppendFormat("SELECT returnValue INTO #tempTGWList3 FROM GuidListSplit(COALESCE('{0}', ''), DEFAULT)\r\n", paras.Tags3);
                }

                sql.AppendLine();
                if (!disablePaging)
                {
                    if (paras.RelationParams == null || !GroupInfoActive)
                    {
                        // TODO: Create index for OBJ_ID if queries slow down
                        sql.Append("CREATE TABLE #PageTable\r\n");
                        sql.Append("(\r\n");
                        sql.Append("  RecNumber  INT IDENTITY PRIMARY KEY,\r\n");
                        sql.Append("  OBJ_ID     UNIQUEIDENTIFIER NOT NULL\r\n");
                        sql.Append(")\r\n");

                        sql.AppendLine();
                        sql.Append("INSERT INTO #PageTable (OBJ_ID)\r\n");

                        sql.AppendFormat("SELECT {0} hitbl_DataObject_OBJ.OBJ_ID\r\n", topAmmount);

                    }
                    else
                    {
                        sql.Append("CREATE TABLE #PageTable\r\n");
                        sql.Append("(\r\n");
                        sql.Append("  RecNumber  INT IDENTITY PRIMARY KEY,\r\n");
                        sql.Append("  OBJ_ID     UNIQUEIDENTIFIER NOT NULL,\r\n");
                        sql.Append("  INFO_OBJ_ID     UNIQUEIDENTIFIER NOT NULL,\r\n");
                        sql.Append("  INFO_TYPE         INT,\r\n");
                        sql.Append("  INFO_TITLE         NVARCHAR(100)\r\n");

                        sql.Append(")\r\n");

                        sql.AppendLine();
                        sql.Append("INSERT INTO #PageTable (OBJ_ID, INFO_OBJ_ID, INFO_TYPE, INFO_TITLE)\r\n");
                        sql.AppendFormat("SELECT {0} hitbl_DataObject_OBJ.OBJ_ID, INFO.OBJ_ID, INFO.OBJ_TYPE, INFO.OBJ_TITLE\r\n", topAmmount);

                    }
                }
                else
                {
                    if (paras.RelationParams == null || !GroupInfoActive)
                    {

                        sql.AppendLine();
                        sql.Append("SELECT 0, 0, 0\r\n");
                        sql.Append(GetSelectString<T>(listItem, paras, GetData));
                    }
                    else
                    {
                        sql.AppendLine();
                        sql.Append("SELECT 0, 0, 0\r\n");
                        sql.Append(GetSelectString<T>(listItem, paras, GetData));
                        sql.Append(", INFO.OBJ_ID AS INFO_OBJ_ID, INFO.OBJ_TYPE AS INFO_TYPE, INFO.OBJ_TITLE AS INFO_TITLE\r\n");
                    }
                }

                sql.Append("FROM hitbl_DataObject_OBJ\r\n");

                sql.AppendFormat("{0}\r\n", listItem.GetJoinSQL(paras, GetData.Parameters));

                if (paras.SortBy == QuickSort.Linked)
                    sql.Append("  LEFT OUTER JOIN hivw_Quick_Object_Object_Linked ON hitbl_DataObject_OBJ.OBJ_ID = hivw_Quick_Object_Object_Linked.OBJ_OriginalObjID\r\n");

                if (paras.RelationParams != null)
                {
                    if (paras.RelationParams.ChildObjectID.HasValue || paras.RelationParams.ChildObjectType.HasValue)
                    {
                        queryChildRelation = "ChildObjects";
                        if (paras.RelationParams.ExcludeSystemObjects)
                            sql.Append("  INNER JOIN hirel_ObjToObj_OTO AS ChildObjects ON ChildObjects.OTO_Obj1_ID = hitbl_DataObject_OBJ.OBJ_ID AND ChildObjects.OTO_Obj1_Type <> 5\r\n");
                        else
                            sql.Append("  INNER JOIN hirel_ObjToObj_OTO AS ChildObjects ON ChildObjects.OTO_Obj1_ID = hitbl_DataObject_OBJ.OBJ_ID\r\n");

                        if (paras.RelationParams.ChildObjectID.HasValue)
                        {
                            sqlWhere.Append("(ChildObjects.OTO_Obj2_ID = @RelationChildObjectID) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationChildObjectID", SqlDbType.UniqueIdentifier, paras.RelationParams.ChildObjectID.Value));
                        }
                        if (paras.RelationParams.ChildObjectType.HasValue)
                        {
                            sqlWhere.Append("(ChildObjects.OTO_Obj2_Type = @RelationChildObjectType) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationChildObjectType", SqlDbType.Int, paras.RelationParams.ChildObjectType.Value));
                        }
                        if (!string.IsNullOrEmpty(paras.RelationParams.RelationType))
                        {
                            sqlWhere.Append("(ChildObjects.OTO_RelType LIKE @RelationChildRelationType) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationChildRelationType", SqlDbType.NVarChar, 50, paras.RelationParams.RelationType));
                        }
                    }
                    if (paras.RelationParams.ParentObjectID.HasValue || paras.RelationParams.ParentObjectType.HasValue)
                    {
                        queryParentRelation = "ParentObjects";
                        if (paras.RelationParams.ExcludeSystemObjects)
                            sql.Append("  INNER JOIN hirel_ObjToObj_OTO AS ParentObjects ON ParentObjects.OTO_Obj2_ID = hitbl_DataObject_OBJ.OBJ_ID AND ParentObjects.OTO_Obj2_Type <> 5\r\n");
                        else
                            sql.Append("  INNER JOIN hirel_ObjToObj_OTO AS ParentObjects ON ParentObjects.OTO_Obj2_ID = hitbl_DataObject_OBJ.OBJ_ID\r\n");

                        sql.Append("  INNER JOIN  hitbl_DataObject_OBJ AS INFO ON ParentObjects.OTO_Obj1_ID = INFO.OBJ_ID\r\n");

                        if (paras.RelationParams.ParentObjectID.HasValue)
                        {
                            sqlWhere.Append("(ParentObjects.OTO_Obj1_ID = @RelationParentObjectID) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationParentObjectID", SqlDbType.UniqueIdentifier, paras.RelationParams.ParentObjectID.Value));
                        }
                        if (paras.RelationParams.ParentObjectType.HasValue)
                        {
                            sqlWhere.Append("(ParentObjects.OTO_Obj1_Type = @RelationParentObjectType) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationParentObjectType", SqlDbType.Int, paras.RelationParams.ParentObjectType.Value));
                        }
                        if (!string.IsNullOrEmpty(paras.RelationParams.RelationType))
                        {
                            sqlWhere.Append("(ParentObjects.OTO_RelType LIKE @RelationParentRelationType) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@RelationParentRelationType", SqlDbType.NVarChar, 50, paras.RelationParams.RelationType));
                        }
                        if (paras.RelationParams.ParentUserId.HasValue)
                        {
                            sqlWhere.Append("(INFO.USR_ID LIKE @ParentUserId) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ParentUserId", SqlDbType.UniqueIdentifier, paras.RelationParams.ParentUserId));
                        }
                        if (paras.RelationParams.ParentCommunityId.HasValue)
                        {
                            sqlWhere.Append("(INFO.CTY_ID LIKE @ParentCommunityId) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ParentCommunityId", SqlDbType.UniqueIdentifier, paras.RelationParams.ParentCommunityId));
                        }
                        if (paras.RelationParams.ParentShowState.HasValue)
                        {
                            sqlWhere.Append("(INFO.OBJ_ShowState = @ParentShowState) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ParentShowState", SqlDbType.Int, (int)paras.RelationParams.ParentShowState));
                        }
                    }
                }

                if (paras.MembershipParams != null)
                {
                    if (paras.MembershipParams.UserID.HasValue)
                    {
                        sql.Append("  INNER JOIN hirel_Community_User_CUR ON hirel_Community_User_CUR.CTY_ID = hitbl_DataObject_OBJ.OBJ_ID AND hirel_Community_User_CUR.USR_ID = @MembershipParamsUserID\r\n");
                        GetData.Parameters.Add(SqlHelper.AddParameter("@MembershipParamsUserID", SqlDbType.UniqueIdentifier, paras.MembershipParams.UserID.Value));

                        if (paras.MembershipParams.IsOwner.HasValue)
                        {
                            sqlWhere.Append("(hirel_Community_User_CUR.CUR_IsOwner = @MembershipParamsIsOwner) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@MembershipParamsIsOwner", SqlDbType.Bit, paras.MembershipParams.IsOwner.Value ? 1 : 0));
                        }
                        if (paras.MembershipParams.IsCreator.HasValue && paras.MembershipParams.IsCreator.Value)
                        {
                            sqlWhere.Append("(hitbl_DataObject_OBJ.USR_ID = @MembershipParamsUserID) AND\r\n");
                        }
                    }
                    else if (paras.MembershipParams.CommunityID.HasValue)
                    {
                        sql.Append("  INNER JOIN hirel_Community_User_CUR ON hirel_Community_User_CUR.USR_ID = hitbl_DataObject_OBJ.OBJ_ID AND hirel_Community_User_CUR.CTY_ID = @MembershipParamsCommunityID\r\n");
                        GetData.Parameters.Add(SqlHelper.AddParameter("@MembershipParamsCommunityID", SqlDbType.UniqueIdentifier, paras.MembershipParams.CommunityID.Value));
                    }
                }

                if (paras.ViewLogParams != null)
                {
                    sql.Append("  INNER JOIN hitbl_ViewLog_VIW ON hitbl_ViewLog_VIW.USR_ID = hitbl_DataObject_OBJ.OBJ_ID AND hitbl_ViewLog_VIW.OBJ_ID = @ViewLogParamsObjectID\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ViewLogParamsObjectID", SqlDbType.UniqueIdentifier, paras.ViewLogParams.ObjectID));
                }

                if (paras.ObjectType == Helper.GetObjectType("User").NumericId)
                {

                    if (paras.OnlyWithImage.HasValue && paras.OnlyWithImage.Value == true)
                    {
                        sqlWhere.Append("(NOT OBJ_URLImageSmall IS NULL) AND\r\n");
                    }
                }
                if (paras.ObjectType != 0)
                {
                    sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_Type = @ObjectType) AND\r\n", paras.ObjectType);
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ObjectType", SqlDbType.Int, paras.ObjectType));
                }
                else if (!string.IsNullOrEmpty(paras.ObjectTypes))
                {
                    sqlWhere.AppendFormat("( hitbl_DataObject_OBJ.OBJ_Type in({0}) ) AND\r\n", paras.ObjectTypes.Replace('|', ','));
                }
                if (paras.ObjectID.HasValue)
                {
                    sqlWhere.Append("( hitbl_DataObject_OBJ.OBJ_ID = @ObjectID ) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ObjectID", SqlDbType.UniqueIdentifier, paras.ObjectID.Value));
                }
                if (paras.CommunityID.HasValue)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.CTY_ID = @CommunityID\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CommunityID", SqlDbType.UniqueIdentifier, paras.CommunityID.Value));

                    if (paras.IncludeGroups != null && string.IsNullOrEmpty(paras.ParentObjectID))
                    {
                        sqlWhere.AppendFormat("OR hitbl_DataObject_OBJ.OBJ_Parent_OBJ_ID = @CommunityID\r\n", paras.CommunityID.Value); //Include SubCommunity
                    }
                    sqlWhere.Append(") AND\r\n");
                }
                else if (!string.IsNullOrEmpty(paras.Communities))
                {
                    sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.CTY_ID in ( '{0}')\r\n", paras.Communities.Replace("|", "','"));
                    if (paras.IncludeGroups != null && string.IsNullOrEmpty(paras.ParentObjectID))
                    {
                        sqlWhere.AppendFormat("or hitbl_DataObject_OBJ.OBJ_Parent_OBJ_ID in ('{0}')\r\n", paras.Communities.Replace("|", "','")); //Include SubCommunity
                    }
                    sqlWhere.Append(") AND\r\n");
                }
                if (!string.IsNullOrEmpty(paras.ExcludeObjectIds))
                {
                    sqlWhere.AppendFormat("hitbl_DataObject_OBJ.OBJ_ID NOT IN ('{0}') AND\r\n", paras.ExcludeObjectIds.Replace("|", "','"));
                }
                if (paras.UserID.HasValue)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.USR_ID = @UserID ) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@UserID", SqlDbType.UniqueIdentifier, paras.UserID.Value));
                }

                if (paras.TagID.HasValue)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_ID IN (SELECT DISTINCT OTO_Obj1_ID FROM hirel_ObjToObj_OTO WHERE (hirel_ObjToObj_OTO.OTO_Obj1_ID = hitbl_DataObject_OBJ.OBJ_ID) AND (hirel_ObjToObj_OTO.OTO_Obj2_ID = @TagID))) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@TagID", SqlDbType.UniqueIdentifier, paras.TagID));
                }
                else
                {
                    if (!string.IsNullOrEmpty(paras.Tags1))
                        sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_ID IN (SELECT DISTINCT TGL.OTO_Obj1_ID FROM hirel_ObjToObj_OTO as TGL JOIN #tempTGWList1 as LS on (TGL.OTO_Obj2_ID = LS.returnValue))) AND\r\n", paras.Tags1);
                    if (!string.IsNullOrEmpty(paras.Tags2))
                        sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_ID IN (SELECT DISTINCT TGL.OTO_Obj1_ID FROM hirel_ObjToObj_OTO as TGL JOIN #tempTGWList2 as LS on (TGL.OTO_Obj2_ID = LS.returnValue))) AND\r\n", paras.Tags2);
                    if (!string.IsNullOrEmpty(paras.Tags3))
                        sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_ID IN (SELECT DISTINCT TGL.OTO_Obj1_ID FROM hirel_ObjToObj_OTO as TGL JOIN #tempTGWList3 as LS on (TGL.OTO_Obj2_ID = LS.returnValue))) AND\r\n", paras.Tags3);
                }

                if (paras.MembershipParams == null)
                {
                    if (checkPage)
                    {
                        sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Status = 2) AND\r\n");
                    }
                    else if (checkCommunityMember)
                    {
                        sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Status = 2 OR (hitbl_DataObject_OBJ.OBJ_Status = 1 AND @IsCommunityMember = 1)) AND\r\n");
                    }
                    else if (checkProfileFriendship)
                    {
                        sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Status = 2 OR (hitbl_DataObject_OBJ.USR_ID = @LoggedInUserId) OR (hitbl_DataObject_OBJ.OBJ_Status = 1 AND (OBJ_FriendVisibility & @IsUserFriend) > 0)) AND\r\n");
                        GetData.Parameters.Add(SqlHelper.AddParameter("@LoggedInUserId", SqlDbType.UniqueIdentifier, paras.Udc.UserID));
                    }
                }

                if (paras.ObjectStatus != null)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Status = @ObjectStatus) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ObjectStatus", SqlDbType.Int, (int)paras.ObjectStatus));
                }

                if (paras.ShowState != null)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_ShowState = @ShowState) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ShowState", SqlDbType.Int, (int)paras.ShowState));
                }
                else
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_ShowState <> @ShowState) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ShowState", SqlDbType.Int, (int)ObjectShowState.Deleted));
                }

                if (paras.FromInserted != null)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_InsertedDate >= @FromInserted) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@FromInserted", SqlDbType.DateTime, paras.FromInserted.Value.Date));
                }
                if (paras.ToInserted != null)
                {
                    sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_InsertedDate <=  @ToInserted) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ToInserted", SqlDbType.DateTime, paras.ToInserted.Value.Date));
                }

                if (paras.Featured != null)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Featured = @Featured) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Featured", SqlDbType.Int, paras.Featured.Value));
                }

                if (paras.GroupID.HasValue)
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_GroupID = @GroupID) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@GroupID", SqlDbType.UniqueIdentifier, paras.GroupID.Value));
                }

                if (paras.WithCopy != null && paras.WithCopy.Value == false)
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_OriginalObjID IS NULL) AND\r\n");

                if (paras.FromStartDate != null || paras.ToEndDate != null || paras.ToStartDate != null || paras.FromEndDate != null)
                {
                    if (paras.DateQueryMethode.Value == QuickDateQueryMethode.BetweenStartRangeEndRange)
                    {
                        if (paras.FromStartDate != null)
                        {
                            sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_StartDate >= @FromStartDate) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@FromStartDate", SqlDbType.DateTime, paras.FromStartDate.Value));
                        }
                        if (paras.ToStartDate != null)
                        {
                            sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_StartDate <= @ToStartDate) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ToStartDate", SqlDbType.DateTime, paras.ToStartDate.Value));
                        }
                        if (paras.FromEndDate != null)
                        {
                            sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_EndDate >= @FromEndDate) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@FromEndDate", SqlDbType.DateTime, paras.FromEndDate.Value));
                        }
                        if (paras.ToEndDate != null)
                        {
                            sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_EndDate <= @ToEndDate) AND\r\n");
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ToEndDate", SqlDbType.DateTime, paras.ToEndDate.Value));
                        }
                    }
                    else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.BetweenStartAndEnd)
                    {
                        sqlWhere.Append("NOT ((hitbl_DataObject_OBJ.OBJ_EndDate < @ToStartDate) OR (hitbl_DataObject_OBJ.OBJ_StartDate > @FromEndDate)) AND\r\n");
                        GetData.Parameters.Add(SqlHelper.AddParameter("@ToStartDate", SqlDbType.DateTime, paras.ToStartDate.Value));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@FromEndDate", SqlDbType.DateTime, paras.FromEndDate.Value));
                    }
                    else
                    {
                        string strCompStartDate = ">=";
                        string strCompEndDate = "<=";
                        if (paras.DateQueryMethode == null || paras.DateQueryMethode.Value == QuickDateQueryMethode.StartOpenEndOpen)
                        {
                            strCompStartDate = ">=";
                            strCompEndDate = "<=";
                        }
                        else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.StartOpenEndExact)
                        {
                            strCompStartDate = ">=";
                            strCompEndDate = "=";
                        }
                        else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.StartExactEndOpen)
                        {
                            strCompStartDate = "=";
                            strCompEndDate = "<=";
                        }
                        else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.StartExactEndExact)
                        {
                            strCompStartDate = "=";
                            strCompEndDate = "=";
                        }
                        else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.BetweenStartOpenEndOpen)
                        {
                            strCompStartDate = "<=";
                            strCompEndDate = ">=";
                        }
                        else if (paras.DateQueryMethode.Value == QuickDateQueryMethode.BetweenStartRangeEndRange)
                        {
                            strCompStartDate = "<=";
                            strCompEndDate = ">=";
                        }

                        if (paras.FromStartDate != null)
                        {
                            sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_StartDate {0} @FromStartDate) AND\r\n", strCompStartDate);
                            GetData.Parameters.Add(SqlHelper.AddParameter("@FromStartDate", SqlDbType.DateTime, paras.FromStartDate.Value));
                        }
                        if (paras.ToEndDate != null)
                        {
                            sqlWhere.AppendFormat("(hitbl_DataObject_OBJ.OBJ_EndDate {0} @ToEndDate) AND\r\n", strCompEndDate);
                            GetData.Parameters.Add(SqlHelper.AddParameter("@ToEndDate", SqlDbType.DateTime, paras.ToEndDate.Value));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(paras.Country))
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Country_ISO LIKE @Country) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Country", SqlDbType.NVarChar, 5, Common.Extensions.PrepareLike(paras.Country, false, true)));
                }
                if (!string.IsNullOrEmpty(paras.Zip))
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Zip LIKE @Zip) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Zip", SqlDbType.NVarChar, 8, Common.Extensions.PrepareLike(paras.Zip, false, true)));
                }
                if (!string.IsNullOrEmpty(paras.City))
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_City LIKE @City) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@City", SqlDbType.NVarChar, 50, Common.Extensions.PrepareLike(paras.City, false, true)));
                }
                if (paras.DistanceKm != null && paras.GeoLat != null & paras.GeoLong != null)
                {
                    sqlWhere.Append("(dbo.hifu_CalcDistance(OBJ_Geo_Lat, OBJ_Geo_Long, @GeoLat, @GeoLong) <= @DistanceKm) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@GeoLat", SqlDbType.Float, paras.GeoLat.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@GeoLong", SqlDbType.Float, paras.GeoLong.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DistanceKm", SqlDbType.Float, paras.DistanceKm.Value));
                }
                if (paras.OnlyGeoTagged != null)
                {
                    if (paras.OnlyGeoTagged.Value)
                    {
                        sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Geo_Lat IS NOT NULL AND hitbl_DataObject_OBJ.OBJ_Geo_Long IS NOT NULL) AND\r\n");
                    }
                    else
                    {
                        sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Geo_Lat IS NULL AND hitbl_DataObject_OBJ.OBJ_Geo_Long IS NULL) AND\r\n");
                    }
                }
                if (!string.IsNullOrEmpty(paras.Nickname) || !string.IsNullOrEmpty(paras.Title) || !string.IsNullOrEmpty(paras.Description))
                {
                    string tempORStmt = string.Empty;
                    if (!string.IsNullOrEmpty(paras.Nickname))
                    {
                        tempORStmt = "(hitbl_DataObject_OBJ.USR_Nickname LIKE @Nickname) OR\r\n";
                        GetData.Parameters.Add(SqlHelper.AddParameter("@Nickname", SqlDbType.NVarChar, 256, Common.Extensions.PrepareLike(paras.Nickname, true, true)));
                    }
                    if (!string.IsNullOrEmpty(paras.Title))
                    {
                        tempORStmt += "(hitbl_DataObject_OBJ.OBJ_Title LIKE @Title) OR\r\n";
                        GetData.Parameters.Add(SqlHelper.AddParameter("@Title", SqlDbType.NVarChar, 100, Common.Extensions.PrepareLike(paras.Title, true, true)));
                    }
                    if (!string.IsNullOrEmpty(paras.Description))
                    {
                        tempORStmt += "(hitbl_DataObject_OBJ.OBJ_Description LIKE @Description)\r\n";
                        GetData.Parameters.Add(SqlHelper.AddParameter("@Description", SqlDbType.NVarChar, Common.Extensions.PrepareLike(paras.Description, true, true)));
                    }
                    if (tempORStmt.EndsWith("OR\r\n"))
                    {
                        tempORStmt = tempORStmt.Substring(0, tempORStmt.Length - 4);
                    }
                    sqlWhere.AppendFormat("({0}) AND\r\n", tempORStmt);
                }
                if (!string.IsNullOrEmpty(paras.ParentObjectID))
                {
                    sqlWhere.Append("(hitbl_DataObject_OBJ.OBJ_Parent_OBJ_ID = @ParentObjectID) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@ParentObjectID", SqlDbType.UniqueIdentifier, paras.ParentObjectID));
                }

                if (paras.CheckUserRoleRight)
                {
                    sqlWhere.Append("(@CheckUserRoleRight = (@CheckUserRoleRight & hitbl_DataObject_OBJ.OBJ_RoleRight)) AND\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CheckUserRoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleInt(paras.UserRole)));
                }

                if (!string.IsNullOrEmpty(paras.TitleLeftChar))
                {
                    List<string> list = new List<string>(1);
                    list.Add(paras.TitleLeftChar);
                    List<string> chrs = Business.DataAccessConfiguration.CharacterDieresis(paras.TitleLeftChar[0].ToString());
                    foreach (string chr in chrs)
                    {
                        if (paras.TitleLeftChar.Length == 1)
                            list.Add(chr);
                        else
                            list.Add(string.Concat(chr, paras.TitleLeftChar.Substring(1)));
                    }
                    sqlWhere.Append("(");
                    string strOR = string.Empty;
                    int ic = 0;
                    foreach (string item in list)
                    {
                        string paraName = "@Left" + ic.ToString();
                        sqlWhere.AppendFormat("{0}hitbl_DataObject_OBJ.OBJ_Title LIKE {1}", strOR, paraName);
                        GetData.Parameters.Add(SqlHelper.AddParameter(paraName, SqlDbType.NVarChar, 100, Common.Extensions.PrepareLike(item, false, true)));
                        if (strOR.Length == 0)
                            strOR = " OR ";
                        ic++;
                    }
                    sqlWhere.Append(") AND\r\n");
                }

                sqlWhere.AppendFormat("{0}", listItem.GetWhereSQL(paras, GetData.Parameters));

                if (!string.IsNullOrEmpty(paras.GeneralSearch))
                {
                    if (paras.CatalogSearchType == DBCatalogSearchType.FreetextTable)
                    {
                        sqlWhere.AppendFormat("( FREETEXT(hitbl_DataObject_OBJ.*, @ObjectGeneralSearch, LANGUAGE 0x0) {0}) AND\r\n", listItem.GetFullTextWhereSQL(paras, GetData.Parameters));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@ObjectGeneralSearch", SqlDbType.NVarChar, 512, paras.GeneralSearch));
                    }
                    else if (paras.CatalogSearchType == DBCatalogSearchType.ContainsTable)
                    {
                        sqlWhere.AppendFormat("( CONTAINS(hitbl_DataObject_OBJ.*, @ObjectGeneralSearch, LANGUAGE 0x0) {0}) AND\r\n", listItem.GetFullTextWhereSQL(paras, GetData.Parameters));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@ObjectGeneralSearch", SqlDbType.NVarChar, 512, FullTextSearch.GetNormalForm(paras.GeneralSearch)));
                    }
                }


                string whereStatement = sqlWhere.ToString();
                if (whereStatement.EndsWith("AND\r\n"))
                {
                    sql.AppendFormat("WHERE ({0})\r\n", whereStatement.Substring(0, sqlWhere.Length - 6));
                }
                else
                {
                    sql.AppendFormat("WHERE ({0})\r\n", whereStatement);
                }
                string relOrder = string.Empty;
                if (GroupInfoActive)
                {
                    if (paras.SortBy != QuickSort.RelationSortNumber)
                    {
                        switch (paras.RelationParams.GroupSort)
                        {
                            case QuickSort.InsertedDate:
                                relOrder = string.Format(" INFO.OBJ_InsertedDate {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.Viewed:
                                relOrder = string.Format(" INFO.OBJ_ViewCount {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.Commented:
                                relOrder = string.Format(" INFO.OBJ_CommentCount {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.RatedCount:
                                relOrder = string.Format(" INFO.OBJ_RatedCount {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.RatedAverage:
                                relOrder = string.Format(" INFO.OBJ_RatedAverage {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.IncentivePoints:
                                relOrder = string.Format(" INFO.OBJ_IncentivePoints {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.MemberCount:
                                relOrder = string.Format(" INFO.OBJ_MemberCount {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.Random:
                                relOrder = string.Format("NEWID() {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.Agility:
                                relOrder = string.Format(" INFO.OBJ_Agility {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.RatedConsolidated:
                                relOrder = string.Format(" INFO.OBJ_RatedConsolidated {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.Title:
                                relOrder = string.Format(" INFO.OBJ_Title {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.FavoriteCount:
                                relOrder = string.Format(" INFO.OBJ_FavoriteCount {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.ModifiedDate:
                                relOrder = string.Format(" INFO.OBJ_UpdatedDate {0},", paras.RelationParams.GroupSortDirection);
                                break;
                            case QuickSort.NotSorted:
                                break;
                            default:
                                relOrder = string.Format(" INFO.OBJ_StartDate {0},", paras.RelationParams.GroupSortDirection);
                                break;
                        }

                    }
                }
                switch (paras.SortBy)
                {
                    case QuickSort.Linked:
                        sql.AppendFormat("ORDER BY {0} hivw_Quick_Object_Object_Linked.OBJ_Linked {1}, hitbl_DataObject_OBJ.OBJ_ViewCount Desc\r\n", relOrder, paras.Direction.ToString());
                        break;
                    case QuickSort.InsertedDate:
                        if (paras.ViewLogParams == null)
                            sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_InsertedDate {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        else
                            sql.AppendFormat("ORDER BY {0} VIW_InsertedDate {1}\r\n", relOrder, paras.Direction.ToString());
                        break;
                    case QuickSort.ModifiedDate:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_UpdatedDate {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Viewed:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_ViewCount {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Commented:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_CommentCount {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.RatedCount:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_RatedCount {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.RatedAverage:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_RatedAverage {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.IncentivePoints:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_IncentivePoints {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.MemberCount:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_MemberCount {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Random:
                        sql.Append("ORDER BY NEWID()\r\n");
                        break;
                    case QuickSort.Agility:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_Agility {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.RatedConsolidated:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_RatedConsolidated {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.FavoriteCount:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_FavoriteCount {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Title:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_Title {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Nickname:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.USR_Nickname {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.Accuracy:
                        if (!string.IsNullOrEmpty(paras.GeneralSearch))
                            sql.AppendFormat("ORDER BY {0} Rank DESC\r\n", relOrder);
                        else
                            sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_StartDate {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                    case QuickSort.RelationSortNumber:
                        if (queryChildRelation != null && queryParentRelation != null)
                        {
                            if (paras.RelationParams.SortType == RelationSortType.Child)
                                sql.AppendFormat("ORDER BY {0}.OTO_OrderNr {1}\r\n", queryChildRelation, paras.Direction);
                            else
                                sql.AppendFormat("ORDER BY {0}.OTO_OrderNr {1}\r\n", queryParentRelation, paras.Direction);
                        }
                        else if (queryChildRelation != null)
                        {
                            sql.AppendFormat("ORDER BY {0}.OTO_OrderNr {1}\r\n", queryChildRelation, paras.Direction);
                        }
                        else
                        {
                            sql.AppendFormat("ORDER BY {0}.OTO_OrderNr {1}\r\n", queryParentRelation, paras.Direction);
                        }
                        break;
                    case QuickSort.NotSorted:
                        break;
                    default:
                        sql.AppendFormat("ORDER BY {0} hitbl_DataObject_OBJ.OBJ_StartDate {1}{2}\r\n", relOrder, paras.Direction.ToString(), GetSortOrderString(paras.SortBySecond, paras.DirectionSecond));
                        break;
                }

                if (!disablePaging)
                {
                    sql.AppendLine();
                    sql.Append("SET @RowCount = @@ROWCOUNT\r\n");
                    sql.Append("SET @RowTotal = @RowCount\r\n");

                    if (paras.CurrentObjectID.HasValue)
                        sql.AppendFormat("SELECT @PageNumber = CEILING(CAST(RecNumber AS FLOAT)/@PageSize) FROM #PageTable WHERE OBJ_ID = '{0}'\r\n", paras.CurrentObjectID.Value);

                    sql.Append("SET @FromID = ((@PageNumber - 1) * @PageSize) + 1\r\n");
                    sql.Append("SET @ToID = @PageNumber *  @PageSize\r\n");

                    sql.Append("SET @PageTotal = (@RowCount /  @PageSize)\r\n");
                    sql.Append("IF (@RowCount > ( @PageSize * @PageTotal))\r\n");
                    sql.Append("SET @PageTotal = @PageTotal + 1\r\n");

                    sql.Append("SELECT @PageTotal, @RowTotal, @PageNumber\r\n");

                    sql.Append(GetSelectString<T>(listItem, paras, GetData));

                    if (GroupInfoActive)
                    {
                        sql.Append(", #PageTable.INFO_OBJ_ID, #PageTable.INFO_TYPE, #PageTable.INFO_TITLE\r\n");
                    }

                    sql.Append("FROM hitbl_DataObject_OBJ\r\n");
                    sql.AppendFormat("{0}\r\n", listItem.GetJoinSQL(paras, GetData.Parameters));
                    sql.Append("  INNER JOIN #PageTable ON hitbl_DataObject_OBJ.OBJ_ID = #PageTable.OBJ_ID\r\n");
                    sql.Append("WHERE #PageTable.RecNumber BETWEEN @FromID AND @ToID\r\n");
                    sql.Append("ORDER BY #PageTable.RecNumber\r\n");

                    sql.Append("DROP TABLE #PageTable\r\n");
                }

                GetData.CommandText = sql.ToString();
                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    paras.PageTotal = sqlReader.GetInt32(0);
                    paras.ItemTotal = sqlReader.GetInt32(1);
                    paras.PageNumber = sqlReader.GetInt32(2);
                    sqlReader.NextResult();
                }
            }
            catch (Exception ex)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
                throw new Exception(ex.Message + "<br/>" + sql.ToString(), ex);
            }

            return sqlReader;
        }
        public static SqlDataReader GetReaderAllVisits(Business.QuickParameters paras)
        {
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObjectUset_Visits_LoadAll_InsertedDate_Desc";

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, paras.CommunityID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, Helper.GetObjectTypeNumericID("Community")));

                if (paras.OnlyWithImage.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit, 1));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit));

                if (paras.UserIDLogedIn.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID_LogedIn", SqlDbType.UniqueIdentifier, paras.UserIDLogedIn.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID_LogedIn", SqlDbType.UniqueIdentifier));

                GetData.Parameters.Add(SqlHelper.AddParameter("@Amount", SqlDbType.Int, paras.Amount));

                if (paras.ShowState == null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, DBNull.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)(paras.ShowState.Value)));

                if (paras.FromInserted == null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromInserted", SqlDbType.DateTime, new DateTime(2000, 1, 1)));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromInserted", SqlDbType.DateTime, paras.FromInserted.Value.Date));

                if (paras.ToInserted == null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToInserted", SqlDbType.DateTime, new DateTime(2100, 1, 1)));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToInserted", SqlDbType.DateTime, paras.ToInserted.Value.Date));

                if (paras.Featured != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Featured", SqlDbType.Int, paras.Featured.Value));

                if (paras.FromStartDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromStartDate", SqlDbType.DateTime, paras.FromStartDate.Value.Date));

                if (paras.ToEndDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToEndDate", SqlDbType.DateTime, paras.ToEndDate.Value.Date));

                if (paras.DateQueryMethode != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DateQueryMethode", SqlDbType.Int, (int)(paras.DateQueryMethode.Value)));

                if (!string.IsNullOrEmpty(paras.Country))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Country", SqlDbType.NVarChar, 5, paras.Country));

                if (!string.IsNullOrEmpty(paras.Zip))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Zip", SqlDbType.NVarChar, 8, Helper.PrepareLike(paras.Zip, false, true)));

                if (!string.IsNullOrEmpty(paras.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@City", SqlDbType.NVarChar, 50, Helper.PrepareLike(paras.City, false, true)));

                if (paras.DistanceKm != null && paras.GeoLat != null & paras.GeoLong != null)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Lat", SqlDbType.Float, paras.GeoLat.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Long", SqlDbType.Float, paras.GeoLong.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DistanceKm", SqlDbType.Int, paras.DistanceKm.Value));
                }

                if (!string.IsNullOrEmpty(paras.ParentObjectID))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_ObjectID", SqlDbType.UniqueIdentifier, new Guid(paras.ParentObjectID)));

                if (paras.CheckUserRoleRight)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleInt(paras.UserRole)));

                if (paras.PageNumber > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageNumber", SqlDbType.Int, paras.PageNumber));

                if (paras.PageSize > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageSize", SqlDbType.Int, paras.PageSize));

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    paras.PageTotal = sqlReader.GetInt32(0);
                    paras.ItemTotal = sqlReader.GetInt32(1);
                    sqlReader.NextResult();
                }
            }
            catch (Exception e)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
                throw e;
            }

            return sqlReader;
        }

        public static SqlDataReader GetReaderAllBest(Business.QuickParametersUser paras)
        {
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = string.Format("hisp_DataObjectUser_LoadAll_CalculatedRank");

                GetData.Parameters.Add(SqlHelper.AddParameter("@ForObjectType", SqlDbType.Int, paras.ForObjectType.Value));

                if (paras.OnlyWithImage != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit, 1));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit));

                GetData.Parameters.Add(SqlHelper.AddParameter("@Amount", SqlDbType.Int, paras.Amount));

                if (paras.ShowState != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)(paras.ShowState.Value)));

                if (paras.FromInserted == null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromInserted", SqlDbType.DateTime, new DateTime(2000, 1, 1)));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromInserted", SqlDbType.DateTime, paras.FromInserted.Value.Date));

                if (paras.ToInserted == null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToInserted", SqlDbType.DateTime, new DateTime(2100, 1, 1)));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToInserted", SqlDbType.DateTime, paras.ToInserted.Value.Date));

                if (paras.Featured != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Featured", SqlDbType.Int, paras.Featured.Value));

                if (paras.FromStartDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromStartDate", SqlDbType.DateTime, paras.FromStartDate.Value.Date));

                if (paras.ToEndDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToEndDate", SqlDbType.DateTime, paras.ToEndDate.Value.Date));

                if (paras.DateQueryMethode != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DateQueryMethode", SqlDbType.Int, (int)(paras.DateQueryMethode.Value)));

                if (!string.IsNullOrEmpty(paras.Country))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Country", SqlDbType.NVarChar, 5, paras.Country));

                if (!string.IsNullOrEmpty(paras.Zip))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Zip", SqlDbType.NVarChar, 8, Helper.PrepareLike(paras.Zip, false, true)));

                if (!string.IsNullOrEmpty(paras.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@City", SqlDbType.NVarChar, 50, Helper.PrepareLike(paras.City, false, true)));

                if (paras.DistanceKm != null && paras.GeoLat != null & paras.GeoLong != null)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Lat", SqlDbType.Float, paras.GeoLat.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Long", SqlDbType.Float, paras.GeoLong.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DistanceKm", SqlDbType.Int, paras.DistanceKm.Value));
                }

                if (!string.IsNullOrEmpty(paras.ParentObjectID))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_ObjectID", SqlDbType.UniqueIdentifier, new Guid(paras.ParentObjectID)));

                if (paras.CheckUserRoleRight)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleInt(paras.UserRole)));

                if (paras.PageNumber > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageNumber", SqlDbType.Int, paras.PageNumber));

                if (paras.PageSize > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageSize", SqlDbType.Int, paras.PageSize));

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    paras.PageTotal = sqlReader.GetInt32(0);
                    paras.ItemTotal = sqlReader.GetInt32(1);
                    sqlReader.NextResult();
                }
            }
            catch (Exception e)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
                throw e;
            }

            return sqlReader;
        }

        public static SqlDataReader GetReaderAllFriends(Business.QuickParametersFriends paras)
        {
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObjectFriends_LoadAll";
                paras.Direction = QuickSortDirection.Asc;
                paras.SortBy = QuickSort.Title;
                if (paras.CurrentUserID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, paras.CurrentUserID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier));

                if (paras.OnlyWithImage.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit, paras.OnlyWithImage.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyWithPicture", SqlDbType.Bit));

                if (!paras.FriendType.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@UFR_TypeID", SqlDbType.Int, DBNull.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@UFR_TypeID", SqlDbType.Int, (int)paras.FriendType.Value));

                if (!string.IsNullOrEmpty(paras.Nickname))
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@UserName", SqlDbType.NVarChar, 64, Helper.PrepareLike(paras.Nickname, true, true)));
                }
                else
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@UserName", SqlDbType.NVarChar, 64, DBNull.Value));
                }

                if (!string.IsNullOrEmpty(paras.FriendSearchParam))
                {
                    paras.FriendSearchParam = Helper.PrepareLike(paras.FriendSearchParam, true, true);
                    GetData.Parameters.Add(SqlHelper.AddParameter("@GenSearchParam", SqlDbType.NVarChar, 64, paras.FriendSearchParam));
                }
                else
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@GenSearchParam", SqlDbType.NVarChar, 64, DBNull.Value));
                }

                if (!paras.OnlyNotBlocked.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyNotBlocked", SqlDbType.Bit, DBNull.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyNotBlocked", SqlDbType.Bit, paras.OnlyNotBlocked));

                GetData.Parameters.Add(SqlHelper.AddParameter("@Amount", SqlDbType.Int, paras.Amount));

                if (paras.ShowState != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)(paras.ShowState.Value)));

                if (paras.FromInserted != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromInserted", SqlDbType.DateTime, paras.FromInserted.Value.Date));

                if (paras.ToInserted != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToInserted", SqlDbType.DateTime, paras.ToInserted.Value.Date));

                if (paras.Featured != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Featured", SqlDbType.Int, paras.Featured.Value));

                if (paras.FromStartDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FromStartDate", SqlDbType.DateTime, paras.FromStartDate.Value.Date));

                if (paras.ToEndDate != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ToEndDate", SqlDbType.DateTime, paras.ToEndDate.Value.Date));

                if (paras.DateQueryMethode != null)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DateQueryMethode", SqlDbType.Int, (int)(paras.DateQueryMethode.Value)));

                if (!string.IsNullOrEmpty(paras.Country))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Country", SqlDbType.NVarChar, 5, paras.Country));

                if (!string.IsNullOrEmpty(paras.Zip))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Zip", SqlDbType.NVarChar, 8, Helper.PrepareLike(paras.Zip, false, true)));

                if (!string.IsNullOrEmpty(paras.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@City", SqlDbType.NVarChar, 50, Helper.PrepareLike(paras.City, false, true)));

                if (paras.DistanceKm != null && paras.GeoLat != null & paras.GeoLong != null)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Lat", SqlDbType.Float, paras.GeoLat.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Geo_Long", SqlDbType.Float, paras.GeoLong.Value));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@DistanceKm", SqlDbType.Int, paras.DistanceKm.Value));
                }
                if (paras.OnlyGeoTagged != null)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OnlyGeoTagged", SqlDbType.Bit, paras.OnlyGeoTagged.Value));
                }
                if (paras.IncludeGroups != null)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@IncludeGroups", SqlDbType.Bit, paras.IncludeGroups.Value));
                }
                if (!string.IsNullOrEmpty(paras.Nickname))
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_Nickname", SqlDbType.NVarChar, 258, Common.Extensions.PrepareLike(paras.Nickname, true, true)));
                }
                if (!string.IsNullOrEmpty(paras.Title))
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Title", SqlDbType.NVarChar, 258, Common.Extensions.PrepareLike(paras.Title, true, true)));
                }
                if (!string.IsNullOrEmpty(paras.Description))
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Description", SqlDbType.NVarChar, 258, Common.Extensions.PrepareLike(paras.Description, true, true)));
                }
                GetData.Parameters.Add(SqlHelper.AddParameter("@SortAttr", SqlDbType.VarChar, 32, paras.SortBy.ToString()));
                GetData.Parameters.Add(SqlHelper.AddParameter("@SortDir", SqlDbType.VarChar, 4, paras.Direction.ToString()));

                if (!string.IsNullOrEmpty(paras.ParentObjectID))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_ObjectID", SqlDbType.UniqueIdentifier, new Guid(paras.ParentObjectID)));

                if (paras.CheckUserRoleRight)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleInt(paras.UserRole)));

                if (paras.PageNumber > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageNumber", SqlDbType.Int, paras.PageNumber));

                if (paras.PageSize > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PageSize", SqlDbType.Int, paras.PageSize));

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    paras.PageTotal = sqlReader.GetInt32(0);
                    paras.ItemTotal = sqlReader.GetInt32(1);
                    sqlReader.NextResult();
                }
            }
            catch (Exception e)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
                throw e;
            }

            return sqlReader;
        }

        public static SqlDataReader GetReaderAllTags(Business.QuickParametersTag paras)
        {
            string strConn = Helper.GetSiemeConnectionString();

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                bool blnCheckDate = ((paras.RelevanceGroupAmount.HasValue && paras.RelevanceGroupAmount.Value > 0) || (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All));

                StringBuilder sql = new StringBuilder(500);

                if (blnCheckDate)
                {
                    sql.AppendLine("DECLARE @BIS AS DATETIME");
                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                    {
                        if (!paras.RelevanceGroupAmount.HasValue)
                            paras.RelevanceGroupAmount = 365;

                        // maximal 10 Jahre
                        if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Hour && paras.RelevanceGroupAmount > 87600)
                            paras.RelevanceGroupAmount = 87600;
                        else if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Day && paras.RelevanceGroupAmount > 3650)
                            paras.RelevanceGroupAmount = 3650;
                        else if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Week && paras.RelevanceGroupAmount > 520)
                            paras.RelevanceGroupAmount = 520;
                        else if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Month && paras.RelevanceGroupAmount > 120)
                            paras.RelevanceGroupAmount = 120;
                        else if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Quarter && paras.RelevanceGroupAmount > 40)
                            paras.RelevanceGroupAmount = 40;
                        else if (paras.RelevanceGroup == QuickTagCloudRelevanceGroup.Year && paras.RelevanceGroupAmount > 10)
                            paras.RelevanceGroupAmount = 10;

                        sql.AppendFormat("SET @BIS = DATEADD({0}, -{1}, GETDATE())\r\n", paras.RelevanceGroup, paras.RelevanceGroupAmount);
                    }
                    else
                        sql.AppendLine("SET @BIS = DATEADD(Year, -100, GETDATE())");
                }

                if (paras.Relevance == QuickTagCloudRelevance.ViewLog)
                {
                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("SELECT TGW.OBJ_ID, SUM(REL.VIW_Count) * (1.0 / (DATEDIFF({0}, REL.VIW_InsertedDate, GETDATE()) + 1)) AS Faktor\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("SELECT TGW.OBJ_ID, 1.0 AS Faktor");

                    sql.AppendLine("INTO #TEMP_TBL");
                    sql.AppendLine("FROM hiobj_Tag AS TGW");
                    sql.AppendLine("INNER JOIN hirel_ObjToObj_OTO AS TGL ON TGW.OBJ_ID = TGL.OTO_Obj2_ID AND TGL.OTO_Obj2_Type = 5 AND TGL.OTO_RelType IS NULL");
                    sql.AppendLine("INNER JOIN hitbl_ViewLog_VIW AS REL ON TGL.OTO_Obj1_ID = REL.OBJ_ID");
                    if (blnCheckDate)
                        sql.AppendLine("AND REL.VIW_InsertedDate >= @BIS");

                    sql.AppendLine("INNER JOIN hitbl_DataObject_OBJ AS SEL ON TGL.OTO_Obj1_ID = SEL.OBJ_ID AND SEL.OBJ_ShowState <> 3");
                    if (paras.RelatedObjectType.HasValue)
                        sql.AppendFormat("AND SEL.OBJ_Type = {0}\r\n", paras.RelatedObjectType.Value);

                    if (paras.RelatedUserID.HasValue)
                        sql.AppendFormat("AND SEL.USR_ID = '{0}'\r\n", paras.RelatedUserID.Value);

                    if (paras.RelatedCommunityID.HasValue)
                        sql.AppendFormat("AND SEL.CTY_ID = '{0}'\r\n", paras.RelatedCommunityID.Value);

                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("GROUP BY TGW.OBJ_ID, DATEDIFF({0}, REL.VIW_InsertedDate, GETDATE())\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("GROUP BY TGW.OBJ_ID");
                }
                else if (paras.Relevance == QuickTagCloudRelevance.ObjectView)
                {
                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("SELECT TGW.OBJ_ID, SUM(REL.OBJ_ViewCount) * (1.0 / (DATEDIFF({0}, REL.OBJ_StartDate, GETDATE()) + 1)) AS Faktor\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("SELECT TGW.OBJ_ID, 1.0 AS Faktor");

                    sql.AppendLine("INTO #TEMP_TBL");
                    sql.AppendLine("FROM hiobj_Tag AS TGW");
                    sql.AppendLine("INNER JOIN hirel_ObjToObj_OTO AS TGL ON TGW.OBJ_ID = TGL.OTO_Obj2_ID AND TGL.OTO_Obj2_Type = 5 AND TGL.OTO_RelType IS NULL");
                    sql.AppendLine("INNER JOIN hitbl_DataObject_OBJ AS REL ON TGL.OTO_Obj1_ID = REL.OBJ_ID AND REL.OBJ_ShowState <> 3");
                    if (blnCheckDate)
                        sql.AppendLine("AND REL.OBJ_StartDate >= @BIS");

                    if (paras.RelatedObjectType.HasValue)
                        sql.AppendFormat("AND REL.OBJ_Type = {0}\r\n", paras.RelatedObjectType.Value);

                    if (paras.RelatedUserID.HasValue)
                        sql.AppendFormat("AND REL.USR_ID = '{0}'\r\n", paras.RelatedUserID.Value);

                    if (paras.RelatedCommunityID.HasValue)
                        sql.AppendFormat("AND REL.CTY_ID = '{0}'\r\n", paras.RelatedCommunityID.Value);

                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("GROUP BY TGW.OBJ_ID, DATEDIFF({0}, REL.OBJ_StartDate, GETDATE())\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("GROUP BY TGW.OBJ_ID");
                }
                else if (paras.Relevance == QuickTagCloudRelevance.RelatedObjects)
                {
                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("SELECT TGW.OBJ_ID, COUNT(REL.OBJ_ID) * (1.0 / (DATEDIFF({0}, REL.OBJ_StartDate, GETDATE()) + 1)) AS Faktor\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("SELECT TGW.OBJ_ID, 1.0 AS Faktor");

                    sql.AppendLine("INTO #TEMP_TBL");
                    sql.AppendLine("FROM hiobj_Tag AS TGW");
                    sql.AppendLine("INNER JOIN hirel_ObjToObj_OTO AS TGL ON TGW.OBJ_ID = TGL.OTO_Obj2_ID AND TGL.OTO_Obj2_Type = 5 AND TGL.OTO_RelType IS NULL");
                    sql.AppendLine("INNER JOIN hitbl_DataObject_OBJ AS REL ON TGL.OTO_Obj1_ID = REL.OBJ_ID AND REL.OBJ_ShowState <> 3");
                    if (blnCheckDate)
                        sql.AppendLine("AND REL.OBJ_StartDate >= @BIS");

                    if (paras.RelatedObjectType.HasValue)
                        sql.AppendFormat("AND REL.OBJ_Type = {0}\r\n", paras.RelatedObjectType.Value);

                    if (paras.RelatedUserID.HasValue)
                        sql.AppendFormat("AND REL.USR_ID = '{0}'\r\n", paras.RelatedUserID.Value);

                    if (paras.RelatedCommunityID.HasValue)
                        sql.AppendFormat("AND REL.CTY_ID = '{0}'\r\n", paras.RelatedCommunityID.Value);

                    if (paras.RelevanceGroup != QuickTagCloudRelevanceGroup.All)
                        sql.AppendFormat("GROUP BY TGW.OBJ_ID, DATEDIFF({0}, REL.OBJ_StartDate, GETDATE())\r\n", paras.RelevanceGroup);
                    else
                        sql.AppendLine("GROUP BY TGW.OBJ_ID");
                }
                else
                {
                    sql.AppendLine("SELECT TGW.OBJ_ID, SUM(REL.OBJ_ViewCount) * 1.0 AS Faktor");

                    sql.AppendLine("INTO #TEMP_TBL");
                    sql.AppendLine("FROM hiobj_Tag AS TGW");
                    sql.AppendLine("INNER JOIN hirel_ObjToObj_OTO AS TGL ON TGW.OBJ_ID = TGL.OTO_Obj2_ID AND TGL.OTO_Obj2_Type = 5 AND TGL.OTO_RelType IS NULL");
                    sql.AppendLine("INNER JOIN hitbl_DataObject_OBJ AS REL ON TGL.OTO_Obj1_ID = REL.OBJ_ID AND REL.OBJ_ShowState <> 3");
                    if (blnCheckDate)
                        sql.AppendLine("AND REL.OBJ_StartDate >= @BIS");

                    if (paras.RelatedObjectType.HasValue)
                        sql.AppendFormat("AND REL.OBJ_Type = {0}\r\n", paras.RelatedObjectType.Value);

                    if (paras.RelatedUserID.HasValue)
                        sql.AppendFormat("AND REL.USR_ID = '{0}'\r\n", paras.RelatedUserID.Value);

                    if (paras.RelatedCommunityID.HasValue)
                        sql.AppendFormat("AND REL.CTY_ID = '{0}'\r\n", paras.RelatedCommunityID.Value);

                    sql.AppendLine("GROUP BY TGW.OBJ_ID");
                }

                sql.AppendLine("SELECT OBJ_ID, SUM(Faktor) AS Summe");
                sql.AppendLine("INTO #TEMP_SEL");
                sql.AppendLine("FROM #TEMP_TBL");
                sql.AppendLine("GROUP BY OBJ_ID");

                if (paras.Amount > 0)
                    sql.AppendFormat("SELECT TOP {0} hitbl_DataObject_OBJ.*, 0 AS OBJ_SelectCount, #TEMP_SEL.Summe AS TGW_Relevance\r\n", paras.Amount);
                else
                    sql.AppendLine("SELECT hitbl_DataObject_OBJ.*, 0 AS OBJ_SelectCount, #TEMP_SEL.Summe AS TGW_Relevance");

                sql.AppendLine("FROM #TEMP_SEL INNER JOIN hitbl_DataObject_OBJ ON hitbl_DataObject_OBJ.OBJ_ID = #TEMP_SEL.OBJ_ID");
                sql.AppendLine("ORDER BY TGW_Relevance DESC");

                sql.AppendLine("DROP TABLE #TEMP_TBL");
                sql.AppendLine("DROP TABLE #TEMP_SEL");

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sql.ToString();

                Conn.Open();
                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.HasRows)
                {
                    paras.PageTotal = 1;
                    paras.ItemTotal = paras.Amount.Value;
                }
            }
            catch (Exception e)
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
                throw e;
            }

            return sqlReader;
        }

        public static string GetSelectString<T>(Business.DataObject listItem, Business.QuickParameters paras, SqlCommand cmd) where T : Business.DataObject, new()
        {
            StringBuilder sql = new StringBuilder();
            if (paras.DisablePaging.HasValue && paras.DisablePaging.Value && paras.Amount > 0)
                sql.AppendFormat("SELECT TOP {0} ", paras.Amount);
            else
                sql.Append("SELECT ");
            sql.Append("hitbl_DataObject_OBJ.*,\r\n");

            switch (paras.SortBy)
            {
                case QuickSort.Viewed:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_ViewCount as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.Commented:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_CommentCount as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.RatedCount:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_ViewCount as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.IncentivePoints:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_IncentivePoints as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.MemberCount:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_MemberCount as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.Agility:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_Agility as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.RatedAverage:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_RatedAverage as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.RatedConsolidated:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_RatedConsolidated as OBJ_SelectCount\r\n");
                    break;
                case QuickSort.FavoriteCount:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_FavoriteCount as OBJ_SelectCount\r\n");
                    break;
                default:
                    sql.Append("hitbl_DataObject_OBJ.OBJ_ViewCount as OBJ_SelectCount\r\n");
                    break;
            }

            sql.AppendFormat("{0}\r\n", listItem.GetSelectSQL(paras, cmd.Parameters));

            return sql.ToString();
        }

        private static string GetSortOrderString(QuickSort sortBy, QuickSortDirection direction)
        {
            if (sortBy == QuickSort.NotSorted)
                return string.Empty;
            else if (sortBy == QuickSort.InsertedDate)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_InsertedDate {0}", direction);
            else if (sortBy == QuickSort.Viewed)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_ViewCount {0}", direction);
            else if (sortBy == QuickSort.Commented)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_CommentCount {0}", direction);
            else if (sortBy == QuickSort.RatedCount)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_RatedCount {0}", direction);
            else if (sortBy == QuickSort.RatedAverage)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_RatedAverage {0}", direction);
            else if (sortBy == QuickSort.IncentivePoints)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_IncentivePoints {0}", direction);
            else if (sortBy == QuickSort.MemberCount)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_MemberCount {0}", direction);
            else if (sortBy == QuickSort.Random)
                return ", NEWID()";
            else if (sortBy == QuickSort.Agility)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_Agility {0}", direction);
            else if (sortBy == QuickSort.RatedConsolidated)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_RatedConsolidated {0}", direction);
            else if (sortBy == QuickSort.FavoriteCount)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_FavoriteCount {0}", direction);
            else if (sortBy == QuickSort.Title)
                return string.Format(", hitbl_DataObject_OBJ.OBJ_Title {0}", direction);
            else
                return string.Empty;
        }

        #endregion GetObject

        #region DBCache

        public static DataTable LoadFromDBCache(string key)
        {
            if (key.IndexOf("-XX-") < 0) // in der DB kann es nur Objekte von Anonymen-User geben
                return null;

            SqlConnection Conn = new SqlConnection(strConn);
            DataTable dt = null;
            StringReader reader = null;
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DBCache_Load";

                GetData.Parameters.Add(SqlHelper.AddParameter("@DBC_Key", SqlDbType.VarChar, 250, key));

                DataSet ds = new DataSet();
                Conn.Open();
                object objDT = GetData.ExecuteScalar();
                if (objDT != null)
                {
                    reader = new StringReader(objDT.ToString());
                    ds.ReadXml(reader);
                    dt = ds.Tables[0];
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                Conn.Close();
            }
            return dt;
        }

        #endregion  DBCache

        private static bool columnExists(SqlDataReader reader, string columnName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }

        private static bool columnExists(DataRow row, string ColumnName)
        {
            return row.Table.Columns.Contains(ColumnName);
        }

        public static Guid? GetTagGuid(string tagWord)
        {
            if (string.IsNullOrEmpty(tagWord))
                return null;

            if (Common.Extensions.IsGuid(tagWord))
                return Common.Extensions.ToGuid(tagWord);

            Guid? tagId = null;

            SqlConnection connection = new SqlConnection(strConn);
            try
            {
                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "hisp_TagWord_GetIDByWord";

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@TGW_Wordlower", SqlDbType.NVarChar, tagWord.ToLower()));

                connection.Open();
                object returnValue = sqlCommand.ExecuteScalar();
                if (returnValue != null)
                    tagId = Common.Extensions.ToNullableGuid(returnValue.ToString());
            }
            finally
            {
                connection.Close();
            }
            return tagId;
        }

        public static Dictionary<int, int> GetEmphasisList(XmlElement xmlRoot)
        {
            Dictionary<int, int> list = new Dictionary<int, int>();
            foreach (XmlElement xmlItem in xmlRoot.SelectNodes("Emphasis"))
            {
                list.Add(int.Parse(xmlItem.GetAttribute("ObjectType")), Convert.ToInt32(xmlItem.InnerText));
            }
            return list;
        }

        public static void SetEmphasisList(XmlElement xmlRoot, Dictionary<int, int> list)
        {
            ClearEmphasisList(xmlRoot);

            while (list.Count > 0)
            {
                int intMax = -1;
                int? enuType = null;
                foreach (KeyValuePair<int, int> kvp in list)
                {
                    if (kvp.Value > intMax)
                    {
                        intMax = kvp.Value;
                        enuType = kvp.Key;
                    }
                }
                if (enuType != null)
                {
                    AddEmphasisList(xmlRoot, enuType.Value, intMax);
                    list.Remove(enuType.Value);
                }
            }
        }

        private static void ClearEmphasisList(XmlElement xmlRoot)
        {
            XmlNode xmlItem = xmlRoot.SelectSingleNode("Emphasis");
            while (xmlItem != null)
            {
                xmlRoot.RemoveChild(xmlItem);
                xmlItem = xmlRoot.SelectSingleNode("Emphasis");
            }
        }

        private static void AddEmphasisList(XmlElement xmlRoot, int objectType, int percent)
        {
            XmlElement xmlItem = XmlHelper.AppendNode(xmlRoot, "Emphasis") as XmlElement;
            xmlItem.SetAttribute("ObjectType", Convert.ToString(objectType));
            xmlItem.InnerText = percent.ToString();
        }
    }

    internal class EmphasisSorter : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}