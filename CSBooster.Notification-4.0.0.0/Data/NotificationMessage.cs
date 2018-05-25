//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		10.08.2007 / PT
//******************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Notification;
using _4screen.CSB.Common.Notification;
using _4screen.Utils.Web;

namespace _4screen.CSB.Notification.Data
{
    internal class NotificationMessage
    {
        bool blnFlip = true;
        string strWebRoot = string.Empty;
        CarrierType enuCarrierType = CarrierType.None;

        string strRootFolder = string.Empty;
        StringBuilder strBase = new StringBuilder(0);
        StringBuilder strEventList = new StringBuilder(500);
        StringBuilder strEvent = new StringBuilder();
        StringBuilder strEventItem = new StringBuilder();
        StringBuilder strEventAlternateItem = new StringBuilder();
        EventIdentifier enuLastEventIdentifier = EventIdentifier.NotDefined;
        string strGroupID = string.Empty;
        string strAddress = string.Empty;
        string strSubject = "Notification";
        bool blnConfident = false;
        System.Collections.Hashtable hasEvents = new System.Collections.Hashtable(50);

        public NotificationMessage(CarrierType carrierType, string webRoot, string rootFolder)
        {
            enuCarrierType = carrierType;
            strWebRoot = webRoot;
            strRootFolder = rootFolder;
            strGroupID = Guid.NewGuid().ToString();
            LoadEventBase();
        }

        public bool Confident
        {
            get { return blnConfident; }
        }

        public string Subject
        {
            get { return strSubject; }
        }

        public string Address
        {
            get { return strAddress; }
        }

        public CarrierType Carrier
        {
            get { return enuCarrierType; }
        }

        public string GroupID
        {
            get { return strGroupID; }
        }

        public void AddEvent(SqlDataReader sqlReader)
        {

            if (hasEvents.ContainsKey(sqlReader["NEV_ID"].ToString()))
                return;
            else
                hasEvents.Add(sqlReader["NEV_ID"].ToString(), null);

            EventIdentifier enuEventIdentifier = (EventIdentifier)Convert.ToInt32(sqlReader["NRE_Identifier"]);

            if (strAddress.Length == 0)
            {
                strAddress = sqlReader[string.Format("NUS_Carrier_{0}", (int)Carrier)].ToString();
                ReplaceTag(strBase, sqlReader, enuEventIdentifier);
            }

            //Wenn irgend ein Event 'true' ist so müssen alle 'true' sein
            if (!blnConfident)
            {
                if (Convert.ToBoolean(sqlReader["NEV_Confident"]))
                    blnConfident = true;
            }

            if (enuLastEventIdentifier != enuEventIdentifier)
            {
                enuLastEventIdentifier = enuEventIdentifier;
                strEventList.Append(strEvent.ToString().Replace(Constants.K_ITEM, string.Empty));
                LoadEventTemplate(enuEventIdentifier);
                blnFlip = true;
            }

            StringBuilder strItem = null;
            if (blnFlip)
                strItem = new StringBuilder(strEventItem.ToString());
            else
                strItem = new StringBuilder(strEventAlternateItem.ToString());
            blnFlip = !blnFlip;

            ReplaceTag(strItem, sqlReader, enuEventIdentifier);

            strItem.AppendLine(Constants.K_ITEM);
            strEvent = strEvent.Replace(Constants.K_ITEM, strItem.ToString());
        }

        public void Commit()
        {
            if (strEvent.Length > 0)
                strEventList.Append(strEvent.ToString().Replace(Constants.K_ITEM, string.Empty));
            strEvent = new StringBuilder();
        }

        private void ReplaceTag(StringBuilder target, SqlDataReader sqlReader, EventIdentifier eventIdentifier)
        {
            int enuObjectType = (int)Convert.ToInt32(sqlReader["OBJ_Type"]);
            DateTime dat = DateTime.Parse(sqlReader["NEV_Time"].ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_WEB_ROOT) > -1)
                target = target.Replace(Constants.K_FIELD_WEB_ROOT, strWebRoot);

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_LINK) > -1)
            {
                if (eventIdentifier == EventIdentifier.NewMember)
                    target = target.Replace(Constants.K_FIELD_EVENT_LINK, EventLink(Helper.GetObjectTypeNumericID("Community"), sqlReader["NRE_ForObjectID"].ToString()));
                else
                    target = target.Replace(Constants.K_FIELD_EVENT_LINK, EventLink(enuObjectType, sqlReader["OBJ_ID"].ToString()));
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_USER_LINK) > -1)
            {
                target = target.Replace(Constants.K_FIELD_USER_LINK, UserLink(sqlReader["OBJ_ID"].ToString()));
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_COMMUNITY_LINK) > -1)
            {
                target = target.Replace(Constants.K_FIELD_COMMUNITY_LINK, CommunityLink(sqlReader["OBJ_ID"].ToString()));
            }


            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_TITLE) > -1)
                target = target.Replace(Constants.K_FIELD_EVENT_TITLE, sqlReader["OBJ_Title"].ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_AGE) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    int intAge = DateTime.Now.Year - Convert.ToDateTime(sqlReader["OBJ_Birthday"]).Year;
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_AGE, intAge.ToString());
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_AGE, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY, Convert.ToDateTime(sqlReader["OBJ_Birthday"]).Day.ToString());
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY_LONG) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY_LONG, Convert.ToDateTime(sqlReader["OBJ_Birthday"]).ToString("dddd"));
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_DAY_LONG, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH, Convert.ToDateTime(sqlReader["OBJ_Birthday"]).Month.ToString());
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH_LONG) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH_LONG, Convert.ToDateTime(sqlReader["OBJ_Birthday"]).ToString("MMMM"));
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_MONTH_LONG, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_YEAR) > -1)
            {
                if (sqlReader["OBJ_Birthday"] != DBNull.Value)
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_YEAR, Convert.ToDateTime(sqlReader["OBJ_Birthday"]).Year.ToString());
                }
                else
                {
                    target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_BIRTHDATE_YEAR, "[not set]");
                }
            }

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_DATE) > -1)
                target = target.Replace(Constants.K_FIELD_EVENT_DATE, dat.ToShortDateString());

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_TIME) > -1)
                target = target.Replace(Constants.K_FIELD_EVENT_TIME, dat.ToShortTimeString());

            if (target.ToString().IndexOf(Constants.K_FIELD_EVENT_OBJECT_TYPE) > -1)
                target = target.Replace(Constants.K_FIELD_EVENT_OBJECT_TYPE, enuObjectType.ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_USER_ID) > -1)
                target = target.Replace(Constants.K_FIELD_USER_ID, sqlReader["NUS_USR_ID"].ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_USER_NICKNAME) > -1)
                target = target.Replace(Constants.K_FIELD_USER_NICKNAME, sqlReader["NUS_Nickname"].ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_USER_NAME) > -1)
                target = target.Replace(Constants.K_FIELD_USER_NAME, sqlReader["NUS_Name"].ToString());

            if (target.ToString().IndexOf(Constants.K_FIELD_USER_FIRSTNAME) > -1)
            {
                if (sqlReader["NUS_Firstname"] == null)
                    target = target.Replace(Constants.K_FIELD_USER_FIRSTNAME, sqlReader["NUS_Firstname"].ToString());
                else
                    target = target.Replace(Constants.K_FIELD_USER_FIRSTNAME, sqlReader["NUS_Nickname"].ToString());
            }
        }

        private void LoadEventBase()
        {
            strSubject = GuiLanguage.GetGuiLanguage("Templates").GetString(string.Format("Notification{0}Subject", Carrier));

            strBase.Append(GuiLanguage.GetGuiLanguage("Templates").GetString(string.Format("Notification{0}Body", Carrier)));
        }

        private void LoadEventTemplate(EventIdentifier eventIdentifier)
        {
            strEvent.Append(GuiLanguage.GetGuiLanguage("Templates").GetString(string.Format("Notification{0}Body{1}", Carrier, eventIdentifier)));

            int intStart = strEvent.ToString().IndexOf("<itemtemplate>", StringComparison.InvariantCultureIgnoreCase);
            int intStopp = strEvent.ToString().IndexOf("</itemtemplate>", StringComparison.InvariantCultureIgnoreCase);

            if (intStart >= 0 && intStopp > intStart)
            {
                strEventItem = new StringBuilder();

                int intLength = intStopp - (intStart + 14);
                strEventItem.Append(strEvent.ToString().Substring(intStart + 14, intLength));
                strEventAlternateItem = new StringBuilder(strEventItem.ToString());

                strEvent = strEvent.Remove(intStart, (intStopp - intStart) + 15);
                strEvent.Insert(intStart, Constants.K_ITEM);
            }

            intStart = strEvent.ToString().IndexOf("<alternatingitemtemplate>", StringComparison.InvariantCultureIgnoreCase);
            intStopp = strEvent.ToString().IndexOf("</alternatingitemtemplate>", StringComparison.InvariantCultureIgnoreCase);

            if (intStart >= 0 && intStopp > intStart)
            {
                strEventAlternateItem = new StringBuilder();

                int intLength = intStopp - (intStart + 25);
                strEventAlternateItem.Append(strEvent.ToString().Substring(intStart + 25, intLength));

                strEvent = strEvent.Remove(intStart, (intStopp - intStart) + 26);
            }
        }

        private string EventLink(int objectType, string objectID)
        {
            return Helper.GetDetailLink(objectType, objectID);
        }

        private string UserLink(string userID)
        {
            return string.Concat(strWebRoot, Common.Constants.Links["LINK_TO_USER_DETAIL"].Url, userID);
        }

        private string CommunityLink(string communityID)
        {
            return string.Concat(strWebRoot, Common.Constants.Links["LINK_TO_COMMUNITY_DETAIL"].Url, communityID);
        }

        public string Body
        {
            get
            {
                return strBase.Replace(Constants.K_LIST, strEventList.ToString()).ToString();
            }
        }

    }
}
