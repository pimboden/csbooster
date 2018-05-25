//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.1.0.0    27.12.2007 / AW
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
    [XmlRoot(ElementName = "PinboardOffer")]
    public class DataObjectPinboardOffer : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        public string Price { get; set; }
        public int Amount { get; set; }

        public DataObjectPinboardOffer()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectPinboardOffer(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("PinboardOffer").NumericId;
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectPinboardOffer.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPinboardOffer.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectPinboardOffer));
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
                        case "Price":
                            Price = reader.ReadString();
                            break;
                        case "Amount":
                            string amount = reader.ReadString();
                            if (!string.IsNullOrEmpty(amount))
                                Amount = int.Parse(amount);
                            break;

                    }
                }
            }

        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteRaw(string.Format("<Price>{0}</Price>", Price));
            writer.WriteRaw(string.Format("<Amount>{0}</Amount>", Price));
            /*writer.WriteRaw(string.Format("<PinboardPic1>{0}</PinboardPic1>", this.PinboardPicture01Lg));
         writer.WriteRaw(string.Format("<PinboardPic2>{0}</PinboardPic2>", this.PinboardPicture02Lg));
         writer.WriteRaw(string.Format("<PinboardPic3>{0}</PinboardPic3>", this.PinboardPicture03Lg));*/
        }

        #endregion
    }
}