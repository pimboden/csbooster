<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageEdit.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.PageEdit" %>
<ul id="functions" runat="server" visible="false" class="toolbarMenu">
    <li class="title">
        <web:TextControl ID="LabelSettings" runat="server" LanguageFile="UserControls.WebUI.Base" TextKey="LabelSettings" />
    </li>
    <asp:PlaceHolder ID="functionsPlaceHolder" runat="server" />
</ul>
