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
    [XmlRoot(ElementName = "Picture")]
    public class DataObjectPicture : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        private PhotoNotes listNotes = null;
        internal XmlDocument photoNotesXml;

        public long SizeByte { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool External { get; set; }
        public string URLReferenced { get; set; }
        public decimal AspectRatio { get; set; }
        public string TypeReferenced { get; set; }
        public string Site { get; set; }

        public PhotoNotes Notes
        {
            get
            {
                if (listNotes == null)
                    listNotes = new PhotoNotes(photoNotesXml.DocumentElement, this);
                return listNotes;
            }
        }

        public DataObjectPicture()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectPicture(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Picture").NumericId;
            photoNotesXml = new XmlDocument();
            XmlHelper.CreateRoot(photoNotesXml, "Root");
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectPicture.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectPicture.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectPicture));
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
                        case "Height":
                            string height = reader.ReadString();
                            if (!string.IsNullOrEmpty(height))
                                Height = int.Parse(height);
                            break;
                        case "Width":
                            string width = reader.ReadString();
                            if (!string.IsNullOrEmpty(width))
                                Width = int.Parse(width);
                            break;

                    }
                }
            }

        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            //writer.WriteRaw(string.Format("<PicLarge>{0}</PicLarge>", this.URLImage));
            writer.WriteRaw(string.Format("<Height>{0}</Height>", Height));
            writer.WriteRaw(string.Format("<Width>{0}</Width>", Width));
        }

        #endregion
    }
}
