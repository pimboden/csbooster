using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;

namespace _4screen.CSB.Widget
{
    public interface IMapViewerVE
    {
        /// <summary>
        /// ->in Javascript and QueryString:VEMW
        /// Set the Width of the Map Div
        /// </summary>
        Unit MapWidth { get; set; }

        /// <summary>
        /// ->in Javascript and QueryString:VEMH
        /// Set the Width of the Map Div
        /// </summary>
        Unit MapHeight { get; set; }

        /// <summary>
        /// ->in Javascript and QueryString:VEMNW
        /// Set the Width of the Map Div
        /// </summary>
        Unit NavigationPanelWidth { get; set; }

        /// <summary>
        /// ->in Javascript and QueryString:VENC
        /// Defines the Control of the Map
        /// </summary>
        VEMAPNavControl MapNavControl { get; set; }

        /// <summary>
        /// ->in Javascript and QueryString:VEMM
        /// Defines the Avalable Mode(s) of the Map
        /// </summary>
        VEMAPMode MapMode { get; set; }

        /// <summary>
        /// ->in Javascript and QueryString:VEMS
        /// Defines the Style of the Map
        /// </summary>
        VEMAPSytle MapStyle { get; set; }


        /// <summary>
        /// If set to true All Properties may be overwritten by URL. If set to false then the Querystring Variables overwrites the Controls settings
        /// </summary>
        bool OverWriteByURL { get; set; }

        /// <summary>
        /// --> in Javascript and QueryString:VEIPFX
        /// Image Prefix  
        /// </summary>
        string ImagePrefix { get; set; }

        /// <summary>
        /// Image extension --> in Javascript and QueryString:VEIEX
        /// Default = "gif"
        /// </summary>
        string ImageExt { get; set; }

        /// <summary>
        /// --> in Javascript and QueryString:VEPURL
        /// Url of the Pins' Images
        /// Default: /library/images/PlaceOnMap
        /// </summary>
        string PinURL { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:AM
        /// Maximum Resulst schown per layer. 
        /// In Simpleview it shows MaxPerLayer items of each of the  search conditions.
        /// </summary>
        int MaxPerLayer { get; set; }

        /// <summary>
        ///  -->in Javascript and QueryString:VEECSS
        /// Link to an external Stylesheet that may override :
        /// .VEMap
        /// .VELinkImg
        /// .VENav
        ///</summary>
        string ExternalStyleSheet { get; set; } //Link to an external stysheet

        /// <summary>
        /// -->in Javascript and QueryString:TGL1
        /// Tagwords that should be shown in the SearchBy Tag options(Coma separated).
        /// Tagwords that were not found in the db will be ignored.
        /// Enpty means all found tagwords.
        /// </summary>
        string TagWords { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:TGL2
        /// Tagwords that were not found in the db will be ignored.
        /// Enpty means all found tagwords.
        /// </summary>
        string TagWords2 { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:TGL3
        /// Tagwords that were not found in the db will be ignored.
        /// Enpty means all found tagwords.
        /// </summary>
        string TagWords3 { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VESLat
        /// Latitude where the map should center on start.
        /// Default: 46.86770273172813
        /// </summary>
        string StartLat { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VESLong
        /// Logitude where the map should center on start.
        /// Default: 8.3990478515625
        /// </summary>
        string StartLong { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VESZoom
        /// Zoom level that the map should have on start.
        /// Default: 7
        /// </summary>
        string StartZoom { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:CN
        /// Contains the ID of the Datasource:
        /// if DataSource 
        /// . CommunityAndGroups => CommunityId or GroupId,
        /// . SingleCommunity => CommunityId,
        /// .Profile =>  Profile Community ID,
        /// .Site => will be ignored
        /// </summary>
        string SourceID { get; set; }


        /// <summary>
        /// -->in Javascript and QueryString:UI
        /// 
        /// </summary>
        Guid? UserID { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VETOL
        /// Type of the layout:
        ///	SimpleByTag => No navigation is shown... al found items are schown on the map (searches by Tag),
        ///	SimpleByObjectType =>No navigation is shown... al found items are schown on the map(searches by ObjectType)
        ///	SingleLayerdByTag => Navigation is shown. Layers will not overlap. Only one layer at the time will be shown (searches by Tag)
        ///	SingleLayerByObjectType = Navigation is shown. Layers will not overlap. Only one layer at the time will be shown (searches by ObjectType)
        ///	MultiLayerByTag =  Navigation is shown. Layer can be overlaped (searches by Tag)
        ///	MultiLayerByObjectType = Navigation is shown. Layer can be overlaped (searches by ObjectType)
        /// </summary>
        VEMAPVWLayoutType TypeOfLayout { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VEDS
        /// Datasource form where the Data should be searched
        /// </summary>
        WidgetDataSource DataSource { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString: VEOT
        /// Object Types that should be searched for (Coma separated).
        /// On searches by Object Type this list will also influence the Links for the Navigation Pane
        /// </summary>
        string ObjectTypes { get; set; }

        /// <summary>
        /// -->in Javascript and QueryString:VEOID
        /// If Set all the other parameters are ignored and the map returns only the Given Object ID and the map Layout is set to SimpleByObjectType
        /// </summary>
        string SingleObjectID { get; set; }

        bool HasContent { get; set; }
    }
}