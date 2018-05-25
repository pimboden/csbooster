// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal static class DataObjects
    {
        public static Business.DataObjectList<T> Load<T>(Business.QuickParameters paras) where T : Business.DataObject, new()
        {
            if (!paras.Amount.HasValue)
            {
                if (paras.ObjectType > 0)
                    paras.Amount = Helper.GetObjectType(paras.ObjectType).DefaultLoadAmount;
                else
                {
                    T type = new T();
                    if (type.ObjectType > 0)
                        paras.Amount = Helper.GetObjectType(type.ObjectType).DefaultLoadAmount;
                    else
                        paras.Amount = 1000;
                }
            }

            QuickCacheHandler cacheHandler = new QuickCacheHandler(paras, typeof(T).ToString());
            Business.DataObjectList<T> list = (Business.DataObjectList<T>)cacheHandler.Get();
            if (list == null)
            {
                list = new Business.DataObjectList<T>(paras);

                SqlDataReader sqlReader = null;
                try
                {
                    if (paras is Business.QuickParametersFriends)
                    {
                        sqlReader = DataObjectsHelper.GetReaderAllFriends((Business.QuickParametersFriends)paras);
                    }
                    else if (paras is Business.QuickParametersUser && ((Business.QuickParametersUser)paras).ForObjectType.HasValue)
                    {
                        sqlReader = DataObjectsHelper.GetReaderAllBest((Business.QuickParametersUser)paras);
                    }
                    else if (paras is Business.QuickParametersUser && ((Business.QuickParametersUser)paras).LoadVisits.HasValue)
                    {
                        sqlReader = DataObjectsHelper.GetReaderAllVisits((Business.QuickParametersUser)paras);
                    }
                    else if (paras is Business.QuickParametersTag)
                    {
                        sqlReader = DataObjectsHelper.GetReaderAllTags((Business.QuickParametersTag)paras);
                    }
                    else
                    {
                        sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, null);
                    }

                    int rank = 0;
                    list.PageTotal = paras.PageTotal;
                    list.ItemTotal = paras.ItemTotal;
                    list.PageNumber = paras.PageNumber;

                    while (sqlReader.Read())
                    {
                        rank++;
                        T item = new T();
                        item.FillObject(sqlReader);
                        list.Add(item);
                    }

                    if (rank > 0 && list.ItemTotal == 0 && paras.DisablePaging.HasValue && paras.DisablePaging.Value)
                    {
                        list.PageTotal = 1;
                        list.ItemTotal = rank; ;
                        list.PageNumber = 1;
                    }
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }
                    sqlReader = null;

                    if (!string.IsNullOrEmpty(paras.GeneralSearch) && list.PageNumber == 1)
                    {
                        _4screen.CSB.Extensions.Business.TrackingManager.TrackEventSearch(paras.GeneralSearch, list.ItemTotal, paras.ObjectType);
                    }

                }
                cacheHandler.Insert(list);

            }

            return list;
        }
    }
}