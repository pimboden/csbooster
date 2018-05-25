<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionsUser.ascx.cs" Inherits="_4screen.CSB.Widget.FunctionsUser" %>
<asp:HyperLink ID="RECLINK" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server"><%=languageShared.GetString("CommandPropose")%></asp:HyperLink>
<asp:HyperLink ID="MAPLINK" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandShowMap")%></asp:HyperLink>
<asp:Panel ID="FUNCP2" runat="server">
   <asp:HyperLink ID="ALELINK" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server"><%=languageShared.GetString("CommandAlerts")%></asp:HyperLink>
   <asp:HyperLink ID="LnkJoin" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandBecomeAMember")%></asp:HyperLink>
   <asp:HyperLink ID="LnkInvite" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandInviteFriend")%></asp:HyperLink>
   <asp:HyperLink ID="LnkFriendRequest" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandFriendshipQuery")%></asp:HyperLink>
</asp:Panel>
<asp:HyperLink ID="REPLINK" CssClass="CSB_button1" Style="margin-top: 5px;" runat="server" Visible="false"><%=languageShared.GetString("CommandContentReport")%></asp:HyperLink>