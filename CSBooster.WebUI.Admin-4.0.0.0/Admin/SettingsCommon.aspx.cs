// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using _4screen.CSB.Common;
using _4screen.Utils.Web;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class SettingsCommon : System.Web.UI.Page
    {
        protected GuiLanguage language = GuiLanguage.GetGuiLanguage("WebUI.Admin");

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("SettingsCommon");

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            if (!IsPostBack)
            {
                CustomizationSection customization = CustomizationSection.CachedInstance;

                CBCustomizationBar.Checked = customization.CustomizationBar.Enabled;
                CBWidgets.Checked = customization.CustomizationBar["Widgets"].Enabled;
                CBLayout.Checked = customization.CustomizationBar["Layout"].Enabled;
                CBTheme.Checked = customization.CustomizationBar["Theme"].Enabled;
                CBStyle.Checked = customization.CustomizationBar["Style"].Enabled;

                CBChangePassword.Checked = customization.Profile["ChangePassword"].Enabled;
                CBPermalink.Checked = customization.Profile["Permalink"].Enabled;
                CBUILanguage.Checked = customization.Profile["UILanguage"].Enabled;
                CBUserPoints.Checked = customization.Profile["UserPoints"].Enabled;
                CBDeleteAccount.Checked = customization.Profile["DeleteAccount"].Enabled;
                CBPictureColors.Checked = customization.Profile["PictureColors"].Enabled;
                CBPicture.Checked = customization.Profile["Picture"].Enabled;
                CBColors.Checked = customization.Profile["Colors"].Enabled;
                CBName.Checked = customization.Profile["Name"].Enabled;
                CBSurname.Checked = customization.Profile["Surname"].Enabled;
                CBLastname.Checked = customization.Profile["Lastname"].Enabled;
                CBGenderBirthday.Checked = customization.Profile["GenderBirthday"].Enabled;
                CBGender.Checked = customization.Profile["Gender"].Enabled;
                CBBirthday.Checked = customization.Profile["Birthday"].Enabled;
                CBUserLocation.Checked = customization.Profile["UserLocation"].Enabled;
                CBAddress.Checked = customization.Profile["Address"].Enabled;
                CBCountry.Checked = customization.Profile["Country"].Enabled;
                CBLanguage.Checked = customization.Profile["Language"].Enabled;
                CBPersonal.Checked = customization.Profile["Personal"].Enabled;
                CBPersonalRelationship.Checked = customization.Profile["PersonalRelationship"].Enabled;
                CBPersonalAttractedTo.Checked = customization.Profile["PersonalAttractedTo"].Enabled;
                CBPersonalEyeColor.Checked = customization.Profile["PersonalEyeColor"].Enabled;
                CBPersonalHairColor.Checked = customization.Profile["PersonalHairColor"].Enabled;
                CBPersonalBodyHeight.Checked = customization.Profile["PersonalBodyHeight"].Enabled;
                CBPersonalBodyWeight.Checked = customization.Profile["PersonalBodyWeight"].Enabled;
                CBPhone.Checked = customization.Profile["Phone"].Enabled;
                CBPhonenumberMobile.Checked = customization.Profile["PhonenumberMobile"].Enabled;
                CBPhonenumberLandline.Checked = customization.Profile["PhonenumberLandline"].Enabled;
                CBCommunication.Checked = customization.Profile["Communication"].Enabled;
                CBCommunicationMSN.Checked = customization.Profile["CommunicationMSN"].Enabled;
                CBCommunicationYahoo.Checked = customization.Profile["CommunicationYahoo"].Enabled;
                CBCommunicationSkype.Checked = customization.Profile["CommunicationSkype"].Enabled;
                CBCommunicationICQ.Checked = customization.Profile["CommunicationICQ"].Enabled;
                CBCommunicationAIM.Checked = customization.Profile["CommunicationAIM"].Enabled;
                CBWeb.Checked = customization.Profile["Web"].Enabled;
                CBHomepage.Checked = customization.Profile["Homepage"].Enabled;
                CBBlog.Checked = customization.Profile["Blog"].Enabled;
                CBJobTalents.Checked = customization.Profile["JobTalents"].Enabled;
                CBJob.Checked = customization.Profile["Job"].Enabled;
                CBSlogan.Checked = customization.Profile["Slogan"].Enabled;
                CBInterests.Checked = customization.Profile["Interests"].Enabled;

                CBMessaging.Checked = customization.Modules["Messaging"].Enabled;
                CBFriends.Checked = customization.Modules["Friends"].Enabled;
                CBComments.Checked = customization.Modules["Comments"].Enabled;
                CBAlerts.Checked = customization.Modules["Alerts"].Enabled;
                CBFavorites.Checked = customization.Modules["Favorites"].Enabled;
                CBMemberships.Checked = customization.Modules["Memberships"].Enabled;

                CBFacebookUserId.Checked = customization.Logins["FacebookUserId"].Enabled;
                CBOpenID.Checked = customization.Logins["OpenID"].Enabled;
                CBInfoCard.Checked = customization.Logins["InfoCard"].Enabled;
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                CustomizationSection customization = CustomizationSection.CachedInstance;

                customization.CustomizationBar.Enabled = CBCustomizationBar.Checked;
                customization.CustomizationBar["Widgets"].Enabled = CBWidgets.Checked;
                customization.CustomizationBar["Layout"].Enabled = CBLayout.Checked;
                customization.CustomizationBar["Theme"].Enabled = CBTheme.Checked;
                customization.CustomizationBar["Style"].Enabled = CBStyle.Checked;

                customization.Profile["ChangePassword"].Enabled = CBChangePassword.Checked;
                customization.Profile["Permalink"].Enabled = CBPermalink.Checked;
                customization.Profile["UILanguage"].Enabled = CBUILanguage.Checked;
                customization.Profile["UserPoints"].Enabled = CBUserPoints.Checked;
                customization.Profile["DeleteAccount"].Enabled = CBDeleteAccount.Checked;
                customization.Profile["PictureColors"].Enabled = CBPictureColors.Checked;
                customization.Profile["Picture"].Enabled = CBPicture.Checked;
                customization.Profile["Colors"].Enabled = CBColors.Checked;
                customization.Profile["Name"].Enabled = CBName.Checked;
                customization.Profile["Surname"].Enabled = CBSurname.Checked;
                customization.Profile["Lastname"].Enabled = CBLastname.Checked;
                customization.Profile["GenderBirthday"].Enabled = CBGenderBirthday.Checked;
                customization.Profile["Gender"].Enabled = CBGender.Checked;
                customization.Profile["Birthday"].Enabled = CBBirthday.Checked;
                customization.Profile["UserLocation"].Enabled = CBUserLocation.Checked;
                customization.Profile["Address"].Enabled = CBAddress.Checked;
                customization.Profile["Country"].Enabled = CBCountry.Checked;
                customization.Profile["Language"].Enabled = CBLanguage.Checked;
                customization.Profile["Personal"].Enabled = CBPersonal.Checked;
                customization.Profile["PersonalRelationship"].Enabled = CBPersonalRelationship.Checked;
                customization.Profile["PersonalAttractedTo"].Enabled = CBPersonalAttractedTo.Checked;
                customization.Profile["PersonalEyeColor"].Enabled = CBPersonalEyeColor.Checked;
                customization.Profile["PersonalHairColor"].Enabled = CBPersonalHairColor.Checked;
                customization.Profile["PersonalBodyHeight"].Enabled = CBPersonalBodyHeight.Checked;
                customization.Profile["PersonalBodyWeight"].Enabled = CBPersonalBodyWeight.Checked;
                customization.Profile["Phone"].Enabled = CBPhone.Checked;
                customization.Profile["PhonenumberMobile"].Enabled = CBPhonenumberMobile.Checked;
                customization.Profile["PhonenumberLandline"].Enabled = CBPhonenumberLandline.Checked;
                customization.Profile["Communication"].Enabled = CBCommunication.Checked;
                customization.Profile["CommunicationMSN"].Enabled = CBCommunicationMSN.Checked;
                customization.Profile["CommunicationYahoo"].Enabled = CBCommunicationYahoo.Checked;
                customization.Profile["CommunicationSkype"].Enabled = CBCommunicationSkype.Checked;
                customization.Profile["CommunicationICQ"].Enabled = CBCommunicationICQ.Checked;
                customization.Profile["CommunicationAIM"].Enabled = CBCommunicationAIM.Checked;
                customization.Profile["Web"].Enabled = CBWeb.Checked;
                customization.Profile["Homepage"].Enabled = CBHomepage.Checked;
                customization.Profile["Blog"].Enabled = CBBlog.Checked;
                customization.Profile["JobTalents"].Enabled = CBJobTalents.Checked;
                customization.Profile["Job"].Enabled = CBJob.Checked;
                customization.Profile["Slogan"].Enabled = CBSlogan.Checked;
                customization.Profile["Interests"].Enabled = CBInterests.Checked;

                customization.Modules["Messaging"].Enabled = CBMessaging.Checked;
                customization.Modules["Friends"].Enabled = CBFriends.Checked;
                customization.Modules["Comments"].Enabled = CBComments.Checked;
                customization.Modules["Alerts"].Enabled = CBAlerts.Checked;
                customization.Modules["Favorites"].Enabled = CBFavorites.Checked;
                customization.Modules["Memberships"].Enabled = CBMemberships.Checked;

                customization.Logins["FacebookUserId"].Enabled = CBFacebookUserId.Checked;
                customization.Logins["OpenID"].Enabled = CBOpenID.Checked;
                customization.Logins["InfoCard"].Enabled = CBInfoCard.Checked;

                Helper.SaveSectionToFile(string.Format(@"{0}\Configurations\Customization.config", WebRootPath.Instance.ToString()), "customizationSection", customization);
            }
            catch (Exception ex)
            {
                this.PnlMsg.Visible = true;
                this.LitMsg.Text = language.GetString("MessageSaveError") + "<br/>" + ex.Message;
            }
        }
    }
}
