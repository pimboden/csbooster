<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.UserNavi" CodeBehind="UserNavi.ascx.cs" %>
<%@ Register Src="~/UserControls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="csb" %>
<a id="headerLogo" name="top" href="/"></a>
<csb:QuickSearch ID="QS" runat="server" />
<div id="userNav">
    <asp:HyperLink ID="LnkRss" runat="server" CssClass="rss" />
    <ul>
        <li id="LiProfile" runat="server" visible="false">
            <asp:HyperLink ID="LnkProfile" runat="server"><%=language.GetString("CommandNaviProfile")%></asp:HyperLink>
        </li>
        <asp:Literal ID="LitMenu" runat="server" />
    </ul>
</div>
