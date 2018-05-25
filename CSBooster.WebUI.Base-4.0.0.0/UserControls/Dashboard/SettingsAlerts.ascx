<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsAlerts.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.SettingsAlerts" %>
<asp:PlaceHolder ID="PhMessaging" runat="server" />
<asp:LinkButton ID="lbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
