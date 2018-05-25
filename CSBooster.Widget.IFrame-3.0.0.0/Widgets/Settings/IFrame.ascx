<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="IFrame.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.IFrame" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="lbl1" EnableViewState="false" LabelFile="WidgetIFrame" LabelKey="LabelUrl" ToolTipFile="WidgetIFrame" ToolTipKey="TooltipUrl" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="TxtIFrameURL" Width="99%" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="lbl2" EnableViewState="false" LabelFile="WidgetIFrame" LabelKey="LabelShowFrame" ToolTipFile="WidgetIFrame" ToolTipKey="TooltipShowFrame" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="CbBorder" runat="server" Text="" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="lbl3" EnableViewState="false" LabelFile="WidgetIFrame" LabelKey="LabelHeight" ToolTipFile="WidgetIFrame" ToolTipKey="TooltipHeight" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbHeight" Width="120" runat="server" MinValue="100" MaxValue="1000" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" IncrementSettings-Step="50" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>