// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public enum ForumTopicState
    {
        StateOpen = 1,
        StateClose = 2
    }

    public class DataObjectForumTopic : _4screen.CSB.DataAccess.Business.DataObject
    {
        public ForumTopicState TopicState { get; set; }
        public int TopicItemCount { get; set; }
        public DateTime? LastTopicItemDate { get; set; }
        public Guid? LastTopicItemID { get; set; }
        public Guid? LastTopicItemUserID { get; set; }
        public string LastTopicItemNickname { get; set; }
        public bool IsModerated { get; set; }
        public CommunityUsersType TopicItemCreationUsers { get; set; }

        public DataObjectForumTopic()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectForumTopic(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("ForumTopic").NumericId;
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);

            Title = Title.StripHTMLTags();
            Description = Description.StripHTMLTags();
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectForumTopic.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForumTopic.GetOrderBySQL(qParas, parameters);
        }
        #endregion
    }
}