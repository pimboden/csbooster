// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsDocument : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataObjectDocument document = (DataObjectDocument)dataObject;
            GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base"); 

            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            if (document != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(document.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }
            string strSize = document.SizeByte < 1000 ? string.Format("{0} Bytes", document.SizeByte) : string.Format("{0:f1} KB", document.SizeByte / 1024.00);

            this.LitDownload.Text = string.Format("<a href='{0}{1}' alt={2} target='_blank'>{5} {3} ({4}) </a>", SiteConfig.MediaDomainName, document.URLDocument, document.Title, document.Title.CropString(25), strSize, language.GetString("LabelDocumentDownload"));
            this.DESCLIT.Text = document.DescriptionLinked;
            if (!string.IsNullOrEmpty(document.Author))
            {
                this.DESCLIT.Text += string.Format("<br/>{0}: {1} ", language.GetString("LabelDocumentAuthor"), document.Author);
            }
            if (!string.IsNullOrEmpty(document.Version))
            {
                this.DESCLIT.Text += string.Format("<br/>{0}: {1} ", language.GetString("LabelDocumentVersion"), document.Version);
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