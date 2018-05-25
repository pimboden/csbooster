// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public interface IMap
    {
        MapLayout MapLayout { get; set; }
        MapNavigation MapNaviation { get; set; }
        MapStyle MapStyle { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int Zoom { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        List<MapObjectTypeElement> ObjectTypesAndTags { get; set; }
        Guid? ObjectId { get; set; }
        QuickParameters QuickParameters { get; set; }
        bool HasContent { get; set; }
    }
}