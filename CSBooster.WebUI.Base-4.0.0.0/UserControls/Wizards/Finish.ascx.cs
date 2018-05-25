// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Finish : StepsASCX
    {
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ObjectType == Helper.GetObjectTypeNumericID("Community"))
            {
                DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(ObjectID.Value, null, true);
                string anchorContent = string.Empty;
                if (SiteConfig.UsePopupWindows)
                {
                    anchorContent = string.Format("href=\"javascript:void(0)\" onClick=\"RedirectParentPage('/{0}');GetRadWindow().Close();\"", community.VirtualURL);
                }
                else
                {
                    anchorContent = string.Format("href=\"/{0}\"", community.VirtualURL);
                }
                this.LitMsg.Text = string.Format(language.GetString("MessageCreateCommunitySuccess"), anchorContent, community.Title);
            }
            else if (ObjectType == Helper.GetObjectTypeNumericID("Page"))
            {
                DataObjectPage cmsPage = DataObject.Load<DataObjectPage>(ObjectID.Value, null, true);
                string anchorContent = string.Empty;
                if (SiteConfig.UsePopupWindows)
                {
                    anchorContent = string.Format("href=\"javascript:void(0)\" onClick=\"RedirectParentPage('/{0}');GetRadWindow().Close();\"", cmsPage.VirtualURL);
                }
                else
                {
                    anchorContent = string.Format("href=\"/{0}\"", cmsPage.VirtualURL);
                }
                this.LitMsg.Text = string.Format(language.GetString("MessageCreatePageSuccess"), anchorContent, cmsPage.Title);
            }
            else if (ObjectType == Helper.GetObjectTypeNumericID("User"))
            {
                this.LitMsg.Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("MessageRegistrationSucceded");
            }
            else
            {
                DataObject community = DataObject.Load<DataObject>(CommunityID.Value, null, true);

                string message = string.Empty;
                string goToCommunity = string.Empty;
                string goToCommunityText = string.Empty;
                if (community.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                {
                    message = string.Format(language.GetString("MessageUploadCommunity"), Helper.GetObjectName(ObjectType, false), community.Title);
                    goToCommunity = string.Format("{0}", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("Community"), community.ObjectID.ToString()));
                    goToCommunityText = string.Format(language.GetString("MessageGoTo"), community.Title);
                }
                else if (community.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    message = string.Format(language.GetString("MessageUploadProfile"), Helper.GetObjectName(ObjectType, false), community.Title);
                    goToCommunity = string.Format("{0}", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), community.Nickname));
                    goToCommunityText = string.Format(language.GetString("MessageGoToProfile"));
                }
                string goToMyContent = string.Format("{0}&T={1}&I=true", Helper.GetDashboardLink(Common.Dashboard.ManageContent), ObjectType);
                string goToMyContentText = string.Format(language.GetString("MessageGoToMyContent"), Helper.GetObjectName(ObjectType, false));
                string goToReturnUrl = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    goToReturnUrl = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnUrl"]));

                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                if (SiteConfig.UsePopupWindows)
                {
                    sb.AppendFormat("<li><a href=\"javascript:void(0)\" onClick=\"RedirectParentPage('{0}');GetRadWindow().Close();\">{1}</a></li>", goToCommunity, goToCommunityText);
                    sb.AppendFormat("<li><a href=\"javascript:void(0)\" onClick=\"RedirectParentPage('{0}');GetRadWindow().Close();\">{1}</a></li>", goToMyContent, goToMyContentText);
                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        sb.AppendFormat("<li><a href=\"javascript:void(0)\" onClick=\"RedirectParentPage('{0}');GetRadWindow().Close();\">{1}</a></li>", goToReturnUrl, language.GetString("MessageGoBack"));
                }
                else
                {
                    sb.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", goToCommunity, goToCommunityText);
                    sb.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", goToMyContent, goToMyContentText);
                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        sb.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", goToReturnUrl, language.GetString("MessageGoBack"));
                }
                sb.Append("</ul>");

                this.LitMsg.Text = message;
                this.LitMsg2.Text = sb.ToString();

                Guid? uploadSession = Request.QueryString["UploadSession"].ToNullableGuid();
                if (uploadSession.HasValue)
                {
                    DataObjectList<DataObject> dataObjectsByUploadSession = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(UserProfile.Current.UserName), ObjectType = ObjectType, SortBy = QuickSort.InsertedDate, Direction = QuickSortDirection.Asc, GroupID = uploadSession, DisablePaging = true, IgnoreCache = true, QuerySourceType = QuerySourceType.MyContent });
                    if (dataObjectsByUploadSession.Count > 0)
                    {
                        for (int i = 0; i < dataObjectsByUploadSession.Count; i++)
                            UserActivities.Insert(UserDataContext.GetUserDataContext(), UserActivityWhat.HasUploadetOneObject, dataObjectsByUploadSession[i].CommunityID.Value, dataObjectsByUploadSession[i].ObjectID.Value, false);
                    }
                }
                else if (ObjectID.HasValue)
                {
                    UserActivities.Insert(UserDataContext.GetUserDataContext(), UserActivityWhat.HasUploadetOneObject, CommunityID.Value, ObjectID.Value, false);
                }
            }
        }
    }
}