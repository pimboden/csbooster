// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class ObjectLists : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetObjectLists");

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);

            int dataSource = 1;
            bool saveObjectType = false;
            bool manageSiblings = false;
            bool byUrl = true;
            if (WidgetHost.ParentPageType == PageType.None)
            {
                if (WidgetHost.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (WidgetHost.ParentObjectType == 19)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 0);

                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            }
            else if (WidgetHost.ParentPageType == PageType.Overview || WidgetHost.ParentPageType == PageType.Homepage)
            {
                if (WidgetHost.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (WidgetHost.ParentObjectType == 19)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 0);
                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", true);
            }
            else if (WidgetHost.ParentPageType == PageType.Detail)
            {
                if (WidgetHost.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (WidgetHost.ParentObjectType == 19)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 0);
                else if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                    dataSource = 4;
                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", true);
            }
            else
            {
                dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 1);
                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            }

            string objectType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ObjectType", "0");
            string userId = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "UserId", string.Empty);
            string communitId = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "CommunityId", string.Empty);
            int pageSize = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MaxNumber", 5);
            QuickSort sortBy = (QuickSort)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.StartDate);
            QuickSortDirection sortDirection = (QuickSortDirection)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortDirection", (int)QuickSortDirection.Desc);
            bool showPagerTop = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", true);
            bool showPagerBottom = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
            int featured = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Featured", 0);
            DateConstraint dateConstraint = (DateConstraint)Enum.Parse(typeof(DateConstraint), XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DateConstraint", "None"));
            bool renderHtml = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RenderHtml", false);

            if (objectType == "0")
            {
                if (string.IsNullOrEmpty(Request.QueryString["OT"]))
                    return false;
                else
                    objectType = Request.QueryString["OT"];
            }
            else
            {
                saveObjectType = true;
            }

            string template = "SmallOutputObject.ascx";
            string repeater = "DataObjectLists.ascx";
            if (WidgetHost.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            QuickParameters paras = new QuickParameters();
            if (WidgetHost.ParentObjectType == 1) // Community
                paras.QuerySourceType = QuerySourceType.Community;
            else if (WidgetHost.ParentObjectType == 19)  // Profile
                paras.QuerySourceType = QuerySourceType.Profile;
            else if (WidgetHost.ParentObjectType == 20) // Page
                paras.QuerySourceType = QuerySourceType.Page;
            else
                paras.QuerySourceType = QuerySourceType.MyContent;

            if (XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Anonymous", false))
                paras.Udc = UserDataContext.GetUserDataContext(Constants.ANONYMOUS_USERNAME);
            else
                paras.Udc = UserDataContext.GetUserDataContext();

            paras.ObjectType = Helper.GetObjectTypeNumericID(objectType);

            if (featured > 0)
                paras.Featured = featured;

            if (dataSource != 4)
            {
                paras.Tags1 = QuickParameters.GetDelimitedTagIds(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList1", string.Empty), Constants.TAG_DELIMITER);
                paras.Tags2 = QuickParameters.GetDelimitedTagIds(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList2", string.Empty), Constants.TAG_DELIMITER);
                paras.Tags3 = QuickParameters.GetDelimitedTagIds(XmlHelper.GetElementValue(xmlDocument.DocumentElement, "TagList3", string.Empty), Constants.TAG_DELIMITER);
            }

            if (dataSource == 0)
            {
                DataObject profil = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                paras.UserID = profil.UserID;
                byUrl = false;
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                paras.UserID = userId.ToGuid();
            }
            else if (dataSource == 2)
            {
                paras.Communities = communitId.Replace(",", "|");
            }
            else if (dataSource == 3)
            {
                paras.CommunityID = this.WidgetHost.ParentCommunityID;
            }
            else if (dataSource == 4)
            {
                DataObject detail = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                paras.CommunityID = detail.CommunityID;
                //paras.ExcludeObjectIds = detail.ObjectID.ToString();
                byUrl = false;
                manageSiblings = true;
            }
            else if (dataSource != 1)
            {
                return false;
            }

            if ((!showPagerTop && !showPagerBottom) || sortBy == QuickSort.Random)
            {
                paras.Amount = pageSize;
                paras.DisablePaging = true;
                if (sortBy == QuickSort.Random)
                    paras.IgnoreCache = true;
            }
            else
            {
                paras.PageNumber = 1;
                paras.PageSize = pageSize;
            }

            paras.SortBy = sortBy;
            paras.Direction = sortDirection;

            paras.ShowState = ObjectShowState.Published;

            switch (dateConstraint)
            {
                case DateConstraint.UntilYesterday:
                    paras.FromStartDate = SqlDateTime.MinValue.Value;
                    paras.ToStartDate = DateTime.Today.GetEndOfDay() - new TimeSpan(1, 0, 0, 0);
                    paras.FromEndDate = SqlDateTime.MinValue.Value;
                    paras.ToEndDate = DateTime.Today.GetEndOfDay() - new TimeSpan(1, 0, 0, 0);
                    paras.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    break;
                case DateConstraint.UntilToday:
                    paras.FromStartDate = SqlDateTime.MinValue.Value;
                    paras.ToStartDate = DateTime.Today.GetEndOfDay();
                    paras.FromEndDate = SqlDateTime.MinValue.Value;
                    paras.ToEndDate = DateTime.Today.GetEndOfDay();
                    paras.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    break;
                case DateConstraint.Today:
                    paras.FromStartDate = SqlDateTime.MinValue.Value;
                    paras.ToStartDate = DateTime.Today.GetEndOfDay();
                    paras.FromEndDate = DateTime.Today.GetStartOfDay();
                    paras.ToEndDate = SqlDateTime.MaxValue.Value;
                    paras.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    break;
                case DateConstraint.FromToday:
                    paras.FromStartDate = DateTime.Today.GetStartOfDay();
                    paras.ToStartDate = SqlDateTime.MaxValue.Value;
                    paras.FromEndDate = DateTime.Today.GetStartOfDay();
                    paras.ToEndDate = SqlDateTime.MaxValue.Value;
                    paras.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    break;
                case DateConstraint.FromTomorrow:
                    paras.FromStartDate = DateTime.Today.GetStartOfDay() + new TimeSpan(1, 0, 0, 0);
                    paras.ToStartDate = SqlDateTime.MaxValue.Value;
                    paras.FromEndDate = DateTime.Today.GetStartOfDay() + new TimeSpan(1, 0, 0, 0);
                    paras.ToEndDate = SqlDateTime.MaxValue.Value;
                    paras.DateQueryMethode = QuickDateQueryMethode.BetweenStartRangeEndRange;
                    break;
            }

            if (byUrl)
            {
                paras.FromNameValueCollection(Request.QueryString);
                if (saveObjectType)
                {
                    paras.ObjectType = Helper.GetObjectTypeNumericID(objectType);
                }
            }

            if (paras.SortBy != QuickSort.Title && paras.SortBySecond == QuickSort.NotSorted)
            {
                paras.SortBySecond = QuickSort.Title;
                paras.DirectionSecond = QuickSortDirection.Asc;
            }

            IRepeater overview = CreateControl(paras, showPagerTop, showPagerBottom, repeater, template, manageSiblings, renderHtml);
            if (overview != null)
            {
                this.Ph.Controls.Add((Control)overview);
                return overview.HasContent;
            }

            return false;
        }

        private IRepeater CreateControl(QuickParameters paras, bool showPagerTop, bool showPagerBottom, string repeater, string template, bool manageSiblings, bool renderHtml)
        {
            Control ctrl = LoadControl("~/UserControls/Repeaters/" + repeater);

            IRepeater overview = ctrl as IRepeater;
            if (overview != null)
            {
                overview.QuickParameters = paras;
                overview.OutputTemplate = template;
                overview.TopPagerVisible = showPagerTop;
                overview.BottomPagerVisible = showPagerBottom;
                overview.RenderHtml = renderHtml;
            }

            ISettings settings = ctrl as ISettings;
            if (settings != null)
            {
                if (settings.Settings == null)
                    settings.Settings = new System.Collections.Generic.Dictionary<string, object>();

                if (!settings.Settings.ContainsKey("ParentPageType"))
                    settings.Settings.Add("ParentPageType", (int)WidgetHost.ParentPageType);

                if (WidgetHost.WidgetInstance.INS_HeadingTag.HasValue)
                    settings.Settings["HeadingTag"] = WidgetHost.WidgetInstance.INS_HeadingTag.Value;

                if (manageSiblings)
                    settings.Settings.Add("SelectCurrentPage", manageSiblings);

                settings.Settings.Add("Width", WidgetHost.ColumnWidth - WidgetHost.ContentPadding);
                settings.Settings.Add("WidgetInstanceId", WidgetHost.WidgetInstance.INS_ID.ToString());
            }

            return overview;
        }
    }
}