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
    internal class Configuration
    {
        private EventIdentifier enuIdentifier = EventIdentifier.NotDefined;
        private bool blnAvailably = false;
        private CarrierList listCarrier = new CarrierList();
        private Dictionary<string, Role> listRole = new Dictionary<string, Role>();
        private ObjTypeList listObjectType = new ObjTypeList();
        private string strGlobalApplicable = string.Empty;
        private string strGlobalObject = string.Empty;
        private string strLocalApplicable = string.Empty;

        public string LocalApplicable
        {
            get { return strLocalApplicable; }
            set { strLocalApplicable = value; }
        }

        public string GlobalObject
        {
            get { return strGlobalObject; }
            set { strGlobalObject = value; }
        }

        public ObjTypeList ObjTypes
        {
            get { return listObjectType; }
            set { listObjectType = value; }
        }

        public string GlobalApplicable
        {
            get { return strGlobalApplicable; }
            set { strGlobalApplicable = value; }
        }

        public bool IsEventAvailably(int objectType, bool global)
        {

            if (objectType != 0)
            {
                if (global)
                {
                    string[] strTest = strGlobalApplicable.ToLower().Split(',');
                    foreach (string str in strTest)
                    {
                        if (str == Helper.GetObjectType(objectType).Id.ToLower())
                            return true;
                    }
                }
                else
                {
                    string[] strTest = strLocalApplicable.ToLower().Split(',');
                    foreach (string str in strTest)
                    {
                        if (str == Helper.GetObjectType(objectType).Id.ToLower())
                            return true;
                    }
                }
            }
            return false;
        }

        public bool IsObjectTypeAvailably(int objectType, bool global)
        {
            if (objectType != 0)
            {
                if (global)
                {
                    string[] strTest = strGlobalObject.ToLower().Split(',');
                    foreach (string str in strTest)
                    {
                        if (str == Helper.GetObjectType(objectType).Id.ToLower())
                            return true;
                    }
                }
                else
                {
                    string[] strTest = strLocalApplicable.ToLower().Split(',');
                    foreach (string str in strTest)
                    {
                        if (str == Helper.GetObjectType(objectType).Id.ToLower())
                            return true;
                    }
                }
            }
            return false;
        }

        public Dictionary<string, Role> Roles
        {
            get { return listRole; }
        }


        public Role FindRole(string[] userRoles)
        {
            foreach (string key in listRole.Keys)
            {
                foreach (string userRole in userRoles)
                {
                    if (key.ToLower().IndexOf(userRole.ToLower()) >= 0)
                        return listRole[key];
                }
            }
            return null;
        }

        public EventIdentifier Identifier
        {
            get { return enuIdentifier; }
            set { enuIdentifier = value; }
        }

        public bool Availably
        {
            get { return blnAvailably; }
            set { blnAvailably = value; }
        }

    }
}
