<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Statistics.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Statistics" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label1" LanguageFile="WidgetStatistics" LabelKey="LabelSelectType" ToolTipKey="TooltipSelectType" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxStatisticsType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CbxStatisticsType_OnSelectedIndexChanged">
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label2" LanguageFile="WidgetStatistics" LabelKey="LabelGroup" ToolTipKey="TooltipGroup" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadComboBox ID="CbxGroupBy" runat="server">
                </telerik:RadComboBox>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label3" LanguageFile="WidgetStatistics" LabelKey="LabelTime" ToolTipKey="TooltipTime" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:RadioButtonList ID="RblInitTimeRange" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="aktueller Monat" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="letzter Monat" Value="2"></asp:ListItem>
                    <asp:ListItem Text="letzte 2 Monate" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="inputBlockError">
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
