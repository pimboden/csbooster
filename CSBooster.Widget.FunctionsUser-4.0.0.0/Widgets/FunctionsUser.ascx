<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionsUser.ascx.cs" Inherits="_4screen.CSB.Widget.FunctionsUser" %>
<asp:HyperLink ID="RECLINK" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server"><%=languageShared.GetString("CommandRecommend")%></asp:HyperLink>
<asp:HyperLink ID="MAPLINK" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandShowMap")%></asp:HyperLink>
<asp:Panel ID="FUNCP2" runat="server">
   <asp:HyperLink ID="ALELINK" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server"><%=languageShared.GetString("CommandAlerts")%></asp:HyperLink>
   <asp:HyperLink ID="LnkJoin" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandBecomeAMember")%></asp:HyperLink>
   <asp:HyperLink ID="LnkInvite" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandInviteFriend")%></asp:HyperLink>
   <asp:HyperLink ID="LnkFriendRequest" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandFriendshipQuery")%></asp:HyperLink>
</asp:Panel>
<asp:HyperLink ID="REPLINK" CssClass="inputButton" Style="margin-bottom: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandContentReport")%></asp:HyperLink>
