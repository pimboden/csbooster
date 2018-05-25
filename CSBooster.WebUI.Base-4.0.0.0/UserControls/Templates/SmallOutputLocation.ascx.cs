// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputLocation : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataObject.GetImage(PictureVersion.S, false)))
            {
                PnlImage.Visible = true;
                PnlImage.ID = null;
                Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            }
            LnkTitle.Text = DataObject.Title;
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString());
            LnkImg.NavigateUrl = LnkTitle.NavigateUrl;
            LitDesc.Text = DataObject.Description.StripHTMLTags().CropString(128);
            LitType.Text = string.Join(", ", Helper.GetMappedTagWords(DataObject.TagList).ToArray());
            Img.ID = null;
            LnkTitle.ID = null;
            LnkImg.ID = null;
        }
    }
}