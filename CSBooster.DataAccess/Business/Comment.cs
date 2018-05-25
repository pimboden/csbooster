//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.Notification.Business;
using _4screen.CSB.Common.Notification;

namespace _4screen.CSB.DataAccess.Business
{
    public class Comment
    {
        private UserDataContext udc;
        internal ObjectState enuState = ObjectState.Added;
        internal Guid? id = null;
        internal Guid? objectId = null;
        internal string strText = string.Empty;
        internal Guid? userId = null;
        internal DateTime datInserted = DateTime.MinValue;
        internal DateTime datUpdated = DateTime.MinValue;

        public Comment(UserDataContext userDataContext)
        {
            udc = userDataContext;
        }

        public ObjectState State
        {
            get { return enuState; }
        }

        public Guid? ID
        {
            get { return id; }
            set
            {
                if (id != value)
                    enuState = ObjectState.Changed;
                id = value;
            }
        }

        public Guid? ObjectID
        {
            get { return objectId; }
            set
            {
                if (objectId != value)
                    enuState = ObjectState.Changed;
                objectId = value;
            }
        }

        public Guid? UserID
        {
            get { return userId; }
        }

        public string Text
        {
            get { return strText; }
            set
            {
                if (strText != value)
                    enuState = ObjectState.Changed;
                strText = value;
            }
        }

        public DateTime Inserted
        {
            get { return datInserted; }
        }

        public DateTime Updated
        {
            get { return datUpdated; }
        }

        public void Insert()
        {
            if (udc.UserID == Constants.ANONYMOUS_USERID.ToGuid())
                throw new Exception("Access rights missing");

            if (State == ObjectState.Changed)
            {
                if (Text.Length == 0)
                    throw new MissingFieldException("Text is missing");
                else
                    Text = FilterEngine.FilterStringBadWords(Text, _4screen.CSB.DataAccess.Data.FilterObjectTypes.Comment,ObjectID.Value, udc.UserID);

                if (!ObjectID.HasValue)
                    throw new MissingFieldException("ObjectID is missing");

                Data.Comment objData = new Data.Comment();
                id = objData.Insert(ObjectID.Value, Text, udc.UserID);
                if (ID.HasValue)
                {
                    enuState = ObjectState.Saved;
                    Event.ReportEvent(EventIdentifier.Comment, udc.UserID, ObjectID);
                    Business.UserActivities.InsertComment(udc, ObjectID.Value);
                }
            }
        }
    }
}