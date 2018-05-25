//******************************************************************************
//  Company:    4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:     CSBooster.DataAccess - RssEngine
//
//  Created:    #1.0.0.0                21.08.2007 10:09:06 / aw
//  Updated:   
//******************************************************************************

namespace _4screen.CSB.DataAccess.Data
{
    internal class RssItemConfig
    {
        private string type;
        private int objectTypeId;
        private string title;
        private int days;
        private string imageProperty;
        private string link;
        private int maxDescLength;

        internal RssItemConfig(string type, int objectTypeId, string title, int days, string imageProperty, string link, int maxDescLength)
        {
            this.type = type;
            this.objectTypeId = objectTypeId;
            this.title = title;
            this.days = days;
            this.imageProperty = imageProperty;
            this.link = link;
            this.maxDescLength = maxDescLength;
        }

        internal string Type
        {
            get { return type; }
            set { type = value; }
        }

        internal int ObjectTypeId
        {
            get { return objectTypeId; }
            set { objectTypeId = value; }
        }

        internal string Title
        {
            get { return title; }
            set { title = value; }
        }

        internal int Days
        {
            get { return days; }
            set { days = value; }
        }

        public string ImageProperty
        {
            get { return imageProperty; }
            set { imageProperty = value; }
        }

        internal string Link
        {
            get { return link; }
            set { link = value; }
        }

        internal int MaxDescLength
        {
            get { return maxDescLength; }
            set { maxDescLength = value; }
        }
    }
}