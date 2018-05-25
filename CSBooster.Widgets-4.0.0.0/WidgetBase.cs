// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget
{
    public abstract class WidgetBase : System.Web.UI.UserControl
    {
        public IWidgetHost WidgetHost { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool widgetHasContent = ShowObject(WidgetHost.WidgetInstance.INS_XmlStateData);

                if (!widgetHasContent)
                {
                    if (!WidgetHost.SiteContext.Udc.IsAdmin)
                    {
                        Control widgetControl = WidgetHelper.GetWidgetHost(this, 0, 5);
                        if (widgetControl != null)
                            widgetControl.Visible = false;
                    }
                    else
                    {
                        Controls.Add(new LiteralControl(string.Format("<div class=\"widgetEmpty\">{0}</div>", GuiLanguage.GetGuiLanguage("Widgets").GetString("MessageShowOnlyForAdmin"))));
                    }
                }
            }
            catch (Exception exception)
            {
                Visible = true;
                Controls.Clear();
                LogManager.WriteEntry(exception);
                Controls.Add(new LiteralControl(exception.Message));
            }
        }

        public abstract bool ShowObject(string settingsXml);
    }
}