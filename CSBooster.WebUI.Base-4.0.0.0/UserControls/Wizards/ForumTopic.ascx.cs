// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class ForumTopic : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        private DataObjectForumTopic forumTopic;

        protected void Page_Load(object sender, EventArgs e)
        {
            forumTopic = DataObject.Load<DataObjectForumTopic>(ObjectID, null, true);
            foreach (RadComboBoxItem item in this.RcbRights.Items)
            {
                item.Text = language.GetString(string.Format("LableForumRights{0}", item.Value));
            }

            if (forumTopic.State == ObjectState.Added)
            {
                forumTopic.ObjectID = ObjectID;
                forumTopic.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                forumTopic.CommunityID = CommunityID;
                forumTopic.ShowState = ObjectShowState.InProgress;
                forumTopic.Status = ObjectStatus.Public;
                forumTopic.TopicItemCreationUsers = CommunityUsersType.Authenticated;
                forumTopic.Insert(UserDataContext.GetUserDataContext(), Request.QueryString["FID"].ToGuid(), Helper.GetObjectTypeNumericID("Forum"));
                forumTopic.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                forumTopic.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                forumTopic.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                forumTopic.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                forumTopic.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        forumTopic.Geo_Lat = geoLat;
                        forumTopic.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                forumTopic.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                forumTopic.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                forumTopic.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                forumTopic.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.TxtTitle.Text = forumTopic.Title;
            this.TxtDesc.Content = forumTopic.Description;
            this.CbModerated.Checked = forumTopic.IsModerated;
            this.RcbRights.SelectedValue = ((int)forumTopic.TopicItemCreationUsers).ToString();
            this.HFTagWords.Value = forumTopic.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)forumTopic.Status).ToString();
            this.HFShowState.Value = ((int)forumTopic.ShowState).ToString();
            this.HFCopyright.Value = forumTopic.Copyright.ToString();
            if (forumTopic.Geo_Lat != double.MinValue && forumTopic.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = forumTopic.Geo_Lat.ToString();
                this.HFGeoLong.Value = forumTopic.Geo_Long.ToString();
            }
            this.HFZip.Value = forumTopic.Zip;
            this.HFCity.Value = forumTopic.City;
            this.HFStreet.Value = forumTopic.Street;
            this.HFCountry.Value = forumTopic.CountryCode;
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            try
            {
                forumTopic.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                forumTopic.Description = this.TxtDesc.Content;
                forumTopic.IsModerated = CbModerated.Checked;
                forumTopic.TopicItemCreationUsers = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), this.RcbRights.SelectedValue);
                forumTopic.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                forumTopic.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), this.HFStatus.Value);
                if(AccessMode == AccessMode.Insert)
                    forumTopic.ShowState = ObjectShowState.Published;
                else
                    forumTopic.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), this.HFShowState.Value);
                forumTopic.Copyright = int.Parse(this.HFCopyright.Value);
                double geoLat;
                if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                    forumTopic.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                    forumTopic.Geo_Long = geoLong;
                forumTopic.Zip = this.HFZip.Value;
                forumTopic.City = this.HFCity.Value;
                forumTopic.Street = this.HFStreet.Value;
                forumTopic.CountryCode = this.HFCountry.Value;

                forumTopic.Update(UserDataContext.GetUserDataContext());

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