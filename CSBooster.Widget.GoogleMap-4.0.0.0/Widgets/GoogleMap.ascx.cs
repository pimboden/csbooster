// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Linq;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class GoogleMap : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(settingsXml);
            bool urlOverride = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Url", false);
            string mapPreset = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapPreset", "Default");
            MapStyle mapStyle = (MapStyle)Enum.Parse(typeof(MapStyle), XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapStyle", MapStyle.Aerial.ToString()));
            MapNavigation mapNavigation = (MapNavigation)Enum.Parse(typeof(MapNavigation), XmlHelper.GetElementValue(xmlDocument.DocumentElement, "MapNavigation", MapNavigation.Normal.ToString()));
            double latitude = (double)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Latitude", 47.05m);
            double longitude = (double)XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Longitude", 8.3m);
            int zoom = XmlHelper.GetElementValue(xmlDocument.DocumentElement, "Zoom", 8);

            IMap map = (IMap)LoadControl("~/UserControls/GoogleMap.ascx");
            if (urlOverride)
            {
                map.MapLayout = MapLayout.MapOnly;
                if (!string.IsNullOrEmpty(Request.QueryString["OID"]))
                {
                    map.ObjectId = Request.QueryString["OID"].ToGuid();
                }
                else
                {
                    QuickParameters quickParameters = new QuickParameters();
                    quickParameters.Udc = UserDataContext.GetUserDataContext();
                    quickParameters.QuerySourceType = QuerySourceType.Page;
                    quickParameters.OnlyGeoTagged = true;
                    quickParameters.FromNameValueCollection(Request.QueryString);
                    quickParameters.Amount = 1000;
                    quickParameters.DisablePaging = true;
                    quickParameters.ShowState = ObjectShowState.Published;
                    quickParameters.QuerySourceType = QuerySourceType.Page;
                    map.QuickParameters = quickParameters;
                }
            }
            else
            {
                var mapSettings = MapSection.CachedInstance.Maps[mapPreset];
                map.MapLayout = mapSettings.MapLayout;
                map.ObjectTypesAndTags = mapSettings.ObjectTypes.LINQEnumarable.ToList();
            }
            map.Zoom = zoom;
            map.Latitude = latitude;
            map.Longitude = longitude;
            map.MapNaviation = mapNavigation;
            map.MapStyle = mapStyle;
            int width = map.MapLayout == MapLayout.MapOnly ? WidgetHost.ColumnWidth - WidgetHost.ContentPadding : WidgetHost.ColumnWidth - WidgetHost.ContentPadding - 200;
            map.Width = width;
            map.Height = (int)Math.Round(width * 0.75);
            Ph.Controls.Add((UserControl)map);

            return map.HasContent;
        }
    }
}