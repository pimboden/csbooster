using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
