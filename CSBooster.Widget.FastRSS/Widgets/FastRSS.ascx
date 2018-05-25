<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FastRSS.ascx.cs" Inherits="_4screen.CSB.Widget.FastRSS" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:UpdatePanel runat="server" ID="upPanl"  UpdateMode="Conditional">
	<contenttemplate>
<asp:Panel ID="RssContainer" runat="server"></asp:Panel>
</contenttemplate>
</asp:UpdatePanel>