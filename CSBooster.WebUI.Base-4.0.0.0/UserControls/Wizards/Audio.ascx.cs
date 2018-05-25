// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Audio : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        private DataObjectAudio audio;
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

            audio = DataObject.Load<DataObjectAudio>(ObjectID, null, true);
            if (audio.State != ObjectState.Added) // Changing an existing object
            {
                if (tagWords != null) audio.TagList = tagWords;
                if (objectStatus.HasValue) audio.Status = objectStatus.Value;
                if (objectShowState.HasValue) audio.ShowState = objectShowState.Value;
                if (friendType.HasValue) audio.FriendVisibility = friendType.Value;
                if (copyright.HasValue) audio.Copyright = copyright.Value;
                if (uploadSession != null) audio.GroupID = uploadSession;
                if (geoLat != double.MinValue) audio.Geo_Lat = geoLat;
                if (geoLong != double.MinValue) audio.Geo_Long = geoLong;
                if (geoZip != null) audio.Zip = geoZip;
                if (geoCity != null) audio.City = geoCity;
                if (geoStreet != null) audio.Street = geoStreet;
                if (geoCountry != null) audio.CountryCode = geoCountry;

                // Don't save yet - save on SaveStep()

                FillEditForm();
            }
            else if (uploadSession.HasValue) // Creating an new object
            {
                try
                {
                    string cutFileName = FileInfo.Name.Substring(32);
                    string title = cutFileName.Substring(0, cutFileName.LastIndexOf(".")).Replace("_", " ").CropString(100);
                    audio.ObjectID = Guid.NewGuid();
                    audio.Title = title;
                    audio.CommunityID = CommunityID;
                    if (tagWords != null) audio.TagList = tagWords;
                    if (objectStatus.HasValue) audio.Status = objectStatus.Value;
                    if (objectShowState.HasValue) audio.ShowState = objectShowState.Value;
                    if (friendType.HasValue) audio.FriendVisibility = friendType.Value;
                    if (copyright.HasValue) audio.Copyright = copyright.Value;
                    if (uploadSession != null) audio.GroupID = uploadSession;
                    if (geoLat != double.MinValue) audio.Geo_Lat = geoLat;
                    if (geoLong != double.MinValue) audio.Geo_Long = geoLong;
                    if (geoZip != null) audio.Zip = geoZip;
                    if (geoCity != null) audio.City = geoCity;
                    if (geoStreet != null) audio.Street = geoStreet;
                    if (geoCountry != null) audio.CountryCode = geoCountry;

                    string mediaFolder = string.Format(@"{0}\{1}\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], UserProfile.Current.UserId.ToString(), Helper.GetMediaFolder(ObjectType));
                    if (!Directory.Exists(mediaFolder))
                        Directory.CreateDirectory(mediaFolder);
                    string audioFile = string.Format(@"{0}\{1}.mp3", mediaFolder, audio.ObjectID);
                    FileInfo.CopyTo(audioFile);
                    string audioUrl = audioFile.Substring(ConfigurationManager.AppSettings["ConverterRootPathMedia"].Length).Replace("\\", "/");
                    audio.Location = audioUrl;
                    audio.SizeByte = (int)FileInfo.Length;

                    HandleID3Tags(audioFile, audio);
                    try
                    {
                        audio.Insert(UserDataContext.GetUserDataContext());
                    }
                    catch
                    {
                        ClearID3Tags(audio, title);
                        audio.Insert(UserDataContext.GetUserDataContext());
                    }

                    FillEditForm();
                }
                catch (Exception ex)
                {
                    this.LitMsg.Text = string.Format("{0}: ", language.GetString("MessageSaveError")) + ex.Message;
                }
            }
        }

        private void HandleID3Tags(string mp3File, DataObjectAudio audio)
        {
            try
            {
                TagLib.File tagInfo = TagLib.File.Create(mp3File);
                if (!string.IsNullOrEmpty(tagInfo.Tag.FirstPerformer))
                    audio.Interpreter = tagInfo.Tag.FirstPerformer;
                if (!string.IsNullOrEmpty(tagInfo.Tag.Album))
                    audio.Album = tagInfo.Tag.Album;
                if (!string.IsNullOrEmpty(tagInfo.Tag.Title))
                    audio.Title = tagInfo.Tag.Title;
                if (!string.IsNullOrEmpty(tagInfo.Tag.FirstGenre))
                    audio.Genere = tagInfo.Tag.FirstGenre;
                if (tagInfo.Tag.Pictures.Count() > 0)
                {
                    Stream streamPic = new MemoryStream(tagInfo.Tag.Pictures[0].Data.Data, false);
                    string uploadFolder = string.Format(@"{0}\{1}\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathUpload"], UserProfile.Current.UserId.ToString(), Helper.GetMediaFolder(Helper.GetObjectTypeNumericID("Picture")));
                    string imageFile = string.Format(@"{0}\{1}.jpg", uploadFolder, audio.ObjectID);
                    _4screen.CSB.ImageHandler.Business.ImageHandler imageHandler = new _4screen.CSB.ImageHandler.Business.ImageHandler(_4screen.CSB.Common.SiteConfig.MediaDomainName, ConfigurationManager.AppSettings["ConverterRootPath"], UserProfile.Current.UserId.ToString(), audio.ObjectID.Value.ToString(), false, Server.MapPath("/Configurations"));
                    imageHandler.SaveStreamToJpeg(streamPic, imageFile);
                    if (File.Exists(imageFile))
                    {
                        Helper.SetActions(Helper.GetPictureFormatFromFilename(imageFile), audio.ObjectType, out saveOriginalAction, out saveLargeAction, out saveMediumAction, out saveSmallAction, out saveExtraSmallAction, out saveCropAction, imageTypes);

                        imageHandler.DoConvert(imageFile, saveOriginalAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                        string originalImagePath = imageHandler.TargetImage;

                        imageHandler.DoConvert(originalImagePath, saveLargeAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                        if (saveMediumAction != null)
                            imageHandler.DoConvert(originalImagePath, saveMediumAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                        imageHandler.DoConvert(originalImagePath, saveSmallAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);
                        imageHandler.DoConvert(originalImagePath, saveExtraSmallAction, _4screen.CSB.ImageHandler.Business.ImageHandler.ReturnPath.Url);

                        audio.Image = imageHandler.ImageName;
                        foreach (var imageType in imageTypes)
                            audio.SetImageType(imageType.Key, imageType.Value);
                    }
                }
            }
            catch { }
        }

        private void ClearID3Tags(DataObjectAudio audio, string title)
        {
            audio.Title = title;
            audio.Interpreter = string.Empty;
            audio.Album = string.Empty;
            audio.Description = string.Empty;
        }

        private void FillEditForm()
        {
            if (IsSubStep)
            {
                this.PnlGeo.CssClass = "inputBlock";
                this.PnlTagwords.CssClass = "inputBlock";
            }

            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, audio.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin={1}', 'Bild hochladen', 400, 100, false, null, 'imageWin')", audio.ObjectID, "wizardWin"));

            this.TxtTitle.Text = audio.Title;
            this.TxtArtist.Text = audio.Interpreter;
            this.TxtAlbum.Text = audio.Album;
            this.TxtGenre.Text = audio.Genere;
            this.TxtDesc.Text = audio.Description;
            this.TxtTagWords.Text = audio.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)audio.Status).ToString();
            this.HFShowState.Value = ((int)audio.ShowState).ToString();
            this.HFFriendType.Value = ((int)audio.FriendVisibility).ToString();
            this.HFCopyright.Value = audio.Copyright.ToString();
            if (audio.Geo_Lat != double.MinValue && audio.Geo_Long != double.MinValue)
            {
                this.TxtGeoLat.Text = audio.Geo_Lat.ToString();
                this.TxtGeoLong.Text = audio.Geo_Long.ToString();
            }
            this.HFZip.Value = audio.Zip;
            this.HFCity.Value = audio.City;
            this.HFStreet.Value = audio.Street;
            this.HFCountry.Value = audio.CountryCode;

            this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                audio.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                audio.Interpreter = Common.Extensions.StripHTMLTags(this.TxtArtist.Text);
                audio.Album = Common.Extensions.StripHTMLTags(this.TxtAlbum.Text);
                audio.Genere = Common.Extensions.StripHTMLTags(this.TxtGenre.Text);
                audio.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                audio.TagList = Common.Extensions.StripHTMLTags(this.TxtTagWords.Text);
                audio.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                audio.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                audio.FriendVisibility = (FriendType)Enum.Parse(typeof(FriendType), this.HFFriendType.Value);
                audio.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                    audio.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                    audio.Geo_Long = geoLong;
                audio.Zip = this.HFZip.Value;
                audio.City = this.HFCity.Value;
                audio.Street = this.HFStreet.Value;
                audio.CountryCode = this.HFCountry.Value;

                audio.Update(UserDataContext.GetUserDataContext());

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