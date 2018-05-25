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
using _4screen.CSB.DataAccess.Data;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class OutputTemplates : System.Web.UI.UserControl, IWidgetSettings
    {
        private Guid? currentTemplateId;

        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
            var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select widgInstances).FirstOrDefault();
            currentTemplateId = widgetInstance.INS_OutputTemplate;
            HfTemplate.Value = currentTemplateId.ToString();

            List<Guid> outputTemplateIds = WidgetSection.CachedInstance.Widgets[widgetInstance.WDG_ID].OutputTemplates.Split(';').ToList().ConvertAll(value => new Guid(value));
            var outputTemplates = OutputTemplatesSection.CachedInstance.Templates.LINQEnumarable.Where(x => outputTemplateIds.Contains(x.Id));
            RepTemplates.DataSource = outputTemplates;
            RepTemplates.DataBind();
        }

        public bool Save()
        {
            try
            {
                CSBooster_DataContext dataContext = new CSBooster_DataContext(Helper.GetSiemeConnectionString());
                var widgetInstance = (from widgInstances in dataContext.hitbl_WidgetInstance_INs.Where(x => x.INS_ID == InstanceId) select widgInstances).FirstOrDefault();
                widgetInstance.INS_OutputTemplate = HfTemplate.Value.ToNullableGuid();
                dataContext.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }

        protected void OnRepTemplatesDataBound(object sender, RepeaterItemEventArgs e)
        {
            OutputTemplateElement template = (OutputTemplateElement)e.Item.DataItem;

            HtmlGenericControl div = new HtmlGenericControl("div");
            if (template.Id == currentTemplateId)
                div.Attributes.Add("class", "itemSelected");
            else
                div.Attributes.Add("class", "itemNotSelected");
            div.Attributes.Add("id", template.Id.ToString());
            div.Attributes.Add("onClick", "SelectElement('" + template.Id + "', '" + HfTemplate.ClientID + "')");

            div.Controls.Add(new LiteralControl(string.Format("<img src=\"/Library/Images/OutputTemplates/{0}\"/>", template.Image)));

            PlaceHolder panel = (PlaceHolder)e.Item.FindControl("PhTemplate");
            panel.Controls.Add(div);
        }
    }
}
