<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Sharing.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Sharing" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Lbl1" LabelFile="WidgetSharing" LabelKey="LabelVisibility" ToolTipFile="WidgetSharing" ToolTipKey="TooltipVisibility" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <asp:CheckBox ID="CbxShowExtSharing" runat="server" /><label for="<%=CbxShowExtSharing.ClientID %>"><%=language.GetString("LabelShowExtSharing")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxShowSendUrl" runat="server" /><label for="<%=CbxShowSendUrl.ClientID %>"><%=language.GetString("LabelShowSendUrl")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxShowEmbedAndCopy" runat="server" /><label for="<%=CbxShowEmbedAndCopy.ClientID %>"><%=language.GetString("LabelShowEmbedAndCopy")%></label>
        </div>
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Lbl2" LabelFile="WidgetSharing" LabelKey="LabelEmbedWidth" ToolTipFile="WidgetSharing" ToolTipKey="TooltipEmbedWidth" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <telerik:RadNumericTextBox ID="RntbEmbedWidth" Width="120" runat="server" MinValue="50" MaxValue="1000" IncrementSettings-Step="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
        </div>
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Lbl3" LabelFile="WidgetSharing" LabelKey="LabelEmbedHeight" ToolTipFile="WidgetSharing" ToolTipKey="TooltipEmbedHeight" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <div>
            <telerik:RadNumericTextBox ID="RntbEmbedHeight" Width="120" runat="server" MinValue="50" MaxValue="1000" IncrementSettings-Step="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
        </div>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
