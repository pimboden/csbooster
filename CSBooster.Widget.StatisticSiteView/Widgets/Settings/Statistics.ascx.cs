//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		17.08.2007 / PI
//                         Inherits StepsASCX
//                         Step with Basic Info
//  Updated:   
//******************************************************************************

using System;
using System.Xml;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Statistic;
using _4screen.CSB.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using _4screen.CSB.DataAccess.Business;

namespace _4screen.CSB.Widget.Settings
{
    public partial class Statistics : System.Web.UI.UserControl, IWidgetSettings
    {
        private GuiLanguage languageDataAccess = GuiLanguage.GetGuiLanguage("DataAccess");  
        public DataObject ParentDataObject { get; set; }
        public Guid InstanceId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (ListItem item in this.RblInitTimeRange.Items)
            {
                string key = string.Format("EnumStatisticDataRange{0}", (DataRange)Convert.ToInt32(item.Value));
                item.Text = languageDataAccess.GetString(key);  
            }

            XmlDocument xmlDocument = Utils.LoadWidgetInstanceSettings(InstanceId);
            if (!IsPostBack) 
                FillControls(xmlDocument, true);
            else
                FillControls(xmlDocument, false);
        }

        public bool Save()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlHelper.CreateRoot(xmlDocument, "root");
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "StatisticsType", CbxStatisticsType.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "GroupBy", CbxGroupBy.SelectedValue);
                XmlHelper.SetElementInnerText(xmlDocument.DocumentElement, "InitTimeRange", RblInitTimeRange.SelectedValue);
                return Utils.SaveWidgetInstanceSettings(InstanceId, xmlDocument);
            }
            catch
            {
                return false;
            }
        }

        private void FillControls(XmlDocument xmlDom, bool setValue)
        {
            int statisticsType = XmlHelper.GetElementValue(xmlDom.DocumentElement, "StatisticsType", (int)StatisticsType.PageView);

            if (CbxStatisticsType.Items.Count == 0)
            {
                foreach (string enu in Enum.GetNames(typeof(StatisticsType)))
                {
                    string text = languageDataAccess.GetString("EnumStatisticsType" + enu);
                    int val = Convert.ToInt32(Enum.Parse(typeof(StatisticsType), enu));
                    if (val < 4) continue;
                    Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(text, val.ToString());
                    if (setValue) 
                        item.Selected = (val == statisticsType);
                    CbxStatisticsType.Items.Add(item);
                }
            }

            if (setValue)
            {
                int groupBy = XmlHelper.GetElementValue(xmlDom.DocumentElement, "GroupBy", (int)GroupBy.None);
                FillGroupBy(statisticsType, groupBy);
            }
            else
            {
                CbxStatisticsType_OnSelectedIndexChanged(null, null);
            }

            if (setValue)
            {
                int initTimeRange = XmlHelper.GetElementValue(xmlDom.DocumentElement, "InitTimeRange", 1);
                RblInitTimeRange.SelectedIndex = initTimeRange - 1;
            }
        }

        private void FillGroupBy(int statisticsType, int groupBy)
        {
            CbxGroupBy.Items.Clear();
            string test = ",1,2,4,8,16,32,64,";

            if (statisticsType == (int)StatisticsType.UserSession || statisticsType == (int)StatisticsType.UniqueUser)
                test = ",1,16,32,";
            else if (statisticsType == (int)StatisticsType.PageView || statisticsType == (int)StatisticsType.PageViewDayRange)
                test = ",1,2,4,8,16,32,64,";
            else
                test = ",1,4,16,32,";

            foreach (string enu in Enum.GetNames(typeof(GroupBy)))
            {
                int val = Convert.ToInt32(Enum.Parse(typeof(GroupBy), enu));
                if (test.IndexOf(string.Format(",{0},", val)) > -1)
                {
                    string text = languageDataAccess.GetString("EnumStatisticGroupBy" + enu);

                    Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(text, val.ToString());
                    item.Selected = (val == groupBy);
                    CbxGroupBy.Items.Add(item);
                }
            }

            if (CbxGroupBy.SelectedIndex == -1)
                CbxGroupBy.SelectedIndex = 0;
        }

        protected void CbxStatisticsType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int statisticsType = int.Parse(CbxStatisticsType.SelectedItem.Value);
            FillGroupBy(statisticsType, 0); 

        }

    }
}