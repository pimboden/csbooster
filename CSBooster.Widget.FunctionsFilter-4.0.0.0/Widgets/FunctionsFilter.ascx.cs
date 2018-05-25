// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class FunctionsFilter : WidgetBase
    {
        private bool hasContent = false;
        private string queryStringWithoutTag;

        public override bool ShowObject(string settingsXml)
        {
            queryStringWithoutTag = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "TGL1", "PN" }, false);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            string output = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "rcbOutput", "AutoFilter");

            if (output == "ManualFilter")
            {
                SetTagFilter(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagWords", string.Empty));
            }
            else
            {
                int amount = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MaxCount", 20);
                int relvance = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Relevance", 3);
                SetAutoTagFilter(amount, relvance);
            }

            return hasContent;
        }

        private void SetAutoTagFilter(int amount, int relvance)
        {
            QuickTagCloudRelevanceGroup enuRepevance = (QuickTagCloudRelevanceGroup)relvance;
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
            quickParametersTag.Amount = amount;
            quickParametersTag.RelevanceGroup = enuRepevance;
            quickParametersTag.RelevanceGroupAmount = groupAmount;
            quickParametersTag.Relevance = QuickTagCloudRelevance.ObjectView;

            if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                DataObject profileCommunity = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                quickParametersTag.RelatedUserID = profileCommunity.UserID.Value;
            }
            else if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                quickParametersTag.RelatedCommunityID = WidgetHost.ParentCommunityID;
            }
            if (this.WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page") && !string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                if (this.WidgetHost.ParentPageType == PageType.Overview)
                {
                    quickParametersTag.RelatedObjectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]);

                    if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                        quickParametersTag.RelatedUserID = Request.QueryString["XUI"].ToGuid();

                    if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                        quickParametersTag.RelatedCommunityID = Request.QueryString["XCN"].ToGuid();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            List<TagCloudItem> relatedTags = DataObjectTag.GetTagCloud(5, quickParametersTag);

            if (relatedTags.Count > 0)
            {
                hasContent = true;

                HtmlGenericControl li;
                HyperLink link;

                foreach (TagCloudItem tagWord in relatedTags)
                {
                    li = new HtmlGenericControl("li");
                    string tagClass = "tagClass" + tagWord.TagClass;
                    link = new HyperLink();
                    link.Text = Helper.GetMappedTagWord(tagWord.Tag.Title);
                    link.NavigateUrl = string.Format("{0}?TGL1={1}{2}", Request.GetRawPath(), tagWord.Tag.Title, queryStringWithoutTag);
                    if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]) && Request.QueryString["TGL1"] == tagWord.Tag.Title)
                        tagClass += " tagActive";
                    li.Attributes.Add("class", tagClass);
                    li.Controls.Add(link);
                    Ph.Controls.Add(li);
                }

                RenderResetLink();
            }
        }

        private void SetTagFilter(string tagWords)
        {
            string[] manualTags = tagWords.Split(',');


            if (manualTags.Length > 0)
            {
                hasContent = true;

                HtmlGenericControl li;
                HyperLink link;
                foreach (string tag in manualTags)
                {
                    li = new HtmlGenericControl("li");
                    link = new HyperLink();
                    link.Text = tag;
                    link.NavigateUrl = string.Format("{0}?TGL1={1}{2}", Request.GetRawPath(), tag, queryStringWithoutTag);
                    if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]) && Request.QueryString["TGL1"] == tag)
                        link.CssClass = "tagActive";
                    li.Controls.Add(link);
                    Ph.Controls.Add(li);
                }

                RenderResetLink();
            }

        }

        private void RenderResetLink()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]))
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "tagFilterReset");
                HyperLink link = new HyperLink();
                link.Text = GuiLanguage.GetGuiLanguage("WidgetFunctionsFilter").GetString("CommandFilterReset");
                link.NavigateUrl = string.Format("{0}?{1}", Request.GetRawPath(), queryStringWithoutTag.TrimStart('&'));
                div.Controls.Add(link);
                PhReset.Controls.Add(div);
            }
        }
    }
}