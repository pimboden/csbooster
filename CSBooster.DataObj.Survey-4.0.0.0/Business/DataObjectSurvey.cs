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
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.DataObj.Business
{
    [XmlRoot(ElementName = "Survey")]
    public class DataObjectSurvey : DataAccess.Business.DataObject, IXmlSerializable
    {
        private string header;
        private string footer;
        private double punkteGruen;
        private double punkteGelb;
        private double punkteRot;
        private string headerLinked;
        private string footerLinked;
        private bool isContest;
        private string mailTo;
        private bool showForm ;


        public DataObjectSurvey()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectSurvey(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Survey").NumericId;
            Header = string.Empty;
            Footer = string.Empty;
            HeaderLinked = string.Empty;
            FooterLinked = string.Empty;
            PunkteGruen = 0;
            PunkteGelb = 0;
            PunkteRot = 0;
            isContest = false;
            showForm = false;
        }

        public string Header
        {
            get { return header; }
            set
            {
                if (header != value)
                    objectState = ObjectState.Changed;
                header = value;
            }
        }

        public string HeaderLinked
        {
            get
            {
                if (string.IsNullOrEmpty(headerLinked))
                    return header;
                else
                    return headerLinked;
            }
            internal set
            {
                if (headerLinked != value)
                    objectState = ObjectState.Changed;
                headerLinked = value;
            }
        }

        public string Footer
        {
            get { return footer; }
            set
            {
                if (footer != value)
                    objectState = ObjectState.Changed;
                footer = value;
            }
        }

        public string FooterLinked
        {
            get
            {
                if (string.IsNullOrEmpty(footerLinked))
                    return footer;
                else
                    return footerLinked;
            }
            internal set
            {
                if (footerLinked != value)
                    objectState = ObjectState.Changed;
                footerLinked = value;
            }
        }

        public double PunkteGruen
        {
            get { return punkteGruen; }
            set
            {
                if (punkteGruen != value)
                    objectState = ObjectState.Changed;
                punkteGruen = value;
            }
        }

        public double PunkteGelb
        {
            get { return punkteGelb; }
            set
            {
                if (punkteGelb != value)
                    objectState = ObjectState.Changed;
                punkteGelb = value;
            }
        }

        public double PunkteRot
        {
            get { return punkteRot; }
            set
            {
                if (punkteRot != value)
                    objectState = ObjectState.Changed;
                punkteRot = value;
            }
        }

        public bool IsContest
        {
            get { return isContest; }
            set
            {
                if (isContest != value)
                    objectState = ObjectState.Changed;
                isContest = value;
            }
        }
        public bool ShowForm
        {
            get { return showForm; }
            set
            {
                if (showForm != value)
                    objectState = ObjectState.Changed;
                showForm = value;
            }
        }
        public string MailTo
        {
            get { return mailTo; }
            set
            {
                if (mailTo != value)
                    objectState = ObjectState.Changed;
                mailTo = value;
            }
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);
        }

        #region IXmlSerializable
        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            var serializer = new XmlSerializer(typeof(DataObjectSurvey));
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
            string baseUrlXslt = string.Empty;
            if (PartnerID.HasValue)
            {
                Partner partner = Partner.Get(PartnerID.Value, null);
                if (partner != null && !string.IsNullOrEmpty(partner.BaseUrlXSLT))
                    baseUrlXslt = partner.BaseUrlXSLT;
            }
            return Helper.TransformDataObject(ObjectType, GetXml(), UrlXSLT, outputType, baseUrlXslt, templatesUrl,
                                              argumentList);
        }


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

            Data.DataObjectSurvey.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetSelectSql(qParas);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetInsertSql(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetUpdateSql(this, parameters);
        }

        public override string GetJoinSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetJoinSql(qParas);
        }

        public override string GetWhereSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetWhereSql(qParas);
        }

        public override string GetFullTextWhereSQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetFullTextWhereSql(qParas);
        }

        public override string GetOrderBySQL(QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectSurvey.GetOrderBySql();
        }

        internal void SetValuesFromQuerySting()
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpServerUtility Server = HttpContext.Current.Server;
            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
            {
                TagList = Server.UrlDecode(Request.QueryString["TG"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
            {
                Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
            {
                ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
            {
                Copyright = int.Parse(Request.QueryString["CR"]);
            }
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
            {
                Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
            {
                City = Server.UrlDecode(Request.QueryString["CI"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
            {
                Region = Server.UrlDecode(Request.QueryString["RE"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
            {
                CountryCode = Server.UrlDecode(Request.QueryString["CO"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["IsContest"]))
            {
                bool contestParam;
                IsContest = bool.TryParse(Request.QueryString["IsContest"], out contestParam) && contestParam;
            }
        }

        #endregion
    }
}