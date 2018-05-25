//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		24.10.2007 / PT
//             #1.2.0.0    23.01.2008 / PT   QuickLoad (SQL) anpassen / Objekttypen erweitert 
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Data
{
    internal class QuickStatistic
    {
        public string CommunityID = string.Empty;
        public long Count = 0;
        public long MinAmount = 0;
        public bool Changed = false;
    }

    internal class DataObjectBackground
    {
        private string strConn = string.Empty;
        private bool blnCancel = false;

        public DataObjectBackground()
        {
            strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;
            blnCancel = false;
        }

        public bool Cancel
        {
            set
            {
                if (value)
                    blnCancel = true;
            }
        }

        public void SetAgility()
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Quick_User_Background_SetAgility";

                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(string.Format(@"{0}\configurations\dataAccess.config", WebRootPath.Instance.ToString()));
                XmlNode xmlAgility = xmlConfig.SelectSingleNode("//AgilityPositions");

                GetData.Parameters.Add(SqlHelper.AddParameter("@Day", SqlDbType.Int, XmlHelper.GetElementValue(xmlAgility, "DayCount", -3)));
                for (int i = 1; i <= 10; i++)
                {
                    string strSQL = string.Format("@Pos{0}", i);
                    string strXML = string.Format("Agility_{0}", i);
                    GetData.Parameters.Add(SqlHelper.AddParameter(strSQL, SqlDbType.Decimal, XmlHelper.GetElementValue(xmlAgility, strXML, Convert.ToDecimal(i * 10))));
                }

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }

        public int SetEmphasis()
        {
            int intTotal = 0;
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load(string.Format(@"{0}\configurations\dataAccess.config", WebRootPath.Instance.ToString()));

            UserDataContext udc = UserDataContext.GetUserDataContext();

            Dictionary<int, int> dicThreshold = new Dictionary<int, int>();
            foreach (XmlElement xmlItem in xmlConfig.SelectNodes("//EmphasisPercent/Threshold"))
            {
                int enuObjectType = Helper.GetObjectTypeNumericID(xmlItem.GetAttribute("ObjectType"));
                int intCount = Convert.ToInt32(xmlItem.InnerText);
                if (!dicThreshold.ContainsKey(enuObjectType))   
                    dicThreshold.Add(enuObjectType, intCount);
            }

            SqlDataReader sqlReader = null;
            try
            {
                // User
                sqlReader = GetEmphasisReader(Helper.GetObjectType("User").NumericId);
                while (sqlReader.Read())
                {
                    if (blnCancel)
                        break;

                    if (SetEmphasisUser(sqlReader["OBJ_ID"].ToString().ToGuid(), dicThreshold, udc))
                        intTotal++;
                }
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;

                if (blnCancel)
                    return intTotal;

                // Community
                sqlReader = GetEmphasisReader(Helper.GetObjectType("Community").NumericId);
                while (sqlReader.Read())
                {
                    if (blnCancel)
                        break;

                    if (SetEmphasisCommunity(sqlReader["OBJ_ID"].ToString().ToGuid(), dicThreshold, udc))
                        intTotal++;
                }
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }

            return intTotal;
        }

        private bool SetEmphasisUser(Guid? userID, Dictionary<int, int> dicThreshold, UserDataContext udc)
        {
            int intTotal = 0;
            SqlDataReader sqlReader = null;
            try
            {
                Dictionary<int, int> list = new Dictionary<int, int>();


                sqlReader = Data.InfoObjects.GetReader(null, null, userID, null);
                while (sqlReader.Read())
                {
                    int enuObjectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
                    if (!dicThreshold.ContainsKey(enuObjectType))
                        continue;

                    int intCount = Convert.ToInt32(sqlReader["OBJ_Count"].ToString());
                    if (intCount < dicThreshold[enuObjectType])
                        continue;

                    list.Add(enuObjectType, intCount);
                    intTotal += intCount;
                }

                Business.DataObjectUser objUser = Business.DataObject.Load<Business.DataObjectUser>(userID.Value, null, false);
                if (objUser.State == ObjectState.Saved)
                {
                    List<int> keys = new List<int>(list.Keys);
                    foreach (int key in keys)
                    {
                        list[key] = (int)Math.Round(100.0 / intTotal * list[key]);
                    }

                    objUser.EmphasisList = list;
                    //objUser.UpdateSpezialXml();
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }

            return (intTotal > 0);
        }

        private bool SetEmphasisCommunity(Guid? communityID, Dictionary<int, int> dicThreshold, UserDataContext udc)
        {
            int intTotal = 0;
            SqlDataReader sqlReader = null;
            try
            {
                Dictionary<int, int> list = new Dictionary<int, int>();

                sqlReader = Data.InfoObjects.GetReader(null, communityID, null, null);
                while (sqlReader.Read())
                {
                    int enuObjectType = Convert.ToInt32(sqlReader["OBJ_Type"]);
                    if (!dicThreshold.ContainsKey(enuObjectType))
                        continue;

                    int intCount = Convert.ToInt32(sqlReader["OBJ_Count"].ToString());
                    if (intCount < dicThreshold[enuObjectType])
                        continue;

                    list.Add(enuObjectType, intCount);
                    intTotal += intCount;
                }

                Business.DataObjectCommunity objCty = Business.DataObject.Load<Business.DataObjectCommunity>(communityID.Value, null, false);
                if (objCty.State == ObjectState.Saved)
                {
                    List<int> keys = new List<int>(list.Keys);
                    foreach (int key in keys)
                    {
                        list[key] = (int)Math.Round(100.0 / intTotal * list[key]);
                    }

                    objCty.EmphasisList = list;
                    //objCty.UpdateSpezialXml();
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }

            return (intTotal > 0);
        }

        private SqlDataReader GetEmphasisReader(int objectType)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Quick_User_Background_Emphasis_Load";

                GetData.Parameters.Add(SqlHelper.AddParameter("@OBJ_Type", SqlDbType.Int, (int)objectType));

                Conn.Open();

                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            return sqlReader;
        }

        /*public string Generate(string communityID, string userID, string tagID, ObjectType objectType, QuickSort sortBy, QuickSortDirection direction, int amount, string spezKey, DateTime expirationDate)
        {
            tagID = DataObjectsHelper.GetTagGuid(tagID);

            Business.QuickParameters paras = new Business.QuickParameters();
            paras.Amount = amount;
            paras.ObjectType = objectType;
            paras.CommunityId = communityID;
            paras.UserId = userID;
            paras.TagID = tagID;
            paras.SortBy = sortBy;
            paras.Direction = direction;
            QuickCacheHandler objCache = new QuickCacheHandler(paras, spezKey);

            string strFileName = string.Empty;
            SqlDataReader sqlReader = null;
            try
            {
                DateTime datStart = DateTime.Now;
                DataTable dt = new DataTable("Quick");

                if (objectType == ObjectType.Community)
                {
                    sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, ObjectType.Community);
                }
                else if (objectType == ObjectType.User)
                {
                    if (!string.IsNullOrEmpty(spezKey))
                        paras.OnlyWithImage = bool.Parse(spezKey);

                    sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, ObjectType.User);
                }
                else if (objectType == ObjectType.Video)
                {
                    if (!string.IsNullOrEmpty(spezKey))
                        paras.OnlyConverted = bool.Parse(spezKey);

                    sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, ObjectType.Video);
                }
                else if (objectType == ObjectType.Tag)
                {
                    sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, ObjectType.Tag);
                }
                else
                {
                    sqlReader = DataObjectsHelper.GetReaderAll<T>(paras, null);
                }

                if (sqlReader != null)
                {
                    dt.Load(sqlReader);
                    if (sqlReader != null)
                        sqlReader.Close();
                    sqlReader = null;

                    TimeSpan tisDuration = new TimeSpan(DateTime.Now.Ticks - datStart.Ticks);
                    DBCacheSave(objCache.Key, dt, expirationDate, tisDuration);
                    strFileName = objCache.Key;
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }
            return strFileName;
        }*/

        /*private void DBCacheSave(string key, DataTable dt, DateTime expirationDate, TimeSpan duration)
        {
            SqlConnection Conn = new SqlConnection(strConn);
            try
            {
                SqlCommand GetData = new SqlCommand();

                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_DBCache_Save";

                GetData.Parameters.Add(SqlHelper.AddParameter("@DBC_Key", SqlDbType.VarChar, 250, key));
                GetData.Parameters.Add(SqlHelper.AddParameter("@DBC_ExpirationDate", SqlDbType.DateTime, expirationDate));
                GetData.Parameters.Add(SqlHelper.AddParameter("@DBC_Duration", SqlDbType.BigInt, (long) duration.Ticks));

                MemoryStream stream = new MemoryStream(100000);
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
                dt.WriteXml(writer, XmlWriteMode.WriteSchema);
                byte[] arr = stream.ToArray();
                UnicodeEncoding utf = new UnicodeEncoding();

                GetData.Parameters.Add(SqlHelper.AddParameter("@DBC_DataTable", SqlDbType.Xml, utf.GetString(arr).Trim()));

                Conn.Open();
                GetData.ExecuteNonQuery();
            }
            finally
            {
                Conn.Close();
            }
        }*/

        public int StartBirthdayCheck(int dayCountBefore)
        {
            int intTotal = 0;
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = GetBirthdayNotificationCandidatesReader(dayCountBefore);
                while (sqlReader.Read())
                {
                    Guid _UserID = sqlReader["ASP_UserId"].ToString().ToGuid();
                    Guid _FriendID = sqlReader["ASP_FriendId"].ToString().ToGuid();
                    string strName = sqlReader["USR_Nickname"].ToString();
                    bool blnShow = Convert.ToBoolean(sqlReader["BirthdayShow"]);

                    if (blnShow)
                    {
                        // event eintragen da sowiso sichtbar nach aussen
                        Notification.Business.Event.ReportBirthdayNotification(_UserID, _FriendID, strName, Convert.ToDateTime(sqlReader["Birthday"]));
                        intTotal++;
                    }
                    else
                    {
                        // checken ob dieser Freund evtl. dass datum doch sehen darf
                        /*Business.UserProfileDatas listProfile = new Business.UserProfileDatas();
                        listProfile.LoadForFriend(UserProfileDataLoadType.LoadForFriendSetting, _UserID, _FriendID);
                        if (listProfile[UserProfileDataKey.Birthday].Show)
                        {
                            Notification.Business.Event.ReportBirthdayNotification(_UserID, _FriendID, strName, Convert.ToDateTime(sqlReader["Birthday"]));
                            intTotal++;
                        }*/
                    }
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                sqlReader = null;
            }

            return intTotal;
        }

        private SqlDataReader GetBirthdayNotificationCandidatesReader(int dayCountBefore)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString;

            SqlConnection Conn = new SqlConnection(strConn);
            SqlDataReader sqlReader = null;
            SqlCommand GetData = new SqlCommand();
            try
            {
                GetData.Connection = Conn;
                GetData.CommandType = CommandType.StoredProcedure;
                GetData.CommandText = "hisp_Notification_Event_SelectBirthdayNotificationCandidates";

                GetData.Parameters.Add(SqlHelper.AddParameter("@DayCountBefore", SqlDbType.Int, dayCountBefore));

                Conn.Open();

                sqlReader = GetData.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (Conn != null && Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            return sqlReader;
        }
    } // END CLASS
} // END NAMESPACE