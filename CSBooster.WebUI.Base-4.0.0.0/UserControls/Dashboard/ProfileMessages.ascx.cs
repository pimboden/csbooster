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
    public partial class ProfileMessages : ProfileQuestionsControl
    {
        private DataObjectUser user;
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            try
            {
                user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId, null, true);
                FillRadioListAnswer(rblMsgFrom, user.MsgFrom, FillListBy.Value);
                FillRadioListAnswer(rblEmail, user.GetMail, FillListBy.Value);

            }
            catch
            {
                //ignore errors 
            }
        }

        public void Save()
        {
            user.MsgFrom = GetComaSeparatetdValues(rblMsgFrom.Items);
            user.MsgFromShow = true;
            user.GetMail = GetComaSeparatetdValues(rblEmail.Items);
            user.GetMailShow = true;
            user.Update(UserDataContext.GetUserDataContext());
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            Save();
        }
    }
}