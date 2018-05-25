<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Statistics.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Statistics" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label1" LabelFile="WidgetStatistics" LabelKey="LabelSelectType" ToolTipFile="WidgetStatistics" ToolTipKey="TooltipSelectType" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <telerik:RadComboBox ID="CbxStatisticsType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CbxStatisticsType_OnSelectedIndexChanged">
                </telerik:RadComboBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label2" LabelFile="WidgetStatistics" LabelKey="LabelGroup" ToolTipFile="WidgetStatistics" ToolTipKey="TooltipGroup" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <telerik:RadComboBox ID="CbxGroupBy" runat="server">
                </telerik:RadComboBox>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label3" LabelFile="WidgetStatistics" LabelKey="LabelTime" ToolTipFile="WidgetStatistics" ToolTipKey="TooltipTime" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:RadioButtonList ID="RblInitTimeRange" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="aktueller Monat" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="letzter Monat" Value="2"></asp:ListItem>
                    <asp:ListItem Text="letzte 2 Monate" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
