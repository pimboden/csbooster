<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SiteTagCloud.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.SiteTagCloud" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetSiteTagCloud" LabelKey="LabelMaxNumber" ToolTipKey="TooltipMaxNumber" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="100" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock" id="DivRelevance" runat="server">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetSiteTagCloud" LabelKey="LabelRelevance" ToolTipKey="TooltipRelevance" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="RblRelevance" runat="server">
            <asp:ListItem Selected="True" Value="0">unwichtig</asp:ListItem>
            <asp:ListItem Value="5">wichtiger</asp:ListItem>
            <asp:ListItem Value="3">sehr wichtig</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock" id="Div1" runat="server">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label3" LanguageFile="WidgetSiteTagCloud" LabelKey="LabelRelevanceType" ToolTipKey="TooltipRelevanceType" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:RadioButtonList ID="RblRelevanceType" runat="server">
            <asp:ListItem Selected="True" Value="1">ViewLog</asp:ListItem>
            <asp:ListItem Value="2">ObjectView</asp:ListItem>
            <asp:ListItem Value="3">RelatedObjects</asp:ListItem>
            <asp:ListItem Value="0">NoRelevance</asp:ListItem>            
        </asp:RadioButtonList>
    </div>
    <div class="inputBlockError">
    </div>
</div>
