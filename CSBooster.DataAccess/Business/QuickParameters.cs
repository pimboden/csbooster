//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//             #1.2.0.0    23.01.2008 / PT   QuickLoad (SQL) anpassen / Objekttypen erweitert 
//******************************************************************************
using System;
using System.Collections.Specialized;
using System.Web;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class QuickParameters
    {
        private int objectType = 0;
        private Guid? communityID = null;
        private Guid? userID = null;
        private Guid? tagID = null;
        private Guid? groupID = null;
        private QuickSort sortBy = QuickSort.StartDate;
        private QuickSortDirection direction = QuickSortDirection.Desc;
        private DBCatalogSearchType dbCatalogSearchType = DBCatalogSearchType.ContainsTable;
        private int? amount = null;
        private int pageNumber = 1;
        private int pageSize = 10;
        private bool? disablePaging;
        private Guid? currentObjectID = null;
        private string excludeObjectIds = null;
        private ObjectShowState? showState = null;
        private DateTime? fromInserted = null;
        private DateTime? toInserted = null;
        private int? featured = null;
        private bool? withCopy = null;
        private DateTime? fromStartDate = null;
        private DateTime? toStartDate = null;
        private DateTime? fromEndDate = null;
        private DateTime? toEndDate = null;
        private QuickDateQueryMethode? dateQueryMethode = null;
        private string country = null;
        private string zip = null;
        private string city = null;
        private float? geoLat = null;
        private float? geoLong = null;
        private int? distanceKm = null;
        private bool? onlyConverted = null;
        private bool? onlyWithImage = null;
        private bool? includeGroups = null;
        private bool? onlyGeoTagged = null;
        private string nickname = null;
        private string title = null;
        private string titleLeftChar = null;
        private string description = null;
        private string userSearch = null;
        private string generalSearch = null;
        private string tags1 = null;
        private string tags2 = null;
        private string tags3 = null;
        private string rawTags1 = null;
        private string rawTags2 = null;
        private string rawTags3 = null;
        private string oTypes = null;
        private string cties = null;
        private Guid? objectID = null;
        private UserDataContext udc = null;
        private int pageTotal = 0;
        private int itemTotal = 0;
        private bool? ignoreCache = null;
        private int? cachingTimeInMinutes;
        private string parentObjectID = null;
        private bool checkUserRoleRight = false;

        public RelationParams RelationParams { get; set; }

        public MembershipParams MembershipParams { get; set; }

        public ViewLogParams ViewLogParams { get; set; }

        public UserDataContext Udc
        {
            get { return udc; }
            set { udc = value; }
        }

        internal Guid? UserIDLogedIn
        {
            get { return udc.UserID; }
        }

        public int ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        public string ObjectTypes
        {
            get { return oTypes; }
            set { oTypes = value; }
        }

        public Guid? CommunityID
        {
            get { return communityID; }
            set { communityID = value; }
        }

        public string Communities
        {
            get { return cties; }
            set { cties = value; }
        }

        public Guid? UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public Guid? TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        public string Tags1
        {
            get { return tags1; }
            set { tags1 = value; }
        }

        public string Tags2
        {
            get { return tags2; }
            set { tags2 = value; }
        }

        public string Tags3
        {
            get { return tags3; }
            set { tags3 = value; }
        }

        public string RawTags1
        {
            get { return rawTags1; }
            set { rawTags1 = value; }
        }

        public string RawTags2
        {
            get { return rawTags2; }
            set { rawTags2 = value; }
        }

        public string RawTags3
        {
            get { return rawTags3; }
            set { rawTags3 = value; }
        }

        public Guid? GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public QuickSort SortBy
        {
            get { return sortBy; }
            set { sortBy = value; }
        }

        public QuickSortDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public DBCatalogSearchType CatalogSearchType
        {
            get { return dbCatalogSearchType; }
            set { dbCatalogSearchType = value; }
        }

        public int? Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public bool? DisablePaging
        {
            get { return disablePaging; }
            set { disablePaging = value; }
        }

        public Guid? CurrentObjectID
        {
            get { return currentObjectID; }
            set { currentObjectID = value; }
        }

        public string ExcludeObjectIds
        {
            get { return excludeObjectIds; }
            set { excludeObjectIds = value; }
        }

        public ObjectShowState? ShowState
        {
            get { return showState; }
            set { showState = value; }
        }

        public DateTime? FromInserted
        {
            get { return fromInserted; }
            set { fromInserted = value; }
        }

        public DateTime? ToInserted
        {
            get { return toInserted; }
            set { toInserted = value; }
        }

        public bool? IgnoreCache
        {
            get { return ignoreCache; }
            set { ignoreCache = value; }
        }

        public int? CachingTimeInMinutes
        {
            get { return cachingTimeInMinutes; }
            set { cachingTimeInMinutes = value; }
        }

        public int? Featured
        {
            get { return featured; }
            set { featured = value; }
        }

        public bool? WithCopy
        {
            get { return withCopy; }
            set { withCopy = value; }
        }

        public DateTime? FromStartDate
        {
            get { return fromStartDate; }
            set { fromStartDate = value; }
        }

        public DateTime? ToStartDate
        {
            get { return toStartDate; }
            set { toStartDate = value; }
        }

        public DateTime? FromEndDate
        {
            get { return fromEndDate; }
            set { fromEndDate = value; }
        }

        public DateTime? ToEndDate
        {
            get { return toEndDate; }
            set { toEndDate = value; }
        }

        public QuickDateQueryMethode? DateQueryMethode
        {
            get { return dateQueryMethode; }
            set { dateQueryMethode = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public float? GeoLat
        {
            get { return geoLat; }
            set { geoLat = value; }
        }

        public float? GeoLong
        {
            get { return geoLong; }
            set { geoLong = value; }
        }

        public int? DistanceKm
        {
            get { return distanceKm; }
            set { distanceKm = value; }
        }

        public string ParentObjectID
        {
            get { return parentObjectID; }
            set { parentObjectID = value; }
        }

        public bool CheckUserRoleRight
        {
            get { return checkUserRoleRight; }
            set { checkUserRoleRight = value; }
        }

        internal string UserRole
        {
            get
            {
                if (CheckUserRoleRight)
                    return udc.UserRole;
                else
                    return string.Empty;
            }
        }

        public bool? OnlyConverted
        {
            get { return onlyConverted; }
            set { onlyConverted = value; }
        }

        public bool? IncludeGroups
        {
            get { return includeGroups; }
            set { includeGroups = value; }
        }

        public bool? OnlyGeoTagged
        {
            get { return onlyGeoTagged; }
            set { onlyGeoTagged = value; }
        }

        public bool? OnlyWithImage
        {
            get { return onlyWithImage; }
            set { onlyWithImage = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                if (!string.IsNullOrEmpty(title))
                    title = title.TrimEnd();
            }
        }

        public string TitleLeftChar
        {
            get { return titleLeftChar; }
            set { titleLeftChar = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string UserSearch
        {
            get { return userSearch; }
            set { userSearch = value; }
        }

        public string GeneralSearch
        {
            get { return generalSearch; }
            set { generalSearch = value; }
        }

        public Guid? ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }

        internal int PageTotal
        {
            get { return pageTotal; }
            set { pageTotal = value; }
        }

        internal int ItemTotal
        {
            get { return itemTotal; }
            set { itemTotal = value; }
        }

        //internal int InitialCapacity
        //{
        //    get
        //    {
        //        if (amount > 0 && amount < pageSize)
        //            return amount;
        //        else if (pageSize > 0)
        //            return pageSize;
        //        else
        //            return 25;
        //    }
        //}

        public void FillFromQuickParameter(QuickParameters quickParameterToClone)
        {

            RelationParams = quickParameterToClone.RelationParams;

            MembershipParams = quickParameterToClone.MembershipParams;

            ViewLogParams = quickParameterToClone.ViewLogParams;

            Udc = quickParameterToClone.Udc;

            ObjectType = quickParameterToClone.ObjectType;

            ObjectTypes = quickParameterToClone.ObjectTypes;

            CommunityID = quickParameterToClone.CommunityID;

            Communities = quickParameterToClone.Communities;

            UserID = quickParameterToClone.UserID;

            TagID = quickParameterToClone.TagID;

            Tags1 = quickParameterToClone.Tags1;

            Tags2 = quickParameterToClone.Tags2;

            Tags3 = quickParameterToClone.Tags3;

            RawTags1 = quickParameterToClone.RawTags1;

            RawTags2 = quickParameterToClone.RawTags2;

            RawTags3 = quickParameterToClone.RawTags3;

            GroupID = quickParameterToClone.GroupID;

            SortBy = quickParameterToClone.SortBy;

            Direction = quickParameterToClone.Direction;

            CatalogSearchType = quickParameterToClone.CatalogSearchType;

            Amount = quickParameterToClone.Amount;

            PageNumber = quickParameterToClone.PageNumber;

            PageSize = quickParameterToClone.PageSize;

            DisablePaging = quickParameterToClone.DisablePaging;

            CurrentObjectID = quickParameterToClone.CurrentObjectID;

            ShowState = quickParameterToClone.ShowState;

            FromInserted = quickParameterToClone.FromInserted;

            ToInserted = quickParameterToClone.ToInserted;

            IgnoreCache = quickParameterToClone.IgnoreCache;

            CachingTimeInMinutes = quickParameterToClone.CachingTimeInMinutes;

            Featured = quickParameterToClone.Featured;

            WithCopy = quickParameterToClone.WithCopy;

            FromStartDate = quickParameterToClone.FromStartDate;

            ToStartDate = quickParameterToClone.ToStartDate;

            FromEndDate = quickParameterToClone.FromEndDate;

            ToEndDate = quickParameterToClone.ToEndDate;

            DateQueryMethode = quickParameterToClone.DateQueryMethode;

            Country = quickParameterToClone.Country;

            Zip = quickParameterToClone.Zip;

            City = quickParameterToClone.City;

            GeoLat = quickParameterToClone.GeoLat;

            GeoLong = quickParameterToClone.GeoLong;

            DistanceKm = quickParameterToClone.DistanceKm;

            ParentObjectID = quickParameterToClone.ParentObjectID;

            CheckUserRoleRight = quickParameterToClone.CheckUserRoleRight;

            OnlyConverted = quickParameterToClone.OnlyConverted;

            IncludeGroups = quickParameterToClone.IncludeGroups;

            OnlyGeoTagged = quickParameterToClone.OnlyGeoTagged;

            OnlyWithImage = quickParameterToClone.OnlyWithImage;

            Nickname = quickParameterToClone.Nickname;

            Title = quickParameterToClone.Title;

            TitleLeftChar = quickParameterToClone.TitleLeftChar;

            Description = quickParameterToClone.Description;

            UserSearch = quickParameterToClone.UserSearch;

            GeneralSearch = quickParameterToClone.GeneralSearch;

            ObjectID = quickParameterToClone.ObjectID;

            PageTotal = quickParameterToClone.PageTotal;

            ItemTotal = quickParameterToClone.ItemTotal;

        }

        public virtual void FromNameValueCollection(NameValueCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["OT"]))
                ObjectType = Helper.GetObjectTypeNumericID(collection["OT"]);
            if (!string.IsNullOrEmpty(collection["OTS"]))
            {
                ObjectType = 0;
                ObjectTypes = QuickParameters.GetDelimitedObjectTypeIDs(collection["OTS"], ',');
            }
            string paramCtyId = string.Empty;
            if (collection["XCN"] != null)
                paramCtyId = collection["XCN"];
            else if (!string.IsNullOrEmpty(collection["CN"]))
                paramCtyId = collection["CN"];
            if (!string.IsNullOrEmpty(paramCtyId))
            {
                if (!paramCtyId.IsGuid())
                    CommunityID = DataObjectCommunity.GetCommunityIDByVirtualURL(paramCtyId);
                else
                    CommunityID = paramCtyId.ToGuid();
            }
            if (!string.IsNullOrEmpty(collection["CNS"]))
            {
                CommunityID = null;
                Communities = QuickParameters.GetDelimitedCommunityIDs(collection["CNS"], ',');
            }

            string paramUserId = string.Empty;
            if (collection["XUI"] != null)
                paramUserId = collection["XUI"];
            else if (!string.IsNullOrEmpty(collection["UI"]))
                paramUserId = collection["UI"];
            if (!string.IsNullOrEmpty(paramUserId))
            {
                if (!paramUserId.IsGuid())
                    UserID = DataObjectUser.GetUserIDByNickname(paramUserId);
                else
                    UserID = paramUserId.ToGuid();
            }

            if (!string.IsNullOrEmpty(collection["TG"]))
                TagID = HttpUtility.UrlDecode(collection["TG"]).ToNullableGuid();
            if (!string.IsNullOrEmpty(collection["TGL1"]))
            {
                TagID = null;
                RawTags1 = collection["TGL1"];
                Tags1 = QuickParameters.GetDelimitedTagIds(HttpUtility.UrlDecode(collection["TGL1"]), ',');
            }
            if (!string.IsNullOrEmpty(collection["TGL2"]))
            {
                TagID = null;
                RawTags2 = collection["TGL2"];
                Tags2 = QuickParameters.GetDelimitedTagIds(HttpUtility.UrlDecode(collection["TGL2"]), ',');
            }
            if (!string.IsNullOrEmpty(collection["TGL3"]))
            {
                TagID = null;
                RawTags3 = collection["TGL3"];
                Tags3 = QuickParameters.GetDelimitedTagIds(HttpUtility.UrlDecode(collection["TGL3"]), ',');
            }
            if (!string.IsNullOrEmpty(collection["SO"]))
                SortBy = (QuickSort)Enum.Parse(typeof(QuickSort), collection["SO"], true);
            if (!string.IsNullOrEmpty(collection["SD"]))
                Direction = (QuickSortDirection)Enum.Parse(typeof(QuickSortDirection), collection["SD"], true);
            if (!string.IsNullOrEmpty(collection["AM"]))
            {
                int temp;
                if (int.TryParse(collection["AM"], out temp))
                    amount = temp;
            }
            if (!string.IsNullOrEmpty(collection["PN"]))
                int.TryParse(collection["PN"], out pageNumber);
            if (!string.IsNullOrEmpty(collection["PS"]))
            {
                int.TryParse(collection["PS"], out pageSize);
                pageSize = Math.Min(pageSize, 100);
            }
            if (!string.IsNullOrEmpty(collection["SS"]))
                ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), collection["SS"], true);
            if (!string.IsNullOrEmpty(collection["FI"]))
            {
                DateTime formInserted;
                if (DateTime.TryParse(collection["FI"], out formInserted))
                    fromInserted = formInserted;
            }
            if (!string.IsNullOrEmpty(collection["TI"]))
            {
                DateTime toInserted;
                if (DateTime.TryParse(collection["TI"], out toInserted))
                    this.toInserted = toInserted;
            }
            if (!string.IsNullOrEmpty(collection["FE"]))
            {
                int featured;
                if (int.TryParse(collection["FE"], out featured))
                    this.featured = featured;
            }
            if (!string.IsNullOrEmpty(collection["WC"]))
            {
                bool withCopy;
                if (bool.TryParse(collection["WC"], out withCopy))
                    this.withCopy = withCopy;
            }
            if (!string.IsNullOrEmpty(collection["FS"]))
            {
                DateTime fromStartDate;
                if (DateTime.TryParse(collection["FS"], out fromStartDate))
                    this.fromStartDate = fromStartDate;
            }
            if (!string.IsNullOrEmpty(collection["TS"]))
            {
                DateTime toEndDate;
                if (DateTime.TryParse(collection["TS"], out toEndDate))
                    this.toEndDate = toEndDate;
            }
            if (!string.IsNullOrEmpty(collection["DM"]))
                DateQueryMethode = (QuickDateQueryMethode)Enum.Parse(typeof(QuickDateQueryMethode), collection["DM"], true);
            if (!string.IsNullOrEmpty(collection["CO"]))
                Country = collection["CO"];
            if (!string.IsNullOrEmpty(collection["ZP"]))
                Zip = collection["ZP"];
            if (!string.IsNullOrEmpty(collection["CI"]))
                City = HttpUtility.UrlDecode(collection["CI"]);
            if (!string.IsNullOrEmpty(collection["GC"]))
            {
                string[] coordsList = collection["GC"].Split(new char[] { ',' });
                if (coordsList.Length == 2)
                {
                    GeoLat = float.Parse(coordsList[0]);
                    GeoLong = float.Parse(coordsList[1]);
                }
            }
            if (!string.IsNullOrEmpty(collection["DI"]))
                DistanceKm = int.Parse(collection["DI"]);
            if (!string.IsNullOrEmpty(collection["OC"]))
            {
                bool onlyConverted;
                if (bool.TryParse(collection["OC"], out onlyConverted))
                    this.onlyConverted = onlyConverted;
            }
            if (!string.IsNullOrEmpty(collection["OI"]))
            {
                bool onlyWithImage;
                if (bool.TryParse(collection["OI"], out onlyWithImage))
                    this.onlyWithImage = onlyWithImage;
            }
            if (!string.IsNullOrEmpty(collection["NI"]))
                Nickname = HttpUtility.UrlDecode(collection["NI"]);
            if (!string.IsNullOrEmpty(collection["TL"]))
                Title = HttpUtility.UrlDecode(collection["TL"]);
            if (!string.IsNullOrEmpty(collection["DE"]))
                Description = HttpUtility.UrlDecode(collection["DE"]);
            if (!string.IsNullOrEmpty(collection["SU"]))
                UserSearch = HttpUtility.UrlDecode(collection["SU"]);
            if (!string.IsNullOrEmpty(collection["IG"]))
            {
                bool boolVal;
                if (bool.TryParse(collection["IG"], out boolVal))
                    IncludeGroups = boolVal;
            }
            if (!string.IsNullOrEmpty(collection["IC"]))
            {
                bool boolVal;
                if (bool.TryParse(collection["IC"], out boolVal))
                    IgnoreCache = boolVal;
            }
            if (!string.IsNullOrEmpty(collection["GT"]))
            {
                bool boolVal;
                if (bool.TryParse(collection["GT"], out boolVal))
                    OnlyGeoTagged = boolVal;
            }
            if (!string.IsNullOrEmpty(collection["SG"]))
                GeneralSearch = HttpUtility.UrlDecode(collection["SG"]);

            if (!string.IsNullOrEmpty(collection["OID"]))
                ObjectID = collection["OID"].ToGuid();
        }

        public string ToJSON()
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(this);
        }

        public string ToJSON(int recursionDepth)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(this);
        }

        public override string ToString()
        {
            string strUserID = "XX";
            if (UserIDLogedIn.HasValue && UserIDLogedIn.Value != Constants.ANONYMOUS_USERID.ToGuid())
                strUserID = string.Concat("X", UserIDLogedIn);

            System.Text.StringBuilder sb = new System.Text.StringBuilder(string.Format("OT{0}-{1}-{2}-{3}-{4}-{5}-{6}", (int)ObjectType, strUserID, (int)SortBy, CommunityID, UserID, TagID, (int)Direction));

            if (Featured != null)
                sb.AppendFormat("-B{0}", Featured.Value);

            if (WithCopy != null)
                sb.AppendFormat("-C{0}", WithCopy.Value ? "1" : "0");

            if (ShowState != null)
                sb.AppendFormat("-D{0}", (int)ShowState.Value);

            if (FromInserted != null)
                sb.AppendFormat("-E{0}", FromInserted.Value.Date.Ticks);

            if (ToInserted != null)
                sb.AppendFormat("-F{0}", ToInserted.Value.Date.Ticks);

            if (FromStartDate != null)
                sb.AppendFormat("-G1{0}", FromStartDate.Value.Date.Ticks);

            if (ToStartDate != null)
                sb.AppendFormat("-G2{0}", ToStartDate.Value.Date.Ticks);

            if (FromEndDate != null)
                sb.AppendFormat("-H1{0}", FromEndDate.Value.Date.Ticks);

            if (ToEndDate != null)
                sb.AppendFormat("-H2{0}", ToEndDate.Value.Date.Ticks);

            if (DateQueryMethode != null)
                sb.AppendFormat("-I{0}", (int)DateQueryMethode.Value);

            if (!string.IsNullOrEmpty(Country))
                sb.AppendFormat("-J{0}", Country.ToLower());

            if (!string.IsNullOrEmpty(Zip))
                sb.AppendFormat("-K{0}", Zip.ToLower());

            if (!string.IsNullOrEmpty(City))
                sb.AppendFormat("-L{0}", City.ToLower());

            if (DistanceKm != null && GeoLat != null && GeoLong != null)
                sb.AppendFormat("-M{0}({1}{2})", DistanceKm.Value, GeoLat.Value, GeoLong.Value);

            if (!string.IsNullOrEmpty(ParentObjectID))
                sb.AppendFormat("-N{0}", ParentObjectID);

            if (CheckUserRoleRight)
                sb.AppendFormat("-O{0}", UserRole.ToLower());

            if (PageSize > 0 || Amount > 0)
                sb.AppendFormat("-P{0}", string.Format("{0}.{1}.{2}", PageNumber, PageSize, Amount));

            if (!string.IsNullOrEmpty(Communities))
            {
                sb.AppendFormat("-R{0}", Communities.ToLower());
            }

            if (!string.IsNullOrEmpty(Tags1))
            {
                sb.AppendFormat("-S1{0}", Tags1.ToLower());
            }
            if (!string.IsNullOrEmpty(RawTags1))
            {
                sb.AppendFormat("-SR1{0}", RawTags1.ToLower());
            }

            if (!string.IsNullOrEmpty(Tags2))
            {
                sb.AppendFormat("-S2{0}", Tags2.ToLower());
            }
            if (!string.IsNullOrEmpty(RawTags2))
            {
                sb.AppendFormat("-SR2{0}", RawTags2.ToLower());
            }

            if (!string.IsNullOrEmpty(Tags3))
            {
                sb.AppendFormat("-S3{0}", Tags3.ToLower());
            }
            if (!string.IsNullOrEmpty(RawTags3))
            {
                sb.AppendFormat("-SR3{0}", RawTags3.ToLower());
            }

            if (!string.IsNullOrEmpty(ObjectTypes))
            {
                sb.AppendFormat("-T{0}", ObjectTypes.ToLower());
            }

            if (GroupID.HasValue)
                sb.AppendFormat("-X1{0}", GroupID.Value);

            if (CatalogSearchType != DBCatalogSearchType.None)
                sb.AppendFormat("-X2{0}", (int)CatalogSearchType);

            if (DisablePaging.HasValue)
                sb.AppendFormat("-X3{0}", DisablePaging.Value ? "1" : "0");

            if (CurrentObjectID.HasValue)
                sb.AppendFormat("-XC4{0}", CurrentObjectID.Value);

            if (ObjectID.HasValue)
                sb.AppendFormat("-XO4{0}", ObjectID.Value);

            if (OnlyConverted.HasValue)
                sb.AppendFormat("-X5{0}", OnlyConverted.Value ? "1" : "0");

            if (OnlyWithImage.HasValue)
                sb.AppendFormat("-X6{0}", OnlyWithImage.Value ? "1" : "0");

            if (IncludeGroups.HasValue)
                sb.AppendFormat("-X7{0}", IncludeGroups.Value ? "1" : "0");

            if (OnlyGeoTagged.HasValue)
                sb.AppendFormat("-X8{0}", OnlyGeoTagged.Value ? "1" : "0");

            if (!string.IsNullOrEmpty(nickname))
                sb.AppendFormat("-X9{0}", nickname.ToLower());

            if (!string.IsNullOrEmpty(title))
                sb.AppendFormat("-X10{0}", title.ToLower());

            if (!string.IsNullOrEmpty(description))
                sb.AppendFormat("-X11{0}", description.ToLower());

            if (!string.IsNullOrEmpty(userSearch))
                sb.AppendFormat("-X12{0}", userSearch.ToLower());

            if (!string.IsNullOrEmpty(generalSearch))
                sb.AppendFormat("-X13{0}", generalSearch.ToLower());

            if (!string.IsNullOrEmpty(oTypes))
                sb.AppendFormat("-X14{0}", oTypes.ToLower());

            if (!string.IsNullOrEmpty(cties))
                sb.AppendFormat("-X15{0}", cties.ToLower());

            if (!string.IsNullOrEmpty(titleLeftChar))
                sb.AppendFormat("-X16{0}", titleLeftChar.ToLower());

            if (RelationParams != null)
                sb.AppendFormat("-RP{0}", RelationParams.ToString());

            if (MembershipParams != null)
                sb.AppendFormat("-MP{0}", MembershipParams.ToString());

            if (ViewLogParams != null)
                sb.AppendFormat("-LP{0}", ViewLogParams.ToString());



            //public ViewLogParams ViewLogParams { get; set; }


            return sb.ToString();
        }

        public static string GetDelimitedTagIds(string delimitedTags, char delimiter)
        {
            string allObjs = string.Empty;
            if (!string.IsNullOrEmpty(delimitedTags))
            {
                string allObjsToFind = string.Empty;
                string[] ids = delimitedTags.RemoveDuplicates(delimiter).Split(delimiter);
                foreach (string id in ids)
                {
                    if (id.Length > 0)
                    {
                        if (id.IsGuid())
                        {
                            allObjs += id + "|";
                        }
                        else
                        {
                            allObjsToFind += string.Format("{0}|", id);
                        }
                    }
                }
                if (allObjsToFind.Length > 0)
                {
                    allObjsToFind = allObjsToFind.TrimEnd('|');
                    Data.CSBooster_DataAccessDataContext csbDAC = new _4screen.CSB.DataAccess.Data.CSBooster_DataAccessDataContext();
                    var founsIDs = csbDAC.hisp_TagWord_GetIDSByWords(allObjsToFind);
                    foreach (var foundID in founsIDs)
                    {
                        allObjs += foundID.TGW_ID.ToString() + "|";
                    }
                }
                if (allObjs.Length > 0)
                {
                    allObjs = allObjs.TrimEnd('|');
                    allObjs.RemoveDuplicates('|');
                }
            }
            return allObjs;
        }

        public static string GetDelimitedCommunityIDs(string delimitedCommunities, char delimiter)
        {
            string allObjs = string.Empty;
            if (!string.IsNullOrEmpty(delimitedCommunities))
            {
                string allObjsToFind = string.Empty;
                string[] ids = delimitedCommunities.RemoveDuplicates(delimiter).Split(delimiter);
                foreach (string id in ids)
                {
                    if (id.Length > 0)
                    {
                        if (id.IsGuid())
                        {
                            allObjs += id + "|";
                        }
                        else
                        {
                            allObjsToFind += string.Format("{0}|", id);
                        }
                    }
                }
                if (allObjsToFind.Length > 0)
                {
                    allObjsToFind = allObjsToFind.TrimEnd('|');
                    Data.CSBooster_DataAccessDataContext csbDAC = new _4screen.CSB.DataAccess.Data.CSBooster_DataAccessDataContext();
                    var founsIDs = csbDAC.hisp_Community_GetIDSByVUrls(allObjsToFind);
                    foreach (var foundID in founsIDs)
                    {
                        allObjs += foundID.CTY_ID.ToString() + "|";
                    }
                }
                if (allObjs.Length > 0)
                {
                    allObjs = allObjs.TrimEnd('|');
                    allObjs.RemoveDuplicates('|');
                }
            }
            return allObjs;
        }

        public static string GetDelimitedObjectTypeIDs(string delimitedObjectTypes, char delimiter)
        {
            string allObjs = string.Empty;
            if (!string.IsNullOrEmpty(delimitedObjectTypes))
            {
                string[] ids = delimitedObjectTypes.RemoveDuplicates(delimiter).Split(delimiter);
                foreach (string id in ids)
                {
                    if (id.Length > 0)
                    {
                        int objid = -1;
                        ;
                        if (int.TryParse(id, out objid))
                        {
                            allObjs += id + "|";
                        }
                        else
                        {
                            try
                            {
                                objid = Helper.GetObjectTypeNumericID(id);
                                allObjs += string.Format("{0}|", objid);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                if (allObjs.Length > 0)
                {
                    allObjs = allObjs.TrimEnd('|');
                    allObjs.RemoveDuplicates('|');
                }
            }
            return allObjs;
        }
    }
}