<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RSS.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.RSS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetRSS" LabelKey="LabelURL" ToolTipKey="TooltipURL" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:TextBox ID="txtURL" Width="99%" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetRSS" LabelKey="LabelCount" ToolTipKey="TooltipCount" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbFC" Width="60" runat="server" MinValue="1" MaxValue="100" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label3" LanguageFile="WidgetRSS" LabelKey="LabelAbstract" ToolTipKey="TooltipAbstract" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="cbxDesc" runat="server" Text="" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label4" LanguageFile="WidgetRSS" LabelKey="LabelReload" ToolTipKey="TooltipReload" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
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
    <div class="inputBlockError">
    </div>
</div>
