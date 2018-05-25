// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI.WebControls;
using _4screen.CSB.Common;
using _4screen.CSB.WebUI.Code;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class ProfileUILanguage : ProfileQuestionsControl
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("UserControls.Dashboard.WebUI.Base");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlLang.Items.Add(new ListItem(language.GetString("TextBrowserLanguage"), ""));
                foreach (var lang in _4screen.Utils.Web.SiteConfig.Languages)
                {
                    DdlLang.Items.Add(new ListItem(lang.Value, lang.Key));
                }

                ListItem selectedItem = DdlLang.Items.FindByValue(UserProfile.Current.PrefferedCulture);
                if (selectedItem != null)
                    DdlLang.SelectedValue = selectedItem.Value;
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            UserProfile.Current.PrefferedCulture = DdlLang.SelectedValue;
            UserProfile.Current.Save();
            Response.Redirect(Request.RawUrl);
        }
    }
}