//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//******************************************************************************
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class Message
    {
        private Guid _MsgID;

        public Guid MsgID
        {
            get { return _MsgID; }
            set { _MsgID = value; }
        }

        private Guid _UserId;

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Guid _FromUserID;

        public Guid FromUserID
        {
            get { return _FromUserID; }
            set { _FromUserID = value; }
        }

        private string _FromUserName;

        public string FromUserName
        {
            get { return _FromUserName; }
            set { _FromUserName = value; }
        }

        private int _TypOfMessage;

        public int TypOfMessage
        {
            get { return _TypOfMessage; }
            set { _TypOfMessage = value; }
        }

        private DateTime _DateSent;

        public DateTime DateSent
        {
            get { return _DateSent; }
            set { _DateSent = value; }
        }

        private string _Subject;

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        private string _MsgText;

        public string MsgText
        {
            get { return _MsgText; }
            set { _MsgText = value; }
        }

        private bool _IsRead;

        public bool IsRead
        {
            get { return _IsRead; }
            set { _IsRead = value; }
        }

        private bool _ShowInInbox;

        public bool ShowInInbox
        {
            get { return _ShowInInbox; }
            set { _ShowInInbox = value; }
        }

        private bool _ShowInOutbox;

        public bool ShowInOutbox
        {
            get { return _ShowInOutbox; }
            set { _ShowInOutbox = value; }
        }

        private bool _Flagged;

        public bool Flagged
        {
            get { return _Flagged; }
            set { _Flagged = value; }
        }

        private bool _Answered;

        public bool Answered
        {
            get { return _Answered; }
            set { _Answered = value; }
        }

        private bool _Forwarded;

        public bool Forwarded
        {
            get { return _Forwarded; }
            set { _Forwarded = value; }
        }

        private string _XMLData;

        public string XMLData
        {
            get { return _XMLData; }
            set { _XMLData = value; }
        }

        public SiteContext SiteContext { get; set; }

        public MessageState MessageState
        {
            get
            {
                MessageState state = MessageState.None;
                if (_Answered)
                    state = MessageState.Replied;
                if (_Forwarded)
                    state = MessageState.Forwarded;
                return state;
            }
        }

        public Message(SiteContext SiteContext)
        {
            this.SiteContext = SiteContext;
            _MsgID = Guid.NewGuid();
            _UserId = Guid.Empty;
            _UserName = string.Empty;
            _FromUserID = Guid.Empty;
            _FromUserName = string.Empty;
            _TypOfMessage = 0;
            _DateSent = DateTime.Now;
            _Subject = string.Empty;
            _MsgText = string.Empty;
            _IsRead = false;
            _ShowInInbox = true;
            _ShowInOutbox = true;
            _Flagged = false;
            _Answered = false;
            _Forwarded = false;
            _XMLData = string.Empty;
        }

        public void Load(Guid msgId)
        {
            try
            {
                CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

                var result = wdc.hisp_UserMessages_GetMessage(msgId).ElementAtOrDefault(0);
                if (result != null)
                {
                    FillMessage(this, result);
                }
            }
            catch
            {
            }
        }

        public void Save()
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            System.Xml.Linq.XElement xmlData = null;
            if (!string.IsNullOrEmpty(_XMLData))
            {
                xmlData = System.Xml.Linq.XElement.Parse(_XMLData);
            }
            wdc.hisp_UserMessages_SaveMessage(_MsgID, _UserId, _FromUserID, _TypOfMessage, _Subject, _MsgText, Convert.ToInt32(_IsRead), _ShowInInbox, ShowInOutbox, xmlData);
        }

        public bool SendNormalMessage()
        {
            return SendNormalMessage(false, false);
        }

        public bool SendNormalMessage(bool ignoreProfileMessageRules, bool ignoreProfileEmailRules)
        {
            //Check if the receiver has blocked the sender
            if (!FriendHandler.IsBlocked(_UserId, _FromUserID))
            {
                //Check if the sender is a friend of the receiver
                if (FriendHandler.IsFriend(_UserId, _FromUserID))
                    _TypOfMessage = (int)MessageTypes.FriendMessage;
                else
                    _TypOfMessage = (int)MessageTypes.NormalMessage;

                MembershipUser objToUser = Membership.GetUser(_UserId, false);

                ReceiveMessageFrom rmfUserSetting = DataObjectUser.GetAllowedMessages(_UserId);
                if (ignoreProfileMessageRules || rmfUserSetting == ReceiveMessageFrom.All || (rmfUserSetting == ReceiveMessageFrom.Friends && _TypOfMessage == (int)MessageTypes.FriendMessage))
                {
                    Save();
                    if (ignoreProfileEmailRules || DataObjectUser.GetSendEmailOnMessage(_UserId) == YesNo.Yes)
                    {
                        MembershipUser objFromUser = Membership.GetUser(_FromUserID, false);
                        SendMail(objFromUser.UserName, objToUser,  GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("LableMessagePrivateMessage"), GuiLanguage.GetGuiLanguage("Templates").GetString("EmailNewMessageReceived"));
                    }
                }
            }
            return true;
        }

        public static void SendEmail(string toEmail, string subject, string mailBody)
        {
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpSection.From);
                mailMessage.To.Add(new MailAddress(toEmail));
                mailMessage.Subject = subject;
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }
            catch
            {
            }
        }

        private void SendMail(string fromName, MembershipUser user, string subject, string mailBody)
        {
            SendMail(fromName, user.UserName, user.Email, subject, mailBody);
        }

        private void SendMail(string fromName, string toName, string toEmail, string subject, string mailBody)
        {
            try
            {
                mailBody = mailBody.Replace("<%SITE_URL%>", SiteContext.SiteURL);
                mailBody = mailBody.Replace("<%TO_USERNAME%>", toName);
                mailBody = mailBody.Replace("<%FROM_USERNAME%>", fromName);

                try
                {
                    SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(smtpSection.From, SiteContext.SiteName);
                    mailMessage.To.Add(new MailAddress(toEmail));
                    mailMessage.Subject = subject;
                    mailMessage.Body = mailBody;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Send(mailMessage);
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        public bool SendRequestsMessage()
        {
            //Check if the receiver has blocked the sender
            if (!FriendHandler.IsBlocked(_UserId, _FromUserID))
            {
                _TypOfMessage = (int)MessageTypes.FriendRequest;
                Save();
                MembershipUser objToUser = Membership.GetUser(_UserId, false);

                if (DataObjectUser.GetSendEmailOnMessage(_UserId) == YesNo.Yes)
                {
                    MembershipUser objFromUser = Membership.GetUser(_FromUserID, false);
                    SendMail(objFromUser.UserName, objToUser, GuiLanguage.GetGuiLanguage("Shared").GetString("CommandFriendshipQuery"), GuiLanguage.GetGuiLanguage("Templates").GetString("EmailNewFriendRequestReceived"));
                }
            }
            return true;
        }

        public bool SendInviteMessage()
        {
            //Check if the receiver has blocked the sender
            if (!FriendHandler.IsBlocked(_UserId, _FromUserID))
            {
                _TypOfMessage = (int)MessageTypes.InviteToCommunity;
                Save();
                MembershipUser objToUser = Membership.GetUser(_UserId, false);

                if (DataObjectUser.GetSendEmailOnMessage(_UserId) == YesNo.Yes)
                {
                    MembershipUser objFromUser = Membership.GetUser(_FromUserID, false);
                    SendMail(objFromUser.UserName, objToUser, "Einladung zur Privat-Community", GuiLanguage.GetGuiLanguage("Templates").GetString("EmailNewInvitationToCommunity"));
                }
            }
            return true;
        }

        public bool SendSystemMessage()
        {
            _TypOfMessage = (int)MessageTypes.SystemMessage;
            Save();
            MembershipUser objToUser = Membership.GetUser(_UserId, false);
            SendMail(SiteContext.SiteName, objToUser, GuiLanguage.GetGuiLanguage("Shared").GetString("LabelMessageImportant"), GuiLanguage.GetGuiLanguage("Templates").GetString("EmailNewSystemMessage"));
            return true;
        }

        public static void DeleteMessage(Guid msgId, Guid userId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            wdc.hisp_UserMessages_DeleteMessage(msgId, userId);
        }

        public static void ToggleMessageFlag(Guid msgId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);
            wdc.hisp_UserMessages_ToggleFlag(msgId);
        }

        public static void SetRead(Guid msgId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            wdc.hisp_UserMessages_SetRead(msgId);
        }

        public static void SetAnswered(Guid msgId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            wdc.hisp_UserMessages_SetAnswered(msgId);
        }

        public static void SetForwarded(Guid msgId)
        {
            CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

            wdc.hisp_UserMessages_SetForwarded(msgId);
        }

        public static Message LoadMessage(Guid msgId, SiteContext siteContext)
        {
            Message newMessage = null;

            try
            {
                CSBooster_DataContext wdc = new CSBooster_DataContext(ConfigurationManager.ConnectionStrings["CSBoosterConnectionString"].ConnectionString);

                var result = wdc.hisp_UserMessages_GetMessage(msgId).ElementAtOrDefault(0);
                if (result != null)
                {
                    newMessage = new Message(siteContext);
                    FillMessage(newMessage, result);
                }
            }
            catch
            {
            }
            return newMessage;
        }

        internal static void FillMessage(Message newMessage, MessageResult result)
        {
            newMessage._MsgID = result.MSG_MsgID;
            newMessage._UserId = result.ASP_UserId;
            newMessage._UserName = result.UserName ?? string.Empty;
            newMessage._FromUserID = result.ASP_FromUserID;
            newMessage._FromUserName = result.FromUserName ?? string.Empty;
            newMessage._TypOfMessage = result.MSG_TypOfMessage;
            newMessage._DateSent = result.MSG_DateSent;
            newMessage._Subject = result.MSG_Subject;
            newMessage._MsgText = result.MSG_Message;
            newMessage._IsRead = Convert.ToBoolean(result.MSG_IsRead);
            newMessage._ShowInInbox = result.MSG_ShowInInbox;
            newMessage._ShowInOutbox = result.MSG_ShowInOutbox;
            newMessage._Flagged = result.MSG_Flagged;
            newMessage._Answered = result.MSG_Answered;
            newMessage._Forwarded = result.MSG_Forwarded;
            newMessage._XMLData = result.MSG_XMLData != null ? result.MSG_XMLData.ToString(System.Xml.Linq.SaveOptions.DisableFormatting) : string.Empty;
        }

        public string GetXMLNodeValue(string XPath)
        {
            string strReturn = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(_XMLData);
                XmlNode xmlNode = xmlDoc.SelectSingleNode(XPath);
                if (xmlNode != null)
                {
                    strReturn = xmlNode.InnerXml;
                }
            }
            catch
            {
            }
            return strReturn;
        }

        public void SetXMLNode(string XPath, string strNodeValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(_XMLData);
                XmlNode xmlNode = xmlDoc.SelectSingleNode(XPath);
                if (xmlNode != null)
                {
                    xmlNode.InnerXml = strNodeValue;
                }
            }
            catch
            {
            }
        }
    }
}