// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI.WebControls;

namespace _4screen.CSB.Widget
{
    public interface IObjectOverview : IRepeater
    {
        RepeatLayout RepeaterLayout { get; set; }

        int ItemsPerRow { get; set; }
    }
}