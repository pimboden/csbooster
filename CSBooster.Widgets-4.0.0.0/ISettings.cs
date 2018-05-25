// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;

namespace _4screen.CSB.Widget
{
    public interface ISettings
    {
        Dictionary<string, object> Settings { get; set; }
    }
}
