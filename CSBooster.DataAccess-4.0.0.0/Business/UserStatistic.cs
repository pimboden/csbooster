using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4screen.CSB.DataAccess.Business
{
    public class UserStatistic
    {
        public static UserStatisticList GetUserStatisticList(string nickName, DateTime fromDate, DateTime toDate, bool allUser, List<UserStatisticAction> includeAction)
        {
            return Data.UserStatistic.GetUserStatisticList(nickName, fromDate, toDate, allUser, includeAction);
        }

        public static List<UserStatisticActivity> GetUserActivityList(string nickName, DateTime fromDate, DateTime toDate, List<UserStatisticAction> includeAction)
        {
            return Data.UserStatistic.GetUserActivityList(nickName, fromDate, toDate, includeAction);
        }
    }
}
