// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public class DialogEngine
    {
        /// <summary>
        /// Get all dialogs matching the given page name
        /// </summary>
        /// <param name="pageName">A unique page name</param>
        /// <param name="userId">A unique user id</param>
        /// <returns>A dialog list</returns>
        public static List<Dialog> GetDialogByPageName(List<string> pageNames, Guid userId)
        {
            return Data.DialogEngine.GetDialogByPageName(pageNames, userId);
        }
    }
}