// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsObject : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LitSDate.Text = DataObject.StartDate.ToString();
            LitEDate.Text = DataObject.EndDate.ToString();
            LnkTitle.Text = DataObject.Title.CropString(32);
            LnkTitle.NavigateUrl = Helper.GetDetailLink(DataObject.ObjectType, DataObject.ObjectID.Value.ToString());
            LitDesc.Text = DataObject.Description.StripHTMLTags().CropString(128, " ");
            if (string.IsNullOrEmpty(LitDesc.Text))
                LitDesc.Text = "-";

            Img1.ImageUrl = SiteConfig.MediaDomainName + DataObject.GetImage(PictureVersion.S);
            LnkImg.NavigateUrl = LnkTitle.NavigateUrl;
            LnkAutor.Text = DataObject.Nickname.CropString(16);
            LnkAutor.NavigateUrl = Helper.GetDetailLink("User", DataObject.UserID.Value.ToString());
            Community community = new Community(DataObject.CommunityID.Value);
            if (community != null && community.ProfileOrCommunity != null)
            {
                if (community.ProfileOrCommunity.ObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                {
                    litCom.Text = string.Format("<a href='{0}'>{1}</a>", Helper.GetDetailLink("User", community.ProfileOrCommunity.Nickname), GuiLanguage.GetGuiLanguage("SiteObjects").GetString("ProfileCommunity"));
                }
                else
                {
                    litCom.Text = string.Format("<a href='{0}'>{1}</a>", Helper.GetDetailLink("Community", ((DataObjectCommunity)community.ProfileOrCommunity).VirtualURL, true), community.ProfileOrCommunity.Title);
                }
            }

            LnkAutor.ID = null;
            LnkImg.ID = null;
            LnkTitle.ID = null;
            Img1.ID = null;
        }
    }
}