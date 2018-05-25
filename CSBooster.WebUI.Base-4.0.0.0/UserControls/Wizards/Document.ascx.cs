// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Configuration;
using System.IO;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Document : StepsASCX
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");

        private DataObjectDocument document;

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

            document = DataObject.Load<DataObjectDocument>(ObjectID, null, true);

            if (document.State != ObjectState.Added) // Changing an existing object
            {
                if (tagWords != null) document.TagList = tagWords;
                if (objectStatus.HasValue) document.Status = objectStatus.Value;
                if (objectShowState.HasValue) document.ShowState = objectShowState.Value;
                if (friendType.HasValue) document.FriendVisibility = friendType.Value;
                if (copyright.HasValue) document.Copyright = copyright.Value;
                if (uploadSession != null) document.GroupID = uploadSession;
                if (geoLat != double.MinValue) document.Geo_Lat = geoLat;
                if (geoLong != double.MinValue) document.Geo_Long = geoLong;
                if (geoZip != null) document.Zip = geoZip;
                if (geoCity != null) document.City = geoCity;
                if (geoStreet != null) document.Street = geoStreet;
                if (geoCountry != null) document.CountryCode = geoCountry;

                // Don't save yet - save on SaveStep()

                FillEditForm();
            }
            else if (uploadSession.HasValue) // Creating an new object
            {
                try
                {
                    string cutFileName = FileInfo.Name.Substring(32);
                    document.Title = cutFileName.Substring(0, cutFileName.LastIndexOf(".")).Replace("_", " ").CropString(100);
                    document.CommunityID = CommunityID;
                    if (tagWords != null) document.TagList = tagWords;
                    if (objectStatus.HasValue) document.Status = objectStatus.Value;
                    if (objectShowState.HasValue) document.ShowState = objectShowState.Value;
                    if (friendType.HasValue) document.FriendVisibility = friendType.Value;
                    if (copyright.HasValue) document.Copyright = copyright.Value;
                    if (uploadSession != null) document.GroupID = uploadSession;
                    if (geoLat != double.MinValue) document.Geo_Lat = geoLat;
                    if (geoLong != double.MinValue) document.Geo_Long = geoLong;
                    if (geoZip != null) document.Zip = geoZip;
                    if (geoCity != null) document.City = geoCity;
                    if (geoStreet != null) document.Street = geoStreet;
                    if (geoCountry != null) document.CountryCode = geoCountry;
               
                    string mediaFolder = string.Format(@"{0}\{1}\{2}", System.Configuration.ConfigurationManager.AppSettings["ConverterRootPathMedia"], UserProfile.Current.UserId.ToString(), Helper.GetMediaFolder(ObjectType));
                    if (!Directory.Exists(mediaFolder))
                        Directory.CreateDirectory(mediaFolder);
                    string documentFile = string.Format(@"{0}\{1}", mediaFolder, FileInfo.Name);
                    FileInfo.CopyTo(documentFile);
                    string documentUrl = documentFile.Substring(ConfigurationManager.AppSettings["ConverterRootPathMedia"].Length).Replace("\\", "/");
                    document.URLDocument = documentUrl;
                    document.SizeByte = FileInfo.Length;

                    document.Insert(UserDataContext.GetUserDataContext());

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

            this.TxtTitle.Text = document.Title;
            this.TxtDesc.Text = document.Description;
            this.TxtAuthor.Text = document.Author;
            this.TxtVersion.Text = document.Version;
            this.TxtTagWords.Text = document.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)document.Status).ToString();
            this.HFShowState.Value = ((int)document.ShowState).ToString();
            this.HFFriendType.Value = ((int)document.FriendVisibility).ToString();
            this.HFCopyright.Value = document.Copyright.ToString();
            if (document.Geo_Lat != double.MinValue && document.Geo_Long != double.MinValue)
            {
                this.TxtGeoLat.Text = document.Geo_Lat.ToString();
                this.TxtGeoLong.Text = document.Geo_Long.ToString();
            }
            this.HFZip.Value = document.Zip;
            this.HFCity.Value = document.City;
            this.HFStreet.Value = document.Street;
            this.HFCountry.Value = document.CountryCode;

            this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                document.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                document.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                document.Author = Common.Extensions.StripHTMLTags(this.TxtAuthor.Text);
                document.Version = Common.Extensions.StripHTMLTags(this.TxtVersion.Text);
                document.TagList = Common.Extensions.StripHTMLTags(this.TxtTagWords.Text);
                document.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                document.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                document.FriendVisibility = (FriendType)Enum.Parse(typeof(FriendType), this.HFFriendType.Value);
                document.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                    document.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                    document.Geo_Long = geoLong;
                document.Zip = this.HFZip.Value;
                document.City = this.HFCity.Value;
                document.Street = this.HFStreet.Value;
                document.CountryCode = this.HFCountry.Value;

                document.Update(UserDataContext.GetUserDataContext());

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