// ******************************************************************************
//  Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//  System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.DataAccess;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public partial class RSS : WidgetBase
    {
        protected bool showDescription = true;
        private bool hasValue = true;

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["XMLSettings"] == null)
            {
                base.Page_Load(sender, e);
            }
            if (Request.Form["__EVENTTARGET"] == TimReload.UniqueID)
            {
                OnTimerReloadTick(null, null);
            }
        }

        public override bool ShowObject(string strXml)
        {
            ViewState["XMLSettings"] = strXml;
            IMGPRGRS.ImageUrl = string.Format("{0}/Library/Images/Layout/icon_loader.gif", SiteContext.VRoot);
            TimReload.Enabled = false;
            return hasValue;
        }

        protected void OnTimerReloadTick(object sender, EventArgs e)
        {
            MV.ActiveViewIndex = 0;
            TimReload.Enabled = false;
            TimLoad.Enabled = true;
        }

        protected void OnTimerLoadTick(object sender, EventArgs e)
        {
            TimLoad.Enabled = false;
            TimReload.Enabled = false;
            string settingsXml = (string)ViewState["XMLSettings"];

            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);
            string url = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtURL", string.Empty);
            int feedItemsCount = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlFC", 10);
            showDescription = XmlHelper.GetElementValue(xmlDom.DocumentElement, "cbxDesc", true);
            int reloadTimeout = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlFR", 0);

            SyndicationFeed feed = null;

            try
            {
                using (XmlReader reader = XmlReader.Create(url))
                {
                    Rss20FeedFormatter feedFormatter = new Rss20FeedFormatter();
                    feedFormatter.ReadFrom(reader);
                    feed = feedFormatter.Feed;
                }
            }
            catch { }

            if (feed == null)
            {
                try
                {
                    using (XmlReader reader = XmlReader.Create(url))
                    {
                        Atom10FeedFormatter feedFormatter = new Atom10FeedFormatter();
                        feedFormatter.ReadFrom(reader);
                        feed = feedFormatter.Feed;
                    }
                }
                catch { }
            }

            hasValue = false;
            if (feed != null)
            {
                RepFeed.DataSource = feed.Items.Take(feedItemsCount);
                RepFeed.DataBind();
            }
            else
            {
                LitMsg.Text = string.Format("<div>{0}</div>", GuiLanguage.GetGuiLanguage("WidgetRSS").GetString("MessageFeedError"));
            }

            if (reloadTimeout == 0)
            {
                TimReload.Enabled = false;
            }
            else
            {
                TimReload.Interval = reloadTimeout * 1000;
                TimReload.Enabled = true;
            }

            MV.ActiveViewIndex = 1;
        }

        protected void OnRepFeedItemBound(object sender, RepeaterItemEventArgs e)
        {
            hasValue = true;
            SyndicationItem item = (SyndicationItem)e.Item.DataItem;
            Literal date = (Literal)e.Item.FindControl("LitDate");
            HyperLink link = (HyperLink)e.Item.FindControl("FeedLink");
            Panel description = (Panel)e.Item.FindControl("PnlDesc");

            date.Text = item.PublishDate.ToLocalTime().ToString();
            int pos = date.Text.IndexOf("+");
            if (pos > 0)
                date.Text = date.Text.Substring(0, pos - 1);

            link.Text = item.Title.Text;

            if (item.Links.Count > 0)
                link.NavigateUrl = item.Links[0].Uri.ToString();

            if (item.Summary != null && !string.IsNullOrEmpty(item.Summary.Text) && showDescription)
            {
                description.Controls.Add(new LiteralControl(item.Summary.Text));
                description.Visible = true;
            }

            date.ID = null;
            link.ID = null;
            description.ID = null;
        }
    }
}