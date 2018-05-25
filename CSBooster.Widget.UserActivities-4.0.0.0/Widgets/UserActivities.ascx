<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserActivities.ascx.cs" Inherits="_4screen.CSB.Widget.UserActivities" %>
<asp:Panel ID="PnlInput" runat="server" Visible="true" CssClass="userActivitiesStatus">
    <div class="userActivitiesStatusElement">
        <web:labelcontrol id="LableAddText" languagefile="WidgetUserActivities" labelkey="LableAddText" tooltipkey="TooltipAddText" runat="server" />
    </div>
    <div class="userActivitiesStatusElement">
        <asp:TextBox ID="TxtInput" runat="server" Width="200" MaxLength="256" />
    </div>
    <div class="userActivitiesStatusElement">
        <asp:LinkButton ID="LbtnInput" CssClass="inputButton" OnClick="LbtnInput_Click1" runat="server">
            <web:textcontrol id="CommandAdd" languagefile="Shared" textkey="CommandAdd" runat="server" />
        </asp:LinkButton>
    </div>
</asp:Panel>
<asp:Panel ID="PnlCnt" runat="server" CssClass="userActivities" />
<div class="clearBoth">
</div>
