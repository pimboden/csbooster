<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Rating.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Rating" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetRating" LabelKey="LabelShowInfo" ToolTipFile="WidgetRating" ToolTipKey="TooltipShowInfo" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
            <asp:CheckBox ID="CbxShowInfo" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>