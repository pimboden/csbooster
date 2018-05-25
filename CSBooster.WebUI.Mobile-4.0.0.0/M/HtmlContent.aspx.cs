using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M
{
    public partial class HtmlContent : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObject content = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
            LitTitle.Text = content.Title;
            LitContent.Text = content.Description.Replace("<br>", "<br/>");
        }
    }
}