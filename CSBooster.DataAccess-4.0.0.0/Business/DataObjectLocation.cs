// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "Location")]
    public class DataObjectLocation : DataAccess.Business.DataObject, IXmlSerializable
    {
        public Uri Website { get; set; }

        public DataObjectLocation()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectLocation(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Location").NumericId;
        }

        #region IXmlSerializable Members

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
        }

        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectLocation));
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

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectLocation.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetSelectSQL(qParas);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetJoinSQL(qParas);
        }

        public override string GetWhereSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetWhereSQL(qParas);
        }

        public override string GetFullTextWhereSQL(DataAccess.Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetFullTextWhereSQL(qParas);
        }

        public override string GetOrderBySQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectLocation.GetOrderBySQL();
        }
        #endregion

        public void SetValuesFromQuerySting()
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpServerUtility Server = HttpContext.Current.Server;
            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        Geo_Lat = geoLat;
                        Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                Region = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                CountryCode = Server.UrlDecode(Request.QueryString["CO"]);
        }
    }
}