using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;
using _4screen.CSB.DataAccess;
using System.Text;
using OpenFlashChartLib;
using OpenFlashChartLib.Charts;
using OpenFlashChartLib.Controls;
using ChartType = _4screen.CSB.Common.Statistic.ChartType;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget
{
    public partial class Statistics : WidgetBase
    {
        protected GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");  
        protected string VRoot = string.Empty;
        protected string DefaultUrlEncoded = string.Empty;
        protected string BaseUrl = string.Empty;
        protected string ChartWidth;
        protected string ChartHeight;
        private DateTime initDateTo;
        private DateTime initDateFrom;

        public ChartType ChartType { get; set; }
        public int InitTimeRange { get; set; }
        public GroupBy GroupBy { get; set; }
        public Granularity Granularity { get; set; }
        public bool ShowLabel { get; set; }
        public int ImageWidth { get; set; }
        public StatisticsType StatisticsType { get; set; }
        public List<object> include { get; set; }

        public override bool ShowObject(string settingsXml)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(settingsXml);


            this.StatisticsType = (StatisticsType)XmlHelper.GetElementValue(xmlDom.DocumentElement, "StatisticsType", (int)StatisticsType.PageView);
            this.GroupBy = (GroupBy)XmlHelper.GetElementValue(xmlDom.DocumentElement, "GroupBy", (int)GroupBy.None);

            this.include = new List<object>();
            foreach (XmlElement item in xmlDom.DocumentElement.SelectNodes("Include"))
            {
                if (item.InnerText == "all")
                {
                    this.include.Clear();
                    break;
                }
                else
                {
                    include.Add(item.InnerText);   
                }
            }

            this.InitTimeRange = XmlHelper.GetElementValue(xmlDom.DocumentElement, "InitTimeRange", 1);
            initDateTo= DateTime.Now.GetEndOfMonth();
            if (InitTimeRange == 1)
                initDateFrom = initDateTo.GetStartOfMonth();
            else if (InitTimeRange == 2)
                initDateFrom = initDateTo.AddMonths(-1).GetStartOfMonth();
            else if (InitTimeRange == 3)
                initDateFrom = initDateTo.AddMonths(-2).GetStartOfMonth();
            else
                initDateFrom = initDateTo.GetStartOfMonth();


            this.dateFrom.SelectedDate = initDateFrom;
            this.dateTo.SelectedDate = initDateTo;


            this.ShowLabel = XmlHelper.GetElementValue(xmlDom.DocumentElement, "ShowLabel", false);
            int imageMargin = int.Parse(XmlHelper.GetElementValue(xmlDom.DocumentElement, "ddlImgMargin", "6"));
            this.ImageWidth = (base._Host.ColumnWidth - 2 * imageMargin);

            this.VRoot = SiteConfig.SiteVRoot;
            this.ChartWidth = this.ImageWidth.ToString();
            this.ChartHeight = (this.ImageWidth / 2).ToString();

            this.BaseUrl = VRoot + "/Pages/Other/ChartHandler.ashx";
            this.BaseUrl += string.Format("?StatisticsType={0}", (int)this.StatisticsType);
            this.BaseUrl += string.Format("&GroupBy={0}", (int)this.GroupBy);
            foreach (object item in include)
            {
                this.BaseUrl += string.Format("&Incl={0}", item.ToString());
            }

            this.BaseUrl += string.Format("&ParentObjectID={0}", base._Host.ParentCommunityID);
            this.BaseUrl += string.Format("&ParentObjectType={0}", base._Host.ParentObjectType);
            this.BaseUrl += string.Format("&UserID={0}", UserProfile.Current.UserId);
            this.BaseUrl += string.Format("&ImageWidth={0}", this.ImageWidth);
            this.BaseUrl += string.Format("&ChartId={0}", this.myChart.ClientID);
            this.BaseUrl += string.Format("&ErrorId={0}", this.myChartErr.ClientID);

            string sufix = RenderJavaScript();

            this.dateFrom.ClientEvents.OnDateSelected = string.Format("reloadTheChart{0}", sufix);
            this.dateTo.ClientEvents.OnDateSelected = string.Format("reloadTheChart{0}", sufix);  

            this.DefaultUrlEncoded = Server.UrlEncode(getDefaultChartUrl());

            return true;
        }

        private string RenderJavaScript()
        { 
            long sufix = DateTime.Now.Ticks;   
            StringBuilder sb = new StringBuilder();

            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.AppendFormat("function reloadTheChart{0}() {{\r\n", sufix); 
	        sb.AppendFormat("GetJson('{0}','{1}','{2}'); return true;\r\n", this.BaseUrl, dateFrom.ClientID, dateTo.ClientID);
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");

            litJS.Text = sb.ToString();
            return sufix.ToString(); 
        }

        private string getDefaultChartUrl()
        {
            StringBuilder url = new StringBuilder();
            url.Append(VRoot + "/Pages/Other/ChartHandler.ashx");
            url.AppendFormat("?StatisticsType={0}", (int)this.StatisticsType);
            url.AppendFormat("&GroupBy={0}", (int)this.GroupBy);
            foreach (object item in include)
            {
                url.AppendFormat("&Incl={0}", item.ToString());
            }

            url.AppendFormat("&ParentObjectID={0}", base._Host.ParentCommunityID);
            url.AppendFormat("&ParentObjectType={0}", base._Host.ParentObjectType);
            url.AppendFormat("&UserID={0}", UserProfile.Current.UserId);
            url.AppendFormat("&ImageWidth={0}", this.ImageWidth);
            url.AppendFormat("&ChartId={0}", this.myChart.ClientID);
            url.AppendFormat("&ErrorId={0}", this.myChartErr.ClientID);
            url.AppendFormat("&DateFrom={0}.{1}.{2}", initDateFrom.Year, initDateFrom.Month, initDateFrom.Day);
            url.AppendFormat("&DateTo={0}.{1}.{2}", initDateTo.Year, initDateTo.Month, initDateTo.Day);         
            
            return url.ToString();
        }


    }
}