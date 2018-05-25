// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class PinboardSearch : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectPinboardSearch pinboardSearch;

        protected void Page_Load(object sender, EventArgs e)
        {
            pinboardSearch = DataObject.Load<DataObjectPinboardSearch>(ObjectID, null, true);

            if (pinboardSearch.State == ObjectState.Added)
            {
                pinboardSearch.ObjectID = ObjectID;
                pinboardSearch.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                pinboardSearch.CommunityID = CommunityID;
                pinboardSearch.ShowState = ObjectShowState.Draft;
                pinboardSearch.Insert(UserDataContext.GetUserDataContext());
                pinboardSearch.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                pinboardSearch.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                pinboardSearch.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                pinboardSearch.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                pinboardSearch.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        pinboardSearch.Geo_Lat = geoLat;
                        pinboardSearch.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                pinboardSearch.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                pinboardSearch.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                pinboardSearch.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                pinboardSearch.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.TxtTitle.Text = pinboardSearch.Title;
            this.TxtDesc.Text = pinboardSearch.Description;
            this.TxtPrice.Text = pinboardSearch.Price;
            if (pinboardSearch.EndDate > this.RDPExpDate.MaxDate)
                this.RDPExpDate.SelectedDate = this.RDPExpDate.MaxDate;
            else
                this.RDPExpDate.SelectedDate = pinboardSearch.EndDate;
            this.HFTagWords.Value = pinboardSearch.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)pinboardSearch.Status).ToString();
            this.HFShowState.Value = ((int)pinboardSearch.ShowState).ToString();
            this.HFCopyright.Value = pinboardSearch.Copyright.ToString();
            if (pinboardSearch.Geo_Lat != double.MinValue && pinboardSearch.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = pinboardSearch.Geo_Lat.ToString();
                this.HFGeoLong.Value = pinboardSearch.Geo_Long.ToString();
            }
            this.HFZip.Value = pinboardSearch.Zip;
            this.HFCity.Value = pinboardSearch.City;
            this.HFStreet.Value = pinboardSearch.Street;
            this.HFCountry.Value = pinboardSearch.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                pinboardSearch.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                pinboardSearch.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                pinboardSearch.Price = Common.Extensions.StripHTMLTags(this.TxtPrice.Text);
                if (this.RDPExpDate.SelectedDate.HasValue)
                    pinboardSearch.EndDate = this.RDPExpDate.SelectedDate.Value;
                pinboardSearch.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                pinboardSearch.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                pinboardSearch.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                pinboardSearch.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    pinboardSearch.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    pinboardSearch.Geo_Long = geoLong;
                pinboardSearch.Zip = this.HFZip.Value;
                pinboardSearch.City = this.HFCity.Value;
                pinboardSearch.Street = this.HFStreet.Value;
                pinboardSearch.CountryCode = this.HFCountry.Value;

                pinboardSearch.Update(UserDataContext.GetUserDataContext());

                return true;
            }
            catch (Exception ex)
            {
                this.LitMsg.Text = string.Format("{0}: ", language.GetString("MessageSaveError")) + ex.Message;
                return false;
            }
        }
    }
}