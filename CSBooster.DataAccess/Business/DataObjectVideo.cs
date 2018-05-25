//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Data.SqlClient;
using _4screen.CSB.Common;
using System.ServiceModel.Syndication;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "Video")]
    public class DataObjectVideo : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        public string UserEmail { get; set; }
        public double VideoPreviewPictureTimepointSec { get; set; }
        public int EstimatedWorkTimeSec { get; set; }
        public VideoFormat OriginalFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int SizeByte { get; set; }
        public int DurationSecond { get; set; }
        public string Location { get; set; }
        public string OriginalLocation { get; set; }
        public string ConvertMessage { get; set; }
        public bool External { get; set; }
        public string URLReferenced { get; set; }
        public decimal AspectRatio { get; set; }
        public string TypeReferenced { get; set; }
        public string Site { get; set; }

        public DataObjectVideo()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectVideo(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = Helper.GetObjectType("Video").NumericId;
        }

        public new void Validate(AccessMode accessMode)
        {
            base.Validate(accessMode);

            if (string.IsNullOrEmpty(OriginalLocation))
                throw new SiemeArgumentException("DataObjectVideo", "Validate", "OriginalLocation", "OriginalLocation must not be empty");

            if (string.IsNullOrEmpty(UserEmail))
                throw new SiemeArgumentException("DataObjectVideo", "Validate", "UserEmail", "UserEmail is missing");
        }

        public void Insert(bool addToConverterQueue)
        {
            if (OriginalLocation.Length == 0)
                throw new SiemeArgumentException("DataObjectVideo", "Insert", "OriginalLocation", "OriginalLocation is missing");

            if (UserEmail.Length == 0)
                throw new SiemeArgumentException("DataObjectVideo", "Insert", "UserEmail", "UserEmail is missing");

            base.Insert();
            if (addToConverterQueue)
            {
                Business.ConvertQueue objQueue = new ConvertQueue();
                objQueue.ObjectID = ObjectID;
                objQueue.ObjectType = Helper.GetObjectTypeNumericID("Video");
                objQueue.Status = MediaConvertedState.NotConvertet;
                objQueue.VideoPreviewPictureTimepointSec = VideoPreviewPictureTimepointSec;
                objQueue.UserEmail = UserEmail;
                objQueue.EstimatedWorkTimeSec = EstimatedWorkTimeSec;
                objQueue.Insert();
            }
        }

        public void Update(bool reconvert)
        {
            if (reconvert)
            {
                ShowState = ObjectShowState.ConversionFailed;
                Image = string.Empty;
                Business.ConvertQueue objQueue = new ConvertQueue();
                objQueue.ObjectID = ObjectID;
                objQueue.ObjectType = Helper.GetObjectTypeNumericID("Video");
                objQueue.Status = MediaConvertedState.NotConvertet;
                objQueue.VideoPreviewPictureTimepointSec = VideoPreviewPictureTimepointSec;
                objQueue.UserEmail = UserEmail;
                objQueue.EstimatedWorkTimeSec = EstimatedWorkTimeSec;
                objQueue.Insert();
            }
            base.Update();
        }

        public static string GetPlaylistFeed(Guid? objectID)
        {
            UserDataContext udc = UserDataContext.GetUserDataContext();

            DataObjectVideo dataObjectVideo =  DataObject.Load<DataObjectVideo>(objectID);
            DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(dataObjectVideo.CommunityID);
 
            QuickParameters quickParameters = new QuickParameters();
            quickParameters.Udc = udc;
            quickParameters.ObjectType = Helper.GetObjectTypeNumericID("Video");
            quickParameters.SortBy = QuickSort.StartDate;
            quickParameters.Amount = 100;
            quickParameters.PageSize = 100;
            quickParameters.PageNumber = 1;
            quickParameters.ShowState = ObjectShowState.Published;
            if (community.ObjectType ==  Helper.GetObjectTypeNumericID("Community"))
                quickParameters.CommunityID = community.ObjectID;
            else if (community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                quickParameters.UserID = community.UserID;

            MediaSyndicationFeed feed = new MediaSyndicationFeed();

            string title = string.Empty;
            if (community.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                title = "Video aus " + community.Title;
            else if (community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                title = "Videos von " + community.Nickname;

            feed.Title = TextSyndicationContent.CreatePlaintextContent(title);

            //feed.Description = TextSyndicationContent.CreatePlaintextContent("");
            string feedUrl = SiteConfig.SiteURL + Helper.GetDetailLink(community.ObjectType, community.ObjectID.Value.ToString());
            feed.Links.Add(new SyndicationLink(new Uri(feedUrl)));
            feed.Language = "de-CH";

            List<SyndicationItem> items = new List<SyndicationItem>();
            DataObjectList<DataObjectVideo> videos = DataObjects.Load<DataObjectVideo>(quickParameters);
            foreach (DataObjectVideo video in videos)
            {
                MediaSyndicationItem item = new MediaSyndicationItem();

                item.MediaContentUrl = new Uri(string.Format("{0}{1}", Helper.GetVideoBaseURL(), video.Location));
                item.MediaContentDuration = video.DurationSecond;
                item.MediaContentType = "video/x-flv";
                item.MediaThumbnailUrl = new Uri(string.Format("{0}{1}", SiteConfig.MediaDomainName, video.GetImage(PictureVersion.S)));
                item.MediaKeywords = video.TagList.Replace(Constants.TAG_DELIMITER.ToString(), ", ");
                item.MediaCredit = video.Nickname;

                item.Title = TextSyndicationContent.CreatePlaintextContent(video.Title.StripHTMLTags());

                item.Id = video.ObjectID.Value.ToString();

                string itemUrl = SiteConfig.SiteURL + Helper.GetDetailLink(video.ObjectType, video.ObjectID.Value.ToString());
                item.AddPermalink(new Uri(itemUrl));

                item.PublishDate = video.StartDate;

                items.Add(item);
            }

            feed.Items = items;

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter xmlWriter = XmlWriter.Create(sb, settings);
            feed.SaveAsRss20(xmlWriter);
            xmlWriter.Close();

            string feedXml = sb.ToString();
            feedXml = Regex.Replace(feedXml, @"^<.*?xml.*?>\s*", "");
            return feedXml;
        }        

        public void AddToConverterQueue()
        {
            Business.ConvertQueue objQueue = new ConvertQueue();
            objQueue.ObjectID = ObjectID;
            objQueue.ObjectType = Helper.GetObjectType("Video").NumericId;
            objQueue.Status = MediaConvertedState.NotConvertet;
            objQueue.VideoPreviewPictureTimepointSec = VideoPreviewPictureTimepointSec;
            objQueue.UserEmail = UserEmail;
            objQueue.EstimatedWorkTimeSec = EstimatedWorkTimeSec;
            objQueue.Insert();
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectVideo.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectVideo.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectVideo));
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
                        case "Video":
                            Location = reader.ReadString();
                            break;
                    }
                }
            }

        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteRaw(string.Format("<Video>{0}</Video>", Location));
        }

        #endregion
    }
}