using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class UserActivities
    {
        public static bool UserActivityIsActiv()
        {
            return Business.DataAccessConfiguration.UserActivityIsActiv();
        }

        public static bool UserActivityIsActivityActiv(UserActivityWhat activityWhat)
        {
            return Business.DataAccessConfiguration.UserActivityIsActivityActiv(activityWhat);
        }

        public static UserActivityList<UserActivity> Load(UserActivityParameters userActivityParameters)
        {
            return Data.UserActivities.Load(userActivityParameters);
        }

        public static void Insert(UserDataContext udc, UserActivityWhat activityWhat, Guid targetObjectID, Guid detailObjectID, bool onlyVisibleForAdmin)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, activityWhat, targetObjectID, detailObjectID, null, onlyVisibleForAdmin);
        }

        public static void InsertIsNowOnline(UserDataContext udc)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.IsNowOnline, null, null, null, null);
        }

        public static void InsertDoNowThis(UserDataContext udc, string text)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.DoNowThis, null, null, text, null);
        }

        public static void InsertComment(UserDataContext udc, Guid objectID)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.HasCommentedObject, null, objectID, null, null);
        }

        public static void InsertAnotatedObject(UserDataContext udc, Guid objectID)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.HasAnotatedObject, null, objectID, null, null);
        }

        public static void InsertRatedObject(UserDataContext udc, Guid objectID)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.HasRatedObject, null, objectID, null, null);
        }

        public static void InsertFriendship(UserDataContext udc, Guid friendID)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.AreNowFriends, friendID, null, null, null);
        }

        public static void InsertFriendship(Guid userID, string nickname, Guid friendID)
        {
            Data.UserActivities.Insert(userID, nickname, UserActivityWhat.AreNowFriends, friendID, null, null, null);
        }

        public static void InsertMembership(UserDataContext udc, Guid communityID)
        {
            Data.UserActivities.Insert(udc.UserID, udc.Nickname, UserActivityWhat.IsNowMember, communityID, null, null, null);
        }
    }

    public class UserActivity
    {
        internal UserActivity(Guid id, UserActivityWhat activityWhat)
        {
            ID = id;
            ActivityWhat = activityWhat;
            TargetObjectID = Guid.Empty;
            TargetObjectType = 0;
            DetailObjectID = Guid.Empty;
            DetailObjectType = 0;
        }

        public Guid ID
        {
            get;
            internal set;
        }

        public UserActivityWhat ActivityWhat
        {
            get;
            internal set;
        }

        public UserActivityType ActivityType
        {
            get { return Data.UserActivities.GetTypeDependOnWhat(ActivityWhat); }
        }

        public Guid UserID
        {
            get;
            internal set;
        }

        public string UserNickname
        {
            get;
            internal set;
        }

        public DateTime Date
        {
            get;
            internal set;
        }

        public Guid TargetObjectID
        {
            get;
            internal set;
        }

        public int TargetObjectType
        {
            get;
            internal set;
        }

        public string TargetObjectText
        {
            get;
            internal set;
        }

        public Guid DetailObjectID
        {
            get;
            internal set;
        }

        public int DetailObjectType
        {
            get;
            internal set;
        }

        public string DetailObjectText
        {
            get;
            internal set;
        }

        public bool OnlyVisibleForAdmin
        {
            get;
            internal set;
        }

        public override string ToString()
        {
            string vRoot = SiteConfig.SiteVRoot;
            bool isEqual = (DetailObjectID.ToString() == TargetObjectID.ToString());

            StringBuilder format = new StringBuilder(GetFormatString(isEqual), 300);

            try
            {
                if (format.ToString().IndexOf("{U}") > -1)
                    format = format.Replace("{U}", string.Format("<a href='{0}{1}'>{2}</a>", vRoot, Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), UserNickname), UserNickname));

                if (format.ToString().IndexOf("{T}") > -1)
                    format = format.Replace("{T}", string.Format("<a href='{0}{1}'>{2}</a>", vRoot, Helper.GetDetailLink(TargetObjectType, TargetObjectID.ToString()), TargetObjectText));

                if (format.ToString().IndexOf("{TTX}") > -1)
                    format = format.Replace("{TTX}", TargetObjectText);

                if (format.ToString().IndexOf("{D}") > -1)
                    format = format.Replace("{D}", string.Format("<a href='{0}{1}'>{2}</a>", vRoot, Helper.GetDetailLink(DetailObjectType, DetailObjectID.ToString()), DetailObjectText));

                if (format.ToString().IndexOf("{DTX}") > -1)
                    format = format.Replace("{DTX}", DetailObjectText);

                if (format.ToString().IndexOf("{TOT}") > -1)
                {
                    if (ActivityWhat == UserActivityWhat.HasUploadetMultipleObjects)
                        format = format.Replace("{TOT}", Helper.GetObjectName(TargetObjectType, true));
                    else
                        format = format.Replace("{TOT}", Helper.GetObjectName(TargetObjectType, true));
                }

                if (format.ToString().IndexOf("{DOT}") > -1)
                {
                    if (ActivityWhat == UserActivityWhat.HasUploadetMultipleObjects)
                        format = format.Replace("{DOT}", Helper.GetObjectName(DetailObjectType, false));
                    else
                        format = format.Replace("{DOT}", Helper.GetObjectName(DetailObjectType, true));
                }
            }
            catch (Exception)
            {
                format.Append("*** Formatting error");
            }

            return format.ToString();
        }

        private string GetFormatString(bool isEqual)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            string baseKey = string.Format("MessageWhat{0}", (int)ActivityWhat);
            string value = null;

            if (isEqual)
            {
                value = language.GetString(baseKey + "EQL");
                if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                    return value;
            }

            string detail = "None";
            string target = "None";
            if (DetailObjectType > 0)
                detail = Helper.GetObjectType(DetailObjectType).Id;

            if (TargetObjectType > 0)
                target = Helper.GetObjectType(TargetObjectType).Id;

            value = language.GetString(baseKey + detail + target);
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;

            value = language.GetString(baseKey + detail + "None");
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;

            value = language.GetString(baseKey + "None" + target);
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;

            value = language.GetString(baseKey + detail);
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;

            value = language.GetString(baseKey + target);
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;

            value = language.GetString(baseKey);
            if (!string.IsNullOrEmpty(value) && !value.StartsWith("[") && !value.EndsWith("["))
                return value;
            else
                return string.Format(" [missing message '{0}'] ", baseKey);

        }
    }
}
