//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #2.0.0.0    16.01.2009 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Syndication;

namespace _4screen.CSB.DataAccess.Business
{
    public class MediaSyndicationItem : SyndicationItem
    {
        public Uri MediaContentUrl { get; set; }
        public int MediaContentDuration { get; set; }
        public string MediaContentType { get; set; }
        public Uri MediaThumbnailUrl { get; set; }
        public string MediaKeywords { get; set; }
        public string MediaCredit { get; set; }

        protected override void WriteElementExtensions(XmlWriter writer, string version)
        {
            writer.WriteRaw(string.Format("<media:content url=\"{0}\" duration=\"{1}\" type=\"{2}\" />", MediaContentUrl, MediaContentDuration, MediaContentType));
            writer.WriteRaw(string.Format("<media:thumbnail url=\"{0}\" />", MediaThumbnailUrl));
            writer.WriteRaw(string.Format("<media:keywords>{0}</media:keywords>", MediaKeywords));
            writer.WriteRaw(string.Format("<media:credit>{0}</media:credit>", MediaCredit));
        }
    }
}
