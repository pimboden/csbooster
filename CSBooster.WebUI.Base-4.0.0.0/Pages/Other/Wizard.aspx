<%@ Page Language="C#" MasterPageFile="~/MasterPages/Empty.master" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.Pages.Other.Wizard" CodeBehind="Wizard.aspx.cs" %>

<%@ Register Src="~/UserControls/Wizards/Wizard.ascx" TagName="Wizard" TagPrefix="csb" %>
<asp:Content ID="Cnt" ContentPlaceHolderID="Cnt1" runat="Server">
    <telerik:radformdecorator id="Rfd" runat="server" enableroundedcorners="false" decoratedcontrols="all" />
    <csb:Wizard ID="Wiz" runat="server" />
</asp:Content>
