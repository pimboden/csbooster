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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;   


namespace _4screen.CSB.Notification.Business
{
   public class RegistrationDefaultList
   {
      private User objUser;
      private Dictionary<EventIdentifier, RegistrationDefault> list = new Dictionary<EventIdentifier, RegistrationDefault>();
      private string strRootFolder;
      internal Guid userId;
      private string[] strRoles;

      public RegistrationDefaultList(Common.UserDataContext udc, string rootFolder)
      {
         strRootFolder = rootFolder;
         userId = udc.UserID;
         strRoles = udc.UserRoles;
         objUser = new User(userId, string.Empty);
      }

      public RegistrationDefaultList(Guid userID, string[] roles, string rootFolder)
      {
         strRootFolder = rootFolder;
         userId = userID;
         strRoles = roles;
         objUser = new User(userId, string.Empty);
      }

      internal void Add(RegistrationDefault item)
      {
         list.Add(item.Identifier, item);
      }

      public void Save()
      {
         Data.RegistrationDefault objData = new Data.RegistrationDefault();
         objData.Save(this);
      }

      public void Load()
      {
         ConfigurationList listConfig = new ConfigurationList();
         listConfig.Load(strRootFolder);
         objUser.Load();

         foreach (KeyValuePair<EventIdentifier, Configuration> kvp in listConfig)
         {
            Configuration itemConfig = kvp.Value;
            //if (!itemConfig.IsEventAvailably(objectType, global))
            //   continue;

            Role itemRole = itemConfig.FindRole(strRoles);
            if (itemRole == null)
               continue;

            //if (itemRole.Range == ObjectRange.MyObject && userId != objectUserID)
            //   continue;

            RegistrationDefault itemReg = new RegistrationDefault();
            itemReg.Identifier = itemConfig.Identifier;
            foreach (CarrierType soll in itemRole.CarrierTypes)
            {
               Carrier itemCarr = new Carrier(itemReg.Carriers);
               itemCarr.Type = soll;
               itemCarr.Availably = objUser.Carriers.Item(soll).IsValid;
               itemReg.Carriers.Add(itemCarr);
            }

            this.Add(itemReg);
         }

         Data.RegistrationDefault objData = new Data.RegistrationDefault();
         objData.Load(this);
      }

      public RegistrationDefault Item(EventIdentifier key)
      {
         if (list.ContainsKey(key))
            return list[key];
         else
            return null;
      }

      public IEnumerator<RegistrationDefault> GetEnumerator()
      {
         foreach (EventIdentifier key in list.Keys)
         {
            yield return (list[key]);
         }
      }

   }
}
