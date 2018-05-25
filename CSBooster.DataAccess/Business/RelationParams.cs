//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		06.08.2008 / PI
//******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class RelationParams
    {
        public UserDataContext Udc { get; set; }

        public RelationSortType SortType { get; set; }

        public Guid? ParentObjectID { get; set; }

        public int? ParentObjectType { get; set; }

        public Guid? ChildObjectID { get; set; }

        public int? ChildObjectType { get; set; }

        public string RelationType { get; set; }

        public bool ExcludeSystemObjects { get; set; } // ExcludeTypen:  5=Tag,

        public QuickSort GroupSort { get; set; }

        public QuickSortDirection GroupSortDirection { get; set; }

        public Guid? ParentUserId { get; set; }

        public Guid? ParentCommunityId { get; set; }

        public ObjectShowState? ParentShowState{ get; set; }

        public RelationParams()
        {
            ExcludeSystemObjects = true;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(50);

            if (ParentObjectID.HasValue)
                sb.AppendFormat("PO{0}", ParentObjectID.Value);

            if (ParentObjectType.HasValue)
                sb.AppendFormat("PT{0}", ParentObjectType.Value);

            if (ChildObjectID.HasValue)
                sb.AppendFormat("CO{0}", ChildObjectID.Value);

            if (ChildObjectType.HasValue)
                sb.AppendFormat("CT{0}", ChildObjectType.Value);

            if (!string.IsNullOrEmpty(RelationType))
                sb.AppendFormat("RT{0}", RelationType.ToLower());

            if (ExcludeSystemObjects)
                sb.Append("ESO");

            sb.AppendFormat("ST{0}", (int)SortType);

            return sb.ToString();
        }
    }
}