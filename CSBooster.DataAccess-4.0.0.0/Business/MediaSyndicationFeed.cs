// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.ServiceModel.Syndication;
using System.Xml;

namespace _4screen.CSB.DataAccess.Business
{
    public class MediaSyndicationFeed : SyndicationFeed
    {
        public MediaSyndicationFeed()
        {
            AttributeExtensions.Add(new XmlQualifiedName("media", "http://www.w3.org/2000/xmlns/"), "http://search.yahoo.com/mrss/");
        }

        protected override SyndicationItem CreateItem()
        {
            return (SyndicationItem)new MediaSyndicationItem();
        }
    }
}
