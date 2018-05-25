//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.1.0.0    27.12.2007 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.ServiceModel.Security;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using _4screen.CSB.Common;
using _4screen.CSB.Extensions.Business;
using _4screen.CSB.Notification.Business;
using _4screen.CSB.Common.Notification;
using System.Reflection;

namespace _4screen.CSB.DataAccess.Business
{
    public class DataObject
    {
        internal UserDataContext userDataContext;
        public ObjectState objectState;
        internal Guid? objectID;
        internal string externalObjectID;
        internal Guid? partnerID;
        internal Guid? userID;
        internal string nickname;
        internal Guid? instanceID;
        internal Guid? communityID;
        public int objectType;
        internal string langCode;
        internal string title;
        internal string description;
        internal string descriptionLinked;
        internal string descriptionMobile;
        internal string image;
        internal string tagList;
        internal DateTime dateInserted;
        internal DateTime dateUpdated;
        internal string insertedBy;
        internal string updatedBy;
        internal bool statusChanged;
        internal ObjectStatus objectStatus;
        internal ObjectShowState objectShowState;
        internal bool changedToPublished;
        internal int featured;
        internal int agility;
        internal DateTime startDate;
        internal DateTime endDate;
        internal string urlXSLT;
        internal string region;
        internal string street;
        internal string zip;
        internal string city;
        internal string countryCode;
        internal double geoLat;
        internal double geoLong;
        internal int copyright;
        internal int viewCount;
        internal int ratedCount;
        internal int ratedAmount;
        internal decimal ratedAverage;
        internal decimal ratedConsolidated;
        internal int commentedCount;
        internal int incentivePoints;
        internal int memberCount;
        internal int favoriteCount;
        internal XmlDocument xmlData;
        internal Guid? originalObjectID;
        internal int numCopies;
        internal Guid? groupID;
        internal Guid? parentObjectID;
        internal Dictionary<string, bool> roleRights;
        internal int roleRight;
        internal int roleRightSave;
        internal Dictionary<PictureVersion, string> pictureFormat = new Dictionary<PictureVersion, string>();
        internal double? selectCount;

        #region Properties

        public GroupByInfo GroupByInfo { get; internal set; }

        public UserDataContext UserDataContext
        {
            get { return userDataContext; }
            set { userDataContext = value; }
        }

        public Guid? ParentObjectID
        {
            get { return parentObjectID; }
            set { parentObjectID = value; }
        }

        public Dictionary<string, bool> RoleRight
        {
            get
            {
                if (roleRights == null)
                {
                    roleRightSave = roleRight;
                    roleRights = DataAccessConfiguration.LoadRoleRight(roleRight);
                }
                return roleRights;
            }
        }

        internal bool HasRoleRightChanged()
        {
            if (roleRights == null)
                return false;
            else
                return (roleRightSave != DataAccessConfiguration.GetRoleRightValue(roleRights));
        }

        public Guid? GroupID
        {
            get { return groupID; }
            set
            {
                if (GroupID != value)
                    objectState = ObjectState.Changed;
                groupID = value;
            }
        }

        protected XmlDocument XmlData
        {
            get { return xmlData; }
        }

        public string GetXMLValue(string propertyName)
        {
            return XmlHelper.GetElementValue(XmlData.DocumentElement, propertyName, string.Empty);
        }

        public void SetXMLValue(string propertyName, string value)
        {
            XmlHelper.SetElementInnerText(XmlData.DocumentElement, propertyName, value);
        }

        public override string ToString()
        {
            return string.Format("{0} [ObjectType {1}]", title, objectType);
        }

        public int ObjectType
        {
            get { return objectType; }
            internal set { objectType = value; }
        }

        public ObjectState State
        {
            get
            {
                if (roleRights == null)
                    return objectState;
                else
                {
                    if (HasRoleRightChanged())
                        return ObjectState.Changed;
                    else
                        return objectState;
                }
            }
            internal set { objectState = value; }
        }

        public ObjectShowState ShowState
        {
            get { return objectShowState; }
            set
            {
                if (objectShowState != value)
                {
                    if (value == ObjectShowState.Published)
                        changedToPublished = true;
                    objectState = ObjectState.Changed;
                }
                objectShowState = value;
            }
        }

        public Guid? ObjectID
        {
            get { return objectID; }
            set
            {
                if (objectID != value)
                    objectState = ObjectState.Changed;
                objectID = value;
            }
        }

        public Guid? InstanceID
        {
            get { return instanceID; }
            set
            {
                if (instanceID != value)
                    objectState = ObjectState.Changed;
                instanceID = value;
            }
        }

        public string ExternalObjectID
        {
            get { return externalObjectID; }
            set
            {
                if (externalObjectID != value)
                    objectState = ObjectState.Changed;
                externalObjectID = value;
            }
        }

        public Guid? PartnerID
        {
            get { return partnerID; }
            set
            {
                if (partnerID != value)
                    objectState = ObjectState.Changed;
                partnerID = value;
            }
        }

        public Guid? UserID
        {
            get { return userID; }
            set
            {
                if (userID != value)
                {
                    objectState = ObjectState.Changed;
                    userID = value;
                }
            }
        }

        public string Nickname
        {
            get { return nickname; }
            set
            {
                if (nickname != value)
                    objectState = ObjectState.Changed;
                nickname = value;
            }
        }

        public Guid? CommunityID
        {
            get { return communityID; }
            set
            {
                if (communityID != value)
                    objectState = ObjectState.Changed;
                communityID = value;
            }
        }

        public string LangCode
        {
            get { return langCode; }
            set
            {
                if (langCode != value)
                    objectState = ObjectState.Changed;
                langCode = value;
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                    objectState = ObjectState.Changed;
                title = value;
                title = title.StripHTMLTags();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                    objectState = ObjectState.Changed;
                description = value;
            }
        }

        public string DescriptionLinked
        {
            get
            {
                if (string.IsNullOrEmpty(descriptionLinked))
                    return description;
                else
                    return descriptionLinked;
            }
            internal set
            {
                if (descriptionLinked != value)
                    objectState = ObjectState.Changed;
                descriptionLinked = value;
            }
        }

        public string DescriptionMobile
        {
            get { return descriptionMobile; }
            set
            {
                if (descriptionMobile != value)
                    objectState = ObjectState.Changed;
                descriptionMobile = value;
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                if (image != value)
                    objectState = ObjectState.Changed;
                image = value;
            }
        }

        public int Copyright
        {
            get { return copyright; }
            set
            {
                if (copyright != value)
                    objectState = ObjectState.Changed;
                copyright = value;
            }
        }

        public string TagList
        {
            get { return tagList; }
            set
            {
                if (tagList != value.Trim())
                    objectState = ObjectState.Changed;
                else
                    return;

                tagList = value.Replace(',', Constants.TAG_DELIMITER).Replace('¦', Constants.TAG_DELIMITER).Replace('|', Constants.TAG_DELIMITER).Replace(';', Constants.TAG_DELIMITER).RemoveDuplicates(Constants.TAG_DELIMITER).StripHTMLTags();
            }
        }

        public ObjectStatus Status
        {
            get { return objectStatus; }
            set
            {
                if (objectStatus != value)
                {
                    objectState = ObjectState.Changed;
                    if (ObjectType == Helper.GetObjectType("Community").NumericId)
                    {
                        statusChanged = true;
                    }
                }

                objectStatus = value;
            }
        }

        public DateTime Inserted
        {
            get { return dateInserted; }
            internal set { dateInserted = value; }
        }

        public DateTime Updated
        {
            get { return dateUpdated; }
            internal set { dateUpdated = value; }
        }

        public string InsertedBy
        {
            get { return insertedBy; }
        }

        public string UpdatedBy
        {
            get { return updatedBy; }
        }

        public string Region
        {
            get { return region; }
            set
            {
                if (region != value)
                    objectState = ObjectState.Changed;
                region = value;
                region = region.StripHTMLTags();
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                if (street != value)
                    objectState = ObjectState.Changed;
                street = value;
                street = street.StripHTMLTags();
            }
        }

        public string Zip
        {
            get { return zip; }
            set
            {
                if (zip != value)
                    objectState = ObjectState.Changed;
                zip = value;
                zip = zip.StripHTMLTags();
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                    objectState = ObjectState.Changed;
                city = value;
                city = city.StripHTMLTags();
            }
        }

        public string CountryCode
        {
            get { return countryCode; }
            set
            {
                if (countryCode != value)
                    objectState = ObjectState.Changed;
                countryCode = value;
                countryCode = countryCode.StripHTMLTags();
            }
        }

        public double Geo_Lat
        {
            get { return geoLat; }
            set
            {
                if (geoLat != value)
                    objectState = ObjectState.Changed;
                geoLat = value;
            }
        }

        public double Geo_Long
        {
            get { return geoLong; }
            set
            {
                if (geoLong != value)
                    objectState = ObjectState.Changed;
                geoLong = value;
            }
        }

        public int ViewCount
        {
            get { return viewCount; }
            internal set { viewCount = value; }
        }

        public int RatedCount
        {
            get { return ratedCount; }
            internal set { ratedCount = value; }
        }

        public int RatedAmount
        {
            get { return ratedAmount; }
        }

        public decimal RatedAverage
        {
            get
            {
                int intRatedAverage = Convert.ToInt32(Math.Truncate(ratedAverage));
                decimal decTemp = Math.Round(ratedAverage, 1) - intRatedAverage;

                if (decTemp > (decimal)0.7)
                    return Convert.ToDecimal(intRatedAverage + 1);
                else if (decTemp < (decimal)0.3)
                    return Convert.ToDecimal(intRatedAverage);
                else
                    return Convert.ToDecimal(intRatedAverage + 0.5);
            }
        }

        public decimal RatedConsolidated
        {
            get
            {
                return ratedConsolidated;
            }
        }

        public int CommentedCount
        {
            get { return commentedCount; }
            internal set { commentedCount = value; }
        }

        public int IncentivePoints
        {
            get { return incentivePoints; }
        }

        public int MemberCount
        {
            get { return memberCount; }
        }

        public int FavoriteCount
        {
            get { return favoriteCount; }
        }

        public Guid? OriginalObjectID
        {
            get { return originalObjectID; }
            set
            {
                if (originalObjectID != value)
                    objectState = ObjectState.Changed;
                originalObjectID = value;
            }
        }

        public int NumCopies
        {
            get
            {
                numCopies = Data.DataObject.GetNumCopies(ObjectID.Value);
                return numCopies;
            }
        }

        public int Featured
        {
            get { return featured; }
            set
            {
                if (Featured != value)
                    objectState = ObjectState.Changed;
                featured = value;
            }
        }

        public int Agility
        {
            get { return agility; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                    objectState = ObjectState.Changed;
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                    objectState = ObjectState.Changed;
                endDate = value;
            }
        }

        public string UrlXSLT
        {
            get { return urlXSLT; }
            set
            {
                if (urlXSLT != value)
                    objectState = ObjectState.Changed;
                urlXSLT = value;
                urlXSLT = urlXSLT.StripHTMLTags();
            }
        }

        public int ParentObjectType
        {
            get { return XmlHelper.GetElementValue(XmlData.DocumentElement, "ParentObjectType", 0); }
            set
            {
                if (ParentObjectType != value)
                {
                    objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(XmlData.DocumentElement, "ParentObjectType", value);
            }
        }

        public Dictionary<PictureVersion, string> PictureFormats
        {
            get { return pictureFormat; }
        }

        public bool IsFromCache { get; set; }

        public double? SelectCount
        {
            get { return selectCount; }
        }

        public SerializationType SerializationType { get; set; }
        #endregion

        public DataObject()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObject(UserDataContext userDataContext)
        {
            this.userDataContext = userDataContext;
            Init();
            if (userDataContext != null)
            {
                userID = userDataContext.UserID;
                nickname = userDataContext.Nickname;
            }
        }

        private void Init()
        {
            objectState = ObjectState.Added;
            objectID = null;
            externalObjectID = string.Empty;
            partnerID = null;
            userID = null;
            nickname = string.Empty;
            instanceID = null;
            communityID = null;
            objectType = 0;
            langCode = string.Empty;
            title = string.Empty;
            description = string.Empty;
            descriptionLinked = string.Empty;
            descriptionMobile = string.Empty;
            image = string.Empty;
            tagList = string.Empty;
            dateInserted = DateTime.MinValue;
            dateUpdated = DateTime.MinValue;
            insertedBy = string.Empty;
            updatedBy = string.Empty;
            statusChanged = false;
            objectStatus = ObjectStatus.Private;
            objectShowState = ObjectShowState.Published;
            changedToPublished = false;
            featured = 0;
            startDate = DateTime.Now;
            endDate = DateTime.MaxValue;
            urlXSLT = string.Empty;
            region = string.Empty;
            street = string.Empty;
            zip = string.Empty;
            city = string.Empty;
            countryCode = string.Empty;
            geoLat = double.MinValue;
            geoLong = double.MinValue;
            xmlData = new XmlDocument();
            XmlHelper.CreateRoot(xmlData, "Root");
            originalObjectID = null;
            groupID = null;
            parentObjectID = null;
            SerializationType = SerializationType.Transfer;
        }

        protected virtual void Validate(AccessMode accessMode)
        {
            if (accessMode == AccessMode.Update)
            {
                if (!ObjectID.HasValue)
                    throw new SiemeArgumentException("DataObject", "Validate", "ObjectId", "ObjectId is missing");
            }
            if (accessMode == AccessMode.Insert || accessMode == AccessMode.Update)
            {
                if (!UserID.HasValue)
                    throw new SiemeArgumentException("DataObject", "Validate", "UserID", "UserId is missing");
                if (!CommunityID.HasValue)
                    throw new SiemeArgumentException("DataObject", "Validate", "CommunityId", "CommunityId is missing");
                if (string.IsNullOrEmpty(Title))
                    throw new SiemeArgumentException("DataObject", "Validate", "Title", "Title is missing");
                if (EndDate < StartDate)
                    throw new SiemeArgumentException("DataObject", "Validate", "EndDate", "End date is smaller than start date");
            }
        }

        #region Read / Write Methods
        public virtual void FillObject(SqlDataReader sqlReader)
        {
            Data.DataObject.FillObject(this, sqlReader);
        }

        public virtual string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObject.GetSelectSQL(qParas, parameters);
        }

        public virtual string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObject.GetInsertSQL(this, parameters);
        }

        public virtual string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObject.GetUpdateSQL(this, parameters);
        }

        public virtual string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObject.GetJoinSQL(qParas, parameters);
        }

        public virtual string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObject.GetWhereSQL(qParas, parameters);
        }

        public virtual string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObject.GetFullTextWhereSQL(qParas, parameters);
        }

        public virtual string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObject.GetOrderBySQL(qParas, parameters);
        }

        public virtual void Insert(Guid pObjectID, int parentObjectType)
        {
            Insert(pObjectID, parentObjectType, null);
        }

        public virtual void Insert(Guid pObjectID, int parentObjectType, string relationType)
        {
            Insert();
            if (State == ObjectState.Saved)
            {
                Data.DataObject objData = new Data.DataObject();
                objData.RelInsert(new RelationParams { ParentObjectID = pObjectID, ParentObjectType = parentObjectType, ChildObjectID = ObjectID, ChildObjectType = ObjectType, Udc = UserDataContext.GetUserDataContext(), RelationType = relationType }, null);
            }
        }

        public virtual void Insert()
        {
            if ((GetUserAccess() & ObjectAccessRight.Insert) != ObjectAccessRight.Insert)
            {
                throw new SiemeSecurityException("DataObject", "Insert", AccessMode.Insert, "Access rights missing");
            }

            _4screen.CSB.DataAccess.Business.FilterEngine.FilterObject(this);

            Validate(AccessMode.Insert);
            Data.DataObject.Insert(this);
            TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.Inserted, string.Empty);
            IncentivePointsManager.AddIncentivePointEvent(string.Format("{0}_UPLOAD", Helper.GetObjectType(ObjectType).Id.ToUpper()), this.UserDataContext, this.ObjectID.Value.ToString());
            if (ObjectType != Helper.GetObjectType("ForumTopicItem").NumericId)
                Event.ReportEvent(EventIdentifier.NewObject, userDataContext.UserID, ObjectID);
        }

        public void UpdateBackground()
        {
            // No access check -> never call from web ui

            // TODO: Set state in field change
            //if (State == ObjectState.Changed)
            //{
            if (!ObjectID.HasValue)
                throw new SiemeArgumentException("DataObject", "UpdateBackground", "ObjectId", "ObjectId is missing");

            if (HttpContext.Current != null)
            {
                string cacheKey = string.Format("{0}_{1}", this.GetType(), objectID);
                if (HttpContext.Current.Items[cacheKey] != null)
                    HttpContext.Current.Items.Remove(cacheKey);
            }

            Validate(AccessMode.Update);
            Data.DataObject.Update(this, userDataContext);
            if (statusChanged)
            {
                Data.DataObject.UpdateCommunityChildObjects(this);
            }
            //}
        }

        public virtual void Update()
        {
            if ((GetUserAccess() & ObjectAccessRight.Update) != ObjectAccessRight.Update)
            {
                throw new SiemeSecurityException("DataObject", "Update", AccessMode.Update, "Access rights missing");
            }

            _4screen.CSB.DataAccess.Business.FilterEngine.FilterObject(this);

            // TODO: Set state in field change
            //if (State == ObjectState.Changed)
            //{
            if (HttpContext.Current != null)
            {
                string cacheKey = string.Format("{0}_{1}", this.GetType(), objectID);
                if (HttpContext.Current.Items[cacheKey] != null)
                    HttpContext.Current.Items.Remove(cacheKey);
            }

            Validate(AccessMode.Update);
            Data.DataObject.Update(this, userDataContext);
            if (statusChanged)
            {
                Data.DataObject.UpdateCommunityChildObjects(this);
            }
            Event.ReportEvent(EventIdentifier.ChangeObject, userDataContext.UserID, ObjectID);
            TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.Updated, string.Empty);
            //}
        }

        public void MarkAsDeleted()
        {
            Delete(true, false);
        }

        public void Delete()
        {
            Delete(false, false);
        }

        public void Delete(bool deep)
        {
            Delete(false, deep);
        }

        public void Delete(bool showStateOnly, bool deep)
        {
            if ((GetUserAccess() & ObjectAccessRight.Delete) != ObjectAccessRight.Delete)
                throw new SiemeSecurityException("DataObject", "Delete", AccessMode.Delete, "Access rights missing");

            if (!ObjectID.HasValue)
                throw new SiemeArgumentException("DataObject", "Delete", "ObjectId", "ObjectId is missing");

            if (deep)
                DeleteChildren(ObjectID.Value);

            if (HttpContext.Current != null && HttpContext.Current.Items[ObjectID] != null)
                HttpContext.Current.Items.Remove(ObjectID);
            Data.DataObject objData = new Data.DataObject();
            objData.Delete(this, userDataContext, showStateOnly);
        }

        private void DeleteChildren(Guid parentId)
        {
            DataObjectList<DataObject> children = DataObjects.Load<DataObject>(new QuickParameters()
            {
                Udc = UserDataContext,
                RelationParams = new RelationParams() { ParentObjectID = parentId },
                IgnoreCache = true,
                DisablePaging = true
            });
            foreach (var child in children)
            {
                DeleteChildren(child.ObjectID.Value);
                child.Delete();
            }
        }

        public static T Load<T>(Guid? objectID) where T : Business.DataObject, new()
        {
            return Load<T>(null, objectID, null, null, ObjectShowState.Published, false);
        }

        public static T Load<T>(UserDataContext udc, Guid? objectID, ObjectShowState? showState, bool ignoreCache) where T : Business.DataObject, new()
        {
            return Load<T>(udc, objectID, null, null, showState, ignoreCache);
        }

        public static T Load<T>(Guid? objectID, ObjectShowState? showState, bool ignoreCache) where T : Business.DataObject, new()
        {
            return Load<T>(null, objectID, null, null, showState, ignoreCache);
        }

        public static T Load<T>(UserDataContext udc, Guid partnerID, string externalObjectID) where T : Business.DataObject, new()
        {
            return Load<T>(udc, null, partnerID, externalObjectID, null, true);
        }

        public static DataObject LoadByReflection(Guid objectID)
        {
            return LoadByReflection(objectID, GetObjectType(objectID));
        }

        public static DataObject LoadByReflection(Guid objectID, int objectType)
        {
            string typeName = Helper.GetObjectType(objectType).Type;

            MethodInfo loadMethod = HttpRuntime.Cache["DataObject.Load" + typeName] as MethodInfo;
            if (loadMethod == null)
            {
                Type type = null;

                if (!string.IsNullOrEmpty(Helper.GetObjectType(objectType).Assembly))
                {
                    Assembly assembly = Assembly.Load(Helper.GetObjectType(objectType).Assembly);
                    type = assembly.GetType(typeName);
                }
                else
                {
                    type = Type.GetType(typeName);
                }

                loadMethod = typeof(DataObject).GetMethod("Load", new Type[] { typeof(Guid?) });
                loadMethod = loadMethod.MakeGenericMethod(type);
                HttpRuntime.Cache.Insert("DataObject.Load" + typeName, loadMethod, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return (DataObject)loadMethod.Invoke(null, new object[] { objectID });
        }

        private static T Load<T>(UserDataContext udc, Guid? objectID, Guid? partnerID, string externalObjectID, ObjectShowState? showState, bool ignoreCache) where T : Business.DataObject, new()
        {
            DataObject dataObject;

            if (objectID.HasValue)
            {
                if (HttpContext.Current != null)
                {
                    string cacheKey = string.Format("{0}_{1}", typeof(T), objectID);
                    if (!ignoreCache && HttpContext.Current.Items[cacheKey] != null)
                        dataObject = (T)HttpContext.Current.Items[cacheKey];
                    else
                        dataObject = LoadFormDB<T>(udc, objectID, partnerID, externalObjectID, showState, ignoreCache);
                }
                else
                {
                    dataObject = Data.DataObject.Load<T>(udc, objectID, partnerID, externalObjectID, showState, ignoreCache);
                    //TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.LoadedFromDB, string.Empty, objUserDataContext);
                }
            }
            else if (!string.IsNullOrEmpty(externalObjectID) && partnerID.HasValue)
            {
                dataObject = LoadFormDB<T>(udc, objectID, partnerID, externalObjectID, showState, ignoreCache);
            }
            else
            {
                dataObject = new T();
            }

            if (dataObject.State != ObjectState.Added && (dataObject.GetUserAccess() & ObjectAccessRight.Select) != ObjectAccessRight.Select)
            {
                throw new SiemeSecurityException("DataObject", "Load", AccessMode.Load, "Access rights missing");
            }

            return (T)dataObject;
        }

        private static T LoadFormDB<T>(UserDataContext udc, Guid? objectID, Guid? partnerID, string externalObjectID, ObjectShowState? showState, bool ignoreCache) where T : Business.DataObject, new()
        {
            if (!objectID.HasValue && (string.IsNullOrEmpty(externalObjectID) || !partnerID.HasValue))
                throw new SiemeArgumentException("DataObject", "LoadFormDB", "ObjectId,ExternalID", "ObjectID or ExternalID are missing");

            T item = Data.DataObject.Load<T>(udc, objectID, partnerID, externalObjectID, showState, ignoreCache);
            if (HttpContext.Current != null && objectID.HasValue && item.State != ObjectState.Added)
            {
                string cacheKey = string.Format("{0}_{1}", typeof(T), objectID);
                HttpContext.Current.Items[cacheKey] = item;
            }
            if (item.IsFromCache)
            {
                //TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.LoadedFromCache, string.Empty, objUserDataContext);
            }
            else
            {
                //TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.LoadedFromDB, string.Empty, objUserDataContext);
            }
            return item;

        }

        public static int GetObjectType(Guid objectID)
        {
            return Data.DataObject.GetObjectType(objectID);
        }
        #endregion


        // TODO: Check if this still works
        public DataObject CopyToCommunity(Guid? targetCommunityID, ObjectStatus enuStatus, string strTagList)
        {
            DataObject doNewObject = new DataObject(userDataContext);

            doNewObject.Status = enuStatus;
            doNewObject.TagList = strTagList;
            doNewObject.CommunityID = targetCommunityID;
            if (OriginalObjectID.HasValue)
                doNewObject.OriginalObjectID = OriginalObjectID;
            else
                doNewObject.OriginalObjectID = ObjectID;

            doNewObject.ObjectType = ObjectType;
            doNewObject.Copyright = Copyright;
            doNewObject.Description = Description;
            doNewObject.DescriptionLinked = DescriptionLinked;
            doNewObject.DescriptionMobile = DescriptionMobile;
            doNewObject.LangCode = LangCode;
            doNewObject.Title = Title;
            doNewObject.Image = Image;
            doNewObject.UserID = userDataContext.UserID;
            doNewObject.Nickname = userDataContext.Nickname;
            doNewObject.xmlData = xmlData;
            doNewObject.CountryCode = CountryCode;
            doNewObject.Region = Region;
            doNewObject.Zip = Zip;
            doNewObject.City = City;
            doNewObject.Geo_Lat = Geo_Lat;
            doNewObject.Geo_Long = Geo_Long;
            doNewObject.ShowState = ShowState;
            return doNewObject;
        }

        // TODO: Check if this still works
        public DataObject CopyToCommunity(Guid targetCommunityID)
        {
            DataObjectCommunity doTargetCommmunity = DataObject.Load<DataObjectCommunity>(targetCommunityID);
            return CopyToCommunity(targetCommunityID, doTargetCommmunity.Status, doTargetCommmunity.TagList);
        }

        // TODO: Check if this still works
        public void DeleteWithCopies()
        {
            if ((GetUserAccess() & ObjectAccessRight.Delete) != ObjectAccessRight.Delete)
            {
                throw new SiemeSecurityException("DataObject", "DeleteWithCopies", AccessMode.Delete, "Access rights missing");
            }

            if (!ObjectID.HasValue)
                throw new SiemeArgumentException("DataObject", "DeleteWithCopies", "ObjectId", "ObjectID is missing");

            if (HttpContext.Current != null && HttpContext.Current.Items[ObjectID] != null)
                HttpContext.Current.Items.Remove(ObjectID);

            Data.DataObject objData = new Data.DataObject();
            objData.DeleteWithCopies(this, userDataContext);
        }

        public void SetImageType(PictureVersion version, PictureFormat format)
        {
            objectState = ObjectState.Changed;
            XmlHelper.SetElementInnerText(XmlData.DocumentElement, "Picture" + version, format.ToString());
        }

        public string GetImage(PictureVersion version)
        {
            return GetImage(version, true);
        }

        public string GetImage(PictureVersion version, bool returnDefault)
        {
            string format = XmlHelper.GetElementValue(XmlData.DocumentElement, "Picture" + version, string.Empty);
            if (!string.IsNullOrEmpty(format) && !string.IsNullOrEmpty(image))
                return string.Format("/{0}/P/{1}/{2}.{3}", UserID, version.ToString(), image, format.ToLower());
            else if (!string.IsNullOrEmpty(image))
                return image;
            else if (returnDefault)
                return Helper.GetDefaultURLImageSmall(version, ObjectType);
            else
                return null;
        }

        public void AddViewed()
        {
            if (!ObjectID.HasValue)
                throw new SiemeArgumentException("DataObject", "AddViewed", "ObjectId", "ObjectID is missing");

            if (ObjectType == 0)
                throw new SiemeArgumentException("DataObject", "AddViewed", "ObjectType", "ObjectType is missing");

            Data.DataObject objData = new Data.DataObject();
            objData.AddViewed(ObjectID.Value, ObjectType, userDataContext);
            TrackingManager.TrackObjectEvent(ObjectType, ObjectID, TrackRule.Viewed, string.Empty);
        }

        public ObjectAccessRight GetUserAccess()
        {
            ObjectAccessRight retval = ObjectAccessRight.None;
            if (userDataContext == null)
                userDataContext = UserDataContext.GetUserDataContext();
            if (userDataContext.UserID != userDataContext.AnonymousUserId)
            {
                if (userDataContext.IsAdmin)
                {
                    retval = ObjectAccessRight.Insert | ObjectAccessRight.Update | ObjectAccessRight.Delete | ObjectAccessRight.Select;
                }
                else
                {
                    if (SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectInsert"))
                        retval |= ObjectAccessRight.Insert;
                    if (SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectUpdate"))
                        retval |= ObjectAccessRight.Update;
                    if (SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectDelete"))
                        retval |= ObjectAccessRight.Delete;
                    if (SecuritySection.CachedInstance.CurrentUserHasAccess("DataObjectSelect"))
                        retval |= ObjectAccessRight.Select;

                    Data.DataObject objData = new Data.DataObject();
                    retval |= objData.GetUserAccess(this);
                }
                return retval;
            }
            else
            {
                return ObjectAccessRight.None;
            }
        }

        public static void AddViewed(UserDataContext userDataContext, Guid? objectID, int objectType)
        {
            if (!objectID.HasValue)
                throw new SiemeArgumentException("DataObject", "AddViewed", "ObjectID", "ObjectID is missing");

            Data.DataObject objData = new Data.DataObject();
            objData.AddViewed(objectID.Value, objectType, userDataContext);
            TrackingManager.TrackObjectEvent(objectType, objectID, TrackRule.Viewed, string.Empty);
        }

        public static bool AddRating(UserDataContext userDataContext, Guid? objectID, int objectType, Business.Rating rating)
        {
            if (!objectID.HasValue)
                throw new SiemeArgumentException("DataObject", "AddRating", "ObjectID", "ObjectID is missing");

            Data.DataObject objData = new Data.DataObject();
            if (objData.AddRating(objectID.Value, objectType, userDataContext.UserID, userDataContext.UserRole, rating))
            {
                Event.ReportEvent(EventIdentifier.Rate, userDataContext.UserID, objectID);
                TrackingManager.TrackObjectEvent(objectType, objectID, TrackRule.Rated, string.Empty);
                UserActivities.InsertRatedObject(userDataContext, objectID.Value);
            }
            // immer false damit nicht zweimal hintereinander gerated werden kann
            return false;
        }

        public static bool AddRating(UserDataContext userDataContext, Guid? objectID, int objectType, int points)
        {
            Rating objRating = new Rating();
            objRating.SetAsStandard(1, points, objectID.Value);
            objRating.RatingObjects[0].VoteThis = points;

            return AddRating(userDataContext, objectID.Value, objectType, objRating);
        }

        public static bool IsRatingPossible(UserDataContext userDataContext, Guid? objectID)
        {
            Data.DataObject objData = new Data.DataObject();
            return objData.IsRatingPossible(objectID.Value, userDataContext.UserID, userDataContext.UserRole, RatingType.Standard);
        }

        public static bool IsRatingPossible(UserDataContext userDataContext, Guid? objectID, RatingType ratingType)
        {
            Data.DataObject objData = new Data.DataObject();
            return objData.IsRatingPossible(objectID.Value, userDataContext.UserID, userDataContext.UserRole, ratingType);
        }

        public static bool Exists(Guid? objectID)
        {
            Data.DataObject objData = new Data.DataObject();
            return objData.Exists(objectID.Value);
        }

        public static bool IsUserOwner(Guid objectID, Guid userId)
        {
            return Data.DataObject.IsUserObjectOwner(objectID, userId);
        }

        public static void CloneObject(DataObject item, DataObject srcItem)
        {
            item.objectID = srcItem.objectID;
            item.userID = srcItem.userID;
            item.nickname = srcItem.nickname;
            item.externalObjectID = srcItem.externalObjectID;
            item.partnerID = srcItem.partnerID;
            item.instanceID = srcItem.instanceID;
            item.communityID = srcItem.communityID;
            item.objectType = srcItem.objectType;
            item.title = srcItem.title;
            item.description = srcItem.description;
            item.descriptionLinked = srcItem.descriptionLinked;
            item.descriptionMobile = srcItem.descriptionMobile;
            item.image = srcItem.image;
            item.copyright = srcItem.copyright;
            item.tagList = srcItem.tagList;
            item.objectStatus = srcItem.objectStatus;
            item.langCode = srcItem.langCode;
            item.dateInserted = srcItem.dateInserted;
            item.dateUpdated = srcItem.dateUpdated;
            item.insertedBy = srcItem.insertedBy;
            item.updatedBy = srcItem.updatedBy;
            item.viewCount = srcItem.viewCount;
            item.ratedCount = srcItem.ratedCount;
            item.ratedAmount = srcItem.ratedAmount;
            item.ratedAverage = srcItem.ratedAverage;
            item.commentedCount = srcItem.commentedCount;
            item.incentivePoints = srcItem.incentivePoints;
            item.memberCount = srcItem.memberCount;
            item.favoriteCount = srcItem.favoriteCount; 
            item.agility = srcItem.agility;
            item.featured = srcItem.featured;
            item.xmlData = srcItem.xmlData;
            item.originalObjectID = srcItem.originalObjectID;
            item.CountryCode = srcItem.CountryCode;
            item.Region = srcItem.Region;
            item.Street = srcItem.Street;
            item.Zip = srcItem.Zip;
            item.City = srcItem.City;
            item.Geo_Lat = srcItem.Geo_Lat;
            item.Geo_Long = srcItem.Geo_Long;
            item.ShowState = srcItem.ShowState;
            item.startDate = srcItem.startDate;
            item.endDate = srcItem.endDate;
            item.UrlXSLT = srcItem.UrlXSLT;
            item.ParentObjectID = srcItem.ParentObjectID;
            item.roleRight = srcItem.roleRight;
            item.objectState = srcItem.objectState;
        }

        public static void AddToFavorite(UserDataContext userDataContext, Guid objectID, int objectType)
        {
            if (userDataContext.IsAuthenticated)
            {
                Data.DataObject.ManageFavorite(objectID, objectType, userDataContext.UserID, true);
            }
        }

        public static void RemoveFromFavorite(UserDataContext userDataContext, Guid objectID)
        {
            if (userDataContext.IsAuthenticated)
            {
                Data.DataObject.ManageFavorite(objectID, null, userDataContext.UserID, false);
            }
        }

        public static bool IsObjectFavaorite(Guid objectID, Guid userID)
        {
            return Data.DataObject.IsObjectFavaorite(objectID, userID);
        }

        protected void WriteXml(System.Xml.XmlWriter writer)
        {
            if (SerializationType == SerializationType.Full)
                writer.WriteElementString("UserId", UserID.ToString());
            if (SerializationType == SerializationType.Transfer)
                writer.WriteElementString("ExtID", ExternalObjectID);
            writer.WriteElementString("CtyID", CommunityID.ToString());
            if (SerializationType == SerializationType.Full)
                writer.WriteElementString("Nickname", Nickname);
            writer.WriteElementString("LangCode", LangCode);
            if (SerializationType == SerializationType.Full)
                writer.WriteElementString("InsertDate", Inserted.ToString("o"));
            if (SerializationType == SerializationType.Full)
                writer.WriteElementString("UpdateDate", Updated.ToString("o"));
            writer.WriteElementString("StartDate", StartDate.ToString("o"));
            writer.WriteElementString("EndDate", EndDate.ToString("o"));
            writer.WriteElementString("Title", Title);
            writer.WriteElementString("Desc", Description);
            writer.WriteElementString("Pic", GetImage(PictureVersion.XS));
            writer.WriteElementString("Copyright", Copyright.ToString());
            writer.WriteElementString("Tags", TagList);
            writer.WriteElementString("Priority", Featured.ToString());
            writer.WriteElementString("GeoStreet", Street);
            writer.WriteElementString("GeoZip", Zip);
            writer.WriteElementString("GeoCity", City);
            writer.WriteElementString("GeoCountry", CountryCode);
            writer.WriteElementString("GeoLat", Geo_Lat.ToString());
            writer.WriteElementString("GeoLong", Geo_Long.ToString());
            writer.WriteElementString("UrlXSLT", UrlXSLT.ToString());
        }

        protected void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.NodeType == System.Xml.XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "ExtID":
                        ExternalObjectID = reader.ReadString();
                        break;
                    case "CtyID":
                        CommunityID = reader.ReadString().ToGuid();
                        break;
                    case "LangCode":
                        LangCode = reader.ReadString();
                        break;
                    case "StartDate":
                        string startDate = reader.ReadString();
                        if (!string.IsNullOrEmpty(startDate))
                            StartDate = DateTime.Parse(startDate);
                        break;
                    case "EndDate":
                        string endDate = reader.ReadString();
                        if (!string.IsNullOrEmpty(endDate))
                            EndDate = DateTime.Parse(endDate);
                        break;
                    case "Title":
                        Title = reader.ReadString();
                        break;
                    case "Desc":
                        Description = reader.ReadString();
                        break;
                    case "Pic":
                        Image = reader.ReadString();
                        break;
                    case "Copyright":
                        string copyright = reader.ReadString();
                        if (!string.IsNullOrEmpty(copyright))
                            Copyright = int.Parse(copyright);
                        break;
                    case "Tags":
                        TagList = reader.ReadString();
                        break;
                    case "Priority":
                        Featured = int.Parse(reader.ReadString());
                        break;
                    case "GeoStreet":
                        Street = reader.ReadString();
                        break;
                    case "GeoZip":
                        Zip = reader.ReadString();
                        break;
                    case "GeoCity":
                        City = reader.ReadString();
                        break;
                    case "GeoCountry":
                        CountryCode = reader.ReadString();
                        break;
                    case "GeoLat":
                        string geoLat = reader.ReadString();
                        if (!string.IsNullOrEmpty(geoLat))
                            Geo_Lat = double.Parse(geoLat);
                        break;
                    case "GeoLong":
                        string geoLong = reader.ReadString();
                        if (!string.IsNullOrEmpty(geoLong))
                            Geo_Long = double.Parse(geoLong);
                        break;
                    case "UrlXSLT":
                        UrlXSLT = reader.ReadString();
                        break;
                }
            }
        }

        #region Object To Object Relations

        public DataObjectList<T> GetChildren<T>() where T : DataObject, new()
        {
            return GetChildren<T>(false);
        }

        public DataObjectList<T> GetChildren<T>(bool ignoreCache) where T : DataObject, new()
        {
            DataObjectList<T> children = DataObjects.Load<T>(new QuickParameters()
            {
                Udc = UserDataContext,
                RelationParams = new RelationParams() { ParentObjectID = this.ObjectID },
                SortBy = QuickSort.RelationSortNumber,
                Direction = QuickSortDirection.Asc,
                IgnoreCache = ignoreCache,
                DisablePaging = true
            });
            return children;
        }

        public DataObjectList<T> GetParents<T>() where T : DataObject, new()
        {
            return GetParents<T>(false);
        }

        public DataObjectList<T> GetParents<T>(bool ignoreCache) where T : DataObject, new()
        {
            DataObjectList<T> parents = DataObjects.Load<T>(new QuickParameters()
            {
                Udc = UserDataContext,
                RelationParams = new RelationParams() { ChildObjectID = this.ObjectID },
                SortBy = QuickSort.RelationSortNumber,
                Direction = QuickSortDirection.Asc,
                IgnoreCache = ignoreCache,
                DisablePaging = true
            });
            return parents;
        }

        public static void RelInsert(RelationParams relParams, int? sortOrder)
        {
            Data.DataObject objData = new Data.DataObject();
            if (CheckAllowedRelations(relParams.ParentObjectType, relParams.ChildObjectType))
            {
                objData.RelInsert(relParams, sortOrder);
            }
            else
            {
                throw new SiemeArgumentException("DataObject", "RelInsert", "RelationParams", "Object types are not defined or not allowed");
            }
        }

        private static bool CheckAllowedRelations(int? parentObjectType, int? childObjectType)
        {
            return (parentObjectType.HasValue && childObjectType.HasValue);
        }

        public static void RelDelete(RelationParams relParams)
        {
            RelDelete(relParams, false);
        }

        public static void RelDelete(RelationParams relParams, bool deep)
        {
            if (relParams.Udc == null)
            {
                relParams.Udc = UserDataContext.GetUserDataContext();
            }
            Data.DataObject objData = new Data.DataObject();
            objData.RelDelete(relParams, deep);
        }

        public static void RelUpdate(Guid? parentObjectId, Guid? childObjectId, int sortOrder)
        {
            Data.DataObject objData = new Data.DataObject();
            objData.RelUpdate(new RelationParams { Udc = UserDataContext.GetUserDataContext(), ParentObjectID = parentObjectId, ChildObjectID = childObjectId }, sortOrder);
        }

        #endregion
    }

    public class DataObjectEqualityComparer : IEqualityComparer<DataObject>
    {
        bool IEqualityComparer<DataObject>.Equals(DataObject x, DataObject y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.ObjectID.HasValue && y.ObjectID.HasValue && x.ObjectID == y.ObjectID;
        }

        int IEqualityComparer<DataObject>.GetHashCode(DataObject obj)
        {
            return obj.ObjectID.GetHashCode();
        }
    }
}