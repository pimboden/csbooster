<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SiteTagCloud.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.SiteTagCloud" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetSiteTagCloud" LabelKey="LabelMaxNumber" ToolTipFile="WidgetSiteTagCloud" ToolTipKey="TooltipMaxNumber" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="100" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block" id="DivRelevance" runat="server">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetSiteTagCloud" LabelKey="LabelRelevance" ToolTipFile="WidgetSiteTagCloud" ToolTipKey="TooltipRelevance" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:RadioButtonList ID="RblRelevance" runat="server">
            <asp:ListItem Selected="True" Value="0">unwichtig</asp:ListItem>
            <asp:ListItem Value="5">wichtiger</asp:ListItem>
            <asp:ListItem Value="3">sehr wichtig</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block" id="Div1" runat="server">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetSiteTagCloud" LabelKey="LabelRelevanceType" ToolTipFile="WidgetSiteTagCloud" ToolTipKey="TooltipRelevanceType" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:RadioButtonList ID="RblRelevanceType" runat="server">
            <asp:ListItem Selected="True" Value="1">ViewLog</asp:ListItem>
            <asp:ListItem Value="2">ObjectView</asp:ListItem>
            <asp:ListItem Value="3">RelatedObjects</asp:ListItem>
            <asp:ListItem Value="0">NoRelevance</asp:ListItem>            
        </asp:RadioButtonList>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
