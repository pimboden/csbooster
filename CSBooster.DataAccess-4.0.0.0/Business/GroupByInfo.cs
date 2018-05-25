// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Business
{
    public class GroupByInfo
    {
        public Guid ObjectID { get; set; }
        public int ObjectType { get; set; }
        public string Title { get; set; }

    }
    public class GroupByInfoComparer : IEqualityComparer<GroupByInfo>
    {
        public bool Equals(GroupByInfo x, GroupByInfo y)
        {
            return x.ObjectID == y.ObjectID;
        }

        public int GetHashCode(GroupByInfo obj)
        {
            return obj.ObjectID.GetHashCode();
        }
    }
}
