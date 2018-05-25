// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Caching;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataAccess.Data
{
    internal class DataObject
    {
        private static string strConn = Helper.GetSiemeConnectionString();

        public Guid? GetCommunityIDByVirtualURL(string VirtualUrl)
        {
            string ID = string.Empty;
            Guid? retVal = null;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_GetIDByVirtualUrl";
                GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_VirtualUrl", SqlDbType.NVarChar, 100, VirtualUrl.CropString(100)));
                Conn.Open();
                ID = GetData.ExecuteScalar().ToString();
            }
            finally
            {
                Conn.Close();
            }
            if (!string.IsNullOrEmpty(ID))
                retVal = ID.ToGuid();
            return retVal;
        }

        public Guid? GetUserIDByNickname(string Nickname)
        {
            string ID = string.Empty;
            Guid? retVal = null;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "aspnet_Membership_GetUserByName";
                GetData.Parameters.Add(SqlHelper.AddParameter("@ApplicationName", SqlDbType.NVarChar, 256, "CSBooster"));
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserName", SqlDbType.NVarChar, 256, Nickname.CropString(256)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.Now));
                GetData.Parameters.Add(SqlHelper.AddParameter("@UpdateLastActivity", SqlDbType.Bit, 0));
                Conn.Open();
                SqlDataReader dr = GetData.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr["UserId"].ToString();
                }
            }
            finally
            {
                Conn.Close();
            }
            if (!string.IsNullOrEmpty(ID))
                retVal = ID.ToGuid();
            return retVal;
        }

        public static string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetInsertSQL(Business.DataObject item, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetUpdateSQL(Business.DataObject item, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return string.Empty;
        }

        public static void FillObject(Business.DataObject item, SqlDataReader sqlReader)
        {
            item.objectID = sqlReader["OBJ_ID"].ToString().ToGuid();
            item.userID = sqlReader["USR_ID"].ToString().ToGuid();
            item.nickname = sqlReader["USR_Nickname"].ToString();
            item.email = sqlReader["OBJ_Email"].ToString();
            item.ip = sqlReader["OBJ_IP"].ToString();
            item.externalObjectID = sqlReader["OBJ_EXT_ID"] != DBNull.Value ? sqlReader["OBJ_EXT_ID"].ToString() : string.Empty;
            if (sqlReader["PAR_ID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["PAR_ID"].ToString()))
                item.partnerID = sqlReader["PAR_ID"].ToString().ToGuid();

            if (sqlReader["INS_ID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["INS_ID"].ToString()))
            {
                item.instanceID = sqlReader["INS_ID"].ToString().ToGuid();
            }
            item.communityID = sqlReader["CTY_ID"].ToString().ToGuid();
            item.objectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
            item.title = sqlReader["OBJ_Title"].ToString();
            item.description = sqlReader["OBJ_Description"].ToString();
            item.descriptionLinked = sqlReader["OBJ_DescriptionLinked"].ToString();
            item.descriptionMobile = sqlReader["OBJ_DescriptionMobile"].ToString();
            item.image = sqlReader["OBJ_URLImageSmall"].ToString();
            item.copyright = Convert.ToInt32(sqlReader["OBJ_Copyright"]);
            item.tagList = sqlReader["OBJ_TagList"].ToString();
            item.objectStatus = (ObjectStatus)Convert.ToInt32(sqlReader["OBJ_Status"]);
            item.friendVisibility = (FriendType)Convert.ToInt32(sqlReader["OBJ_FriendVisibility"]);
            item.langCode = sqlReader["OBJ_LangCode"].ToString();
            item.dateInserted = Convert.ToDateTime(sqlReader["OBJ_InsertedDate"]);
            if (sqlReader["OBJ_UpdatedDate"] != DBNull.Value)
                item.dateUpdated = Convert.ToDateTime(sqlReader["OBJ_UpdatedDate"]);
            item.insertedBy = sqlReader["OBJ_InsertedBy"].ToString();
            item.updatedBy = sqlReader["OBJ_UpdatedBy"].ToString();
            item.viewCount = Convert.ToInt32(sqlReader["OBJ_ViewCount"]);
            item.ratedCount = Convert.ToInt32(sqlReader["OBJ_RatedCount"]);
            item.ratedAmount = Convert.ToInt32(sqlReader["OBJ_RatingAmount"]);
            item.ratedAverage = Convert.ToDecimal(sqlReader["OBJ_RatedAverage"]);
            item.ratedConsolidated = Convert.ToDecimal(sqlReader["OBJ_RatedConsolidated"]);
            item.commentedCount = Convert.ToInt32(sqlReader["OBJ_CommentCount"]);
            item.incentivePoints = Convert.ToInt32(sqlReader["OBJ_IncentivePoints"]);
            item.memberCount = Convert.ToInt32(sqlReader["OBJ_MemberCount"]);

            item.agility = Convert.ToInt32(sqlReader["OBJ_Agility"]);
            item.featured = Convert.ToInt32(sqlReader["OBJ_Featured"]);

            if (sqlReader["OBJ_SpezialXml"] != DBNull.Value && sqlReader["OBJ_SpezialXml"].ToString().Length > 0)
            {
                item.xmlData.LoadXml(sqlReader["OBJ_SpezialXml"].ToString());

                item.pictureFormat.Add(PictureVersion.XS, XmlHelper.GetElementValue(item.xmlData.DocumentElement, "Picture" + PictureVersion.XS, string.Empty));
                item.pictureFormat.Add(PictureVersion.S, XmlHelper.GetElementValue(item.xmlData.DocumentElement, "Picture" + PictureVersion.S, string.Empty));
                item.pictureFormat.Add(PictureVersion.M, XmlHelper.GetElementValue(item.xmlData.DocumentElement, "Picture" + PictureVersion.M, string.Empty));
                item.pictureFormat.Add(PictureVersion.L, XmlHelper.GetElementValue(item.xmlData.DocumentElement, "Picture" + PictureVersion.L, string.Empty));
            }

            if (sqlReader["OBJ_OriginalObjID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["OBJ_OriginalObjID"].ToString()))
                item.originalObjectID = sqlReader["OBJ_OriginalObjID"].ToString().ToGuid();

            item.CountryCode = sqlReader["OBJ_Country_ISO"] != DBNull.Value ? sqlReader["OBJ_Country_ISO"].ToString() : string.Empty;
            item.Region = sqlReader["OBJ_Region"] != DBNull.Value ? sqlReader["OBJ_Region"].ToString() : string.Empty;
            item.Street = sqlReader["OBJ_Street"] != DBNull.Value ? sqlReader["OBJ_Street"].ToString() : string.Empty;
            item.Zip = sqlReader["OBJ_Zip"] != DBNull.Value ? sqlReader["OBJ_Zip"].ToString() : string.Empty;
            item.City = sqlReader["OBJ_City"] != DBNull.Value ? sqlReader["OBJ_City"].ToString() : string.Empty;
            item.Geo_Lat = sqlReader["OBJ_Geo_Lat"] != DBNull.Value ? Convert.ToDouble(sqlReader["OBJ_Geo_Lat"]) : double.MinValue;
            item.Geo_Long = sqlReader["OBJ_Geo_Long"] != DBNull.Value ? Convert.ToDouble(sqlReader["OBJ_Geo_Long"]) : double.MinValue;
            item.ShowState = (ObjectShowState)Convert.ToInt32(sqlReader["OBJ_ShowState"]);

            if (sqlReader["OBJ_StartDate"] != DBNull.Value)
                item.startDate = Convert.ToDateTime(sqlReader["OBJ_StartDate"]);
            else
                item.startDate = item.dateInserted;

            if (sqlReader["OBJ_EndDate"] != DBNull.Value)
                item.endDate = Convert.ToDateTime(sqlReader["OBJ_EndDate"]);

            DateTime dt = new DateTime(3000, 1, 1);
            if (item.endDate >= dt)
                item.endDate = DateTime.MaxValue;

            item.UrlXSLT = sqlReader["OBJ_URL_XSLT"] != DBNull.Value ? sqlReader["OBJ_URL_XSLT"].ToString() : string.Empty;

            if (sqlReader["OBJ_Parent_OBJ_ID"] != DBNull.Value)
                item.ParentObjectID = (sqlReader["OBJ_Parent_OBJ_ID"].ToString().ToGuid());

            if (sqlReader["OBJ_RoleRight"] != DBNull.Value)
                item.roleRight = Convert.ToInt32(sqlReader["OBJ_RoleRight"]);

            if (sqlReader.ColumnExists("INFO_OBJ_ID"))
            {
                item.GroupByInfo = new GroupByInfo();
                if (sqlReader["INFO_OBJ_ID"] != DBNull.Value)
                    item.GroupByInfo.ObjectID = sqlReader["INFO_OBJ_ID"].ToString().ToGuid();
                if (sqlReader["INFO_TYPE"] != DBNull.Value)
                    item.GroupByInfo.ObjectType = Convert.ToInt32(sqlReader["INFO_TYPE"]);
                if (sqlReader["INFO_TITLE"] != DBNull.Value)
                    item.GroupByInfo.Title = sqlReader["INFO_TITLE"].ToString();

            }

            if (sqlReader.ColumnExists("OBJ_FavoriteCount"))
            {
                item.favoriteCount = Convert.ToInt32(sqlReader["OBJ_FavoriteCount"]);
            }
            else
            {
                item.favoriteCount = 0;
            }

            if (sqlReader.ColumnExists("OBJ_SelectCount"))
            {
                if (sqlReader["OBJ_SelectCount"] != DBNull.Value)
                    item.selectCount = Convert.ToDouble(sqlReader["OBJ_SelectCount"]);
                else
                    item.selectCount = 0.0;
            }
            else
            {
                item.selectCount = 0.0;
            }
            item.objectState = ObjectState.Saved;
        }

        public static void Insert(Business.DataObject item)
        {
            Guid? finalObjectID = null;
            if (item.ObjectType == Helper.GetObjectType("User").NumericId)
            {
                finalObjectID = item.UserID;
            }
            else if (item.ObjectType == Helper.GetObjectType("Community").NumericId)
            {
                finalObjectID = item.CommunityID;
            }
            else if (item.ObjectType == Helper.GetObjectType("Page").NumericId)
            {
                finalObjectID = item.CommunityID;
            }
            else if (item.ObjectType == Helper.GetObjectType("ProfileCommunity").NumericId)
            {
                finalObjectID = item.CommunityID;
            }
            else if (item.ObjectID.HasValue)
            {
                finalObjectID = item.ObjectID;
            }
            else
            {
                finalObjectID = Guid.NewGuid();
            }

            FillGeoKooridnates(item);

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"
DECLARE @ObjectInserted BIT
SET @ObjectInserted = 0

IF (@OBJ_EXT_ID IS NULL AND @PAR_ID IS NULL) OR NOT EXISTS (SELECT * FROM hitbl_DataObject_OBJ WHERE OBJ_EXT_ID = @OBJ_EXT_ID AND PAR_ID = @PAR_ID)
BEGIN
INSERT INTO [hitbl_DataObject_OBJ]
   ([OBJ_ID]
   ,[USR_ID]
   ,[USR_Nickname]
   ,[OBJ_Email]
   ,[OBJ_IP]
   ,[OBJ_EXT_ID]
   ,[PAR_ID]
   ,[INS_ID]
   ,[CTY_ID]
   ,[OBJ_Type]
   ,[OBJ_Title]
   ,[OBJ_Description]
   ,[OBJ_DescriptionLinked]
   ,[OBJ_DescriptionMobile]
   ,[OBJ_URLImageSmall]
   ,[OBJ_Copyright]
   ,[OBJ_TagList]
   ,[OBJ_Status]
   ,[OBJ_LangCode]
   ,[OBJ_InsertedBy]
   ,[OBJ_SpezialXml]
   ,[OBJ_Country_ISO]
   ,[OBJ_Region]
   ,[OBJ_Street]
   ,[OBJ_Zip]
   ,[OBJ_City]
   ,[OBJ_Geo_Lat]
   ,[OBJ_Geo_Long]
   ,[OBJ_ShowState]
   ,[OBJ_GroupID]
   ,[OBJ_Featured]
   ,[OBJ_StartDate]
   ,[OBJ_EndDate]
   ,[OBJ_URL_XSLT]
   ,[OBJ_Parent_OBJ_ID]
   ,[OBJ_RoleRight]
   ,[OBJ_OriginalObjID]
   ,[OBJ_FriendVisibility]
) VALUES (
   @OBJ_ID,
   @USR_ID,
   @USR_Nickname,
   @OBJ_Email,
   @OBJ_IP,
   @OBJ_EXT_ID,
   @PAR_ID,
   @INS_ID,
   @CTY_ID,
   @OBJ_Type,
   @OBJ_Title,
   @OBJ_Description,
   @OBJ_DescriptionLinked,
   @OBJ_DescriptionMobile,
   @OBJ_URLImageSmall,
   @OBJ_Copyright,
   @OBJ_TagList,
   @OBJ_Status,
   @OBJ_LangCode,
   @OBJ_InsertedBy,
   @OBJ_SpezialXml,
   @OBJ_Country_ISO,
   @OBJ_Region,
   @OBJ_Street,
   @OBJ_Zip,
   @OBJ_City,
   @OBJ_Geo_Lat,
   @OBJ_Geo_Long,
   @OBJ_ShowState,
   @OBJ_GroupID,
   @OBJ_Featured,
   @OBJ_StartDate,
   @OBJ_EndDate,
   @OBJ_URL_XSLT,
   @OBJ_Parent_OBJ_ID,
   @OBJ_RoleRight,
   @OBJ_OriginalObjID,
   @OBJ_FriendVisibility
)");

                sb.AppendLine(item.GetInsertSQL(GetData.Parameters));

                sb.AppendLine(@"
SET @ObjectInserted = 1
END
SELECT @ObjectInserted");

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sb.ToString();
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, finalObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, item.UserID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_Nickname", SqlDbType.NVarChar, item.Nickname));

                if (!string.IsNullOrEmpty(item.Email))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Email", SqlDbType.NVarChar, 128, item.Email));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Email", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.IP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_IP", SqlDbType.VarChar, 128, item.IP));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_IP", SqlDbType.VarChar));

                if (!string.IsNullOrEmpty(item.ExternalObjectID))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EXT_ID", SqlDbType.NVarChar, 128, item.ExternalObjectID));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EXT_ID", SqlDbType.NVarChar));

                if (item.PartnerID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PAR_ID", SqlDbType.UniqueIdentifier, item.PartnerID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PAR_ID", SqlDbType.UniqueIdentifier));

                if (item.InstanceID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@INS_ID", SqlDbType.UniqueIdentifier, item.InstanceID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@INS_ID", SqlDbType.UniqueIdentifier));

                if (item.CommunityID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_ID", SqlDbType.UniqueIdentifier, item.CommunityID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_ID", SqlDbType.UniqueIdentifier));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)item.ObjectType));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Title", SqlDbType.NVarChar, item.Title));
                if (!string.IsNullOrEmpty(item.Description))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Description", SqlDbType.NVarChar, item.Description));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Description", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.descriptionLinked))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionLinked", SqlDbType.NVarChar, item.descriptionLinked));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionLinked", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.DescriptionMobile))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionMobile", SqlDbType.NVarChar, item.DescriptionMobile));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionMobile", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Image))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URLImageSmall", SqlDbType.VarChar, item.Image));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URLImageSmall", SqlDbType.VarChar));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Copyright", SqlDbType.Int, item.Copyright));

                if (!string.IsNullOrEmpty(item.TagList))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_TagList", SqlDbType.NVarChar, -1, item.TagList));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_TagList", SqlDbType.NVarChar));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Status", SqlDbType.Int, (int)item.Status));

                if (!string.IsNullOrEmpty(item.LangCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_LangCode", SqlDbType.VarChar, item.LangCode));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_LangCode", SqlDbType.VarChar));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_InsertedBy", SqlDbType.UniqueIdentifier, item.UserID.Value));

                if (item.xmlData.OuterXml.Length > 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_SpezialXml", SqlDbType.Xml, item.xmlData.OuterXml));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_SpezialXml", SqlDbType.Xml));

                if (item.OriginalObjectID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_OriginalObjID", SqlDbType.UniqueIdentifier, item.OriginalObjectID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_OriginalObjID", SqlDbType.UniqueIdentifier));

                if (!string.IsNullOrEmpty(item.CountryCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Country_ISO", SqlDbType.NVarChar, 5, item.CountryCode));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Country_ISO", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Region", SqlDbType.NVarChar, 25, item.Region));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Region", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Street))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Street", SqlDbType.NVarChar, 128, item.Street));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Street", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Zip))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Zip", SqlDbType.NVarChar, 8, item.Zip));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Zip", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_City", SqlDbType.NVarChar, 50, item.City));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_City", SqlDbType.NVarChar));

                if (item.Geo_Lat != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Lat", SqlDbType.Float, item.Geo_Lat));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Lat", SqlDbType.Float));

                if (item.Geo_Long != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Long", SqlDbType.Float, item.Geo_Long));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Long", SqlDbType.Float));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)item.ShowState));

                if (item.GroupID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_GroupID", SqlDbType.UniqueIdentifier, item.GroupID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_GroupID", SqlDbType.UniqueIdentifier));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Featured", SqlDbType.Int, item.Featured));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_StartDate", SqlDbType.DateTime, item.StartDate));

                DateTime dt = new DateTime(3000, 1, 1);
                if (item.EndDate > dt)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EndDate", SqlDbType.DateTime, dt));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EndDate", SqlDbType.DateTime, item.EndDate));

                if (!string.IsNullOrEmpty(item.UrlXSLT))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URL_XSLT", SqlDbType.NVarChar, 256, item.UrlXSLT));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URL_XSLT", SqlDbType.NVarChar));

                if (item.ParentObjectID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_OBJ_ID", SqlDbType.UniqueIdentifier, item.ParentObjectID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Parent_OBJ_ID", SqlDbType.UniqueIdentifier));

                if (item.roleRights != null && item.ObjectType == Helper.GetObjectType("Page").NumericId)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_RoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleRightValue(item.RoleRight)));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_RoleRight", SqlDbType.Int, Constants.ROLE_RIGHT_MAX_VALUE));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FriendVisibility", SqlDbType.Int, (int)item.FriendVisibility));

                GetData.Parameters.Add(new SqlParameter("@ObjectInserted", SqlDbType.Bit));
                GetData.Parameters["@ObjectInserted"].Direction = ParameterDirection.ReturnValue;

                Conn.Open();
                if (!bool.Parse(GetData.ExecuteScalar().ToString()))
                    throw new Exception("Object already exists");

                item.ObjectID = finalObjectID;
                item.objectState = ObjectState.Saved;
            }
            finally
            {
                Conn.Close();
            }

            TagListUpdate(item, "||"); // leere listen

            if (item.ObjectType == 0 || item.ObjectType == 5 || item.ObjectType == Helper.GetObjectType("User").NumericId || item.ObjectType == Helper.GetObjectType("ProfileCommunity").NumericId)
                return;

            // Action Log 'Insert' für diese Objekte erstellen
            try
            {
                AddActionLog(item.UserID.Value, Helper.GetObjectType("User").NumericId, item.ObjectID.Value.ToString(), item.ObjectType, TrackRule.Inserted);
            }
            catch
            {
            }
            if (item.ShowState == ObjectShowState.Published)
                QuickCacheHandler.RemoveCache(item.ObjectType, item.UserID);
        }

        public static void Update(Business.DataObject item, UserDataContext udc)
        {
            QuickCacheHandler.RemoveCache("_" + item.ObjectID.Value);
            string strTagList = GetTaglist(item.ObjectID.Value);

            FillGeoKooridnates(item);

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"
UPDATE [hitbl_DataObject_OBJ] SET 
[OBJ_Title] = @OBJ_Title,
[OBJ_Email] = @OBJ_Email,
[OBJ_IP] = @OBJ_IP,
[OBJ_EXT_ID] = @OBJ_EXT_ID,
[PAR_ID] = @PAR_ID,
[OBJ_Description] = @OBJ_Description,
[OBJ_DescriptionLinked] = @OBJ_DescriptionLinked,
[OBJ_DescriptionMobile] = @OBJ_DescriptionMobile,
[OBJ_URLImageSmall] = @OBJ_URLImageSmall,
[OBJ_Copyright] = @OBJ_Copyright,
[OBJ_TagList] = @OBJ_TagList,
[OBJ_Status] = @OBJ_Status,
[OBJ_UpdatedBy] = @OBJ_UpdatedBy,
[OBJ_UpdatedDate]= GETDATE(),
[OBJ_Country_ISO] = @OBJ_Country_ISO,
[OBJ_Region] = @OBJ_Region,
[OBJ_Street] = @OBJ_Street,
[OBJ_Zip] = @OBJ_Zip,
[OBJ_City] = @OBJ_City,
[OBJ_Geo_Lat] = @OBJ_Geo_Lat,
[OBJ_Geo_Long] = @OBJ_Geo_Long,
[OBJ_ShowState] = @OBJ_ShowState,
[OBJ_Featured] = @OBJ_Featured,
[OBJ_StartDate] = @OBJ_StartDate,
[OBJ_EndDate] = @OBJ_EndDate,
[OBJ_URL_XSLT] = @OBJ_URL_XSLT,
[OBJ_SpezialXml] = @OBJ_SpezialXml,
[OBJ_RoleRight] = @OBJ_RoleRight,
[OBJ_FriendVisibility] = @OBJ_FriendVisibility
WHERE	[OBJ_ID] = @OBJ_ID");

                string specificUpdateSQL = item.GetUpdateSQL(GetData.Parameters);
                if (!string.IsNullOrEmpty(specificUpdateSQL))
                    sb.AppendFormat(@"{0} WHERE [OBJ_ID] = @OBJ_ID", specificUpdateSQL);

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;
                GetData.CommandText = sb.ToString();
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Title", SqlDbType.NVarChar, item.Title));

                if (!string.IsNullOrEmpty(item.Email))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Email", SqlDbType.NVarChar, 128, item.Email));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Email", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.IP))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_IP", SqlDbType.VarChar, 128, item.IP));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_IP", SqlDbType.VarChar));

                if (!string.IsNullOrEmpty(item.ExternalObjectID))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EXT_ID", SqlDbType.NVarChar, 128, item.ExternalObjectID));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EXT_ID", SqlDbType.NVarChar));

                if (item.PartnerID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PAR_ID", SqlDbType.UniqueIdentifier, item.PartnerID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PAR_ID", SqlDbType.UniqueIdentifier));

                if (!string.IsNullOrEmpty(item.Description))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Description", SqlDbType.NVarChar, item.Description));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Description", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.descriptionLinked))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionLinked", SqlDbType.NVarChar, item.descriptionLinked));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionLinked", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.DescriptionMobile))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionMobile", SqlDbType.NVarChar, item.DescriptionMobile));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_DescriptionMobile", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Image))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URLImageSmall", SqlDbType.VarChar, item.Image));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URLImageSmall", SqlDbType.VarChar));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Copyright", SqlDbType.Int, item.Copyright));

                if (!string.IsNullOrEmpty(item.TagList))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_TagList", SqlDbType.NVarChar, -1, item.TagList));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_TagList", SqlDbType.NVarChar));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Status", SqlDbType.Int, (int)item.Status));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_UpdatedBy", SqlDbType.UniqueIdentifier, item.UserID.Value));

                if (item.ObjectType == 0)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_SpezialXml", SqlDbType.Xml));
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.xmlData.OuterXml))
                        GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_SpezialXml", SqlDbType.Xml, item.xmlData.OuterXml));
                    else
                        GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_SpezialXml", SqlDbType.Xml));
                }

                if (!string.IsNullOrEmpty(item.CountryCode))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Country_ISO", SqlDbType.NVarChar, 5, item.CountryCode));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Country_ISO", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Region))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Region", SqlDbType.NVarChar, 25, item.Region));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Region", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Street))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Street", SqlDbType.NVarChar, 128, item.Street));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Street", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.Zip))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Zip", SqlDbType.NVarChar, 8, item.Zip));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Zip", SqlDbType.NVarChar));

                if (!string.IsNullOrEmpty(item.City))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_City", SqlDbType.NVarChar, 50, item.City));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_City", SqlDbType.NVarChar));

                if (item.Geo_Lat != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Lat", SqlDbType.Float, item.Geo_Lat));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Lat", SqlDbType.Float));

                if (item.Geo_Long != double.MinValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Long", SqlDbType.Float, item.Geo_Long));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Geo_Long", SqlDbType.Float));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)item.ShowState));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Featured", SqlDbType.Int, item.Featured));

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_StartDate", SqlDbType.DateTime, item.StartDate));

                DateTime dt = new DateTime(3000, 1, 1);
                if (item.EndDate > dt)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EndDate", SqlDbType.DateTime, dt));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EndDate", SqlDbType.DateTime, item.EndDate));

                if (!string.IsNullOrEmpty(item.UrlXSLT))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URL_XSLT", SqlDbType.NVarChar, 256, item.UrlXSLT));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_URL_XSLT", SqlDbType.NVarChar));

                if (item.ObjectType == Helper.GetObjectType("Page").NumericId && item.HasRoleRightChanged())
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_RoleRight", SqlDbType.Int, Business.DataAccessConfiguration.GetRoleRightValue(item.RoleRight)));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@InheritRoleRight", SqlDbType.Bit, 1));
                }
                else
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_RoleRight", SqlDbType.Int, item.roleRight));
                }

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_FriendVisibility", SqlDbType.Int, (int)item.FriendVisibility));

                Conn.Open();
                GetData.ExecuteNonQuery();
                item.objectState = ObjectState.Saved;
            }
            finally
            {
                Conn.Close();
            }
            TagListUpdate(item, strTagList);

            //if (item.ShowState == ObjectShowState.Published && item.changedToPublished)
            QuickCacheHandler.RemoveCache(item.ObjectType, udc.UserID);
        }

        public static void UpdateCommunityChildObjects(Business.DataObject item)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_UpdateCommunityChilds";
                GetData.Parameters.Add(SqlHelper.AddParameter("@CTY_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Status", SqlDbType.Int, (int)item.Status));

                Conn.Open();
                GetData.ExecuteNonQuery();
                item.objectState = ObjectState.Saved;
            }
            finally
            {
                Conn.Close();
            }
        }

        public static T Load<T>(Guid? objectID, Guid? partnerID, string externalObjectID, ObjectShowState? showState, bool ignoreCache) where T : Business.DataObject, new()
        {

            string cacheKey = null;
            QuickCacheHandler objCache = null;

            if (!ignoreCache && objectID.HasValue)
            {
                cacheKey = string.Format("{0}_{1}", typeof(T), objectID);
                if (showState != null)
                    cacheKey += string.Format("_{0}", (int)showState.Value);

                objCache = new QuickCacheHandler(cacheKey);
                object cacheObj = objCache.Get();
                if (cacheObj != null)
                {
                    T item = (T)cacheObj;
                    item.IsFromCache = true;
                    return item;
                }
            }

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                T item = new T();

                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.Text;

                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("SELECT hitbl_DataObject_OBJ.*{0}\r\n", item.GetSelectSQL(null, GetData.Parameters));
                sb.Append("FROM hitbl_DataObject_OBJ\r\n");
                sb.AppendFormat("{0}\r\n", item.GetJoinSQL(null, GetData.Parameters));
                sb.Append("WHERE ");
                if (objectID.HasValue)
                {
                    sb.Append("hitbl_DataObject_OBJ.OBJ_ID = @OBJ_ID\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID.Value));
                }
                else if (!string.IsNullOrEmpty(externalObjectID) && partnerID.HasValue)
                {
                    sb.Append("OBJ_EXT_ID = @OBJ_EXT_ID AND PAR_ID = @PAR_ID\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_EXT_ID", SqlDbType.VarChar, 512, externalObjectID));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@PAR_ID", SqlDbType.UniqueIdentifier, partnerID.Value));
                }

                if (showState != null)
                {
                    sb.Append("AND OBJ_ShowState = @OBJ_ShowState\r\n");
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ShowState", SqlDbType.Int, (int)showState.Value));
                }
                GetData.CommandText = sb.ToString();

                Conn.Open();
                SqlDataReader sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlReader.Read())
                {
                    item.FillObject(sqlReader);
                    if (!ignoreCache && objectID.HasValue)
                    {
                        if (item.State != ObjectState.Added)
                        {
                            objCache.Insert(item);
                        }
                    }
                    return item;
                }
                else
                {
                    item.State = ObjectState.Added;
                }
                sqlReader.Close();
                return item;
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
        }

        public static int GetObjectType(Guid objectID)
        {
            int objectType = 0;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_GetObjectTypeByObjectID";
                GetData.Parameters.AddWithValue("@OBJ_ID", objectID);

                Conn.Open();
                object ret = GetData.ExecuteScalar();
                Conn.Close();
                if (ret != null)
                    objectType = (int)ret;
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            return objectType;
        }

        public void Delete(Business.DataObject item, UserDataContext udc, bool showStateOnly)
        {
            QuickCacheHandler.RemoveCache("_" + item.ObjectID.Value);

            string strTagList = string.Empty;
            try
            {
                strTagList = GetTaglist(item.ObjectID.Value);
            }
            catch
            {
            }

            SqlConnection sqlConnection = new SqlConnection(strConn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                if (item.ObjectType == Helper.GetObjectType("Community").NumericId || item.ObjectType == Helper.GetObjectType("Page").NumericId || item.ObjectType == Helper.GetObjectType("ProfileCommunity").NumericId)
                {
                    if (showStateOnly)
                        sqlCommand.CommandText = "hisp_DataObject_MarkCommunityAsDeleted";
                    else
                        sqlCommand.CommandText = "hisp_DataObject_DeleteCommunity";
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    if (showStateOnly)
                        sqlCommand.CommandText = "hisp_DataObject_MarkAsDeleted";
                    else
                        sqlCommand.CommandText = "hisp_DataObject_Delete";
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                    sqlCommand.ExecuteNonQuery();
                }

                item.TagList = string.Empty;
                item.objectState = ObjectState.Deleted;
            }
            finally
            {
                sqlConnection.Close();
            }
            TagListUpdate(item, strTagList);
        }

        public void DeleteWithCopies(Business.DataObject item, UserDataContext udc)
        {
            string strTagList = GetTaglist(item.ObjectID.Value);

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_DeleteAllFromOriginal";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));

                Conn.Open();
                GetData.ExecuteNonQuery();
                item.TagList = string.Empty;
                item.objectState = ObjectState.Deleted;
            }
            finally
            {
                Conn.Close();
            }
            TagListUpdate(item, strTagList);
        }


        private static void TagListUpdate(Business.DataObject item, string oldList)
        {
            if (oldList.ToLower().Replace(" ", string.Empty) == item.TagList.ToLower().Replace(" ", string.Empty))
                return;

            Dictionary<string, TagWord> listNew = new Dictionary<string, TagWord>(10);
            Dictionary<string, TagWord> listOld = new Dictionary<string, TagWord>(10);

            string[] strNewList = null;
            strNewList = item.TagList.Split(Constants.TAG_DELIMITER);

            for (int i = 0; i < strNewList.Length; i++)
            {
                if (strNewList[i].Trim().Length > 0)
                {
                    TagWord tgw = new TagWord(strNewList[i]);
                    if (!listNew.ContainsKey(tgw.WordLower))
                    {
                        tgw.Position = i;
                        listNew.Add(tgw.WordLower, tgw);
                    }
                }
            }

            string[] strOldList = oldList.Split(Constants.TAG_DELIMITER);
            for (int i = 0; i < strOldList.Length; i++)
            {
                if (strOldList[i].Trim().Length > 0)
                {
                    TagWord tgw = new TagWord(strOldList[i]);
                    if (!listOld.ContainsKey(tgw.WordLower))
                        listOld.Add(tgw.WordLower, tgw);
                }
            }


            // Process all added tag words
            foreach (string strKey in listNew.Keys)
            {
                TagWord tagWord = listNew[strKey];
                if (!listOld.ContainsKey(strKey)) // Add relation
                {
                    ManageTagListLog(item.ObjectID.Value, item.ObjectType, tagWord, false);
                }
            }

            // Process all deleted tag words
            foreach (string strKey in listOld.Keys)
            {
                if (!listNew.ContainsKey(strKey)) // Delete relation
                {
                    ManageTagListLog(item.ObjectID.Value, item.ObjectType, listOld[strKey], true);
                }
            }
        }

        private static void ManageTagListLog(Guid objectID, int objectType, TagWord tagWord, bool forDelete)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_ManageTaglog";

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, objectType));

                GetData.Parameters.Add(SqlHelper.AddParameter("@TGW_Wordlower", SqlDbType.NVarChar, 100, tagWord.WordLower));

                if (!forDelete)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@TGW_Word", SqlDbType.NVarChar, 100, tagWord.Word));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@TGW_Word", SqlDbType.NVarChar, 100, null));

                GetData.Parameters.Add(SqlHelper.AddParameter("@RecordType", SqlDbType.Int, forDelete));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        private static string GetTaglist(Guid objectID)
        {
            string list = string.Empty;
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_LoadTaglist";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                Conn.Open();
                object tagList = GetData.ExecuteScalar();
                if (tagList != null)
                    list = tagList.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        // Get geo info from cache -> just for user objects
        private static void FillGeoKooridnates(Business.DataObject item)
        {
            if (item.ObjectType == Helper.GetObjectType("User").NumericId && !string.IsNullOrEmpty(item.Zip) && !string.IsNullOrEmpty(item.CountryCode) && (item.Geo_Lat == double.MinValue || item.Geo_Long == double.MinValue))
            {
                _4screen.CSB.GeoTagging.GeoPoint geoPoint = Business.Utils.GetGeoPointFromDB(null, item.Zip, null, item.CountryCode);
                if (geoPoint != null)
                {
                    item.Geo_Lat = geoPoint.Lat;
                    item.Geo_Long = geoPoint.Long;
                    item.Region = geoPoint.Region;
                }
            }
        }

        public bool AddRating(Guid objectID, int objectType, Guid userID, string roles, Business.Rating rating)
        {
            bool blnRet = false;
            Business.RatingConfig objConfig = Business.DataAccessConfiguration.GetRatingConfig(rating.RatingType);
            int intFactor = objConfig.GetRoleMultiplier(roles, GetVirtualUrl(objectID));
            if (intFactor > 0)
            {
                int intTimespan = objConfig.TimeSpanSecond;
                if (intTimespan == 0)
                    intTimespan = 990000000;

                foreach (Business.RatingObject item in rating.RatingObjects)
                {
                    SqlConnection Conn = new SqlConnection(strConn);
                    try
                    {
                        SqlCommand GetData = new SqlCommand();

                        GetData.Connection = Conn;
                        GetData.CommandType = CommandType.StoredProcedure;
                        GetData.CommandText = "hisp_DataObject_AddRating";
                        GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)objectType));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userID));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@RAT_Points", SqlDbType.Int, item.VoteThis * intFactor));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@RAT_Count", SqlDbType.Int, intFactor));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@TimeSpanSecond", SqlDbType.Int, intTimespan));
                        GetData.Parameters.Add(SqlHelper.AddParameter("@RatedConsolidatedFactor", SqlDbType.Decimal, objConfig.GetConsolidatedFactor())); //#1.1.0.0

                        Conn.Open();
                        if (GetData.ExecuteScalar().ToString() == "1")
                            blnRet = true;
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
            return blnRet;
        }

        public bool IsRatingPossible(Guid objectID, Guid userID, string roles, RatingType ratingType)
        {
            bool blnRet = false;
            Business.RatingConfig objConfig = Business.DataAccessConfiguration.GetRatingConfig(ratingType);
            int intFactor = objConfig.GetRoleMultiplier(roles, GetVirtualUrl(objectID));
            if (intFactor > 0)
            {
                int intTimespan = objConfig.TimeSpanSecond;
                if (intTimespan == 0)
                    intTimespan = 990000000;

                SqlConnection Conn = new SqlConnection(strConn);
                try
                {
                    SqlCommand GetData = new SqlCommand();

                    GetData.Connection = Conn;
                    GetData.CommandType = CommandType.StoredProcedure;
                    GetData.CommandText = "hisp_DataObject_AddRatingCheck";
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userID));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@TimeSpanSecond", SqlDbType.Int, intTimespan));

                    Conn.Open();
                    if (GetData.ExecuteScalar().ToString() == "1")
                        blnRet = true;
                }
                finally
                {
                    Conn.Close();
                }
            }
            return blnRet;
        }

        private string GetVirtualUrl(Guid objectID)
        {
            string strKey = string.Concat("virturl", objectID.ToString());
            object objUrl = HttpRuntime.Cache.Get(strKey);
            if (objUrl != null)
                return objUrl.ToString();

            string strUrl = string.Empty;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_GetVirtualUrl";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));

                Conn.Open();
                strUrl = GetData.ExecuteScalar().ToString();
            }
            catch
            {
                strUrl = string.Empty;
            }
            finally
            {
                Conn.Close();
            }
            HttpRuntime.Cache.Insert(strKey, strUrl, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
            return strUrl;
        }

        /// <summary>
        /// Increases the view count of an object and of all attached tag words too
        /// </summary>
        public void AddViewed(Guid objectID, int objectType, UserDataContext userDataContext)
        {
            Business.ViewConfig objConfig = Business.DataAccessConfiguration.GetViewConfig();
            int userTimeSpan = objConfig.UserTimeSpanSecond > 0 ? objConfig.UserTimeSpanSecond : 990000000;
            int ipTimeSpan = objConfig.IPTimeSpanSecond > 0 ? objConfig.IPTimeSpanSecond : 990000000;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_AddView";
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userDataContext.IsAuthenticated ? userDataContext.UserID : userDataContext.AnonymousUserId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)objectType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@IP", SqlDbType.Char, userDataContext.UserIP));
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserTimeSpanSecond", SqlDbType.Int, userTimeSpan));
                GetData.Parameters.Add(SqlHelper.AddParameter("@IPTimeSpanSecond", SqlDbType.Int, ipTimeSpan));
                GetData.Parameters.Add(SqlHelper.AddParameter("@IsAuthenticated", SqlDbType.Bit, userDataContext.IsAuthenticated));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }
        public void AddCommented(Guid objectID)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_AddComment";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }
        public static void AddActionLog(Guid objectID, int objectType, string referenzID, int referenzType, TrackRule action)
        {
            if (objectType == Helper.GetObjectType("User").NumericId && objectID == Constants.ANONYMOUS_USERID.ToGuid())
                return;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_ActionLog_Add";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)objectType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@REF_ID", SqlDbType.UniqueIdentifier, new Guid(referenzID)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@REF_Type", SqlDbType.Int, (int)referenzType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@ALO_Type", SqlDbType.Int, (int)action));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        public bool Exists(Guid objectID)
        {
            bool blnExists = false;

            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_Exists";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectID));

                Conn.Open();
                if (GetData.ExecuteScalar().ToString() == "1")
                    blnExists = true;
            }
            finally
            {
                Conn.Close();
            }
            return blnExists;
        }

        public static Guid? GetTagID(string tagWord)
        {
            object tagObject = HttpRuntime.Cache[string.Format("tagid_{0}", tagWord.ToLower())];
            if (tagObject != null)
            {
                return tagObject.ToString().ToGuid();
            }
            else
            {
                Guid? tagId = DataObjectsHelper.GetTagGuid(tagWord);
                if (tagId.HasValue)
                {
                    HttpRuntime.Cache.Insert(string.Format("tagid_{0}", tagWord.ToLower()), tagId);
                    return tagId;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }



        public static ObjectAccessRight GetUserAccess(UserDataContext udc, Guid? objectId, Guid? communityId, int objectType)
        {
            ObjectAccessRight objectAccessRight = ObjectAccessRight.None;

            SqlConnection sqlConnection = new SqlConnection(strConn);
            try
            {
                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "hisp_DataObject_GetUserAccess";
                if (objectId.HasValue)
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectId", SqlDbType.UniqueIdentifier, objectId.Value));
                if (communityId.HasValue)
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@TargetCommunityId", SqlDbType.UniqueIdentifier, communityId.Value));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CurrentUserId", SqlDbType.UniqueIdentifier, udc.UserID));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectType", SqlDbType.Int, objectType));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Anonymous", SqlDbType.Bit, !udc.IsAuthenticated));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectAccessRightInsert", SqlDbType.Bit));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectAccessRightUpdate", SqlDbType.Bit));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectAccessRightDelete", SqlDbType.Bit));
                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectAccessRightSelect", SqlDbType.Bit));
                sqlCommand.Parameters["@ObjectAccessRightInsert"].Direction = ParameterDirection.Output;
                sqlCommand.Parameters["@ObjectAccessRightUpdate"].Direction = ParameterDirection.Output;
                sqlCommand.Parameters["@ObjectAccessRightDelete"].Direction = ParameterDirection.Output;
                sqlCommand.Parameters["@ObjectAccessRightSelect"].Direction = ParameterDirection.Output;
                sqlConnection.Open();
                sqlCommand.ExecuteScalar();
                if ((bool)sqlCommand.Parameters["@ObjectAccessRightInsert"].Value)
                    objectAccessRight |= ObjectAccessRight.Insert;
                if ((bool)sqlCommand.Parameters["@ObjectAccessRightUpdate"].Value)
                    objectAccessRight |= ObjectAccessRight.Update;
                if ((bool)sqlCommand.Parameters["@ObjectAccessRightDelete"].Value)
                    objectAccessRight |= ObjectAccessRight.Delete;
                if ((bool)sqlCommand.Parameters["@ObjectAccessRightSelect"].Value)
                    objectAccessRight |= ObjectAccessRight.Select;
            }
            finally
            {
                sqlConnection.Close();
            }

            return objectAccessRight;
        }

        internal static int GetNumCopies(Guid ObjectID)
        {
            string strConn = Helper.GetSiemeConnectionString();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_GetNumCopies";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, ObjectID));
                Conn.Open();
                return Convert.ToInt32(GetData.ExecuteScalar());
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static bool IsUserCommunityOwner(Guid communityId, Guid userID)
        {
            string strConn = Helper.GetSiemeConnectionString();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Community_IsUserOwner";
                GetData.Parameters.Add(SqlHelper.AddParameter("@CommunityId", SqlDbType.UniqueIdentifier, communityId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@UserId", SqlDbType.UniqueIdentifier, userID));
                Conn.Open();
                return Convert.ToBoolean(GetData.ExecuteScalar());
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static bool IsUserObjectOwner(Guid objectId, Guid userID)
        {
            string strConn = Helper.GetSiemeConnectionString();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_IsUserOwner";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userID));
                Conn.Open();
                return Convert.ToBoolean(GetData.ExecuteScalar());
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static void ManageFavorite(Guid objectId, int? objectType, Guid userId, bool add)
        {
            string strConn = Helper.GetSiemeConnectionString();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_ManageFavorite";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userId));
                if (add)
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, objectType));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Add", SqlDbType.Bit, 1));
                }
                else
                {
                    GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, 0));
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Add", SqlDbType.Bit, 0));
                }
                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal static bool IsObjectFavaorite(Guid objectId, Guid userID)
        {
            string strConn = Helper.GetSiemeConnectionString();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_IsObjectFavorite";
                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_ID", SqlDbType.UniqueIdentifier, objectId));
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, userID));
                Conn.Open();
                return Convert.ToBoolean(GetData.ExecuteScalar());
            }
            finally
            {
                Conn.Close();
            }
        }

        #region Object To Object Relations

        internal void RelInsert(Business.RelationParams relParams, int? sortOrder)
        {
            Data.DataObject objData = new Data.DataObject();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_Rel_Insert";
                GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ParentObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_Type", SqlDbType.Int, (int)relParams.ParentObjectType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ChildObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_Type", SqlDbType.Int, (int)relParams.ChildObjectType));
                GetData.Parameters.Add(SqlHelper.AddParameter("@SortOrder", SqlDbType.Int, sortOrder.HasValue ? (int)sortOrder.Value : -1));
                if (!string.IsNullOrEmpty(relParams.RelationType))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar, 50, relParams.RelationType));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal void RelUpdate(Business.RelationParams relParams, int? sortOrder)
        {
            Data.DataObject objData = new Data.DataObject();
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DataObject_Rel_Update";
                GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ParentObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ChildObjectID.Value));
                GetData.Parameters.Add(SqlHelper.AddParameter("@SortOrder", SqlDbType.Int, sortOrder.HasValue ? (int)sortOrder.Value : -1));
                if (!string.IsNullOrEmpty(relParams.RelationType))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar, 50, relParams.RelationType));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        internal void RelDelete(Business.RelationParams relParams, bool deep)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                if (!deep)
                    GetData.CommandText = "hisp_DataObject_Rel_Delete";
                else
                    GetData.CommandText = "hisp_DataObject_Rel_Delete_Deep";

                if (relParams.ParentObjectID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ParentObjectID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_ID", SqlDbType.UniqueIdentifier));

                if (relParams.ParentObjectType.HasValue && relParams.ParentObjectType.Value != 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_Type", SqlDbType.Int, (int)relParams.ParentObjectType.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Parent_OBJ_Type", SqlDbType.Int));

                if (relParams.ChildObjectID.HasValue)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_ID", SqlDbType.UniqueIdentifier, relParams.ChildObjectID.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_ID", SqlDbType.UniqueIdentifier));

                if (relParams.ChildObjectType.HasValue && relParams.ChildObjectType.Value != 0)
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_Type", SqlDbType.Int, (int)relParams.ChildObjectType.Value));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@Child_OBJ_Type", SqlDbType.Int));

                if (!string.IsNullOrEmpty(relParams.RelationType))
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar, 50, relParams.RelationType));
                else
                    GetData.Parameters.Add(SqlHelper.AddParameter("@RelType", SqlDbType.NVarChar));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion
    }

    internal class TagWord
    {
        public string Word = string.Empty;
        public string WordLower = string.Empty;
        public string TagWordID { get; set; }
        public int Position { get; set; }

        internal TagWord(string word)
        {
            Word = word.Trim();
            WordLower = Word.ToLower();
        }
    }
}