using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
