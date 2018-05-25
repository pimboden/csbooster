<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ObjectListsProp.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.ObjectListsProp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetObjectLists" LabelKey="LabelMaxNumber" TooltipKey="TooltipMaxNumber" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="200" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label3" LanguageFile="WidgetObjectLists" LabelKey="LabelOrderBy" TooltipKey="TooltipOrderBy" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadComboBox ID="CbxOrderBy" runat="server" MaxHeight="80" />
        <telerik:RadComboBox ID="RcbOrderDir" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetObjectLists" LabelKey="LabelPager" TooltipKey="TooltipPager" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxPagerTop" runat="server" /><label for="<%=CbxPagerTop.ClientID %>"><%=language.GetString("LabelPagerTop")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxPagerBottom" runat="server" /><label for="<%=CbxPagerBottom.ClientID %>"><%=language.GetString("LabelPagerBottom")%></label>
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelControl2" LanguageFile="WidgetObjectLists" LabelKey="LabelAnonymous" TooltipKey="TooltipAnonymous" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxAnonymous" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<asp:Panel ID="PnlRenderHtml" runat="server" CssClass="inputBlock" Visible="false">
    <div class="inputBlockLabel">
        <web:LabelControl ID="LabelControl1" LanguageFile="WidgetObjectLists" LabelKey="LabelRenderHtml" TooltipKey="TooltipRenderHtml" runat="server">
        </web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <asp:CheckBox ID="CbxRenderHtml" runat="server" />
    </div>
    <div class="inputBlockError">
    </div>
</asp:Panel>
