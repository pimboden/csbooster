using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class HtmlContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataObject content = DataObject.Load<DataObject>(Request.QueryString["OID"].ToGuid());
            LitContent.Text = content.Description;
        }
    }
}
