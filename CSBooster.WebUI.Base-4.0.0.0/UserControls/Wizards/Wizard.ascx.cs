// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Wizard : System.Web.UI.UserControl
    {
        private string wizardId = string.Empty;
        private int stepNumber;
        private Guid? communityId;
        private Guid? objectId;
        private Dictionary<int, StepsASCX> wizardControls = new Dictionary<int, StepsASCX>();
        private bool? communityAccessGranted;
        private WizardElement wizard;
        private List<WizardStepElement> wizardSteps;
        private bool closedUserGroup;

        protected void Page_Load(object sender, EventArgs e)
        {
            wizardId = Request.QueryString["WizardID"];
            if (wizardId == "UserRegistration")
                closedUserGroup = Helper.IsClosedUserGroup();

            PnlWiz.CssClass = SiteConfig.UsePopupWindows ? "wizardPopup" : "wizardWidget";
            PnlWiz.ID = null;

            if (!string.IsNullOrEmpty(Request.QueryString["XCN"]))
                communityId = !Request.QueryString["XCN"].IsGuid() ? DataObjectCommunity.GetCommunityIDByVirtualURL(Request.QueryString["XCN"]) : Request.QueryString["XCN"].ToGuid();
            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                objectId = Request.QueryString["OID"].ToGuid();
            if (!string.IsNullOrEmpty(Request.QueryString["Step"]))
                stepNumber = int.Parse(Request.QueryString["Step"]);

            wizard = WizardSection.CachedInstance.Wizards[wizardId];

            if (wizard.AccessMode == AccessMode.Update)
                _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardEdit);
            else
                if (string.IsNullOrEmpty(Request.QueryString["Flash"]))
                    _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardInsert);

            wizardSteps = wizard.Steps.LINQEnumarable.ToList();

            if (wizardSteps.Count > 0 && stepNumber < wizardSteps.Count &&
                ((wizard.NeedsAuthentication && Request.IsAuthenticated) || !wizard.NeedsAuthentication))
            {
                CommunityUsersType communityUploadUsers = CommunityUsersType.Members;
                bool isOwner = false;
                bool isMember = false;
                if (communityId.HasValue)
                {
                    Guid currentUserId = !string.IsNullOrEmpty(Request.QueryString["UI"]) ? Request.QueryString["UI"].ToGuid() : UserProfile.Current.UserId;

                    DataObjectCommunity community = DataObject.Load<DataObjectCommunity>(communityId, null, true);
                    if (community.State != ObjectState.Added)
                        communityUploadUsers = community.UploadUsers;

                    isOwner = DataAccess.Business.Community.GetIsUserOwner(currentUserId, communityId.Value, out isMember);
                    if (communityUploadUsers == CommunityUsersType.Members && isMember)
                        communityAccessGranted = true;
                    else if (communityUploadUsers == CommunityUsersType.Owners && isOwner)
                        communityAccessGranted = true;
                    else
                        communityAccessGranted = false;
                }

                RenderTabs();
                RenderContent();
            }
        }

        private void RenderTabs()
        {
            TabStrip.Tabs.Clear();
            TabStrip.TabClick += OnTabClick;
            for (int i = 0; i < wizardSteps.Count; i++)
            {
                string localizationBaseFileName = "UserControls.Wizards.WebUI.Base";
                if (!string.IsNullOrEmpty(wizard.LocalizationBaseFileName))
                    localizationBaseFileName = wizard.LocalizationBaseFileName;
                GuiLanguage resMan = GuiLanguage.GetGuiLanguage(localizationBaseFileName);
                RadTab radTab = new RadTab(resMan.GetString(wizardSteps[i].ResourceKey));
                if (wizard.AccessMode == AccessMode.Insert && i != stepNumber)
                {
                    radTab.Enabled = false;
                }
                TabStrip.Tabs.Add(radTab);
            }
            TabStrip.SelectedIndex = stepNumber;
        }

        private void RenderContent()
        {
            if (!wizard.NeedsAuthentication || !communityAccessGranted.HasValue || communityAccessGranted.Value)
            {
                MultiPage.PageViews.Clear();
                for (int i = 0; i < wizardSteps.Count; i++)
                {
                    RadPageView radPageView = new RadPageView();
                    radPageView.ID = "Page" + i;
                    if (i == stepNumber)
                    {
                        StepsASCX wizardControl = (StepsASCX)this.LoadControl(wizardSteps[i].Control);
                        wizardControl.AccessMode = wizard.AccessMode;
                        wizardControl.ObjectType = Helper.GetObjectType(wizard.ObjectType).NumericId;
                        wizardControl.StepNumber = i;
                        wizardControl.WizardId = wizardId;
                        wizardControl.CommunityID = communityId;
                        wizardControl.ObjectID = objectId;
                        wizardControl.Settings = wizardSteps[i].Settings.LINQEnumarable.ToDictionary(x => x.Key, x => x.Value);
                        wizardControl.ID = "Step" + i;
                        wizardControls.Add(i, wizardControl);
                        radPageView.Controls.Add(wizardControl);
                        RenderButtons(radPageView, i);
                        radPageView.Controls.Add(new LiteralControl("<div class=\"clearBoth\"></div>"));
                    }
                    MultiPage.PageViews.Add(radPageView);
                }
            }
            MultiPage.SelectedIndex = stepNumber;
        }

        private void RenderButtons(Control parentControl, int stepNumber)
        {
            HtmlGenericControl block = new HtmlGenericControl("div");
            block.Attributes.Add("class", "inputBlock2");
            parentControl.Controls.Add(block);

            HtmlGenericControl content = new HtmlGenericControl("div");
            content.Attributes.Add("class", "inputBlockContent");
            block.Controls.Add(content);

            List<Control> buttons = new List<Control>();

            if (wizard.AccessMode == AccessMode.Insert)
            {
                if (stepNumber > 0 && wizardSteps[stepNumber].AllowBack)
                {
                    LinkButton backButton = new LinkButton() { ID = "LbtnBack" + stepNumber, CssClass = "inputButtonSecondary", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandPrevious") };
                    backButton.Click += new EventHandler(OnBackClick);
                    buttons.Add(backButton);
                }

                if (stepNumber < wizardSteps.Count - 1 && wizardSteps[stepNumber].AllowNext && (!communityAccessGranted.HasValue || communityAccessGranted.Value))
                {
                    LinkButton nextButton = new LinkButton() { ID = "LbtnNext" + stepNumber, CssClass = "inputButton", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandNext") };
                    nextButton.Click += new EventHandler(OnNextClick);

                    if (wizardSteps[stepNumber].LastSaveStep)
                    {
                        if (wizardId == "UserRegistration")
                            nextButton.Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSignup");
                        else
                            nextButton.Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave");
                    }
                    else
                    {
                        nextButton.Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandNext");
                    }

                    buttons.Insert(0, nextButton);
                }

                if (stepNumber < wizardSteps.Count - 1 && SiteConfig.UsePopupWindows)
                {
                    LinkButton cancelButton = new LinkButton() { ID = "LbtnCancel" + stepNumber, CssClass = "inputButtonSecondary", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandCancel") };
                    cancelButton.Click += new EventHandler(OnCancelClick);
                    cancelButton.CausesValidation = false;
                    buttons.Add(cancelButton);
                }

                if (stepNumber == wizardSteps.Count - 1 && SiteConfig.UsePopupWindows && wizard.AccessMode == AccessMode.Insert && !wizardSteps[stepNumber].LastSaveStep)
                {
                    HyperLink closeLink = new HyperLink() { ID = "LnkClose" + stepNumber, CssClass = "inputButton", NavigateUrl = "javascript:void(0)", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandClose") };
                    buttons.Insert(0, closeLink);

                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        closeLink.Attributes.Add("OnClick", string.Format("RedirectParentPage('{0}');GetRadWindow().Close();", System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnUrl"]))));
                    else if (Request.QueryString["Refresh"] != null && Request.QueryString["Refresh"] == "false")
                        closeLink.Attributes.Add("OnClick", "GetRadWindow().Close();");
                    else if (Request.QueryString["Callback"] != null)
                        closeLink.Attributes.Add("OnClick", string.Format("CallFunctionOnRadWindow('{0}', '{1}');CloseWindow();", Request.QueryString["CallbackWindow"], Request.QueryString["Callback"]));
                    else
                        closeLink.Attributes.Add("OnClick", "RefreshParentPage();GetRadWindow().Close();");
                }

                if (stepNumber == wizardSteps.Count - 1 && SiteConfig.UsePopupWindows && wizard.AccessMode == AccessMode.Insert && wizardSteps[stepNumber].LastSaveStep)
                {
                    LinkButton saveAndCloseButton = new LinkButton() { ID = "LbtnSaveAndClose" + stepNumber, CssClass = "inputButton", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave") };
                    saveAndCloseButton.Click += new EventHandler(OnSaveAndCloseClick);
                    buttons.Insert(0, saveAndCloseButton);
                }
            }
            else
            {
                if (SiteConfig.UsePopupWindows)
                {
                    LinkButton saveAndCloseButton = new LinkButton() { ID = "LbtnSaveAndClose" + stepNumber, CssClass = "inputButton", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave") };
                    saveAndCloseButton.Click += new EventHandler(OnSaveAndCloseClick);
                    buttons.Insert(0, saveAndCloseButton);

                    LinkButton cancelButton = new LinkButton() { ID = "LbtnCancel" + stepNumber, CssClass = "inputButtonSecondary", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandCancel") };
                    cancelButton.Click += new EventHandler(OnCancelClick);
                    cancelButton.CausesValidation = false;
                    buttons.Add(cancelButton);
                }
                else
                {
                    LinkButton saveButton = new LinkButton() { ID = "LbtnSave" + stepNumber, CssClass = "inputButton", Text = GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave") };
                    saveButton.Click += new EventHandler(OnSaveAndCloseClick);
                    buttons.Insert(0, saveButton);
                }
            }

            if (!closedUserGroup)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    content.Controls.Add(buttons[i]);
                }
            }
        }

        private void OnTabClick(object sender, RadTabStripEventArgs e)
        {
            GoToStep(TabStrip.Tabs.IndexOf(e.Tab));
        }

        private void GoToStep(int newStepNumber)
        {
            NameValueCollection stepQueryStringList = new NameValueCollection();
            if (wizardControls[stepNumber].SaveStep(ref stepQueryStringList))
            {
                List<string> exludeQueryKeys = new List<string>();
                exludeQueryKeys.Add("Step");
                exludeQueryKeys.AddRange(stepQueryStringList.AllKeys);

                string stepQueryString = string.Empty;
                foreach (var queryKey in stepQueryStringList.AllKeys)
                    stepQueryString += string.Format("&{0}={1}", queryKey, stepQueryStringList.Get(queryKey));

                Response.Redirect(string.Format("{0}?{1}{2}&Step={3}", Request.Url.LocalPath, Helper.GetFilteredQueryString(Request.QueryString, exludeQueryKeys, true), stepQueryString, newStepNumber));
            }
        }

        protected void OnBackClick(object sender, EventArgs e)
        {
            GoToStep(stepNumber - 1);
        }

        protected void OnNextClick(object sender, EventArgs e)
        {
            GoToStep(stepNumber + 1);
        }

        protected void OnSaveAndCloseClick(object sender, EventArgs e)
        {
            NameValueCollection stepQueryStringList = new NameValueCollection();
            if (wizardControls[stepNumber].SaveStep(ref stepQueryStringList))
            {
                if (SiteConfig.UsePopupWindows)
                {
                    if (Request.QueryString["Refresh"] != null && Request.QueryString["Refresh"] == "false")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeWindow", "$telerik.$(function() { GetRadWindow().Close(); } );", true);
                    else if (Request.QueryString["Callback"] != null)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeWindow", string.Format("$telerik.$(function() {{ CloseWindow();CallFunctionOnRadWindow('{0}', '{1}'); }} );", Request.QueryString["CallbackWindow"], Request.QueryString["Callback"]), true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeWindow", "$telerik.$(function() { RefreshParentPage();GetRadWindow().Close(); } );", true);
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    Response.Redirect(System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnUrl"])));
            }
        }

        protected void OnCancelClick(object sender, EventArgs e)
        {
            if (wizard.AccessMode == AccessMode.Insert)
            {
                if (objectId.HasValue)
                {
                    DataObject dataObject = DataObject.Load<DataObject>(objectId, null, true);
                    if (dataObject.State != ObjectState.Added)
                        dataObject.Delete();
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["UploadSession"]))
                {
                    DataObjectList<DataObject> dataObjectsByUploadSession = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(), ObjectType = Helper.GetObjectType(wizard.ObjectType).NumericId, SortBy = QuickSort.InsertedDate, Direction = QuickSortDirection.Asc, GroupID = Request.QueryString["UploadSession"].ToGuid(), DisablePaging = true, IgnoreCache = true, QuerySourceType = QuerySourceType.MyContent });
                    foreach (var dataObject in dataObjectsByUploadSession)
                    {
                        dataObject.Delete();
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeWindow", "$telerik.$(function() { GetRadWindow().Close(); } );", true);
        }
    }
}