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
    public partial class UserLists : WidgetBase
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserLists");
        private bool hasContent = false;

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);

            int userListType = XmlHelper.GetElementValue(xmlDom.DocumentElement, "UserListType", 0);  
            int pageSize = XmlHelper.GetElementValue(xmlDom.DocumentElement, "PageSize", 5);
            QuickSort sortBy = (QuickSort)XmlHelper.GetElementValue(xmlDom.DocumentElement, "SortBy", (int)QuickSort.InsertedDate);
            bool showPagerTop = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowPagerTop", true);
            bool showPagerBottom = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowPagerBottom", true);

            string template = "UserListsLarge.ascx";
            string repeater = "UserLists.ascx";
            if (WidgetHost.OutputTemplate != null)
            { 
                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.OutputTemplateControl))
                    template = WidgetHost.OutputTemplate.OutputTemplateControl;

                if (!string.IsNullOrEmpty(WidgetHost.OutputTemplate.RepeaterControl))
                    repeater = WidgetHost.OutputTemplate.RepeaterControl;
            }

            int amount = -1; // default aus SiteConfig lesen
            if (!showPagerTop && !showPagerBottom)
                amount = pageSize; 

            IRepeater overview = null;
            if (userListType == 1)
                overview = LoadNewestUsers(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);
            else if (userListType == 2)
                overview = LoadMyFriends(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);
            else if (userListType == 3)
                overview = LoadCommunityMembers(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);
            else if (userListType == 4)
                overview = LoadLastVisitors(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);
            else if (userListType == 5)
                overview = LoadLastOnlineUsers(pageSize, showPagerTop, showPagerBottom, amount, sortBy, repeater, template);

            if (overview != null)
            {
                this.Ph.Controls.Add((Control)overview);
                hasContent = overview.HasContent;  
            }

            return hasContent;
        }

        private IRepeater LoadNewestUsers(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            QuickParametersUser paras = new QuickParametersUser();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            if (amount >= 0)
                paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = sortBy;
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
                overview.ItemNameSingular = language.GetString("TextNewUsersSingular");
                overview.ItemNamePlural = language.GetString("TextNewUsersPlural");  
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

        private IRepeater LoadMyFriends(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {

            QuickParametersFriends paras = new QuickParametersFriends();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            if (this.WidgetHost.ParentPageType == PageType.None)
            {
                DataObject user = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);
                paras.CurrentUserID = user.UserID; 
            }
            else
            {
                paras.CurrentUserID = paras.Udc.UserID;
            }
            paras.OnlyNotBlocked = true;
            if (amount >= 0)
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
                overview.ItemNameSingular = language.GetString("TextMyFriendsSingular");
                overview.ItemNamePlural = language.GetString("TextMyFriendsPlural");  
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

        private IRepeater LoadCommunityMembers(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            QuickParametersUser paras = new QuickParametersUser();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.MembershipParams = new MembershipParams()
            {
                CommunityID = this.WidgetHost.ParentCommunityID
            };
            if (amount >= 0)
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
                overview.ItemNameSingular = language.GetString("TextCommunityMembersSingular");
                overview.ItemNamePlural = language.GetString("TextCommunityMembersPlural");  
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

        public IRepeater LoadLastVisitors(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            DataObject community = DataObject.Load<DataObject>(this.WidgetHost.ParentCommunityID);

            QuickParametersUser paras = new QuickParametersUser();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.ViewLogParams = new ViewLogParams()
                                {
                                    ObjectID = community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") ? community.UserID : this.WidgetHost.ParentCommunityID, 

                                };
            if (amount >= 0)
                paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = sortBy;
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
                overview.ItemNameSingular = language.GetString("TextLastVisitorsSingular");
                overview.ItemNamePlural = language.GetString("TextLastVisitorsPlural");
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

        private IRepeater LoadLastOnlineUsers(int pageSize, bool showPagerTop, bool showPagerBottom, int amount, QuickSort sortBy, string repeater, string template)
        {
            QuickParametersUser paras = new QuickParametersUser();
            paras.ObjectType = 2;
            paras.Udc = UserDataContext.GetUserDataContext();
            paras.IsOnline = true;
            if (amount >= 0)
                paras.Amount = amount;
            paras.PageNumber = 1;
            paras.PageSize = pageSize;
            paras.DisablePaging = (!showPagerTop && !showPagerBottom);
            paras.SortBy = QuickSort.Random;
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
                overview.ItemNameSingular = language.GetString("TextOnlineUsersSingular");
                overview.ItemNamePlural = language.GetString("TextOnlineUsersPlural");
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