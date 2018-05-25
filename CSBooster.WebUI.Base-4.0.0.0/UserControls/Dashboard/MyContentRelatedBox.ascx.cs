// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class MyContentRelatedBox : System.Web.UI.UserControl, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }
        public bool IsSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsSource)
            {
                LitItemStart.Text = "<li objectid=\"" + DataObject.ObjectID + "\" class=\"draggable\">";
            }
            else
            {
                LitItemStart.Text = "<li objectid=\"" + DataObject.ObjectID + "\">";
                LitFunctions.Text = "<a href=\"javascript:void(0)\" class=\"ui-icon-remove\"></a><input type=\"hidden\" name=\"relatedObjectId\" value=\"" + DataObject.ObjectID + "\" />";
            }
            LitItemEnd.Text = "</li>";

            LitTitle.Text = DataObject.Title;
            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ID = null;
        }
    }
}