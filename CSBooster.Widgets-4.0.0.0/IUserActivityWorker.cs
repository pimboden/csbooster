// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IUserActivityWorker
    {
        UserActivity UserActivity { get; set; }
        string SplitterText { get; set; }
        string FolderParams { get; set; }
        string DateFormatString { get; set; }
    }
}