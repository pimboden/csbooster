// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls.Wizards
{
    public partial class Event : StepsASCX
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Wizards.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");
        private DataObjectEvent dataObjectEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            dataObjectEvent = DataObject.Load<DataObjectEvent>(ObjectID, null, true);

            if (dataObjectEvent.State == ObjectState.Added)
            {
                dataObjectEvent.ObjectID = ObjectID;
                dataObjectEvent.Title = GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUnnamed");
                dataObjectEvent.CommunityID = CommunityID;
                dataObjectEvent.ShowState = ObjectShowState.Draft;
                dataObjectEvent.Insert(UserDataContext.GetUserDataContext());
                dataObjectEvent.Title = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["TG"]))
                dataObjectEvent.TagList = Server.UrlDecode(Request.QueryString["TG"]);
            if (!string.IsNullOrEmpty(Request.QueryString["OS"]))
                dataObjectEvent.Status = (ObjectStatus)int.Parse(Request.QueryString["OS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["SS"]))
                dataObjectEvent.ShowState = (ObjectShowState)int.Parse(Request.QueryString["SS"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CR"]))
                dataObjectEvent.Copyright = int.Parse(Request.QueryString["CR"]);
            if (!string.IsNullOrEmpty(Request.QueryString["GC"]))
            {
                string[] geoLatLong = Request.QueryString["GC"].Split(',');
                double geoLat, geoLong = double.MinValue;
                if (geoLatLong.Length == 2)
                {
                    if (double.TryParse(geoLatLong[0], out geoLat) && double.TryParse(geoLatLong[1], out geoLong))
                    {
                        dataObjectEvent.Geo_Lat = geoLat;
                        dataObjectEvent.Geo_Long = geoLong;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ZP"]))
                dataObjectEvent.Zip = Server.UrlDecode(Request.QueryString["ZP"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CI"]))
                dataObjectEvent.City = Server.UrlDecode(Request.QueryString["CI"]);
            if (!string.IsNullOrEmpty(Request.QueryString["RE"]))
                dataObjectEvent.Street = Server.UrlDecode(Request.QueryString["RE"]);
            if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                dataObjectEvent.CountryCode = Server.UrlDecode(Request.QueryString["CO"]);

            FillEditForm();
        }

        private void FillEditForm()
        {
            this.Img.Src = string.Format("{0}/{1}", _4screen.CSB.Common.SiteConfig.MediaDomainName, dataObjectEvent.GetImage(PictureVersion.S));
            this.LnkImage.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/SinglePictureUpload.aspx?OID={0}&ParentRadWin=wizardWin', 'Bild hochladen', 400, 100, false, null, 'imageWin')", dataObjectEvent.ObjectID));

            RevWebsite.ValidationExpression = Constants.REGEX_URL;

            this.TxtTitle.Text = dataObjectEvent.Title;
            this.TxtDesc.Text = dataObjectEvent.Description;
            this.TxtEvent.Content = dataObjectEvent.Content;
            this.TxtWebsite.Text = dataObjectEvent.Website != null ? dataObjectEvent.Website.ToString() : string.Empty;
            this.HFTagWords.Value = dataObjectEvent.TagList.Replace(Constants.TAG_DELIMITER, ',');
            this.HFStatus.Value = ((int)dataObjectEvent.Status).ToString();
            this.HFShowState.Value = ((int)dataObjectEvent.ShowState).ToString();
            this.HFCopyright.Value = dataObjectEvent.Copyright.ToString();
            if (dataObjectEvent.Geo_Lat != double.MinValue && dataObjectEvent.Geo_Long != double.MinValue)
            {
                this.HFGeoLat.Value = dataObjectEvent.Geo_Lat.ToString();
                this.HFGeoLong.Value = dataObjectEvent.Geo_Long.ToString();
            }
            this.HFZip.Value = dataObjectEvent.Zip;
            this.HFCity.Value = dataObjectEvent.City;
            this.HFStreet.Value = dataObjectEvent.Street;
            this.HFCountry.Value = dataObjectEvent.CountryCode;

            this.RDPFromDate.SelectedDate = dataObjectEvent.StartDate;
            this.RDPToDate.SelectedDate = dataObjectEvent.EndDate == DateTime.MaxValue ? dataObjectEvent.StartDate : dataObjectEvent.EndDate;
            this.TxtTime.Text = dataObjectEvent.Time;
            this.TxtPrice.Text = dataObjectEvent.Price;
            this.TxtAge.Text = dataObjectEvent.Age;

            DataObjectLocation currentLocation = DataObjects.Load<DataObjectLocation>(new QuickParameters()
            {
                Udc = UserDataContext.GetUserDataContext(),
                DisablePaging = true,
                IgnoreCache = true,
                RelationParams = new RelationParams()
                    {
                        ChildObjectID = dataObjectEvent.ObjectID
                    }
            }).SingleOrDefault();

            DDLocations.EmptyMessage = "Location suchen";
            DDLocations.DataTextField = "Title";
            DDLocations.DataValueField = "ObjectID";
            DDLocations.DataSource = DataObjects.Load<DataObjectLocation>(new QuickParameters()
                                                                              {
                                                                                  Udc = UserDataContext.GetUserDataContext(),
                                                                                  SortBy = QuickSort.Title,
                                                                                  Direction = QuickSortDirection.Asc,
                                                                                  DisablePaging = true,
                                                                                  IgnoreCache = true
                                                                              });
            DDLocations.DataBind();
            if (currentLocation != null)
                DDLocations.SelectedValue = currentLocation.ObjectID.ToString();

            LbtnAddLoc.Attributes.Add("onClick", string.Format("radWinOpen('/Pages/Popups/AddLocation.aspx?OID={0}&CN={1}&ParentRadWin=wizardWin', 'Location hinzufügen', 400, 400, false, 'OnLocationAdded', 'locationWin');", Guid.NewGuid(), dataObjectEvent.CommunityID));

            foreach (string i in Enum.GetNames(typeof(EventType)))
            {
                string key = string.Format("Text{0}", i);
                string text = languageShared.GetString(key);
                if (!string.IsNullOrEmpty(text))
                {
                    ListItem item = new ListItem(text, i);
                    item.Selected = dataObjectEvent.TagList.Contains(i);
                    CblType.Items.Add(item);
                }
            }
        }

        public override bool SaveStep(ref System.Collections.Specialized.NameValueCollection queryString)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    dataObjectEvent.Title = Common.Extensions.StripHTMLTags(this.TxtTitle.Text);
                    dataObjectEvent.Description = Common.Extensions.StripHTMLTags(this.TxtDesc.Text).CropString(20000);
                    dataObjectEvent.Content = this.TxtEvent.Content;
                    dataObjectEvent.Website = !string.IsNullOrEmpty(TxtWebsite.Text) ? new Uri(TxtWebsite.Text) : null;
                    dataObjectEvent.TagList = Common.Extensions.StripHTMLTags(this.HFTagWords.Value);
                    dataObjectEvent.Status = ObjectStatus.Public;
                    dataObjectEvent.ShowState = ObjectShowState.Published;
                    dataObjectEvent.Copyright = 0;
                    double geoLat;
                    if (double.TryParse(this.HFGeoLat.Value, out geoLat))
                        dataObjectEvent.Geo_Lat = geoLat;
                    double geoLong;
                    if (double.TryParse(this.HFGeoLong.Value, out geoLong))
                        dataObjectEvent.Geo_Long = geoLong;
                    dataObjectEvent.Zip = this.HFZip.Value;
                    dataObjectEvent.City = this.HFCity.Value;
                    dataObjectEvent.Street = this.HFStreet.Value;
                    dataObjectEvent.CountryCode = this.HFCountry.Value;

                    if (this.RDPFromDate.SelectedDate.HasValue)
                        dataObjectEvent.StartDate = this.RDPFromDate.SelectedDate.Value;
                    dataObjectEvent.EndDate = RDPToDate.SelectedDate.HasValue ? RDPToDate.SelectedDate.Value : dataObjectEvent.StartDate;
                    if (dataObjectEvent.StartDate == dataObjectEvent.EndDate)
                        dataObjectEvent.Featured = 1;
                    else
                        dataObjectEvent.Featured = 2;
                    dataObjectEvent.Time = Common.Extensions.StripHTMLTags(this.TxtTime.Text);
                    dataObjectEvent.Price = Common.Extensions.StripHTMLTags(this.TxtPrice.Text);
                    dataObjectEvent.Age = Common.Extensions.StripHTMLTags(this.TxtAge.Text);
                    dataObjectEvent.Geo_Lat = double.MinValue;
                    dataObjectEvent.Geo_Long = double.MinValue;

                    if (DDLocations.SelectedValue.IsGuid())
                    {
                        DataObjectLocation location = DataObject.Load<DataObjectLocation>(DDLocations.SelectedValue.ToGuid());
                        if (location.Geo_Lat != Double.MinValue && location.Geo_Long != Double.MinValue)
                        {
                            dataObjectEvent.Geo_Lat = location.Geo_Lat;
                            dataObjectEvent.Geo_Long = location.Geo_Long;
                        }
                        DataObject.RelDelete(new RelationParams()
                        {
                            Udc = UserDataContext.GetUserDataContext(),
                            ParentObjectType = location.ObjectType,
                            ChildObjectID = dataObjectEvent.ObjectID,
                            ChildObjectType = dataObjectEvent.ObjectType
                        });
                        DataObject.RelInsert(new RelationParams()
                        {
                            Udc = UserDataContext.GetUserDataContext(),
                            ParentObjectID = location.ObjectID,
                            ParentObjectType = location.ObjectType,
                            ChildObjectID = dataObjectEvent.ObjectID,
                            ChildObjectType = dataObjectEvent.ObjectType
                        }, 0);
                    }
                    List<string> tags = new List<string>();
                    foreach (ListItem item in CblType.Items)
                        if (item.Selected)
                            tags.Add(item.Value);
                    dataObjectEvent.TagList = string.Join(",", tags.ToArray());

                    dataObjectEvent.Update(UserDataContext.GetUserDataContext());

                    return true;
                }
                catch (Exception ex)
                {
                    LitMsg.Text = language.GetString("MessageSaveError") + ex.Message;
                }
            }
            return false;
        }

        protected void ValidateDate(object source, ServerValidateEventArgs e)
        {
            e.IsValid = RDPFromDate.SelectedDate <= RDPToDate.SelectedDate;
        }

        protected void ValidateType(object source, ServerValidateEventArgs e)
        {
            e.IsValid = false;
            foreach (ListItem item in CblType.Items)
            {
                if (item.Selected)
                    e.IsValid = true;
            }
        }
    }
}