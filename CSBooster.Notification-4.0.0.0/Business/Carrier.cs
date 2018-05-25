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
using _4screen.CSB.Common.Notification;   

namespace _4screen.CSB.Notification.Business
{
   [Serializable]
   public class Carrier
   {
      internal static CarrierType GetCarrierTypeEnum(string value, CarrierType defaultValue)
      {
         foreach (CarrierType item in Enum.GetValues(typeof(CarrierType)))
         {
            if (item.ToString() == value)
               return item;
         }
         return defaultValue;
      }

      internal Carrier(CarrierList parent)
      {
         objParent = parent;
      }

      private CarrierList objParent = null;
      private CarrierType enuCarrierType = CarrierType.None;
      private CarrierCollect enuCarrierCollect = CarrierCollect.Immediately;
      private int intCollectValue = 1;
      private bool blnChecked = false;
      private string strFile = string.Empty;
      private string strAddress = string.Empty;
      private bool blnAvailably = false;

      internal bool IsValid
      {
         get
         {
            if (strAddress.Length > 0)
               return true;
            else
               return false;
         }
      }

      public bool Availably
      {
         get { return blnAvailably; }
         internal set { blnAvailably = value; }
      }

      public string Address
      {
         get { return strAddress; }
         set { strAddress = value; }
      }

      internal string File
      {
         get { return strFile; }
         set { strFile = value; }
      }

      public bool Checked
      {
         get { return blnChecked; }
         set
         {
            blnChecked = value;
            if (blnChecked)
               objParent.SetChecked(Type);
         }
      }

      public int CollectValue
      {
         get { return intCollectValue; }
         set { intCollectValue = value; }
      }

      public CarrierCollect Collect
      {
         get { return enuCarrierCollect; }
         set { enuCarrierCollect = value; }
      }

      public CarrierType Type
      {
         get { return enuCarrierType; }
         set { enuCarrierType = value; }
      }

      public override string ToString()
      {
         return string.Format("{0} / '{1}'", Type, Address);
      }
   }
}
