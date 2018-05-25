using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;
using _4screen.CSB.DataAccess.Data;
using System.Configuration;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class StyleSettingsWidget : System.Web.UI.UserControl, IWidgetSettings
    {
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }
        public string StylesId { get; set; }
        private List<hitbl_WidgetTemplates_WTP> widgetTemplates;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                this.Rpb.Items[0].Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitlePanelHeader");
                this.Rpb.Items[1].Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitlePanelContent");
                this.Rpb.Items[2].Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitlePanelFooter");
                this.Rpb.Items[3].Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("TitlePanelEditStyle");
            }
            catch { }

            var panelItems = this.Rpb.GetAllItems();
            TextBox customStyle = (TextBox)panelItems.ToList().Find(x => x.Value == "CustomStyle").FindControl("TxtStyle");
            StylesId = customStyle.ClientID;

            LoadTemplates();

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from instances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select instances).FirstOrDefault();

            if (!IsPostBack)
            {
                if (widgetInstance.WTP_ID.HasValue)
                    LoadTemplate(widgetTemplates.Find(x => x.WTP_ID == widgetInstance.WTP_ID.Value));
                else
                    LoadTemplate(widgetTemplates.Find(x => x.WTP_ID == Constants.DEFAULT_LAYOUTID.ToGuid()));
            }
        }

        private void LoadTemplates()
        {
            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            widgetTemplates = (from templates in dataContext.hitbl_WidgetTemplates_WTPs select templates).ToList();

            List<hitbl_WidgetTemplates_WTP> publicWidgetTemplates = new List<hitbl_WidgetTemplates_WTP>();
            if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page") || ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("User"))
                publicWidgetTemplates = (from templates in widgetTemplates.Where(x => (x.UserID == null || x.UserID == UserProfile.Current.UserId) && x.WTP_ExplicitInserted) select templates).OrderBy(x => x.WTP_Name).ToList();
            else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                publicWidgetTemplates = (from templates in widgetTemplates.Where(x => (x.UserID == null || x.UserID == ParentDataObject.ObjectID) && x.WTP_ExplicitInserted) select templates).OrderBy(x => x.WTP_Name).ToList();
            RcbTemplates.Items.Clear();
            foreach (var widgetTemplate in publicWidgetTemplates)
                RcbTemplates.Items.Add(new RadComboBoxItem(widgetTemplate.WTP_Name, widgetTemplate.WTP_ID.ToString()));
            RcbTemplates.Items.Insert(0, new RadComboBoxItem("", "Custom"));

            List<hitbl_WidgetTemplates_WTP> privateWidgetTemplates = publicWidgetTemplates;
            privateWidgetTemplates.RemoveAll(x => x.WTP_ID == Constants.DEFAULT_LAYOUTID.ToGuid());
            if (!UserDataContext.GetUserDataContext().IsAdmin)
                privateWidgetTemplates.RemoveAll(x => x.UserID == null);
            RcbTemplates2.Items.Clear();
            foreach (var widgetTemplate in privateWidgetTemplates)
                RcbTemplates2.Items.Add(new RadComboBoxItem(widgetTemplate.WTP_Name, widgetTemplate.WTP_ID.ToString()));
        }

        private void LoadTemplate(hitbl_WidgetTemplates_WTP template)
        {
            RcbTemplates.SelectedValue = template.WTP_ID.ToString();

            string styleRules = ".widget .top { } .widget .cnt { } .widget .cnt a { } .widget .bottom { }";

            if (!string.IsNullOrEmpty(template.WTP_XMLTemplate))
            {
                _4screen.CSB.DataAccess.Business.StyleSettingsWidget styleSettingsWidget = DataAccess.Business.StyleSettingsWidget.ParseXml(template.WTP_XMLTemplate);

                var panelItems = this.Rpb.GetAllItems();
                StyleSettings headerStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Header").FindControl("SP");
                headerStyle.SetStyleSettings(styleSettingsWidget.Header);

                StyleSettings contentStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Content").FindControl("SP");
                contentStyle.SetStyleSettings(styleSettingsWidget.Content);

                StyleSettings footerStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Footer").FindControl("SP");
                footerStyle.SetStyleSettings(styleSettingsWidget.Footer);

                TextBox customStyle = (TextBox)panelItems.ToList().Find(x => x.Value == "CustomStyle").FindControl("TxtStyle");
                customStyle.Text = styleSettingsWidget.CustomStyle;

                styleRules = styleSettingsWidget.CustomStyle;
            }

            this.LitStyles.Text = "<style type=\"text/css\" title=\"styles\">" + styleRules + "</style>";
        }

        private _4screen.CSB.DataAccess.Business.StyleSettingsWidget GetStyleSettings()
        {
            _4screen.CSB.DataAccess.Business.StyleSettingsWidget styleSettingsWidget = new DataAccess.Business.StyleSettingsWidget();

            var panelItems = this.Rpb.GetAllItems();
            StyleSettings headerStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Header").FindControl("SP");
            styleSettingsWidget.Header = headerStyle.GetStyleSettings();

            StyleSettings contentStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Content").FindControl("SP");
            styleSettingsWidget.Content = contentStyle.GetStyleSettings();

            StyleSettings footerStyle = (StyleSettings)panelItems.ToList().Find(x => x.Value == "Footer").FindControl("SP");
            styleSettingsWidget.Footer = footerStyle.GetStyleSettings();

            TextBox customStyle = (TextBox)panelItems.ToList().Find(x => x.Value == "CustomStyle").FindControl("TxtStyle");
            styleSettingsWidget.CustomStyle = customStyle.Text;

            return styleSettingsWidget;
        }

        public bool Save()
        {
            _4screen.CSB.DataAccess.Business.StyleSettingsWidget styleSettingsWidget = GetStyleSettings();

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from instances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select instances).FirstOrDefault();

            if (RcbTemplates.SelectedValue != Constants.DEFAULT_LAYOUTID)
            {
                dataContext.hisp_WidgetTemplates_ReduceCount(widgetInstance.INS_PAG_ID, widgetInstance.WTP_ID);

                var widgetTemplate = (from templates in dataContext.hitbl_WidgetTemplates_WTPs.Where(x => x.WTP_ID == widgetInstance.WTP_ID) select templates).FirstOrDefault();

                if (RcbTemplates.SelectedValue != "Custom")
                {
                    if (widgetTemplate != null && !widgetTemplate.WTP_ExplicitInserted)
                        dataContext.hitbl_WidgetTemplates_WTPs.DeleteOnSubmit(widgetTemplate);
                    widgetInstance.WTP_ID = RcbTemplates.SelectedValue.ToGuid();
                }
                else
                {
                    if (widgetTemplate == null || widgetTemplate.WTP_ExplicitInserted)
                    {
                        widgetTemplate = new hitbl_WidgetTemplates_WTP();
                        widgetTemplate.WTP_ID = Guid.NewGuid();
                        widgetTemplate.UserID = UserProfile.Current.UserId;
                        widgetTemplate.WTP_Name = GetRandomName();
                        widgetTemplate.WTP_ExplicitInserted = false;
                        widgetTemplate.WTP_Template = styleSettingsWidget.CustomStyle.Replace("widget", "widget_" + widgetTemplate.WTP_Name);
                        widgetTemplate.WTP_XMLTemplate = styleSettingsWidget.GetXml();
                        dataContext.hitbl_WidgetTemplates_WTPs.InsertOnSubmit(widgetTemplate);

                        widgetInstance.WTP_ID = widgetTemplate.WTP_ID;
                    }
                    else
                    {
                        widgetTemplate.WTP_Template = styleSettingsWidget.CustomStyle.Replace("widget", "widget_" + widgetTemplate.WTP_Name);
                        widgetTemplate.WTP_XMLTemplate = styleSettingsWidget.GetXml();
                    }
                }
                dataContext.SubmitChanges();

                dataContext.hisp_WidgetTemplates_IncreaseCount(widgetInstance.INS_PAG_ID, widgetInstance.WTP_ID);
            }
            else
            {
                widgetInstance.WTP_ID = Constants.DEFAULT_LAYOUTID.ToGuid();
                dataContext.SubmitChanges();
            }

            return true;
        }

        public void OnSelectedTemplateChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Guid? templateId = e.Value.ToNullableGuid();
            if (templateId.HasValue)
                LoadTemplate(widgetTemplates.Find(x => x.WTP_ID == templateId));
        }

        protected void OnTemplateSaveClick(object sender, EventArgs e)
        {
            string templateName = RcbTemplates2.Text;
            _4screen.CSB.DataAccess.Business.StyleSettingsWidget styleSettingsWidget = GetStyleSettings();

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetTemplate = (from templates in dataContext.hitbl_WidgetTemplates_WTPs.Where(x => x.WTP_Name.ToLower() == templateName.ToLower()) select templates).FirstOrDefault();
            if (widgetTemplate == null)
            {
                widgetTemplate = new hitbl_WidgetTemplates_WTP();
                widgetTemplate.WTP_ID = Guid.NewGuid();
                if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page") || ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                    widgetTemplate.UserID = UserProfile.Current.UserId;
                else if (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community"))
                    widgetTemplate.UserID = ParentDataObject.ObjectID;
                widgetTemplate.WTP_Name = templateName;
                widgetTemplate.WTP_ExplicitInserted = true;
                widgetTemplate.WTP_Template = styleSettingsWidget.CustomStyle.Replace("widget", "widget_" + widgetTemplate.WTP_Name);
                widgetTemplate.WTP_XMLTemplate = styleSettingsWidget.GetXml();
                dataContext.hitbl_WidgetTemplates_WTPs.InsertOnSubmit(widgetTemplate);
                dataContext.SubmitChanges();
                //this.LitStatus.Text = string.Format("<div class=\"CSB_wiz_msg\">{0}</div>", GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("MessageStyleSaved"));
                LoadTemplates();
                LoadTemplate(widgetTemplate);
            }
            else
            {
                if (((ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Page") || ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity")) && widgetTemplate.UserID == UserProfile.Current.UserId) ||
                     (ParentDataObject.ObjectType == Helper.GetObjectTypeNumericID("Community")) && widgetTemplate.UserID == ParentDataObject.ObjectID ||
                     UserDataContext.GetUserDataContext().IsAdmin)
                {
                    widgetTemplate.WTP_Template = styleSettingsWidget.CustomStyle.Replace("widget", "widget_" + widgetTemplate.WTP_Name);
                    widgetTemplate.WTP_XMLTemplate = styleSettingsWidget.GetXml();
                    dataContext.SubmitChanges();
                    //this.LitStatus.Text = string.Format("<div class=\"CSB_wiz_msg\">{0}</div>", GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("MessageStyleSaved"));
                    LoadTemplates();
                    LoadTemplate(widgetTemplate);
                }
                else
                {
                    //this.LitStatus.Text = string.Format("<div class=\"CSB_wiz_msg\">{0}</div>", GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("ErrorMessageStyleNotSaved"));
                }
            }
        }

        private static string GetRandomName()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                char value = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                sb.Append(value);
            }
            return sb.ToString().ToLower();
        }
    }
}