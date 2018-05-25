// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "Community")]
    public class DataObjectCommunity : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        internal XmlDocument emphasisListXml;

        public string VirtualURL { get; set; }
        public bool Managed { get; set; }
        public CommunityUsersType CreateGroupUser { get; set; }
        public CommunityUsersType UploadUsers { get; set; }

        public Dictionary<int, int> EmphasisList
        {
            get { return Data.DataObjectsHelper.GetEmphasisList(emphasisListXml.DocumentElement); }
            internal set { Data.DataObjectsHelper.SetEmphasisList(emphasisListXml.DocumentElement, value); objectState = ObjectState.Changed; }
        }

        public DataObjectCommunity()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectCommunity(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Community").NumericId;
            emphasisListXml = new XmlDocument();
            XmlHelper.CreateRoot(emphasisListXml, "Root");
            CreateGroupUser = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), SiteConfig.GetSetting("DefaultCreateGroupUsers"));
            UploadUsers = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), SiteConfig.GetSetting("DefaultUploadUsers"));
        }

        public new static bool IsUserOwner(Guid communityId, Guid userId)
        {
            return Data.DataObject.IsUserCommunityOwner(communityId, userId);
        }

        public static Guid? GetCommunityIDByVirtualURL(string virtualUrl)
        {
            Data.DataObject dObj = new Data.DataObject();
            return dObj.GetCommunityIDByVirtualURL(virtualUrl);
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectCommunity.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectCommunity.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectCommunity));
            XmlDocument xmlDocument = new XmlDocument();
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, this);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            xmlDocument.Load(stream);
            stream.Close();

            SerializationType = SerializationType.Transfer;
            return xmlDocument;
        }

        public override string GetOutput(string outputType, string templatesUrl, XsltArgumentList argumentList)
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

        #region IXmlSerializable

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

            //writer.WriteRaw(string.Format("<XYZ>{0}</XYZ>", this.XYZ));
        }

        #endregion
    }
}