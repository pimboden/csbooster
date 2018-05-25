using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess;

namespace _4screen.CSB.Widget
{
    public partial class VEMapViewer : WidgetBase
    {
        public override bool ShowObject(string settingsXml)
        {
            bool HasContent = false;
            try
            {
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.LoadXml(settingsXml);

                Control control = LoadControl("~/UserControls/MapViewerVE.ascx");
                IMapViewerVE mapViewer = (IMapViewerVE) control;
                mapViewer.OverWriteByURL = Convert.ToBoolean(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbOverWriteByURL", 0));

                mapViewer.TagWords = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords", string.Empty);
                mapViewer.TagWords2 = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords2", string.Empty);
                mapViewer.TagWords3 = XmlHelper.GetElementValue(xmlDom.DocumentElement, "tagWords3", string.Empty);
                mapViewer.TypeOfLayout = (VEMAPVWLayoutType) (XmlHelper.GetElementValue(xmlDom.DocumentElement, "layoutType", (int) VEMAPVWLayoutType.MultiLayerByObjectType));
                mapViewer.DataSource = (WidgetDataSource) (XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbDS", (int) WidgetDataSource.CommunityAndGroups));
                mapViewer.SourceID = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ctyID", CommunityID.ToString());
                mapViewer.ObjectTypes = XmlHelper.GetElementValue(xmlDom.DocumentElement, "objTypes", string.Empty);
                mapViewer.UserID = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtUI", SiteContext.UserProfile.UserId.ToString()).ToNullableGuid();
                mapViewer.MaxPerLayer = XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbAnz", 10000);

                string urlECSS = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtECSS", string.Empty);
                if (!string.IsNullOrEmpty(urlECSS))
                {
                    mapViewer.ExternalStyleSheet = urlECSS;
                }

                string urlImg = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtIP", string.Empty);

                if (!string.IsNullOrEmpty(urlImg))
                {
                    mapViewer.PinURL = urlImg;
                }

                mapViewer.ImagePrefix = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtIP", string.Empty);
                mapViewer.ImageExt = XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbPT", "png");
                ;
                mapViewer.StartLat = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLat", "46.86770273172813");
                mapViewer.StartLong = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLong", "8.3990478515625");
                mapViewer.StartZoom = XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtZoom", "7");
                mapViewer.MapHeight = Unit.Parse(XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtMH", "400px"));
                mapViewer.MapWidth = Unit.Parse(XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtMW", "400px"));
                mapViewer.NavigationPanelWidth = Unit.Parse(XmlHelper.GetElementValue(xmlDom.DocumentElement, "txtLPW", "200px"));
                try
                {
                    mapViewer.MapStyle = ((VEMAPSytle) Convert.ToInt32(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbMS", "2")));
                }
                catch
                {
                    mapViewer.MapStyle = VEMAPSytle.Road;
                }
                try
                {
                    mapViewer.MapMode = ((VEMAPMode) Convert.ToInt32(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbMM", "1")));
                }
                catch
                {
                    mapViewer.MapMode = VEMAPMode.Both;
                }
                try
                {
                    mapViewer.MapNavControl = ((VEMAPNavControl) Convert.ToInt32(XmlHelper.GetElementValue(xmlDom.DocumentElement, "rcbNC", "1")));
                }
                catch
                {
                    mapViewer.MapNavControl = VEMAPNavControl.Normal;
                }

                PnlCnt.Controls.Add(control);
                //Its hasrd to define here what is COntent of the Map: Because it might have pins for some layers 
                //but not for others, SO just return true;

                HasContent = true;
            }
            catch
            {
            }
            return HasContent;
        }
    }
}