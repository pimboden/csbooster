// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Specialized;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserActivityParameters
    {
        // With corresponding query keys
        private UserDataContext udc = null;
        private int objectType = 0;
        private Guid? objectID = null;
        private int amount = 0;
        //private int pageNumber = 1;
        //private int pageSize = 10;
        //private int pageTotal = 0;
        private int itemTotal = 0;
        private bool? ignoreCache = null;
        private bool withAdminData = false;
        private UserActivityType? userActivityType = null;

        public void FromNameValueCollection(NameValueCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["OT"]))
                ObjectType = Helper.GetObjectTypeNumericID(collection["OT"]);

            if (!string.IsNullOrEmpty(collection["OID"]))
                ObjectID = collection["OID"].ToGuid();

            if (!string.IsNullOrEmpty(collection["AM"]))
                int.TryParse(collection["AM"], out amount);

            //if (!string.IsNullOrEmpty(collection["PN"]))
            //    int.TryParse(collection["PN"], out pageNumber);

            //if (!string.IsNullOrEmpty(collection["PS"]))
            //{
            //    int.TryParse(collection["PS"], out pageSize);
            //    pageSize = Math.Min(pageSize, 100);
            //}

            //if (!string.IsNullOrEmpty(collection["IO"]))
            //{
            //   bool onlyVisibleForAdmin;
            //   if (bool.TryParse(collection["IO"], out onlyVisibleForAdmin))
            //      this.onlyVisibleForAdmin = onlyVisibleForAdmin;
            //}

            if (!string.IsNullOrEmpty(collection["IC"]))
            {
                bool boolVal;
                if (bool.TryParse(collection["IC"], out boolVal))
                    IgnoreCache = boolVal;
            }
        }

        public bool WithAdminData
        {
            get { return withAdminData; }
            set { withAdminData = value; }
        }

        public UserDataContext Udc
        {
            get { return udc; }
            set { udc = value; }
        }

        public int ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        public Guid? ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }

        public UserActivityType? UserActivityType
        {
            get { return userActivityType; }
            set { userActivityType = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        //public int PageNumber
        //{
        //    get { return pageNumber; }
        //    set { pageNumber = value; }
        //}

        //public int PageSize
        //{
        //    get { return pageSize; }
        //    set { pageSize = value; }
        //}

        public bool? IgnoreCache
        {
            get { return ignoreCache; }
            set { ignoreCache = value; }
        }

        //internal int PageTotal
        //{
        //    get { return pageTotal; }
        //    set { pageTotal = value; }
        //}

        public int ItemTotal
        {
            get { return itemTotal; }
            internal set { itemTotal = value; }
        }

        internal int InitialCapacity
        {
            get
            {
                if (amount > 25)
                    return amount;
                else
                    return 25;

                //if (amount > 0 && amount < pageSize)
                //    return amount;
                //else if (pageSize > 0)
                //    return pageSize;
                //else
                //    return 25;
            }
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

    }
}