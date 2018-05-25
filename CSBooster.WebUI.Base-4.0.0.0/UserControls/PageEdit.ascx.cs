// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class PageEdit : System.Web.UI.UserControl, ISettings
    {
        private bool isAdmin;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid? containerId = null;
            if (PageInfo.UserId.HasValue)
                containerId = PageInfo.UserId;
            else if (PageInfo.CommunityId.HasValue)
                containerId = PageInfo.CommunityId;

            isAdmin = UserDataContext.GetUserDataContext().IsAdmin;

            if (containerId.HasValue && (isAdmin || CustomizationSection.CachedInstance.CustomizationBar.Enabled))
            {
                DataObject dataObj = DataObject.Load<DataObject>(containerId.Value, null, false);
                if (dataObj.State != ObjectState.Added)
                {
                    if ((dataObj.GetUserAccess(UserDataContext.GetUserDataContext()) & ObjectAccessRight.Update) == ObjectAccessRight.Update)
                    {
                        functions.Visible = true;
                        RenderControls(functionsPlaceHolder, dataObj);
                    }
                }
            }

            functions.ID = null;
        }

        private void RenderControls(PlaceHolder ph, DataObject dataObject)
        {
            if (dataObject.ObjectType == Helper.GetObjectTypeNumericID("Community") || dataObject.ObjectType == Helper.GetObjectTypeNumericID("Page") || dataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
            {
                string urlPath = Request.RawUrl.IndexOf('?') > 0 ? Request.RawUrl.Substring(0, Request.RawUrl.IndexOf('?')) : "";
                if (isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Style"].Enabled || CustomizationSection.CachedInstance.CustomizationBar["Layout"].Enabled || CustomizationSection.CachedInstance.CustomizationBar["Theme"].Enabled)
                {
                    ph.Controls.Add(new LiteralControl(string.Format("<li class=\"editStyle\"><a href=\"{0}?edit=style{1}\" rel=\"nofollow\">{2}</a></li>", urlPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit" }, false), (new TextControl() { LanguageFile = "UserControls.WebUI.Base", TextKey = "TitleCustomizeStyle" }).Text)));
                }
                if (isAdmin || CustomizationSection.CachedInstance.CustomizationBar["Widgets"].Enabled || CustomizationSection.CachedInstance.CustomizationBar["Content"].Enabled)
                {
                    ph.Controls.Add(new LiteralControl(string.Format("<li class=\"editContent\"><a href=\"{0}?edit=content{1}\" rel=\"nofollow\">{2}</a></li>", urlPath, Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit" }, false), (new TextControl() { LanguageFile = "UserControls.WebUI.Base", TextKey = "TitleCustomizeContent" }).Text)));
                }
            }

            if (dataObject.ObjectType != Helper.GetObjectTypeNumericID("User"))
            {
                string editWizardUrl = string.Format("{0}", Helper.GetEditWizardLink(dataObject.ObjectType, dataObject.ObjectID.ToString(), _4screen.CSB.Common.SiteConfig.UsePopupWindows));
                string editWizardLink;
                if (_4screen.CSB.Common.SiteConfig.UsePopupWindows)
                    editWizardLink = string.Format("javascript:radWinOpen('{0}', '{1}', 850, 525, false, null, 'wizardWin')", editWizardUrl, languageShared.GetString("CommandPageSetting").StripForScript());
                else
                    editWizardLink = string.Format("{0}&ReturnUrl={1}", editWizardUrl, System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));

                ph.Controls.Add(new LiteralControl(string.Format("<li class=\"editPage\"><a href=\"{0}\">{1}</a></li>", editWizardLink, (new TextControl() { LanguageFile = "Shared", TextKey = "CommandPageSetting" }).Text)));
            }

            ph.Controls.Add(new LiteralControl(string.Format("<li class=\"manageContent\"><a href=\"{0}\">{1}</a></li>", Helper.GetDashboardLink(Common.Dashboard.ManageContent), (new TextControl() { LanguageFile = "UserControls.WebUI.Base", TextKey = "CommandPageMyContent" }).Text)));

            if (isAdmin)
            {
                string strUploadUrl = string.Format("{0}&OID={1}&ReturnUrl={2}", Helper.GetUploadWizardLink("Page", _4screen.CSB.Common.SiteConfig.UsePopupWindows), Guid.NewGuid().ToString(), System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl)));
                if (_4screen.CSB.Common.SiteConfig.UsePopupWindows)
                    ph.Controls.Add(new LiteralControl(string.Format("<li class=\"createPage\"><a href=\"javascript:radWinOpen('{0}', '{1}', 800, 500, false, null, 'wizardWin')\" rel=\"nofollow\">{2}</a></li>", strUploadUrl, languageShared.GetString("LabelCreatePage").StripForScript(), (new TextControl() { LanguageFile = "Shared", TextKey = "LabelCreatePage" }).Text)));
                else
                    ph.Controls.Add(new LiteralControl(string.Format("<li class=\"createPage\"><a href=\"{0}\" rel=\"nofollow\">{1}</a></li>", strUploadUrl, (new TextControl() { LanguageFile = "Shared", TextKey = "LabelCreatePage" }).Text)));

                ph.Controls.Add(new LiteralControl(string.Format("<li class=\"admin\"><a href=\"/Admin/\" rel=\"nofollow\">{0}</a></li>", (new TextControl() { LanguageFile = "UserControls.WebUI.Base", TextKey = "CommandSiteAdmin" }).Text)));
            }
        }
    }
}
