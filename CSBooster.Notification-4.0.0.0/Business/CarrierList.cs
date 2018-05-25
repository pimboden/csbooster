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
   public class CarrierList
   {
      internal CarrierList()
      {
      }

      private Dictionary<CarrierType, Carrier> listCarrier = new Dictionary<CarrierType, Carrier>();

      internal void Add(Carrier item)
      {
         if (!listCarrier.ContainsKey(item.Type))
            listCarrier.Add(item.Type, item);
      }

      public Carrier Item(CarrierType type)
      {
         if (listCarrier.ContainsKey(type))
            return listCarrier[type];
         else
            return null;
      }

      internal void SetChecked(CarrierType type)
      {
         foreach (CarrierType key in listCarrier.Keys)
         {
            if (key != type)
               listCarrier[key].Checked = false;
         }
      }

      public void SetChecked(CarrierType type, bool value)
      {
         if (listCarrier.ContainsKey(type))
            listCarrier[type].Checked = value;
      }

      public Carrier CheckedCarrier()
      {
         foreach (KeyValuePair<CarrierType, Carrier> kvp in listCarrier)
         {
            if (kvp.Value.Checked)
               return kvp.Value;
         }

         return null;
      }

      public IEnumerator<Carrier> GetEnumerator()
      {
         foreach (CarrierType key in listCarrier.Keys)
         {
            yield return (listCarrier[key]);
         }
      }
   }
}
