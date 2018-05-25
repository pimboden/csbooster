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
    public partial class PinboardOffer : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectPinboardOffer pinboardOffer;

        protected void Page_Load(object sender, EventArgs e)
        {
            pinboardOffer = DataObject.Load<DataObjectPinboardOffer>(ObjectID, null, true);

            if (pinboardOffer.State == ObjectState.Added)
            {
                pinboardOffer.ObjectID = ObjectID;
                pinboardOffer.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                pinboardOffer.CommunityID = CommunityID;
                pinboardOffer.ShowState = ObjectShowState.Draft;
                pinboardOffer.Insert(UserDataContext.GetUserDataContext());
                pinboardOffer.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                pinboardOffer.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                pinboardOffer.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                pinboardOffer.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                pinboardOffer.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        pinboardOffer.Geo_Lat = geoLat;
                        pinboardOffer.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                pinboardOffer.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                pinboardOffer.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                pinboardOffer.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                pinboardOffer.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.TxtTitle.Text = pinboardOffer.Title;
            this.TxtDesc.Text = pinboardOffer.Description;
            this.TxtPrice.Text = pinboardOffer.Price;
            if (pinboardOffer.EndDate > this.RDPExpDate.MaxDate)
                this.RDPExpDate.SelectedDate = this.RDPExpDate.MaxDate;
            else
                this.RDPExpDate.SelectedDate = pinboardOffer.EndDate;
            this.HFTagWords.Value = pinboardOffer.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)pinboardOffer.Status).ToString();
            this.HFShowState.Value = ((int)pinboardOffer.ShowState).ToString();
            this.HFCopyright.Value = pinboardOffer.Copyright.ToString();
            if (pinboardOffer.Geo_Lat != double.MinValue && pinboardOffer.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = pinboardOffer.Geo_Lat.ToString();
                this.HFGeoLong.Value = pinboardOffer.Geo_Long.ToString();
            }
            this.HFZip.Value = pinboardOffer.Zip;
            this.HFCity.Value = pinboardOffer.City;
            this.HFStreet.Value = pinboardOffer.Street;
            this.HFCountry.Value = pinboardOffer.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                pinboardOffer.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                pinboardOffer.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                pinboardOffer.Price = Common.Extensions.StripHTMLTags(this.TxtPrice.Text);
                if (this.RDPExpDate.SelectedDate.HasValue)
                    pinboardOffer.EndDate = this.RDPExpDate.SelectedDate.Value;
                pinboardOffer.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                pinboardOffer.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                pinboardOffer.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                pinboardOffer.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    pinboardOffer.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    pinboardOffer.Geo_Long = geoLong;
                pinboardOffer.Zip = this.HFZip.Value;
                pinboardOffer.City = this.HFCity.Value;
                pinboardOffer.Street = this.HFStreet.Value;
                pinboardOffer.CountryCode = this.HFCountry.Value;

                pinboardOffer.Update(UserDataContext.GetUserDataContext());

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