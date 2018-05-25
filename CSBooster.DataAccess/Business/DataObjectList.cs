//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public class DataObjectList<T> : List<T> where T : Business.DataObject, new()
    {
        public DataObjectList(Business.QuickParameters paras)
            : base(50)
        {
            pageSize = paras.PageSize;
            pageNumber = paras.PageNumber;
            if (paras.Amount.HasValue)   
                amount = paras.Amount.Value;
        }

        public DataObjectList(int capacity)
            : base(capacity)
        {
        }

        public DataObjectList()
            : base()
        {
        }

        private DateTime? cacheInserted = null;
        private int pageTotal = 0;
        private int itemTotal = 0;
        private int pageSize = 0;
        private int pageNumber = 0;
        private int amount = 0;

        public int Amount
        {
            get { return amount; }
            internal set { amount = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            internal set { pageNumber = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            internal set { pageSize = value; }
        }

        public int ItemTotal
        {
            get { return itemTotal; }
            internal set { itemTotal = value; }
        }

        public int PageTotal
        {
            get { return pageTotal; }
            internal set { pageTotal = value; }
        }

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

        public DataObjectList<T> Clone(bool deep)
        {
            if (deep)
            {
                throw new NotImplementedException();
            }
            else
            {
                DataObjectList<T> list = new DataObjectList<T>();
                list.Amount = amount;
                list.PageNumber = pageNumber;
                list.PageSize = pageSize;
                list.ItemTotal = itemTotal;
                list.PageTotal = pageTotal;
                if (cacheInserted.HasValue)
                    list.CacheInserted = (DateTime)cacheInserted;
                list.AddRange(this);
                return list;
            }
        }

        public new DataObjectList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) where TOutput : Business.DataObject, new()
        {
            DataObjectList<TOutput> list = new DataObjectList<TOutput>();
            list.Amount = amount;
            list.PageNumber = pageNumber;
            list.PageSize = pageSize;
            list.ItemTotal = itemTotal;
            list.PageTotal = pageTotal;
            if (cacheInserted.HasValue)
                list.CacheInserted = (DateTime)cacheInserted;
            for (int i = 0; i < base.Count; i++)
            {
                list.Add(converter(this[i]));
            }
            return list;
        }

        public static DataObjectList<DataObject> ConvertToListOfObjects<T>(DataObjectList<T> list) where T : DataObject, new()
        {
            return list.ConvertAll<DataObject>(t => t);
        }

    }
}