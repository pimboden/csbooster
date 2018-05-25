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
