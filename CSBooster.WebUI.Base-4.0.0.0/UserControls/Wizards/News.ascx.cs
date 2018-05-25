// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.UserControls.Wizards.Helpers;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class News : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectNews news;

        protected void Page_Load(object sender, EventArgs e)
        {
            news = DataObject.Load<DataObjectNews>(ObjectID, null, true);

            if (news.State == ObjectState.Added)
            {
                news.ObjectID = ObjectID;
                news.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                news.CommunityID = CommunityID;
                news.ShowState = ObjectShowState.Draft;
                news.Insert(UserDataContext.GetUserDataContext());
                news.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                news.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                news.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                news.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                news.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        news.Geo_Lat = geoLat;
                        news.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                news.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                news.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                news.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                news.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.TxtTitle.Text = news.Title;
            this.TxtDesc.Text = news.Description;
            this.TxtEvent.Content = news.NewsText;
            this.RDPFromDate.SelectedDate = news.StartDate;
            this.RDPToDate.SelectedDate = news.EndDate > this.RDPToDate.MaxDate ? this.RDPToDate.MaxDate : news.EndDate;
            this.TxtNewsRef.Text = news.ReferenceURL != null ? news.ReferenceURL.ToString() : string.Empty;
            this.HFTagWords.Value = news.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)news.Status).ToString();
            this.HFShowState.Value = ((int)news.ShowState).ToString();
            this.HFCopyright.Value = news.Copyright.ToString();
            if (news.Geo_Lat != double.MinValue && news.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = news.Geo_Lat.ToString();
                this.HFGeoLong.Value = news.Geo_Long.ToString();
            }
            this.HFZip.Value = news.Zip;
            this.HFCity.Value = news.City;
            this.HFStreet.Value = news.Street;
            this.HFCountry.Value = news.CountryCode;

            int linkCounter = 0;
            this.PhLinks.Controls.Clear();
            if (news.Links.Count > 0)
            {
                foreach (Link link in news.Links)
                {
                    NewsLinks newsLinks = (NewsLinks)this.LoadControl("~/UserControls/Wizards/Helpers/NewsLinks.ascx");
                    newsLinks.Title = link.Title;
                    newsLinks.URL = link.URL;
                    newsLinks.ID = string.Format("L{0}", linkCounter);
                    LinkButton removeButton = new LinkButton();
                    removeButton.ID = string.Format("LbtnR{0}", linkCounter);
                    removeButton.CommandArgument = linkCounter.ToString();
                    removeButton.CssClass = "CSB_input_btn_del";
                    removeButton.ToolTip = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base").GetString("TooltipNewsLinkRemove");
                    removeButton.Click += new EventHandler(OnRemoveLinkClick);
                    newsLinks.RemoveButton = removeButton;
                    this.PhLinks.Controls.Add(newsLinks);
                    linkCounter++;
                }
            }
            else
            {
                NewsLinks newsLinks = (NewsLinks)this.LoadControl("~/UserControls/Wizards/Helpers/NewsLinks.ascx");
                newsLinks.ID = string.Format("L{0}", linkCounter);
                this.PhLinks.Controls.Add(newsLinks);
            }
        }

        void OnRemoveLinkClick(object sender, EventArgs e)
        {
            int linkIndex = int.Parse(((LinkButton)sender).CommandArgument);
            List<Link> links = news.Links;
            links.RemoveAt(linkIndex);
            news.Links = links;
            news.Update(UserDataContext.GetUserDataContext());

            FillEditForm();
        }

        protected void OnAddMoreLinksClick(object sender, EventArgs e)
        {
            List<Link> links = new List<Link>();
            foreach (Control newsLinks in this.PhLinks.Controls)
            {
                if (newsLinks is NewsLinks)
                {
                    Link link = new Link();
                    link.Title = ((NewsLinks)newsLinks).Title;
                    link.URL = ((NewsLinks)newsLinks).URL;
                    links.Add(link);
                }
            }
            links.Add(new Link());
            news.Links = links;
            news.Update(UserDataContext.GetUserDataContext());

            FillEditForm();
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                news.Title = this.TxtTitle.Text.StripHTMLTags();
                news.Description = this.TxtDesc.Text.StripHTMLTags().CropString(20000);
                news.NewsText = this.TxtEvent.Content;
                if (AccessMode == AccessMode.Insert)
                    news.StartDate = DateTime.Now;
                if (this.RDPFromDate.SelectedDate.HasValue && this.RDPFromDate.SelectedDate.Value != DateTime.Now.GetStartOfDay())
                    news.StartDate = this.RDPFromDate.SelectedDate.Value;
                else
                    news.StartDate = DateTime.Now;
                if (this.RDPToDate.SelectedDate.HasValue)
                    news.EndDate = this.RDPToDate.SelectedDate.Value;
                news.ReferenceURL = !string.IsNullOrEmpty(this.TxtNewsRef.Text) ? new Uri(this.TxtNewsRef.Text) : null;
                news.TagList = this.HFTagWords.Value.StripHTMLTags();
                news.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                news.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                news.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    news.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    news.Geo_Long = geoLong;
                news.Zip = this.HFZip.Value;
                news.City = this.HFCity.Value;
                news.Street = this.HFStreet.Value;
                news.CountryCode = this.HFCountry.Value;

                List<Link> links = new List<Link>();
                foreach (Control newsLinks in this.PhLinks.Controls)
                {
                    if (newsLinks is NewsLinks)
                    {
                        Link link = new Link();
                        link.Title = ((NewsLinks)newsLinks).Title;
                        link.URL = ((NewsLinks)newsLinks).URL;
                        if (!string.IsNullOrEmpty(link.Title) && link.URL != null)
                            links.Add(link);
                    }
                }
                news.Links = links;

                news.Update(UserDataContext.GetUserDataContext());

                return true;
            }
            catch (Exception ex)
            {
                this.LitMsg.Text = "Fehler beim Speichern: " + ex.Message;
                return false;
            }
        }
    }
}