<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickSearch.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.QuickSearch" %>
<div id="headerSearch">
    <div class="headerSearchBox">
        <asp:TextBox ID="TxtSearch" Width="160" runat="server" />
    </div>
    <div class="headerSearchButton">
        <asp:HyperLink ID="LnkSearch" NavigateUrl='javascript:void(0);' runat="server" />
    </div>
</div>
