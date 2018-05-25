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
    public partial class DetailsEvent : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        private DataObjectEvent dataObjectEvent;
        private GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public Dictionary<string, object> Settings { get; set; }
        public DataObject DataObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            dataObjectEvent = (DataObjectEvent)DataObject;

            RTTM.Visible = false;
            if (dataObjectEvent != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(dataObjectEvent.ObjectID.Value))
                {
                    RTTM.TargetControls.Add(tooltipId, true);
                    RTTM.Visible = true;
                }
            }

            PrintEvent();
        }

        private void PrintEvent()
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

            if (!string.IsNullOrEmpty(dataObjectEvent.Image))
            {
                Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + dataObjectEvent.GetImage(PictureVersion.M);
            }
            else
            {
                Img.Visible = false;
            }

            DataObjectLocation currentLocation = DataObjects.Load<DataObjectLocation>(new QuickParameters()
            {
                Udc = UserDataContext.GetUserDataContext(),
                DisablePaging = true,
                IgnoreCache = true,
                RelationParams = new RelationParams()
                {
                    ChildObjectID = dataObjectEvent.ObjectID
                }
            }).SingleOrDefault();
            if (currentLocation != null)
            {
                LnkLocation.Text = currentLocation.Title;
                LnkLocation.NavigateUrl = Helper.GetDetailLink(currentLocation.ObjectType, currentLocation.ObjectID.ToString());
                LnkLocation.ID = null;
                if (!string.IsNullOrEmpty(currentLocation.Street))
                {
                    LitAddress.Text += currentLocation.Street + "<br/>";
                }
                if (!string.IsNullOrEmpty(currentLocation.City))
                {
                    if (!string.IsNullOrEmpty(currentLocation.Zip))
                        LitAddress.Text += currentLocation.Zip + " " + currentLocation.City + "<br/>";
                    else
                        LitAddress.Text += currentLocation.City + "<br/>";
                }
            }

            LitWhen.Text = dataObjectEvent.StartDate.ToShortDateString();
            if (dataObjectEvent.EndDate != DateTime.MaxValue && dataObjectEvent.EndDate != dataObjectEvent.StartDate)
            {
                LitWhen.Text += " - " + dataObjectEvent.EndDate.ToShortDateString();
            }
            if (!string.IsNullOrEmpty(dataObjectEvent.Time))
            {
                LitWhen.Text += "<br/>" + dataObjectEvent.Time;
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.TagList))
            {
                LitType.Text = string.Join(", ", Helper.GetMappedTagWords(dataObjectEvent.TagList).ToArray());
            }

            if (dataObjectEvent.Website != null)
            {
                TrWebsite.Visible = true;
                LnkWebsite.Text = dataObjectEvent.Website.ToString();
                LnkWebsite.NavigateUrl = dataObjectEvent.Website.ToString();
            }
            LnkWebsite.ID = null;

            if (!string.IsNullOrEmpty(dataObjectEvent.Age))
            {
                TrAge.Visible = true;
                LitAge.Text = dataObjectEvent.Age;
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.Price))
            {
                TrPrice.Visible = true;
                LitPrice.Text = dataObjectEvent.Price;
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.DescriptionLinked))
            {
                TrDesc.Visible = true;
                LitDesc.Text = dataObjectEvent.DescriptionLinked;
            }

            if (!string.IsNullOrEmpty(dataObjectEvent.Content))
            {
                TrEvent.Visible = true;
                LitEvent.Text = dataObjectEvent.Content;
            }

            TrWebsite.ID = null;
            TrAge.ID = null;
            TrPrice.ID = null;
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