// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.Common
{
    public enum CommunityStatus
    {
        Initializing = 0,
        Ready = 1
    }

    public enum VieverType
    {
        Anonymous = 0,
        Visitor = 1,
        Invited = 2,
        Owner = 3
    }

    public enum AnswerType
    {
        Unspecified = 0,
        String = 1,
        Int = 2,
        Float = 3,
        Date = 4
    }

    public enum FillListBy
    {
        Value = 0,
        Text = 1,
        Any = 2
    }

    public enum MessageTypes
    {
        AllMessages = 0,
        FriendRequest = 1,
        FriendMessage = 2,
        NormalMessage = 3,
        SystemMessage = 4,
        NotificationMessage = 5,
        PinboardOfferResponse = 6,
        PinboardSearchResponse = 7,
        InviteToCommunity = 8
    }

    public enum CarrierType
    {
        None = 0,
        EMail = 1,
        CSBMessage = 2
    }

    public enum AuthenticationType
    {
        CSBooster,
        OpenID,
        InformationCard,
        FacebookConnect
    }

    public enum MessageState
    {
        None = 0,
        Replied = 1,
        Forwarded = 2
    }

    public enum CommentStatus
    {
        None = 0,
        Deleted = 1
    }

    public enum ReceiveMessageFrom
    {
        All = 0,
        Friends = 1,
        None = 2
    }

    public enum MessageGroupTypes
    {
        Inbox = 1,
        Outbox = 2
    }

    public enum YesNo
    {
        Yes = 0,
        No = 1
    }

    public enum VideoVersion
    {
        None,
        XS,
        S,
        M,
        L
    }

    public enum VideoFormat
    {
        Unknow = 0,
        Avi = 1,
        Mpg = 2,
        Mp4 = 3,
        Mov = 4,
        M4v = 5,
        Flv = 6,
        _3gp = 7,
        Wmv = 8
    }

    public enum PictureFormat
    {
        Unknown,
        Jpg,
        Gif,
        Png
    }

    public enum PictureVersion
    {
        None,
        A,
        XS,
        S,
        M,
        L,
        C1,
        C2,
        C3,
        C4,
        C5
    }

    public enum AudioFormat
    {
        Unknow = 0,
        mp3 = 1,
        mp4 = 2,
        wma = 3
    }

    public enum MediaConvertedState
    {
        NotConvertet = 0,
        InProgress = 1,
        Convertet = 2,
        ConvertError = 99
    }

    public enum ObjectState
    {
        Added = 0,
        Saved = 1,
        Changed = 2,
        Deleted = 3
    }

    public enum ObjectShowState
    {
        ConversionFailed = -2,
        InProgress = -1,
        Published = 0,
        Draft = 1,
        Blocked = 2,
        Deleted = 3
    }


    public enum DBCatalogSearchType
    {
        None = 0,
        FreetextTable = 1,
        ContainsTable = 4
    }

    public enum SerializationType
    {
        Full,
        Transfer
    }

    public enum AccessMode
    {
        Load,
        Insert,
        Update,
        Delete
    }

    [Flags]
    public enum ObjectAccessRight
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 4,
        Select = 8
    }

    public enum ClickerQueryType
    {
        Default = 0,
        MyMemberships = 1,
        CommunityMembers = 2,
        LastVisitors = 3,
        MyFriends = 4,
        MyCommunities = 5
    }

    public enum QuickSort
    {
        NotSorted = 0,
        Viewed = 1,
        Commented = 2,
        RatedCount = 3,
        RatedAverage = 4,
        IncentivePoints = 5,
        MemberCount = 6,
        InsertedDate = 7,
        Random = 8,
        Linked = 9,
        Agility = 10,
        StartDate = 11,
        RatedConsolidated = 12,
        Title = 13,
        Accuracy = 14,
        RelationSortNumber = 15,
        FavoriteCount = 16,
        Nickname = 17,
        ModifiedDate
    }

    public enum RelationSortType
    {
        Child = 1,
        Parent = 2
    }

    public enum QuickSortDirection
    {
        Asc = 1,
        Desc = 2
    }

    public enum QuickDateQueryMethode
    {
        StartOpenEndOpen = 1,
        StartOpenEndExact = 2,
        StartExactEndOpen = 3,
        StartExactEndExact = 4,
        BetweenStartOpenEndOpen = 5,
        BetweenStartRangeEndRange = 6,
        BetweenStartAndEnd = 7
    }

    public enum DateConstraint
    {
        None = 0,
        UntilYesterday = 1,
        UntilToday = 2,
        Today = 3,
        FromToday = 4,
        FromTomorrow = 5
    }

    public enum DataAccessType
    {
        AccessByCommunity = 1,
        AccessByTag = 2,
        AccessByObjectType = 3,
        AccessByUser = 4
    }

    public enum PageType
    {
        None = 0,
        Detail = 1,
        Overview = 2,
        Homepage = 3
    }

    public enum QuickTagType
    {
        MainTagList = 0,
        CategoryTagList = 1,
        TagList = 2
    }

    /// <summary>
    /// Cloud Typ und Gewichtung
    /// </summary>
    public enum QuickTagCloudRelevance
    {
        /// <summary>
        /// Summe der Views, keine Gewichtung nach Datum
        /// </summary>
        NoRelevance = 0,
        /// <summary>
        /// Summe der Views, Gewichtung nach Betrachtungsdatum der Objekte
        /// </summary>
        ViewLog = 1,
        /// <summary>
        /// Summe der Views, Gewichtung nach Erstelldatum der Objekte
        /// </summary>
        ObjectView = 2,
        /// <summary>
        /// Summe der Tag Relations, Gewichtung nach Erstelldatum der Objekte
        /// </summary>
        RelatedObjects = 3
    }

    public enum QuickTagCloudRelevanceGroup
    {
        All = 0,
        Minute = 1,
        Hour = 2,
        Day = 3,
        Week = 4,
        Month = 5,
        Quarter = 6,
        Year = 7
    }

    public enum ObjectStatus
    {
        Private = 1,
        Public = 2,
        Unlisted = 3
    }

    public enum QuerySourceType
    {
        Page = 1,
        Community = 2,
        Profile = 3,
        MyContent = 4
    }

    [Flags]
    public enum FriendType
    {
        Type1 = 1,
        Type2 = 2,
        Type3 = 4,
        Type4 = 8,
        Type5 = 16,
        Type6 = 32,
        Type7 = 64,
        Type8 = 128,
        Type9 = 256,
        Type10 = 512,
    }

    public enum LocationType
    {
        LocationType1 = 1,
        LocationType2 = 2,
        LocationType3 = 3,
        LocationType4 = 4,
        LocationType5 = 5,
        LocationType6 = 6,
        LocationType7 = 7,
        LocationType8 = 8,
        LocationType9 = 9,
        LocationType10 = 10
    }

    public enum EventType
    {
        EventType1 = 1,
        EventType2 = 2,
        EventType3 = 3,
        EventType4 = 4,
        EventType5 = 5,
        EventType6 = 6,
        EventType7 = 7,
        EventType8 = 8,
        EventType9 = 9,
        EventType10 = 10
    }

    public enum TrackRule
    {
        NoTraking = 0,
        Viewed = 1,
        Upload = 2,
        Download = 3,
        Rated = 4,
        Forwarded = 5,
        Commented = 6,
        Tagged = 7,
        Inserted = 8,
        Updated = 9,
        Searched = 10,
        Deleted = 11,
        LoadedFromCache = 12,
        LoadedFromDB = 13,
        LoadedFromHTTPContext = 14
    }

    public enum RatingType
    {
        Standard = 0,
        OneVersusOne = 1,
        BestOfMany = 2
    }

    public enum MonitoringLogState
    {
        OK = 0,
        OKWithInformation = 1,
        OKWithWarning = 2,
        Aborted = 3,
        AbortedMissionCritical = 4
    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public enum StyleSection
    {
        Header = 0,
        Body = 1,
        Footer = 2
    }

    public enum FolderSort
    {
        FolderSort = 0,
        Alphabetic = 1,
        ObjectType = 2
    }

    public enum MessageBoxType
    {
        Inbox,
        Outbox,
        Flagged
    }

    public enum FriendsActionType
    {
        RequestReceived,
        RequestSent,
        Friends
    }

    public enum CommentsType
    {
        CommentsReceived,
        CommentsPosted
    }

    public enum UserProfileDataKey
    {
        Vorname = 0,
        Name,
        Sex,
        Birthday,
        Street,
        Zip,
        City,
        Land,
        Languages,
        Status,
        AttractedTo,
        BodyHeight,
        BodyWeight,
        EyeColor,
        HairColor,
        PriColor,
        SecColor,
        Mobile,
        Phone,
        MSN,
        Yahoo,
        Skype,
        ICQ,
        AIM,
        Homepage,
        Blog,
        MsgFrom,
        GetMail,
        DisplayAds,
        Beruf,
        Lebensmoto,
        Talent1,
        Talent2,
        Talent3,
        Talent4,
        Talent5,
        Talent6,
        Talent7,
        Talent8,
        Talent9,
        Talent10,
        Talent11,
        Talent12,
        Talent13,
        Talent14,
        Talent15,
        Talent16,
        Talent17,
        Talent18,
        Talent19,
        Talent20,
        Talent21,
        Talent22,
        Talent23,
        Talent24,
        Talent25,
        InterestTopic1,
        InterestTopic2,
        InterestTopic3,
        InterestTopic4,
        InterestTopic5,
        InterestTopic6,
        InterestTopic7,
        InterestTopic8,
        InterestTopic9,
        InterestTopic10,
        Interesst1,
        Interesst2,
        Interesst3,
        Interesst4,
        Interesst5,
        Interesst6,
        Interesst7,
        Interesst8,
        Interesst9,
        Interesst10
    }

    public enum UserProfileDataLoadType
    {
        LoadAll = 0,
        LoadForRegistration = 1,
        LoadForProfileData = 2,
        LoadForFriendSetting = 3
    }

    public enum CommunityUsersType
    {
        Owners = 0,
        Members = 1,
        Authenticated = 2,
        Anonymous = 3
    }

    public enum WidgetDataSource
    {
        CommunityAndGroups = 1,
        SingleCommunity = 2,
        Site = 4
    }

    public enum WidgetShowAfterInsert
    {
        Nothing = 0,
        Settings = 1,
        CreateWizard = 2
    }

    public enum MyContentMode
    {
        Admin = 1,
        Selection = 2,
        Widgets = 3,
        Related
    }

    public enum MapLayout
    {
        SidebarAndMap,
        MapOnly
    }

    public enum MapStyle
    {
        Road = 1,
        Aerial = 2,
        Hybrid = 3,
        Terrain = 4
    }

    public enum MapNavigation
    {
        None = 0,
        Normal = 1,
        Small = 2,
    }

    public enum MobileSettings
    {
        None = 0,
        All = 1,
        OnlyMessaging = 2
    }

    public enum UserActivityWhat
    {
        DoNowThis = 0,
        AreNowFriends = 1,
        IsNowMember = 2,
        HasUploadetOneObject = 3,
        HasUploadetMultipleObjects = 4,
        HasCommentedObject = 5,
        HasAnotatedObject = 6,
        HasRatedObject = 7,
        IsNowOnline = 8
    }

    public enum UserActivityType
    {
        News = 1,
        Relationship = 2,
        Objects = 3
    }

    public enum UserStartPage
    {
        Default,
        Homepage,
        Dashboard
    }

    public enum Dashboard
    {
        Dashboard,
        MessagesInbox,
        MessagesOutbox,
        MessagesFlagged,
        SettingsBasic,
        SettingsAdvanced,
        SettingsInterests,
        SettingsAlerts,
        FriendsRequestsSent,
        FriendsRequestsReceived,
        Friends,
        BlockedUsers,
        CommentsPosted,
        CommentsReceived,
        CommunityMemberships,
        CommunityInvitations,
        Alerts,
        Favorites,
        ManageContent,
        Surveys
    }

    public enum LogSitePageType
    {
        Unknow = 0,
        Detail = 1,
        Overview = 2,
        Static = 3,
        CmsPage = 4,
        Community = 5,
        UserProfile = 6,
        Homepage = 7,       //default.aspx
        MyContent = 8,
        Messaging = 9,
        Friends = 10,
        Comments = 11,
        Favorites = 13,
        Memberships = 14,
        WizardInsert = 15,
        WizardEdit = 16,
        UserProfileData = 17,
        SiteAdmin = 18,
        Activities = 19,
        WizardSpezial = 20,
        Notification = 21,
        Reporting = 22,
        WidgetSettings = 23
    }

    namespace Statistic
    {
        public enum StatisticsType
        {
            CountCommunityMembers = 0,
            CountProfileFriends = 1,
            TotalObjectsCount = 2,
            CreationObjects = 3,
            PageView = 4,
            PageViewDayRange = 5,
            UniqueUser = 6,
            UserSession = 7
        }

        public enum Granularity
        {
            None = 0,
            Year = 1,
            Month = 2,
            Day = 3
        }

        [Flags]
        public enum GroupBy
        {
            None = 1,
            Role = 2,
            Type = 4,
            Mobile = 8,
            Age = 16,
            Sex = 32,
            PageType = 64
        }

        public enum DataRange
        {
            ThisMonth = 1,
            LastMonth = 2,
            LastTwoMonth = 3
        }

        public enum ChartType
        {
            Line = 1,
            Bar = 2,
            Column = 3,
            Pie = 4
        }
    }

    namespace Widget
    {
        public enum FormAdressSave
        {
            Always = 0,
            Ask = 1,
            Never = 2
        }
    }

    namespace Notification
    {
        public enum EventIdentifier
        {
            NotDefined = 0,
            Rate = 1,
            Comment = 2,
            NewMember = 3,
            NewObject = 4,
            ChangeObject = 5,
            NewTopicItem = 6,
            NewPhotoNote = 7,
            BirthdayNotification = 8
            //Newsletter = 7,
            //Message = 8
        }

        public enum CarrierCollect
        {
            Immediately = 1,
            Daily = 2,
            Weekly = 3,
            Monthly = 4
        }

        public enum ObjectRange
        {
            MyObject = 1,
            ForeignObject = 2
        }
    }
}