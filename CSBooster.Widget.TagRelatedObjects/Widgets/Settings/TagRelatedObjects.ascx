<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="TagRelatedObjects.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.TagRelatedObjects" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetTagRelatedObjects" LabelKey="LabelMaxNumber" ToolTipFile="WidgetTagRelatedObjects" ToolTipKey="TooltipMaxNumber" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="20" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetTagRelatedObjects" LabelKey="LabelPager" ToolTipFile="WidgetTagRelatedObjects" ToolTipKey="TooltipPager" runat="server"></csb:LabelControl>
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
<asp:Panel ID="PnlOrderBy" runat="server" CssClass="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetTagRelatedObjects" LabelKey="LabelOrderBy" ToolTipFile="WidgetTagRelatedObjects" ToolTipKey="TooltipOrderBy" runat="server"></csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadComboBox ID="CbxOrderBy" runat="server"></telerik:RadComboBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</asp:Panel>   
