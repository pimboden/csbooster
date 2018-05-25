// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SiteConfig=_4screen.CSB.Common.SiteConfig;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Generic : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private DataObjectGeneric generic;

        protected void Page_Load(object sender, EventArgs e)
        {
            generic = DataObject.Load<DataObjectGeneric>(ObjectID, null, true);
            if (generic.State == ObjectState.Added)
            {
                generic.ObjectID = ObjectID;
                generic.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                generic.CommunityID = CommunityID;
                generic.ShowState = ObjectShowState.Draft;
                generic.Insert(UserDataContext.GetUserDataContext());
                generic.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                generic.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                generic.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                generic.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                generic.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        generic.Geo_Lat = geoLat;
                        generic.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                generic.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                generic.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                generic.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                generic.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.Img.Src = string.Format("{0}/{1}", SiteConfig.MediaDomainName, generic.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin={1}', 'Bild hochladen', 400, 100, false, null, 'imageWin')", generic.ObjectID, "wizardWin"));

            this.TxtTitle.Text = generic.Title;
            this.TxtDesc.Text = generic.Description;
            this.TxtXml.Text = generic.GenericData;
            this.TxtXsl.Text = generic.UrlXSLT;
            this.HFTagWords.Value = generic.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)generic.Status).ToString();
            this.HFShowState.Value = ((int)generic.ShowState).ToString();
            this.HFCopyright.Value = generic.Copyright.ToString();
            if (generic.Geo_Lat != double.MinValue && generic.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = generic.Geo_Lat.ToString();
                this.HFGeoLong.Value = generic.Geo_Long.ToString();
            }
            this.HFZip.Value = generic.Zip;
            this.HFCity.Value = generic.City;
            this.HFStreet.Value = generic.Street;
            this.HFCountry.Value = generic.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                generic.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                generic.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                generic.GenericData = this.TxtXml.Text;
                generic.UrlXSLT = Common.Extensions.StripHTMLTags(this.TxtXsl.Text);
                generic.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                generic.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                generic.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                generic.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    generic.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    generic.Geo_Long = geoLong;
                generic.Zip = this.HFZip.Value;
                generic.City = this.HFCity.Value;
                generic.Street = this.HFStreet.Value;
                generic.CountryCode = this.HFCountry.Value;

                generic.Update(UserDataContext.GetUserDataContext());

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