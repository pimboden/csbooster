using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabContent : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<SiteObjectType> objectTypes = Helper.GetActiveUserContentObjectTypes(false);

            MyContent.PageSize = 18;
            MyContent.Sort = CustomizationSection.CachedInstance.MyContent.DefaultSortOrder;
            MyContent.SortDirection = MyContent.Sort == QuickSort.Title ? QuickSortDirection.Asc : QuickSortDirection.Desc;
            MyContent.MyContentMode = MyContentMode.Widgets;
            MyContent.Settings = new Dictionary<string, object>();

            var objectType = objectTypes.Find(x => x.DetailWidgetId != Guid.Empty);
            if (objectType != null)
            {
                MyContentSearch.ObjectType = objectType.NumericId;
                MyContent.ObjectType = objectType.NumericId;
            }

            MyContentSearch.MyContentMode = MyContentMode.Widgets;
            MyContentSearch.MyContent = MyContent;
        }

        protected void OnCloseClick(object sender, EventArgs e)
        {
            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit", "tab" }, true);
            Response.Redirect(string.Format("{0}?{1}", Request.GetRawPath(), filteredQueryString));
        }
    }
}