// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using _4screen.WebControls;
using SiteConfig = _4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsLocation : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        private DataObjectLocation location;
        private GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public DataObject DataObject { get; set; }
        public Dictionary<string, object> Settings { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            location = (DataObjectLocation)DataObject;

            RTTM.Visible = false;
            if (location != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(location.ObjectID.Value))
                {
                    RTTM.TargetControls.Add(tooltipId, true);
                    RTTM.Visible = true;
                }
            }

            PrintLocation();
        }

        private void PrintLocation()
        {
            if (Request.IsAuthenticated)
            {
                string initQuerySegment = "&XCN=" + UserProfile.Current.ProfileCommunityID + "&OID=" + DataObject.ObjectID;
                if (!SiteConfig.UsePopupWindows)
                    initQuerySegment += "&ReturnUrl=" + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(Request.RawUrl));
                LnkRelCnt.NavigateUrl = string.Format("javascript:radWinOpen('{0}{1}', '{2}', 800, 500, true, null, 'wizardWin')", Helper.GetWizardLink("AddPicturesAndVideos", SiteConfig.UsePopupWindows), initQuerySegment, GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base").GetString("CommandAddPicturesAndVideos"));
            }
            else
            {
                LnkRelCnt.Enabled = false;
                LnkRelCnt.ToolTip = (new TextControl() { LanguageFile = "UserControls.Templates.WebUI.Base", TextKey = "TooltipLoginToAddPhotosAndVideos" }).Text;
            }
            LnkRelCnt.ID = null;

            if (!string.IsNullOrEmpty(location.Image))
            {
                Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + location.GetImage(PictureVersion.M);
            }
            else
            {
                Img.Visible = false;
            }

            LitAddress.Text += location.Title + "<br/>";
            if (!string.IsNullOrEmpty(location.Street))
            {
                LitAddress.Text += location.Street + "<br/>";
            }
            if (!string.IsNullOrEmpty(location.City))
            {
                if (!string.IsNullOrEmpty(location.Zip))
                    LitAddress.Text += location.Zip + " " + location.City + "<br/>";
                else
                    LitAddress.Text += location.City + "<br/>";
            }

            if (!string.IsNullOrEmpty(location.TagList))
            {
                LitType.Text = string.Join(", ", Helper.GetMappedTagWords(location.TagList).ToArray());
            }

            if (location.Website != null)
            {
                TrWebsite.Visible = true;
                LnkWebsite.Text = location.Website.ToString();
                LnkWebsite.NavigateUrl = location.Website.ToString();
            }
            LnkWebsite.ID = null;

            if (!string.IsNullOrEmpty(location.DescriptionLinked))
            {
                TrDesc.Visible = true;
                LitDesc.Text = location.DescriptionLinked;
            }

            TrWebsite.ID = null;
            TrDesc.ID = null;
            LitDesc.ID = null;
        }

        protected void OnAjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            string[] tooltipId = e.TargetControlID.Split(new char[] { '_' });
            if (tooltipId.Length == 4)
            {
                Literal literal = new Literal();
                literal.Text = _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignContent(new Guid(tooltipId[0]), new Guid(tooltipId[1]), UserDataContext.GetUserDataContext(), tooltipId[2], "Popup");
                literal.Text = System.Text.RegularExpressions.Regex.Replace(literal.Text, @"(/Pages/Other/AdCampaignRedirecter.aspx\?CID=\w{8}-\w{4}-\w{4}-\w{4}-\w{12})", "$1&OID=" + tooltipId[1] + "&Word=" + tooltipId[2] + "&Type=PopupLink");
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
            }
        }
    }
}