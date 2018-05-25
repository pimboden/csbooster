<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FunctionsFilter.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.FunctionsFilter" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="inputBlock">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label1" LanguageFile="WidgetFunctionsFilter" LabelKey="LableFilterType" TooltipKey="TooltipFilter" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:CheckBox ID="CbxFilterType" runat="server" oncheckedchanged="CbxFilterType_CheckedChanged" AutoPostBack="True" />
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock" id="DivTagwords" runat="server">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label2" LanguageFile="WidgetFunctionsFilter" LabelKey="LabelTagwords" TooltipKey="TooltipTagwords" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <asp:TextBox ID="txtTGL" runat="server" Width="450px" Height="100px" TextMode="MultiLine" />
            </div>
            <div class="inputBlockError">
                <asp:RequiredFieldValidator ID="RfvtxtTGL" runat="server" ControlToValidate="txtTGL" Display="Dynamic"><%=language.GetString("MessageTagwordMust")%></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inputBlock" id="DivMaxCount" runat="server">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label3" LanguageFile="WidgetFunctionsFilter" LabelKey="LabelMaxCount" TooltipKey="TooltipMaxCount" runat="server">
                </web:LabelControl>
            </div>
            <div class="inputBlockContent">
                <telerik:RadNumericTextBox ID="RntbMaxCount" Width="120" runat="server" MinValue="1" MaxValue="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
            </div>
            <div class="inputBlockError">
            </div>
        </div>
        <div class="inputBlock" id="DivRelevance" runat="server">
            <div class="inputBlockLabel">
                <web:LabelControl ID="Label4" LanguageFile="WidgetFunctionsFilter" LabelKey="LabelRelevance" TooltipKey="TooltipRelevance" runat="server">
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
    </ContentTemplate>
</asp:UpdatePanel>
     
