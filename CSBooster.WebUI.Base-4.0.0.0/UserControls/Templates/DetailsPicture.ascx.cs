// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsPicture : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        private DataObjectPicture picture;
        private UserDataContext udc;

        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            picture = (DataObjectPicture)dataObject;
            udc = UserDataContext.GetUserDataContext();

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (picture != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(picture.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            if (Settings.ContainsKey("Width") && !string.IsNullOrEmpty(Settings["Width"].ToString()))
            {
                int width = 0;
                if (int.TryParse(Settings["Width"].ToString(), out width))
                {
                    if (picture.Width > width)
                        Img.Attributes.Add("max-width", width + "px");
                }
            }
            Img.ImageUrl = _4screen.CSB.Common.SiteConfig.MediaDomainName + picture.GetImage(PictureVersion.L);

            var copyrightConfig = Helper.LoadConfig("Copyrights.config", string.Format("{0}/Configurations/Copyrights.config", WebRootPath.Instance.ToString()));
            string copyrightText = (from copyright in copyrightConfig.Element("Copyrights").Elements("Copyright") where int.Parse(copyright.Attribute("Value").Value) == picture.Copyright select copyright.Attribute("Name").Value).Single();
            LitCopyright.Text = copyrightText;

            if (!string.IsNullOrEmpty(picture.DescriptionLinked))
            {
                PnlDesc.Visible = true;
                PnlDesc.ID = null;
                LitDesc.Text = picture.DescriptionLinked;
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
    }
}