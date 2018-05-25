//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#2.0.0.0		27.03.2008 / AW
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using _4screen.WebControls;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class CustomizationBarTabWidgets : UserControl
    {
        private List<WidgetElement> widgets;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base");

        public Guid CommunityID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObject profileOrCommunity = DataObject.Load<DataObject>(CommunityID);
            string currentPageType = null;
            if (profileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                currentPageType = "PROFILE";
            else
                currentPageType = Helper.GetObjectType(profileOrCommunity.ObjectType).Id.ToUpper();
            string currentRole = UserDataContext.GetUserDataContext().UserRole.ToUpper();

            widgets = WidgetSection.CachedInstance.Widgets.Cast<WidgetElement>().Where(w => (w.Roles == "*" || w.Roles.ToUpper().Contains(currentRole)) &&
                                                                                      (w.Communities == "*" || w.Communities.ToUpper().Contains(CommunityID.ToString().ToUpper())) &&
                                                                                      (w.PageTypes == "*" || w.PageTypes.ToUpper().Contains(currentPageType))).OrderBy(w => w.OrderKey).ToList();

            LoadData();

            ShowDialog();
        }

        private void LoadData()
        {
            var groupIds = (from widget in widgets select widget.GroupId).Distinct();

            CblWidgets.Items.Clear();
            foreach (int groupId in groupIds)
            {
                CblWidgets.Items.Add(new ListItem() { Text = (new TextControl() { LanguageFile = "UserControls.WebUI.Base", TextKey = "LableWidgetGroup" + groupId }).Text, Value = groupId.ToString(), Selected = true });
            }

            WG.DataSource = widgets;
            WG.DataBind();
        }

        private void ShowDialog()
        {
            List<string> pageNames = new List<string>();
            pageNames.Add("ProfileEditWidget");
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
                ScriptManager.RegisterStartupScript((Control)this, this.GetType(), "DialogWin", "SetPopupWindow('" + this.ClientID + string.Format("', 700, 0, 200, '{0}', '", language.GetString("LableWidgetMessages").StripForScript()) + content + "', true);", true);
            }
        }

        protected void OnWidgetItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            WidgetElement widget = (WidgetElement)e.Item.DataItem;

            Panel widgetTemplate = (Panel)e.Item.FindControl("WT");
            widgetTemplate.Attributes.Add("InstanceId", widget.Id.ToString());
            widgetTemplate.Attributes.Add("IsTemplate", "true");

            ((Literal)e.Item.FindControl("LitTitle")).Text = (new TextControl() { LanguageFile = widget.LocalizationBaseFileName, TextKey = widget.TitleKey }).Text;
            ((Literal)e.Item.FindControl("LitDesc")).Text = (new TextControl() { LanguageFile = widget.LocalizationBaseFileName, TextKey = widget.DescriptionKey }).Text;
        }

        protected void OnWidgetFilterChanged(object sender, EventArgs e)
        {
            List<int> groupIds = new List<int>();
            foreach (ListItem item in ((CheckBoxList)sender).Items)
            {
                if (item.Selected)
                    groupIds.Add(int.Parse(item.Value));
            }
            WG.DataSource = widgets.FindAll(x => groupIds.Contains(x.GroupId));
            WG.DataBind();
        }

        protected void OnCloseClick(object sender, EventArgs e)
        {
            string filteredQueryString = Helper.GetFilteredQueryString(Request.QueryString, new List<string> { "edit", "tab" }, true);
            Response.Redirect(string.Format("{0}?{1}", Request.GetRawPath(), filteredQueryString));
        }
    }
}