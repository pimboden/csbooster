// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class GroupByTest : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        protected void Page_Load(object sender, EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            QuickParameters quickParameters = new QuickParameters();
            quickParameters.Udc = UserDataContext.GetUserDataContext();
            quickParameters.ObjectType = 5;
            quickParameters.IgnoreCache = true;
            quickParameters.PageSize = 9999999;
            quickParameters.DisablePaging = true;
            quickParameters.SortBy = QuickSort.Title;
            quickParameters.Direction = QuickSortDirection.Asc;
            quickParameters.RelationParams = new RelationParams { ExcludeSystemObjects = false, RelationType = "Synonym", ParentObjectType = 5, GroupSort = QuickSort.Title, GroupSortDirection = QuickSortDirection.Asc };
            DataObjectList<DataObjectTag> tags = DataObjects.Load<DataObjectTag>(quickParameters);
            string strOut = "<h1>ParentObjectType = 5</h1><table>";
            foreach (var tag in tags)
            {
                strOut += string.Format("<tr><td>HauptSnonyme</td><td>{0}</td><td> Synonyme {1}</td></tr>", tag.GroupByInfo != null ? tag.GroupByInfo.Title : "N/A", tag.Title);


            }
             lt.Text = strOut + "</table>";
             try
             {
                 strOut = "<h1>ParentObjectType = 5 (LINQ)</h1><table>";
                 tags = DataObjects.Load<DataObjectTag>(quickParameters);
                 GroupByInfoComparer comp = new GroupByInfoComparer();
                 IEnumerable<IGrouping<GroupByInfo, DataObjectTag>> outerSquence = tags.GroupBy(x => x.GroupByInfo, comp);
                 foreach (var keyGroupSequence in outerSquence)
                 {
                     strOut += string.Format("<tr><td>{0}</td><td>{1}</td><td> {2} ", language.GetString("LableGroupBySynonymMaster"), keyGroupSequence.Key.Title, language.GetString("LableGroupBySynonym"));
                     foreach (DataObjectTag DataObjectTag in keyGroupSequence)
                     {
                         strOut += DataObjectTag.Title + "<br/>";
                     }
                     strOut += string.Format("</td><td>Total </td><td>{0}</td> ", keyGroupSequence.Count());
                     strOut += string.Format("</tr>");


                 }
                 lt.Text += strOut + "</table>";
             }
            catch
            {
                
            }
            quickParameters.RelationParams = new RelationParams { ExcludeSystemObjects = false, RelationType = "Synonym", ChildObjectType = 5, GroupSort = QuickSort.Title, GroupSortDirection = QuickSortDirection.Asc };
            tags = DataObjects.Load<DataObjectTag>(quickParameters);
            strOut = "<h1>ChildObjectType = 5 </h1><table>";
            foreach (var tag in tags)
            {
                strOut += string.Format("<tr><td>{0}</td><td>{1}</td><td> {2} {3}</td></tr>", language.GetString("LableGroupBySynonymMaster"), tag.GroupByInfo != null ? tag.GroupByInfo.Title : "N/A", language.GetString("LableGroupBySynonym"), tag.Title);


            }
            lt.Text += strOut + "</table>";

            quickParameters.RelationParams = new RelationParams { ExcludeSystemObjects = false, RelationType = "Synonym", ParentObjectType = 5, ChildObjectType = 5, GroupSort = QuickSort.Title, GroupSortDirection = QuickSortDirection.Asc };
            tags = DataObjects.Load<DataObjectTag>(quickParameters);
            strOut = "<h1>ParentObjectType = 5 ChildObjectType = 5</h1><table>";
            foreach (var tag in tags)
            {
                strOut += string.Format("<tr><td>{0}</td><td>{1}</td><td> {2} {3}</td></tr>", language.GetString("LableGroupBySynonymMaster"), tag.GroupByInfo != null ? tag.GroupByInfo.Title : "N/A", language.GetString("LableGroupBySynonym"), tag.Title);


            }
            lt.Text += strOut + "</table>";

        }
    }
}
