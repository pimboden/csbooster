//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		04.02.2009 / AW
//******************************************************************************
using System;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

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
                int relatedObjectType = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "RelatedObjectType", 0);
                int pageSize = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "NumberRelations", 5);
                QuickSort sortBy = (QuickSort)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "SortBy", (int)QuickSort.Title);
                bool showPagerTop = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", true);
                bool showPagerBottom = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);

                xmlDocument = null;

                Guid objectId = Request.QueryString["OID"].ToGuid();
                DataObject dataObject = DataObject.Load<DataObject>(objectId);

                if (string.IsNullOrEmpty(dataObject.TagList))
                    return false;

                if (relatedObjectType == 0)
                    relatedObjectType = dataObject.ObjectType;

                string template = "SmallOutputObject.ascx";
                string repeater = "DataObjectLists.ascx";
                if (_Host.OutputTemplate != null)
                {
                    if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                        template = _Host.OutputTemplate.OutputTemplateControl;

                    if (!string.IsNullOrEmpty(_Host.OutputTemplate.RepeaterControl))
                        repeater = _Host.OutputTemplate.RepeaterControl;
                }

                int amount = -1; // default aus SiteConfig lesen
                if (!showPagerTop && !showPagerBottom)
                    amount = pageSize;

                IRepeater overview = LoadMyObjects(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template, relatedObjectType, dataObject.TagList, dataObject.ObjectID);
                if (overview != null)
                {
                    this.Ph.Controls.Add((Control)overview);
                    return overview.HasContent;
                }
            }
            return false;
        }

        private IRepeater LoadMyObjects(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template, int relatedObjectType, string tagList, Guid? excludeObjectId)
        {
            DataObject community = DataObject.Load<DataObject>(this.CommunityID);

            QuickParameters paras = new QuickParameters();
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.ObjectType = relatedObjectType;
            paras.Tags1 = QuickParameters.GetDelimitedTagIds(tagList, Constants.TAG_DELIMITER);
            paras.ExcludeObjectIds = excludeObjectId.ToString();
            if (amount >= 0) 
                paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = QuickSort.Title;
            if (sortBy == QuickSort.Title)  
                paras.Direction = QuickSortDirection.Asc;
            paras.IgnoreCache = false;
            paras.WithCopy = false;
            paras.OnlyConverted = true;
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
                    settings.Settings.Add("ParentPageType", (int)_Host.ParentPageType);

                settings.Settings.Add("Width", _Host.ColumnWidth);
            }

            return overview;
        }

    }
}