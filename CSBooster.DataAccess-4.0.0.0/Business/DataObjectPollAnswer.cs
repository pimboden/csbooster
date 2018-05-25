// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{

    [XmlRoot(ElementName = "PollAnswer")]
    public class DataObjectPollAnswer : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        public int Answer { get; set; }
        public int Position { get; internal set; }
        public string Comment { get; set; }
        public string IP { get; internal set; }

        public DataObjectPollAnswer()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectPollAnswer(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("PollAnswer").NumericId;
            IP = userDataContext.UserIP;  
            Answer = 1;
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectPollAnswer.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPollAnswer.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectPollAnswer));
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
                    switch (reader.Name)
                    {
                        case "Answer":
                            Answer = reader.ReadString().ToInt32(1);
                            break;
                        case "Position":
                            Position = reader.ReadString().ToInt32(1);
                            break;
                        case "Comment":
                            Comment = reader.ReadString();
                            break;
                        case "IP":
                            IP = reader.ReadString();
                            break;
                    }
                }
            }
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteElementString("Answer", Answer.ToString());
            writer.WriteElementString("Position", Position.ToString());
            writer.WriteElementString("Comment", Comment);
            writer.WriteElementString("IP", IP);
        }

        #endregion
    }

}