// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class Permalink : System.Web.UI.UserControl, IDataObjectWorker
    {
        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<input class=\"CSB_textbox\" style=\"width:97%\" type=\"textbox\" onClick=\"this.select()\" value=\"{0}{1}\">", SiteConfig.SiteURL, Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString()));
            this.LitCnt.Text = sb.ToString();
        }
    }
}
