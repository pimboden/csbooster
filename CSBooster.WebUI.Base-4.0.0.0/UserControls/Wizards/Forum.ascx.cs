// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Forum : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");
        private DataObjectForum forum;

        protected void Page_Load(object sender, EventArgs e)
        {
            forum = DataObject.Load<DataObjectForum>(ObjectID, null, true);
            foreach (RadComboBoxItem item in this.RcbRights.Items)
            {
                item.Text = language.GetString(string.Format("LableForumRights{0}", item.Value));
            }

            if (forum.State == ObjectState.Added)
            {
                forum.ObjectID = ObjectID;
                forum.Title = languageShared.GetString("LabelUnnamed");
                forum.CommunityID = CommunityID;
                forum.ShowState = ObjectShowState.InProgress;
                forum.Insert(UserDataContext.GetUserDataContext());
                forum.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                forum.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                forum.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                forum.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                forum.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        forum.Geo_Lat = geoLat;
                        forum.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                forum.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                forum.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                forum.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                forum.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, forum.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin={1}', 'Bild hochladen', 400, 100, false, null, 'imageWin')", forum.ObjectID, "wizardWin"));

            this.TxtTitle.Text = forum.Title;
            this.TxtDesc.Content = forum.Description;
            this.RcbRights.SelectedValue = ((int)forum.ThreadCreationUsers).ToString();
            foreach (ListItem item in this.CblUserVisibliity.Items)
            {
                CommunityUsersType enu = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), item.Value);
                item.Text = languageDataAccess.GetString(string.Format("EnumCommunityUsersType{0}", enu.ToString()));
                item.Selected = forum.ShowCommunityUsersType(enu);
            }
            this.CbxCharied.Checked = forum.ShowChaired;

            this.HFTagWords.Value = forum.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)forum.Status).ToString();
            this.HFShowState.Value = ((int)forum.ShowState).ToString();
            this.HFCopyright.Value = forum.Copyright.ToString();
            if (forum.Geo_Lat != double.MinValue && forum.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = forum.Geo_Lat.ToString();
                this.HFGeoLong.Value = forum.Geo_Long.ToString();
            }
            this.HFZip.Value = forum.Zip;
            this.HFCity.Value = forum.City;
            this.HFStreet.Value = forum.Street;
            this.HFCountry.Value = forum.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                forum.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                forum.Description = this.TxtDesc.Content;
                forum.ThreadCreationUsers = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), this.RcbRights.SelectedValue);
                foreach (ListItem item in this.CblUserVisibliity.Items)
                {
                    CommunityUsersType enu = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), item.Value);
                    forum.ShowCommunityUsersType(enu, item.Selected);
                }
                forum.ShowChaired = CbxCharied.Checked;

                forum.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                forum.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                forum.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                forum.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    forum.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    forum.Geo_Long = geoLong;
                forum.Zip = this.HFZip.Value;
                forum.City = this.HFCity.Value;
                forum.Street = this.HFStreet.Value;
                forum.CountryCode = this.HFCountry.Value;

                forum.Update(UserDataContext.GetUserDataContext());

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