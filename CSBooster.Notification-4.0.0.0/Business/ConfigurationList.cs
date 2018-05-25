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
   internal class ConfigurationList : Dictionary<EventIdentifier, Configuration>
   {
      public void Load(string rootFolder)
      {
         XmlDocument xmlDoc = new XmlDocument(); // TODO: Cache config
         xmlDoc.Load(string.Format(@"{0}\configurations\notification.config", rootFolder));
         foreach (XmlElement xmlEvent in xmlDoc.SelectNodes("//Event[@Availably='true']"))
         {
            Configuration item = new Configuration();
            item.Identifier = Event.GetEventIdentifierEnum(xmlEvent.GetAttribute("Identifier"), EventIdentifier.NotDefined);
            if (item.Identifier == EventIdentifier.NotDefined)
               continue;

            item.Availably = bool.Parse(xmlEvent.GetAttribute("Availably"));

            XmlElement xmlTest = xmlEvent.SelectSingleNode("Global") as XmlElement;
            if (xmlTest != null)
            {
               item.GlobalApplicable = xmlTest.GetAttribute("Applicable");
               item.GlobalObject = xmlTest.GetAttribute("ObjectType");
            }

            xmlTest = xmlEvent.SelectSingleNode("Local") as XmlElement;
            if (xmlTest != null)
            {
               item.LocalApplicable = xmlTest.GetAttribute("Applicable");
            }

            foreach (var enu in _4screen.CSB.Common.Helper.GetObjectTypes())
            {
               if (enu.NumericId != 0)
               {
                   item.ObjTypes.Add(new ObjType(item.ObjTypes, enu.NumericId));
               }
            }

            foreach (XmlElement xmlRole in xmlEvent.SelectNodes("Role"))
            {
               Role objRole = new Role();
               objRole.Roles = xmlRole.GetAttribute("Roles");
               objRole.Range = Role.GetObjectRangeEnum(xmlRole.GetAttribute("Range"), ObjectRange.MyObject);
               string[] strCarriers = xmlRole.GetAttribute("Carriers").Split(',');
               foreach (string strCarrier in strCarriers)
               {
                  objRole.CarrierTypes.Add(Carrier.GetCarrierTypeEnum(strCarrier, CarrierType.None));
               }
               item.Roles.Add(objRole.Roles, objRole);
            }
            this.Add(item.Identifier, item);
         }
      }
   }
}
