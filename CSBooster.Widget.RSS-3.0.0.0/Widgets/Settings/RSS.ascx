<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RSS.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.RSS" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label1" LabelFile="WidgetRSS" LabelKey="LabelURL" ToolTipFile="WidgetRSS" ToolTipKey="TooltipURL" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:TextBox ID="txtURL" Width="99%" runat="server" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label2" LabelFile="WidgetRSS" LabelKey="LabelCount" ToolTipFile="WidgetRSS" ToolTipKey="TooltipCount" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadNumericTextBox ID="RntbFC" Width="60" runat="server" MinValue="1" MaxValue="100" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label3" LabelFile="WidgetRSS" LabelKey="LabelAbstract" ToolTipFile="WidgetRSS" ToolTipKey="TooltipAbstract" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <asp:CheckBox ID="cbxDesc" runat="server" Text="" />
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
<div class="CSB_input_block">
    <div class="CSB_input_label">
        <csb:LabelControl ID="Label4" LabelFile="WidgetRSS" LabelKey="LabelReload" ToolTipFile="WidgetRSS" ToolTipKey="TooltipReload" runat="server">
        </csb:LabelControl>
    </div>
    <div class="CSB_input_cnt">
        <telerik:RadComboBox ID="CbxFR" runat="server">
            <Items>
                <telerik:RadComboBoxItem Text="5 Sekunden" Value="5" />
                <telerik:RadComboBoxItem Text="15 Sekunden" Value="15" />
                <telerik:RadComboBoxItem Text="30 Sekunden" Value="30" />
                <telerik:RadComboBoxItem Text="1 Minute" Value="60" />
                <telerik:RadComboBoxItem Text="10 Minuten" Value="600" />
                <telerik:RadComboBoxItem Text="Nicht Aktualisieren" Value="0" />
            </Items>
        </telerik:RadComboBox>
    </div>
    <div class="CSB_error_cnt">
    </div>
</div>
