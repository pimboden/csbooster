using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.WebUI
{
    public partial class GoogleMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MapElement mapSettings;
            if (!string.IsNullOrEmpty(Request.QueryString["PID"]))
            {
                MapSection mapSection = new MapSection();
                mapSection.Deserialize(XmlReader.Create(new StringReader(Partner.Get(Request.QueryString["PID"].ToGuid(), null).MapConfig)));
                if (!string.IsNullOrEmpty(Request.QueryString["Preset"]))
                    mapSettings = mapSection.Maps[Request.QueryString["Preset"]];
                else
                    mapSettings = mapSection.Maps.GetItemAt(0);
            }
            else
            {
                mapSettings = MapSection.CachedInstance.Maps.GetItemAt(0);
            }

            IMap map = (IMap)LoadControl("~/UserControls/GoogleMap.ascx");
            map.Width = mapSettings.Width;
            map.Height = mapSettings.Height;
            map.Zoom = mapSettings.Zoom;
            map.Latitude = mapSettings.Latitude;
            map.Longitude = mapSettings.Longitude;
            map.MapLayout = mapSettings.MapLayout;
            map.MapNaviation = mapSettings.MapNavigation;
            map.MapStyle = mapSettings.MapStyle;
            map.ObjectTypesAndTags = mapSettings.ObjectTypes.LINQEnumarable.ToList();
            Ph.Controls.Add((UserControl)map);
        }
    }
}
