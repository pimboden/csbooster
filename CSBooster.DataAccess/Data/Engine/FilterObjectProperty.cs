//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - FilterEngine
//
//  Created:    #1.0.0.0                10.08.2007 11:02:36 / aw
//  Updated:   
//******************************************************************************

namespace _4screen.CSB.DataAccess.Data
{
    internal class FilterObjectProperty
    {
        private string name;
        private string linkedName;

        internal FilterObjectProperty(string name, string copyToName)
        {
            this.name = name;
            linkedName = copyToName;
        }

        internal string Name
        {
            get { return name; }
            set { name = value; }
        }

        internal string LinkedName
        {
            get { return linkedName; }
            set { linkedName = value; }
        }
    }
}