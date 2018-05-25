<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Requests" CodeBehind="Requests.ascx.cs" %>
<%@ Register Src="~/UserControls/Dashboard/FriendActions.ascx" TagName="Actions" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="uc1" %>
<div id="dashboardFriends">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <uc1:Pager ID="pager1" PageSize="25" runat="server" />
            <div class="friends">
                <asp:Repeater ID="friendRepeater" runat="server" OnItemDataBound="OnFriendRequestItemDataBound">
                    <ItemTemplate>
                        <div class="friend">
                            <asp:Panel ID="FT" CssClass="friendType" runat="server" />
                            <center>
                                <asp:Panel ID="UD" CssClass="friendImage" runat="server" />
                            </center>
                            <asp:Panel ID="ACT" CssClass="friendActions" runat="server" />
                            <asp:Panel ID="FPAN" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageNoRequest")%>
            </asp:Panel>
            <uc1:Pager ID="pager2" PageSize="25" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="up" AssociatedUpdatePanelID="upnl" runat="server">
        <ProgressTemplate>
            <div class="updateProgress">
                <%=languageShared.GetString("LabelUpdateProgress")%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
