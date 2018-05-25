//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//  Updated:   
//******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Notification;
using _4screen.CSB.Common.Notification;

namespace _4screen.CSB.Notification.Data
{
    internal class Registration
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

        public void Save(Business.Registration item)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "hisp_Notification_RegisteredEvent_Update";

                if (string.IsNullOrEmpty(item.ID))
                    item.ID = Guid.NewGuid().ToString();

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@NRE_ID", SqlDbType.UniqueIdentifier, new Guid(item.ID)));

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, item.CurrentUserID));

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Identifier", SqlDbType.Int, (int)item.Identifier));

                if (item.ObjectID.HasValue)
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                else
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@ObjectID", SqlDbType.UniqueIdentifier));

                if (item.UserID.HasValue)
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@UserID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                else
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@UserID", SqlDbType.UniqueIdentifier));

                if (item.CommunityID.HasValue)
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CommunityID", SqlDbType.UniqueIdentifier, item.ObjectID.Value));
                else
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CommunityID", SqlDbType.UniqueIdentifier));

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@IsGlobal", SqlDbType.Bit, item.IsGlobal ? 1 : 0));

                if (!string.IsNullOrEmpty(item.Title))
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Title", SqlDbType.NVarChar, item.Title));
                else
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Title", SqlDbType.NVarChar));

                Business.Carrier objCarrier = item.Carriers.CheckedCarrier();
                if (objCarrier != null && objCarrier.Type != CarrierType.None)
                {
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Carrier", SqlDbType.Int, (int)objCarrier.Type));
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CarrierCollect", SqlDbType.Int, (int)objCarrier.Collect));
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CollectValue", SqlDbType.Int, (int)objCarrier.CollectValue));
                }
                else
                {
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@Carrier", SqlDbType.Int, 0));
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CarrierCollect", SqlDbType.Int, 0));
                    sqlCommand.Parameters.Add(SqlHelper.AddParameter("@CollectValue", SqlDbType.Int, 0));
                }

                DateTime datNext;

                if (objCarrier.Collect == CarrierCollect.Daily)
                {
                    datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
                }
                else if (objCarrier.Collect == CarrierCollect.Weekly)
                {
                    int days = (int)DateTime.Now.DayOfWeek;
                    datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(7 - days);
                }
                else if (objCarrier.Collect == CarrierCollect.Monthly)
                {
                    datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
                }
                else
                {
                    datNext = new DateTime(1900, 1, 1);
                }

                sqlCommand.Parameters.Add(SqlHelper.AddParameter("@NextSend", SqlDbType.DateTime, datNext));

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                if (objCarrier != null && objCarrier.Type != CarrierType.None)
                {
                    foreach (Business.ObjType objectTypeItem in item.ObjectTypeList.GetEnumeratorOnlyAvailably)
                    {
                        sqlCommand = new SqlCommand();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "hisp_Notification_RegisteredEventObjectType_Update";
                        sqlCommand.Parameters.Add(SqlHelper.AddParameter("@NRE_ID", SqlDbType.UniqueIdentifier, new Guid(item.ID)));
                        sqlCommand.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)objectTypeItem.Identifier));
                        sqlCommand.ExecuteNonQuery();
                    }

                    foreach (Business.TagWord tagWord in item.TagWords)
                    {
                        sqlCommand = new SqlCommand();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "hisp_Notification_RegisteredEventTagWord_Update";
                        sqlCommand.Parameters.Add(SqlHelper.AddParameter("@NRE_ID", SqlDbType.UniqueIdentifier, new Guid(item.ID)));
                        sqlCommand.Parameters.Add(SqlHelper.AddParameter("@TGW_ID", SqlDbType.UniqueIdentifier, new Guid(tagWord.TagID)));
                        sqlCommand.Parameters.Add(SqlHelper.AddParameter("@NET_TagWordGroup", SqlDbType.Int, tagWord.GroupID));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void Load(bool useConfig, Business.RegistrationList list, Guid currentUserID, Guid? objectID, Guid? userID, Guid? communityID, int[] objectTypes, List<Business.TagWord> tagWords, bool global)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT * ");
            sb.AppendFormat("FROM hitbl_Notification_RegisteredEvent_NRE ");
            sb.AppendFormat("WHERE ");
            sb.AppendFormat("NUS_USR_ID = '{0}' ", currentUserID);
            sb.AppendFormat("AND NRE_IsGlobal = {0} ", global ? 1 : 0);
            if (objectID.HasValue)
                sb.AppendFormat("AND NRE_ObjectID = '{0}' ", objectID);
            if (userID.HasValue)
                sb.AppendFormat("AND NRE_UserID = '{0}' ", userID);
            if (communityID.HasValue)
                sb.AppendFormat("AND NRE_CommunityID = '{0}' ", communityID);
            if (objectTypes != null && objectTypes.Length > 0)
            {
                sb.AppendFormat("AND (");
                for (int i = 0; i < objectTypes.Length; i++)
                {
                    sb.AppendFormat("({0} IN (SELECT OBJ_Type FROM hirel_Notification_Event_ObjectType_NEO WHERE hirel_Notification_Event_ObjectType_NEO.NRE_ID = hitbl_Notification_RegisteredEvent_NRE.NRE_ID)) ", (int)objectTypes[i]);
                    if (i < objectTypes.Length - 1)
                        sb.AppendFormat("AND ");
                }
                sb.AppendFormat(") ");
            }
            if (tagWords != null && tagWords.Count > 0)
            {
                sb.AppendFormat("AND ( 1=1 ");
                for (int tagGroupID = 1; tagGroupID < 4; tagGroupID++)
                {
                    List<Business.TagWord> tagWordPerGroup = tagWords.FindAll(x => x.GroupID == tagGroupID);
                    if (tagWordPerGroup.Count > 0)
                    {
                        for (int i = 0; i < tagWordPerGroup.Count; i++)
                        {
                            sb.AppendFormat("AND ('{0}' IN (SELECT TGW_ID FROM hirel_Notification_Event_TagLog_NET WHERE hirel_Notification_Event_TagLog_NET.NRE_ID = hitbl_Notification_RegisteredEvent_NRE.NRE_ID AND hirel_Notification_Event_TagLog_NET.NET_TagWordGroup = {1})) ", tagWordPerGroup[i].TagID, tagWordPerGroup[i].GroupID);
                        }
                    }
                    else
                    {
                        sb.AppendFormat("AND (NOT EXISTS (SELECT TGW_ID FROM hirel_Notification_Event_TagLog_NET WHERE hirel_Notification_Event_TagLog_NET.NRE_ID = hitbl_Notification_RegisteredEvent_NRE.NRE_ID AND hirel_Notification_Event_TagLog_NET.NET_TagWordGroup = {0})) ", tagGroupID);
                    }
                }
                sb.AppendFormat(") ");
            }
            sb.AppendFormat(" ORDER BY NRE_CommunityID, NRE_UserID, NRE_Identifier");

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sb.ToString();

                sqlConnection.Open();
                SqlDataReader registrationItemSqlReader = sqlCommand.ExecuteReader();
                while (registrationItemSqlReader.Read())
                {
                    Business.Registration item;
                    if (useConfig)
                    {
                        EventIdentifier identifier = (EventIdentifier)Convert.ToInt32(registrationItemSqlReader["NRE_Identifier"]);
                        item = list.GetItemByType(identifier);
                    }
                    else
                    {
                        Business.ConfigurationList listConfig = new Business.ConfigurationList(); // TODO: Load config in business logic
                        listConfig.Load(list.RootFolder);
                        EventIdentifier eventType = (EventIdentifier)Convert.ToInt32(registrationItemSqlReader["NRE_Identifier"]);
                        item = CopyConfig(listConfig[eventType], list, (bool)registrationItemSqlReader["NRE_IsGlobal"]);
                        list.Add(item);
                    }
                    FillObject(item, registrationItemSqlReader);
                }
                registrationItemSqlReader.Close();

                foreach (Business.Registration item in list)
                {
                    Business.Carrier objCarrier = item.Carriers.CheckedCarrier();
                    if (objCarrier != null && objCarrier.Type != CarrierType.None)
                    {
                        SqlCommand sqlCommand2 = new SqlCommand();
                        sqlCommand2.Connection = sqlConnection;
                        sqlCommand2.CommandType = CommandType.Text;
                        sqlCommand2.CommandText = string.Format("SELECT * FROM hirel_Notification_Event_ObjectType_NEO WHERE NRE_ID = '{0}'", item.ID);
                        SqlDataReader objectTypesSqlReader = sqlCommand2.ExecuteReader();
                        item.ObjectTypeList.Clear();
                        while (objectTypesSqlReader.Read())
                        {
                            FillObjectTypes(item, objectTypesSqlReader);
                        }
                        objectTypesSqlReader.Close();

                        SqlCommand sqlCommand3 = new SqlCommand();
                        sqlCommand3.Connection = sqlConnection;
                        sqlCommand3.CommandType = CommandType.Text;
                        sqlCommand3.CommandText = string.Format("SELECT * FROM hirel_Notification_Event_TagLog_NET WHERE NRE_ID = '{0}'", item.ID);
                        SqlDataReader tagWordsSqlReader = sqlCommand3.ExecuteReader();
                        item.TagWords.Clear();
                        while (tagWordsSqlReader.Read())
                        {
                            FillObjectTagWords(item, tagWordsSqlReader);
                        }
                        tagWordsSqlReader.Close();
                    }
                }
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
        }

        public bool HasRegistration(string userID, string objectID, bool global)
        {
            bool blnRet = false;

            SqlConnection Conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Notification_RegisteredEvent_Load";

                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, new Guid(userID)));
                GetData.Parameters.Add(SqlHelper.AddParameter("@IsGlobal", SqlDbType.Int, global ? 1 : 0));
                GetData.Parameters.Add(SqlHelper.AddParameter("@ForObjectID", SqlDbType.UniqueIdentifier, new Guid(objectID)));

                Conn.Open();
                object objRet = GetData.ExecuteScalar();
                if (objRet != null && objRet.ToString() == "1")
                    blnRet = true;
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
            return blnRet;
        }

        public void DeleteAllForUser(string userID)
        {
            SqlConnection Conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Notification_User_Delete";
                GetData.Parameters.Add(SqlHelper.AddParameter("@USR_ID", SqlDbType.UniqueIdentifier, new Guid(userID)));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
            }
        }

        private Business.Registration CopyConfig(Business.Configuration itemConfig, Business.RegistrationList list, bool global)
        {
            Business.Role itemRole = itemConfig.FindRole(list.CurrentRoles);
            if (itemRole == null)
                return null;

            Business.Registration itemReg = new Business.Registration(list.CurrentUser.UserID);
            itemReg.Identifier = itemConfig.Identifier;
            itemReg.IsGlobal = global;
            foreach (CarrierType soll in itemRole.CarrierTypes)
            {
                Business.Carrier itemCarr = new Business.Carrier(itemReg.Carriers);
                itemCarr.Type = soll;
                itemCarr.Availably = list.CurrentUser.Carriers.Item(soll).IsValid;
                itemReg.Carriers.Add(itemCarr);
            }

            foreach (Business.ObjType itemObjectType in itemConfig.ObjTypes)
            {
                Business.ObjType objType = new Business.ObjType(itemReg.ObjectTypeList, itemObjectType.Identifier);
                if (!itemConfig.IsObjectTypeAvailably(itemObjectType.Identifier, global))
                    objType.Availably = false;
                /*else if (!global && objectType.IndexOf(string.Format(",{0},", (int)itemObjectType.Identifier)) == -1)
                   objType.Availably = false;*/

                itemReg.ObjectTypeList.Add(objType);
            }
            itemReg.ObjectTypeList.SetChecked(itemReg.ObjectTypeList.GetChecked());

            return itemReg;
        }

        private void FillObject(Business.Registration item, SqlDataReader sqlReader)
        {
            if (item != null)
            {
                item.ID = sqlReader["NRE_ID"].ToString();

                item.CurrentUserID = sqlReader["NUS_USR_ID"].ToString().ToGuid();
                item.IsGlobal = (bool)sqlReader["NRE_IsGlobal"];
                if (sqlReader["NRE_ObjectID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["NRE_ObjectID"].ToString()))
                    item.ObjectID = sqlReader["NRE_ObjectID"].ToString().ToGuid();
                if (sqlReader["NRE_UserID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["NRE_UserID"].ToString()))
                    item.UserID = sqlReader["NRE_UserID"].ToString().ToGuid();
                if (sqlReader["NRE_CommunityID"] != DBNull.Value && !string.IsNullOrEmpty(sqlReader["NRE_CommunityID"].ToString()))
                    item.CommunityID = sqlReader["NRE_CommunityID"].ToString().ToGuid();
                item.Title = sqlReader["NRE_Title"].ToString();
                Business.Carrier objCarrier = item.Carriers.Item((CarrierType)Convert.ToInt32(sqlReader["NRE_Carrier"]));
                if (objCarrier != null)
                {
                    objCarrier.Checked = true;
                    objCarrier.Collect = (CarrierCollect)Convert.ToInt32(sqlReader["NRE_CarrierCollect"]);
                    objCarrier.CollectValue = int.Parse(sqlReader["NRE_CollectValue"].ToString());
                }
            }
        }

        private void FillObjectTypes(Business.Registration item, SqlDataReader sqlReader)
        {
            if (item != null)
            {
                int objectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
                item.ObjectTypeList.Add(new _4screen.CSB.Notification.Business.ObjType(item.ObjectTypeList, objectType) { Availably = true, Checked = true, Identifier = objectType });
            }
        }

        private void FillObjectTagWords(Business.Registration item, SqlDataReader sqlReader)
        {
            if (item != null)
            {
                item.TagWords.Add(new Business.TagWord() { TagID = sqlReader["TGW_ID"].ToString(), GroupID = (int)sqlReader["NET_TagWordGroup"] });
            }
        }
    }
}
