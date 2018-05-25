// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.Widget
{
    public interface IObjectsToObjectRelator
    {
        Guid? ParentObjectID { get; set; }
		List<string> ChildObjectTypes { get; set; }
        Guid? UserId { get; set; }
        int MaxChildObjects { get; set; }
        string RelationType { get; set; }
        bool ExcludeSystemObjects{ get; set; }
        bool Save();
    }
}
