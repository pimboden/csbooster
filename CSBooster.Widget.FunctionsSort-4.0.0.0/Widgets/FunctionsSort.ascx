<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionsSort.ascx.cs" Inherits="_4screen.CSB.Widget.FunctionsSort" %>
<div class="menu">
    <ul>
        <li id="LiTitle" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByTitle" runat="server"><%=language.GetString("CommandByTitle")%></asp:HyperLink>
        </li>
        <li id="LiActivity" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByActivity" runat="server"><%=language.GetString("CommandByActivity") %></asp:HyperLink>
        </li>
        <li id="LiMember" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByMember" runat="server"><%=language.GetString("CommandByMember")%></asp:HyperLink>
        </li>
        <li id="LiDate" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByDate" runat="server"><%=language.GetString("CommandByDate")%></asp:HyperLink>
        </li>
        <li id="LiVisits" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByVisits" runat="server"><%=language.GetString("CommandByVisits")%></asp:HyperLink>
        </li>
        <li id="LiRatings" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByRatings" runat="server"><%=language.GetString("CommandByRatings")%></asp:HyperLink>
        </li>
        <li id="LiRatingConsolidated" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByRatingConsolidated" runat="server"><%=language.GetString("CommandByRatingConsolidated")%></asp:HyperLink>
        </li>
        <li id="LiComments" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByComments" runat="server"><%=language.GetString("CommandByComments")%></asp:HyperLink>
        </li>
        <li id="LiLinks" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="ByLinks" runat="server"><%=language.GetString("CommandByLinks")%></asp:HyperLink>
        </li>
        <li id="LiSort" runat="server" class="inactive" visible="false">
            <asp:HyperLink ID="NoSort" runat="server"><%=language.GetString("CommandSetDefault")%></asp:HyperLink>
        </li>
    </ul>
</div>
