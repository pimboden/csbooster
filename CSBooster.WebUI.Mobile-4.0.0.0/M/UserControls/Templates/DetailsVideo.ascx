<%@ Control Language="C#" AutoEventWireup="True" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.DetailsVideo" CodeBehind="DetailsVideo.ascx.cs" %>
<li>
    <asp:HyperLink ID="lnkBack" runat="server" CssClass="back" />
    <span class="title">
        <asp:Literal ID="litBack" runat="server" /></span> </li>
<li class="detail detailObject">
    <div class="title">
        <asp:Literal ID="litTitle" runat="server" />
    </div>
    <div class="image">
        <asp:HyperLink ID="lnkVideo" runat="server" />
    </div>
    <div class="copyright">
        <asp:Literal ID="litCopyright" runat="server" />
    </div>
    <div class="desc">
        <asp:Literal ID="litDesc" runat="server" />
    </div>
    <asp:Panel ID="pnlGeoTag" runat="server" Visible="false">
        <asp:HyperLink ID="lnkGeoTag" runat="server" CssClass="button">
                <%=language.GetString("CommandShowOnMap")%>
        </asp:HyperLink>
    </asp:Panel>
</li>
