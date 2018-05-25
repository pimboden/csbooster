<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyFavorites" CodeBehind="MyFavorites.ascx.cs" %>
<%@ Register Src="~/UserControls/ObjectDetailsSmall.ascx" TagName="ObjectDetailsSmall" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<div id="dashboardFavorites">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <csb:Pager ID="pager1" PageSize="20" runat="server" />
            <table class="favorites" cellpadding="0" cellspacing="0">
                <asp:Repeater ID="favRepeater" runat="server" EnableViewState="False" OnItemDataBound="OnFavoriteItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litTyp" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Literal ID="litTit" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Literal ID="LitUser" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:PlaceHolder ID="phDel" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="noitem" runat="server">
                 <%=language.GetString("MessageNoFavorites")%>
                 </asp:Panel>
            <csb:Pager ID="pager2" PageSize="20" runat="server" Visible="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
