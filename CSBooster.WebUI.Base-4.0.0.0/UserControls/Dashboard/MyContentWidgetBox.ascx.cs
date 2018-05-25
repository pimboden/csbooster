// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyContentWidgetBox : System.Web.UI.UserControl
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            WT.Attributes.Add("IsTemplate", "true");
            WT.Attributes.Add("ObjectId", DataObject.ObjectID.ToString());
            WT.Attributes.Add("ObjectType", DataObject.ObjectType.ToString());

            PnlTitle.Controls.Add(new LiteralControl(DataObject.Title));
            PnlTitle.ToolTip = DataObject.Title;
            PnlTitle.ID = null;

            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ID = null;
        }
    }
}