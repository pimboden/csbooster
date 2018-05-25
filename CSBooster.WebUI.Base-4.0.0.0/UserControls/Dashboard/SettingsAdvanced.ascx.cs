// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Web.UI;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.UserControls.Dashboard
{
    public partial class SettingsAdvanced : System.Web.UI.UserControl
    {
        protected GuiLanguage languageShared = GuiLanguage.GetGuiLanguage("Shared");

        protected void Page_Load(object sender, EventArgs e)
        {
            PhProfile.Controls.Clear();
            if (CustomizationSection.CachedInstance.Modules["Messaging"].Enabled)
            {
                ProfileMessages profileMessages = this.LoadControl("~/UserControls/Dashboard/ProfileMessages.ascx") as ProfileMessages;
                profileMessages.ID = "PME";
                PhProfile.Controls.Add(profileMessages);
            }
            if (CustomizationSection.CachedInstance.Profile["ChangePassword"].Enabled)
            {
                ProfileChangePassword profileChangePassword = this.LoadControl("~/UserControls/Dashboard/ProfileChangePassword.ascx") as ProfileChangePassword;
                profileChangePassword.ID = "PCP";
                PhProfile.Controls.Add(profileChangePassword);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Logins["OpenID"].Enabled)
            {
                ProfileOpenID profileOpenId = this.LoadControl("~/UserControls/Dashboard/ProfileOpenID.ascx") as ProfileOpenID;
                profileOpenId.ID = "POID";
                PhProfile.Controls.Add(profileOpenId);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Logins["InfoCard"].Enabled)
            {
                ProfileInfoCard profileInfoCard = this.LoadControl("~/UserControls/Dashboard/ProfileInfoCard.ascx") as ProfileInfoCard;
                profileInfoCard.ID = "PIC";
                PhProfile.Controls.Add(profileInfoCard);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Profile["Permalink"].Enabled)
            {
                ProfilePermalink profilePermalink = this.LoadControl("~/UserControls/Dashboard/ProfilePermalink.ascx") as ProfilePermalink;
                profilePermalink.ID = "PPL";
                PhProfile.Controls.Add(profilePermalink);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Profile["ShowAds"].Enabled)
            {
                ProfileShowAds profileShowAds = this.LoadControl("~/UserControls/Dashboard/ProfileShowAds.ascx") as ProfileShowAds;
                profileShowAds.ID = "PSA";
                PhProfile.Controls.Add(profileShowAds);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Profile["UILanguage"].Enabled)
            {
                ProfileUILanguage profileUILanguage = this.LoadControl("~/UserControls/Dashboard/ProfileUILanguage.ascx") as ProfileUILanguage;
                profileUILanguage.ID = "PUIL";
                PhProfile.Controls.Add(profileUILanguage);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Profile["UserPoints"].Enabled)
            {
                ProfileUserPoints profileUserPoints = this.LoadControl("~/UserControls/Dashboard/ProfileUserPoints.ascx") as ProfileUserPoints;
                profileUserPoints.ID = "PUP";
                PhProfile.Controls.Add(profileUserPoints);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
            if (CustomizationSection.CachedInstance.Profile["DeleteAccount"].Enabled)
            {
                ProfileDeleteCommunity profileDeleteAccount = this.LoadControl("~/UserControls/Dashboard/ProfileDeleteCommunity.ascx") as ProfileDeleteCommunity;
                profileDeleteAccount.ID = "PDE";
                PhProfile.Controls.Add(profileDeleteAccount);
                PhProfile.Controls.Add(new LiteralControl(@"<div class=""CSB_input_separator""></div>"));
            }
        }
    }
}