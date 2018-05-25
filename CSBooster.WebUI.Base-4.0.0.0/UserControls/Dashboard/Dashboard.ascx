<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="_4screen.CSB.WebUI.UserControls.Dashboard.Dashboard" %>
<div id="dashboard">
    <div class="dashboardBox1">
        <fieldset>
            <legend>
                <%=language.GetString("TitlePersonalStatistics") %>
            </legend>
            <ul>
                <li class="dashboardMessages">
                    <asp:HyperLink ID="lnkNewMessages" runat="server" />
                </li>
                <li class="dashboardRequests">
                    <asp:HyperLink ID="lnkNewRequests" runat="server" />
                </li>
                <li class="dashboardComments">
                    <asp:HyperLink ID="lnkNewComments" runat="server" />
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>
                <%=language.GetString("TitleProfileVisitors") %>
            </legend>
            <div>
                <asp:PlaceHolder ID="PhFriends" runat="server" />
            </div>
        </fieldset>
    </div>
    <div class="dashboardBox2">
        <fieldset>
            <legend>
                <%=language.GetString("TitleUserActivities") %>
            </legend>
            <asp:Panel ID="PnlInput" runat="server" Visible="true" CssClass="userActivitiesStatus">
                <div class="userActivitiesStatusElement">
                    <%=language.GetString("LableAddText")%>
                </div>
                <div class="userActivitiesStatusElement">
                    <asp:TextBox ID="TxtInput" runat="server" Width="200" MaxLength="256" />
                </div>
                <div class="userActivitiesStatusElement">
                    <asp:LinkButton ID="LbtnInput" CssClass="inputButton" OnClick="OnAddStatusClick" runat="server"><%=languageShared.GetString("CommandAdd")%></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlCnt" runat="server" CssClass="userActivities" />
        </fieldset>
    </div>
</div>
<div class="clearBoth">
</div>
