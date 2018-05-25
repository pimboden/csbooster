<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.DeleteObject" CodeBehind="DeleteObject.aspx.cs" %>

<%@ Import Namespace="_4screen.Utils.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
    <asp:Label ID="statusLabel" runat="server" Visible="true"><%=GuiLanguage.GetGuiLanguage("Shared").GetString("TextShowStateInProgress")%>....</asp:Label>
    <asp:Literal ID="litScript" runat="server" />
</asp:Content>
