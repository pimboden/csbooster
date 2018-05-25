using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.UserControls
{
    public partial class Search : System.Web.UI.UserControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");

        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearch.Attributes.Add("onkeypress", string.Format("RedirectOnEnterKey(event, '/M/objectoverview.aspx?OT={0}&SG=', '{1}')", Request.QueryString["OT"], txtSearch.ClientID));
            hlkSearh.Attributes.Add("onclick",
                                    string.Format("RedirectOnClick('/M/objectoverview.aspx?OT={0}&SG=', '{1}')", Request.QueryString["OT"], txtSearch.ClientID));
            if(!string.IsNullOrEmpty(Request.QueryString["SG"]))
            {
                txtSearch.Text = Request.QueryString["SG"];
            }
        }
    }
}