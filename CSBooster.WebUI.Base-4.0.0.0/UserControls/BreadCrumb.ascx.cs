//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		27.08.2008 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class BreadCrumb : System.Web.UI.UserControl, IBreadCrumb
    {
        private List<Control> breadCrumbs = new List<Control>();

        public string BreadCrumpImage { get; set; }

        public List<Control> BreadCrumbs
        {
            get { return breadCrumbs; }
            set { breadCrumbs = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void RenderBreadCrumbs()
        {
            HtmlGenericControl container = new HtmlGenericControl("div");
            container.Attributes.Add("id", "breadcrumbs");
            if (!string.IsNullOrEmpty(BreadCrumpImage))
            {
                string image = string.Format("<div class=\"breadcrumbImage\"><img src=\"{0}\"></div>", BreadCrumpImage);
                container.Controls.Add(new LiteralControl(image));
            }
            for (int i = 0; i < breadCrumbs.Count; i++)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "breadcrumb");
                div.Controls.Add(breadCrumbs[i]);
                container.Controls.Add(div);
                if (i < breadCrumbs.Count - 1)
                    container.Controls.Add(new LiteralControl("<div class=\"breadcrumbGap\"> - </div>"));
            }
            phBc.Controls.Clear();
            phBc.Controls.Add(container);
        }

        public void RenderDetailPageBreadCrumbs(DataObject dataObject)
        {
            HyperLink userOrCommunityLink = null;
            string userCommunityParameter = "&XCN=";
            if (CustomizationSection.CachedInstance.Common.BreadCrumbUserOrCommunityEnabled)
            {
                userOrCommunityLink = new HyperLink();
                Community community = new Community(dataObject.CommunityID.Value);
                userCommunityParameter = community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity") ? string.Format("&XUI={0}&XCN=", community.ProfileOrCommunity.UserID) : string.Format("&XCN={0}", community.ProfileOrCommunity.ObjectID);
                if (community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    userOrCommunityLink.Text = community.ProfileOrCommunity.Nickname;
                    userOrCommunityLink.NavigateUrl = Helper.GetDetailLink("User", community.ProfileOrCommunity.Nickname);
                }
                else
                {
                    userOrCommunityLink.Text = community.ProfileOrCommunity.Title;
                    userOrCommunityLink.NavigateUrl = Helper.GetDetailLink("Community", ((DataObjectCommunity)community.ProfileOrCommunity).VirtualURL);
                }
            }

            // Set page title
            if (userOrCommunityLink == null)
                this.Page.Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat2, SiteConfig.SiteName, Helper.GetObjectName(dataObject.ObjectType, false), dataObject.Title);
            else
                this.Page.Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat3, SiteConfig.SiteName, userOrCommunityLink.Text, Helper.GetObjectName(dataObject.ObjectType, false), dataObject.Title);

            // Set breadcrumbs
            this.BreadCrumpImage = string.Format("/Library/Images/Layout/{0}", Helper.GetObjectIcon(dataObject.ObjectType));
            
            this.breadCrumbs.Clear();
            if (userOrCommunityLink != null)
                this.BreadCrumbs.Add(userOrCommunityLink);

            if (dataObject.objectType == Helper.GetObjectTypeNumericID("ForumTopic"))
            {
                List<DataObjectForum> forum = DataObjects.Load<DataObjectForum>(new QuickParameters()
                                                                                    {
                                                                                        Udc = UserDataContext.GetUserDataContext(),
                                                                                        Amount = 1,
                                                                                        DisablePaging = true,
                                                                                        RelationParams = new RelationParams()
                                                                                                             {
                                                                                                                 ChildObjectID = dataObject.ObjectID
                                                                                                             }
                                                                                    });
                if (forum.Count == 1)
                {
                    this.BreadCrumbs.Add(new HyperLink() { Text = Helper.GetObjectName("Forum", false), NavigateUrl =  Helper.GetOverviewLink("Forum", false).Replace("&XCN=", "") + userCommunityParameter });
                    this.BreadCrumbs.Add(new HyperLink() { Text = forum[0].Title, NavigateUrl =  Helper.GetDetailLink(Helper.GetObjectTypeNumericID("Forum"), forum[0].ObjectID.ToString()) });
                }
            }
            else
            {
                this.BreadCrumbs.Add(new HyperLink() { Text = Helper.GetObjectName(dataObject.ObjectType, false), NavigateUrl =  Helper.GetOverviewLink(dataObject.ObjectType, false).Replace("&XCN=", "") + userCommunityParameter });
            }

            if (!string.IsNullOrEmpty(Request.QueryString["CFID"]))
            {
                string[] folderIds = Request.QueryString["CFID"].Split(';');
                for (int i = 0; i < folderIds.Length; i++)
                {
                    string parentFolderIds = string.Empty;
                    for (int j = 0; j < i; j++)
                        parentFolderIds += folderIds[j] + ";";
                    if (parentFolderIds.Length > 0)
                        parentFolderIds = "&CFID=" + parentFolderIds;
                    parentFolderIds = parentFolderIds.TrimEnd(';');

                    DataObjectFolder folder = DataAccess.Business.DataObject.Load<DataObjectFolder>(folderIds[i].ToGuid());
                    if (folder.State != ObjectState.Added)
                    {
                        this.BreadCrumbs.Add(new HyperLink() { Text = folder.Title, NavigateUrl =  Helper.GetDetailLink("Folder", folder.ObjectID.ToString(), false) + parentFolderIds });
                    }
                }
            }

            this.BreadCrumbs.Add(new LiteralControl(dataObject.Title));
            this.RenderBreadCrumbs();
        }

        public void RenderOverviewPageBreadCrumbs(QuickParameters quickParameters)
        {
            HyperLink userOrCommunityLink = null;
            if (CustomizationSection.CachedInstance.Common.BreadCrumbUserOrCommunityEnabled)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["XUI"]))
                {
                    MembershipUser membershipUser = Membership.GetUser(new Guid(Request.QueryString["XUI"]), false);
                    userOrCommunityLink = new HyperLink();
                    userOrCommunityLink.Text = membershipUser.UserName;
                    userOrCommunityLink.NavigateUrl = Helper.GetDetailLink("User", membershipUser.UserName);
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                {
                    Guid communityId = new Guid(Request.QueryString["XCN"]);
                    DataObjectCommunity community = DataAccess.Business.DataObject.Load<DataObjectCommunity>(communityId);
                    userOrCommunityLink = new HyperLink();
                    userOrCommunityLink.Text = community.Title;
                    userOrCommunityLink.NavigateUrl =  Helper.GetDetailLink(community.ObjectType, community.VirtualURL);
                }
            }

            // Set page title
            if (userOrCommunityLink == null)
                this.Page.Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat1, SiteConfig.SiteName, Helper.GetObjectName(quickParameters.ObjectType, false));
            else
                this.Page.Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat2, SiteConfig.SiteName, userOrCommunityLink.Text, Helper.GetObjectName(quickParameters.ObjectType, false));

            // Set breadcrumbs
            this.BreadCrumpImage = string.Format("/Library/Images/Layout/{0}", Helper.GetObjectIcon(quickParameters.ObjectType));
            this.breadCrumbs.Clear();
            if (userOrCommunityLink != null)
                this.BreadCrumbs.Add(userOrCommunityLink);
            this.BreadCrumbs.Add(new LiteralControl(string.Format("<h1>{0}</h1>", Helper.GetObjectName(quickParameters.ObjectType, false))));
            this.RenderBreadCrumbs();
        }

        public void RenderAdminPageBreadCrumbs(string title)
        {
            // Set page title
            this.Page.Master.Page.Title = string.Format(CustomizationSection.CachedInstance.Common.TitleFormat2, SiteConfig.SiteName, UserProfile.Current.Nickname, title);

            // Set breadcrumbs
            this.BreadCrumpImage = string.Format("/Library/Images/Layout/{0}",  Helper.GetObjectIcon(Helper.GetObjectTypeNumericID("User")));
            this.breadCrumbs.Clear();
            this.BreadCrumbs.Add(new HyperLink() { Text = UserProfile.Current.Nickname, NavigateUrl =  Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), UserProfile.Current.Nickname) });
            this.BreadCrumbs.Add(new LiteralControl(title));
            this.RenderBreadCrumbs();
        }
    }
}