// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class SettingsWidgets : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");
        private UserDataContext userDataContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("SettingsWidgets");

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            userDataContext = UserDataContext.GetUserDataContext();

            if (!IsPostBack)
            {
                WidgetSection widgets = WidgetSection.CachedInstance;
                this.RepWidgets.DataSource = widgets.Widgets.Cast<WidgetElement>().OrderBy(x => x.GroupId).ThenBy(x => x.OrderKey);
                this.RepWidgets.DataBind();
            }
        }

        protected void RepWidgetsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                WidgetElement widget = (WidgetElement)e.Item.DataItem;

                e.Item.ID = widget.Id.ToString();

                Literal name = (Literal)e.Item.FindControl("LitName");
                name.Text = GuiLanguage.GetGuiLanguage("WidgetsBase").GetString(widget.TitleKey);

                TextBox groupId = (TextBox)e.Item.FindControl("TxtGroupId");
                groupId.Text = widget.GroupId.ToString();

                TextBox orderKey = (TextBox)e.Item.FindControl("TxtOrderKey");
                orderKey.Text = widget.OrderKey.ToString();

                TextBox roles = (TextBox)e.Item.FindControl("TxtRoles");
                roles.Text = widget.Roles;
                if (string.IsNullOrEmpty(roles.Text))
                    ((HtmlTableRow)e.Item.FindControl("WidgetRow")).Attributes.Add("style", "background-color:#FFEEEE;");
                else
                    ((HtmlTableRow)e.Item.FindControl("WidgetRow")).Attributes.Add("style", "background-color:#EEFFEE;");

                TextBox communities = (TextBox)e.Item.FindControl("TxtCommunities");
                communities.Text = widget.Communities;

                TextBox pageTypes = (TextBox)e.Item.FindControl("TxtPageTypes");
                pageTypes.Text = widget.PageTypes;

                TextBox settings = (TextBox)e.Item.FindControl("TxtSettings");
                settings.Text = widget.Settings.Value;

                if (userDataContext.UserID != Constants.ADMIN_USERID.ToGuid())
                {
                    communities.ReadOnly = true;
                    communities.CssClass = "CSB_admin_readonly";
                    pageTypes.ReadOnly = true;
                    pageTypes.CssClass = "CSB_admin_readonly";
                    settings.ReadOnly = true;
                    settings.CssClass = "CSB_admin_readonly";
                }
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            WidgetSection widgets = WidgetSection.CachedInstance;

            try
            {
                foreach (WidgetElement widget in widgets.Widgets)
                {
                    widget.GroupId = int.Parse(Request.Form[string.Format("{0}${1}$TxtGroupId", this.RepWidgets.UniqueID, widget.Id)]);
                    widget.OrderKey = int.Parse(Request.Form[string.Format("{0}${1}$TxtOrderKey", this.RepWidgets.UniqueID, widget.Id)]);
                    widget.Roles = Request.Form[string.Format("{0}${1}$TxtRoles", this.RepWidgets.UniqueID, widget.Id)];
                    widget.Communities = Request.Form[string.Format("{0}${1}$TxtCommunities", this.RepWidgets.UniqueID, widget.Id)];
                    widget.PageTypes = Request.Form[string.Format("{0}${1}$TxtPageTypes", this.RepWidgets.UniqueID, widget.Id)];
                    widget.Settings.Value = Request.Form[string.Format("{0}${1}$TxtSettings", this.RepWidgets.UniqueID, widget.Id)];
                }

                Helper.SaveSectionToFile(string.Format(@"{0}\Configurations\Widgets.config", WebRootPath.Instance.ToString()), "widgetSection", widgets);
            }
            catch (Exception ex)
            {
                this.PnlMsg.Visible = true;
                this.LitMsg.Text = language.GetString("MessageSaveError") + "<br/>" + ex.Message;
            }

            this.RepWidgets.DataSource = widgets.Widgets.Cast<WidgetElement>().OrderBy(x => x.GroupId).ThenBy(x => x.OrderKey);
            this.RepWidgets.DataBind();
        }
    }
}
