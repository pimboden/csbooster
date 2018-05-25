//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//             #1.2.0.0    23.01.2008 / PT   QuickLoad (SQL) anpassen / Objekttypen erweitert 
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using _4screen.CSB.Common;
using System.Reflection;
using System.Web;

namespace _4screen.CSB.DataAccess.Business
{
    public static class DataObjects
    {
        public static DataObjectList<DataObject> LoadByReflection(QuickParameters quickParameters)
        {
            string typeName = Helper.GetObjectType(quickParameters.ObjectType).Type;

            MethodInfo loadMethod = HttpRuntime.Cache["DataObjects.Load" + typeName] as MethodInfo;
            if (loadMethod == null)
            {
                Type type = null;
                if (!string.IsNullOrEmpty(Helper.GetObjectType(quickParameters.ObjectType).Assembly))
                {
                    Assembly assembly = Assembly.Load(Helper.GetObjectType(quickParameters.ObjectType).Assembly);
                    type = assembly.GetType(typeName);
                }
                else
                {
                    type = Type.GetType(typeName);
                }

                loadMethod = typeof(Data.DataObjects).GetMethod("Load", new Type[] { typeof(QuickParameters) });
                loadMethod = loadMethod.MakeGenericMethod(type);
                HttpRuntime.Cache.Insert("DataObjects.Load" + typeName, loadMethod, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);    
            }
            object list = loadMethod.Invoke(null, new object[] { quickParameters });

            MethodInfo method = HttpRuntime.Cache["DataObjectList.ConvertToListOfObjects" + typeName] as MethodInfo;
            if (method == null)
            {
                method = typeof(DataObjectList<DataObject>).GetMethod("ConvertToListOfObjects", BindingFlags.Static | BindingFlags.Public);
                Type listType = list.GetType().GetGenericArguments()[0];
                method = method.MakeGenericMethod(new[] { listType });
                HttpRuntime.Cache.Insert("DataObjectList.ConvertToListOfObjects" + typeName, method, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);    
            }
            return (DataObjectList<DataObject>)method.Invoke(null, new object[] { list });
        }

        public static DataObjectList<T> Load<T>(QuickParameters quickParameters) where T : Business.DataObject, new()
        {
            return Data.DataObjects.Load<T>(quickParameters);
        }
        public static DataObjectList<T> LoadByObjectID<T>(string[] objectID, QuickParameters quickParameters) where T : DataObject, new()
        {
            DataObjectList<T> list = new DataObjectList<T>();
            foreach (string strID in objectID)
            {
                if (strID.Length > 0 && strID.IsGuid())
                    list.Add(DataObject.Load<T>(strID.ToNullableGuid(), quickParameters.ShowState, false));
            }

            if (quickParameters.PageSize > 0)
            {
                int pageTotal = 0;
                list.ItemTotal = list.Count;
                list = DoPaging<T>(list, quickParameters.PageNumber, quickParameters.PageSize, out pageTotal);
                list.PageTotal = pageTotal;
            }

            return list;
        }

        private static DataObjectList<T> DoPaging<T>(DataObjectList<T> list, int pageNumber, int pageSize, out int pageTotal) where T : Business.DataObject, new()
        {
            DataObjectList<T> newList = new DataObjectList<T>(pageSize);

            pageTotal = list.Count / pageSize;
            if (list.Count > pageSize * pageTotal)
                pageTotal++;

            if (pageNumber > pageTotal)
                pageNumber = pageTotal;

            int intFrom = 0;
            if (list.Count > 0 && pageNumber > 0 && pageSize > 0)
                intFrom = (pageNumber - 1) * pageSize;

            for (int i = intFrom; i < list.Count; i++)
            {
                if (newList.Count == pageSize)
                    break;

                newList.Add(list[i]);
            }

            return newList;
        }

    }
}