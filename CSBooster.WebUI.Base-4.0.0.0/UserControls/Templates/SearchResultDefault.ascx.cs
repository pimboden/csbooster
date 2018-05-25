using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class SearchResultDefault : System.Web.UI.UserControl, ISettings
    {
        private Dictionary<string, object> settings = new Dictionary<string, object>();

        public Dictionary<string, object> Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObject dataObject = (DataObject)Settings["DataObject"];
            LnkTitle.Text = dataObject.Title.StripHTMLTags();
            LnkTitle.NavigateUrl = Helper.GetDetailLink(dataObject.objectType, dataObject.ObjectID.ToString());
            LnkDesc.NavigateUrl = LnkTitle.NavigateUrl;
            LitDesc.Text = dataObject.Description.StripHTMLTags().CropString(400);

            string imageUrl = dataObject.GetImage(PictureVersion.S, false);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                Img.Visible = true;
                Img.ImageUrl = SiteConfig.MediaDomainName + imageUrl;
                Img.ID = null;
            }

            LnkTitle.ID = null;
            LnkDesc.ID = null;
        }
    }
}
