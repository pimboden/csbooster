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
using _4screen.CSB.Common;

namespace _4screen.CSB.Notification.Business
{
   public class User
   {
      private Guid userId = Guid.Empty;
      private string strNickname = string.Empty;
      private string strName = string.Empty;
      private string strFirstname = string.Empty;
      private CarrierList listCarrier = new CarrierList();
      private bool blnLoaded = false;

      private User()
      {
      }

      public User(Guid userID, string nickname)
      {
         FillMediumList();
         userId = userID;
         strNickname = nickname;
         blnLoaded = false;
      }

      private void FillMediumList()
      {
         foreach (CarrierType item in Enum.GetValues(typeof(CarrierType)))
         {
            Carrier objCarrier = new Carrier(listCarrier);
            objCarrier.Type = item;
            listCarrier.Add(objCarrier);
         }
      }

      public void Update()
      {
         Data.User objData = new Data.User();
         objData.Update(this);
      }

      public void Load()
      {
         Data.User objData = new Data.User();
         objData.Load(this);
      }

      public static void Delete(string userID)
      {
         Data.User objData = new Data.User();
         objData.Delete(userID);
      }

      internal bool Loaded
      {
         get { return blnLoaded; }
         set { blnLoaded = value; }
      }

      public CarrierList Carriers
      {
         get { return listCarrier; }
      }

      public string Firstname
      {
         get { return strFirstname; }
         set { strFirstname = value; }
      }

      public string Name
      {
         get { return strName; }
         set { strName = value; }
      }

      public string Nickname
      {
         get { return strNickname; }
         internal set { strNickname = value; }
      }

      public Guid UserID
      {
         get { return userId; }
         internal set { userId = value; }
      }
   }
}
