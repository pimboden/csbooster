// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;
using _4screen.CSB.Notification.Business;

namespace _4screen.CSB.DataAccess.Business
{
    public class DataObjectForumTopicItem : _4screen.CSB.DataAccess.Business.DataObject
    {
        public Guid? ReferencedItemId { get; set; }
        public CommentStatus ItemStatus { get; set; }

        public DataObjectForumTopicItem()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectForumTopicItem(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("ForumTopicItem").NumericId;
        }

        public override void Insert(UserDataContext udc, Guid parentObjectID, int parentObjectType)
        {
            base.Insert(udc, parentObjectID, parentObjectType);

            Event.ReportNewTopicItem(userID.Value, parentObjectID, null);

            DataObjectForumTopic objTopic = Business.DataObject.Load<Business.DataObjectForumTopic>(parentObjectID);
            if (objTopic.State == ObjectState.Saved)
            {
                objTopic.Updated = DateTime.Now;
                objTopic.LastTopicItemDate = DateTime.Now;
                objTopic.LastTopicItemID = ObjectID;
                objTopic.LastTopicItemNickname = Nickname;
                objTopic.LastTopicItemUserID = UserID;
                objTopic.TopicItemCount = objTopic.TopicItemCount + 1;
                objTopic.UpdateBackground();
            }
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectForumTopicItem.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopicItem.GetOrderBySQL(qParas, parameters);
        }
        #endregion
    }
}