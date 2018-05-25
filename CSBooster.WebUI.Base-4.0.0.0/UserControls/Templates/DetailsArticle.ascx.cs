// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsArticle : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        protected DataObjectArticle article;
        private Dictionary<Guid, PictureVersion> articlePictures = new Dictionary<Guid, PictureVersion>();

        public Dictionary<string, object> Settings { get; set; }

        public DataObject DataObject
        {
            get { return dataObject; }
            set { dataObject = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Disable tooltip managers by default -> tooltip managers without targets don't work
            this.RTTM.Visible = false;
            this.RTTMIMG.Visible = false;
            this.article = (DataObjectArticle)dataObject;

            if (article != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(article.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            this.DETSUBTITLE.Text = article.DescriptionLinked;
            this.ARTTXTLIT.Text = article.ArticleTextLinked;

            string idPrefix;
            articlePictures = _4screen.CSB.DataAccess.Business.Utils.GetPicturesFromContent(article.ArticleText, out idPrefix);
            foreach (var pictureId in articlePictures.Keys)
            {
                string imageId = string.Format("Img_{0}", pictureId);
                this.RTTMIMG.TargetControls.Add(imageId, true);
                this.RTTMIMG.Visible = true;
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
            else if (e.TargetControlID.IndexOf("Img_") > -1)
            {
                try
                {
                    Guid pictureId = e.TargetControlID.Substring(4).ToGuid();
                    DataObject picture = DataAccess.Business.DataObject.Load<DataObjectPicture>(pictureId);
                    Literal literal = new Literal();
                    literal.Text = string.Format("<div><div><img src=\"{0}{1}\"></div><div>{2}</div></div>", SiteConfig.MediaDomainName, picture.GetImage(articlePictures[pictureId]), picture.Title);
                    e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
                }
                catch { }
            }
        }
    }
}