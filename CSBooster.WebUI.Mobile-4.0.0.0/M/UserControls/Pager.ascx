<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Pager" %>
<li class="pager">
    <asp:HyperLink ID="lnkPrevious" runat="server" CssClass="pagerPrevious"><%=language.GetString("CommandPrevious")%></asp:HyperLink><asp:HyperLink ID="lnkNext" runat="server" CssClass="pagerNext"><%=language.GetString("CommandNext")%></asp:HyperLink>
</li>
