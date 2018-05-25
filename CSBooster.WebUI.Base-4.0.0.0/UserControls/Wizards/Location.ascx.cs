// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Location : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private DataObjectLocation location;

        protected void Page_Load(object sender, EventArgs e)
        {
            location = DataAccess.Business.DataObject.Load<DataObjectLocation>(ObjectID, null, true);

            if (location.State == ObjectState.Added)
            {
                location.ObjectID = ObjectID;
                location.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                location.CommunityID = CommunityID;
                location.ShowState = ObjectShowState.Draft;
                location.StartDate = DateTime.Now;
                location.EndDate = DateTime.Now.AddYears(10);
                location.Insert(UserDataContext.GetUserDataContext());
                location.Title = string.Empty;
            }
            location.SetValuesFromQuerySting();

            FillEditForm();

            this.LnkOpenMap.Attributes.Add("onClick", string.Format("OpenGeoTagWindow('{0}', '{1}');", this.ClientID, languageShared.GetString("TitleMap").StripForScript()));
        }

        private void FillEditForm()
        {
            Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, location.GetImage(PictureVersion.S));
            LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin=wizardWin', 'Bild hochladen', 400, 100, false, null, 'imageWin')", location.ObjectID));

            RevWebsite.ValidationExpression = Constants.REGEX_URL;

            if (!IsPostBack)
            {
                TxtTitle.Text = location.Title;
                TxtDesc.Text = location.Description;
                TxtWebsite.Text = location.Website != null ? location.Website.ToString() : string.Empty;
                if (location.Geo_Lat != double.MinValue && location.Geo_Long != double.MinValue)
                {
                    TxtGeoLat.Text = location.Geo_Lat.ToString();
                    TxtGeoLong.Text = location.Geo_Long.ToString();
                }

                FillHiddenValues();
            }

            foreach (string i in Enum.GetNames(typeof(LocationType)))
            {
                string key = string.Format("Text{0}", i);
                string text = languageShared.GetString(key);
                if (!string.IsNullOrEmpty(text))
                {
                    ListItem item = new ListItem(text, i);
                    item.Selected = location.TagList.Contains(i);
                    CblType.Items.Add(item);
                }
            }
        }

        public override bool SaveStep(ref NameValueCollection queryString)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    location.Title = TxtTitle.Text.StripHTMLTags();
                    location.Description = TxtDesc.Text.StripHTMLTags();
                    location.Website = !string.IsNullOrEmpty(TxtWebsite.Text) ? new Uri(TxtWebsite.Text) : null;
                    GetHiddenValues();
                    double geoLat;
                    if (double.TryParse(TxtGeoLat.Text, out geoLat))
                        location.Geo_Lat = geoLat;
                    double geoLong;
                    if (double.TryParse(TxtGeoLong.Text, out geoLong))
                        location.Geo_Long = geoLong;
                    location.Status = ObjectStatus.Public;
                    location.ShowState = ObjectShowState.Published;
                    List<string> tags = new List<string>();
                    for (int i = 0; i < CblType.Items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Request.Form[CblType.UniqueID + "$" + i]))
                            tags.Add(CblType.Items[i].Value);
                    }
                    location.TagList = string.Join(",", tags.ToArray());

                    location.Update(UserDataContext.GetUserDataContext());

                    return true;
                }
                catch (Exception ex)
                {
                    LitMsg.Text = language.GetString("MessageSaveError") + ex.Message;

                }
            }
            return false;
        }

        public DataObjectLocation Save()
        {
            NameValueCollection dummyCollection = new NameValueCollection();
            if (SaveStep(ref dummyCollection))
                return location;
            else
                return null;
        }

        private void GetHiddenValues()
        {
            if (AccessMode == AccessMode.Insert)
                location.StartDate = DateTime.Now;
            location.TagList = HFTagWords.Value.StripHTMLTags();
            location.Status = (ObjectStatus)Enum.Parse(typeof(ObjectStatus), HFStatus.Value);
            location.ShowState = (ObjectShowState)Enum.Parse(typeof(ObjectShowState), HFShowState.Value);
            location.Copyright = int.Parse(HFCopyright.Value);
            double geoLat;
            if (double.TryParse(HFGeoLat.Value, out geoLat))
                location.Geo_Lat = geoLat;
            double geoLong;
            if (double.TryParse(HFGeoLong.Value, out geoLong))
                location.Geo_Long = geoLong;
            location.Zip = HFZip.Value;
            location.City = HFCity.Value;
            location.Street = HFStreet.Value;
            location.CountryCode = HFCountry.Value;
        }

        private void FillHiddenValues()
        {
            HFTagWords.Value = location.TagList.Replace(Constants.TAG_DELIMITER, ',');
            HFStatus.Value = ((int)location.Status).ToString();
            HFShowState.Value = ((int)location.ShowState).ToString();
            HFCopyright.Value = location.Copyright.ToString();
            if (location.Geo_Lat != double.MinValue && location.Geo_Long != double.MinValue)
            {
                HFGeoLat.Value = location.Geo_Lat.ToString();
                HFGeoLong.Value = location.Geo_Long.ToString();
            }
            HFZip.Value = location.Zip;
            HFCity.Value = location.City;
            HFStreet.Value = location.Street;
            HFCountry.Value = location.CountryCode;
        }

        protected void ValidateType(object source, ServerValidateEventArgs e)
        {
            e.IsValid = false;
            for (int i = 0; i < CblType.Items.Count; i++)
            {
                if (!string.IsNullOrEmpty(Request.Form[CblType.UniqueID + "$" + i]))
                    e.IsValid = true;
            }
        }
    }
}