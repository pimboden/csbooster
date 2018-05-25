<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsInterests.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.SettingsInterests" %>
<asp:PlaceHolder ID="PhInterests" runat="server" />
<asp:LinkButton ID="lbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
