//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;   


namespace _4screen.CSB.Notification.Business
{
   public class RegistrationList
   {
      private User user = null;
      private List<Registration> list = new List<Registration>();
      private string rootFolder;
      private Guid currentUserID;
      private string currentNickname;
      private string[] currentRoles;

      public int Count
      {
         get { return list.Count; }
      }

      public string[] CurrentRoles
      {
         get { return currentRoles; }
      }

      public User CurrentUser
      {
         get { return user; }
      }

      public string RootFolder
      {
         get { return rootFolder; }
      }

      public RegistrationList(Common.UserDataContext udc, string rootFolder)
      {
         this.rootFolder = rootFolder;
         currentUserID = udc.UserID;
         currentNickname = udc.Nickname;
         currentRoles = udc.UserRoles;
         user = new User(udc.UserID, udc.Nickname);
      }

      public RegistrationList(Guid userID, string nickname, string[] roles, string rootFolder)
      {
         this.rootFolder = rootFolder;
         currentUserID = userID;
         currentNickname = nickname;
         currentRoles = roles;
         user = new User(currentUserID, currentNickname);
      }

      internal void Add(Registration item)
      {
         list.Add(item);
      }

      public void Load(bool useConfig, Guid? objectID, Guid? userID, Guid? communityID, int objectType, int[] objectTypes, List<TagWord> tagWords, Guid currentUserID, bool global)
      {
         ConfigurationList listConfig = new ConfigurationList();
         listConfig.Load(rootFolder);
         user.Load();

         if (useConfig)
         {
            RegistrationDefaultList listDefault = new RegistrationDefaultList(currentUserID, currentRoles, rootFolder);
            listDefault.Load();

            foreach (KeyValuePair<EventIdentifier, Configuration> kvp in listConfig)
            {
               Configuration itemConfig = kvp.Value;
               if (!itemConfig.IsEventAvailably(objectType, global))
                  continue;

               Role itemRole = itemConfig.FindRole(currentRoles);
               if (itemRole == null)
                  continue;

               if (itemRole.Range == ObjectRange.MyObject && this.currentUserID != currentUserID)
                  continue;

               RegistrationDefault itemDefault = listDefault.Item(kvp.Key);

               Registration itemReg = new Registration(currentUserID);
               itemReg.IsGlobal = global;
               itemReg.Identifier = itemConfig.Identifier;
               foreach (CarrierType soll in itemRole.CarrierTypes)
               {
                  Carrier itemCarr = new Carrier(itemReg.Carriers);
                  itemCarr.Type = soll;
                  itemCarr.Availably = user.Carriers.Item(soll).IsValid;

                  if (itemDefault != null)
                  {
                     Carrier itemCarrDefault = itemDefault.Carriers.Item(soll);
                     if (itemCarrDefault != null)
                     {
                        itemCarr.Checked = itemCarrDefault.Checked;
                        itemCarr.Collect = itemCarrDefault.Collect;
                     }
                  }

                  itemReg.Carriers.Add(itemCarr);
               }

               foreach (ObjType itemObjectType in itemConfig.ObjTypes)
               {
                  ObjType objType = new ObjType(itemReg.ObjectTypeList, itemObjectType.Identifier);
                  if (!itemConfig.IsObjectTypeAvailably(itemObjectType.Identifier, global))
                     objType.Availably = false;
                  else if (!global && itemObjectType.Identifier != objectType)
                     objType.Availably = false;

                  itemReg.ObjectTypeList.Add(objType);
               }
               itemReg.ObjectTypeList.SetChecked(itemReg.ObjectTypeList.GetChecked());

               this.Add(itemReg);
            }
         }

         Data.Registration objData = new Data.Registration();
         objData.Load(useConfig, this, currentUserID, objectID, userID, communityID, objectTypes, tagWords, global);
      }

      public Registration GetItemByType(EventIdentifier key)
      {
         return list.Find(x => x.Identifier == key);
      }

      public Registration GetItemByID(string registrationID)
      {
         return list.Find(x => x.ID == registrationID);
      }

      public IEnumerator<Registration> GetEnumerator()
      {
         return list.GetEnumerator();
      }
   }
}
