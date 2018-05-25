// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "News")]
    public class DataObjectNews : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        private List<string> largePictures = new List<string>();
        private List<string> pictureCaptions = new List<string>();
        internal string newsTextLinked;
        internal XmlDocument newsLinksXml;

        public string NewsText { get; set; }
        public Uri ReferenceURL { get; set; }

        public string NewsTextLinked
        {
            get
            {
                if (string.IsNullOrEmpty(newsTextLinked))
                    return NewsText;
                else
                    return newsTextLinked;
            }
            internal set
            {
                if (newsTextLinked != value)
                    objectState = ObjectState.Changed;
                newsTextLinked = value;
            }
        }

        public List<Link> Links
        {
            get
            {
                List<Link> links = new List<Link>();
                XDocument properties = XDocument.Parse(newsLinksXml.OuterXml);
                var linkElements = properties.Element("Root").Element("Links");
                if (linkElements != null)
                {
                    var linkElements2 = from l in linkElements.Elements("Link") select new { Title = l.Element("Title").Value, URL = l.Element("URL").Value };
                    foreach (var linkElement in linkElements2)
                    {
                        links.Add(new Link()
                        {
                            Title = linkElement.Title,
                            URL = !string.IsNullOrEmpty(linkElement.URL) ? new Uri(linkElement.URL) : null
                        });
                    }
                }
                return links;
            }
            set
            {
                XmlNode linksNode = newsLinksXml.DocumentElement.SelectSingleNode("Links");
                if (linksNode == null)
                    linksNode = XmlHelper.AppendNode(newsLinksXml.DocumentElement, "Links");
                linksNode.RemoveAll();
                foreach (Link link in value)
                {
                    XmlElement linkElement = newsLinksXml.CreateElement("Link");
                    XmlElement titleElement = newsLinksXml.CreateElement("Title");
                    titleElement.InnerText = link.Title;
                    XmlElement urlElement = newsLinksXml.CreateElement("URL");
                    urlElement.InnerText = link.URL != null ? link.URL.ToString() : string.Empty;
                    linkElement.AppendChild(titleElement);
                    linkElement.AppendChild(urlElement);
                    linksNode.AppendChild(linkElement);
                }
                objectState = ObjectState.Changed;
            }
        }

        /// <summary>
        /// For deserialization only
        /// </summary>
        public List<string> LargePictures
        {
            get { return largePictures; }
            set { largePictures = value; }
        }

        /// <summary>
        /// For deserialization only
        /// </summary>
        public List<string> PictureCaptions
        {
            get { return pictureCaptions; }
            set { pictureCaptions = value; }
        }

        public DataObjectNews()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectNews(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("News").NumericId;
            newsLinksXml = new XmlDocument();
            XmlHelper.CreateRoot(newsLinksXml, "Root");
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);

            if (!string.IsNullOrEmpty(NewsText) && NewsText.Length > 1000000)
                throw new SiemeArgumentException("DataObjectNews", "Validate", "NewsText", "NewsText is too long");
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectNews.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectNews.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectNews));
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
            List<string> largePictures = new List<string>();
            List<string> pictureCaptions = new List<string>();

            while (reader.Read())
            {
                base.ReadXml(reader);
                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "NewsText":
                            NewsText = reader.ReadString();
                            break;
                        case "NewsRefURL":
                            string url = reader.ReadString();
                            if (!string.IsNullOrEmpty(url))
                                ReferenceURL = new Uri(url);
                            break;
                        case "NewsPicList":
                            System.Xml.XmlReader picListReader = reader.ReadSubtree();
                            while (picListReader.Read())
                            {
                                if (picListReader.NodeType == System.Xml.XmlNodeType.Element)
                                {
                                    switch (picListReader.Name)
                                    {
                                        case "Pic":
                                            largePictures.Add(picListReader.ReadString());
                                            break;
                                        case "Caption":
                                            pictureCaptions.Add(picListReader.ReadString());
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Links":
                            List<Link> links = new List<Link>();
                            Link link = null;
                            System.Xml.XmlReader linksListReader = reader.ReadSubtree();
                            while (linksListReader.Read())
                            {
                                if (linksListReader.NodeType == System.Xml.XmlNodeType.Element)
                                {
                                    switch (linksListReader.Name)
                                    {
                                        case "Link":
                                            link = new Link();
                                            break;
                                        case "Title":
                                            link.Title = linksListReader.ReadString().StripHTMLTags();
                                            break;
                                        case "URL":
                                            string url2 = linksListReader.ReadString();
                                            if (!string.IsNullOrEmpty(url2))
                                                link.URL = new Uri(url2);
                                            break;
                                    }
                                }
                                if (link != null && !string.IsNullOrEmpty(link.Title) && link.URL != null)
                                {
                                    links.Add(link);
                                    link = null;
                                }
                            }
                            Links = links;
                            break;
                    }
                }
            }

            LargePictures = largePictures;
            PictureCaptions = pictureCaptions;
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            //QuickList<DataObject> newsPictures = DataObject.RelGetChilds(new RelationParams() { ParentObjectType = ObjectType, ParentObjectID = ObjectID, ChildObjectType = ObjectType.Picture });

            writer.WriteElementString("NewsText", NewsText);
            writer.WriteElementString("NewsRefURL", ReferenceURL != null ? ReferenceURL.ToString() : string.Empty);
            writer.WriteStartElement("NewsPicList");
            /*foreach (DataObject newsPicture in newsPictures)
            {
                writer.WriteStartElement("NewsPic");
                writer.WriteElementString("Pic", newsPicture.GetImage(PictureVersion.L));
                writer.WriteElementString("Caption", newsPicture.Description);
                writer.WriteEndElement();
            }*/
            writer.WriteEndElement();

            writer.WriteStartElement("Links");
            foreach (Link link in Links)
            {
                writer.WriteStartElement("Link");
                writer.WriteElementString("Title", link.Title);
                writer.WriteElementString("URL", link.URL != null ? link.URL.ToString() : string.Empty);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion
    }
}