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
    public partial class MyContentSelectionBox : System.Web.UI.UserControl, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ID = null;

            string pictureFormats = DataObject.GetXMLValue("PictureXS") + ":" + DataObject.GetXMLValue("PictureS") + ":" + DataObject.GetXMLValue("PictureL") + ":" + DataObject.GetXMLValue("PictureA");

            string onClick = string.Format("CloseWindow('{0},{1}{2},'+document.getElementById('{3}').value+','+document.getElementById('{4}').value+',{5}')", DataObject.ObjectID.Value, SiteConfig.MediaDomainName, DataObject.GetImage((PictureVersion)Settings["SelectionCallbackReturnPictureVersion"]), Settings["SelectionCallbackPictureVersion"], Settings["SelectionCallbackPopupVersion"], pictureFormats);
            LnkDet1.Attributes.Add("OnClick", onClick);

            LnkDet2.Text = DataObject.Title;
            LnkDet2.ToolTip = DataObject.Title;
            LnkDet2.Attributes.Add("OnClick", onClick);
        }
    }
}