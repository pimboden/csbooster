using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class PhotoNote
    {
        private XmlElement xmlNote;
        private DataObjectPicture objParent;

        public PhotoNote(XmlElement xml, DataObjectPicture parent)
        {
            xmlNote = xml;
            objParent = parent;
        }

        internal XmlElement Note
        {
            get { return xmlNote; }
        }

        public int ID
        {
            get { return XmlHelper.GetElementValue(xmlNote, "ID", 0); }
            internal set
            {
                if (ID != value)
                    objParent.objectState = ObjectState.Changed;
                XmlHelper.SetElementInnerText(xmlNote, "ID", value);
            }
        }

        public DateTime AddDate
        {
            get { return XmlHelper.GetElementValue(xmlNote, "AddDate", DateTime.Now); }
            internal set { XmlHelper.SetElementInnerText(xmlNote, "AddDate", value); }
        }

        public DateTime UpdDate
        {
            get { return XmlHelper.GetElementValue(xmlNote, "UpdDate", DateTime.Now); }
            internal set { XmlHelper.SetElementInnerText(xmlNote, "UpdDate", value); }
        }


        public Guid OwnerID
        {
            get { return objParent.UserID.Value; }
        }

        public bool Public
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Public", false); }
            set
            {
                if (Public != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Public", value);
            }
        }

        public string Title
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Title", string.Empty); }
            set
            {
                if (Title != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Title", value.StripHTMLTags());
            }
        }

        public string Text
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Text", string.Empty); }
            set
            {
                if (Text != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Text", value.StripHTMLTags());
            }
        }

        public string WhoIsID
        {
            get { return XmlHelper.GetElementValue(xmlNote, "WhoIsID", string.Empty); }
            set
            {
                if (WhoIsID != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "WhoIsID", value.StripHTMLTags());
            }
        }

        public string WhoIs
        {
            get { return XmlHelper.GetElementValue(xmlNote, "WhoIs", string.Empty); }
            set
            {
                if (WhoIs != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "WhoIs", value.StripHTMLTags());
            }
        }

        public Guid FromID
        {
            get { return XmlHelper.GetElementValue(xmlNote, "FromID", Guid.Empty); }
            internal set
            {
                if (FromID != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "FromID", value);
            }
        }

        public string From
        {
            get { return XmlHelper.GetElementValue(xmlNote, "From", string.Empty); }
            internal set
            {
                if (From != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "From", value.StripHTMLTags());
            }
        }

        public int Top
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Top", 0); }
            set
            {
                if (Top != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Top", value);
            }
        }

        public int Left
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Left", 0); }
            set
            {
                if (Left != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Left", value);
            }
        }

        public int Width
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Width", 0); }
            set
            {
                if (Width != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Width", value);
            }
        }

        public int Height
        {
            get { return XmlHelper.GetElementValue(xmlNote, "Height", 0); }
            set
            {
                if (Height != value)
                {
                    UpdDate = DateTime.Now;
                    objParent.objectState = ObjectState.Changed;
                }
                XmlHelper.SetElementInnerText(xmlNote, "Height", value);
            }
        }
    }
}
