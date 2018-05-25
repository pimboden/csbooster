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
    public partial class SmallOutputUser : System.Web.UI.UserControl, IDataObjectWorker
    {
        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string detailLink =Helper.GetDetailLink("User", DataObject.Nickname);

            Img1.ImageUrl = string.Format("/Library/Images/User/LG/SC/{0}.png", ((DataObjectUser)DataObject).SecondaryColor);
            Img1.ID = null;

            Img2.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            Img2.ID = null;

            Img3.ImageUrl = string.Format("/Library/Images/User/LG/PC/{0}.png", ((DataObjectUser)DataObject).PrimaryColor);
            Img3.ID = null;

            LnkImg1.NavigateUrl = detailLink;
            LnkImg1.ID = null;

            LnkImg2.NavigateUrl = detailLink;
            LnkImg2.ID = null;

            LnkImg3.NavigateUrl = detailLink;
            LnkImg3.ID = null;

            LnkUser.Text = DataObject.Nickname.CropString(16);
            LnkUser.NavigateUrl = detailLink;
            LnkUser.ID = null;

            LitAge.Text = ((DataObjectUser)DataObject).Age != 0 ? ((DataObjectUser)DataObject).Age.ToString() : "-";
            LitAge.ID = null;

            LitCity.Text = !string.IsNullOrEmpty(DataObject.City) ? DataObject.City : "-";
            LitCity.ID = null;
        }
    }
}