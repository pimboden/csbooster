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
    public partial class Folder : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectFolder folder;

        protected void Page_Load(object sender, EventArgs e)
        {
            folder = DataObject.Load<DataObjectFolder>(ObjectID, null, true);
    
            if (folder.State == ObjectState.Added)
            {
                folder.ObjectID = ObjectID;
                folder.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                folder.CommunityID = CommunityID;
                folder.ShowState = ObjectShowState.Draft;
                folder.Insert(UserDataContext.GetUserDataContext());
                folder.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                folder.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                folder.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                folder.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                folder.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        folder.Geo_Lat = geoLat;
                        folder.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                folder.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                folder.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                folder.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                folder.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.OTOR.ParentObjectID = folder.ObjectID.Value;

            var allFolderObjectTypes =  Helper.GetActiveFolderObjectTypes();
            this.OTOR.ChildObjectTypes = allFolderObjectTypes.ConvertAll(o => o.Id);
            //this.OTOR.LabelText = language.GetString("LabelFolder");

            this.TxtTitle.Text = folder.Title;
            this.TxtDesc.Text = folder.Description;
            this.CBMemberAccess.Checked = folder.AllowMemberEdit;
            this.HFTagWords.Value = folder.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)folder.Status).ToString();
            this.HFShowState.Value = ((int)folder.ShowState).ToString();
            this.HFCopyright.Value = folder.Copyright.ToString();
            if (folder.Geo_Lat != double.MinValue && folder.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = folder.Geo_Lat.ToString();
                this.HFGeoLong.Value = folder.Geo_Long.ToString();
            }
            this.HFZip.Value = folder.Zip;
            this.HFCity.Value = folder.City;
            this.HFStreet.Value = folder.Street;
            this.HFCountry.Value = folder.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                this.OTOR.Save();
                DataObjectList<DataObject> slides = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(), RelationParams = new RelationParams { ParentObjectID = ObjectID, ChildObjectType = Helper.GetObjectTypeNumericID("Picture") } });
                if (slides.Count > 0)
                {
                    folder.Image = slides[0].GetImage(PictureVersion.S);
                    foreach (var pictureFormat in slides[0].PictureFormats)
                        if (!string.IsNullOrEmpty(pictureFormat.Value))
                            folder.SetImageType(pictureFormat.Key, (PictureFormat)Enum.Parse(typeof(PictureFormat), pictureFormat.Value));
                }

                folder.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                folder.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                folder.AllowMemberEdit = this.CBMemberAccess.Checked;
                folder.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                folder.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                folder.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                folder.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    folder.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    folder.Geo_Long = geoLong;
                folder.Zip = this.HFZip.Value;
                folder.City = this.HFCity.Value;
                folder.Street = this.HFStreet.Value;
                folder.CountryCode = this.HFCountry.Value;

                folder.Update(UserDataContext.GetUserDataContext());

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