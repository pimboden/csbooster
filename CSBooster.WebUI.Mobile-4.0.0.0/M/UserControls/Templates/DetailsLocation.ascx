<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsLocation.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.DetailsLocation" %>
<li>
    <asp:HyperLink ID="lnkBack" runat="server" CssClass="back" />
    <span class="title">
        <asp:Literal ID="litBack" runat="server" /></span>
    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="edit" Visible="false"><%=language.GetString("CommandEdit")%></asp:HyperLink>
</li>
<li class="detail locationDetail">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="10%" class="locationDetailLabel"><%=language.GetString("LabelLocation")%></td>
            <td width="90%">
                <div class="locationTitle">
                    <asp:Literal ID="LitTitle" runat="server" />
                </div>
                <asp:Image ID="Img" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="locationDetailLabel"><%=language.GetString("LabelAddress")%></td>
            <td>
                <asp:Literal ID="LitAddress" runat="server" />
                <asp:Panel ID="pnlGeoTag" runat="server" Visible="false" CssClass="inputGeoTagging">
                    <asp:HyperLink ID="lnkGeoTag" runat="server" CssClass="button">
                            <%=language.GetString("CommandShowOnMap")%>
                    </asp:HyperLink>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="locationDetailLabel"><%=language.GetString("LabelLocationType")%></td>
            <td colspan="2">
                <asp:Literal ID="LitType" runat="server" />
            </td>
        </tr>
        <tr id="TrWebsite" runat="server" visible="false">
            <td class="locationDetailLabel"><%=language.GetString("LabelWebsite")%></td>
            <td colspan="2">
                <asp:HyperLink ID="LnkWebsite" runat="server" />
            </td>
        </tr>
        <tr id="TrDesc" runat="server" visible="false">
            <td class="locationDetailLabel"><%=language.GetString("LabelDescription")%></td>
            <td colspan="2">
                <asp:Literal ID="LitDesc" runat="server" />
            </td>
        </tr>
    </table>
</li>
