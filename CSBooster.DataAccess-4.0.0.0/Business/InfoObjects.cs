// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public static class InfoObjects
    {
        public static List<InfoObject> LoadForUser(UserDataContext udc, Guid? userID, int? objectType)
        {
            return Data.InfoObjects.Load(null, userID, objectType, udc.UserID);
        }

        public static List<InfoObject> LoadForCommunity(UserDataContext udc, Guid? communityID, int? objectType)
        {
            return Data.InfoObjects.Load(communityID, null, objectType, udc.UserID);
        }
    }

    public class InfoObject
    {
        private int enuObjectType;
        private int intCount;

        internal InfoObject()
        {
        }

        public int Count
        {
            get { return intCount; }
            internal set { intCount = value; }
        }

        public int ObjectType
        {
            get { return enuObjectType; }
            internal set { enuObjectType = value; }
        }
    }
}