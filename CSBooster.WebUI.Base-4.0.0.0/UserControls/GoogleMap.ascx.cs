using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;
using Telerik.Web.UI;

namespace _4screen.CSB.WebUI.UserControls
{
    public partial class GoogleMap : System.Web.UI.UserControl, IMap
    {
        public MapLayout MapLayout { get; set; }
        public MapNavigation MapNaviation { get; set; }
        public MapStyle MapStyle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Zoom { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<MapObjectTypeElement> ObjectTypesAndTags { get; set; }
        public Guid? ObjectId { get; set; }
        public QuickParameters QuickParameters { get; set; }
        public bool HasContent { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference(string.Format("http://maps.google.com/maps/api/js?sensor=false")));
            ScriptManager.GetCurrent(Page).Services.Add(new ServiceReference("/Services/GoogleMapServices.asmx"));

            PnlMap.Attributes.Add("style", string.Format("float:left;width:{0}px", Width));
            PnlMap.ID = null;
            LitMap.Text = string.Format("<div id=\"mapCanvas\" style=\"width:{0}px;height:{1}px;\"></div>", Width, Height);

            string onMapLoadScript = string.Empty;
            if (MapLayout == MapLayout.SidebarAndMap)
            {
                LoadSidebar();
                HasContent = true;
                PnlNavi.Visible = true;
                PnlNavi.ID = null;
            }
            else if (MapLayout == MapLayout.MapOnly)
            {
                StringBuilder sb = new StringBuilder();

                if (ObjectId.HasValue || QuickParameters != null)
                {
                    List<DataObject> dataObjects = new List<DataObject>();
                    ObjectTypesAndTags = new List<MapObjectTypeElement>();
                    if (ObjectId.HasValue)
                    {
                        dataObjects.Add(DataObject.Load<DataObject>(ObjectId));
                        if (dataObjects[0].Geo_Lat != double.MinValue)
                        {
                            Latitude = dataObjects[0].Geo_Lat;
                            Longitude = dataObjects[0].Geo_Long;
                            HasContent = true;
                        }
                    }
                    else if (QuickParameters != null)
                    {
                        dataObjects = DataObjects.Load<DataObject>(QuickParameters);
                        if (dataObjects.Count > 0)
                            HasContent = true;
                    }
                    foreach (var dataObject in dataObjects.DistinctBy((x, y) => x.Geo_Lat + x.Geo_Long == y.Geo_Lat + y.Geo_Long))
                    {
                        sb.AppendLine(string.Format("AddMapMarker({0}, {1}, '{2}', '{3}', '{4}');", dataObject.Geo_Lat, dataObject.Geo_Long, string.Empty, dataObject.ObjectID, dataObject.ObjectType));
                        SiteObjectType objectType = Helper.GetObjectType(dataObject.ObjectType);
                        if (!ObjectTypesAndTags.Exists(x => x.Id == objectType.Id))
                            ObjectTypesAndTags.Add(new MapObjectTypeElement() { Id = objectType.Id, IconUrl = objectType.MapIcon });
                    }
                }
                else if (ObjectTypesAndTags != null)
                {
                    foreach (var objectType in ObjectTypesAndTags)
                    {
                        QuickParameters quickParameters = new QuickParameters();
                        quickParameters.Udc = UserDataContext.GetUserDataContext();
                        quickParameters.ObjectType = Common.Helper.GetObjectTypeNumericID(objectType.Id);
                        quickParameters.Tags1 = QuickParameters.GetDelimitedTagIds(string.Join(",", objectType.Tags.LINQEnumarable.Select(x => x.Id).ToArray()), ',');
                        quickParameters.Amount = 500;
                        quickParameters.DisablePaging = true;
                        quickParameters.OnlyGeoTagged = true;
                        quickParameters.ShowState = ObjectShowState.Published;
                        quickParameters.QuerySourceType = QuerySourceType.Page;
                        var dataObjects = DataObjects.Load<DataObject>(quickParameters);
                        if (dataObjects.Count > 0)
                            HasContent = true;
                        foreach (var dataObject in dataObjects.DistinctBy((x, y) => x.Geo_Lat + x.Geo_Long == y.Geo_Lat + y.Geo_Long))
                        {
                            string tag = string.Empty;
                            foreach (var currentTag in objectType.Tags.LINQEnumarable.Select(x => x.Id))
                            {
                                if (!string.IsNullOrEmpty(currentTag) && dataObject.TagList.ToLower().Contains(currentTag.ToLower()))
                                    tag = "_" + currentTag.ToLower();
                            }
                            sb.AppendLine(string.Format("AddMapMarker({0}, {1}, '{2}', '{3}', '{4}');", dataObject.Geo_Lat, dataObject.Geo_Long, tag, dataObject.ObjectID, dataObject.ObjectType));
                        }
                    }
                }

                onMapLoadScript = sb.ToString();
            }

            StringBuilder loadScript = new StringBuilder();
            loadScript.AppendLine("<script type=\"text/javascript\">");
            loadScript.AppendLine("AttachEvent(window, \"load\", initialize);");
            loadScript.AppendLine("var map;");
            loadScript.AppendLine("var mapTree;");
            loadScript.AppendLine("var mapMarkers = new Object();");
            loadScript.AppendLine("var mapMarkerOptions = new Object();");
            loadScript.AppendLine("var mapObjectTypes;");
            loadScript.AppendLine("var mapInfoWindow = new google.maps.InfoWindow();");
            if (MapLayout == MapLayout.SidebarAndMap)
            {
                loadScript.AppendLine("function OnMapNavigationLoad() {");
                loadScript.AppendLine("  mapTree = $find('" + Rtv.ClientID + "');");
                loadScript.AppendLine("  if(map != null) OnMapNavigationChange();");
                loadScript.AppendLine("};");
            }
            loadScript.AppendLine("function initialize() {");
            loadScript.AppendLine("  var mapOptions = {");
            loadScript.AppendLine("    zoom: " + Zoom + ",");
            loadScript.AppendLine("    center: new google.maps.LatLng(" + Latitude + "," + Longitude + "),");
            switch (MapStyle)
            {
                case MapStyle.Road:
                    loadScript.AppendLine("    mapTypeId: google.maps.MapTypeId.ROADMAP,");
                    break;
                case MapStyle.Aerial:
                    loadScript.AppendLine("    mapTypeId: google.maps.MapTypeId.SATELLITE,");
                    break;
                case MapStyle.Hybrid:
                    loadScript.AppendLine("    mapTypeId: google.maps.MapTypeId.HYBRID,");
                    break;
                case MapStyle.Terrain:
                    loadScript.AppendLine("    mapTypeId: google.maps.MapTypeId.TERRAIN,");
                    break;
            }
            switch (MapNaviation)
            {
                case MapNavigation.Small:
                    loadScript.AppendLine("    style: google.maps.NavigationControlStyle.SMALL");
                    break;
                default:
                    loadScript.AppendLine("    style: google.maps.NavigationControlStyle.DEFAULT");
                    break;
            }
            loadScript.AppendLine("  };");
            loadScript.AppendLine("  map = new google.maps.Map(document.getElementById(\"mapCanvas\"), mapOptions);");
            loadScript.AppendLine("  mapMarkerOptions['0'] = { icon: new google.maps.MarkerImage('/library/images/map/default.png', new google.maps.Size(20, 34)), shadow: new google.maps.MarkerImage('/library/images/map/shadow.png', new google.maps.Size(34, 34), new google.maps.Point(0,0), new google.maps.Point(10, 34)) };");
            if (ObjectTypesAndTags != null)
            {
                foreach (var objectType in ObjectTypesAndTags)
                {
                    loadScript.AppendLine("  mapMarkerOptions['" + Helper.GetObjectTypeNumericID(objectType.Id) + "'] = { icon: new google.maps.MarkerImage('" + objectType.IconUrl + "', new google.maps.Size(20, 34)) };");
                    foreach (MapObjectTypeTagElement tag in objectType.Tags)
                    {
                        loadScript.AppendLine("  mapMarkerOptions['" + Helper.GetObjectTypeNumericID(objectType.Id) + "_" + tag.Id.ToLower() + "'] = { icon: new google.maps.MarkerImage('" + tag.IconUrl + "', new google.maps.Size(20, 34)) };");
                    }
                }
                loadScript.AppendLine("  mapObjectTypes = new Array('" + string.Join("', '", ObjectTypesAndTags.ConvertAll(x => x.Id).ToArray()) + "');");
            }
            loadScript.AppendLine("  " + onMapLoadScript);
            if (MapLayout == MapLayout.SidebarAndMap)
                loadScript.AppendLine("  if(mapTree != null) OnMapNavigationChange();");
            loadScript.AppendLine("};");
            loadScript.AppendLine("</script>");
            LitScript.Text = loadScript.ToString();
        }

        private void LoadSidebar()
        {
            foreach (var objectType in ObjectTypesAndTags)
            {
                RadTreeNode objectNode = new RadTreeNode(Helper.GetObjectName(objectType.Id, false), Helper.GetObjectTypeNumericID(objectType.Id).ToString());
                objectNode.ImageUrl = objectType.IconUrl;
                objectNode.Checked = objectType.Selected;
                Rtv.Nodes.Add(objectNode);
                foreach (MapObjectTypeTagElement tag in objectType.Tags)
                {
                    RadTreeNode tagNode = new RadTreeNode(!string.IsNullOrEmpty(tag.Title) ? tag.Title : tag.Id, tag.Id);
                    tagNode.ImageUrl = tag.IconUrl;
                    tagNode.Checked = objectType.Selected | tag.Selected;
                    objectNode.Nodes.Add(tagNode);
                }
            }
            Rtv.ExpandAllNodes();
        }
    }
}
