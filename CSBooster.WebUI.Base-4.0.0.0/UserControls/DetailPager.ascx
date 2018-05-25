<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailPager.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.DetailPager" %>
<div style="margin-top: 10px; text-align: center;">
    <asp:HyperLink ID="LnkPrevious" runat="server" CssClass="inputButton">
        <web:TextControl ID="CommandPrevious" runat="server" LanguageFile="Shared" TextKey="CommandPrevious" />
    </asp:HyperLink>
    <asp:HyperLink ID="LnkNext" runat="server" CssClass="inputButton">
        <web:TextControl ID="CommandNext" runat="server" LanguageFile="Shared" TextKey="CommandNext" />
    </asp:HyperLink>
</div>
