//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.Widget;

namespace _4screen.CSB.Widget.UserControls.Repeaters
{
    public partial class UserActivities : System.Web.UI.UserControl, IUserActivity
    {
        private GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserActivities");  
        private int numberItems;
        private DateTime lastDate = DateTime.Now;
        private DateTime firstDate = DateTime.MaxValue;
        private int lastGroup = -1;
        private string dateFormat = "g";

        public UserActivityParameters UserActivityParameters { get; set; }
        public bool HasContent { get; set; }
        public string OutputTemplate { get; set; }

        protected override void OnInit(EventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            lastDate = DateTime.Now;
            HasContent = false;
            this.UserActivityParameters.Udc = UserDataContext.GetUserDataContext();
            this.YMRP.DataSource = _4screen.CSB.DataAccess.Business.UserActivities.Load(this.UserActivityParameters);
            this.YMRP.DataBind();
            numberItems = this.UserActivityParameters.ItemTotal;
        }

        protected void YMRP_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            UserActivity item = (UserActivity)e.Item.DataItem;
            PlaceHolder ph = e.Item.FindControl("Ph") as PlaceHolder;

            if (firstDate == DateTime.MaxValue)
                firstDate = item.Date; 

            int thisGroup = -1;

            int dayNow = (DateTime.Now.Year * 365) + DateTime.Now.DayOfYear;
            int dayItem = (item.Date.Year * 365) + item.Date.DayOfYear;

            thisGroup = dayNow - dayItem; 
            if (thisGroup > 3)
                thisGroup = 3;

            if (lastGroup != thisGroup) 
            {
                Control split = LoadControl("~/UserControls/Templates/UserActivitiesSplitter.ascx");
                IUserActivityWorker splitWorker = split as IUserActivityWorker;
                if (splitWorker != null)
                {
                    dateFormat = language.GetString(string.Format("DateFormatString{0}", thisGroup));
                    splitWorker.SplitterText = language.GetString(string.Format("LableSplitterText{0}", thisGroup)); 
                    ph.Controls.Add(split);
                    HasContent = true;
                }
                lastGroup = thisGroup;
            }

            lastDate = item.Date; 

            Control ctrl = LoadControl(string.Format("~/UserControls/Templates/{0}", this.OutputTemplate));

            IUserActivityWorker ctrlWorker = ctrl as IUserActivityWorker;
            if (ctrlWorker != null)
            {
                ctrlWorker.UserActivity = item;
                ctrlWorker.DateFormatString = dateFormat;
                ph.Controls.Add(ctrl);
                HasContent = true;
            }
        }

        public int GetNumberItems()
        {
            return this.numberItems;
        }

    }
}