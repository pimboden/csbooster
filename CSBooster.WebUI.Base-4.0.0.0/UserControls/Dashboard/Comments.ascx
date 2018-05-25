<%@ Control Language="C#" AutoEventWireup="true" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Comments" CodeBehind="Comments.ascx.cs" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Dashboard/CommentPreview.ascx" TagName="CommentPreview" TagPrefix="csb" %>
<%@ Register Src="~/UserControls/Templates/SmallOutputUser2.ascx" TagName="SmallUserOutput" TagPrefix="csb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="dashboardComments">
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="PBPageNum" runat="server" />
            <asp:HiddenField ID="PBSortAttr" runat="server" />
            <asp:HiddenField ID="PBSortDir" runat="server" />
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
            <div class="dashboardSearch">
                <div class="dashboardSearchOptionsSwitch">
                    <%=language.GetString("LableSearchOptions")%>
                    <asp:LinkButton ID="showOptButton" OnClick="OnShowSearchOptionsClick" runat="server"><%=languageShared.GetString("CommandShow")%></asp:LinkButton>
                    <asp:LinkButton ID="hideOptButton" OnClick="OnHideSearchOptionsClick" runat="server"><%=languageShared.GetString("CommandClose")%></asp:LinkButton>
                </div>
                <asp:Panel ID="search" runat="server" CssClass="dashboardSearchOptions" Visible="false">
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="LAB1" Text="Von:" runat="server" />
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBUserName" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="LAB2" Text="Text:" runat="server" />
                        </div>
                        <div class="inputBlockContent">
                            <asp:TextBox ID="PBText" runat="server" />
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="LAB4" Text="Datum von:" runat="server" />
                        </div>
                        <div class="inputBlockContent">
                            <telerik:RadDatePicker ID="PBCreatedFrom" runat="server" MaxDate="2020-12-31" MinDate="2007-01-01">
                                <DatePopupButton ImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" HoverImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="inputBlock">
                        <div class="inputBlockLabel">
                            <asp:Label ID="LAB5" Text="Datum bis:" runat="server" />
                        </div>
                        <div class="inputBlockContent">
                            <telerik:RadDatePicker ID="PBCreatedTo" runat="server" MaxDate="2020-12-31" MinDate="2007-01-01">
                                <DatePopupButton ImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" HoverImageUrl="~/Library/Skins/Custom/DatePicker/Custom.gif" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <csb:Pager ID="pager1" PageSize="10" runat="server" />
            <table class="comments" cellpadding="0" cellspacing="0">
                <tr>
                    <th><asp:Label ID="USRLAB" runat="server" />
                        <asp:LinkButton ID="userAscButton" CommandArgument="UserName Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="userDescButton" CommandArgument="UserName Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=languageShared.GetString("TitleComment")%>
                        <asp:LinkButton ID="commAscButton" CommandArgument="DateCreated Asc" OnClick="OnSortClick" runat="Server" />
                        <asp:LinkButton ID="commDescButton" CommandArgument="DateCreated Desc" OnClick="OnSortClick" runat="Server" />
                    </th>
                    <th>
                        <%=languageShared.GetString("TitleObject")%>
                    </th>
                </tr>
                <asp:Repeater ID="comments" runat="server" OnItemDataBound="OnCommentItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td height="90" align="center">
                                <asp:Panel ID="UD" runat="server" />
                            </td>
                            <td class="commentPreview" valign="top">
                                <asp:Panel ID="CP" runat="server" />
                            </td>
                            <td class="commentObject" align="center" valign="top">
                                <asp:Panel ID="COP" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="noitem" runat="server">
                <%=languageShared.GetString("MessageNoComment")%>
            </asp:Panel>
            <csb:Pager ID="pager2" PageSize="10" runat="server" />
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
