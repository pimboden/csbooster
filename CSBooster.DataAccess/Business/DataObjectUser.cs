//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Data.SqlClient;
using _4screen.CSB.Common;
using _4screen.CSB.Notification.Business;
using _4screen.CSB.Common.Notification;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "User")]
    public class DataObjectUser : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        internal XmlDocument emphasisListXml;
        private string vorname;
  
        #region Properties
        public Dictionary<int, int> EmphasisList
        {
            get { return Data.DataObjectsHelper.GetEmphasisList(emphasisListXml.DocumentElement); }
            internal set
            {
                Data.DataObjectsHelper.SetEmphasisList(emphasisListXml.DocumentElement, value);
            }
        }

        
        public bool IsUserOnline { get; set; }
        public string OpenID { get; set; }
        public string PPID { get; set; }
        public string Vorname
        {
            get
            {
                if (VornameShow || ShowProperty)
                    return vorname;
                else
                    return string.Empty;
            }
            set { vorname = value; }
        }
        public bool VornameShow { get; set; }
        private string name;
        public string Name
        {
            get
            {
                if (NameShow || ShowProperty)
                    return name;
                else
                    return string.Empty;
            }

            set { name = value; }
        }

        public bool NameShow { get; set; }
        private string addressStreet;
        public string AddressStreet
        {
            get
            {
                if (AddressStreetShow || ShowProperty)
                    return addressStreet;
                else
                    return string.Empty;
            }
            set { addressStreet = value; }
        }

        public bool AddressStreetShow { get; set; }
        private string addressZip;
        public string AddressZip
        {
            get
            {
                if (AddressZipShow || ShowProperty)
                    return addressZip;
                else
                    return string.Empty;
            }
            set { addressZip = value; }
        }

        public bool AddressZipShow { get; set; }
        private string addressCity;
        public string AddressCity
        {
            get
            {
                if (AddressCityShow || ShowProperty)
                    return addressCity;
                else
                    return string.Empty;
            }
            set { addressCity = value; }
        }

        public bool AddressCityShow { get; set; }
        private string addressLand;
        public string AddressLand
        {
            get
            {
                if (AddressLandShow || ShowProperty)
                    return addressLand;
                else
                    return string.Empty;
            }
            set { addressLand = value; }
        }

        public bool AddressLandShow { get; set; }
        private string languages;
        public string Languages
        {
            get
            {
                if (LanguagesShow || ShowProperty)
                    return languages;
                else
                    return string.Empty;
            }
            set { languages = value; }
        }

        public bool LanguagesShow { get; set; }
        private string sex;
        public string Sex
        {
            get
            {
                if (SexShow || ShowProperty)
                    return sex;
                else
                    return string.Empty;
            }
            set { sex = value; }
        }

        public bool SexShow { get; set; }
        private DateTime? birthday;
        public DateTime? Birthday
        {
            get
            {
                if (BirthdayShow || ShowProperty)
                    return birthday;
                else
                    return null;
            }
            set { birthday = value; }
        }

        public bool BirthdayShow { get; set; }
        private string relationStatus;
        public string RelationStatus
        {
            get
            {
                if (RelationStatusShow || ShowProperty)
                    return relationStatus;
                else
                    return string.Empty;
            }
            set { relationStatus = value; }
        } //DB: Status
        public bool RelationStatusShow { get; set; }//DB: StatusShow
        private string attractedTo;
        public string AttractedTo
        {
            get
            {
                if (AttractedToShow || ShowProperty)
                    return attractedTo;
                else
                    return string.Empty;
            }
            set { attractedTo = value; }
        }

        public bool AttractedToShow { get; set; }
        private int bodyHeight;
        public int BodyHeight
        {
            get
            {
                if (BodyHeightShow || ShowProperty)
                    return bodyHeight;
                else
                    return 0;
            }
            set { bodyHeight = value; }
        }

        public bool BodyHeightShow { get; set; }
        private int bodyWeight;
        public int BodyWeight
        {
            get
            {
                if (BodyWeightShow || ShowProperty)
                    return bodyWeight;
                else
                    return 0;
            }
            set { bodyWeight = value; }
        }

        public bool BodyWeightShow { get; set; }
        private string eyeColor;
        public string EyeColor
        {
            get
            {
                if (EyeColorShow || ShowProperty)
                    return eyeColor;
                else
                    return string.Empty;
            }
            set { eyeColor = value; }
        }

        public bool EyeColorShow { get; set; }
        private string hairColor;
        public string HairColor
        {
            get
            {
                if (HairColorShow || ShowProperty)
                    return hairColor;
                else
                    return string.Empty;
            }
            set { hairColor = value; }
        }

        public bool HairColorShow { get; set; }
        private string primaryColor;
        public string PrimaryColor
        {
            get { return primaryColor; }
            set { primaryColor = value; }
        } //DB: PriColor
        public bool PrimaryColorShow { get; set; }//DB: PriColorShow
        private string secondaryColor;
        public string SecondaryColor
        {
            get { return secondaryColor; }
            set { secondaryColor = value; }
        } //DB: SecColor
        public bool SecondaryColorShow { get; set; }//DB: SecColorShow
        private string mobile;
        public string Mobile
        {
            get
            {
                if (MobileShow || ShowProperty)
                    return mobile;
                else
                    return string.Empty;
            }
            set { mobile = value; }
        }

        public bool MobileShow { get; set; }
        private string phone;
        public string Phone
        {
            get
            {
                if (PhoneShow || ShowProperty)
                    return phone;
                else
                    return string.Empty;
            }
            set { phone = value; }
        }

        public bool PhoneShow { get; set; }
        private string msn;
        public string MSN
        {
            get
            {
                if (MSNShow || ShowProperty)
                    return msn;
                else
                    return string.Empty;
            }
            set { msn = value; }
        }

        public bool MSNShow { get; set; }
        private string yahoo;
        public string Yahoo
        {
            get
            {
                if (YahooShow || ShowProperty)
                    return yahoo;
                else
                    return string.Empty;
            }
            set { yahoo = value; }
        }

        public bool YahooShow { get; set; }
        private string skype;
        public string Skype
        {
            get
            {
                if (SkypeShow || ShowProperty)
                    return skype;
                else
                    return string.Empty;
            }
            set { skype = value; }
        }

        public bool SkypeShow { get; set; }
        private string icq;
        public string ICQ
        {
            get
            {
                if (ICQShow || ShowProperty)
                    return icq;
                else
                    return string.Empty;
            }
            set { icq = value; }
        }

        public bool ICQShow { get; set; }
        private string aim;
        public string AIM
        {
            get
            {
                if (AIMShow || ShowProperty)
                    return aim;
                else
                    return string.Empty;
            }
            set { aim = value; }
        }

        public bool AIMShow { get; set; }
        private string homepage;
        public string Homepage
        {
            get
            {
                if (HomepageShow || ShowProperty)
                    return homepage;
                else
                    return string.Empty;
            }
            set { homepage = value; }
        }

        public bool HomepageShow { get; set; }
        private string blog;
        public string Blog
        {
            get
            {
                if (BlogShow || ShowProperty)
                    return blog;
                else
                    return string.Empty;
            }
            set { blog = value; }
        }

        public bool BlogShow { get; set; }
        private string beruf;
        public string Beruf
        {
            get
            {
                if (BerufShow || ShowProperty)
                    return beruf;
                else
                    return string.Empty;
            }
            set { beruf = value; }
        }

        public bool BerufShow { get; set; }
        private string lebensmoto;
        public string Lebensmoto
        {
            get
            {
                if (LebensmotoShow || ShowProperty)
                    return lebensmoto;
                else
                    return string.Empty;
            }
            set { lebensmoto = value; }
        }

        public bool LebensmotoShow { get; set; }
        private string talent1;
        public string Talent1
        {
            get
            {
                if (Talent1Show || ShowProperty)
                    return talent1;
                else
                    return string.Empty;
            }
            set { talent1 = value; }
        }

        public bool Talent1Show { get; set; }
        private string talent2;
        public string Talent2
        {
            get
            {
                if (Talent2Show || ShowProperty)
                    return talent2;
                else
                    return string.Empty;
            }
            set { talent2 = value; }
        }

        public bool Talent2Show { get; set; }
        private string talent3;
        public string Talent3
        {
            get
            {
                if (Talent3Show || ShowProperty)
                    return talent3;
                else
                    return string.Empty;
            }
            set { talent3 = value; }
        }

        public bool Talent3Show { get; set; }
        private string talent4;
        public string Talent4
        {
            get
            {
                if (Talent4Show || ShowProperty)
                    return talent4;
                else
                    return string.Empty;
            }
            set { talent4 = value; }
        }

        public bool Talent4Show { get; set; }
        private string talent5;
        public string Talent5
        {
            get
            {
                if (Talent5Show || ShowProperty)
                    return talent5;
                else
                    return string.Empty;
            }
            set { talent5 = value; }
        }

        public bool Talent5Show { get; set; }
        private string talent6;
        public string Talent6
        {
            get
            {
                if (Talent6Show || ShowProperty)
                    return talent6;
                else
                    return string.Empty;
            }
            set { talent6 = value; }
        }

        public bool Talent6Show { get; set; }
        private string talent7;
        public string Talent7
        {
            get
            {
                if (Talent7Show || ShowProperty)
                    return talent7;
                else
                    return string.Empty;
            }
            set { talent7 = value; }
        }

        public bool Talent7Show { get; set; }
        private string talent8;
        public string Talent8
        {
            get
            {
                if (Talent8Show || ShowProperty)
                    return talent8;
                else
                    return string.Empty;
            }
            set { talent8 = value; }
        }

        public bool Talent8Show { get; set; }
        private string talent9;
        public string Talent9
        {
            get
            {
                if (Talent9Show || ShowProperty)
                    return talent9;
                else
                    return string.Empty;
            }
            set { talent9 = value; }
        }

        public bool Talent9Show { get; set; }
        private string talent10;
        public string Talent10
        {
            get
            {
                if (Talent10Show || ShowProperty)
                    return talent10;
                else
                    return string.Empty;
            }
            set { talent10 = value; }
        }

        public bool Talent10Show { get; set; }
        private string talent11;
        public string Talent11
        {
            get
            {
                if (Talent11Show || ShowProperty)
                    return talent11;
                else
                    return string.Empty;
            }
            set { talent11 = value; }
        }

        public bool Talent11Show { get; set; }
        private string talent12;
        public string Talent12
        {
            get
            {
                if (Talent12Show || ShowProperty)
                    return talent12;
                else
                    return string.Empty;
            }
            set { talent12 = value; }
        }

        public bool Talent12Show { get; set; }
        private string talent13;
        public string Talent13
        {
            get
            {
                if (Talent13Show || ShowProperty)
                    return talent13;
                else
                    return string.Empty;
            }
            set { talent13 = value; }
        }

        public bool Talent13Show { get; set; }
        private string talent14;
        public string Talent14
        {
            get
            {
                if (Talent14Show || ShowProperty)
                    return talent14;
                else
                    return string.Empty;
            }
            set { talent14 = value; }
        }

        public bool Talent14Show { get; set; }
        private string talent15;
        public string Talent15
        {
            get
            {
                if (Talent15Show || ShowProperty)
                    return talent15;
                else
                    return string.Empty;
            }
            set { talent15 = value; }
        }

        public bool Talent15Show { get; set; }
        private string talent16;
        public string Talent16
        {
            get
            {
                if (Talent16Show || ShowProperty)
                    return talent16;
                else
                    return string.Empty;
            }
            set { talent16 = value; }
        }

        public bool Talent16Show { get; set; }
        private string talent17;
        public string Talent17
        {
            get
            {
                if (Talent17Show || ShowProperty)
                    return talent17;
                else
                    return string.Empty;
            }
            set { talent17 = value; }
        }

        public bool Talent17Show { get; set; }
        private string talent18;
        public string Talent18
        {
            get
            {
                if (Talent18Show || ShowProperty)
                    return talent18;
                else
                    return string.Empty;
            }
            set { talent18 = value; }
        }

        public bool Talent18Show { get; set; }
        private string talent19;
        public string Talent19
        {
            get
            {
                if (Talent19Show || ShowProperty)
                    return talent19;
                else
                    return string.Empty;
            }
            set { talent19 = value; }
        }

        public bool Talent19Show { get; set; }
        private string talent20;
        public string Talent20
        {
            get
            {
                if (Talent20Show || ShowProperty)
                    return talent20;
                else
                    return string.Empty;
            }
            set { talent20 = value; }
        }

        public bool Talent20Show { get; set; }
        private string talent21;
        public string Talent21
        {
            get
            {
                if (Talent21Show || ShowProperty)
                    return talent21;
                else
                    return string.Empty;
            }
            set { talent21 = value; }
        }

        public bool Talent21Show { get; set; }
        private string talent22;
        public string Talent22
        {
            get
            {
                if (Talent22Show || ShowProperty)
                    return talent22;
                else
                    return string.Empty;
            }
            set { talent22 = value; }
        }

        public bool Talent22Show { get; set; }
        private string talent23;
        public string Talent23
        {
            get
            {
                if (Talent23Show || ShowProperty)
                    return talent23;
                else
                    return string.Empty;
            }
            set { talent23 = value; }
        }

        public bool Talent23Show { get; set; }
        private string talent24;
        public string Talent24
        {
            get
            {
                if (Talent24Show || ShowProperty)
                    return talent24;
                else
                    return string.Empty;
            }
            set { talent24 = value; }
        }

        public bool Talent24Show { get; set; }
        private string talent25;
        public string Talent25
        {
            get
            {
                if (Talent25Show || ShowProperty)
                    return talent25;
                else
                    return string.Empty;
            }
            set { talent25 = value; }
        }

        public bool Talent25Show { get; set; }
        private string interestTopic1;
        public string InterestTopic1
        {
            get
            {
                if (InterestTopic1Show || ShowProperty)
                    return interestTopic1;
                else
                    return string.Empty;
            }
            set { interestTopic1 = value; }
        }

        public bool InterestTopic1Show { get; set; }
        private string interestTopic2;
        public string InterestTopic2
        {
            get
            {
                if (InterestTopic2Show || ShowProperty)
                    return interestTopic2;
                else
                    return string.Empty;
            }
            set { interestTopic2 = value; }
        }

        public bool InterestTopic2Show { get; set; }
        private string interestTopic3;
        public string InterestTopic3
        {
            get
            {
                if (InterestTopic3Show || ShowProperty)
                    return interestTopic3;
                else
                    return string.Empty;
            }
            set { interestTopic3 = value; }
        }

        public bool InterestTopic3Show { get; set; }
        private string interestTopic4;
        public string InterestTopic4
        {
            get
            {
                if (InterestTopic4Show || ShowProperty)
                    return interestTopic4;
                else
                    return string.Empty;
            }
            set { interestTopic4 = value; }
        }

        public bool InterestTopic4Show { get; set; }
        private string interestTopic5;
        public string InterestTopic5
        {
            get
            {
                if (InterestTopic5Show || ShowProperty)
                    return interestTopic5;
                else
                    return string.Empty;
            }
            set { interestTopic5 = value; }
        }

        public bool InterestTopic5Show { get; set; }
        private string interestTopic6;
        public string InterestTopic6
        {
            get
            {
                if (InterestTopic6Show || ShowProperty)
                    return interestTopic6;
                else
                    return string.Empty;
            }
            set { interestTopic6 = value; }
        }

        public bool InterestTopic6Show { get; set; }
        private string interestTopic7;
        public string InterestTopic7
        {
            get
            {
                if (InterestTopic7Show || ShowProperty)
                    return interestTopic7;
                else
                    return string.Empty;
            }
            set { interestTopic7 = value; }
        }

        public bool InterestTopic7Show { get; set; }
        private string interestTopic8;
        public string InterestTopic8
        {
            get
            {
                if (InterestTopic8Show || ShowProperty)
                    return interestTopic8;
                else
                    return string.Empty;
            }
            set { interestTopic8 = value; }
        }

        public bool InterestTopic8Show { get; set; }
        private string interestTopic9;
        public string InterestTopic9
        {
            get
            {
                if (InterestTopic9Show || ShowProperty)
                    return interestTopic9;
                else
                    return string.Empty;
            }
            set { interestTopic9 = value; }
        }

        public bool InterestTopic9Show { get; set; }
        private string interestTopic10;
        public string InterestTopic10
        {
            get
            {
                if (InterestTopic10Show || ShowProperty)
                    return interestTopic10;
                else
                    return string.Empty;
            }
            set { interestTopic10 = value; }
        }

        public bool InterestTopic10Show { get; set; }
        private string interesst1;
        public string Interesst1
        {
            get
            {
                if (Interesst1Show || ShowProperty)
                    return interesst1;
                else
                    return string.Empty;
            }
            set { interesst1 = value; }
        }

        public bool Interesst1Show { get; set; }
        private string interesst2;
        public string Interesst2
        {
            get
            {
                if (Interesst2Show || ShowProperty)
                    return interesst2;
                else
                    return string.Empty;
            }
            set { interesst2 = value; }
        }

        public bool Interesst2Show { get; set; }
        private string interesst3;
        public string Interesst3
        {
            get
            {
                if (Interesst3Show || ShowProperty)
                    return interesst3;
                else
                    return string.Empty;
            }
            set { interesst3 = value; }
        }

        public bool Interesst3Show { get; set; }
        private string interesst4;
        public string Interesst4
        {
            get
            {
                if (Interesst4Show || ShowProperty)
                    return interesst4;
                else
                    return string.Empty;
            }
            set { interesst4 = value; }
        }

        public bool Interesst4Show { get; set; }
        private string interesst5;
        public string Interesst5
        {
            get
            {
                if (Interesst5Show || ShowProperty)
                    return interesst5;
                else
                    return string.Empty;
            }
            set { interesst5 = value; }
        }

        public bool Interesst5Show { get; set; }
        private string interesst6;
        public string Interesst6
        {
            get
            {
                if (Interesst6Show || ShowProperty)
                    return interesst6;
                else
                    return string.Empty;
            }
            set { interesst6 = value; }
        }

        public bool Interesst6Show { get; set; }
        private string interesst7;
        public string Interesst7
        {
            get
            {
                if (Interesst7Show || ShowProperty)
                    return interesst7;
                else
                    return string.Empty;
            }
            set { interesst7 = value; }
        }

        public bool Interesst7Show { get; set; }
        private string interesst8;
        public string Interesst8
        {
            get
            {
                if (Interesst8Show || ShowProperty)
                    return interesst8;
                else
                    return string.Empty;
            }
            set { interesst8 = value; }
        }

        public bool Interesst8Show { get; set; }
        private string interesst9;
        public string Interesst9
        {
            get
            {
                if (Interesst9Show || ShowProperty)
                    return interesst9;
                else
                    return string.Empty;
            }
            set { interesst9 = value; }
        }

        public bool Interesst9Show { get; set; }
        private string interesst10;
        public string Interesst10
        {
            get
            {
                if (Interesst10Show || ShowProperty)
                    return interesst10;
                else
                    return string.Empty;
            }
            set { interesst10 = value; }
        }

        public bool Interesst10Show { get; set; }
        public string MsgFrom { get; set; }
        public bool MsgFromShow { get; set; }
        public string GetMail { get; set; }
        public bool GetMailShow { get; set; }
        public bool DisplayAds { get; set; }
        public bool DisplayAdsShow { get; set; }
        public int Age
        {
            get
            {
                if (BirthdayShow && Birthday.HasValue)
                {
                    DateTime now = DateTime.Today;
                    int years = now.Year - Birthday.Value.Year;
                    if (now.Month < Birthday.Value.Month || (now.Month == Birthday.Value.Month && now.Day < Birthday.Value.Day))
                        --years;
                    return years;
                }
                else
                    return 0;
            }
        }

        private bool ShowProperty
        {
            get
            {
                return (this.UserID == this.UserDataContext.UserID || !this.ObjectID.HasValue || this.UserDataContext.IsAdmin); 
            }
        }
        #endregion

        #region Constructors
        #endregion

        public DataObjectUser()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectUser(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("User").NumericId;
            emphasisListXml = new XmlDocument();
            XmlHelper.CreateRoot(emphasisListXml, "Root");
            title = userDataContext.Nickname;
        }

        #region Public Methods
        #endregion

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);

            if (!UserID.HasValue)
                throw new SiemeArgumentException("DataObjectUser", "Validate", "UserId", "UserId must not be empty");

            if (string.IsNullOrEmpty(Nickname))
                throw new SiemeArgumentException("DataObjectUser", "Validate", "Nickname", "Nickname must not be empty");

            if (string.IsNullOrEmpty(Title))
                Title = Nickname;
        }

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectUser));
            XmlDocument xmlDocument = new XmlDocument();
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, this);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            xmlDocument.Load(stream);
            stream.Close();

            SerializationType = SerializationType.Transfer;
            return xmlDocument;
        }

        public string GetOutput(string outputType, string templatesUrl, XsltArgumentList argumentList)
        {
            string baseUrlXSLT = string.Empty;
            if (PartnerID.HasValue)
            {
                Partner partner = Partner.Get(PartnerID.Value, null);
                if (partner != null && !string.IsNullOrEmpty(partner.BaseUrlXSLT))
                    baseUrlXSLT = partner.BaseUrlXSLT;
            }
            return Helper.TransformDataObject(ObjectType, GetXml(), UrlXSLT, outputType, baseUrlXSLT, templatesUrl, argumentList);
        }

        #region Public static Methods
        #endregion

        public static Guid? GetUserIDByNickname(string Nickname)
        {
            Data.DataObject dObj = new Data.DataObject();
            return dObj.GetUserIDByNickname(Nickname);
        }

        public static void CreateUser(string nickName)
        {
            UserDataContext userDataContext = UserDataContext.GetUserDataContext(nickName);
            DataObjectUser user = new DataObjectUser(userDataContext);
            user.CommunityID = System.Configuration.ConfigurationManager.AppSettings["DefaultCommunityID"].ToGuid();
            user.Status = ObjectStatus.Public;
            user.UserID = userDataContext.UserID;
            user.Nickname = nickName;
            user.PrimaryColor = "3";
            user.SecondaryColor = "5";
            user.Sex = ";-1;";
            user.Insert();
        }

        public static void JoinCommunities(string userId, SiteContext siteContext)
        {
            XmlDocument XmlDefaultWidgets = new XmlDocument();
            StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}/Configurations/DefaultCommJoin.rule", siteContext.VRoot)));
            XmlDefaultWidgets.LoadXml(sr.ReadToEnd());
            sr.Close();
            XmlNodeList xmlCommunities = XmlDefaultWidgets.SelectNodes(string.Format("//root/Community"));
            foreach (XmlNode xmlCom in xmlCommunities)
            {
                string ctyID = xmlCom.Attributes["ID"].Value;
                DataObjectCommunity doComm = DataObject.Load<DataObjectCommunity>(ctyID.ToGuid());
                if (doComm.State != ObjectState.Added)
                {
                    try
                    {
                        CSBooster_DataContext wdc = new CSBooster_DataContext();
                        hirel_Community_User_CUR HirelCommUsr = new hirel_Community_User_CUR();
                        HirelCommUsr.CTY_ID = new Guid(ctyID);
                        HirelCommUsr.USR_ID = new Guid(userId);
                        HirelCommUsr.CUR_IsOwner = false;
                        HirelCommUsr.CUR_Status = 0;
                        HirelCommUsr.CUR_InsertedDate = DateTime.Now;
                        HirelCommUsr.USR_ID_InvitedBy = null;
                        wdc.hirel_Community_User_CURs.InsertOnSubmit(HirelCommUsr);
                        wdc.SubmitChanges();

                        XmlNodeList xmlNotifications = xmlCom.SelectNodes("Notification");
                        foreach (XmlNode xmlNoti in xmlNotifications)
                        {
                            int evtId = Convert.ToInt32(xmlNoti.Attributes["EventIdentifier"].Value);
                            int cType = Convert.ToInt32(xmlNoti.Attributes["CarrierType"].Value);
                            string strForObjType = xmlNoti.Attributes["ForObjectType"].Value;
                            CarrierCollect cColl = (CarrierCollect)Convert.ToInt32(xmlNoti.Attributes["CarrierCollect"].Value);

                            DateTime datNext;

                            if (cColl == CarrierCollect.Daily)
                            {
                                datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
                            }
                            else if (cColl == CarrierCollect.Weekly)
                            {
                                int days = (int)DateTime.Now.DayOfWeek;
                                datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(7 - days);
                            }
                            else if (cColl == CarrierCollect.Monthly)
                            {
                                datNext = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
                            }
                            else
                            {
                                datNext = new DateTime(1900, 1, 1);
                            }
                            string strNotiTitle = string.Format("Community : {0}", doComm.Title);
                            hitbl_Notification_RegisteredEvent_NRE notification = new hitbl_Notification_RegisteredEvent_NRE();
                            notification.NRE_ID = Guid.NewGuid();
                            notification.NUS_USR_ID = new Guid(userId);
                            notification.NRE_IsGlobal = 1;
                            notification.NRE_Identifier = evtId;
                            notification.NRE_ForObjectID = new Guid(ctyID);
                            notification.NRE_ForObjectType = strForObjType;
                            notification.NRE_Title = strNotiTitle;
                            notification.NRE_Carrier = cType;
                            notification.NRE_CarrierCollect = (int)cColl;
                            notification.NRE_CollectValue = 1;
                            notification.NRE_AddDate = DateTime.Now;
                            notification.NRE_NextSend = datNext;
                            wdc.hitbl_Notification_RegisteredEvent_NREs.InsertOnSubmit(notification);
                            wdc.SubmitChanges();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void AddDefaultFriends(string userId)
        {
            try
            {
                foreach (Guid friendId in DataAccessConfiguration.GetDefaultUserFriends())
                {
                    CSBooster_DataContext wdc = new CSBooster_DataContext();
                    hitbl_UserFriends_FRI FriendForUser = new hitbl_UserFriends_FRI();
                    FriendForUser.ASP_UserId = new Guid(userId);
                    FriendForUser.ASP_FriendId = friendId;
                    FriendForUser.UFR_TypeID = 1;
                    FriendForUser.FRI_DateAdded = DateTime.Now;
                    FriendForUser.FRI_Blocked = 0;
                    wdc.hitbl_UserFriends_FRIs.InsertOnSubmit(FriendForUser);

                    hitbl_UserFriends_FRI FriendForAdmin = new hitbl_UserFriends_FRI();
                    FriendForAdmin.ASP_UserId = friendId;
                    FriendForAdmin.ASP_FriendId = new Guid(userId);
                    FriendForAdmin.UFR_TypeID = 1;
                    FriendForAdmin.FRI_DateAdded = DateTime.Now;
                    FriendForAdmin.FRI_Blocked = 0;
                    wdc.hitbl_UserFriends_FRIs.InsertOnSubmit(FriendForAdmin);
                    wdc.SubmitChanges();
                }
            }
            catch
            {
            }
        }

        public static ReceiveMessageFrom GetAllowedMessages(Guid UserId)
        {
            ReceiveMessageFrom retVal = ReceiveMessageFrom.All;

            DataObjectUser user = DataObject.Load<DataObjectUser>(UserId);
            switch (user.MsgFrom)
            {
                case ";All;":
                    retVal = ReceiveMessageFrom.All;
                    break;
                case ";Friends;":
                    retVal = ReceiveMessageFrom.Friends;
                    break;
                case ";Nobody;":
                    retVal = ReceiveMessageFrom.None;
                    break;
            }

            return retVal;
        }

        public static YesNo GetSendEmailOnMessage(Guid UserId)
        {
            YesNo retVal = YesNo.Yes;

            DataObjectUser user = DataObject.Load<DataObjectUser>(UserId);
            switch (user.GetMail)
            {
                case ";Yes;":
                    retVal = YesNo.Yes;
                    break;
                case ";No;":
                    retVal = YesNo.No;
                    break;
            }

            return retVal;
        }


        public static int IsOnlineTotal()
        {
            return Data.DataObjectUser.IsOnlineTotal();
        }

        public static bool IsUserLockedOut(Guid uid)
        {
            return Data.DataObjectUser.IsUserLockedOut(uid);
        }

        public static void LockOut(Guid uid)
        {
            Data.DataObjectUser.LockOut(uid);
        }

        public static void Unlock(Guid uid)
        {
            Data.DataObjectUser.Unlock(uid);
        }


        #region Read / Write Methods
        #endregion

        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectUser.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectUser.GetOrderBySQL(qParas, parameters);
        }

        #region IXmlSerializable
        #endregion

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public new void ReadXml(System.Xml.XmlReader reader)
        {
            while (reader.Read())
            {
                base.ReadXml(reader);

                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    //switch (reader.Name)
                    //{
                    //   //case "XYZ": this.XYZ = reader.ReadString(); break;
                    //}
                }
            }

        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            //writer.WriteRaw(string.Format("<XYZ>{0}</XYZ>", this.GenericData));
        }

    }

    public class NicknameSorterUser : IComparer<DataObjectUser>
    {
        private bool blnDesc = false;

        public NicknameSorterUser(bool desc)
        {
            blnDesc = desc;
        }

        public NicknameSorterUser()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectUser x, DataObjectUser y)
        {
            if (!blnDesc)
                return StringLogicalComparer.Compare(x.Nickname, y.Nickname);

            else
                return StringLogicalComparer.Compare(y.Nickname, x.Nickname);
        }
    }

    public class ViewCountSorterUser : IComparer<DataObjectUser>
    {
        private bool blnDesc = false;

        public ViewCountSorterUser(bool desc)
        {
            blnDesc = desc;
        }

        public ViewCountSorterUser()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectUser x, DataObjectUser y)
        {
            if (!blnDesc)
                return x.ViewCount.CompareTo(y.ViewCount);

            else
                return y.ViewCount.CompareTo(x.ViewCount);
        }
    }

    public class InsertedSorterUser : IComparer<DataObjectUser>
    {
        private bool blnDesc = false;

        public InsertedSorterUser(bool desc)
        {
            blnDesc = desc;
        }

        public InsertedSorterUser()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectUser x, DataObjectUser y)
        {
            if (!blnDesc)
                return x.Inserted.CompareTo(y.Inserted);

            else
                return y.Inserted.CompareTo(x.Inserted);
        }
    }

}
