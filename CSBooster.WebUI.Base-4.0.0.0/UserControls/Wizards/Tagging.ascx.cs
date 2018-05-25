// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Tagging : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private IViewHandler objectViewHandler = null;
        private ITagHandler objectTagHandler = null;
        private DataObject dataObject;

        public string OverrideTagHandlerControl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load values from data object
            if (ObjectID.HasValue)
            {
                dataObject = DataObject.Load<DataObject>(ObjectID, null, true);

                this.Copyright.SelectedValue = dataObject.Copyright.ToString();
                if (dataObject.Geo_Lat != double.MinValue && dataObject.Geo_Long != double.MinValue)
                {
                    this.TxtGeoLat.Text = dataObject.Geo_Lat.ToString();
                    this.TxtGeoLong.Text = dataObject.Geo_Long.ToString();
                }
                this.HFZip.Value = dataObject.Zip;
                this.HFCity.Value = dataObject.City;
                this.HFStreet.Value = dataObject.Street;
                this.HFCountry.Value = dataObject.CountryCode;
            }

            // Overwrite values with query string
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                this.Copyright.SelectedValue = Request.QueryString["CR"];

            string[] geoLatLong = Request.QueryString["GC"] == null ? null : Request.QueryString["GC"].Split(',');
            if (geoLatLong != null && geoLatLong.Length == 2)
            {
                this.TxtGeoLat.Text = geoLatLong[0];
                this.TxtGeoLong.Text = geoLatLong[1];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                this.HFZip.Value = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                this.HFCity.Value = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                this.HFStreet.Value = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                this.HFCountry.Value = Server.UrlDecode(Request.QueryString["CO"]);

            // Load view control
            if (!string.IsNullOrEmpty(Helper.GetObjectType(ObjectType).ViewHandlerCtrl))
            {
                string viewHandlerControl = Helper.GetObjectType(ObjectType).ViewHandlerCtrl;
                objectViewHandler = (IViewHandler)this.LoadControl(viewHandlerControl);
                objectViewHandler.DataObject = dataObject;
                objectViewHandler.ParentDataObject = CommunityID.HasValue ? DataObject.Load<DataObject>(CommunityID, null, true) : null;
                this.PhView.Controls.Add((Control)objectViewHandler);
            }

            // Load tag control
            if (!string.IsNullOrEmpty(Helper.GetObjectType(ObjectType).TagHandlerCtrl))
            {
                string tagHandlerControl = !string.IsNullOrEmpty(OverrideTagHandlerControl) ? OverrideTagHandlerControl : Helper.GetObjectType(ObjectType).TagHandlerCtrl;
                objectTagHandler = (ITagHandler)this.LoadControl(tagHandlerControl);
                if (dataObject != null)
                    objectTagHandler.SetTags(dataObject.TagList);
                this.PhTagging.Controls.Add((Control)objectTagHandler);
            }

            // Load community control
            if (ObjectType == Helper.GetObjectTypeNumericID("Community") && ObjectID.HasValue && !CommunityID.HasValue)
            {
                DataObjectCommunity communtiy = DataObject.Load<DataObjectCommunity>(ObjectID, null, true);
                foreach (Telerik.Web.UI.RadComboBoxItem item in this.RCBCtyGroups.Items)
                    item.Text = language.GetString(string.Format("LableForumRights{0}", item.Value));
                foreach (Telerik.Web.UI.RadComboBoxItem item in this.RCBCtyUpload.Items)
                    item.Text = language.GetString(string.Format("LableForumRights{0}", item.Value));
                this.PnlCtyGroups.Visible = true;
                this.RCBCtyGroups.SelectedValue = ((int)communtiy.CreateGroupUser).ToString();
                this.PnlCtyUpload.Visible = true;
                this.RCBCtyUpload.SelectedValue = ((int)communtiy.UploadUsers).ToString();
            }

            // Load geotagging
            if (CustomizationSection.CachedInstance.Modules["Geotagging"].Enabled && Helper.GetObjectType(ObjectType).IsGeoTaggable)
            {
                PnlGeoTagging.Visible = true;
                this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
            }
        }

        public void Save(DataObject loadedDataObject)
        {
            ObjectStatus objectStatus;
            bool managed = false;
            string selectedRoles = null;
            FriendType friendVisibility = 0;

            if (objectViewHandler != null)
            {
                friendVisibility = objectViewHandler.GetFriendType();
                objectStatus = objectViewHandler.GetObjectStatus();
                selectedRoles = objectViewHandler.GetRoles();
                managed = objectViewHandler.IsManaged();
            }
            else
            {
                objectStatus = loadedDataObject.Status;
                friendVisibility = loadedDataObject.FriendVisibility;
            }

            ObjectShowState objectShowState = ObjectShowState.Published;
            if (loadedDataObject == null || (loadedDataObject.ObjectType != Helper.GetObjectTypeNumericID("ProfileCommunity") && loadedDataObject.ObjectType != Helper.GetObjectTypeNumericID("Page") && loadedDataObject.ObjectType != Helper.GetObjectTypeNumericID("Community")))
                objectShowState = managed ? ObjectShowState.Draft : ObjectShowState.Published;

            string tagWords;
            if (objectTagHandler != null)
                tagWords = objectTagHandler.GetTags();
            else
                tagWords = loadedDataObject.TagList;

            loadedDataObject.TagList = tagWords;
            loadedDataObject.Status = objectStatus;
            loadedDataObject.ShowState = objectShowState;
            loadedDataObject.FriendVisibility = friendVisibility;
            loadedDataObject.Copyright = int.Parse(this.Copyright.SelectedValue);
            if (!string.IsNullOrEmpty(selectedRoles))
            {
                var roles = loadedDataObject.RoleRight.Keys.ToArray<string>();
                foreach (string role in roles)
                {
                    loadedDataObject.RoleRight[role] = false;
                }
                string[] selRoles = selectedRoles.Split(Constants.TAG_DELIMITER);
                for (int i = 0; i < selRoles.Length; i++)
                {
                    loadedDataObject.RoleRight[selRoles[i]] = true;
                }
            }
            double geoLat;
            if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                loadedDataObject.Geo_Lat = geoLat;
            double geoLong;
            if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                loadedDataObject.Geo_Long = geoLong;
            loadedDataObject.Zip = this.HFZip.Value.StripForScript();
            loadedDataObject.City = this.HFCity.Value.StripForScript();
            loadedDataObject.Street = this.HFStreet.Value.StripForScript();
            loadedDataObject.CountryCode = this.HFCountry.Value.StripForScript();
        }

        public override bool SaveStep(ref NameValueCollection queryString)
        {
            ObjectStatus objectStatus = ObjectStatus.Public;
            bool managed = false;
            string selectedRoles = null;
            FriendType friendVisibility = 0;

            if (objectViewHandler != null)
            {
                friendVisibility = objectViewHandler.GetFriendType();
                objectStatus = objectViewHandler.GetObjectStatus();
                selectedRoles = objectViewHandler.GetRoles();
                managed = objectViewHandler.IsManaged();
            }

            ObjectShowState objectShowState = ObjectShowState.Published;
            if (dataObject == null || (dataObject.ObjectType != Helper.GetObjectTypeNumericID("ProfileCommunity") && dataObject.ObjectType != Helper.GetObjectTypeNumericID("Page") && dataObject.ObjectType != Helper.GetObjectTypeNumericID("Community")))
                objectShowState = managed ? ObjectShowState.Draft : ObjectShowState.Published;

            string tagWords = string.Empty;
            if (objectTagHandler != null)
                tagWords = objectTagHandler.GetTags();
            else if (dataObject != null)
                tagWords = dataObject.TagList;

            if (ObjectID.HasValue && dataObject != null && dataObject.State != ObjectState.Added)
            {
                dataObject.TagList = tagWords;
                dataObject.Status = objectStatus;
                dataObject.ShowState = objectShowState;
                dataObject.Copyright = int.Parse(this.Copyright.SelectedValue);
                dataObject.FriendVisibility = friendVisibility;
                if (!string.IsNullOrEmpty(selectedRoles))
                {
                    var roles = dataObject.RoleRight.Keys.ToArray<string>();
                    foreach (string role in roles)
                    {
                        dataObject.RoleRight[role] = false;
                    }
                    string[] selRoles = selectedRoles.Split(Constants.TAG_DELIMITER);
                    for (int i = 0; i < selRoles.Length; i++)
                    {
                        dataObject.RoleRight[selRoles[i]] = true;
                    }
                }
                double geoLat;
                if (double.TryParse(this.TxtGeoLat.Text, out geoLat))
                    dataObject.Geo_Lat = geoLat;
                double geoLong;
                if (double.TryParse(this.TxtGeoLong.Text, out geoLong))
                    dataObject.Geo_Long = geoLong;
                dataObject.Zip = this.HFZip.Value.StripForScript();
                dataObject.City = this.HFCity.Value.StripForScript();
                dataObject.Street = this.HFStreet.Value.StripForScript();
                dataObject.CountryCode = this.HFCountry.Value.StripForScript();
                dataObject.Update(UserDataContext.GetUserDataContext());

                /*DataObjectList<DataObject> relatedObjects = DataObjects.Load<DataObject>(new QuickParameters { Udc = UserDataContext.GetUserDataContext(), QuerySourceType = QuerySourceType.MyContent, IgnoreCache = true, DisablePaging = true, RelationParams = new RelationParams { ParentObjectID = dataObject.ObjectID, ExcludeSystemObjects = true } });
                foreach (DataObject relatedObject in relatedObjects)
                {
                    relatedObject.Status = dataObject.Status;
                    relatedObject.ShowState = dataObject.ShowState;
                    relatedObject.Copyright = dataObject.Copyright;
                    relatedObject.FriendVisibility = dataObject.FriendVisibility;
                    foreach (KeyValuePair<string, bool> roleRight in dataObject.RoleRight)
                    {
                        relatedObject.RoleRight[roleRight.Key] = roleRight.Value;
                    }
                    relatedObject.Update(UserDataContext.GetUserDataContext());
                }*/

                if (ObjectType == Helper.GetObjectTypeNumericID("Community") && ObjectID.HasValue && !CommunityID.HasValue)
                {
                    CommunityUsersType communityUsersType = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), this.RCBCtyGroups.SelectedValue);
                    CommunityUsersType communityUploadUsers = (CommunityUsersType)Enum.Parse(typeof(CommunityUsersType), this.RCBCtyUpload.SelectedValue);
                    DataObjectCommunity communtiy = DataObject.Load<DataObjectCommunity>(ObjectID, null, true);
                    communtiy.CreateGroupUser = communityUsersType;
                    communtiy.UploadUsers = communityUploadUsers;
                    communtiy.Managed = managed;
                    communtiy.Update(UserDataContext.GetUserDataContext());
                }
                return true;
            }
            else
            {
                // If there isn't an upload session and no object id, we habe to create one
                if (string.IsNullOrEmpty(Request.QueryString["UploadSession"]) && !ObjectID.HasValue)
                    queryString.Add("OID", Guid.NewGuid().ToString());

                queryString.Add("TG", Server.UrlEncode(tagWords));
                queryString.Add("OS", ((int)objectStatus).ToString());
                queryString.Add("SS", ((int)objectShowState).ToString());
                queryString.Add("FT", ((int)friendVisibility).ToString());
                queryString.Add("CR", this.Copyright.SelectedValue);
                if (!string.IsNullOrEmpty(selectedRoles))
                {
                    queryString.Add("SR", string.Format("{0}", Server.UrlEncode(selectedRoles)));
                }
                if (!string.IsNullOrEmpty(this.TxtGeoLat.Text) && !string.IsNullOrEmpty(this.TxtGeoLong.Text))
                    queryString.Add("GC", string.Format("{0},{1}", this.TxtGeoLat.Text, this.TxtGeoLong.Text));
                else
                    queryString.Add("GC", string.Empty);
                if (!string.IsNullOrEmpty(this.HFZip.Value))
                    queryString.Add("ZP", string.Format("{0}", Server.UrlEncode(this.HFZip.Value)));
                else
                    queryString.Add("ZP", string.Empty);
                if (!string.IsNullOrEmpty(this.HFCity.Value))
                    queryString.Add("CI", string.Format("{0}", Server.UrlEncode(this.HFCity.Value)));
                else
                    queryString.Add("CI", string.Empty);
                if (!string.IsNullOrEmpty(this.HFStreet.Value))
                    queryString.Add("RE", string.Format("{0}", Server.UrlEncode(this.HFStreet.Value)));
                else
                    queryString.Add("RE", string.Empty);
                if (!string.IsNullOrEmpty(this.HFCountry.Value))
                    queryString.Add("CO", string.Format("{0}", Server.UrlEncode(this.HFCountry.Value)));
                else
                    queryString.Add("CO", string.Empty);
                return true;
            }
        }
    }
}