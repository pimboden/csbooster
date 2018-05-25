<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="UserExport.aspx.cs" Inherits="_4screen.CSB.WebUI.Admin.UserExport" %>

<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="server">
<asp:LinkButton ID="LbtnExport" runat="server" CssClass="CSB_admin_button" OnClick="OnExportUsersClick">Benutzerliste herunterladen</asp:LinkButton>
</asp:Content>