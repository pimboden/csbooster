//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//  Updated:   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace _4screen.CSB.Notification.Business
{
   public class Notification
   {
      public void AllocateNotification()
      {
         Data.Notification objData = new Data.Notification(string.Empty);
         objData.AllocateNotification();
      }

      public int CreateNotification()
      {
         Data.Notification objData = new Data.Notification(_4screen.CSB.Common.WebRootPath.Instance.ToString());
         return objData.GetPending();
      }

      public int SendNotification()
      {
         Data.Notification objData = new Data.Notification(string.Empty);
         return objData.SendNotification();
      }

      public void CheckBirthdayRegistration()
      {
         Data.Notification objData = new Data.Notification(string.Empty);
         objData.CheckBirthdayRegistration(); 
      }
   }
}
