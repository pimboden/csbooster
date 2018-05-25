// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IObjectClicker
    {
        ClickerQueryType QueryType { get; set; }

        QuickSort SortOrder { get; set; }

        int ObjType { get; set; }

        int PageSize { get; set; }

        Guid? CommunityID { get; set; }

        Guid? UserID { get; set; }

        string Communities { get; set; }

        string TagWords1 { get; set; }

        string TagWords2 { get; set; }

        string TagWords3 { get; set; }

        bool IncludeGroups { get; set; }

        bool ShowAuthor { get; set; }

        bool ShowTitle { get; set; }

        bool HasContent { get; set; }

        DataObjectList<DataObject> GetSiblings(int currentPage, int pageSize, bool blnIgnoreCache);
    }
}