// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class WidgetContainer : System.Web.UI.UserControl, IWidgetHost
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");
        private const string langCode = "de-CH";
        private WidgetElement widgetConfig;

        public event Action<hitbl_WidgetInstance_IN> OnWidgetDeleted;

        public bool IsReadOnly { get; set; }
        public int ColumnWidth { get; set; }
        public int ContentPadding { get; set; }
        public Guid ParentCommunityID { get; set; }
        public int ParentObjectType { get; set; }
        public PageType ParentPageType { get; set; }
        public OutputTemplateElement OutputTemplate { get; set; }
        public hitbl_WidgetInstance_IN WidgetInstance { get; set; }
        public SiteContext SiteContext { get; set; }
        public Guid TagWord { get; set; }
        public string LangCode { get; set; }
        public Guid InstanceID { set; get; }
        public string ClassPrefix { set; get; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                bool showWidget = true;
                if (!string.IsNullOrEmpty(WidgetInstance.INS_ViewRoles) && !UserDataContext.GetUserDataContext().IsAdmin)
                {
                    var userRoles = UserDataContext.GetUserDataContext().UserRoles.ToList().ConvertAll(x => x.ToLower());
                    showWidget = Array.Exists(WidgetInstance.INS_ViewRoles.Split(Constants.TAG_DELIMITER), x => userRoles.Contains(x.ToLower()));
                }
                if (showWidget)
                {
                    widgetConfig = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => w.Id == WidgetInstance.WDG_ID).Single();
                    Control widget = LoadControl(widgetConfig.Control);
                    ((WidgetBase)widget).WidgetHost = this;

                    if (!IsReadOnly)
                        widget.ID = "Widget" + WidgetInstance.INS_ID.ToString().Replace("-", "_");
                    else
                        widget.ID = "W" + WidgetInstance.INS_ColumnNo + WidgetInstance.INS_OrderNo;

                    WBP.Controls.Add(widget);

                    // Add drag & drop behaviour
                    if (!IsReadOnly && !WidgetInstance.INS_IsFixed || UserDataContext.GetUserDataContext().IsAdmin)
                    {
                        CustomDragDrop.CustomFloatingBehaviorExtender customFloatingBehaviorExtender = new CustomDragDrop.CustomFloatingBehaviorExtender();
                        customFloatingBehaviorExtender.ID = "WidgetFloatingBehavior";
                        customFloatingBehaviorExtender.DragHandleID = "WidgetHeader";
                        customFloatingBehaviorExtender.TargetControlID = "W";
                        WBP.Controls.Add(customFloatingBehaviorExtender);
                    }
                }
                else
                {
                    Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteEntry(ex);
                WBP.Controls.Add(new LiteralControl(ex.Message));
            }

            WBP.ID = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set output template
            if (WidgetInstance.INS_OutputTemplate.HasValue)
                OutputTemplate = OutputTemplatesSection.CachedInstance.Templates[WidgetInstance.INS_OutputTemplate.Value];

            // Set widget title
            var widgetText = WidgetInstance.hitbl_WidgetInstanceText_WITs.Where(x => x.WIT_LangCode.ToLower() == langCode.ToLower()).ToList();
            string widgetTitle = string.Empty;
            if (widgetText.Count == 1)
            {
                widgetTitle = widgetText[0].WIT_Title;
            }
            SetWidgetTitle(widgetTitle);

            // Set style name
            if (WidgetInstance.WTP_ID.HasValue)
            {
                CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var widgetTemplate = wdc.hitbl_WidgetTemplates_WTPs.Where(x => x.WTP_ID == WidgetInstance.WTP_ID.Value).SingleOrDefault();
                if (widgetTemplate != null)
                {
                    ClassPrefix = widgetTemplate.WTP_Name;
                    ContentPadding = widgetTemplate.WTP_ContentPadding;
                }
            }

            // Add instanceId for widget services
            if (!IsReadOnly)
                W.Attributes.Add("InstanceId", WidgetInstance.INS_ID.ToString());

            // Set edit and close buttons
            if ((!WidgetInstance.INS_IsFixed && !IsReadOnly) || UserDataContext.GetUserDataContext().IsAdmin)
            {
                GuiLanguage widgetResources;
                string title;
                if (widgetConfig != null && !string.IsNullOrEmpty(widgetConfig.LocalizationBaseFileName))
                {
                    widgetResources = GuiLanguage.GetGuiLanguage(widgetConfig.LocalizationBaseFileName);
                    title = widgetResources.GetString(widgetConfig.TitleKey);
                }
                else
                {
                    title = language.GetString("LableWidgetProperties");
                }

                string queryString = string.Empty;
                if (PageInfo.CommunityId.HasValue) queryString = string.Format("ParentId={0}", PageInfo.CommunityId);
                else if (PageInfo.UserId.HasValue) queryString = string.Format("ParentId={0}", PageInfo.UserId);

                if (WidgetInstance.INS_ShowAfterInsert != (int)WidgetShowAfterInsert.Nothing)
                {
                    CSBooster_DataContext wdc = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                    var widgetInstance = wdc.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == WidgetInstance.INS_ID).SingleOrDefault();

                    if (WidgetInstance.INS_ShowAfterInsert == (int)WidgetShowAfterInsert.Settings)
                    {
                        Rw.Visible = true;
                        Rw.Title = title;
                        Rw.ID = "WidgetSettings";
                        Rw.NavigateUrl = string.Format("/Pages/Popups/WidgetSettings.aspx?InstanceId={0}&{1}", WidgetInstance.INS_ID, queryString);
                    }
                    else if (WidgetInstance.INS_ShowAfterInsert == (int)WidgetShowAfterInsert.CreateWizard)
                    {
                        XDocument settings = XDocument.Load(new StringReader(widgetConfig.Settings.Value));
                        if (settings.Root.Element("ObjectType") != null)
                        {
                            string objectType = settings.Root.Element("ObjectType").Value;
                            var type = Helper.GetObjectType(objectType);
                            Guid communityId = ParentObjectType == Helper.GetObjectTypeNumericID("Community") ? PageInfo.EffectiveCommunityId.Value : UserProfile.Current.ProfileCommunityID;
                            Guid objectId = Guid.NewGuid();
                            string initQuerySegment = string.Format("&OID={0}&XCN={1}", objectId, communityId);
                            string returnUrlQuerySegment = "&ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));
                            string createWizardUrl = string.Format("{0}{1}{2}", Helper.GetUploadWizardLink(type.NumericId, _4screen.CSB.Common.SiteConfig.UsePopupWindows), initQuerySegment, returnUrlQuerySegment);
                            string createWizardTitle = string.IsNullOrEmpty(type.LocalizationBaseFileName) ? GuiLanguage.GetGuiLanguage("SiteObjects").GetString(type.NameCreateMenuKey) : GuiLanguage.GetGuiLanguage(type.LocalizationBaseFileName).GetString(type.NameCreateMenuKey);

                            if (_4screen.CSB.Common.SiteConfig.UsePopupWindows)
                            {
                                Rw.Visible = true;
                                Rw.Title = createWizardTitle;
                                Rw.ID = "wizardWin";
                                Rw.NavigateUrl = createWizardUrl;
                                widgetInstance.INS_XmlStateData = string.Format("<root><ObjectType>{0}</ObjectType><ByUrl>False</ByUrl><Source>-1</Source><ObjectID>{1}</ObjectID></root>", type.Id, objectId);
                            }
                        }
                    }
                    widgetInstance.INS_ShowAfterInsert = 0;
                    wdc.SubmitChanges();
                }

                if (widgetConfig != null)
                {
                    WEdt.NavigateUrl = string.Format("javascript:radWinOpen('/Pages/Popups/WidgetSettings.aspx?InstanceId={0}&{1}', '{2}', 740, 580, true, null, 'WidgetSettings');", WidgetInstance.INS_ID, queryString, title);
                }

                WEdt.Visible = true;
                WCl.Visible = true;
                LitTitle.Visible = true;

                WEdt.ID = null;
                pnlOwner.ID = null;
            }
            else
            {
                pnlOwner.Visible = false;
                WidgetHeader.CssClass = "widgetHeaderNoMove";
                LitTitle.Visible = true;
            }
        }

        public void SetWidgetTitle(string title)
        {
            if (WidgetInstance.INS_HeadingTag != null && !string.IsNullOrEmpty(title) && WidgetInstance.INS_HeadingTag > 0)
            {
                title = string.Format("<h{0}>{1}</h{0}>", WidgetInstance.INS_HeadingTag, title);
            }
            LitTitle.Text = title;
        }

        protected void OnDeleteClick(object sender, EventArgs e)
        {
            OnWidgetDeleted(WidgetInstance);
        }
    }
}