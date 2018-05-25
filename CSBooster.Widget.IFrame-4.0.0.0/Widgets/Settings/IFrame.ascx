<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="IFrame.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.IFrame" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="lbl1" EnableViewState="false" LanguageFile="WidgetIFrame" LabelKey="LabelUrl" ToolTipKey="TooltipUrl" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="TxtIFrameURL" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="lbl2" EnableViewState="false" LanguageFile="WidgetIFrame" LabelKey="LabelShowFrame" ToolTipKey="TooltipShowFrame" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbBorder" runat="server" Text="" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="lbl3" EnableViewState="false" LanguageFile="WidgetIFrame" LabelKey="LabelHeight" ToolTipKey="TooltipHeight" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbHeight" Width="120" runat="server" MinValue="100" MaxValue="1000" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" IncrementSettings-Step="50" />
    </div>
    <div class="inputBlockError">
    </div>
</div>