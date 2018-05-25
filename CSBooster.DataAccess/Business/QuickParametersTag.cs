using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class QuickParametersTag : QuickParameters
    {
        public Guid? RelatedUserID { get; set; }
        public Guid? RelatedCommunityID { get; set; }
        public int? RelatedObjectType { get; set; }
        public QuickTagCloudRelevance Relevance { get; set; }
        public QuickTagCloudRelevanceGroup RelevanceGroup { get; set; }
        public int? RelevanceGroupAmount { get; set; }

        public QuickParametersTag()
            : base()
        {
            this.Relevance = QuickTagCloudRelevance.NoRelevance;
            this.RelevanceGroup = QuickTagCloudRelevanceGroup.All;
            this.ObjectType = Helper.GetObjectTypeNumericID("Tag");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            sb.Append("TGW");

            sb.AppendFormat("R{0}", (int)Relevance);
            sb.AppendFormat("G{0}", (int)RelevanceGroup);

            if (RelevanceGroupAmount.HasValue)
                sb.AppendFormat("GA{0}", RelevanceGroupAmount.Value);

            if (RelatedUserID.HasValue)
                sb.AppendFormat("RU{0}", RelatedUserID.Value);

            if (RelatedCommunityID.HasValue)
                sb.AppendFormat("RC{0}", RelatedCommunityID.Value);

            if (RelatedObjectType.HasValue)
                sb.AppendFormat("RT{0}", RelatedObjectType.Value);

            return sb.ToString();
        }
    }
}
