// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.Widget
{
    public interface ISingleObjectSelection
    {
        int ObjectType { get; set; }
        Guid? SelectedObject { get; set; }

        Guid ContainerId { get; set; }
    }
}