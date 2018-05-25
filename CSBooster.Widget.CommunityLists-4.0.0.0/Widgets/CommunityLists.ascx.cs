// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public partial class CommunityLists : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetCommunityLists");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            int communityListType = XmlHelper.GetElementValue(xmlDom.DocumentElement, "CommunityListsType", 0);  
            int pageSize = XmlHelper.GetElementValue(xmlDom.DocumentElement, "PageSize", 5);
            QuickSort sortBy = (QuickSort)XmlHelper.GetElementValue(xmlDom.DocumentElement, "SortBy", (int)QuickSort.Title);
            bool showPagerTop = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowPagerTop", true);
            bool showPagerBottom = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowPagerBottom", true);

            string template = "CommunityListsLarge.ascx";
            string repeater = "CommunityLists.ascx";
            if (WidgetHost.OutputTemplate != null)
            { 
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }
            

            int amount = 0; // default aus SiteConfig lesen
            if (!showPagerTop && !showPagerBottom)
                amount = pageSize; 

            IRepeater overview = null;
            if (communityListType == 1)
                overview = LoadMyCommunities(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);
            else if (communityListType == 2)
                overview = LoadMyMemberships(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);

            if (overview != null)
            {
                this.Ph.Controls.Add((Control)overview);
                hasContent = overview.HasContent;  
            }

            return true;
        }

        private IRepeater LoadMyCommunities(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            DataObject community = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);

            QuickParameters paras = new QuickParameters();
            paras.ObjectType = 1;
            paras.Udc = UserDataContext.GetUserDataContext();
            if (community.ObjectType == 1)
            {
                paras.ParentObjectID = this.WidgetHost.ParentCommunityID.ToString();  
            }
            else
            {
                paras.MembershipParams = new MembershipParams()
                {
                    UserID = community.UserID,
                    IsCreator = true
                };
            }
            paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = QuickSort.Title;
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
                if (community.ObjectType == 1)
                {
                    overview.ItemNameSingular = language.GetString("TextMyGroupsSingular");
                    overview.ItemNamePlural = language.GetString("TextMyGroupsPlural");
                }
                else
                {
                    overview.ItemNameSingular = language.GetString("TextMyCommunitiesSingular");
                    overview.ItemNamePlural = language.GetString("TextMyCommunitiesPlural");
                }
            }

            ISettings settings = ctrl as ISettings;
            if (settings != null)
            {
                if (settings.Settings == null)
                    settings.Settings = new System.Collections.Generic.Dictionary<string, object>();

                if (!settings.Settings.ContainsKey("ParentPageType"))
                    settings.Settings.Add("ParentPageType", (int)WidgetHost.ParentPageType);
            }

            return overview;
        }

        private IRepeater LoadMyMemberships(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            DataObject community = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);

            QuickParameters paras = new QuickParameters();
            paras.ObjectType = 1;
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.MembershipParams = new MembershipParams()
            {
                UserID = community.UserID
            };
            paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = QuickSort.Title;
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
                overview.ItemNameSingular = language.GetString("TextMyMembershipsSingular");
                overview.ItemNamePlural = language.GetString("TextMyMembershipsPlural");
            }

            ISettings settings = ctrl as ISettings;
            if (settings != null)
            {
                if (settings.Settings == null)
                    settings.Settings = new System.Collections.Generic.Dictionary<string, object>();

                if (!settings.Settings.ContainsKey("ParentPageType"))
                    settings.Settings.Add("ParentPageType", (int)WidgetHost.ParentPageType);
            }

            return overview;
        }

    }
}