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
   public class ObjTypeList
   {
      private bool blnMultiselect = true;
      private Dictionary<int, ObjType> listObjectType = new Dictionary<int, ObjType>();

      public ObjTypeList()
      {
      }

      public bool Multiselect
      {
         get { return blnMultiselect; }
         set { blnMultiselect = value; }
      }

      public void Clear()
      {
         listObjectType.Clear();
      }

      public void Add(ObjType item)
      {
         listObjectType.Add(item.Identifier, item);
      }

      public void SetChecked(int identifier)
      {
         if (Multiselect)
            return;

         foreach (int key in listObjectType.Keys)
         {
            if (key != identifier)
               listObjectType[key].Checked = false;
         }
      }

      public void SetChecked(int identifier, bool value)
      {
         if (listObjectType.ContainsKey(identifier))
            listObjectType[identifier].Checked = value;
      }

      public void SetChecked(string value)
      {
         int intCount = 0;
         foreach (KeyValuePair<int, ObjType> kvp in listObjectType)
         {
            kvp.Value.Checked = false;
            if (kvp.Value.Availably)
            {
               intCount++;
               if (value.IndexOf(string.Format(",{0},", (int)kvp.Key)) >= 0)
                  kvp.Value.Checked = true;
            }
         }

         if (intCount == 1)
         {
            foreach (KeyValuePair<int, ObjType> kvp in listObjectType)
            {
               if (kvp.Value.Availably)
                  kvp.Value.Checked = true;
            }
         }
      }

      public string GetChecked()
      {
         string strValue = string.Empty;
         foreach (KeyValuePair<int, ObjType> kvp in listObjectType)
         {
            if (kvp.Value.Availably && kvp.Value.Checked)
               strValue += "," + (int)kvp.Key;
         }

         if (strValue.Length > 0)
            return strValue + ",";
         else
            return string.Empty;
      }

      public IEnumerator GetEnumerator()
      {
         foreach (int key in listObjectType.Keys)
         {
            yield return (listObjectType[key]);
         }
      }

      public IEnumerable GetEnumeratorOnlyAvailably
      {
         get
         {
            foreach (int key in listObjectType.Keys)
            {
               if (listObjectType[key].Availably)
                  yield return (listObjectType[key]);
            }
         }
      }
   }
}
