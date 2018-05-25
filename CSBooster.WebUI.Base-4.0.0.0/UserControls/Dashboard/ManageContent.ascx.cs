// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ManageContent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            myContent.ShowState = null;
            myContent.AccessType = DataAccessType.AccessByObjectType;
            myContent.Sort = CustomizationSection.CachedInstance.MyContent.DefaultSortOrder;
            myContent.SortDirection = myContent.Sort == QuickSort.Title ? QuickSortDirection.Asc : QuickSortDirection.Desc;
            myContent.MyContentMode = MyContentMode.Admin;

            myContentSearch.MyContentMode = MyContentMode.Admin;
            myContentSearch.MyContent = myContent;
        }
    }
}