// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SmallOutputCommunity : System.Web.UI.UserControl, IDataObjectWorker
    {
        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string detailLink = Helper.GetDetailLink(DataObject.ObjectType, ((DataObjectCommunity)DataObject).VirtualURL, true);

            Img.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img.ID = null;

            LnkImg.NavigateUrl = detailLink;
            LnkImg.ID = null;

            LnkTitle.Text = ((DataObjectCommunity)DataObject).Title.CropString(20);
            LnkTitle.NavigateUrl = detailLink;
            LnkTitle.ID = null;

            LitDesc.Text = DataObject.Description.StripHTMLTags().CropString(40);
            LitDesc.ID = null;

            LitMembers.Text = ((DataObjectCommunity)DataObject).MemberCount.ToString();
            LitMembers.ID = null;

            LitViews.Text = DataObject.ViewCount.ToString();
            LitViews.ID = null;
        }
    }
}