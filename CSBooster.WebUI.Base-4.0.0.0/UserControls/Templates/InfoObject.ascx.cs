using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class InfoObject : System.Web.UI.UserControl, IDataObjectWorker
    {
        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LitTitle.Text = DataObject.Title;
        }
    }
}