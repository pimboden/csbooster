<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsEvent.ascx.cs" Inherits="_4screen.CSB.WebUI.M.UserControls.Templates.DetailsEvent" %>
<li>
    <asp:HyperLink ID="lnkBack" runat="server" CssClass="back" />
    <span class="title">
        <asp:Literal ID="litBack" runat="server" /></span>
    <asp:HyperLink ID="lnkEdit" runat="server" CssClass="edit" Visible="false"><%=language.GetString("CommandEdit")%></asp:HyperLink>
</li>
<li class="detail eventDetail">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="10%" class="eventDetailLabel"><%=language.GetString("LabelEvent")%></td>
            <td width="90%">
                <div class="eventTitle">
                    <asp:Literal ID="LitTitle" runat="server" />
                </div>
                <asp:Image ID="Img" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="eventDetailLabel"><%=language.GetString("LabelLocation")%></td>
            <td>
                <asp:HyperLink ID="LnkLocation" runat="server" CssClass="button" />
                <br />
                <asp:Literal ID="LitAddress" runat="server" />
                <asp:Panel ID="pnlGeoTag" runat="server" Visible="false" CssClass="inputGeoTagging">
                    <asp:HyperLink ID="lnkGeoTag" runat="server" CssClass="button">
                            <%=language.GetString("CommandShowOnMap")%>
                    </asp:HyperLink>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="eventDetailLabel"><%=language.GetString("LabelWhen")%></td>
            <td>
                <asp:Literal ID="LitWhen" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="eventDetailLabel"><%=language.GetString("LabelEventType")%></td>
            <td>
                <asp:Literal ID="LitType" runat="server" />
            </td>
        </tr>
        <tr id="TrWebsite" runat="server" visible="false">
            <td class="eventDetailLabel"><%=language.GetString("LabelWebsite")%></td>
            <td>
                <asp:HyperLink ID="LnkWebsite" runat="server" />
            </td>
        </tr>
        <tr id="TrAge" runat="server" visible="false">
            <td class="eventDetailLabel"><%=language.GetString("LabelAge")%></td>
            <td>
                <asp:Literal ID="LitAge" runat="server" />
            </td>
        </tr>
        <tr id="TrPrice" runat="server" visible="false">
            <td class="eventDetailLabel"><%=language.GetString("LabelPrice")%></td>
            <td>
                <asp:Literal ID="LitPrice" runat="server" />
            </td>
        </tr>
        <tr id="TrDesc" runat="server" visible="false">
            <td class="eventDetailLabel"><%=language.GetString("LabelDescription")%></td>
            <td colspan="2">
                <div>
                    <asp:Literal ID="LitDesc" runat="server" />
                </div>
                <div>
                    <asp:Literal ID="LitEvent" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</li>
