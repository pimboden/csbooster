<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.MyMemberships" CodeBehind="MyMemberships.ascx.cs" %>
<%@ Register Src="~/UserControls/ObjectDetailsSmall.ascx" TagName="ObjectDetailsSmall" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<div id="dashboardMemberships">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <csb:Pager ID="pager1" PageSize="10" runat="server" />
            <div class="memberships">
                <asp:Repeater ID="memRepeater" runat="server" OnItemDataBound="OnMembershipItemDataBound">
                    <ItemTemplate>
                        <asp:Panel ID="UD" runat="server" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageNoCommunityMembers")%>
            </asp:Panel>
            <csb:Pager ID="pager2" ItemNamePlural="Communities" ItemNameSingular="Community" PageSize="10" runat="server" Visible="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
