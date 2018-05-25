<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CommunityLists.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.CommunityLists" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label1" LanguageFile="WidgetCommunityLists" LabelKey="LabelUserCount" ToolTipKey="TooltipUserCount" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbUsers" Width="120" runat="server" MinValue="1" MaxValue="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:LabelControl ID="Label2" LanguageFile="WidgetCommunityLists" LabelKey="LabelPager" ToolTipKey="TooltipPager" runat="server"></web:LabelControl>
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxPagerTop" runat="server" /><label for="<%=CbxPagerTop.ClientID %>" ><%=language.GetString("LabelPagerTop")%></label>  
        </div>
        <div>
            <asp:CheckBox ID="CbxPagerBottom" runat="server" /><label for="<%=CbxPagerBottom.ClientID %>" ><%=language.GetString("LabelPagerBottom")%></label>  
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>