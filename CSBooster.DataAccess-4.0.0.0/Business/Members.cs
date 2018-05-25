// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;

namespace _4screen.CSB.DataAccess.Business
{
    public class Members
    {
        public static List<Member> Load(Guid communityID)
        {
            List<Member> list = new List<Member>();
            IDataReader idr = null;
            try
            {
                idr = SPs.HispCommunityUserLoad(communityID, false).GetReader();
                while (idr.Read())
                {
                    Member item = new Member();
                    item.FillMember(idr);
                    list.Add(item);
                }
            }
            catch
            {
            }
            finally
            {
                if (idr != null && !idr.IsClosed)
                {
                    idr.Close();
                }
            }
            return list;
        }

        public static bool Save(List<Member> members)
        {
            int count = 0;
            foreach (Member item in members)
            {
                if (item.IsOwner)
                    count++;
            }

            if (count < 1)
                return false;

            foreach (Member item in members)
            {
                item.SaveMember();
            }
            return true;
        }
    }

    public class Member
    {
        private bool _IsOwnerSave = false;

        public bool Dirty
        {
            get { return (_IsOwnerSave != _IsOwner); }
        }

        private Guid _CommunityID;

        public Guid CommunityID
        {
            get { return _CommunityID; }
        }

        private Guid _UserId;

        public Guid UserId
        {
            get { return _UserId; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
        }

        private bool _IsOwner;

        public bool IsOwner
        {
            get { return _IsOwner; }
            set { _IsOwner = value; }
        }

        public static void RemoveMember(Guid communityID, Guid userID)
        {
            SPs.HispCommunityUserRemoveMember(communityID, userID).Execute();
        }

        public Member()
        {
            _CommunityID = Guid.Empty;
            _UserId = Guid.Empty;
            _UserName = string.Empty;
            _IsOwner = false;
            _IsOwnerSave = false;
        }

        internal void SaveMember()
        {
            if (Dirty)
            {
                SPs.HispCommunityUserSetOwner(CommunityID, UserId, IsOwner).Execute();
                _IsOwnerSave = _IsOwner;
            }
        }

        internal void FillMember(IDataReader idr)
        {
            _CommunityID = new Guid(idr["CTY_ID"].ToString());
            _UserId = new Guid(idr["USR_ID"].ToString());
            _UserName = idr["USR_Nickname"].ToString();
            _IsOwner = Convert.ToBoolean(idr["CUR_IsOwner"]);
            _IsOwnerSave = _IsOwner;
        }
    }
}