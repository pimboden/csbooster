// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataObj.Business
{
    [XmlRoot(ElementName = "Product")]
    public class DataObjectProduct : DataAccess.Business.DataObject, IXmlSerializable
    {
        private double? _price1;
        private double? _price2;
        private double? _price3;
        private double? porto;
        private string _productRef;
        private string productText;
        internal string productTextLinked;

        public DataObjectProduct()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectProduct(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Product").NumericId;
        }

        public string ProductRef
        {
            get { return _productRef; }
            set
            {
                if (_productRef != value)
                    objectState = ObjectState.Changed;
                _productRef = value;
            }
        }

        public double? Price1
        {
            get { return _price1; }
            set
            {
                if (_price1 != value)
                    objectState = ObjectState.Changed;
                _price1 = value;
            }
        }

        public double? Porto
        {
            get { return porto; }
            set
            {
                if (porto != value)
                    objectState = ObjectState.Changed;
                porto = value;
            }
        }

        public double? Price2
        {
            get { return _price2; }
            set
            {
                if (_price2 != value)
                    objectState = ObjectState.Changed;
                _price2 = value;
            }
        }

        public double? Price3
        {
            get { return _price3; }
            set
            {
                if (_price3 != value)
                    objectState = ObjectState.Changed;
                _price3 = value;
            }
        }


        public string ProductText
        {
            get { return productText; }
            set
            {
                if (productText != value)
                    objectState = ObjectState.Changed;
                productText = value;
            }
        }

        public string ProductTextLinked
        {
            get
            {
                if (string.IsNullOrEmpty(productTextLinked))
                    return productText;
                else
                    return productTextLinked;
            }
            internal set
            {
                if (productTextLinked != value)
                    objectState = ObjectState.Changed;
                productTextLinked = value;
            }
        }


        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);
        }

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            var serializer = new XmlSerializer(typeof (DataObjectProduct));
            var xmlDocument = new XmlDocument();
            var stream = new MemoryStream();
            serializer.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
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
            return Helper.TransformDataObject(ObjectType, GetXml(), UrlXSLT, outputType, baseUrlXSLT, templatesUrl,
                                              argumentList);
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "ExtID":
                            ExternalObjectID = reader.ReadString();
                            break;
                        case "CtyID":
                            CommunityID = reader.ReadString().ToGuid();
                            break;
                        case "LangCode":
                            LangCode = reader.ReadString();
                            break;
                        case "StartDate":
                            string startDate = reader.ReadString();
                            if (!string.IsNullOrEmpty(startDate))
                                StartDate = DateTime.Parse(startDate);
                            break;
                        case "EndDate":
                            string endDate = reader.ReadString();
                            if (!string.IsNullOrEmpty(endDate))
                                EndDate = DateTime.Parse(endDate);
                            break;
                        case "Title":
                            Title = reader.ReadString();
                            break;
                        case "Desc":
                            Description = reader.ReadString();
                            break;
                        case "Pic":
                            Image = reader.ReadString();
                            break;
                        case "Copyright":
                            Copyright = int.Parse(reader.ReadString());
                            break;
                        case "Tags":
                            TagList = reader.ReadString();
                            break;
                        case "Priority":
                            Featured = int.Parse(reader.ReadString());
                            break;
                        case "GeoStreet":
                            Street = reader.ReadString();
                            break;
                        case "GeoZip":
                            Zip = reader.ReadString();
                            break;
                        case "GeoCity":
                            City = reader.ReadString();
                            break;
                        case "GeoCountry":
                            CountryCode = reader.ReadString();
                            break;
                        case "GeoLat":
                            string geoLat = reader.ReadString();
                            if (!string.IsNullOrEmpty(geoLat))
                                Geo_Lat = double.Parse(geoLat);
                            break;
                        case "GeoLong":
                            string geoLong = reader.ReadString();
                            if (!string.IsNullOrEmpty(geoLong))
                                Geo_Long = double.Parse(geoLong);
                            break;
                        case "UrlXSLT":
                            UrlXSLT = reader.ReadString();
                            break;
                            //case "XYZ": this.XYZ = reader.ReadString(); break;
                    }
                }
            }
        }

        public new void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            //writer.WriteRaw(string.Format("<XYZ>{0}</XYZ>", this.XYZ));
        }

        #endregion

        #region Read / Write Methods

        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectProduct.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetSelectSQL(qParas);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetJoinSQL(qParas);
        }

        public override string GetWhereSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetWhereSQL(qParas);
        }

        public override string GetFullTextWhereSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetFullTextWhereSQL(qParas);
        }

        public override string GetOrderBySQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectProduct.GetOrderBySQL();
        }

        #endregion
    }
}