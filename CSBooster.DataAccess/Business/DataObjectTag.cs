//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using _4screen.CSB.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace _4screen.CSB.DataAccess.Business
{
    [XmlRoot(ElementName = "News")]
    public class DataObjectTag : _4screen.CSB.DataAccess.Business.DataObject, IXmlSerializable
    {
        public decimal Relevance { get; set; }

        public string TagWordLower
        {
            get { return Title.ToLower(); }
        }

        public DataObjectTag()
            : this(UserDataContext.GetUserDataContext())
        {
        }

        public DataObjectTag(UserDataContext userDataContext)
            : base(userDataContext)
        {
            objectType = 5;
        }

        #region Read / Write Methods
        public override void FillObject(SqlDataReader sqlReader)
        {
            base.FillObject(sqlReader);

            Data.DataObjectTag.FillObject(this, sqlReader);
        }

        public override string GetSelectSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetSelectSQL(qParas, parameters);
        }

        public override string GetInsertSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetInsertSQL(this, parameters);
        }

        public override string GetUpdateSQL(SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetUpdateSQL(this, parameters);
        }

        public override string GetJoinSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetJoinSQL(qParas, parameters);
        }

        public override string GetWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetWhereSQL(qParas, parameters);
        }

        public override string GetFullTextWhereSQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetFullTextWhereSQL(qParas, parameters);
        }

        public override string GetOrderBySQL(Business.QuickParameters qParas, SqlParameterCollection parameters)
        {
            return Data.DataObjectTag.GetOrderBySQL(qParas, parameters);
        }
        #endregion

        public XmlDocument GetXml()
        {
            SerializationType = SerializationType.Full;

            XmlSerializer serializer = new XmlSerializer(typeof(DataObjectTag));
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
            }
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
        }

        #endregion

        public static Guid? GetTagID(string tagWord)
        {
            if (!string.IsNullOrEmpty(tagWord))
                return Data.DataObject.GetTagID(tagWord);
            else
                return null;
        }

        public static List<TagCloudItem> GetTagCloud(int numberTagClasses, QuickParametersTag quickParameters)
        {
            List<TagCloudItem> tagCloud = new List<TagCloudItem>();
            DataObjectList<DataObjectTag> tags = DataObjects.Load<DataObjectTag>(quickParameters);
            if (tags.Count > 0)
            {
                decimal relevanceGap = tags[0].Relevance - tags[tags.Count - 1].Relevance;
                decimal minRelevance = tags[tags.Count - 1].Relevance;
                decimal ratio = relevanceGap / (decimal)numberTagClasses;
                if (ratio == 0)
                    ratio = 1;

                foreach (DataObjectTag tag in tags)
                {
                    int tagClass = Math.Max(1, (int)((tag.Relevance - minRelevance) / ratio + 0.5m));
                    tagCloud.Add(new TagCloudItem(tag, tagClass));
                }

                tagCloud.Sort(new TitleSorterTag());
            }
            return tagCloud;
        }

        public static List<TagReferenceItem> GetReferencedTags(int objectType, Guid? userId, string communityList)
        {
            return Data.DataObjectTag.GetReferencedTags(objectType, userId, communityList);
        }
    }

    public class TagReferenceItem
    {
        public Guid TagId { get; set; }
        public string Title { get; set; }
    }

    public class TagCloudItem
    {
        private DataObjectTag tag;
        private int tagClass;

        public TagCloudItem(DataObjectTag tag, int tagClass)
        {
            this.tag = tag;
            this.tagClass = tagClass;
        }

        public int TagClass
        {
            get { return tagClass; }
            set { tagClass = value; }
        }

        public DataObjectTag Tag
        {
            get { return tag; }
            set { tag = value; }
        }
    }

    public class ViewCountSorterTag : IComparer<DataObjectTag>
    {
        private bool blnDesc = false;

        public ViewCountSorterTag(bool desc)
        {
            blnDesc = desc;
        }

        public ViewCountSorterTag()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectTag x, DataObjectTag y)
        {
            if (x.ViewCount.CompareTo(y.ViewCount) == 0)
            {
                if (!blnDesc)
                    return y.ViewCount.CompareTo(x.ViewCount);
                else
                    return x.ViewCount.CompareTo(y.ViewCount);
            }
            else
            {
                if (!blnDesc)
                    return x.ViewCount.CompareTo(y.ViewCount);
                else
                    return y.ViewCount.CompareTo(x.ViewCount);
            }
        }
    }

    public class TitleSorterTag : IComparer<TagCloudItem>
    {
        private bool blnDesc = false;

        public TitleSorterTag(bool desc)
        {
            blnDesc = desc;
        }

        public TitleSorterTag()
        {
            blnDesc = false;
        }

        public int Compare(TagCloudItem x, TagCloudItem y)
        {
            if (!blnDesc)
                return x.Tag.Title.CompareTo(y.Tag.Title);
            else
                return y.Tag.Title.CompareTo(x.Tag.Title);
        }
    }

    public class TagEqualityComparer : IEqualityComparer<DataObjectTag>
    {
        bool IEqualityComparer<DataObjectTag>.Equals(DataObjectTag x, DataObjectTag y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.ObjectID.HasValue && y.ObjectID.HasValue && x.ObjectID == y.ObjectID;
        }

        int IEqualityComparer<DataObjectTag>.GetHashCode(DataObjectTag obj)
        {
            return obj.ObjectID.GetHashCode();
        }
    }


    public class SelectCountSorterTag : IComparer<DataObjectTag>
    {
        private bool blnDesc = false;

        public SelectCountSorterTag(bool desc)
        {
            blnDesc = desc;
        }

        public SelectCountSorterTag()
        {
            blnDesc = false;
        }

        public int Compare(DataObjectTag x, DataObjectTag y)
        {
            if (x.SelectCount.Value.CompareTo(y.SelectCount.Value) == 0)
            {
                if (!blnDesc)
                    return y.SelectCount.Value.CompareTo(x.SelectCount.Value);
                else
                    return x.SelectCount.Value.CompareTo(y.SelectCount.Value);
            }
            else
            {
                if (!blnDesc)
                    return x.SelectCount.Value.CompareTo(y.SelectCount.Value);
                else
                    return y.SelectCount.Value.CompareTo(x.SelectCount.Value);
            }
        }
    }
}