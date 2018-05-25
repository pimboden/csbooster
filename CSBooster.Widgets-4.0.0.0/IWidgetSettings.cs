// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IWidgetSettings
    {
        DataObject ParentDataObject { get; set; }

        Guid InstanceId { get; set; }

        bool Save();
    }
}
