//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                22.08.2007 16:00:25 / aw
//  Updated:   
//******************************************************************************

using System;

namespace _4screen.CSB.DataAccess.Business
{
    public class AdWordFilterJob
    {
        public void ProcessDataObjectsAll()
        {
            Data.AdWordFilterJob adWordFilterJob = new Data.AdWordFilterJob();
            adWordFilterJob.ProcessDataObjectsAll();
            adWordFilterJob = null;
        }

        public void ProcessDataObjectsForUser(Guid userId)
        {
            Data.AdWordFilterJob adWordFilterJob = new Data.AdWordFilterJob();
            adWordFilterJob.ProcessDataObjectsForUser(userId);
            adWordFilterJob = null;
        }
    }
}