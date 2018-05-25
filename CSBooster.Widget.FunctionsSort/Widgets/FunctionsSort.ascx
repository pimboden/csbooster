<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionsSort.ascx.cs" Inherits="_4screen.CSB.Widget.FunctionsSort" %>
<div>
   <div>
      <asp:HyperLink ID="ByTitle" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByTitle")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByActivity" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByActivity") %></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByMember" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByMember")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByDate" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByDate")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByVisits" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByVisits")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByRatings" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByRatings")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByRetingConsolidated" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByRatingConsolidated")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByComments" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByComments")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="ByLinks" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandByLinks")%></asp:HyperLink>
   </div>
   <div>
      <asp:HyperLink ID="NoSort" CssClass="CSB_admin_menu_a" runat="server" Visible="false"><%=language.GetString("CommandSetDefault")%></asp:HyperLink>
   </div>
</div>