using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.WebUI.UserControls;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI
{
    public partial class Default : System.Web.UI.Page
    {
        private CSBooster_DataContext wdc;
        private DataObject pageOrCommunity;
        private hitbl_Community_CTY community;
        private hitbl_Page_PAG currentPage;
        private List<hitbl_Page_PAG> pages;
        private PageType parentPageType = PageType.None;
        private bool isReadOnly = true;
        private bool isOwner;
        private bool isMember;
        private string page;
        private Panel[] columnPanels;
        private Dictionary<int, int> columnWidths;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());

            if (!string.IsNullOrEmpty(Request.QueryString["UI"]))
            {
                string userKey = Request.QueryString["UI"];
                MembershipUser membershipUser;
                if (userKey.IsGuid())
                    membershipUser = Membership.GetUser(userKey.ToGuid(), false);
                else
                    membershipUser = Membership.GetUser(userKey, false);
                if (membershipUser != null)
                {
                    PageInfo.UserId = membershipUser.ProviderUserKey.ToString().ToNullableGuid();
                    PageInfo.EffectiveCommunityId = UserProfile.GetProfile(membershipUser.UserName).ProfileCommunityID;
                    community = wdc.hitbl_Community_CTies.Where(x => x.CTY_ID == PageInfo.EffectiveCommunityId).SingleOrDefault();
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CN"]))
            {
                string communityKey = Request.QueryString["CN"];
                if (!communityKey.IsGuid())
                {
                    community = wdc.hitbl_Community_CTies.Where(x => x.CTY_VirtualUrl == communityKey).SingleOrDefault();
                    if (community != null && !community.CTY_IsProfile)
                        PageInfo.CommunityId = community.CTY_ID;
                }
                else
                {
                    PageInfo.CommunityId = communityKey.ToGuid();
                    community = wdc.hitbl_Community_CTies.Where(x => x.CTY_ID == PageInfo.CommunityId).SingleOrDefault();
                }
                PageInfo.EffectiveCommunityId = PageInfo.CommunityId;
            }
            else
            {
                community = wdc.hitbl_Community_CTies.Where(x => x.CTY_VirtualUrl == "Default").SingleOrDefault();
                PageInfo.CommunityId = community.CTY_ID;
                PageInfo.EffectiveCommunityId = PageInfo.CommunityId;
            }

            if (PageInfo.EffectiveCommunityId.HasValue)
            {
                pageOrCommunity = DataObject.Load<DataObject>(PageInfo.EffectiveCommunityId);
                if (pageOrCommunity.State == ObjectState.Added)
                {
                    PageInfo.CommunityId = null;
                    PageInfo.EffectiveCommunityId = null;
                }
            }
            if (!PageInfo.EffectiveCommunityId.HasValue)
            {
                Response.Redirect("/Pages/Static/ObjectNotFound.aspx");
            }

            page = Request.QueryString["P"] ?? string.Empty;

            PreparePage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!isReadOnly)
                ScriptManager.GetCurrent(this).Services.Add(new ServiceReference("/Services/WidgetService.asmx"));

            // Show customization bar
            if (!string.IsNullOrEmpty(Request.QueryString["edit"]) && !isReadOnly)
            {
                if (Request.QueryString["edit"] == "content")
                {
                    CustomizationBarContent customizationBar = (CustomizationBarContent)LoadControl("~/UserControls/CustomizationBarContent.ascx");
                    customizationBar.ID = "cb";
                    customizationBar.CommunityID = PageInfo.EffectiveCommunityId.Value;
                    PhCB.Controls.Add(customizationBar);
                }
                else if (Request.QueryString["edit"] == "style")
                {
                    CustomizationBarStyle customizationBar = (CustomizationBarStyle)LoadControl("~/UserControls/CustomizationBarStyle.ascx");
                    customizationBar.ID = "cb";
                    customizationBar.CommunityID = PageInfo.EffectiveCommunityId.Value;
                    PhCB.Controls.Add(customizationBar);
                }

                // Show dialogs
                if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    List<string> pageNames = new List<string>();
                    pageNames.Add("ProfileEditStyle");
                    List<Dialog> dialogs = DialogEngine.GetDialogByPageName(pageNames, UserProfile.Current.UserId);
                    if (dialogs.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (Dialog dialog in dialogs)
                        {
                            sb.AppendFormat("<div><b>{0}</b><br/>{1}</div>", dialog.Title, dialog.Content);
                            sb.AppendFormat("<div style=\"margin-top:10px;margin-bottom:10px;height:1px;background-color:#CCCCCC;\"></div>");
                        }
                        string content = Regex.Replace(sb.ToString(), "<(.*?)>", "&lt;$1&gt;"); // Ugly, but safari needs it
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "DialogWin", "SetPopupWindow('" + this.ClientID + "', 700, 0, 200, 'Mitteilungen', '" + content + "', true);", true);
                    }
                }
            }

            // Load tabbar
            if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community") || pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                TabBar tabBar = (TabBar)LoadControl("~/UserControls/TabBar.ascx");
                tabBar.ID = "tb";
                tabBar.WDC = wdc;
                tabBar.Pages = pages;
                tabBar.CurrentPage = currentPage;
                tabBar.IsOwner = isOwner;
                tabBar.IsMember = isMember;
                tabBar.PageOrCommunity = pageOrCommunity;
                tabBar.Community = community;
                Ph.Controls.Add(tabBar);
            }

            // Load layout
            Layout layout = Layouts.GetLayout(community.CTY_Layout);
            if (layout == null)
            {
                layout = Layouts.GetLayout("Default");
                community.CTY_Layout = "Default";
                wdc.SubmitChanges();
                wdc.hisp_WidgetInstance_ReorderColumns(PageInfo.EffectiveCommunityId.Value, layout.NumberDropZones);
            }
            if (isReadOnly)
                Ph2.Controls.Add(Page.ParseControl(layout.DisplayTemplate));
            else
                Ph2.Controls.Add(Page.ParseControl(layout.DragDropTemplate));
            columnWidths = layout.ColumnWidths;
            columnPanels = new Panel[layout.NumberDropZones];
            for (int i = 0; i < columnPanels.Length; i++)
            {
                columnPanels[i] = (Panel)Ph2.FindControl("WCP" + i);
                if (!isReadOnly)
                    columnPanels[i].Attributes["PageId"] = currentPage.PAG_ID.ToString();
            }

            LoadWidgets();
        }

        private void PreparePage()
        {
            // Check access rights and increase view count
            if ((pageOrCommunity.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
                isReadOnly = false;
            if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") && !CustomizationSection.CachedInstance.CustomizationBar.Enabled)
                isReadOnly = true;
            if (UserDataContext.GetUserDataContext().IsAdmin)
                isReadOnly = false;

            if (UserProfile.Current.UserId != Guid.Empty)
                isOwner = Community.GetIsUserOwner(UserProfile.Current.UserId, PageInfo.EffectiveCommunityId.Value, out isMember);

            if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                if (pageOrCommunity.Status == ObjectStatus.Public || isOwner || isMember)
                    DataObject.AddViewed(UserDataContext.GetUserDataContext(), pageOrCommunity.UserID, Helper.GetObjectTypeNumericID("Community"));
                else
                    Response.Redirect("/pages/static/AccessDenied.aspx", true);
            }
            else if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                DataObject.AddViewed(UserDataContext.GetUserDataContext(), pageOrCommunity.UserID, Helper.GetObjectTypeNumericID("User"));
                DataObject.AddViewed(UserDataContext.GetUserDataContext(), pageOrCommunity.UserID, Helper.GetObjectTypeNumericID("ProfileCommunity"));
            }
            else if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                if (pageOrCommunity.RoleRight.ContainsKey(UserDataContext.GetUserDataContext().UserRole) && pageOrCommunity.RoleRight[UserDataContext.GetUserDataContext().UserRole])
                    DataObject.AddViewed(UserDataContext.GetUserDataContext(), pageOrCommunity.UserID, Helper.GetObjectTypeNumericID("Page"));
                else
                    Response.Redirect("/pages/static/AccessDenied.aspx", true);
            }

            // Load pages
            pages = wdc.hitbl_Page_PAGs.Where(x => x.CTY_ID == PageInfo.EffectiveCommunityId).OrderBy(x => x.PAG_OrderNr).ToList();
            if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community") || pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
            {
                int pageIndex;
                if (isOwner && (page.ToLower() == "dashboard" || string.IsNullOrEmpty(page)))
                {
                    currentPage = pages[0];
                    page = "dashboard";
                }
                else if (!string.IsNullOrEmpty(page) && int.TryParse(page, out pageIndex))
                {
                    currentPage = pages[pageIndex];
                }
                else
                {
                    currentPage = pages[1];
                }
            }
            else
            {
                currentPage = pages[0];
            }

            hitbl_Community_CTY owningCommunity = null;
            Guid? owningCommunityId = null;
            int owningObjectType = 0;

            // Set theme and style
            bool themeAndStyleOverridden = false;
            if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                DataObjectPage page = DataObject.Load<DataObjectPage>(PageInfo.EffectiveCommunityId);
                parentPageType = page.PageType;
                if (parentPageType == PageType.Detail && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    DataObject detail = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
                    if (detail.State != ObjectState.Added)
                    {
                        owningCommunityId = detail.CommunityID;
                        owningObjectType = page.ObjectType;
                        owningCommunity = wdc.hitbl_Community_CTies.Where(x => x.CTY_ID == owningCommunityId).SingleOrDefault();
                        themeAndStyleOverridden = true;
                    }
                    else
                    {
                        Response.Redirect("/Pages/Static/ObjectNotFound.aspx");
                    }
                }
                else if (parentPageType == PageType.Overview && (!string.IsNullOrEmpty(Request.QueryString["XUI"]) || !string.IsNullOrEmpty(Request.QueryString["XCN"])))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                    {
                        MembershipUser membershipUser = Membership.GetUser(new Guid(Request.QueryString["XUI"]), false);
                        owningCommunityId = UserProfile.GetProfile(membershipUser.UserName).ProfileCommunityID;
                        owningObjectType = Helper.GetObjectType("ProfileCommunity").NumericId;
                    }
                    else if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                    {
                        owningCommunityId = new Guid(Request.QueryString["XCN"]);
                        owningObjectType = Helper.GetObjectType("Community").NumericId;
                    }
                    owningCommunity = wdc.hitbl_Community_CTies.Where(x => x.CTY_ID == owningCommunityId).SingleOrDefault();
                    themeAndStyleOverridden = true;
                }
            }

            if (!themeAndStyleOverridden)
                owningCommunity = community;

            Theme = owningCommunity.CTY_Theme;
            ((IWidgetPageMaster)Page.Master).HeaderStyle = owningCommunity.CTY_HeaderStyle;
            ((IWidgetPageMaster)Page.Master).BodyStyle = owningCommunity.CTY_BodyStyle;
            ((IWidgetPageMaster)Page.Master).FooterStyle = owningCommunity.CTY_FooterStyle;

            //Add custom widget styles
            var widgetStyles = wdc.hisp_WidgetTemplates_GetCommunityTemplates(PageInfo.EffectiveCommunityId).ToList();
            StringBuilder styles = new StringBuilder("<style>");
            foreach (var widgetStyle in widgetStyles)
            {
                styles.Append(widgetStyle.WTP_Template);
            }
            styles.Append("</style>");
            PhStyle.Controls.Clear();
            PhStyle.Controls.Add(new LiteralControl(styles.ToString()));

            // Set metadata and breadcrumbs
            // TODO: weitere Format-Config's in Customizaition.config machen (die vorhandenen sind unklar benannt und somit weiss man nicht wo verwendet) 
            ((IWidgetPageMaster)Page.Master).MetaDescription.Content = Server.HtmlEncode(pageOrCommunity.Description.StripHTMLTags());
            ((IWidgetPageMaster)Page.Master).MetaKeywords.Content = Server.HtmlEncode(pageOrCommunity.TagList.StripHTMLTags().Replace(Common.Constants.TAG_DELIMITER.ToString(), ","));

            ((IWidgetPageMaster)Page.Master).MetaOgSiteName.Content = SiteConfig.SiteName;
            ((IWidgetPageMaster)Page.Master).MetaOgUrl.Content = SiteConfig.SiteURL + Request.RawUrl;
            ((IWidgetPageMaster)Page.Master).MetaOgTitle.Content = Server.HtmlEncode(pageOrCommunity.Title.StripHTMLTags());

            if (parentPageType == PageType.Homepage)
            {
                Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat1, SiteConfig.SiteName, pageOrCommunity.Title);
            }
            else if (parentPageType == PageType.Overview) 
            {
                ((IWidgetPageMaster)Page.Master).RssLink.Href = "/pages/other/rssfeed.aspx" + Request.Url.Query;
            }
            else if (parentPageType == PageType.Detail)
            {
                if (Helper.GetObjectTypeNumericID(Request.QueryString["OT"]) == Helper.GetObjectTypeNumericID("ForumTopic") && !string.IsNullOrEmpty(Request.QueryString["OID"]))
                    ((IWidgetPageMaster)Page.Master).RssLink.Href = "/pages/other/rssfeed.aspx?OT=ForumTopicItem&RPID=" + Request.QueryString["OID"];
                else if(Helper.GetObjectTypeNumericID(Request.QueryString["OT"]) == Helper.GetObjectTypeNumericID("Forum"))
                    ((IWidgetPageMaster)Page.Master).RssLink.Href = "/pages/other/rssfeed.aspx?OT=ForumTopicItem";
            }
            else if (parentPageType == PageType.None)
            {
                if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") && page.ToLower() != "dashboard")
                {
                    ((IWidgetPageMaster)Page.Master).BreadCrumb.BreadCrumbs.Add(new LiteralControl(string.Format("{0}", pageOrCommunity.Nickname)));
                    ((IWidgetPageMaster)Page.Master).BreadCrumb.BreadCrumpImage = string.Format("/Library/Images/Layout/{0}", Helper.GetObjectIcon(Helper.GetObjectTypeNumericID("User")));
                    ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderBreadCrumbs();
                    ((IWidgetPageMaster)Page.Master).RssLink.Href = "/pages/other/rssfeed.aspx" + Request.Url.Query;
                    Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat1, SiteConfig.SiteName, pageOrCommunity.Nickname);
                }
                else if (pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Community") || pageOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("Page"))
                {
                    ((IWidgetPageMaster)Page.Master).BreadCrumb.BreadCrumpImage = string.Format("/Library/Images/Layout/{0}", Helper.GetObjectIcon(pageOrCommunity.ObjectType));
                    string title = Regex.Replace(pageOrCommunity.Title, @"\[tracking=.*?\]", "", RegexOptions.IgnoreCase);

                    if (!pageOrCommunity.ParentObjectID.HasValue)
                    {
                        ((IWidgetPageMaster)Page.Master).BreadCrumb.BreadCrumbs.Add(new LiteralControl(title));
                        ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderBreadCrumbs();
                        Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat1, SiteConfig.SiteName, title);
                    }
                    else
                    {
                        Community parentCommunity = new Community(pageOrCommunity.ParentObjectID.Value);
                        string parentTitle = Regex.Replace(parentCommunity.ProfileOrCommunity.Title, @"\[tracking=.*?\]", "", RegexOptions.IgnoreCase);
                        ((IWidgetPageMaster)Page.Master).BreadCrumb.BreadCrumbs.Add(new LiteralControl(string.Format("<a href='{0}'>{1}</a> / {2}", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("Community"), ((DataObjectCommunity)parentCommunity.ProfileOrCommunity).VirtualURL, true), parentTitle, title)));
                        ((IWidgetPageMaster)Page.Master).BreadCrumb.RenderBreadCrumbs();
                        Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat1, SiteConfig.SiteName, parentTitle + " / " + title);
                    }
                }
            }

            // Track events
            if (pageOrCommunity.objectType == Helper.GetObjectType("Page").NumericId)
            {
                if (parentPageType == PageType.Overview)
                    _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(owningCommunityId, owningObjectType, IsPostBack, LogSitePageType.Overview);
                else if (parentPageType == PageType.Detail)
                    _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(owningCommunityId, owningObjectType, IsPostBack, LogSitePageType.Detail);
                else if (parentPageType == PageType.Homepage)
                    _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(owningCommunityId, owningObjectType, IsPostBack, LogSitePageType.Homepage);
                else
                    _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(PageInfo.EffectiveCommunityId, pageOrCommunity.ObjectType, IsPostBack, LogSitePageType.CmsPage);
            }
            else if (pageOrCommunity.objectType == Helper.GetObjectType("Community").NumericId)
                _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(pageOrCommunity.ObjectID, pageOrCommunity.ObjectType, IsPostBack, LogSitePageType.Community);
            else if (pageOrCommunity.objectType == Helper.GetObjectType("ProfileCommunity").NumericId)
                _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(pageOrCommunity.ObjectID, pageOrCommunity.ObjectType, IsPostBack, LogSitePageType.UserProfile);
        }

        private void LoadWidgets()
        {
            foreach (Panel panel in columnPanels)
            {
                List<WidgetContainer> widgets = panel.Controls.OfType<WidgetContainer>().ToList();
                foreach (var widget in widgets)
                    panel.Controls.Remove(widget);
            }

            var widgetInstances = currentPage.hitbl_WidgetInstance_INs.OrderBy(x => x.INS_OrderNo).ToList();
            foreach (var widgetInstance in widgetInstances)
            {
                if (widgetInstance.INS_ColumnNo < columnPanels.Length)
                {
                    Panel panel = columnPanels[widgetInstance.INS_ColumnNo];

                    WidgetContainer widgetContainer = (WidgetContainer)LoadControl(Constants.WIDGET_CONTAINER);
                    if (!isReadOnly)
                        widgetContainer.ID = "WidgetContainer" + widgetInstance.INS_ID.ToString().Replace("-", "_");
                    else
                        widgetContainer.ID = "WC" + currentPage.PAG_OrderNr + widgetInstance.INS_ColumnNo + widgetInstance.INS_OrderNo;
                    widgetContainer.WidgetInstance = widgetInstance;
                    widgetContainer.IsReadOnly = isReadOnly;
                    widgetContainer.ColumnWidth = columnWidths[widgetInstance.INS_ColumnNo];
                    widgetContainer.ParentCommunityID = pageOrCommunity.CommunityID.Value;
                    widgetContainer.ParentObjectType = pageOrCommunity.ObjectType;
                    widgetContainer.ParentPageType = parentPageType;
                    widgetContainer.SiteContext = SiteConfig.GetSiteContext(UserProfile.Current);
                    widgetContainer.OnWidgetDeleted += OnDeleteWidget;

                    try
                    {
                        panel.Controls.Add(widgetContainer);
                    }
                    catch (Exception exception)
                    {
                        panel.Controls.Add(new LiteralControl(exception.Message));
                    }
                }
            }

            if (isReadOnly)
            {
                foreach (Panel panel in columnPanels)
                {
                    if (panel.Controls.Count == 0)
                        panel.Visible = false;
                }
            }
        }

        private void OnDeleteWidget(hitbl_WidgetInstance_IN widgetInstance)
        {
            wdc.hitbl_WidgetInstance_INs.DeleteOnSubmit(widgetInstance);
            wdc.SubmitChanges();
            wdc.hisp_WidgetTemplates_ReduceCount(widgetInstance.INS_PAG_ID, widgetInstance.WTP_ID);
            wdc.hisp_WidgetInstance_ReorderByPageColumn(widgetInstance.INS_PAG_ID, widgetInstance.INS_ColumnNo);

            LoadWidgets();
        }
    }
}
