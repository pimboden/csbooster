<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Alerts" CodeBehind="Alerts.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/Dashboard/AlertPreview.ascx" TagName="AlertPreview" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="dashboardAlerts">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:HiddenField ID="PBSortAttr" runat="server" />
            <asp:HiddenField ID="PBSortDir" runat="server" />
            <csb:Pager ID="pager1" PageSize="20" runat="server" />
            <table class="alerts" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        <%=language.GetString("LableTo")%>
                        <asp:LinkButton ID="userAscButton" CommandArgument="UserName Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="userDescButton" CommandArgument="UserName Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=language.GetString("LableMessage")%>
                        <asp:LinkButton ID="dateAscButton" CommandArgument="DateSent Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="dateDescButton" CommandArgument="DateSent Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=language.GetString("LableAction")%>
                    </th>
                    <th>
                        <%=language.GetString("LableState")%>
                    </th>
                </tr>
                <asp:Repeater ID="alertsRepeater" runat="server" OnItemDataBound="OnAlertItemDataBound">
                    <ItemTemplate>
                        <tr id="alertRow" runat="server" />
                        <td height="90" align="center">
                            <asp:Panel ID="UD" runat="server" />
                        </td>
                        <td class="alertPreview">
                            <asp:Panel ID="MP" runat="server" />
                        </td>
                        <td align="center">
                            <asp:Panel ID="DEL" runat="server" />
                        </td>
                        <td align="center">
                            <asp:Panel ID="STATE" runat="server" />
                        </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageNoAlerts")%>
            </asp:Panel>
            <csb:Pager ID="pager2" PageSize="20" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
