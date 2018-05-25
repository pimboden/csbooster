// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.DataAccess.Data;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class WidgetSettings : System.Web.UI.UserControl, IWidgetSettings
    {
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select widgInstances).FirstOrDefault();

            foreach(ListItem item in RblHeadingTag.Items)
            {
                if (string.IsNullOrEmpty(item.Value))
                {
                    item.Text = GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("LabelWidgetHeadingNo");
                    break;
                }
            }
            if (widgetInstance.INS_HeadingTag.HasValue)
                this.RblHeadingTag.SelectedValue = widgetInstance.INS_HeadingTag.ToString();

            string currentRole = widgetInstance.INS_ViewRoles;

            RcbVisRoles.Items.Clear();
            RcbVisRoles.AllowCustomText = true;
            RcbVisRoles.Text = string.Empty;
            RcbVisRoles.Attributes.Add("OnChange", "setComboboxText('" + RcbVisRoles.ClientID + "')");
            List<string> roles = new List<string>();
            roles.AddRange(Roles.GetAllRoles());
            roles.Add("Anonymous");
            roles.Sort();

            foreach (string role in roles)
            {
                RadComboBoxItem item = new RadComboBoxItem(role, role);
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "CheckBox";
                checkbox.Text = role;
                checkbox.Attributes.Add("onClick", "setComboboxText('" + RcbVisRoles.ClientID + "');StopPropagation(event);");
                item.Controls.Add(checkbox);
                checkbox.Checked = string.IsNullOrEmpty(currentRole) || currentRole.ToLower().Contains(role.ToLower() + Constants.TAG_DELIMITER) || currentRole.ToLower().EndsWith(role.ToLower());
                if (checkbox.Checked)
                {
                    RcbVisRoles.Text += item.Text + ",";
                }
                RcbVisRoles.Items.Add(item);
            }

            RcbVisRoles.Text = RcbVisRoles.Text.TrimEnd(new char[] { ',' });

            CbFixed.Checked = widgetInstance.INS_IsFixed;
        }

        public bool Save()
        {
            try
            {
                string roles = string.Empty;
                for (int i = 0; i < RcbVisRoles.Items.Count; i++)
                {
                    string checkboxId = RcbVisRoles.UniqueID + "$i" + i + "$Checkbox";
                    if (Request.Form[checkboxId] != null)
                    {
                        roles += RcbVisRoles.Items[i].Text + Constants.TAG_DELIMITER;
                    }
                }
                roles = roles.TrimEnd(Constants.TAG_DELIMITER);

                CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select widgInstances).FirstOrDefault();
                if (!string.IsNullOrEmpty(RblHeadingTag.SelectedValue))  
                    widgetInstance.INS_HeadingTag = int.Parse(RblHeadingTag.SelectedValue);
                else
                    widgetInstance.INS_HeadingTag = null;

                widgetInstance.INS_IsFixed = CbFixed.Checked;
                widgetInstance.INS_ViewRoles = roles;
                dataContext.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }
    }
}
