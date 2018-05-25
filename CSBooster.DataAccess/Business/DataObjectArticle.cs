//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Updated:   #1.0.4.0    08.11.2007 / AW
//******************************************************************************
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "Article")]
    public class DataObjectArticle : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        internal string articleTextLinked;

        public string ArticleText { get; set; }

        public string ArticleTextLinked
        {
            get
            {
                if (string.IsNullOrEmpty(articleTextLinked))
                    return ArticleText;
                else
                    return articleTextLinked;
            }
            internal set
            {
                if (articleTextLinked != value)
                    objectState = ObjectState.Changed;
                articleTextLinked = value;
            }
        }

        public DataObjectArticle()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectArticle(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Article").NumericId;
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);

            if (!string.IsNullOrEmpty(ArticleText) && ArticleText.Length > 1000000)
                throw new SiemeArgumentException("DataObjectArticle", "Validate", "ArticleText", "ArticleText is too long");
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectArticle.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectArticle.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectArticle));
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