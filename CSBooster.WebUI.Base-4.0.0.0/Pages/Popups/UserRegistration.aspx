<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.UserRegistration" CodeBehind="UserRegistration.aspx.cs" %>
<%@ Register src="/UserControls/Templates/Registration.ascx" tagname="Registration" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
<uc1:Registration ID="Registration1" runat="server" />


</asp:Content>
