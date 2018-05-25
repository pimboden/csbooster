// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class TagRelatedObjects : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetTagRelatedObjects");

        public override bool ShowObject(string settingsXml)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(settingsXml);
                string relatedObjectType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RelatedObjectType", "0");
                int pageSize = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "NumberRelations", 5);
                QuickSort sortBy = (QuickSort)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.Title);
                QuickSortDirection sortDirection = (QuickSortDirection)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortDirection", (int)QuickSortDirection.Desc);
                bool showPagerTop = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", true);
                bool showPagerBottom = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);

                Guid objectId = Request.QueryString["OID"].ToGuid();
                DataObject dataObject = DataObject.Load<DataObject>(objectId);

                if (string.IsNullOrEmpty(dataObject.TagList))
                    return false;

                if (relatedObjectType == "0")
                    relatedObjectType = dataObject.ObjectType.ToString();

                string template = "SmallOutputObject.ascx";
                string repeater = "DataObjectLists.ascx";
                if (WidgetHost.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                        template = WidgetHost.OutputTemplate.OutputTemplateControl;

                    if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                        repeater = WidgetHost.OutputTemplate.RepeaterControl;
                }

                IRepeater overview = LoadMyObjects(pageSize, showPagerTop, showPagerBottom, sortBy, sortDirection, repeater, template, Helper.GetObjectTypeNumericID(relatedObjectType), dataObject.TagList, dataObject.ObjectID);
                if (overview != null)
                {
                    this.Ph.Controls.Add((Control)overview);
                    return overview.HasContent;
                }
            }
            return false;
        }

        private IRepeater LoadMyObjects(int pageSize, bool showPagerTop, bool showPagerBottom, QuickSort sortBy, QuickSortDirection sortDirection, string repeater, string template, int relatedObjectType, string tagList, Guid? excludeObjectId)
        {
            DataObject community = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);

            QuickParameters paras = new QuickParameters();
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.ObjectType = relatedObjectType;
            paras.Tags1 = QuickParameters.GetDelimitedTagIds(tagList, Constants.TAG_DELIMITER);

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
            if (paras.SortBy != QuickSort.Title && paras.SortBySecond == QuickSort.NotSorted)
            {
                paras.SortBySecond = QuickSort.Title;
                paras.DirectionSecond = QuickSortDirection.Asc;
            }

            paras.ShowState = ObjectShowState.Published;

            Control ctrl = LoadControl("~/UserControls/Repeaters/" + repeater);

            IRepeater overview = ctrl as IRepeater;
            if (overview != null)
            {
                overview.QuickParameters = paras;
                overview.OutputTemplate = template;
                overview.TopPagerVisible = showPagerTop;
                overview.BottomPagerVisible = showPagerBottom;
            }

            ISettings settings = ctrl as ISettings;
            if (settings != null)
            {
                if (settings.Settings == null)
                    settings.Settings = new System.Collections.Generic.Dictionary<string, object>();

                if (!settings.Settings.ContainsKey("ParentPageType"))
                    settings.Settings.Add("ParentPageType", (int)WidgetHost.ParentPageType);

                settings.Settings.Add("Width", WidgetHost.ColumnWidth - WidgetHost.ContentPadding);
                settings.Settings.Add("SelectCurrentPage", true);
                settings.Settings.Add("WidgetInstanceId", WidgetHost.WidgetInstance.INS_ID.ToString());
            }

            return overview;
        }

    }
}