// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsAudio : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        private DataObjectAudio audio;
        private int audioWidth = 400;
        private int audioHeight = 150;

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        protected DataObject dataObject;
        protected string MediaDomainName = _4screen.CSB.Common.SiteConfig.MediaDomainName;

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected string AudioURL
        {
            get
            {
                return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, audio.Location.ToLower());
            }
        }

        protected string ThumbImgURL
        {
            get
            {
                if (string.IsNullOrEmpty(audio.GetImage(PictureVersion.S)))
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, "/Defmedia/DataObjectAudioImageSmall.gif");
                else
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, audio.GetImage(PictureVersion.S));
            }
        }

        protected string LargeImgURL
        {
            get
            {
                if (string.IsNullOrEmpty(audio.GetImage(PictureVersion.L)))
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, "/Defmedia/DataObjectAudioImageSmall.gif");
                else
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, audio.GetImage(PictureVersion.L));
            }
        }

        protected string VideoSkin
        {
            get
            {
                return string.Format("{0}/App_Themes/{1}/videoskin.zip", _4screen.CSB.Common.SiteConfig.HostName, Page.Theme);
            }
        }

        protected string FlashVarsXml
        {
            get
            {
                return string.Format("{0}/App_Themes/{1}/videoflashvars.xml", _4screen.CSB.Common.SiteConfig.HostName, Page.Theme);
            }
        }

        protected string FlashWidth
        {
            get
            {
                return audioWidth.ToString();
            }
        }

        protected string FlashHeight
        {
            get
            {
                return audioHeight.ToString();
            }
        }

        protected string DescLinked
        {
            get
            {
                return audio.DescriptionLinked;
            }
        }

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            audio = (DataObjectAudio)dataObject;

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (audio != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(audio.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            // calculate audio size
            if (Settings.ContainsKey("Width") && !string.IsNullOrEmpty(Settings["Width"].ToString()))
            {
                int width = 0;
                if (int.TryParse(Settings["Width"].ToString(), out width))
                {
                    audioWidth = width;
                }
            }
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

        #endregion
    }
}