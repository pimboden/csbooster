<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="SettingsCommon.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.SettingsCommon" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
    <div class="CSB_admin_title">
        <%=language.GetString("TitleSettings")%>
    </div>
    <div class="CSB_admin_settings_box">
        <asp:CheckBox ID="CBCustomizationBar" runat="server" /><label for="<%=CBCustomizationBar.ClientID %>"><%=language.GetString("LableSettingsCustBar")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBWidgets" runat="server" /><label for="<%=CBWidgets.ClientID %>"><%=language.GetString("LableSettingsWidgets")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBLayout" runat="server" /><label for="<%=CBLayout.ClientID %>"><%=language.GetString("LableSettingsLayout")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBTheme" runat="server" /><label for="<%=CBTheme.ClientID %>"><%=language.GetString("LableSettingsTheme")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBStyle" runat="server" /><label for="<%=CBStyle.ClientID %>"><%=language.GetString("LableSettingsStyle")%></label>
    </div>
    <div class="CSB_admin_sep">
    </div>
    <div class="CSB_admin_title">
        Profil
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="CSB_admin_settings_table">
        <tr>
            <td><asp:CheckBox ID="CBChangePassword" runat="server" /><label for="<%=CBChangePassword.ClientID %>"><%=language.GetString("LableSettingsAllowPwdChanges")%></label></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBPermalink" runat="server" /><label for="<%=CBPermalink.ClientID %>"><%=language.GetString("LableSettingsShowPermalik")%></label></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBUILanguage" runat="server" /><label for="<%=CBUILanguage.ClientID %>"><%=language.GetString("LableSettingsUiLanguage")%></label></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBUserPoints" runat="server" /><label for="<%=CBUserPoints.ClientID %>"><%=language.GetString("LableSettingsShowPoints")%></label></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBDeleteAccount" runat="server" /><label for="<%=CBDeleteAccount.ClientID %>"><%=language.GetString("LableSettingsDeleteUser")%></label></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBPictureColors" runat="server" /><label for="<%=CBPictureColors.ClientID %>"><%=language.GetString("LableSettingsUserAndColor")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBPicture" runat="server" /><label for="<%=CBPicture.ClientID %>"><%=language.GetString("LableSettingsUserPicture")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBColors" runat="server" /><label for="<%=CBColors.ClientID %>"><%=language.GetString("LableSettingsUserColor")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBName" runat="server" /><label for="<%=CBName.ClientID %>"><%=language.GetString("LableSettingsNames")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBSurname" runat="server" /><label for="<%=CBSurname.ClientID %>"><%=language.GetString("LableSettingsFirstName")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBLastname" runat="server" /><label for="<%=CBLastname.ClientID %>"><%=language.GetString("LableSettingsName")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBGenderBirthday" runat="server" /><label for="<%=CBGenderBirthday.ClientID %>"><%=language.GetString("LableSettingsSexAndBirthday")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBGender" runat="server" /><label for="<%=CBGender.ClientID %>"><%=language.GetString("LableSettingsSex")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBBirthday" runat="server" /><label for="<%=CBBirthday.ClientID %>"><%=language.GetString("LableSettingsBirthday")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBUserLocation" runat="server" /><label for="<%=CBUserLocation.ClientID %>"><%=language.GetString("LableSettingsAddressAndLang")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBAddress" runat="server" /><label for="<%=CBAddress.ClientID %>"><%=language.GetString("LableSettingsAddress")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBCountry" runat="server" /><label for="<%=CBCountry.ClientID %>"><%=language.GetString("LableSettingsCountry")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBLanguage" runat="server" /><label for="<%=CBLanguage.ClientID %>"><%=language.GetString("LableSettingsLanguage")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBPersonal" runat="server" /><label for="<%=CBPersonal.ClientID %>"><%=language.GetString("LableSettingsPrivateData")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBPersonalRelationship" runat="server" /><label for="<%=CBPersonalRelationship.ClientID %>"><%=language.GetString("LableSettingsRelationship")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPersonalAttractedTo" runat="server" /><label for="<%=CBPersonalAttractedTo.ClientID %>"><%=language.GetString("LableSettingsTendency")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPersonalEyeColor" runat="server" /><label for="<%=CBPersonalEyeColor.ClientID %>"><%=language.GetString("LableSettingsEyeColor")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPersonalHairColor" runat="server" /><label for="<%=CBPersonalHairColor.ClientID %>"><%=language.GetString("LableSettingsHairColor")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPersonalBodyHeight" runat="server" /><label for="<%=CBPersonalBodyHeight.ClientID %>"><%=language.GetString("LableSettingsBodyHeight")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPersonalBodyWeight" runat="server" /><label for="<%=CBPersonalBodyWeight.ClientID %>"><%=language.GetString("LableSettingsBodyWeight")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBPhone" runat="server" /><label for="<%=CBPhone.ClientID %>"><%=language.GetString("LableSettingsTelefon")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBPhonenumberMobile" runat="server" /><label for="<%=CBPhonenumberMobile.ClientID %>"><%=language.GetString("LableSettingsMobil")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBPhonenumberLandline" runat="server" /><label for="<%=CBPhonenumberLandline.ClientID %>"><%=language.GetString("LableSettingsPhone")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBCommunication" runat="server" /><label for="<%=CBCommunication.ClientID %>"><%=language.GetString("LableSettingsCommunication")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBCommunicationMSN" runat="server" /><label for="<%=CBCommunicationMSN.ClientID %>"><%=language.GetString("LableSettingsMSN")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBCommunicationYahoo" runat="server" /><label for="<%=CBCommunicationYahoo.ClientID %>"><%=language.GetString("LableSettingsYahoo")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBCommunicationSkype" runat="server" /><label for="<%=CBCommunicationSkype.ClientID %>"><%=language.GetString("LableSettingsSkype")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBCommunicationICQ" runat="server" /><label for="<%=CBCommunicationICQ.ClientID %>"><%=language.GetString("LableSettingsICQ")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBCommunicationAIM" runat="server" /><label for="<%=CBCommunicationAIM.ClientID %>"><%=language.GetString("LableSettingsAIM")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBWeb" runat="server" /><label for="<%=CBWeb.ClientID %>"><%=language.GetString("LableSettingsWeb")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBHomepage" runat="server" /><label for="<%=CBHomepage.ClientID %>"><%=language.GetString("LableSettingsHomepage")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBBlog" runat="server" /><label for="<%=CBBlog.ClientID %>"><%=language.GetString("LableSettingsBlog")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBJobTalents" runat="server" /><label for="<%=CBJobTalents.ClientID %>"><%=language.GetString("LableSettingsJobMotto")%></label></td>
            <td>
                <div>
                    <asp:CheckBox ID="CBJob" runat="server" /><label for="<%=CBJob.ClientID %>"><%=language.GetString("LableSettingsJob")%></label>
                </div>
                <div>
                    <asp:CheckBox ID="CBSlogan" runat="server" /><label for="<%=CBSlogan.ClientID %>"><%=language.GetString("LableSettingsSlogan")%></label>
                </div>
            </td>
        </tr>
        <tr>
            <td><asp:CheckBox ID="CBInterests" runat="server" /><label for="<%=CBInterests.ClientID %>"><%=language.GetString("LableSettingsInterests")%></label></td>
            <td></td>
        </tr>
    </table>
    <div class="CSB_admin_sep">
    </div>
    <div class="CSB_admin_title">
        <%=language.GetString("LableSettingsModule")%>
    </div>
    <div class="CSB_admin_settings_box">
        <asp:CheckBox ID="CBMessaging" runat="server" /><label for="<%=CBMessaging.ClientID %>"><%=language.GetString("LableSettingsMessages")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBFriends" runat="server" /><label for="<%=CBFriends.ClientID %>"><%=language.GetString("LableSettingsFriends")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBComments" runat="server" /><label for="<%=CBComments.ClientID %>"><%=language.GetString("LableSettingsComments")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBAlerts" runat="server" /><label for="<%=CBAlerts.ClientID %>"><%=language.GetString("LableSettingsAlerts")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBFavorites" runat="server" /><label for="<%=CBFavorites.ClientID %>"><%=language.GetString("LableSettingsFavorites")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBMemberships" runat="server" /><label for="<%=CBMemberships.ClientID %>"><%=language.GetString("LableSettingsMemberships")%></label>
    </div>
    <div class="CSB_admin_sep">
    </div>
    <div class="CSB_admin_title">
        <%=language.GetString("LableSettingsLogins")%>
    </div>
    <div class="CSB_admin_settings_box">
        <asp:CheckBox ID="CBFacebookUserId" runat="server" /><label for="<%=CBFacebookUserId.ClientID %>"><%=language.GetString("LableSettingsFacebookUserId")%></label>
    </div>
    <div class="CSB_admin_settings_box">
        <asp:CheckBox ID="CBOpenID" runat="server" /><label for="<%=CBOpenID.ClientID %>"><%=language.GetString("LableSettingsOpenID")%></label>
    </div>
    <div class="CSB_admin_settings_box2">
        <asp:CheckBox ID="CBInfoCard" runat="server" /><label for="<%=CBInfoCard.ClientID %>"><%=language.GetString("LableSettingsInfoCard")%></label>
    </div>
    <div class="CSB_admin_sep">
    </div>
    <div>
        <asp:LinkButton ID="LbtnSave" CssClass="CSB_admin_button" runat="server" Text="Speichern" OnClick="OnSaveClick"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("CommandSave")%></asp:LinkButton>
    </div>
    <asp:Panel ID="PnlMsg" runat="server" CssClass="CSB_admin_error" Visible="false">
        <asp:Label ID="LitMsg" runat="server" />
    </asp:Panel>
</asp:Content>
