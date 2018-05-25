// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Business
{
    public class FilterEngine
    {
        /// <summary>
        /// Filters a string for bad words
        /// </summary>
        /// <param name="value">The string, that should be filtered</param>
        /// <param name="type">The type of object the string belongs to</param>
        /// <param name="objectId">The related object id</param>
        /// <param name="userId">The related user id</param>
        /// <returns></returns>
        public static string FilterStringBadWords(string value, Data.FilterObjectTypes type, Guid objectId, Guid userId)
        {
            return Data.FilterEngine.FilterStringBadWords(value, type, objectId, userId);
        }

        /// <summary>
        /// Filters a DataObject for bad words and ad words (configured in FilterEngine.config)
        /// </summary>
        /// <param name="item">The DataObject, that should be filtered</param>
        public static void FilterObject(Business.DataObject item)
        {
            Data.FilterEngine.FilterObject(item);
        }
    }
}