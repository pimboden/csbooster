// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************

using System;
using System.Collections.Specialized;
using System.Text;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class QuickParametersUser : QuickParameters
    {
        #region Properties
        public bool? IsOnline{ get; set; }
        public string Vorname { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string RelationStatus { get; set; }
        public string AttractedTo { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public int? BodyHeightFrom { get; set; }
        public int? BodyHeightTo{ get; set; }
        public int? BodyWeightFrom { get; set; }
        public int? BodyWeightTo { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public Guid? CommunityIDMember { get; set; }
        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string AddressLand { get; set; }
        public int? AddressRangeKM { get; set; }
        public string InterestTopic { get; set; }
        public string Interesst { get; set; }
        public string Talent { get; set; }
        public bool? IsUserLocked { get; set; }
        public int? ForObjectType { get; set; }
        public bool? LoadVisits { get; set; }
        #endregion

        #region Constructors
        #endregion
        public QuickParametersUser()
            : base()
        {
            this.ObjectType = Helper.GetObjectTypeNumericID("User");
        }


        public override void FromNameValueCollection(NameValueCollection collection)
        {
            base.FromNameValueCollection(collection);
            if (!string.IsNullOrEmpty(collection["IO"]))
            {
               bool isOnline;
                if (bool.TryParse(collection["IO"], out isOnline))
                    this.IsOnline = isOnline;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            sb.Append("USR");

            if (IsOnline.HasValue)
                sb.AppendFormat("-A{0}", IsOnline.Value ? "1" : "0");

            if (!string.IsNullOrEmpty(Vorname))
                sb.AppendFormat("-B{0}", Vorname.ToLower());

            if (!string.IsNullOrEmpty(Name))
                sb.AppendFormat("-C{0}", Name.ToLower());

            if (!string.IsNullOrEmpty(Sex))
                sb.AppendFormat("-D{0}", Sex.ToLower());

            if (!string.IsNullOrEmpty(RelationStatus))
                sb.AppendFormat("-E{0}", RelationStatus.ToLower());

            if (!string.IsNullOrEmpty(AttractedTo))
                sb.AppendFormat("-F{0}", AttractedTo.ToLower());

            if (!string.IsNullOrEmpty(EyeColor))
                sb.AppendFormat("-G{0}", EyeColor.ToLower());

            if (!string.IsNullOrEmpty(HairColor))
                sb.AppendFormat("-H{0}", HairColor.ToLower());

            if (BodyHeightFrom.HasValue)
                sb.AppendFormat("-I{0}", BodyHeightFrom.Value);

            if (BodyHeightTo.HasValue)
                sb.AppendFormat("-J{0}", BodyHeightTo.Value);

            if (BodyWeightFrom.HasValue)
                sb.AppendFormat("-K{0}", BodyWeightFrom.Value);

            if (BodyWeightTo.HasValue)
                sb.AppendFormat("-L{0}", BodyWeightTo.Value);

            if (AgeFrom.HasValue)
                sb.AppendFormat("-M{0}", AgeFrom.Value);

            if (AgeTo.HasValue)
                sb.AppendFormat("-N{0}", AgeTo.Value);

            if (CommunityIDMember.HasValue)
                sb.AppendFormat("-O{0}", CommunityIDMember.Value);

            if (!string.IsNullOrEmpty(AddressCity))
                sb.AppendFormat("-P{0}", AddressCity.ToLower());

            if (!string.IsNullOrEmpty(AddressZip))
                sb.AppendFormat("-Q{0}", AddressZip.ToLower());

            if (!string.IsNullOrEmpty(AddressLand))
                sb.AppendFormat("-R0}", AddressLand.ToLower());

            if (AddressRangeKM.HasValue)
                sb.AppendFormat("-S{0}", AddressRangeKM.Value);

            if (!string.IsNullOrEmpty(InterestTopic))
                sb.AppendFormat("-T0}", InterestTopic.ToLower());

            if (!string.IsNullOrEmpty(Interesst))
                sb.AppendFormat("-U0}", Interesst.ToLower());

            if (!string.IsNullOrEmpty(Talent))
                sb.AppendFormat("-V0}", Talent.ToLower());

            if (ForObjectType.HasValue)
                sb.AppendFormat("-W{0}", ForObjectType.Value);

            if (LoadVisits.HasValue)
                sb.AppendFormat("-X{0}", LoadVisits.Value ? "1" : "0");

            return sb.ToString();
        }
    }
}
