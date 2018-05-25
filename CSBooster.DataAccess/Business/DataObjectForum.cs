//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class DataObjectForum : _4screen.CSB.DataAccess.Business.DataObject
    {
        public CommunityUsersType ThreadCreationUsers { get; set; }

        public bool ShowCommunityUsersType(CommunityUsersType userType)
        { 
            return XmlHelper.GetElementValue(this.XmlData.DocumentElement, string.Format("ShowCommunityUsersType{0}", (int)userType), true);     
        }

        public bool ShowCommunityUsersType(CommunityUsersType userType, bool newValue)
        { 
            XmlHelper.SetElementInnerText(this.XmlData.DocumentElement, string.Format("ShowCommunityUsersType{0}", (int)userType), newValue);
            return newValue;
        }

        public bool ShowChaired
        {
            get
            {
                return XmlHelper.GetElementValue(this.XmlData.DocumentElement, "ShowChaired", true);
            }
            set
            {
                XmlHelper.SetElementInnerText(this.XmlData.DocumentElement, "ShowChaired", value);
            }
        }

        public DataObjectForum()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectForum(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Forum").NumericId;
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

            Data.DataObjectForum.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectForum.GetOrderBySQL(qParas, parameters);
        }
        #endregion
    }
}