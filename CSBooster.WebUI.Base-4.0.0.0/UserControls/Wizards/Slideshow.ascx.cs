// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Slideshow : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectSlideShow slideshow;

        protected void Page_Load(object sender, EventArgs e)
        {
            slideshow = DataObject.Load<DataObjectSlideShow>(ObjectID, null, true);
            foreach (Telerik.Web.UI.RadComboBoxItem item in this.DDEffect.Items)
            {
                item.Text = language.GetString(string.Format("LableEffect{0}", item.Value));
            }

            if (slideshow.State == ObjectState.Added)
            {
                slideshow.ObjectID = ObjectID;
                slideshow.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                slideshow.CommunityID = CommunityID;
                slideshow.ShowState = ObjectShowState.Draft;
                slideshow.Insert(UserDataContext.GetUserDataContext());
                slideshow.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                slideshow.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                slideshow.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                slideshow.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                slideshow.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        slideshow.Geo_Lat = geoLat;
                        slideshow.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                slideshow.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                slideshow.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                slideshow.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                slideshow.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.OTOR.ParentObjectID = slideshow.ObjectID;
            this.OTOR.ChildObjectTypes = new List<string>() { "Picture" };
            //this.OTOR.LabelText = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base").GetString("LabelSlideshow");

            this.TxtTitle.Text = slideshow.Title;
            this.TxtDesc.Text = slideshow.Description;
            this.DDEffect.SelectedValue = slideshow.Effect;
            this.HFTagWords.Value = slideshow.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)slideshow.Status).ToString();
            this.HFShowState.Value = ((int)slideshow.ShowState).ToString();
            this.HFCopyright.Value = slideshow.Copyright.ToString();
            if (slideshow.Geo_Lat != double.MinValue && slideshow.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = slideshow.Geo_Lat.ToString();
                this.HFGeoLong.Value = slideshow.Geo_Long.ToString();
            }
            this.HFZip.Value = slideshow.Zip;
            this.HFCity.Value = slideshow.City;
            this.HFStreet.Value = slideshow.Street;
            this.HFCountry.Value = slideshow.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                this.OTOR.Save();
                DataObjectList<DataObject> slides = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(), RelationParams = new RelationParams { ParentObjectID = ObjectID, ChildObjectType = Helper.GetObjectTypeNumericID("Picture") } });
                if (slides.Count > 0)
                {
                    slideshow.Image = slides[0].GetImage(PictureVersion.S);
                    foreach (var pictureFormat in slides[0].PictureFormats)
                        if (!string.IsNullOrEmpty(pictureFormat.Value))
                            slideshow.SetImageType(pictureFormat.Key, (PictureFormat)Enum.Parse(typeof(PictureFormat), pictureFormat.Value));
                }

                slideshow.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                slideshow.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                slideshow.Effect = this.DDEffect.SelectedValue;
                slideshow.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                slideshow.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                slideshow.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                slideshow.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    slideshow.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    slideshow.Geo_Long = geoLong;
                slideshow.Zip = this.HFZip.Value;
                slideshow.City = this.HFCity.Value;
                slideshow.Street = this.HFStreet.Value;
                slideshow.CountryCode = this.HFCountry.Value;

                slideshow.Update(UserDataContext.GetUserDataContext());

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