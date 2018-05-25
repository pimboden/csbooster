<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.ProfileFriendSetting" CodeBehind="ProfileFriendSetting.aspx.cs" %>

<%@ Register Src="~/Pages/Popups/UserControls/ProfileFriendSetting.ascx" TagName="ProfileFriendSetting" TagPrefix="uc1" %>
<asp:Content ID="cph1" ContentPlaceHolderID="Cnt1" runat="Server">
    <uc1:ProfileFriendSetting ID="PFS" runat="server" EnableViewState="False" />
</asp:Content>
