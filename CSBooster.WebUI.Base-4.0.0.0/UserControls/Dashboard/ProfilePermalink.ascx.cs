// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfilePermalink : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LblProfUrl.Text = string.Concat("<a href=\"", Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), UserProfile.Current.Nickname), "\">", _4screen.CSB.Common.SiteConfig.SiteURL, Helper.GetDetailLink(Helper.GetObjectTypeNumericID("User"), UserProfile.Current.Nickname), "</a>");
            hplMail.NavigateUrl = string.Format("Javascript:radWinOpen('/Pages/popups/MessageSend.aspx?MsgType=rec&ObjType=User&ObjId={0}', '{1}', 450, 450, false, null)", UserProfile.Current.UserId.ToString(), language.GetString("TitlePermalink").StripForScript());
        }
    }
}