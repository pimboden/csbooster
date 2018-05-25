// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Video : StepsASCX
    {
        private DataObjectVideo video;
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");

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

            video = DataObject.Load<DataObjectVideo>(ObjectID, null, true);

            if (video.State != ObjectState.Added) // Changing an existing object
            {
                if (tagWords != null) video.TagList = tagWords;
                if (objectStatus.HasValue) video.Status = objectStatus.Value;
                if (objectShowState.HasValue) video.ShowState = objectShowState.Value;
                if (friendType.HasValue) video.FriendVisibility = friendType.Value;
                if (copyright.HasValue) video.Copyright = copyright.Value;
                if (uploadSession != null) video.GroupID = uploadSession;
                if (geoLat != double.MinValue) video.Geo_Lat = geoLat;
                if (geoLong != double.MinValue) video.Geo_Long = geoLong;
                if (geoZip != null) video.Zip = geoZip;
                if (geoCity != null) video.City = geoCity;
                if (geoStreet != null) video.Street = geoStreet;
                if (geoCountry != null) video.CountryCode = geoCountry;

                // Don't save yet - save on SaveStep()

                FillEditForm();
            }
            else if (uploadSession.HasValue) // Creating an new object
            {
                try
                {
                    string cutFileName = FileInfo.Name.Substring(32);
                    video.ObjectID = Guid.NewGuid();
                    video.Title = cutFileName.Substring(0, cutFileName.LastIndexOf(".")).Replace("_", " ").CropString(100);
                    video.CommunityID = CommunityID;
                    if (tagWords != null) video.TagList = tagWords;
                    if (objectStatus.HasValue) video.Status = objectStatus.Value;
                    video.ShowState = ObjectShowState.InProgress;
                    if (friendType.HasValue) video.FriendVisibility = friendType.Value;
                    if (copyright.HasValue) video.Copyright = copyright.Value;
                    if (uploadSession != null) video.GroupID = uploadSession;
                    if (geoLat != double.MinValue) video.Geo_Lat = geoLat;
                    if (geoLong != double.MinValue) video.Geo_Long = geoLong;
                    if (geoZip != null) video.Zip = geoZip;
                    if (geoCity != null) video.City = geoCity;
                    if (geoStreet != null) video.Street = geoStreet;
                    if (geoCountry != null) video.CountryCode = geoCountry;

                    video.EstimatedWorkTimeSec = (int)Math.Ceiling(GetEstimatedConverterTime(FileInfo.Extension, (double)FileInfo.Length));
                    MembershipUser memUser = Membership.GetUser(UserProfile.Current.UserId);
                    video.UserEmail = memUser.Email;
                    video.SizeByte = (int)FileInfo.Length;
                    video.VideoPreviewPictureTimepointSec = 3;
                    video.OriginalLocation = FileInfo.FullName;

                    video.Insert(UserDataContext.GetUserDataContext(), false);

                    FillEditForm();
                }
                catch (Exception ex)
                {
                    this.LitMsg.Text = "Fehler beim Speichern: " + ex.Message;
                }
            }
        }

        private double GetEstimatedConverterTime(string fileExtension, double fileSize)
        {
            if (HttpRuntime.Cache["LoadCacheConverterTimeEstimation"] == null)
                LoadCacheConverterTimeEstimation(false);

            Dictionary<string, double> collEstimationValues = (Dictionary<string, double>)HttpRuntime.Cache["LoadCacheConverterTimeEstimation"];
            fileExtension = fileExtension.ToLower();
            double dblTimeFor1MB = GetEstimatedConverterTime(collEstimationValues, fileExtension);
            double dblTimeEstimated = (dblTimeFor1MB / 1048576.00) * fileSize;
            return dblTimeEstimated;
        }

        private double GetEstimatedConverterTime(Dictionary<string, double> collEstimationValues, string fileExtension)
        {
            if (collEstimationValues.ContainsKey(fileExtension))
                return collEstimationValues[fileExtension];
            else if (collEstimationValues.ContainsKey("default"))
                return collEstimationValues["default"];
            else
                return 0.0;
        }

        private static void LoadCacheConverterTimeEstimation(bool forceLoad)
        {
            if (forceLoad && HttpRuntime.Cache["LoadCacheConverterTimeEstimation"] != null)
                HttpRuntime.Cache.Remove("LoadCacheConverterTimeEstimation");

            Dictionary<string, double> collEstimationValues = new Dictionary<string, double>();
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.IgnoreWhitespace = true;

            string strFile = string.Format(@"{0}Configurations\ConverterTimeEstimate.config", HttpContext.Current.Request.PhysicalApplicationPath);
            XmlReader xmlr = null;
            try
            {
                xmlr = XmlReader.Create(strFile, xmlSet);
                while (xmlr.Read())
                {
                    if (xmlr.Depth == 1 && xmlr.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            collEstimationValues.Add(xmlr.GetAttribute("Extension").ToLower(), xmlr.ReadElementContentAsDouble());
                        }
                        catch
                        { }
                    }
                }
                System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(strFile);
                HttpRuntime.Cache.Insert("LoadCacheConverterTimeEstimation", collEstimationValues, dep);
            }
            finally
            {
                if (xmlr != null)
                {
                    xmlr.Close();
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
            if (video.ShowState != ObjectShowState.InProgress && video.ShowState != ObjectShowState.ConversionFailed)
            {
                this.PnlImg.Visible = true;
            }
            if (this.AccessMode == AccessMode.Insert)
            {
                this.PnlPreview.Visible = true;
            }

            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, video.GetImage(PictureVersion.S));
            this.TxtTitle.Text = video.Title;
            this.TxtDesc.Text = video.Description;
            this.RntbPreview.Value = video.VideoPreviewPictureTimepointSec > 0 ? video.VideoPreviewPictureTimepointSec : 3;
            this.TxtTagWords.Text = video.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)video.Status).ToString();
            this.HFShowState.Value = ((int)video.ShowState).ToString();
            this.HFFriendType.Value = ((int)video.FriendVisibility).ToString();
            this.HFCopyright.Value = video.Copyright.ToString();
            if (video.Geo_Lat != double.MinValue && video.Geo_Long != double.MinValue)
            {
                this.TxtGeoLat.Text = video.Geo_Lat.ToString();
                this.TxtGeoLong.Text = video.Geo_Long.ToString();
            }
            this.HFZip.Value = video.Zip;
            this.HFCity.Value = video.City;
            this.HFStreet.Value = video.Street;
            this.HFCountry.Value = video.CountryCode;

            this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                video.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                video.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                video.VideoPreviewPictureTimepointSec = this.RntbPreview.Value.Value;
                video.TagList = Common.Extensions.StripHTMLTags(this.TxtTagWords.Text);
                video.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                if (AccessMode == AccessMode.Insert)
                    video.ShowState = ObjectShowState.InProgress;
                if (AccessMode == AccessMode.Update && video.ShowState == ObjectShowState.Draft || video.ShowState == ObjectShowState.Published)
                    video.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                video.FriendVisibility = (FriendType)Enum.Parse(typeof(FriendType), this.HFFriendType.Value);
                video.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                    video.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                    video.Geo_Long = geoLong;
                video.Zip = this.HFZip.Value;
                video.City = this.HFCity.Value;
                video.Street = this.HFStreet.Value;
                video.CountryCode = this.HFCountry.Value;
                MembershipUser memUser = Membership.GetUser(UserProfile.Current.UserId);
                video.UserEmail = memUser.Email;

                video.Update(UserDataContext.GetUserDataContext());
                if (AccessMode == AccessMode.Insert)
                {
                    video.AddToConverterQueue();
                }

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