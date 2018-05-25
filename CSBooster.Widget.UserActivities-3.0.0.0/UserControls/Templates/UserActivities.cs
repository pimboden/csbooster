//******************************************************************************
//  Company:	4 screen AG, CH-6005 Lucerne, http://www.4screen.com
//
//  System:		CSB - Community Site Booster
//
//  Created:	#1.0.0.0		26.03.2007 / PI
//  Updated:   
//******************************************************************************

using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using System.Text;
using System;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class UserActivities : System.Web.UI.UserControl, IUserActivityWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserActivities");
        protected string SiteVRoot = SiteConfig.SiteVRoot;

        public UserActivity UserActivity { get; set; }
        public string SplitterText { get; set; }
        public string FolderParams { get; set; }
        public string DateFormatString { get; set; }

        public DateTime Date
        {
            get { return UserActivity.Date ;}
        }
        public string Text
        {
            get { return UserActivity.ToString(); }
        }

        public string DateFormated
        {
            get
            {
                if (!string.IsNullOrEmpty(DateFormatString))
                    return UserActivity.Date.ToString(this.DateFormatString);
                else
                    return UserActivity.Date.ToString();
            }
        }
   }
}