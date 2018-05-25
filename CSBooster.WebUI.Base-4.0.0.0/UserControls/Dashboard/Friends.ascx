<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Friends" CodeBehind="Friends.ascx.cs" %>
<%@ Register Src="~/UserControls/Dashboard/FriendActions.ascx" TagName="Actions" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<div id="dashboardFriends">
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
                <div class="dashboardToolsActions">
                    <asp:DropDownList ID="ddlActions" runat="server" Width="150" AutoPostBack="true" OnTextChanged="OnActionSelected" OnSelectedIndexChanged="OnActionSelected">
                        <asp:ListItem Value="None" Text="Aktionen" Selected="True" />
                        <asp:ListItem Value="Delete" Text="Selektierte Löschen" />
                        <asp:ListItem Value="None2" Text="Freundschaft ändern in ..." style="color: green" />
                    </asp:DropDownList>
                </div>
                <div class="dashboardToolsActions">
                    <%=language.GetString("LableSelectAll")%>
                    <input type="CheckBox" name="SELALL" onclick="SelectAll(this, 'YMSEL')" />
                </div>
            </div>
            <div class="dashboardSearch">
                <div class="dashboardSearchOptionsSwitch">
                    <%=language.GetString("LableSearchOptions")%>
                    <asp:LinkButton ID="showOptButton" OnClick="OnShowSearchOptionsClick" runat="server"><%=languageShared.GetString("CommandShow")%></asp:LinkButton>
                    <asp:LinkButton ID="hideOptButton" OnClick="OnHideSearchOptionsClick" runat="server"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
                </div>
                <asp:Panel ID="search" runat="server" CssClass="dashboardSearchOptions" Visible="false">
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableName")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBUserName" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableFriendship")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:DropDownList ID="PBFriendTypeId" runat="server" Width="150">
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <csb:Pager ID="pager1" PageSize="25" runat="server" />
            <div class="friends">
                <asp:Repeater ID="friendRepeater" runat="server" OnItemDataBound="OnFriendItemDataBound">
                    <ItemTemplate>
                        <div class="friend">
                            <asp:Panel ID="FT" CssClass="friendType" runat="server" />
                            <center>
                                <asp:Panel ID="UD" CssClass="friendImage" runat="server" />
                            </center>
                            <asp:Panel ID="ACT" CssClass="friendActions" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageNoFriend")%>
            </asp:Panel>
            <csb:Pager ID="pager2" PageSize="25" runat="server" />
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
