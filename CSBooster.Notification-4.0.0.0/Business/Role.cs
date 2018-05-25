//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;   


namespace _4screen.CSB.Notification.Business
{
   internal class Role
   {
      public static ObjectRange GetObjectRangeEnum(string value, ObjectRange defaultValue)
      {
         foreach (ObjectRange item in Enum.GetValues(typeof(ObjectRange)))
         {
            if (item.ToString() == value)
               return item;
         }
         return defaultValue;
      }

      private string strRoles = string.Empty;
      private ObjectRange enuObjectRange = ObjectRange.MyObject;
      private List<CarrierType> listCarrierType = new List<CarrierType>();

      public List<CarrierType> CarrierTypes
      {
         get { return listCarrierType; }
         set { listCarrierType = value; }
      }

      public bool IsCarrierAvailably(CarrierType type)
      {
         return listCarrierType.Contains(type);
      }

      internal ObjectRange Range
      {
         get { return enuObjectRange; }
         set { enuObjectRange = value; }
      }

      public string Roles
      {
         get { return strRoles; }
         set { strRoles = value; }
      }
   }
}
