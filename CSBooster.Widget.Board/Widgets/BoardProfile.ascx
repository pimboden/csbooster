﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoardProfile.ascx.cs" Inherits="_4screen.CSB.Widget.BoardProfile" %>
<ul class="CTY_board" runat="server" id="PnlProperties">
    <li class="title">
        <%=language.GetString("LabelProperties")%> 
    </li>
    <li>
        <asp:HyperLink ID="LnkProf1" runat="server" ><%=language.GetString("CommandProfile1")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkProf3" runat="server" ><%=language.GetString("CommandProfile2")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlMessage">
    <li class="title">
        <%=language.GetString("LabelMessage")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkMsg1" runat="server" />
    </li>
    <li>
        <asp:HyperLink ID="LnkMsg2"  runat="server" ><%=language.GetString("MySentMessages")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkMsg3" runat="server"><%=language.GetString("WriteMessage")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlFriends">
    <li class="title">
        <%=language.GetString("LabelFriends")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkFre1" runat="server" />
    </li>
    <li>
        <asp:HyperLink ID="LnkFre2" runat="server" ><%=language.GetString("ShowMyFriends")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkFre3" runat="server" ><%=language.GetString("MyOpenFrienRequests")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkFre4" runat="server"><%=language.GetString("CommandMyBlockedUsers")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlSurvey">
    <li class="title">
        <%=language.GetString("LabelSurvey")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkTest1" runat="server" ><%=language.GetString("ShowMyTest")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlComments">
    <li class="title">
        <%=language.GetString("LabelComments")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkCom1" runat="server"><%=language.GetString("CommandMyCommentsReceived")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkCom2" runat="server" ><%=language.GetString("CommandMyCommentsPosted")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlContents">
    <li class="title">
        <%=language.GetString("LabelContents")%>
    </li>
    <asp:PlaceHolder ID="CNTPH" runat="server" />
</ul>
<ul class="CTY_board" runat="server" id="PnlMembership">
    <li class="title">
         <%=language.GetString("LabelMembership")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkMem1" runat="server" ><%=language.GetString("MyMemberships")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkMem2" runat="server" ><%=language.GetString("MyInvitations")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlNotifications">
    <li class="title">
        <%=language.GetString("LabelNotifications")%> 
    </li>
    <li>
        <asp:HyperLink ID="LnkAlerts1"  runat="server" ><%=language.GetString("ShowAlerts")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkAlerts2"  runat="server"><%=language.GetString("CommandConfigurations")%></asp:HyperLink>
    </li>
</ul>
<ul class="CTY_board" runat="server" id="PnlFavorites">
    <li class="title">
         <%=language.GetString("LabelFavorites")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkFav"  runat="server" ><%=language.GetString("CommandFavoritManage")%></asp:HyperLink>
    </li>
</ul>
