// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IForumDetails
    {
        DataObject DataObject { get; set; }
        int PageSize { get; set; }
        int PagerBreak { get; set; }
        bool ShowTopicColumn { get; set; }
        bool ShowStarterColumn { get; set; }
        bool ShowInfoColumn { get; set; }
        bool ShowLastPosterColumn { get; set; }
    }
}