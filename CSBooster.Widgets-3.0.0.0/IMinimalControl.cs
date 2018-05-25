using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.Widget
{
    //This interface is implemented By simple Controls that don't need to much info
    public interface IMinimalControl
    {
        string Prop1 { get; set; }
        string Prop2 { get; set; }
        bool HasContent { get; set; }
    }
}
