﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="OnlineUser.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.OnlineUser" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label" LabelFile="WidgetOnlineUser" LabelKey="LabelUserCount" ToolTipFile="WidgetOnlineUser" ToolTipKey="TooltipUserCount" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbUsers" Width="120" runat="server" MinValue="1" MaxValue="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>

