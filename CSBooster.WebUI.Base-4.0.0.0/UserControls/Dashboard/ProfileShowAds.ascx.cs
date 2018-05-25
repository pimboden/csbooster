// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileShowAds : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");
        private DataObjectUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);
            if (!IsPostBack)
            {
                this.SHOWADSCB.Checked = user.DisplayAds;
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            user.DisplayAds = this.SHOWADSCB.Checked;
            user.Update(UserDataContext.GetUserDataContext());
            AdWordFilterJob adWordFilterJob = new AdWordFilterJob();
            adWordFilterJob.ProcessDataObjectsForUser(UserProfile.Current.UserId);
        }
    }
}