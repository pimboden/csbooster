<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="UserLists.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.UserLists" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetUserLists" LabelKey="LabelUserCount" ToolTipFile="WidgetUserLists" ToolTipKey="TooltipUserCount" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbUsers" Width="120" runat="server" MinValue="1" MaxValue="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetUserLists" LabelKey="LabelPager" ToolTipFile="WidgetUserLists" ToolTipKey="TooltipPager" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <asp:CheckBox ID="CbxPagerTop" runat="server" /><label for="<%=CbxPagerTop.ClientID %>" ><%=language.GetString("LabelPagerTop")%></label>  
        </div>
        <div>
            <asp:CheckBox ID="CbxPagerBottom" runat="server" /><label for="<%=CbxPagerBottom.ClientID %>" ><%=language.GetString("LabelPagerBottom")%></label>  
        </div>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>