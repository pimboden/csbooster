//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.5.0.0    11.06.2008 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Data.SqlClient;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "Generic")]
    public class DataObjectGeneric : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        public string GenericData { get; set; }

        public DataObjectGeneric()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectGeneric(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Generic").NumericId;
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectGeneric.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectGeneric.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectGeneric));
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
                    switch (reader.Name)
                    {
                        case "GenericData":
                            string innerXML = reader.ReadInnerXml();
                            GenericData = innerXML;
                            break;

                    }
                }
            }
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteRaw(string.Format("<GenericData>{0}</GenericData>", GenericData));
        }

        #endregion
    }
}