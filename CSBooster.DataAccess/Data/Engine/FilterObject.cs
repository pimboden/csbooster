//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

using System.Collections.Generic;

namespace _4screen.CSB.DataAccess.Data
{
    internal class FilterObject
    {
        private string typeName;
        private int objectTypeId;
        private List<FilterObjectProperty> properties = new List<FilterObjectProperty>();

        internal FilterObject(string typeName)
        {
            this.typeName = typeName;
        }

        internal string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        internal int ObjectTypeId
        {
            get { return objectTypeId; }
            set { objectTypeId = value; }
        }

        internal List<FilterObjectProperty> Properties
        {
            get { return properties; }
            set { properties = value; }
        }
    }
}