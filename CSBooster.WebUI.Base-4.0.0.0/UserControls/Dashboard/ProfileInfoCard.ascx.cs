// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.IdentityModel.Claims;
using _4screen.CSB.Common;
using _4screen.CSB.DataAccess.Business;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;
using Microsoft.IdentityModel.TokenProcessor;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileInfoCard : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        private DataObjectUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = DataObject.Load<DataObjectUser>(UserProfile.Current.UserId);
            if (user.State != ObjectState.Added)
            {
                if (!string.IsNullOrEmpty(user.PPID))
                    this.LitInfoCardCur.Text = user.PPID.CropString(20);
                else
                    this.LitInfoCardCur.Text = language.GetString("MessageInfoCardNone");
            }
        }

        public void LbtnInfoCardAddClick(object sender, EventArgs e)
        {
            string secXmlToken;
            secXmlToken = Request.Params["SecXmlToken"];
            if (!string.IsNullOrEmpty(secXmlToken))
            {
                Token token = new Token(secXmlToken);
                string ppid = token.Claims[ClaimTypes.PPID];
                if (!string.IsNullOrEmpty(ppid))
                {
                    user.PPID = ppid;
                    user.Update(UserDataContext.GetUserDataContext());
                    this.LitInfoCardMsg.Text = language.GetString("MessageInfoCardConfirmed");
                    this.LitInfoCardCur.Text = ppid.CropString(20);
                }
            }
            else
            {
                this.LitInfoCardMsg.Text = language.GetString("MessageInfoCardNorConfirmed");
            }
        }

        public void LbtnInfoCardRemoveClick(object sender, EventArgs e)
        {
            user.PPID = string.Empty;
            user.Update(UserDataContext.GetUserDataContext());
            this.LitInfoCardMsg.Text = language.GetString("MessageInfoCardDeleted");
            this.LitInfoCardCur.Text = "";
        }
    }
}