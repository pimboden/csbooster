// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.Widgets.Settings
{
    public partial class GoogleMap : System.Web.UI.UserControl, IWidgetSettings
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetGoogleMap");
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

            foreach (MapElement preset in MapSection.CachedInstance.Maps)
                DdlConfig.Items.Add(new ListItem(preset.Id, preset.Id));

            RblStyle.Items.Add(new ListItem(language.GetString("EnumMapStyle" + MapStyle.Aerial), MapStyle.Aerial.ToString()));
            RblStyle.Items.Add(new ListItem(language.GetString("EnumMapStyle" + MapStyle.Road), MapStyle.Road.ToString()));
            RblStyle.Items.Add(new ListItem(language.GetString("EnumMapStyle" + MapStyle.Hybrid), MapStyle.Hybrid.ToString()));

            RblNavi.Items.Add(new ListItem(language.GetString("EnumMapNavigation" + MapNavigation.None), MapNavigation.None.ToString()));
            RblNavi.Items.Add(new ListItem(language.GetString("EnumMapNavigation" + MapNavigation.Normal), MapNavigation.Normal.ToString()));
            RblNavi.Items.Add(new ListItem(language.GetString("EnumMapNavigation" + MapNavigation.Small), MapNavigation.Small.ToString()));

            if (!IsPostBack)
            {
                CbUrl.Checked = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Url", false);
                DdlConfig.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapPreset", "Default");
                RblStyle.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapStyle", MapStyle.Aerial.ToString());
                RblNavi.SelectedValue = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapNavigation", MapNavigation.Normal.ToString());
                TxtGeoLat.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Latitude", "47.05");
                TxtGeoLong.Text = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Longitude", "8.3");
                string placeOnMapQueryString = string.Format("CtrlID={0}&GC={1},{2}", ClientID, TxtGeoLat.Text, TxtGeoLong.Text);
                LnkOpenMap.Attributes.Add("onClick", string.Format("javascript:radWinOpen('/Pages/Popups/PlaceOnMap.aspx?{0}', 'Karte', 400, 400, false, 'GeoInfoCB', 'mapWin');", placeOnMapQueryString));
                RsZoom.Value = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Zoom", 8);
            }
        }

        public bool Save()
        {
            try
            {
                if (Page.IsValid)
                {
                    XmlDocument xmlDocument = _4screen.CSB.DataAccess.Business.Utils.LoadWidgetInstanceSettings(InstanceId);

                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Url", CbUrl.Checked);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "MapPreset", DdlConfig.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "MapStyle", RblStyle.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "MapNavigation", RblNavi.SelectedValue);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Latitude", TxtGeoLat.Text);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Longitude", TxtGeoLong.Text);
                    XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "Zoom", RsZoom.Value);

                    return _4screen.CSB.DataAccess.Business.Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}