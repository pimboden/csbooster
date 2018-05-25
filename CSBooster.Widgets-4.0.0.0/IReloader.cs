using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.Widget
{
    public interface IReloader
    {
        System.Web.UI.Control BrowsableControl { get; set; }

        bool FullReload { get; set; }

        int ObjectType { get; set; }
    }
}
