//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:   #1.0.0.0    05.04.2007 / PT
//******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;

namespace _4screen.CSB.DataAccess.Business
{
    public static class Statistics
    {
        public static StatisticSiteViewResult GetUserSession(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticSiteViewResult result = new StatisticSiteViewResult(null);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.UserSessionLoad(result, from, to, groupBy, granularity, include);
            return result;
        }


        public static StatisticSiteViewResult GetUniqueUser(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticSiteViewResult result = new StatisticSiteViewResult(null);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.UniqueUserLoad(result, from, to, groupBy, granularity, include);
            return result;
        }

        public static StatisticSiteViewResult GetSiteViewDayRange(DateTime from, DateTime to, GroupBy groupBy, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticSiteViewResult result = new StatisticSiteViewResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = Granularity.None;
            result.LabelX = language.GetString("LableTime");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.SiteViewDayRangeLoad(result, from, to, groupBy, communityID, include);
            return result;
        }


        public static StatisticSiteViewResult GetSiteView(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticSiteViewResult result = new StatisticSiteViewResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.SiteViewLoad(result, from, to, groupBy, granularity, communityID, include);
            return result;
        }

        public static StatisticObjectCountResult GetObjectCountCommunityMembers(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCountResult result = new StatisticObjectCountResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCountLoad(result, 1, result.From, result.To, groupBy, granularity, communityID, include);
            return result;
        }

        public static StatisticObjectCountResult GetObjectCountProfileFriends(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCountResult result = new StatisticObjectCountResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCountLoad(result, 2, result.From, result.To, groupBy, granularity, communityID, include);
            return result;
        }

        public static StatisticTotal GetObjectCountCommunityTotal(Guid? parentID, GroupBy groupBy)
        {
            StatisticTotal total = new StatisticTotal(parentID);

            if ((groupBy & GroupBy.None) == GroupBy.None)
            {
                StatisticResult result = GetObjectCountCommunityMembers(DateTime.Now.AddDays(-1).GetStartOfDay(), DateTime.Now, GroupBy.None, Granularity.None, parentID, null);
                result = result.ConvertToSingleSerie();
                total.Results.Add(result);
            }
            if ((groupBy & GroupBy.Sex) == GroupBy.Sex)
            {
                StatisticResult result = GetObjectCountCommunityMembers(DateTime.Now.AddDays(-1).GetStartOfDay(), DateTime.Now, GroupBy.Sex, Granularity.None, parentID, null);
                result = result.ConvertToSingleSerie();
                total.Results.Add(result);
            }
            if ((groupBy & GroupBy.Age) == GroupBy.Age)
            {
                StatisticResult result = GetObjectCountCommunityMembers(DateTime.Now.AddDays(-1).GetStartOfDay(), DateTime.Now, GroupBy.Age, Granularity.None, parentID, null);
                result = result.ConvertToSingleSerie();
                total.Results.Add(result);
            }
            if ((groupBy & GroupBy.Type) == GroupBy.Type)
            {
                total.Results.Add(GetObjectTotalCommunityObjects(parentID, null));
            }

            return total;
        }

        public static StatisticObjectCreationResult GetObjectCreationObjects(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCreationResult result = new StatisticObjectCreationResult(null);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCreationLoad(result, 1, result.From, result.To, groupBy, granularity, null, include);
            return result;

        }

        public static StatisticObjectCreationResult GetObjectCreationCommunityObjects(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCreationResult result = new StatisticObjectCreationResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCreationLoad(result, 2, result.From, result.To, groupBy, granularity, communityID, include);
            return result;

        }

        public static StatisticObjectCreationResult GetObjectCreationProfileObjects(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCreationResult result = new StatisticObjectCreationResult(communityID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCreationLoad(result, 3, result.From, result.To, groupBy, granularity, communityID, include);
            return result;
        }

        public static StatisticObjectCreationResult GetObjectCreationUserObjects(DateTime from, DateTime to, GroupBy groupBy, Granularity granularity, Guid? userID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectCreationResult result = new StatisticObjectCreationResult(userID);
            result.From = from;
            result.To = to;
            result.Granularity = granularity;
            result.LabelX = language.GetString("LableDate");
            result.LabelY = language.GetString("LableCount");
            Data.Statistics.ObjectCreationLoad(result, 4, result.From, result.To, groupBy, granularity, userID, include);
            return result;
        }

        public static StatisticObjectTotalResult GetObjectTotalObjects(List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectTotalResult result = new StatisticObjectTotalResult(null);
            result.From = DateTime.Now.AddYears(-20);
            result.To = DateTime.Now.GetStartOfDay();
            result.Granularity = Granularity.None;
            result.LabelX = language.GetString("LableCount");
            result.LabelY = language.GetString("LableObject");
            Data.Statistics.ObjectTotalLoad(result, 3, result.From, result.To, GroupBy.Type, Granularity.None, null, include);
            return result;
        }

        public static StatisticObjectTotalResult GetObjectTotalCommunityObjects(Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectTotalResult result = new StatisticObjectTotalResult(communityID);
            result.From = DateTime.Now.AddYears(-20);
            result.To = DateTime.Now.GetStartOfDay();
            result.Granularity = Granularity.None;
            result.LabelX = language.GetString("LableCount");
            result.LabelY = language.GetString("LableObject");
            Data.Statistics.ObjectTotalLoad(result, 4, result.From, result.To, GroupBy.Type, Granularity.None, communityID, include);
            return result;
        }

        public static StatisticObjectTotalResult GetObjectTotalProfileObjects(Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectTotalResult result = new StatisticObjectTotalResult(communityID);
            result.From = DateTime.Now.AddYears(-20);
            result.To = DateTime.Now.GetStartOfDay();
            result.Granularity = Granularity.None;
            result.LabelX = language.GetString("LableCount");
            result.LabelY = language.GetString("LableObject");
            Data.Statistics.ObjectTotalLoad(result, 5, result.From, result.To, GroupBy.Type, Granularity.None, communityID, include);
            return result;
        }

        public static StatisticObjectTotalResult GetObjectTotalUserObjects(Guid? communityID, List<object> include)
        {
            GuiLanguage language = GuiLanguage.GetGuiLanguage("DataAccess");
            StatisticObjectTotalResult result = new StatisticObjectTotalResult(communityID);
            result.From = DateTime.Now.AddYears(-20);
            result.To = DateTime.Now.GetStartOfDay();
            result.Granularity = Granularity.None;
            result.LabelX = language.GetString("LableCount");
            result.LabelY = language.GetString("LableObject");
            Data.Statistics.ObjectTotalLoad(result, 6, result.From, result.To, GroupBy.Type, Granularity.None, communityID, include);
            return result;
        }

    }

    public class StatisticTotal
    {
        private List<StatisticResult> results = new List<StatisticResult>();
        internal StatisticTotal(Guid? parentID)
        {
            ParentID = parentID;
        }

        public List<StatisticResult> Results
        {
            get { return results; }
        }

        public Guid? ParentID
        {
            get;
            internal set;
        }
    }

    public class StatisticResult
    {
        private List<StatisticSerie> series = new List<StatisticSerie>();
        internal StatisticResult()
        {
        }

        public List<StatisticSerie> Series
        {
            get { return series; }
        }

        public Granularity Granularity
        {
            get;
            internal set;
        }

        public DateTime From
        {
            get;
            internal set;
        }

        public DateTime To
        {
            get;
            internal set;
        }

        public string LabelX
        {
            get;
            internal set;
        }

        public string LabelY
        {
            get;
            internal set;
        }

        public int Total
        {
            get
            {
                int total = 0;
                series.ForEach(delegate(StatisticSerie serie)
                {
                    total += serie.Total;
                });
                return total;
            }
        }

        public int MaxY
        {
            get
            {
                int maxY = 0;
                series.ForEach(delegate(StatisticSerie serie)
                {
                    maxY = Math.Max(maxY, serie.MaxY);
                });
                return maxY;
            }
        }

        public int MinY
        {
            get
            {
                int minY = 0;
                series.ForEach(delegate(StatisticSerie serie)
                {
                    minY = Math.Min(minY, serie.MaxY);
                });
                return minY;
            }
        }

        public StatisticResult ConvertToSingleSerie()
        {
            StatisticResult result = new StatisticResult();

            if (series.Count > 0)
            {
                result.series = new List<StatisticSerie>();
                result.series.Add(new StatisticSerie() { Label = string.Empty });
                for (int i = 0; i < series.Count; i++)
                {
                    if (series[i].Values.Count > 0)
                    {
                        result.series[0].Values.Add(new StatisticValueStringInt() { X = series[i].Label, Y = ((StatisticValueDateTimeInt)series[i].Values[0]).Y });
                    }
                }
            }

            return result;
        }
    }

    public class StatisticSiteViewResult : StatisticResult
    {
        public StatisticSiteViewResult(Guid? communityID)
            : base()
        {
            CommunityID = communityID;
        }

        public Guid? CommunityID
        {
            get;
            internal set;
        }
    }

    public class StatisticObjectCountResult : StatisticResult
    {
        public StatisticObjectCountResult(Guid? parentID)
            : base()
        {
            ParentID = parentID;
        }

        public Guid? ParentID
        {
            get;
            internal set;
        }
    }

    public class StatisticObjectCreationResult : StatisticResult
    {
        public StatisticObjectCreationResult(Guid? parentID)
            : base()
        {
            ParentID = parentID;
        }

        public Guid? ParentID
        {
            get;
            internal set;
        }
    }

    public class StatisticObjectTotalResult : StatisticResult
    {
        public StatisticObjectTotalResult(Guid? parentID)
            : base()
        {
            ParentID = parentID;
        }

        public Guid? ParentID
        {
            get;
            internal set;
        }
    }

    public class StatisticSerie
    {
        private List<StatisticValue> values = new List<StatisticValue>();
        internal StatisticSerie()
        {
        }

        public List<StatisticValue> Values
        {
            get { return values; }
        }

        public string Label
        {
            get;
            internal set;
        }

        public int MaxY
        {
            get
            {
                int maxY = 0;
                values.ForEach(delegate(StatisticValue value)
                {
                    maxY = Math.Max(maxY, value.Y);
                });
                return maxY;
            }
        }

        public int MinY
        {
            get
            {
                int minY = 0;
                values.ForEach(delegate(StatisticValue value)
                {
                    minY = Math.Min(minY, value.Y);
                });
                return minY;
            }
        }

        public int Total
        {
            get
            {
                int total = 0;
                values.ForEach(delegate(StatisticValue value)
                {
                    total += value.Y;
                });
                return total;
            }
        }
    }

    public class StatisticValue
    {
        internal StatisticValue()
        {
        }

        public int Y
        {
            get;
            internal set;
        }

    }

    public class StatisticValueDateTimeInt : StatisticValue
    {
        public DateTime X
        {
            get;
            internal set;
        }

        public int? AgeAvg
        {
            get;
            internal set;
        }
    }

    public class StatisticValueStringInt : StatisticValue
    {
        public string X
        {
            get;
            internal set;
        }

        public int? AgeAvg
        {
            get;
            internal set;
        }
    }
}