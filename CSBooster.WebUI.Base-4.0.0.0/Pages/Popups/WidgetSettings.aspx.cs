using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using _4screen.CSB.DataAccess.Data;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class WidgetSettings : System.Web.UI.Page
    {
        private List<IWidgetSettings> widgetSettings = new List<IWidgetSettings>();
        private IWidgetSettings titleSettings;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "jquery", string.Format("<script src=\"/library/scripts/jquery.js\" type=\"text/javascript\" ></script>"), false);

            Guid instanceId = Request.QueryString["InstanceId"].ToGuid();
            Guid parentObjectId = Request.QueryString["ParentId"].ToGuid();
            DataObject parentObject = DataObject.Load<DataObject>(parentObjectId);

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == instanceId) select widgInstances).FirstOrDefault();
            var widget = WidgetSection.CachedInstance.Widgets.LINQEnumarable.ToList().Find(x => x.Id == widgetInstance.WDG_ID);

            this.TabStrip.Tabs.Clear();
            this.MultiPage.PageViews.Clear();

            Control titleControl = LoadControl("~/UserControls/WidgetTitleSettings.ascx");
            titleSettings = (IWidgetSettings)titleControl;
            titleSettings.InstanceId = instanceId;
            titleSettings.ParentDataObject = parentObject;
            widgetSettings.Add(titleSettings);

            HtmlGenericControl div;

            if (widget.Steps.Count == 0)
            {
                this.TabStrip.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base").GetString("TitleWidgetProperties")));
                div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "");
                div.Controls.Add(titleControl);
                div.Controls.Add(new LiteralControl("<div class=\"clearBoth\"></div>"));
                RadPageView page = new RadPageView();
                page.Controls.Add(div);
                this.MultiPage.PageViews.Add(page);
            }
            else
            {
                for (int i = 0; i < widget.Steps.Count; i++)
                {
                    this.TabStrip.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage(widget.LocalizationBaseFileName).GetString(widget.Steps[i].TitleKey)));
                    div = new HtmlGenericControl("div");
                    div.Attributes.Add("class", "");
                    Control control = LoadControl(widget.Steps[i].Control);
                    widgetSettings.Add((IWidgetSettings)control);
                    ((IWidgetSettings)control).InstanceId = instanceId;
                    ((IWidgetSettings)control).ParentDataObject = parentObject;
                    if (i == 0)
                    {
                        div.Controls.Add(titleControl);
                        div.Controls.Add(new LiteralControl("<div class=\"CSB_input_separator\"></div>"));
                    }
                    div.Controls.Add(control);
                    div.Controls.Add(new LiteralControl("<div class=\"clearBoth\"></div>"));
                    RadPageView page = new RadPageView();
                    page.Controls.Add(div);
                    this.MultiPage.PageViews.Add(page);
                }
            }

            if (!string.IsNullOrEmpty(widget.OutputTemplates) && widget.OutputTemplates.Split(';').Length > 1)
            {
                this.TabStrip.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleWidgetOutput")));
                div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "");
                Control widgetSettingsControl = LoadControl("~/UserControls/OutputTemplates.ascx");
                widgetSettings.Add((IWidgetSettings)widgetSettingsControl);
                div.Controls.Add(widgetSettingsControl);
                ((IWidgetSettings)widgetSettingsControl).InstanceId = instanceId;
                ((IWidgetSettings)widgetSettingsControl).ParentDataObject = parentObject;
                RadPageView settingsPage = new RadPageView();
                settingsPage.Controls.Add(div);
                this.MultiPage.PageViews.Add(settingsPage);
            }

            if (UserDataContext.GetUserDataContext().IsAdmin)
            {
                this.TabStrip.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleWidgetSettings")));
                div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "");
                Control widgetSettingsControl = LoadControl("~/UserControls/WidgetSettings.ascx");
                widgetSettings.Add((IWidgetSettings)widgetSettingsControl);
                div.Controls.Add(widgetSettingsControl);
                ((IWidgetSettings)widgetSettingsControl).InstanceId = instanceId;
                ((IWidgetSettings)widgetSettingsControl).ParentDataObject = parentObject;
                RadPageView settingsPage = new RadPageView();
                settingsPage.Controls.Add(div);
                this.MultiPage.PageViews.Add(settingsPage);
            }

            this.TabStrip.Tabs.Add(new RadTab(GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitleWidgetDesign")));
            div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "");
            Control designControl = LoadControl("~/UserControls/StyleSettingsWidget.ascx");
            widgetSettings.Add((IWidgetSettings)designControl);
            div.Controls.Add(designControl);
            ((IWidgetSettings)designControl).InstanceId = instanceId;
            ((IWidgetSettings)designControl).ParentDataObject = parentObject;
            RadPageView designPage = new RadPageView();
            designPage.Controls.Add(div);
            this.MultiPage.PageViews.Add(designPage);

            this.TabStrip.SelectedIndex = 0;
            this.MultiPage.SelectedIndex = 0;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            bool saveError = false;

            foreach (var singleWidgetSettings in widgetSettings)
            {
                if (singleWidgetSettings.Save() == false)
                {
                    saveError = true;
                    continue;
                }
            }

            if (saveError)
            {
                this.LitResult.Text = "<div>Fehler beim Speichern</div>";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", "$telerik.$(function() { RefreshParentPage();CloseWindow(); } );", true);
            }
        }
    }
}
