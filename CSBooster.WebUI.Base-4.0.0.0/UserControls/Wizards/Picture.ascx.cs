// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Picture : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private DataObjectPicture picture;
        private string saveOriginalAction = string.Empty;
        private string saveExtraSmallAction = string.Empty;
        private string saveSmallAction = string.Empty;
        private string saveMediumAction = string.Empty;
        private string saveLargeAction = string.Empty;
        private string saveCropAction = string.Empty;
        private Dictionary<PictureVersion, PictureFormat> imageTypes = new Dictionary<PictureVersion, PictureFormat>();

        public FileInfo FileInfo { get; set; }
        public bool IsSubStep { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid? uploadSession = null;
            string tagWords = null;
            ObjectStatus? objectStatus = null;
            ObjectShowState? objectShowState = null;
            FriendType? friendType = null;
            int? copyright = null;
            double geoLat = double.MinValue;
            double geoLong = double.MinValue;
            string geoZip = null;
            string geoCity = null;
            string geoStreet = null;
            string geoCountry = null;

            if (!string.IsNullOrEmpty(Request.QueryString["UploadSession"]))
                uploadSession = Request.QueryString["UploadSession"].ToGuid();
            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                tagWords = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                objectStatus = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                objectShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["FT"]))
                friendType = (FriendType)int.Parse(Request.QueryString["FT"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                if (geoLatLong.Length == 2)
                {
                    double.TryParse(geoLatLong[0], out geoLat);
                    double.TryParse(geoLatLong[1], out geoLong);
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                geoZip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                geoCity = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                geoStreet = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                geoCountry = Server.UrlDecode(Request.QueryString["CO"]);

            picture = DataObject.Load<DataObjectPicture>(ObjectID, null, true);

            if (picture.State != ObjectState.Added) // Changing an existing object
            {
                if (tagWords != null) picture.TagList = tagWords;
                if (objectStatus.HasValue) picture.Status = objectStatus.Value;
                if (objectShowState.HasValue) picture.ShowState = objectShowState.Value;
                if (friendType.HasValue) picture.FriendVisibility = friendType.Value;
                if (copyright.HasValue) picture.Copyright = copyright.Value;
                if (uploadSession != null) picture.GroupID = uploadSession;
                if (geoLat != double.MinValue) picture.Geo_Lat = geoLat;
                if (geoLong != double.MinValue) picture.Geo_Long = geoLong;
                if (geoZip != null) picture.Zip = geoZip;
                if (geoCity != null) picture.City = geoCity;
                if (geoStreet != null) picture.Street = geoStreet;
                if (geoCountry != null) picture.CountryCode = geoCountry;

                // Don't save yet - save on SaveStep()

                FillEditForm();
            }
            else if (uploadSession.HasValue) // Creating an new object
            {
                try
                {
                    string cutFileName = FileInfo.Name.Substring(32);
                    picture.ObjectID = Guid.NewGuid();
                    picture.Title = cutFileName.Substring(0, cutFileName.LastIndexOf(".")).Replace("_", " ").CropString(100);
                    picture.CommunityID = CommunityID;
                    if (tagWords != null) picture.TagList = tagWords;
                    if (objectStatus.HasValue) picture.Status = objectStatus.Value;
                    if (objectShowState.HasValue) picture.ShowState = objectShowState.Value;
                    if (friendType.HasValue) picture.FriendVisibility = friendType.Value;
                    if (copyright.HasValue) picture.Copyright = copyright.Value;
                    if (uploadSession != null) picture.GroupID = uploadSession;
                    if (geoLat != double.MinValue) picture.Geo_Lat = geoLat;
                    if (geoLong != double.MinValue) picture.Geo_Long = geoLong;
                    if (geoZip != null) picture.Zip = geoZip;
                    if (geoCity != null) picture.City = geoCity;
                    if (geoStreet != null) picture.Street = geoStreet;
                    if (geoCountry != null) picture.CountryCode = geoCountry;

                    _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], UserProfile.Current.UserId.ToString(), picture.ObjectID.Value.ToString(), false, Server.MapPath("/Configurations"));
                    Helper.SetActions(Helper.GetPictureFormatFromFilename(FileInfo.Name), picture.ObjectType, out saveOriginalAction, out saveLargeAction, out saveMediumAction, out saveSmallAction, out saveExtraSmallAction, out saveCropAction, imageTypes);

                    imageHandler.DoConvert(FileInfo.FullName, saveOriginalAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    string originalImagePath = imageHandler.TargetImage;

                    imageHandler.DoConvert(originalImagePath, saveLargeAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    picture.SizeByte = imageHandler.ImageInfo.SizeDisk;
                    picture.Width = imageHandler.ImageInfo.Width;
                    picture.Height = imageHandler.ImageInfo.Height;

                    if (saveMediumAction != null)
                    imageHandler.DoConvert(originalImagePath, saveMediumAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(originalImagePath, saveSmallAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                    imageHandler.DoConvert(originalImagePath, saveExtraSmallAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

                    picture.Image = imageHandler.ImageName;
                    foreach (var imageType in imageTypes)
                        picture.SetImageType(imageType.Key, imageType.Value);

                    if (picture.Height > 0)
                        picture.AspectRatio = Math.Round((decimal)picture.Width / (decimal)picture.Height, 3);

                    picture.Insert(UserDataContext.GetUserDataContext());

                    FillEditForm();
                }
                catch (Exception ex)
                {
                    this.LitMsg.Text = string.Format("{0}: ", language.GetString("MessageSaveError")) + ex.Message;
                }
            }
        }

        private void FillEditForm()
        {
            if (IsSubStep)
            {
                this.PnlGeo.CssClass = "inputBlock";
                this.PnlTagwords.CssClass = "inputBlock";
            }

            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/CropImage.aspx?OID={0}&ParentRadWin={1}', 'Bild zuschneiden', 400, 100, false, null, 'imageWin')", picture.ObjectID, "wizardWin"));
            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, picture.GetImage(PictureVersion.S));
            this.TxtTitle.Text = picture.Title;
            this.TxtDesc.Text = picture.Description;
            this.TxtTagWords.Text = picture.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)picture.Status).ToString();
            this.HFShowState.Value = ((int)picture.ShowState).ToString();
            this.HFFriendType.Value = ((int)picture.FriendVisibility).ToString();
            this.HFCopyright.Value = picture.Copyright.ToString();
            if (picture.Geo_Lat != double.MinValue && picture.Geo_Long != double.MinValue)
            {
                this.TxtGeoLat.Text = picture.Geo_Lat.ToString();
                this.TxtGeoLong.Text = picture.Geo_Long.ToString();
            }
            this.HFZip.Value = picture.Zip;
            this.HFCity.Value = picture.City;
            this.HFStreet.Value = picture.Street;
            this.HFCountry.Value = picture.CountryCode;

            this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                picture.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                picture.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                picture.TagList = Common.Extensions.StripHTMLTags(this.TxtTagWords.Text);
                picture.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                picture.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                picture.FriendVisibility = (FriendType)Enum.Parse(typeof(FriendType), this.HFFriendType.Value);
                picture.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                    picture.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                    picture.Geo_Long = geoLong;
                picture.Zip = this.HFZip.Value;
                picture.City = this.HFCity.Value;
                picture.Street = this.HFStreet.Value;
                picture.CountryCode = this.HFCountry.Value;

                picture.Update(UserDataContext.GetUserDataContext());

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