// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;

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
            if (this._Host.ParentPageType == PageType.Overview && this._Host.ParentObjectType == Helper.GetObjectTypeNumericID("Page") && !string.IsNullOrEmpty(Request.QueryString["OT"]))
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
            bool byLinks = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByLinks", false);

            ByTitle.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByTitle", true);
            ByDate.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByDate", true);
            ByVisits.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByVisits", true);
            ByRatings.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatings", true);
            ByRetingConsolidated.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByRatingConsolidated", false);
            ByComments.Visible = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CbxByComments", true);
            ByLinks.Visible = byLinks;

            if (objectType == Helper.GetObjectTypeNumericID("User"))
            {
                this.ByActivity.Visible = byActivity;
                this.ByLinks.Visible = false;
            }
            else if (objectType == Helper.GetObjectTypeNumericID("Community"))
            {
                this.ByMember.Visible = byMember;
                this.ByLinks.Visible = false;
            }

            hasContent = (ByTitle.Visible || ByDate.Visible || ByActivity.Visible || ByMember.Visible || ByRatings.Visible || ByVisits.Visible || ByComments.Visible || ByLinks.Visible);
            if (!hasContent) return;

            string queryStringWithoutSO = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "SO", "SD", "PN" }, false);

            this.ByTitle.NavigateUrl = string.Format("{0}?SO={1}&SD={2}{3}", Request.CurrentExecutionFilePath, QuickSort.Title, QuickSortDirection.Asc, queryStringWithoutSO);
            this.ByDate.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.StartDate, queryStringWithoutSO);
            this.ByActivity.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.Agility, queryStringWithoutSO);
            this.ByMember.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.MemberCount, queryStringWithoutSO);
            this.ByRatings.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.RatedAverage, queryStringWithoutSO);
            this.ByRetingConsolidated.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.RatedConsolidated, queryStringWithoutSO);
            this.ByVisits.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.Viewed, queryStringWithoutSO);
            this.ByComments.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.Commented, queryStringWithoutSO);
            this.ByLinks.NavigateUrl = string.Format("{0}?SO={1}{2}", Request.CurrentExecutionFilePath, QuickSort.Linked, queryStringWithoutSO);

            if (noSort && !string.IsNullOrEmpty(Request.QueryString["SO"]))
            {
                this.NoSort.Visible = true;
                this.NoSort.NavigateUrl = string.Format("{0}?{1}", Request.CurrentExecutionFilePath, queryStringWithoutSO);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["SO"]))
            {
                switch ((QuickSort)Enum.Parse(typeof(QuickSort), Request.QueryString["SO"]))
                {
                    case QuickSort.StartDate:
                        this.ByDate.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.Agility:
                        this.ByActivity.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.RatedAverage:
                        this.ByRatings.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.RatedConsolidated:
                        this.ByRetingConsolidated.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.Viewed:
                        this.ByVisits.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.Linked:
                        this.ByLinks.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.Commented:
                        this.ByComments.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.Title:
                        this.ByTitle.CssClass = "CSB_admin_menu_i";
                        break;
                    case QuickSort.MemberCount:
                        this.ByMember.CssClass = "CSB_admin_menu_i";
                        break;
                }
            }

        }
    }
}