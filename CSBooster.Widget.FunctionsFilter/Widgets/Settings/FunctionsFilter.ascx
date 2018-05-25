<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FunctionsFilter.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FunctionsFilter" %>
<%@ Register Assembly="CSBooster.WidgetControls" Namespace="_4screen.CSB.WidgetControls" TagPrefix="csb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="CSB_input_block">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label1" LabelFile="WidgetFunctionsFilter" LabelKey="LableFilterType" ToolTipFile="WidgetFunctionsFilter" ToolTipKey="TooltipFilter" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:CheckBox ID="CbxFilterType" runat="server" oncheckedchanged="CbxFilterType_CheckedChanged" AutoPostBack="True" />
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block" id="DivTagwords" runat="server">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label2" LabelFile="WidgetFunctionsFilter" LabelKey="LabelTagwords" ToolTipFile="WidgetFunctionsFilter" ToolTipKey="TooltipTagwords" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <asp:TextBox ID="txtTGL" runat="server" Width="450px" Height="100px" TextMode="MultiLine" />
            </div>
            <div class="CSB_error_cnt">
                <asp:RequiredFieldValidator ID="RfvtxtTGL" runat="server" ControlToValidate="txtTGL" Display="Dynamic"><%=language.GetString("MessageTagwordMust")%></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="CSB_input_block" id="DivMaxCount" runat="server">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label3" LabelFile="WidgetFunctionsFilter" LabelKey="LabelMaxCount" ToolTipFile="WidgetFunctionsFilter" ToolTipKey="TooltipMaxCount" runat="server">
                </csb:LabelControl>
            </div>
            <div class="CSB_input_cnt">
                <telerik:RadNumericTextBox ID="RntbMaxCount" Width="120" runat="server" MinValue="1" MaxValue="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
            </div>
            <div class="CSB_error_cnt">
            </div>
        </div>
        <div class="CSB_input_block" id="DivRelevance" runat="server">
            <div class="CSB_input_label">
                <csb:LabelControl ID="Label4" LabelFile="WidgetFunctionsFilter" LabelKey="LabelRelevance" ToolTipFile="WidgetFunctionsFilter" ToolTipKey="TooltipRelevance" runat="server">
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
    </ContentTemplate>
</asp:UpdatePanel>
     
