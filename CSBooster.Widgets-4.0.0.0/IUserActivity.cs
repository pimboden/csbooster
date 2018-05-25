// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IUserActivity
    {
        UserActivityParameters UserActivityParameters { get; set; }
        bool HasContent { get; set; }
        string OutputTemplate { get; set; }
    }
}