<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResultDefault.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Templates.SearchResultDefault" %>
<div class="searchResultOutput">
    <div class="searchResultOutputTitle">
        <asp:HyperLink ID="LnkTitle" runat="server" />
    </div>
    <div class="searchResultOutputDesc">
        <asp:HyperLink ID="LnkDesc" runat="server">
            <asp:Image ID="Img" runat="server" Visible="false" />
            <asp:Literal ID="LitDesc" runat="server" />
        </asp:HyperLink>
    </div>
</div>
