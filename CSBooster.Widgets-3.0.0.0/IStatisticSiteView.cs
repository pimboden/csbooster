using System;
using System.Collections.Generic; 
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;

namespace _4screen.CSB.Widget
{
    public interface IStatisticSiteView
    {
        StatisticsType StatisticsType { get; set; }
        int ParentObjectType { get; set; }
        Guid? ParentObjectID { get; set; }
        Guid? UserID { get; set; }
        bool HasContent { get; set; }
        DataRange DataRange { get; set; }
        GroupBy GroupBy { get; set; }
        Granularity Granularity { get; set; }
        ChartType ChartType { get; set; }
        bool ThreeD { get; set; }
        bool IgnoreCommunity { get; set; }
        bool ShowLabel { get; set; }
        string ImageWidth { get; set; }
        List<object> include { get; set; }

    }
}
