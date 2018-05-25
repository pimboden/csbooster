using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.GeoTagging;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.M.Admin
{
    public partial class EditLocation : System.Web.UI.Page
    {
        private bool objectExisting = true;
        private DataObjectLocation location;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Mobile");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (string i in Enum.GetNames(typeof(LocationType)))
            {
                string key = string.Format("Text{0}", i);
                string text = languageShared.GetString(key);
                if (!string.IsNullOrEmpty(text))
                {
                    ListItem item = new ListItem(text, i);
                    CblType.Items.Add(item);
                }
            }

            location = DataObject.Load<DataObjectLocation>(Request.QueryString["OID"].ToNullableGuid());
            if (location.State == ObjectState.Added)
            {
                LitTitle.Text = language.GetString("LabelAddLocation");
                lbtnSave.Text = languageShared.GetString("CommandCreate");
                objectExisting = false;
                location.ObjectID = Guid.NewGuid();
                location.CommunityID = UserProfile.Current.ProfileCommunityID;
                location.ShowState = ObjectShowState.Published;
                location.Status = ObjectStatus.Public;
            }
            else
            {
                LitTitle.Text = language.GetString("LabelEditLocation");
                lbtnSave.Text = languageShared.GetString("CommandSave");
                TxtTitle.Text = location.Title;
                TxtDesc.Text = location.Description.Replace("<br/>", Environment.NewLine);
                TxtStreet.Text = location.Street;
                TxtZipCode.Text = location.Zip;
                TxtCity.Text = location.City;

                foreach (ListItem item in CblType.Items)
                    item.Selected = location.TagList.Contains(item.Value);
            }
        }

        private bool Save()
        {
            try
            {
                List<string> tags = new List<string>();
                foreach (ListItem item in CblType.Items)
                    if (item.Selected)
                        tags.Add(item.Value);

                if (string.IsNullOrEmpty(TxtTitle.Text.StripHTMLTags()))
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageTitleRequired");
                }
                else if (tags.Count == 0)
                {
                    pnlStatus.Visible = true;
                    litStatus.Text = language.GetString("MessageLocationTypeRequired");
                }
                else
                {
                    location.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                    location.Description =
                        TxtDesc.Text.StripHTMLTags().Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "<br/>");
                    location.Copyright = 0;
                    location.Street = Common.Extensions.StripHTMLTags(this.TxtStreet.Text);
                    location.Zip = Common.Extensions.StripHTMLTags(this.TxtZipCode.Text);
                    location.City = Common.Extensions.StripHTMLTags(this.TxtCity.Text);
                    location.TagList = string.Join(",", tags.ToArray());

                    if (!string.IsNullOrEmpty(location.Zip) || !string.IsNullOrEmpty(location.City))
                    {
                        GeoPoint geoPoint = GeoTagging.GeoTagging.GetGeoPosition(location.Street, location.Zip,
                                                                                 location.City, string.Empty,
                                                                                 string.Empty);
                        if (geoPoint != null)
                        {
                            location.Geo_Lat = geoPoint.Lat;
                            location.Geo_Long = geoPoint.Long;
                            location.Zip = geoPoint.ZipCode;
                            location.CountryCode = geoPoint.CountryCode;
                        }
                    }

                    if (objectExisting)
                        location.Update(UserDataContext.GetUserDataContext());
                    else
                        location.Insert(UserDataContext.GetUserDataContext());

                    return true;
                }
            }
            catch (Exception e)
            {
                pnlStatus.Visible = true;
                litStatus.Text = "Event konnte nicht gespeichert werden: " + e.Message;
            }
            return false;
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (Save())
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                    Response.Redirect(System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(Request.QueryString["ReturnURL"])));
                else
                    Response.Redirect(Helper.GetMobileDetailLink(location.ObjectType, location.ObjectID.ToString()));
            }
        }
    }
}
