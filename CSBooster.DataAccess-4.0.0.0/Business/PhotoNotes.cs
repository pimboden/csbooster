// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Notification;
using _4screen.CSB.Notification.Business;

namespace _4screen.CSB.DataAccess.Business
{
    public class PhotoNotes : List<PhotoNote>
    {
        private XmlNode xmlRoot;
        private DataObjectPicture objParent;

        internal PhotoNotes(XmlNode root, DataObjectPicture parent)
        {
            objParent = parent;
            xmlRoot = root;
            foreach (XmlElement xmlNote in xmlRoot.SelectNodes("Note"))
            {
                PhotoNote item = new PhotoNote(xmlNote, objParent);
                Add(item);
            }
        }

        public PhotoNote AddNode(UserDataContext udc, Guid fromID, string from)
        {
            int intNewID = 0;
            foreach (PhotoNote item in this)
            {
                if (item.ID > intNewID)
                    intNewID = item.ID;
            }

            XmlElement xmlNote = XmlHelper.AppendNode(xmlRoot, "Note") as XmlElement;
            PhotoNote newItem = new PhotoNote(xmlNote, objParent);
            newItem.ID = intNewID + 1;
            newItem.From = from;
            newItem.FromID = fromID;
            newItem.Public = (objParent.UserID == fromID);
            newItem.AddDate = DateTime.Now;

            Add(newItem);
            objParent.objectState = ObjectState.Changed;
            if (udc.UserID != objParent.UserID.Value)
                Event.ReportEvent(EventIdentifier.NewPhotoNote, udc.UserID, objParent.ObjectID.Value);

            return newItem;
        }

        public PhotoNote ItemByID(string id)
        {
            try
            {
                return ItemByID(int.Parse(id));
            }
            catch
            {
                return null;
            }
        }

        public PhotoNote ItemByID(int id)
        {
            foreach (PhotoNote item in this)
            {
                if (item.ID == id)
                {
                    objParent.objectState = ObjectState.Changed;
                    return item;
                }
            }
            return null;
        }

        public void RemoveNode(string id)
        {
            try
            {
                RemoveNode(int.Parse(id));
            }
            catch
            {
            }
        }

        public void RemoveNode(int id)
        {
            PhotoNote item = ItemByID(id);
            if (item != null)
            {
                xmlRoot.RemoveChild(item.Note);
                Remove(item);
                objParent.objectState = ObjectState.Changed;
                return;
            }
        }

        public bool HasOpenNote()
        {
            foreach (PhotoNote item in this)
            {
                if (!item.Public)
                    return true;
            }
            return false;
        }
    }
}
