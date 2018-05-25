// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IRepeater
    {
        bool BottomPagerVisible { get; set; }

        bool TopPagerVisible { get; set; }

        string TopPagerCustomText { get; set; }

        string BottomPagerCustomText { get; set; }

        int PagerBreak { get; set; }

        QuickParameters QuickParameters { get; set; }

        string Title { get; set; }

        bool HasContent { get; set; }

        string ItemNameSingular { get; set; }

        string ItemNamePlural { get; set; }

        string OutputTemplate { get; set; }

        bool RenderHtml { get; set; }
    }
}