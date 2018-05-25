<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.BlockedUsers" CodeBehind="BlockedUsers.ascx.cs" %>
<%@ Import Namespace="_4screen.Utils.Web" %>
<%@ Register Src="~/UserControls/Dashboard/BlockedUsersActions.ascx" TagName="Actions" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<div id="dashboardBlockedUsers">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:HiddenField ID="PBSearchOptions" runat="server" />
            <div class="dashboardTools">
                <div class="dashboardToolsSearch">
                    <asp:TextBox ID="PBGenSearchParam" runat="server" />
                </div>
                <div class="dashboardToolsSearch">
                    <asp:LinkButton ID="findButton" CssClass="inputButton" OnClick="OnSearchClick" runat="server"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
                    <asp:LinkButton ID="resetButton" CssClass="inputButtonSecondary" OnClick="OnResetSearchClick" runat="server"><%=languageShared.GetString("CommandReset")%></asp:LinkButton>
                </div>
            </div>
            <csb:Pager ID="pager1" PageSize="25" runat="server" />
            <div class="blockedUsers">
                <asp:Repeater ID="blockedUserRepeater" runat="server" OnItemDataBound="OnBlockedUserItemDataBound">
                    <ItemTemplate>
                        <div class="blockedUser">
                            <center>
                                <asp:Panel ID="UD" CssClass="blockedUserImage" runat="server" />
                            </center>
                            <asp:Panel ID="ACT" CssClass="blockedUserActions" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageBlockedUsers")%>
            </asp:Panel>
            <csb:Pager ID="pager2" PageSize="25" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=GuiLanguage.GetGuiLanguage("Shared").GetString("LabelUpdateProgress")%></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
