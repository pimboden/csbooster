//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _4screen.CSB.Notification.Business
{
   [Serializable]
   public class ObjType
   {
      private int enuIdentifier = 0;
      private bool blnChecked = false;
      private ObjTypeList objParent = null;
      private bool blnAvailably = true;

      public ObjType()
      {
      }

      public ObjType(ObjTypeList parent, int identifier)
      {
         objParent = parent;
         enuIdentifier = identifier;
      }

      public bool Availably
      {
         get { return blnAvailably; }
         set { blnAvailably = value; }
      }

      public int Identifier
      {
         get { return enuIdentifier; }
         set { enuIdentifier = value; }
      }

      public bool Checked
      {
         get { return blnChecked; }
         set
         {
            blnChecked = value;
            if (blnChecked)
               objParent.SetChecked(Identifier);
         }
      }

      public override string ToString()
      {
         return enuIdentifier.ToString();
      }

      public static int GetObjectTypeEnum(int value, int defaultValue)
      {
          _4screen.CSB.Common.SiteObjectType ot = null;
          ot = _4screen.CSB.Common.Helper.GetObjectType(value);
          if (ot == null)
          {
              ot = _4screen.CSB.Common.Helper.GetObjectType(defaultValue);
          }
         
         return ot.NumericId;
      }
   }
}
