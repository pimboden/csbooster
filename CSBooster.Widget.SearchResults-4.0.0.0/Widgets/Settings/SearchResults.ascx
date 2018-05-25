<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SearchResults.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.SearchResults" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetSearchResults" LabelKey="LabelCount" ToolTipKey="TooltipCount" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbRes" runat="server" MinValue="1" MaxValue="10" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>