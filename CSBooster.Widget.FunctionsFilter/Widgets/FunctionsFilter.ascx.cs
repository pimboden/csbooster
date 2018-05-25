// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class FunctionsFilter : WidgetBase
    {
        private bool hasContent = false;
        private string queryStringWithoutTag;
        

        public override bool ShowObject(string settingsXml)
        {
            queryStringWithoutTag = Helper.GetFilteredQueryString(Request.QueryString, new List<string> {"TGL1", "PN"}, false);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            string output = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "rcbOutput", "AutoFilter");

            if (output == "ManualFilter")
                SetTagFilter(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagWords", string.Empty));
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

            if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                DataObject profileCommunity = DataObject.Load<DataObject>(this._Host.ParentCommunityID);
                quickParametersTag.RelatedUserID = profileCommunity.UserID.Value;
            }
            else if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                quickParametersTag.RelatedCommunityID = _Host.ParentCommunityID;
            }
            if (this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("Page") && !string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                if (this._Host.ParentPageType == PageType.Overview)
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

                HtmlGenericControl div;
                HyperLink link;

                foreach (TagCloudItem tagWord in relatedTags)
                {
                    div = new HtmlGenericControl("div");
                    link = new HyperLink();
                    link.Text = tagWord.Tag.Title;
                    link.NavigateUrl = string.Format("{0}?TGL1={1}{2}", Request.CurrentExecutionFilePath, tagWord.Tag.Title, queryStringWithoutTag);
                    if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]) && Request.QueryString["TGL1"] == tagWord.Tag.Title)
                        link.CssClass = "CSB_admin_menu_i";
                    else
                        link.CssClass = "CSB_admin_menu_a";

                    div.Controls.Add(link);
                    Ph.Controls.Add(div);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]))
                {
                    div = new HtmlGenericControl("div");
                    link = new HyperLink();
                    link.Text = GuiLanguage.GetGuiLanguage("WidgetFunctionsFilter").GetString("CommandFilterReset");
                    link.NavigateUrl = string.Format("{0}?{1}", Request.CurrentExecutionFilePath, queryStringWithoutTag.TrimStart('&'));
                    link.CssClass = "CSB_admin_menu_a";

                    div.Controls.Add(link);
                    Ph.Controls.Add(div);
                }
            }

        }

        private void SetTagFilter(string tagWords)
        {
            string[] manualTags = tagWords.Split(',');


            if (manualTags.Length > 0)
            {
                hasContent = true;

                HtmlGenericControl div;
                HyperLink link;
                foreach (string tag in manualTags)
                {
                    div = new HtmlGenericControl("div");
                    link = new HyperLink();
                    link.Text = tag;
                    link.NavigateUrl = string.Format("{0}?TGL1={1}{2}", Request.CurrentExecutionFilePath, tag, queryStringWithoutTag);

                    if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]) && Request.QueryString["TGL1"] == tag)
                        link.CssClass = "CSB_admin_menu_i";
                    else
                        link.CssClass = "CSB_admin_menu_a";
                    div.Controls.Add(link);
                    Ph.Controls.Add(div);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["TGL1"]))
                {
                    div = new HtmlGenericControl("div");
                    link = new HyperLink();
                    link.Text = GuiLanguage.GetGuiLanguage("WidgetFunctionsFilter").GetString("CommandFilterReset");
                    link.NavigateUrl = string.Format("{0}?{1}", Request.CurrentExecutionFilePath, queryStringWithoutTag.TrimStart('&'));
                    link.CssClass = string.IsNullOrEmpty(Request.QueryString["TGL1"]) ? "CSB_admin_menu_i" : "CSB_admin_menu_a";

                    div.Controls.Add(link);
                    Ph.Controls.Add(div);
                }
            }

        }
    }
}