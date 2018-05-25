// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class FunctionsSort : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetFunctionsSort");

        private bool hasContent = false;
        public override bool ShowObject(string settingsXml)
        {
            Fill(settingsXml);
            return hasContent;
        }

        private void Fill(string settingsXml)
        {
            int objectType = 0;
            if (this.WidgetHost.ParentPageType == PageType.Overview && WidgetHost.ParentObjectType == Helper.GetObjectTypeNumericID("Page") && !string.IsNullOrEmpty(Request.QueryString["OT"]))
            {
                objectType = Helper.GetObjectTypeNumericID(Request.QueryString["OT"]);
            }
            else
            {
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            bool noSort = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxNoSort", false);
            bool byActivity = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByActivity", false);
            bool byMember = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByMember", false);

            LiTitle.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByTitle", true);
            LiDate.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByDate", true);
            LiVisits.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByVisits", true);
            LiRatings.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatings", true);
            LiRatingConsolidated.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatingConsolidated", false);
            LiComments.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByComments", true);
            LiLinks.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByLinks", false);

            if (objectType == Helper.GetObjectTypeNumericID("User"))
            {
                LiActivity.Visible = byActivity;
                LiLinks.Visible = false;
            }
            else if (objectType == Helper.GetObjectTypeNumericID("Community"))
            {
                LiMember.Visible = byMember;
                LiLinks.Visible = false;
            }

            hasContent = (ByTitle.Visible || ByDate.Visible || ByActivity.Visible || ByMember.Visible || ByRatings.Visible || ByVisits.Visible || ByComments.Visible || ByLinks.Visible);
            if (!hasContent) return;

            string queryStringWithoutSO = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "SO", "SD", "PN" }, false);

            ByTitle.NavigateUrl = string.Format("{0}?SO={1}&SD={2}{3}", Request.GetRawPath(), QuickSort.Title, QuickSortDirection.Asc, queryStringWithoutSO);
            ByDate.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.StartDate, queryStringWithoutSO);
            ByActivity.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.Agility, queryStringWithoutSO);
            ByMember.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.MemberCount, queryStringWithoutSO);
            ByRatings.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.RatedAverage, queryStringWithoutSO);
            ByRatingConsolidated.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.RatedConsolidated, queryStringWithoutSO);
            ByVisits.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.Viewed, queryStringWithoutSO);
            ByComments.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.Commented, queryStringWithoutSO);
            ByLinks.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.GetRawPath(), QuickSort.Linked, queryStringWithoutSO);
            ByTitle.ID = null;
            ByDate.ID = null;
            ByActivity.ID = null;
            ByMember.ID = null;
            ByRatings.ID = null;
            ByRatingConsolidated.ID = null;
            ByVisits.ID = null;
            ByComments.ID = null;
            ByLinks.ID = null;

            if (noSort && !string.IsNullOrEmpty(Request.QueryString["SO"]))
            {
                NoSort.Visible = true;
                NoSort.NavigateUrl = string.Format("{0}?{1}", Request.GetRawPath(), queryStringWithoutSO);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["SO"]))
            {
                switch ((QuickSort)Enum.Parse(typeof(QuickSort), Request.QueryString["SO"]))
                {
                    case QuickSort.StartDate:
                        LiDate.Attributes["class"] = "active";
                        break;
                    case QuickSort.Agility:
                        LiActivity.Attributes["class"] = "active";
                        break;
                    case QuickSort.RatedAverage:
                        LiRatings.Attributes["class"] = "active";
                        break;
                    case QuickSort.RatedConsolidated:
                        LiRatingConsolidated.Attributes["class"] = "active";
                        break;
                    case QuickSort.Viewed:
                        LiVisits.Attributes["class"] = "active";
                        break;
                    case QuickSort.Linked:
                        LiLinks.Attributes["class"] = "active";
                        break;
                    case QuickSort.Commented:
                        LiComments.Attributes["class"] = "active";
                        break;
                    case QuickSort.Title:
                        LiTitle.Attributes["class"] = "active";
                        break;
                    case QuickSort.MemberCount:
                        LiMember.Attributes["class"] = "active";
                        break;
                }
            }

            LiTitle.ID = null;
            LiDate.ID = null;
            LiActivity.ID = null;
            LiMember.ID = null;
            LiRatings.ID = null;
            LiRatingConsolidated.ID = null;
            LiVisits.ID = null;
            LiComments.ID = null;
            LiLinks.ID = null;
        }
    }
}