// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;

namespace _4screen.CSB.DataAccess.Business
{
    [Serializable]
    public class MembershipParams
    {
        public Guid? CommunityID { get; set; }

        public Guid? UserID { get; set; }
        
        public bool? IsOwner { get; set; }

        public bool? IsCreator { get; set; }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(38);

            if (CommunityID.HasValue)
                sb.AppendFormat("C{0}", CommunityID.Value);

            if (UserID.HasValue)
                sb.AppendFormat("U{0}", UserID.Value);

            if (IsOwner.HasValue)
                sb.AppendFormat("IO{0}", IsOwner.Value ? "1" : "0");

            if (IsCreator.HasValue)
                sb.AppendFormat("IC{0}", IsCreator.Value ? "1" : "0");

            return sb.ToString();
        }
    }
}
