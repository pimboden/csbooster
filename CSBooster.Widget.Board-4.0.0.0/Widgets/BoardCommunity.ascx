﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoardCommunity.ascx.cs" Inherits="_4screen.CSB.Widget.BoardCommunity" %>
<ul>
    <li class="menuTitle">
         <%=language.GetString("LabelManage")%>
    </li>
    <li>
        <asp:HyperLink ID="LnkCty1" runat="server" ><%=language.GetString("LabelManageMembers")%></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="LnkCty2"  runat="server" ><%=language.GetString("LabelNewMsgToMembers")%></asp:HyperLink>
    </li>
</ul>
