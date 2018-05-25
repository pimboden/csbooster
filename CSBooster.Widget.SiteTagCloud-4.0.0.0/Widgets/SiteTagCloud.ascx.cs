// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class SiteTagCloud : WidgetBase
    {
        private bool hasContent = false;
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            int tagWordCloudSize = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagWordCloudSize", int.Parse(ConfigurationManager.AppSettings["TagWordCloudSize"]));
            int relevance = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Relevance", 0);
            int relevanceType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RelevanceType", 1);

            int? relatedObjectType = null;
            Guid? relatedCommunityID = null;
            Guid? relatedUserID = null;

            if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                DataObject profileCommunity = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                RenderCloud(tagWordCloudSize, relevance, relevanceType, null, null, profileCommunity.UserID.Value);
            }
            else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                RenderCloud(tagWordCloudSize, relevance, relevanceType, null, WidgetHost.ParentCommunityID, null);
            }
            else
            {
                if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page"))
                {
                    if (this.WidgetHost.ParentPageType == PageType.Overview)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["OT"]))
                        {
                            relatedObjectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]);

                            if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                                relatedUserID = Request.QueryString["XUI"].ToGuid();

                            if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                                relatedCommunityID = Request.QueryString["XCN"].ToGuid();

                            RenderCloud(tagWordCloudSize, relevance, relevanceType, relatedObjectType, relatedCommunityID, relatedUserID);
                        }
                    }
                    else if (this.WidgetHost.ParentPageType == PageType.Homepage ||
                             this.WidgetHost.ParentPageType == PageType.Detail ||
                             this.WidgetHost.ParentPageType == PageType.None)
                    {
                        RenderCloud(tagWordCloudSize, relevance, relevanceType, null, null, null);
                    }
                }
            }

            return hasContent;
        }

        private void RenderCloud(int tagWordCloudSize, int relvance, int relevanceType, int? relatedObjectType, Guid? relatedCommunityID, Guid? relatedUserID)
        {
            QuickTagCloudRelevanceGroup enuRepevance = (QuickTagCloudRelevanceGroup)relvance;
            QuickTagCloudRelevance enurelevanceType = (QuickTagCloudRelevance)relevanceType;
            int groupAmount = 24;
            if (relvance > 0)
            {
                if (relvance <= 3)
                    groupAmount = 60;
                else if (relvance <= 5)
                    groupAmount = 24;
                else
                    groupAmount = 16;
            }

            QuickParametersTag quickParametersTag = new QuickParametersTag();
            quickParametersTag.Udc = UserDataContext.GetUserDataContext();
            quickParametersTag.IgnoreCache = false;
            quickParametersTag.Amount = tagWordCloudSize;
            quickParametersTag.RelevanceGroup = enuRepevance;
            quickParametersTag.RelevanceGroupAmount = groupAmount;
            quickParametersTag.Relevance = enurelevanceType;
            if (relatedObjectType.HasValue)
                quickParametersTag.RelatedObjectType = relatedObjectType.Value;
            if (relatedCommunityID.HasValue)
                quickParametersTag.RelatedCommunityID = relatedCommunityID.Value;
            if (relatedUserID.HasValue)
                quickParametersTag.RelatedUserID = relatedUserID.Value;

            List<TagCloudItem> tagCloud = DataObjectTag.GetTagCloud(5, quickParametersTag);
            hasContent = (tagCloud.Count > 0); 
            StringBuilder sb = new StringBuilder();
            foreach (TagCloudItem tagWord in tagCloud)
            {
                sb.AppendFormat("<span class=\"t{0}\">&bull; <a href=\"/Tag/{1}\">{2}</a></span> ", tagWord.TagClass, tagWord.Tag.Title, tagWord.Tag.Title);
            }
            this.LitTags.Text = sb.ToString();
        }
    }
}
