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
    public partial class ProfileChangePassword : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("ProfileData");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            hplChange.NavigateUrl = string.Format("Javascript:radWinOpen('/Pages/Popups/PasswordChange.aspx', '{0}', 400, 180, false, null)", language.GetString("CommandPasswordChange").StripForScript());
        }

    }
}