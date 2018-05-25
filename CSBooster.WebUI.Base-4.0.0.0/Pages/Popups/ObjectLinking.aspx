<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.Pages.Popups.ObjectLinking" Codebehind="ObjectLinking.aspx.cs" %>

<%@ Register Src="~/Pages/Popups/UserControls/ObjectLinking.ascx" TagName="ObjectLinking" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cnt1" runat="Server">
   <uc1:ObjectLinking ID="OBJLINK" runat="server" />
</asp:Content>
