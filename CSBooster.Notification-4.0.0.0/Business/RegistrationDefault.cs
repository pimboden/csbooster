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
   public class RegistrationDefault
   {
      internal RegistrationDefault()
      {
      }

      private EventIdentifier enuEventIdentifier = EventIdentifier.NotDefined;
      private CarrierList objCarrierList = new CarrierList();

      public EventIdentifier Identifier
      {
         get { return enuEventIdentifier; }
         internal set { enuEventIdentifier = value; }
      }

      public CarrierList Carriers
      {
         get { return objCarrierList; }
      }


   }
}
