// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class RssEngine
    {
        private static _4screen.CSB.DataAccess.Data.RssEngineConfig rssEngineConfig;
        private static string siteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        static RssEngine()
        {
            rssEngineConfig = new _4screen.CSB.DataAccess.Data.RssEngineConfig();
        }

        public static string GetFeed(QuickParameters quickParameters, string type)
        {

            string urlPrefix = System.Configuration.ConfigurationManager.AppSettings["HostName"] + _4screen.Utils.Web.SiteConfig.ApplicationPath;

            UserDataContext udc = UserDataContext.GetUserDataContext();
            quickParameters.Udc = udc;
            quickParameters.ObjectTypes = QuickParameters.GetDelimitedObjectTypeIDs(rssEngineConfig.ObjectTypes, ',');
            quickParameters.SortBy = QuickSort.StartDate;
            quickParameters.Direction = QuickSortDirection.Desc;
            //quickParameters.FromStartDate = DateTime.Now - new TimeSpan(rssEngineConfig.Days, 0, 0, 0);
            quickParameters.Amount = rssEngineConfig.MaxItems;
            quickParameters.PageSize = rssEngineConfig.MaxItems;

            StringBuilder sb;
            List<string> titleSegments = new List<string>();
            SyndicationFeed feed = new SyndicationFeed();

            if (quickParameters.CommunityID.HasValue)
            {
                DataObjectCommunity channelCommunity = DataObject.Load<DataObjectCommunity>(quickParameters.CommunityID);
                if (channelCommunity.State != ObjectState.Added)
                    if (channelCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                        titleSegments.Add(string.Format("Von {0}", channelCommunity.Nickname));
                    else
                        titleSegments.Add(string.Format("Von {0}", channelCommunity.Title));
            }
            else if (quickParameters.UserID.HasValue)
            {
                Business.DataObjectUser channelUser = DataObject.Load<DataObjectUser>(quickParameters.UserID);
                if (channelUser.State != ObjectState.Added)
                    titleSegments.Add(string.Format("Von {0}", channelUser.Nickname));
            }
            if (quickParameters.ObjectType != 0)
            {
                titleSegments.Add(Helper.GetObjectName(quickParameters.ObjectType, false));
            }
            if (!string.IsNullOrEmpty(quickParameters.RawTags1) || !string.IsNullOrEmpty(quickParameters.RawTags2) || !string.IsNullOrEmpty(quickParameters.RawTags3))
            {
                titleSegments.Add(string.Format("Tags {0}", Helper.GetTagWordString(new List<string>() { quickParameters.RawTags1, quickParameters.RawTags2, quickParameters.RawTags3 })));
            }

            sb = new StringBuilder();
            if (titleSegments.Count > 0)
            {
                for (int i = 0; i < titleSegments.Count; i++)
                {
                    sb.Append(titleSegments[i]);
                    if (i < titleSegments.Count - 1)
                        sb.Append(", ");
                }
            }
            string title = string.Format("{0} Feed {1}{2}", siteName, sb.Length != 0 ? "- " : " ", sb.ToString());
            string description = "RSS Feed von " + siteName;

            feed.Title = TextSyndicationContent.CreatePlaintextContent(title);
            feed.Description = TextSyndicationContent.CreatePlaintextContent(description);
            feed.Links.Add(new SyndicationLink(new Uri(urlPrefix)));
            feed.Language = "de-CH";

            List<SyndicationItem> items = new List<SyndicationItem>();
            DataObjectList<DataObject> rssItems = DataObjects.Load<DataObject>(quickParameters);
            foreach (DataObject rssItem in rssItems)
            {
                SyndicationItem item = new SyndicationItem();


                // Set owner as author
                SyndicationPerson author = new SyndicationPerson();
                author.Name = rssItem.Nickname;
                item.Authors.Add(author);

                // Set object's guid
                item.Id = rssItem.objectID.Value.ToString();

                // Link to the object
                string itemUrl = null;
                if (rssItem.ObjectType == Helper.GetObjectTypeNumericID("ForumTopicItem"))
                {
                    item.Title = TextSyndicationContent.CreatePlaintextContent(Helper.GetObjectName(rssItem.ObjectType, true) + " von " + rssItem.Nickname);
                    if (quickParameters.RelationParams != null && quickParameters.RelationParams.ParentObjectID.HasValue)
                    {
                        itemUrl = urlPrefix + Helper.GetDetailLink(Helper.GetObjectTypeNumericID("ForumTopic"), quickParameters.RelationParams.ParentObjectID.Value.ToString(), false) + "&COID=" + rssItem.ObjectID;
                    }
                    else
                    {
                        var forumTopics = DataObjects.Load<DataObjectForumTopic>(new QuickParameters() { Udc = udc, Amount = 1, RelationParams = new RelationParams() { ChildObjectID = rssItem.ObjectID } });
                        if (forumTopics.Count == 1)
                            itemUrl = urlPrefix + Helper.GetDetailLink(Helper.GetObjectTypeNumericID("ForumTopic"), forumTopics[0].ObjectID.ToString(), false) + "&COID=" + rssItem.ObjectID;
                        else
                            continue;
                    }
                }
                else
                {
                    item.Title = TextSyndicationContent.CreatePlaintextContent("[" + Helper.GetObjectName(rssItem.ObjectType, true) + "] " + rssItem.Title);
                    itemUrl = urlPrefix + Helper.GetDetailLink(rssItem.ObjectType, rssItem.objectID.Value.ToString());
                }
                item.AddPermalink(new Uri(itemUrl));

                // Take start date as publish date
                item.PublishDate = rssItem.StartDate;

                // Image if available
                if (!string.IsNullOrEmpty(rssItem.GetImage(PictureVersion.S)) && rssItem.GetImage(PictureVersion.S).ToLower() != Helper.GetDefaultURLImageSmall(rssItem.ObjectType).ToLower())
                {
                    item.Content = SyndicationContent.CreateXhtmlContent("<div><a href=\"" + itemUrl + "\"><img src=\"" + System.Configuration.ConfigurationManager.AppSettings["MediaDomainName"] + rssItem.GetImage(PictureVersion.S) + "\"></a></div><div>" + rssItem.Description.StripHTMLTags().CropString(rssEngineConfig.MaxDescriptionLength) + "</div>");
                }
                else
                {
                    item.Content = TextSyndicationContent.CreatePlaintextContent(rssItem.Description.StripHTMLTags().CropString(rssEngineConfig.MaxDescriptionLength));
                }

                items.Add(item);
            }

            feed.Items = items;

            sb = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(sb);
            if (type == "rss")
            {
                feed.SaveAsRss20(xmlWriter);
            }
            else if (type == "atom")
            {
                feed.SaveAsAtom10(xmlWriter);
            }
            xmlWriter.Close();

            string feedXml = sb.ToString();
            feedXml = Regex.Replace(feedXml, @"^<.*?xml.*?>\s*", "");
            return feedXml;
        }
    }
}
