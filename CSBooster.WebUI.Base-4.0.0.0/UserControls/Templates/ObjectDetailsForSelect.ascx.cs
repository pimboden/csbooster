// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class ObjectDetailsForSelect : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public string FolderParams { get; set; }
        public QuickParameters QuickParameters { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool showNiceLinks = false;
            if (FolderParams == null)
            {
                FolderParams = string.Empty;
                showNiceLinks = true;
            }

            LitTile.Text = DataObject.Title.CropString(32);
            LitDesc.Text = DataObject.Description.StripHTMLTags().CropString(128, " ");
            if (string.IsNullOrEmpty(LitDesc.Text))
                LitDesc.Text = "-";

            Img1.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            LitDesc.ID = null;
            LitTile.ID = null;
            Img1.ID = null;
        }
    }
}