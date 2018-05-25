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
            /*if (_Host.ParentPageType == PageType.Homepage)
            {
                dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 1);
                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            }
            else*/
            if (_Host.ParentPageType == PageType.None)
            {
                if (_Host.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (_Host.ParentObjectType == 19)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 0);

                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", false);
            }
            else if (_Host.ParentPageType == PageType.Overview || _Host.ParentPageType == PageType.Homepage)
            {
                if (_Host.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (_Host.ParentObjectType == 19)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 0);
                byUrl = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ByUrl", true);
            }
            else if (_Host.ParentPageType == PageType.Detail)
            {
                if (_Host.ParentObjectType == 1)
                    dataSource = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "DataSource", 3);
                else if (_Host.ParentObjectType == 19)
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
            bool showPagerTop = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerTop", true);
            bool showPagerBottom = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "ShowPagerBottom", true);
            int featured = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Featured", 0);
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
            if (_Host.OutputTemplate != null)
            {
                if (!string.IsNullOrEmpty(_Host.OutputTemplate.OutputTemplateControl))
                    template = _Host.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(_Host.OutputTemplate.RepeaterControl))
                    repeater = _Host.OutputTemplate.RepeaterControl;
            }

            QuickParameters paras = new QuickParameters();
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
                DataObject profil = DataObject.Load<DataObject>(this.CommunityID);
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
                paras.CommunityID = this.CommunityID;
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

            xmlDocument = null;

            if (!showPagerTop && !showPagerBottom)
                paras.Amount = pageSize;

            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = sortBy;
            if (sortBy == QuickSort.Title)
                paras.Direction = QuickSortDirection.Asc;
            paras.IgnoreCache = false;
            paras.WithCopy = false;
            paras.OnlyConverted = true;
            paras.ShowState = ObjectShowState.Published;

            if (byUrl)
            {
                paras.FromNameValueCollection(Request.QueryString);
                if (saveObjectType)
                {
                    paras.ObjectType = Helper.GetObjectTypeNumericID(objectType);
                }
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
                    settings.Settings.Add("ParentPageType", (int)_Host.ParentPageType);

                if (manageSiblings)
                    settings.Settings.Add("SelectCurrentPage", manageSiblings);

                settings.Settings.Add("Width", _Host.ColumnWidth);
            }

            return overview;
        }

    }
}