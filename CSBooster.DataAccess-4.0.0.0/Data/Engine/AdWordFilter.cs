// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text.RegularExpressions;

namespace _4screen.CSB.DataAccess.Data
{
    internal class AdWordFilter : IWordFilter
    {
        private string word;
        private string wordPattern;
        private AdWordFilterActions action;
        private Guid campaignId;
        private Guid objectId;
        private int linkCounter;
        private MatchEvaluator linkMatchEvaluator;
        private MatchEvaluator popupMatchEvaluator;
        private Regex regex;

        internal AdWordFilter(string word, bool isExactMatch, AdWordFilterActions action, Guid campaignId)
        {
            this.word = word;
            this.action = action;
            this.campaignId = campaignId;

            linkMatchEvaluator = new MatchEvaluator(LinkMatchEvaluator);
            popupMatchEvaluator = new MatchEvaluator(PopupMatchEvaluator);

            if (isExactMatch)
                wordPattern = @"(?<=\W)(" + word + @")(?=\W[^>]*?<)";
            else
                wordPattern = @"(\w*" + word + @"\w*)(?=[^>]*?<)";

            regex = new Regex(wordPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public string Process(string value, Type type, FilterObjectTypes filterObjectType, Guid objectId, Guid userId)
        {
            this.objectId = objectId;
            string processedValue = value;
            value = "<DUMMY_TAG>" + value + "</DUMMY_TAG>";

            if (!AdWordHelper.CreditsLeft(campaignId, action))
                return processedValue;

            switch (action)
            {
                case AdWordFilterActions.Link:
                    if (regex.IsMatch(value))
                    {
                        linkCounter = 0;
                        processedValue = regex.Replace(value, linkMatchEvaluator);
                        processedValue = Regex.Replace(processedValue, "</{0,1}DUMMY_TAG>", "", RegexOptions.IgnoreCase);
                        AdWordHelper.AddToCampaignObjects(campaignId, objectId, word, linkCounter);
                    }
                    break;
                case AdWordFilterActions.Popup:
                    if (regex.IsMatch(value))
                    {
                        linkCounter = 0;
                        processedValue = regex.Replace(value, popupMatchEvaluator);
                        processedValue = Regex.Replace(processedValue, "</{0,1}DUMMY_TAG>", "", RegexOptions.IgnoreCase);
                        AdWordHelper.AddToCampaignObjects(campaignId, objectId, word, linkCounter);
                    }
                    break;
            }

            return processedValue;
        }

        private string LinkMatchEvaluator(Match match)
        {
            linkCounter++;
            return "<a href=\"" + _4screen.CSB.Common.Constants.Links["LINK_TO_AD_CAMPAIGN"].Url + campaignId + "&OID=" + objectId + "&Word=" + word + "&Type=Link\" class=\"CSB_ad_link\">" + match.Groups[1] + "</a>";
        }

        private string PopupMatchEvaluator(Match match)
        {
            linkCounter++;
            return "<a href=\"javascript:void(0)\" id=\"" + campaignId + "_" + objectId + "_" + word + "_" + linkCounter + "\" class=\"CSB_ad_popup_link\">" + match.Groups[1] + "</a>";
        }
    }
}