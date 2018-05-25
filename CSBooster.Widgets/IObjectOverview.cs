using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IObjectOverview : IRepeater
    {
        RepeatLayout RepeaterLayout { get; set; }

        int ItemsPerRow { get; set; }
    }
}