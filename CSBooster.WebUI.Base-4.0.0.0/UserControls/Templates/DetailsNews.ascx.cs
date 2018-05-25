// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Templates
{
    public partial class DetailsNews : System.Web.UI.UserControl, IDataObjectWorker, ISettings
    {
        protected DataObject dataObject;
        protected DataObjectNews news;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Templates.WebUI.Base");
        private DataObjectList<DataObjectPicture> newsPictures;

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
            this.news = (DataObjectNews)dataObject;

            if (news != null)
            {
                foreach (string tooltipId in _4screen.CSB.DataAccess.Business.AdWordHelper.GetCampaignObjectIds(news.ObjectID.Value))
                {
                    this.RTTM.TargetControls.Add(tooltipId, true);
                    this.RTTM.Visible = true;
                }
            }

            this.LitDate.Text = news.StartDate.ToShortDateString() + " " + news.StartDate.ToShortTimeString();
            this.LitDescription.Text = news.DescriptionLinked;
            this.LitContent.Text = news.NewsTextLinked;

            StringBuilder sb = new StringBuilder();
            foreach (Link link in news.Links)
            {
                sb.AppendFormat("<div><a href=\"{0}\">{1}</a></div>", link.URL, link.Title);
            }
            this.LitLinks.Text = sb.ToString();

            newsPictures = DataObjects.Load<DataObjectPicture>(new QuickParameters
                                                                   {
                                                                       RelationParams = new RelationParams { ParentObjectID = news.ObjectID },
                                                                       ShowState = ObjectShowState.Published,
                                                                       Amount = 0,
                                                                       Direction = QuickSortDirection.Asc,
                                                                       PageNumber = 0,
                                                                       PageSize = 999999,
                                                                       SortBy = QuickSort.RelationSortNumber,
                                                                       Udc = UserDataContext.GetUserDataContext(),
                                                                       ObjectType = Helper.GetObjectTypeNumericID("Picture")
                                                                   });

            foreach (DataObject picture in newsPictures)
            {
                string imageId = "Img_" + picture.ObjectID.Value.ToString();
                this.RTTMIMG.TargetControls.Add(imageId, true);
                this.RTTMIMG.Visible = true;
                LiteralControl image = new LiteralControl(string.Format("<div style=\"float:left;width:110px;\"><div><img class ='articlepic' src=\"{0}{1}\" id=\"{2}\" /></div><div>{3}</div></div>", _4screen.CSB.Common.SiteConfig.MediaDomainName, picture.GetImage(PictureVersion.XS), imageId, picture.Title));
                this.PhNewsImgs.Controls.Add(image);
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
                    string pictureId = e.TargetControlID.Substring(4);
                    DataObject picture = newsPictures.Find(x => x.ObjectID.Value == pictureId.ToGuid());
                    Literal literal = new Literal();
                    literal.Text = string.Format("<div><div><img src=\"{0}{1}\"></div><div>{2}</div></div>", _4screen.CSB.Common.SiteConfig.MediaDomainName, picture.GetImage(PictureVersion.L), picture.Title);
                    e.UpdatePanel.ContentTemplateContainer.Controls.Add(literal);
                }
                catch { }
            }
        }
    }
}