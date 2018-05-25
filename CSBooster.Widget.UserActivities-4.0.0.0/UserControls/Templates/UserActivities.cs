// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.DataAccess.Business;
using _4screen.Utils.Web;

namespace _4screen.CSB.Widget.UserControls.Templates
{
    public partial class UserActivities : System.Web.UI.UserControl, IUserActivityWorker
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WidgetUserActivities");

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