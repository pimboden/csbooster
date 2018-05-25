<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SearchResults.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.SearchResults" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetSearchResults" LabelKey="LabelCount" ToolTipFile="WidgetSearchResults" ToolTipKey="TooltipCount" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbRes" runat="server" MinValue="1" MaxValue="10" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>