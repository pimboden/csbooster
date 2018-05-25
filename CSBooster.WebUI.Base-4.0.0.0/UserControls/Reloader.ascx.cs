using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class Reloader : System.Web.UI.UserControl, IReloader
    {
        public Control BrowsableControl { get; set; }
        public bool FullReload { get; set; }
        public int ObjectType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LbtnReload.Text = string.Format(GuiLanguage.GetGuiLanguage("UserControls.WebUI.Base").GetString("CommandLoadMore"), Helper.GetObjectName(ObjectType, false));
        }

        protected void OnReloadClick(object sender, EventArgs e)
        {
            if (FullReload)
                Response.Redirect(Request.RawUrl);
            else
                ((IReloadable)BrowsableControl).Reload();
        }
    }
}