// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class AdWordFilterJob
    {
        public void ProcessDataObjectsAll()
        {
            Data.AdWordFilterJob adWordFilterJob = new Data.AdWordFilterJob();
            adWordFilterJob.ProcessDataObjectsAll(UserDataContext.GetUserDataContext("admin"));
            adWordFilterJob = null;
        }

        public void ProcessDataObjectsForUser(Guid userId)
        {
            Data.AdWordFilterJob adWordFilterJob = new Data.AdWordFilterJob();
            adWordFilterJob.ProcessDataObjectsForUser(UserDataContext.GetUserDataContext("admin"), userId);
            adWordFilterJob = null;
        }
    }
}