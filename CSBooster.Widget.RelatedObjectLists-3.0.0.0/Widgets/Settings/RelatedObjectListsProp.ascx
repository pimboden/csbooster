<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="RelatedObjectListsProp.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.RelatedObjectListsProp" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelMaxNumber" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipMaxNumber" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="20" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<asp:Panel ID="PnlOrderBy" runat="server" CssClass="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelOrderBy" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipOrderBy" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadComboBox ID="CbxOrderBy" runat="server">
        </telerik:RadComboBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</asp:Panel>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelPager" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipPager" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <asp:CheckBox ID="CbxPagerTop" runat="server" /><label for="<%=CbxPagerTop.ClientID %>"><%=language.GetString("LabelPagerTop")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxPagerBottom" runat="server" /><label for="<%=CbxPagerBottom.ClientID %>"><%=language.GetString("LabelPagerBottom")%></label>
        </div>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="LabelControl2" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelAnonymous" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipAnonymous" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="CbxAnonymous" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<asp:Panel ID="PnlRenderHtml" runat="server" CssClass="CSB_input_block" Visible="false">
    <div class="CSB_input_label">
        <csb:LabelControl ID="LabelControl1" LabelFile="WidgetRelatedObjectLists" LabelKey="LabelRenderHtml" ToolTipFile="WidgetRelatedObjectLists" ToolTipKey="TooltipRenderHtml" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="CbxRenderHtml" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</asp:Panel>
