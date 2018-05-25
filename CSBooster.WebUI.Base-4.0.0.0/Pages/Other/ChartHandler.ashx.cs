using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;
using OpenFlashChartLib;
using OpenFlashChartLib.Charts;
using ChartType = _4screen.CSB.Common.Statistic.ChartType;

namespace _4screen.CSB.WebUI.Pages.Other
{
    public class ChartHandler : IHttpHandler
    {
        private GuiLanguage language = null;
        private StatisticsType statisticsType = StatisticsType.PageView;
        private ChartType chartType = ChartType.Line;
        private GroupBy groupBy = Common.Statistic.GroupBy.None;
        private bool granularityAuto = false;
        private bool ignoreCommunity = false;
        private int parentObjectType = 0;
        private Guid? parentObjectID = null;
        private Guid? userID = null;
        private bool threeD = false;
        private bool showLabel = true;
        private List<object> include = null;
        private DateTime dateTo;
        private DateTime dateFrom;
        private string chartId;
        private string errorId;
        private string langCode = "de-CH";

        private int imageWidth = 200;
        private int LABLE_SPACE = 22;
        private int POINT_SPACE = 4;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                dateTo = DateTime.Now.GetEndOfMonth();
                dateFrom = dateTo.GetStartOfMonth();

                if (!string.IsNullOrEmpty(context.Request.QueryString["ChartId"]))
                    chartId = context.Request.QueryString["ChartId"];

                if (!string.IsNullOrEmpty(context.Request.QueryString["ErrorId"]))
                    errorId = context.Request.QueryString["ErrorId"];

                if (!string.IsNullOrEmpty(context.Request.QueryString["StatisticsType"]))
                    statisticsType = (StatisticsType)int.Parse(context.Request.QueryString["StatisticsType"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["DateFrom"]))
                    dateFrom = DateTime.Parse(context.Request.QueryString["DateFrom"]).GetStartOfDay();

                if (!string.IsNullOrEmpty(context.Request.QueryString["DateTo"]))
                    dateTo = DateTime.Parse(context.Request.QueryString["DateTo"]).GetEndOfDay();

                if (!string.IsNullOrEmpty(context.Request.QueryString["ChartType"]))
                    chartType = (ChartType)int.Parse(context.Request.QueryString["ChartType"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["GroupBy"]))
                    groupBy = (GroupBy)int.Parse(context.Request.QueryString["GroupBy"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["UserID"]))
                    userID = context.Request.QueryString["UserID"].ToGuid();

                if (!string.IsNullOrEmpty(context.Request.QueryString["ParentObjectID"]))
                    parentObjectID = context.Request.QueryString["ParentObjectID"].ToGuid();

                if (!string.IsNullOrEmpty(context.Request.QueryString["ParentObjectType"]))
                    parentObjectType = int.Parse(context.Request.QueryString["ParentObjectType"]);

                if (parentObjectType == 1 || parentObjectType == 19)
                    ignoreCommunity = false;
                else
                    ignoreCommunity = true;

                if (!string.IsNullOrEmpty(context.Request.QueryString["ThreeD"]))
                    threeD = bool.Parse(context.Request.QueryString["ThreeD"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["ShowLabel"]))
                    showLabel = bool.Parse(context.Request.QueryString["ShowLabel"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["Incl"]))
                {
                    string[] s = context.Request.QueryString.GetValues("Incl");
                    include = new List<object>(s.Length);
                    for (int i = 0; i < s.Length; i++)
                        include.Add(s[i]);
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["ImageWidth"]))
                    imageWidth = int.Parse(context.Request.QueryString["ImageWidth"]);

                if (!string.IsNullOrEmpty(context.Request.QueryString["LG"]))
                    langCode = context.Request.QueryString["LG"];

                language = GuiLanguage.GetGuiLanguage("DataAccess", langCode);
                // already done by global.asax
                //try
                //{
                //    System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(langCode);
                //    System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
                //    System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
                //}
                //catch
                //{ 
                //    //
                //}

                if (statisticsType == StatisticsType.PageView)
                    PageView(context);
                else if (statisticsType == StatisticsType.PageViewDayRange)
                    PageViewDayRange(context);
                else if (statisticsType == StatisticsType.UniqueUser)
                    UniqueUser(context);
                else if (statisticsType == StatisticsType.UserSession)
                    UserSession(context);
                else
                {
                    context.Response.StatusCode = 501;
                    context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));
                    context.Response.Write(language.GetString("MessageStatisticNotImplemented"));

                    //AllOther(context);
                }
            }
            catch (Exception exc)
            {
                LogManager.WriteEntry(exc);

                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));
                context.Response.Write(language.GetString("MessageStatisticInternalError"));
            }
        }

        private void UserSession(HttpContext context)
        {
            Granularity granularity = Granularity.Day;

            int estPoints = GetRangeDays();
            int maxPoints = imageWidth / POINT_SPACE;
            int step = 1;
            int maxSteps = imageWidth / LABLE_SPACE;

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeMonths();
                granularity = Granularity.Month;
            }

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeYears();
                granularity = Granularity.Year;
            }

            while (estPoints > maxSteps)
            {
                estPoints = estPoints / 2;
                step++;
            }

            StatisticResult result = Statistics.GetUserSession(dateFrom, dateTo, groupBy, granularity, include);

            if (result.Series.Count < 1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));
                context.Response.Write(language.GetString("MessageStatisticNoData"));
                return;
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            DateTime lastDate = DateTime.MinValue;

            List<string> colors = GetColors(result.Series.Count);

            Graph graph = new Graph();

            if (!string.IsNullOrEmpty(result.LabelX))
            {
                Legend xLegend = new Legend();
                xLegend.Text = result.LabelX;
                xLegend.Style = "color: #444444; font-size: 12px;";
                graph.X_Legend = xLegend;
            }

            if (!string.IsNullOrEmpty(result.LabelY))
            {
                Legend yLegend = new Legend();
                yLegend.Text = result.LabelY;
                yLegend.Style = "color: #444444; font-size: 12px;";
                graph.Y_Legend = yLegend;
            }

            XAxis xAxis = new XAxis();
            xAxis.Colour = "#444444";
            xAxis.GridColour = "#CCCCCC";
            xAxis.Steps = step;
            graph.X_Axis = xAxis;

            YAxis yAxis = new YAxis();
            yAxis.Colour = "#444444";
            yAxis.GridColour = "#CCCCCC";
            int y = result.MaxY - result.MinY;
            yAxis.Steps = y / 5;
            graph.Y_Axis = yAxis;

            string toolTip = language.GetString("LableStatisticCalls");

            for (int i = 0; i < result.Series.Count; i++)
            {
                ChartBase chart = new Line();
                chart.Colour = colors[i];
                //chart.Text = result.Series[i].Label.ToLowerInvariant();
                chart.Text = result.Series[i].Label;

                chart.Tooltip = string.Format("#val# {0} ({1})", toolTip, chart.Text);
                chart.Fontsize = 9;

                for (int j = 0; j < result.Series[i].Values.Count; j++)
                {
                    StatisticValueDateTimeInt value = (StatisticValueDateTimeInt)result.Series[i].Values[j];

                    if (lastDate == DateTime.MinValue)
                        lastDate = value.X;

                    if (value.X > maxDate)
                        maxDate = value.X;

                    if (value.X < minDate)
                        minDate = value.X;

                    string xLabel = string.Empty;
                    if (j % xAxis.Steps == 0)
                    {
                        if (granularity == Granularity.Year)
                        {
                            xLabel = value.X.ToString("yyyy");
                        }
                        else if (granularity == Granularity.Month)
                        {
                            if (lastDate.Year != value.X.Year || value.X.Month == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("MMM. yy");
                            }
                            else
                            {
                                xLabel = value.X.ToString("MMM.");
                            }
                        }
                        else if (granularity == Granularity.Day)
                        {
                            if (lastDate.Year != value.X.Year || lastDate.Month != value.X.Month || value.X.Day == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("d.M.");
                            }
                            else
                            {
                                xLabel = value.X.ToString("d.");
                            }
                        }
                    }

                    var val = chart.NewValue(xLabel, value.Y);
                    chart.Values.Add(val);
                }

                graph.AddElement(chart);
            }

            graph.Title = new Title(GetTitle(granularity));
            graph.Bgcolor = "#FFFFFF";

            //context.Response.ContentType = "application/json";
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
            context.Response.Write(graph.ToString());

        }

        private void UniqueUser(HttpContext context)
        {
            Granularity granularity = Granularity.Day;

            int estPoints = GetRangeDays();
            int maxPoints = imageWidth / POINT_SPACE;
            int step = 1;
            int maxSteps = imageWidth / LABLE_SPACE;

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeMonths();
                granularity = Granularity.Month;
            }

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeYears();
                granularity = Granularity.Year;
            }

            while (estPoints > maxSteps)
            {
                estPoints = estPoints / 2;
                step++;
            }

            StatisticResult result = Statistics.GetUniqueUser(dateFrom, dateTo, groupBy, granularity, include);

            if (result.Series.Count < 1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
                context.Response.Write(language.GetString("MessageStatisticNoData"));
                return;
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            DateTime lastDate = DateTime.MinValue;

            List<string> colors = GetColors(result.Series.Count);

            Graph graph = new Graph();

            if (!string.IsNullOrEmpty(result.LabelX))
            {
                Legend xLegend = new Legend();
                xLegend.Text = result.LabelX;
                xLegend.Style = "color: #444444; font-size: 12px;";
                graph.X_Legend = xLegend;
            }

            if (!string.IsNullOrEmpty(result.LabelY))
            {
                Legend yLegend = new Legend();
                yLegend.Text = result.LabelY;
                yLegend.Style = "color: #444444; font-size: 12px;";
                graph.Y_Legend = yLegend;
            }

            XAxis xAxis = new XAxis();
            xAxis.Colour = "#444444";
            xAxis.GridColour = "#CCCCCC";
            xAxis.Steps = step;
            graph.X_Axis = xAxis;

            YAxis yAxis = new YAxis();
            yAxis.Colour = "#444444";
            yAxis.GridColour = "#CCCCCC";
            int y = result.MaxY - result.MinY;
            yAxis.Steps = y / 5;
            graph.Y_Axis = yAxis;

            string toolTip = language.GetString("LableStatisticCalls");

            for (int i = 0; i < result.Series.Count; i++)
            {
                ChartBase chart = new Line();
                chart.Colour = colors[i];
                //chart.Text = result.Series[i].Label.ToLowerInvariant();
                chart.Text = result.Series[i].Label;

                chart.Tooltip = string.Format("#val# {0} ({1})", toolTip, chart.Text);
                chart.Fontsize = 9;

                for (int j = 0; j < result.Series[i].Values.Count; j++)
                {
                    StatisticValueDateTimeInt value = (StatisticValueDateTimeInt)result.Series[i].Values[j];

                    if (lastDate == DateTime.MinValue)
                        lastDate = value.X;

                    if (value.X > maxDate)
                        maxDate = value.X;

                    if (value.X < minDate)
                        minDate = value.X;

                    string xLabel = string.Empty;
                    if (j % xAxis.Steps == 0)
                    {
                        if (granularity == Granularity.Year)
                        {
                            xLabel = value.X.ToString("yyyy");
                        }
                        else if (granularity == Granularity.Month)
                        {
                            if (lastDate.Year != value.X.Year || value.X.Month == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("MMM. yy");
                            }
                            else
                            {
                                xLabel = value.X.ToString("MMM.");
                            }
                        }
                        else if (granularity == Granularity.Day)
                        {
                            if (lastDate.Year != value.X.Year || lastDate.Month != value.X.Month || value.X.Day == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("d.M.");
                            }
                            else
                            {
                                xLabel = value.X.ToString("d.");
                            }
                        }
                    }

                    var val = chart.NewValue(xLabel, value.Y);
                    chart.Values.Add(val);
                }

                graph.AddElement(chart);
            }

            graph.Title = new Title(GetTitle(granularity));
            graph.Bgcolor = "#FFFFFF";

            //context.Response.ContentType = "application/json";
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
            context.Response.Write(graph.ToString());

        }

        private void PageViewDayRange(HttpContext context)
        {
            LABLE_SPACE = 16;
            POINT_SPACE = 4;

            int estPoints = 24;
            int maxPoints = imageWidth / POINT_SPACE;
            int step = 1;
            int maxSteps = imageWidth / LABLE_SPACE;

            while (estPoints > maxSteps)
            {
                estPoints = estPoints / 2;
                step++;
            }

            StatisticResult result = null;
            if (ignoreCommunity || !parentObjectID.HasValue)
                result = Statistics.GetSiteViewDayRange(dateFrom, dateTo, groupBy, null, include);
            else
                result = Statistics.GetSiteViewDayRange(dateFrom, dateTo, groupBy, parentObjectID, include);

            if (result.Series.Count < 1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
                context.Response.Write(language.GetString("MessageStatisticNoData"));
                return;
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            DateTime lastDate = DateTime.MinValue;

            List<string> colors = GetColors(result.Series.Count);

            Graph graph = new Graph();

            if (!string.IsNullOrEmpty(result.LabelX))
            {
                Legend xLegend = new Legend();
                xLegend.Text = result.LabelX;
                xLegend.Style = "color: #444444; font-size: 12px;";
                graph.X_Legend = xLegend;
            }

            if (!string.IsNullOrEmpty(result.LabelY))
            {
                Legend yLegend = new Legend();
                yLegend.Text = result.LabelY;
                yLegend.Style = "color: #444444; font-size: 12px;";
                graph.Y_Legend = yLegend;
            }

            XAxis xAxis = new XAxis();
            xAxis.Colour = "#444444";
            xAxis.GridColour = "#CCCCCC";
            xAxis.Steps = step;
            graph.X_Axis = xAxis;

            YAxis yAxis = new YAxis();
            yAxis.Colour = "#444444";
            yAxis.GridColour = "#CCCCCC";
            int y = result.MaxY - result.MinY;
            yAxis.Steps = y / 5;
            graph.Y_Axis = yAxis;

            string toolTip = language.GetString("LableStatisticCalls");

            for (int i = 0; i < result.Series.Count; i++)
            {
                ChartBase chart = new Line();
                chart.Colour = colors[i];
                //chart.Text = result.Series[i].Label.ToLowerInvariant();
                chart.Text = result.Series[i].Label;

                chart.Tooltip = string.Format("#val# {0} ({1})", toolTip, chart.Text);
                chart.Fontsize = 9;

                for (int j = 0; j < result.Series[i].Values.Count; j++)
                {
                    StatisticValueDateTimeInt value = (StatisticValueDateTimeInt)result.Series[i].Values[j];

                    if (lastDate == DateTime.MinValue)
                        lastDate = value.X;

                    if (value.X > maxDate)
                        maxDate = value.X;

                    if (value.X < minDate)
                        minDate = value.X;

                    string xLabel = string.Empty;
                    if (j % xAxis.Steps == 0)
                    {
                        xLabel = value.X.ToString("HH");
                    }

                    var val = chart.NewValue(xLabel, value.Y);
                    chart.Values.Add(val);
                }

                graph.AddElement(chart);
            }

            graph.Title = new Title(GetTitle(Granularity.None));
            graph.Bgcolor = "#FFFFFF";

            //context.Response.ContentType = "application/json";
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
            context.Response.Write(graph.ToString());

        }

        private void PageView(HttpContext context)
        {
            Granularity granularity = Granularity.Day; 
            
            int estPoints = GetRangeDays();
            int maxPoints = imageWidth / POINT_SPACE;
            int step  = 1;
            int maxSteps = imageWidth / LABLE_SPACE;

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeMonths();
                granularity = Granularity.Month; 
            }

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeYears();
                granularity = Granularity.Year; 
            }

            while (estPoints > maxSteps)
            {
                estPoints = estPoints / 2;
                step++;
            }

            StatisticResult result = null;
            if (ignoreCommunity || !parentObjectID.HasValue)
                result = Statistics.GetSiteView(dateFrom, dateTo, groupBy, granularity, null, include);
            else
                result = Statistics.GetSiteView(dateFrom, dateTo, groupBy, granularity, parentObjectID, include);

            if (result.Series.Count < 1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
                context.Response.Write(language.GetString("MessageStatisticNoData"));
                return;
            }

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            DateTime lastDate = DateTime.MinValue;   

            List<string> colors = GetColors(result.Series.Count);

            Graph graph = new Graph();

            if (!string.IsNullOrEmpty(result.LabelX))
            {
                Legend xLegend = new Legend();
                xLegend.Text = result.LabelX;
                xLegend.Style = "color: #444444; font-size: 12px;";
                graph.X_Legend = xLegend;
            }

            if (!string.IsNullOrEmpty(result.LabelY))
            {
                Legend yLegend = new Legend();
                yLegend.Text = result.LabelY;
                yLegend.Style = "color: #444444; font-size: 12px;";
                graph.Y_Legend = yLegend;
            }

            XAxis xAxis = new XAxis();
            xAxis.Colour = "#444444";
            xAxis.GridColour = "#CCCCCC";
            xAxis.Steps = step;
            graph.X_Axis = xAxis;

            YAxis yAxis = new YAxis();
            yAxis.Colour = "#444444";
            yAxis.GridColour = "#CCCCCC";
            int y = result.MaxY - result.MinY;
            yAxis.Steps = y / 5;
            graph.Y_Axis = yAxis;

            string toolTip = language.GetString("LableStatisticCalls");

            for (int i = 0; i < result.Series.Count; i++)
            {
                ChartBase chart = new Line();
                chart.Colour = colors[i];
                //chart.Text = result.Series[i].Label.ToLowerInvariant();
                chart.Text = result.Series[i].Label;

                chart.Tooltip = string.Format("#val# {0} ({1})", toolTip, chart.Text);
                chart.Fontsize = 9;

                for (int j = 0; j < result.Series[i].Values.Count; j++)
                {
                    StatisticValueDateTimeInt value = (StatisticValueDateTimeInt)result.Series[i].Values[j];

                    if (lastDate == DateTime.MinValue)
                        lastDate = value.X;  

                    if (value.X > maxDate)
                        maxDate = value.X;

                    if (value.X < minDate)
                        minDate = value.X;

                    string xLabel = string.Empty;
                    if (j % xAxis.Steps == 0)
                    {
                        if (granularity == Granularity.Year)
                        {
                            xLabel = value.X.ToString("yyyy");
                        }
                        else if (granularity == Granularity.Month)
                        {
                            if (lastDate.Year != value.X.Year || value.X.Month == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("MMM. yy");
                            }
                            else
                            {
                                xLabel = value.X.ToString("MMM.");
                            }
                        }
                        else if (granularity == Granularity.Day)
                        {
                            if (lastDate.Year != value.X.Year || lastDate.Month != value.X.Month || value.X.Day == 1)
                            {
                                lastDate = value.X;
                                xLabel = value.X.ToString("d.M.");
                            }
                            else
                            {
                                xLabel = value.X.ToString("d.");
                            }
                        }
                    }

                    var val = chart.NewValue(xLabel, value.Y);
                    chart.Values.Add(val);
                }

                graph.AddElement(chart);
            }

            graph.Title = new Title(GetTitle(granularity));
            graph.Bgcolor = "#FFFFFF";

            //context.Response.ContentType = "application/json";
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
            context.Response.Write(graph.ToString());

        }

        private void AllOther(HttpContext context)
        {
            Granularity granularity = Granularity.Day;

            int estPoints = GetRangeDays();
            int maxPoints = imageWidth / POINT_SPACE;
            int step = 1;
            int maxSteps = imageWidth / LABLE_SPACE;

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeMonths();
                granularity = Granularity.Month;
            }

            if (estPoints > maxPoints)
            {
                estPoints = GetRangeYears();
                granularity = Granularity.Year;
            }

            while (estPoints > maxSteps)
            {
                estPoints = estPoints / 2;
                step++;
            }

            StatisticResult result = null;

            switch (statisticsType)
            {
                case StatisticsType.CountCommunityMembers:
                    result = Statistics.GetObjectCountCommunityMembers(dateFrom, dateTo, groupBy, granularity, parentObjectID, include);
                    break;
                case StatisticsType.CountProfileFriends:
                    result = Statistics.GetObjectCountProfileFriends(dateFrom, dateTo, groupBy, granularity, parentObjectID, include);
                    break;
                case StatisticsType.TotalObjectsCount:
                    result = Statistics.GetObjectCountCommunityTotal(parentObjectID, groupBy).Results[0];
                    break;
                case StatisticsType.CreationObjects:
                    if (!ignoreCommunity && parentObjectID.HasValue)
                    {
                        if (parentObjectType == Helper.GetObjectTypeNumericID("Community"))
                            result = Statistics.GetObjectCreationCommunityObjects(dateFrom, dateTo, groupBy, granularity, parentObjectID, include);
                        else if (parentObjectType == Helper.GetObjectTypeNumericID("ProfileCommunity"))
                            result = Statistics.GetObjectCreationProfileObjects(dateFrom, dateTo, groupBy, granularity, parentObjectID, include);
                        else
                            result = Statistics.GetObjectCreationObjects(dateFrom, dateTo, groupBy, granularity, include);
                    }
                    else
                    {
                        result = Statistics.GetObjectCreationObjects(dateFrom, dateTo, groupBy, granularity, include);
                    }
                    break;
            }

            if (result == null || result.Series.Count < 1)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 501;
                context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));
                context.Response.Write(language.GetString("MessageStatisticNoData"));
                return;
            }

            List<string> colors = GetColors(result.Series.Count);
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;
            DateTime lastDate = DateTime.MinValue;   

            Graph graph = new Graph();
            graph.Bgcolor = "#FFFFFF";

            if (!string.IsNullOrEmpty(result.LabelX))
            {
                Legend xLegend = new Legend();
                xLegend.Text = result.LabelX;
                xLegend.Style = "color: #444444; font-size: 12px;";
                graph.X_Legend = xLegend;
            }

            if (!string.IsNullOrEmpty(result.LabelY))
            {
                Legend yLegend = new Legend();
                yLegend.Text = result.LabelY;
                yLegend.Style = "color: #444444; font-size: 12px;";
                graph.Y_Legend = yLegend;
            }

            XAxis xAxis = new XAxis();
            xAxis.Colour = "#444444";
            xAxis.GridColour = "#CCCCCC";
            xAxis.Steps = step;
            graph.X_Axis = xAxis;

            YAxis yAxis = new YAxis();
            yAxis.Colour = "#444444";
            yAxis.GridColour = "#CCCCCC";
            int y = result.MaxY - result.MinY;
            yAxis.Steps = y / 5;
            graph.Y_Axis = yAxis;

            string toolTip = language.GetString("LableStatisticCalls");

            for (int i = 0; i < result.Series.Count; i++)
            {
                ChartBase chart = null;

                switch (chartType)
                {
                    case ChartType.Bar:
                        chart = new Bar();
                        break;
                    case ChartType.Line:
                        chart = new Line();
                        break;
                    case ChartType.Column:
                        chart = new HBar();
                        break;
                    case ChartType.Pie:
                        chart = new Pie();
                        break;
                }

                chart.Colour = colors[i];
                //chart.Text = result.Series[i].Label.ToLowerInvariant();
                chart.Text = result.Series[i].Label;

                if (statisticsType == StatisticsType.PageView)
                    chart.Tooltip = string.Format("#val# {0} ({1})", toolTip, chart.Text);
                else
                    chart.Tooltip = string.Format("#val#");

                chart.Fontsize = 9;

                for (int j = 0; j < result.Series[i].Values.Count; j++)
                {
                    StatisticValue value = result.Series[i].Values[j];
                    string xLabel = string.Empty;

                    if (value is StatisticValueDateTimeInt)
                    {
                        StatisticValueDateTimeInt valueDateTime = (StatisticValueDateTimeInt)value;

                        if (lastDate == DateTime.MinValue)
                            lastDate = valueDateTime.X;

                        if (valueDateTime.X > maxDate)
                            maxDate = valueDateTime.X;

                        if (valueDateTime.X < minDate)
                            minDate = valueDateTime.X;

                        if (j % xAxis.Steps == 0)
                        {
                            if (granularity == Granularity.Year)
                            {
                                xLabel = valueDateTime.X.ToString("yyyy");
                            }
                            else if (granularity == Granularity.Month)
                            {
                                if (lastDate.Year != valueDateTime.X.Year || valueDateTime.X.Month == 1)
                                {
                                    lastDate = valueDateTime.X;
                                    xLabel = valueDateTime.X.ToString("MMM. yy");
                                }
                                else
                                {
                                    xLabel = valueDateTime.X.ToString("MMM.");
                                }
                            }
                            else if (granularity == Granularity.Day)
                            {
                                if (lastDate.Year != valueDateTime.X.Year || lastDate.Month != valueDateTime.X.Month || valueDateTime.X.Day == 1)
                                {
                                    lastDate = valueDateTime.X;
                                    xLabel = valueDateTime.X.ToString("d.M.");
                                }
                                else
                                {
                                    xLabel = valueDateTime.X.ToString("d.");
                                }
                            }
                        }
                    }
                    else if (value is StatisticValueStringInt)
                    {
                        xLabel = ((StatisticValueStringInt)value).X;
                    }

                    var val = chart.NewValue(xLabel, value.Y);
                    chart.Values.Add(val);
                }

                graph.Title = new Title(GetTitle(granularity));
                graph.AddElement(chart);
            }

            //context.Response.ContentType = "application/json";

            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("IDS", string.Concat(chartId, "|", errorId));  
            context.Response.Write(graph.ToString());
        }

        private string GetTitle(Granularity granularity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(language.GetString(string.Format("EnumStatisticsType{0}", statisticsType)));

            if (granularity == Granularity.Year)
            {
                if (dateFrom.Year != dateTo.Year)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("yyyy"), language.GetString("LableTo"), dateTo.ToString("yyyy"));
                else
                    sb.AppendFormat(" - {0}", dateTo.ToString("yyyy"));
            }
            else if (granularity == Granularity.Month)
            {
                if (dateFrom.Year != dateTo.Year)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("MMMM yyyy"), language.GetString("LableTo"), dateTo.ToString("MMMM yyyy"));
                else if (dateFrom.Month != dateTo.Month)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("MMMM"), language.GetString("LableTo"), dateTo.ToString("MMMM yyyy"));
                else
                    sb.AppendFormat(" - {0}", dateTo.ToString("MMMM yyyy"));
            }
            else if (granularity == Granularity.Day)
            {
                if (dateFrom.Year != dateTo.Year)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d.MMMM yyyy"), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else if (dateFrom.Month != dateTo.Month)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d.MMMM"), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else if (dateFrom.Day != dateTo.Day)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d."), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else
                    sb.AppendFormat(" - {0}", dateTo.ToString("dddd d.MMMM yyyy"));
            }
            else
            {
                if (dateFrom.Year != dateTo.Year)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d.MMMM yyyy"), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else if (dateFrom.Month != dateTo.Month)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d.MMMM"), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else if (dateFrom.Day != dateTo.Day)
                    sb.AppendFormat(" - {0} {1} {2}", dateFrom.ToString("dddd d."), language.GetString("LableTo"), dateTo.ToString("dddd d.MMMM yyyy"));
                else
                    sb.AppendFormat(" - {0}", dateTo.ToString("dddd d.MMMM yyyy"));
            }

            if (groupBy != GroupBy.None)
            {
                if (sb.Length > 0) 
                    sb.Append(" - ");
                sb.AppendFormat(GuiLanguage.GetGuiLanguage("DataAccess").GetString("EnumStatisticGroupBy"), GuiLanguage.GetGuiLanguage("DataAccess").GetString(string.Format("EnumStatisticGroupBy{0}", groupBy)));
            }

            return sb.ToString();
        }

        private static List<string> GetColors(int count)
        {
            List<string> colors = new List<string>(8);
            colors.Add("#EED300");
            colors.Add("#F192BC");
            colors.Add("#72A347");
            colors.Add("#124B54");
            colors.Add("#EE6038");
            colors.Add("#5F93A8");
            colors.Add("#2C5BA1");
            colors.Add("#414B4D");

            while (colors.Count < count)
            {
                Random random = new Random();
                colors.Add(string.Format("#{0:00}{1:00}{2:00}", random.Next(0, 255).ToString("X"), random.Next(0, 255).ToString("X"), random.Next(0, 255).ToString("X")));
            }

            return colors;
        }

        private int GetRangeDays()
        {
            TimeSpan range;
            if (dateTo.Year == DateTime.Now.Year && dateTo.Month == DateTime.Now.Month)      
                range = dateTo.GetEndOfMonth().Subtract(dateFrom);
            else
                range = dateTo.Subtract(dateFrom);

            return range.Days;
        }

        private int GetRangeMonths()
        {
            TimeSpan range = dateTo.Subtract(dateFrom);
            return range.Days / 31;
        }

        private int GetRangeYears()
        {
            TimeSpan range = dateTo.Subtract(dateFrom);
            return range.Days / 365;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
