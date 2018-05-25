using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4screen.CSB.Common;
using System.Xml;
using System.Data.SqlClient;

namespace _4screen.CSB.DataAccess.Business
{
    public class DataObjectFriend: DataObjectUser
    {
        #region Properties
        public bool? Blocked;
        public int? FriendTypeID;
        public int? AllowBirthdayNotification;
        #endregion

        #region Constructors
        #endregion

        public DataObjectFriend()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectFriend(UserDataContext userDataContext)
            : base(userDataContext)
        {
        }

        #region Read / Write Methods
        #endregion

        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectFriend.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetSelectSQL((QuickParametersFriends)qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetJoinSQL((QuickParametersFriends)qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetWhereSQL((QuickParametersFriends)qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetFullTextWhereSQL((QuickParametersFriends)qParas, parameters);
        }


        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectFriend.GetOrderBySQL((QuickParametersFriends)qParas, parameters);
        }

    }
    public class NicknameSorterFriend : IComparer<DataObjectFriend>
    {
        private bool blnDesc = false;

        public NicknameSorterFriend(bool desc)
        {
            blnDesc = desc;
        }

        public NicknameSorterFriend()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectFriend x, DataObjectFriend y)
        {
            if (!blnDesc)
                return StringLogicalComparer.Compare(x.Nickname, y.Nickname);

            else
                return StringLogicalComparer.Compare(y.Nickname, x.Nickname);
        }
    }
}
