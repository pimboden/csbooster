using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class QuickParametersFriends : QuickParametersUser
    {
        #region Properties
        
        public Guid? CurrentUserID { get; set; }
        public bool? OnlyNotBlocked { get; set; }
        public int? FriendType { get; set; }
        public string FriendSearchParam { get; set; }

        #endregion

        #region Constructors
        #endregion

        public QuickParametersFriends()
            : base()
        {
            this.ObjectType = Helper.GetObjectTypeNumericID("User");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            sb.Append("FRI");

            if (CurrentUserID.HasValue)
                sb.AppendFormat("U{0}", CurrentUserID.Value);

            if (OnlyNotBlocked.HasValue)
                sb.AppendFormat("B{0}", OnlyNotBlocked.Value ? "1" : "0");

            if (FriendType.HasValue)
                sb.AppendFormat("T{0}", FriendType.Value);

            if (!string.IsNullOrEmpty(FriendSearchParam))
                sb.AppendFormat("S{0}", FriendSearchParam.ToLower());

            return sb.ToString();
        }
    }
}
