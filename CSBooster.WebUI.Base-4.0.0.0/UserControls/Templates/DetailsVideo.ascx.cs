// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsVideo : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObjectVideo Video;
        private int videoWidth = 400;
        private int videoHeight = 300;

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        protected DataObject dataObject;
        protected string MediaDomainName = _4screen.CSB.Common.SiteConfig.MediaDomainName;

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected string VideoContentId
        {
            get { return "VideoContent_" + Video.ObjectID; }
        }

        protected string VideoPlayerId
        {
            get { return "Player_" + Video.ObjectID; }
        }

        protected string VideoURL
        {
            get
            {
                return string.Format("{0}{1}", Helper.GetVideoBaseURL(), Video.GetLocation(VideoFormat.Flv, VideoVersion.None));
            }
        }

        protected string ThumbImgURL
        {
            get
            {
                if (string.IsNullOrEmpty(Video.GetImage(PictureVersion.L)))
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, "/Defmedia/DataObjectVideoImageSmall.gif");
                else
                    return string.Format("{0}{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, Video.GetImage(PictureVersion.L));
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
                return videoWidth.ToString();
            }
        }

        protected string FlashHeight
        {
            get
            {
                return videoHeight.ToString();
            }
        }


        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            Video = (DataObjectVideo)dataObject;

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (Video != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(Video.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            // calculate video size
            if (Settings.ContainsKey("Width") && !string.IsNullOrEmpty(Settings["Width"].ToString()))
            {
                int width = 0;
                if (int.TryParse(Settings["Width"].ToString(), out width))
                {
                    if (Video.Width > width)
                    {
                        videoWidth = width;

                        float fltRatio = (float)Video.Height / (float)Video.Width;
                        videoHeight = Convert.ToInt32(fltRatio * (float)videoWidth);
                    }
                    else
                    {
                        if (Video.Width > 0)
                            videoWidth = Video.Width;

                        if (Video.Height > 0)
                            videoHeight = Video.Height;
                    }
                }
            }


            var copyrightConfig = Helper.LoadConfig("Copyrights.config", string.Format("{0}/Configurations/Copyrights.config", WebRootPath.Instance.ToString()));
            string copyrightText = (from copyright in copyrightConfig.Element("Copyrights").Elements("Copyright") where int.Parse(copyright.Attribute("Value").Value) == Video.Copyright select copyright.Attribute("Name").Value).Single();
            LitCopyright.Text = copyrightText;

            if (!string.IsNullOrEmpty(Video.DescriptionLinked))
            {
                PnlDesc.Visible = true;
                PnlDesc.ID = null;
                LitDesc.Text = Video.DescriptionLinked;
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