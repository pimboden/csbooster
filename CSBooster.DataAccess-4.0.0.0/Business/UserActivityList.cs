// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserActivityList<T> : List<T>
    {
        public UserActivityList(Business.UserActivityParameters paras) : base(paras.InitialCapacity)
        {
            amount = paras.Amount;
        }

        public UserActivityList(int capacity)
            : base(capacity)
        {
        }

        public UserActivityList()
            : base()
        {
        }

        private DateTime? cacheInserted = null;
        //private int pageTotal = 0;
        //private int itemTotal = 0;
        //private int pageSize = 0;
        //private int pageNumber = 0;
        private int amount = 0;

        public int Amount
        {
            get { return amount; }
            internal set { amount = value; }
        }

        //public int PageNumber
        //{
        //    get { return pageNumber; }
        //    internal set { pageNumber = value; }
        //}

        //public int PageSize
        //{
        //    get { return pageSize; }
        //    internal set { pageSize = value; }
        //}

        //public int ItemTotal
        //{
        //    get { return itemTotal; }
        //    internal set { itemTotal = value; }
        //}

        //public int PageTotal
        //{
        //    get { return pageTotal; }
        //    internal set { pageTotal = value; }
        //}

        public DateTime CacheInserted
        {
            get
            {
                if (cacheInserted == null)
                    return DateTime.Now;
                else
                    return cacheInserted.Value;
            }
            internal set { cacheInserted = value; }
        }

        public bool IsFromCache
        {
            get
            {
                if (cacheInserted != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
