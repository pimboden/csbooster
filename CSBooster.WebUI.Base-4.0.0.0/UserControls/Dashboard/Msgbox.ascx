<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Msgbox" CodeBehind="Msgbox.ascx.cs" %>
<%@ Register Src="~/UserControls/Dashboard/MessagePreview.ascx" TagName="MessagePreview" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/MessageActions.ascx" TagName="Actions" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="dashboardMessageBox">
    <asp:HiddenField ID="PBGrpID" runat="server" />
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:HiddenField ID="PBSortAttr" runat="server" />
            <asp:HiddenField ID="PBSortDir" runat="server" />
            <asp:HiddenField ID="PBSearchOptions" runat="server" />
            <div class="dashboardTools">
                <div class="dashboardToolsSearch">
                    <asp:TextBox ID="PBGenSearchParam" runat="server" Width="150" />
                </div>
                <div class="dashboardToolsSearch">
                    <asp:LinkButton ID="findButton" CssClass="inputButton" OnClick="OnSearchClick" runat="server"><%=languageShared.GetString("CommandSearch")%></asp:LinkButton>
                    <asp:LinkButton ID="resetButton" CssClass="inputButtonSecondary" OnClick="OnResetSearchClick" runat="server"><%=languageShared.GetString("CommandReset")%></asp:LinkButton>
                </div>
                <div class="dashboardToolsActions">
                    <asp:DropDownList ID="ddlActions" runat="server" Width="150" AutoPostBack="true" OnTextChanged="OnActionSelected" OnSelectedIndexChanged="OnActionSelected">
                        <asp:ListItem Value="None" />
                        <asp:ListItem Value="Delete" />
                        <asp:ListItem Value="ToggleFlag" />
                    </asp:DropDownList>
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
                            <asp:Label ID="LAB1" runat="server" />
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBUserName" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableSubject")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBSubject" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableMessage")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBMessage" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableDateFrom")%>
                        </div>
                        <div class="inputBlockContent">
                            <telerik:RadDatePicker ID="PBDateSentFrom" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableDateTo")%>
                        </div>
                        <div class="inputBlockContent">
                            <telerik:RadDatePicker ID="PBDateSentTo" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableMarked")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:CheckBox ID="PBFlagged" runat="Server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <%=language.GetString("LableUnread")%>
                        </div>
                        <div class="inputBlockContent">
                            <asp:CheckBox ID="PBIsRead" runat="Server" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <csb:Pager ID="pager1" PageSize="20" runat="server" />
            <table class="messageBox" cellpadding="0" cellspacing="0">
                <tr>
                    <th><asp:Label ID="USRLAB" runat="server" />
                        <asp:LinkButton ID="userAscButton" CommandArgument="UserName Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="userDescButton" CommandArgument="UserName Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=language.GetString("LableMessage")%>
                        <asp:LinkButton ID="msgAscButton" CommandArgument="DateSent Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="msgDescButton" CommandArgument="DateSent Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=language.GetString("LableAction")%>
                    </th>
                    <th><input type="CheckBox" name="SELALL" onclick="SelectAll(this, 'SEL')" /> </th>
                    <th>
                        <%=language.GetString("LableState")%>
                    </th>
                </tr>
                <asp:Repeater ID="msgbox" runat="server" OnItemDataBound="OnMessageItemDataBound">
                    <ItemTemplate>
                        <tr id="messageRow" runat="server">
                            <td height="90" align="center">
                                <asp:PlaceHolder ID="UD" runat="server" />
                            </td>
                            <td class="messageBoxPreview" valign="top">
                                <asp:PlaceHolder ID="MP" runat="server" />
                            </td>
                            <td align="center">
                                <asp:PlaceHolder ID="ACT" runat="server" />
                            </td>
                            <td align="center"><input type="checkbox" name="SEL" value="<%# Eval("MsgID") %>" /> </td>
                            <td align="center">
                                <asp:Panel ID="STATE" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="noitem" runat="server">
                <%=language.GetString("MessageNoMessage")%>
            </asp:Panel>
            <csb:Pager ID="pager2" PageSize="20" runat="server" />
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
