<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FunctionsUser.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FunctionsUser" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="FunctionsUser" LabelKey="LabelVisibility" ToolTipFile="FunctionsUser" ToolTipKey="TooltipVisibility" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div id="DivMessage" runat="server">
            <asp:CheckBox ID="CbxMessage" runat="server" /><label for="<%=CbxMessage.ClientID %>"><%=language.GetString("LabelMessage")%></label>
        </div>
        <div id="DivFriends" runat="server">
            <asp:CheckBox ID="CbxFriends" runat="server" /><label for="<%=CbxFriends.ClientID %>"><%=language.GetString("LabelFriends")%></label>
        </div>
        <div id="DivMembership" runat="server">
            <asp:CheckBox ID="CbxMembership" runat="server" /><label for="<%=CbxMembership.ClientID %>"><%=language.GetString("LabelMembership")%></label>
        </div>
        <div id="DivNotifications" runat="server">
            <asp:CheckBox ID="CbxNotifications" runat="server" /><label for="<%=CbxNotifications.ClientID %>"><%=language.GetString("LabelNotifications")%></label>
        </div>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
