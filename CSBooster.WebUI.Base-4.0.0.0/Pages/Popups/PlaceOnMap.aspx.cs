//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.4.0		07.12.2007 / PI
//******************************************************************************

using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using SubSonic;

namespace _4screen.CSB.WebUI.Pages.Popups
{
    public partial class PlaceOnMap : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("Pages.Popups.WebUI.Base");
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        public string GoogleMapKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["GoogleMapKey"]; }
        }

        protected override void OnInit(EventArgs e)
        {
            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.WizardSpezial);

            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference(string.Format("http://maps.google.com/maps/api/js?sensor=false")));

            base.OnInit(e);

            LoadCountries();

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    string objectId = Request.QueryString["OID"];
                    DataObject dataObject = DataObject.Load<DataObject>(objectId.ToGuid(), null, false);
                    if (dataObject.State != ObjectState.Added)
                    {
                        txtPlz.Text = dataObject.Zip;
                        txtOrt.Text = dataObject.City;
                        txtStreet.Text = dataObject.Street;

                        if (!string.IsNullOrEmpty(dataObject.CountryCode))
                            ddlLand.SelectedIndex = ddlLand.Items.IndexOf(ddlLand.Items.FindByValue(dataObject.CountryCode));

                        if (dataObject.Geo_Lat != double.MinValue && dataObject.Geo_Long != double.MinValue)
                        {
                            txtLat.Text = txtLatH.Value = dataObject.Geo_Lat.ToString();
                            txtLong.Text = txtLongH.Value = dataObject.Geo_Long.ToString();
                        }
                    }
                }
                else
                {
                    txtPlz.Text = Request.QueryString["ZP"] == null ? string.Empty : Server.UrlDecode(Request.QueryString["ZP"]);
                    txtOrt.Text = Request.QueryString["CI"] == null ? string.Empty : Server.UrlDecode(Request.QueryString["CI"]);
                    txtStreet.Text = Request.QueryString["RE"] == null ? string.Empty : Server.UrlDecode(Request.QueryString["RE"]);

                    if (!string.IsNullOrEmpty(Request.QueryString["CO"]))
                        ddlLand.SelectedIndex = ddlLand.Items.IndexOf(ddlLand.Items.FindByValue(Request.QueryString["CO"]));

                    string[] geoLatLong = Request.QueryString["GC"] == null ? null : Request.QueryString["GC"].Split(',');
                    if (geoLatLong != null && geoLatLong.Length == 2 && !string.IsNullOrEmpty(geoLatLong[0]) && !string.IsNullOrEmpty(geoLatLong[1]))
                    {
                        txtLat.Text = txtLatH.Value = geoLatLong[0];
                        txtLong.Text = txtLongH.Value = geoLatLong[1];
                    }
                }
            }
            else
            {
                litScript.Text = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        private void LoadCountries()
        {
            IDataReader countries = null;
            try
            {
                ddlLand.Items.Add(new ListItem(language.GetString("LableMapLandCH"), "CH"));
                ddlLand.Items.Add(new ListItem(language.GetString("LableMapLandDE"), "DE"));
                ddlLand.Items.Add(new ListItem(language.GetString("LableMapLandAT"), "AT"));
                ddlLand.Items.Add(new ListItem("---------------------------------", "--"));
                countries = HitblCountryNameCou.FetchByParameter(HitblCountryNameCou.Columns.LangCode, "de-CH", OrderBy.Asc(HitblCountryNameCou.Columns.CountryName));
                while (countries.Read())
                {
                    string countryCode = countries[HitblCountryNameCou.Columns.CountryCode].ToString();
                    string countryName = countries[HitblCountryNameCou.Columns.CountryName].ToString();
                    if (countryCode != "CH" && countryCode != "DE" && countryCode != "AT")
                        ddlLand.Items.Add(new ListItem(countryName, countryCode));
                }
                ddlLand.SelectedIndex = 0;
            }
            finally
            {
                if (countries != null && !countries.IsClosed)
                    countries.Close();
            }
        }

        protected void ibtnS_Click(object sender, EventArgs e)
        {
            string controlId = string.IsNullOrEmpty(Request.QueryString["CtrlID"]) ? string.Empty : Request.QueryString["CtrlID"];
            string geoInfoString = string.Format("{0},{1},{2},{3},{4},{5},{6}", controlId, txtLatH.Value.Replace(',', ' '), txtLongH.Value.Replace(',', ' '), txtPlz.Text.Replace(',', ' '), txtOrt.Text.Replace(',', ' '), txtStreet.Text.Replace(',', ' '), ddlLand.SelectedValue.Replace(',', ' '));

            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
            {
                string objectId = Request.QueryString["OID"];
                DataObject dataObject = DataObject.Load<DataObject>(objectId.ToGuid(), null, false);
                if (dataObject.State != ObjectState.Added)
                {
                    dataObject.City = txtOrt.Text;
                    dataObject.Street = txtStreet.Text;
                    dataObject.CountryCode = ddlLand.SelectedValue;
                    try
                    {
                        dataObject.Geo_Lat = Convert.ToDouble(txtLatH.Value.Replace(',', '.'));
                        dataObject.Geo_Long = Convert.ToDouble(txtLongH.Value.Replace(',', '.'));
                    }
                    catch
                    {
                    }
                    dataObject.Zip = txtPlz.Text;
                    dataObject.Update(UserDataContext.GetUserDataContext());
                    litScript.Text = string.Format("<script language='Javascript'>$telerik.$(function() {{ GetRadWindow().Close('{0}'); }} );</script>", geoInfoString);
                }
            }
            else
            {
                litScript.Text = string.Format("<script language='Javascript'>$telerik.$(function() {{ GetRadWindow().Close('{0}'); }} );</script>", geoInfoString);
            }
        }
    }
}