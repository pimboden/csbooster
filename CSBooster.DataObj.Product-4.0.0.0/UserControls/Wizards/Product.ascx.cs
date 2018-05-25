// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.DataObj.UserControls.Wizards
{
    public partial class Products : _4screen.CSB.DataAccess.Business.StepsASCX
    {
        private Business.DataObjectProduct product;
        protected void Page_Load(object sender, EventArgs e)
        {

            product = _4screen.CSB.DataAccess.Business.DataObject.Load<Business.DataObjectProduct>(ObjectID, null, true);

            if (product.State == ObjectState.Added)
            {
                product.ObjectID = ObjectID;
                product.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                product.CommunityID = CommunityID;
                product.ShowState = ObjectShowState.Draft;
                product.Insert(UserDataContext.GetUserDataContext());
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                product.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                product.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                product.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                product.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        product.Geo_Lat = geoLat;
                        product.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                product.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                product.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                product.Region = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                product.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            TxtTitle.Text = product.Title;
            TxtAbstract.Text = product.Description;
            TxtDesc.Content = product.ProductText;
            txtRef.Text = product.ProductRef;
            txtPrice1.Text = product.Price1.HasValue ? product.Price1.Value.ToString() : string.Empty;
            txtPrice2.Text = product.Price2.HasValue ? product.Price2.Value.ToString() : string.Empty;
            TxtPorto.Text = product.Porto.HasValue ? product.Porto.Value.ToString() : string.Empty;
            RdpStart.SelectedDate = product.StartDate;
            if (product.EndDate < DateTime.MaxValue)
                RdpEnd.SelectedDate = product.EndDate;
            HFTagWords.Value = product.TagList.Replace(Constants.TAG_DELIMITER, ',');
            HFStatus.Value = ((int)product.Status).ToString();
            HFShowState.Value = ((int)product.ShowState).ToString();
            HFCopyright.Value = product.Copyright.ToString();
            if (product.Geo_Lat != double.MinValue && product.Geo_Long != double.MinValue)
            {
                HFGeoLat.Value = product.Geo_Lat.ToString();
                HFGeoLong.Value = product.Geo_Long.ToString();
            }
            HFZip.Value = product.Zip;
            HFCity.Value = product.City;
            HFRegion.Value = product.Region;
            HFCountry.Value = product.CountryCode;

        }


        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                product.Title = Common.Extensions.StripHTMLTags(TxtTitle.Text);
                product.Description = Common.Extensions.StripHTMLTags(TxtAbstract.Text).CropString(20000);
                product.ProductText = TxtDesc.Content;
                product.ProductRef = txtRef.Text;
                double price;
                if (double.TryParse(txtPrice1.Text, out price))
                    product.Price1 = price;
                if (double.TryParse(txtPrice2.Text, out price))
                    product.Price2 = price;
                double porto;
                if (double.TryParse(TxtPorto.Text, out porto))
                    product.Porto = porto;

                if (AccessMode == AccessMode.Insert)
                {
                    product.StartDate = DateTime.Now;
                }
                else
                {
                    if (RdpStart.SelectedDate.HasValue)
                        product.StartDate = RdpStart.SelectedDate.Value;
                    if (RdpEnd.SelectedDate.HasValue)
                        product.EndDate = RdpEnd.SelectedDate.Value;
                }
                product.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                product.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                product.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                product.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    product.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    product.Geo_Long = geoLong;
                product.Zip = this.HFZip.Value;
                product.City = this.HFCity.Value;
                product.Region = this.HFRegion.Value;
                product.CountryCode = this.HFCountry.Value;

                product.Update(UserDataContext.GetUserDataContext());

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