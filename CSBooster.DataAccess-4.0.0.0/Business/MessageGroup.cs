// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class MessageGroup
    {
        private string strUserID;
        private int intGroupID;
        private MessageGroupTypes enuGroupType;
        private string strTitle;
        private bool blnDirty = false;
        private int intCount = 0;

        public MessageGroup()
        {
        }

        internal MessageGroup(string userID, MessageGroupTypes groupType, int groupID, string title, int count)
        {
            strUserID = userID;
            enuGroupType = groupType;
            intGroupID = groupID;
            strTitle = title;
            blnDirty = false;
            intCount = count;
        }

        public static List<MessageGroup> LoadWithCount(string userID, MessageGroupTypes groupType)
        {
            List<MessageGroup> list = new List<MessageGroup>();
            list.Add(new MessageGroup(userID, groupType, 0, "Default", 0));

            Data.MessageGroup objData = new Data.MessageGroup();
            objData.Load(list, userID, groupType, true);

            return list;
        }

        public static List<MessageGroup> Load(string userID, MessageGroupTypes groupType)
        {
            List<MessageGroup> list = new List<MessageGroup>();
            list.Add(new MessageGroup(userID, groupType, 0, groupType.ToString(), 0));

            Data.MessageGroup objData = new Data.MessageGroup();
            objData.Load(list, userID, groupType, false);

            return list;
        }

        public static bool Exist(string userID, MessageGroupTypes groupType, string title)
        {
            if (title.ToLower().Trim() == groupType.ToString().ToLower())
                return true;

            Data.MessageGroup objData = new Data.MessageGroup();
            return objData.Exist(userID, groupType, title);
        }

        public static void Add(string userID, MessageGroupTypes groupType, string title)
        {
            MessageGroup item = new MessageGroup(userID, groupType, -1, title, 0);

            Data.MessageGroup objData = new Data.MessageGroup();
            objData.Save(item);
        }

        public void Update(string userID, MessageGroupTypes groupType)
        {
            if (intGroupID == 0)
                return;

            Data.MessageGroup objData = new Data.MessageGroup();
            objData.Save(this);
        }

        public static void Delete(string userID, MessageGroupTypes groupType, int groupID)
        {
            if (groupID == 0)
                return;

            Data.MessageGroup objData = new Data.MessageGroup();
            objData.Delete(userID, groupType, groupID);
        }


        public string UserID
        {
            get { return strUserID; }
        }

        public int GroupID
        {
            get { return intGroupID; }
            internal set { intGroupID = value; }
        }

        public MessageGroupTypes GroupType
        {
            get { return enuGroupType; }
        }

        public string Title
        {
            get { return strTitle; }
            set
            {
                if (intGroupID >= 0)
                {
                    if (strTitle != value)
                        blnDirty = true;
                    strTitle = value;
                }
            }
        }

        internal bool Dirty
        {
            get
            {
                if (intGroupID == -1)
                    return true;
                else
                    return blnDirty;
            }
            set { blnDirty = value; }
        }
    }
}