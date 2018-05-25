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
   [Serializable]
   public class Registration : ICloneable
   {
      private EventIdentifier eventIdentifier = EventIdentifier.NotDefined;
      private CarrierList carrierList = new CarrierList();
      private ObjTypeList objectTypeList = new ObjTypeList();
      private List<TagWord> tagWords = new List<TagWord>();
      private bool removable = false;

      public bool IsGlobal { get; set; }
      public string ID { get; set; }
      public Guid? CurrentUserID { get; set; }
      public Guid? ObjectID { get; set; }
      public Guid? UserID { get; set; }
      public Guid? CommunityID { get; set; }
      public string Title { get; set; }

      public List<TagWord> TagWords
      {
         get { return tagWords; }
         set { tagWords = value; }
      }

      public ObjTypeList ObjectTypeList
      {
         get { return objectTypeList; }
         set { objectTypeList = value; }
      }

      public bool Registered
      {
         get { return CurrentUserID.HasValue; }
      }

      public EventIdentifier Identifier
      {
         get { return eventIdentifier; }
         internal set { eventIdentifier = value; }
      }

      public CarrierList Carriers
      {
         get { return carrierList; }
      }

      public bool Removable
      {
         get { return removable; }
         internal set { removable = value; }
      }

      public object Clone()
      {
         BinaryFormatter BF = new BinaryFormatter();
         MemoryStream memStream = new MemoryStream();
         BF.Serialize(memStream, this);
         memStream.Position = 0;

         return (BF.Deserialize(memStream));
      }

      public void Save()
      {
         Data.Registration objData = new Data.Registration();
         objData.Save(this);
      }

      public override string ToString()
      {
         if (IsGlobal)
            return string.Format("G / {0} / {1}", Identifier, ID);
         else
            return string.Format("O / {0} / {1}", Identifier, ID);
      }

      public static bool HasGlobalRegistration(string userID, string objectID)
      {
         Data.Registration objData = new Data.Registration();
         return objData.HasRegistration(userID, objectID, true);
      }

      public static bool HasLocalRegistration(string userID, string objectID)
      {
         Data.Registration objData = new Data.Registration();
         return objData.HasRegistration(userID, objectID, false);
      }

      internal Registration(Guid userID)
      {
         CurrentUserID = userID;
      }
   }
}
