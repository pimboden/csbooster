<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsBasic.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.SettingsBasic" %>
<asp:PlaceHolder ID="PhProperties" runat="server" />
<asp:LinkButton ID="lbtnSave" runat="server" CssClass="inputButton" OnClick="OnSaveClick"><%=languageShared.GetString("CommandSave")%></asp:LinkButton>
