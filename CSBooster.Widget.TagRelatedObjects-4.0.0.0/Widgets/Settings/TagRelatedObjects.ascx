<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="TagRelatedObjects.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.TagRelatedObjects" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:labelcontrol id="Label1" languagefile="WidgetTagRelatedObjects" labelkey="LabelMaxNumber" tooltipkey="TooltipMaxNumber" runat="server">
        </web:labelcontrol>
    </div>
    <div class="inputBlockContent">
        <telerik:RadNumericTextBox ID="RntbMaxNumber" Width="120" runat="server" MinValue="1" MaxValue="20" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
    </div>
    <div class="inputBlockError">
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:labelcontrol id="Label3" languagefile="WidgetTagRelatedObjects" labelkey="LabelOrderBy" tooltipkey="TooltipOrderBy" runat="server"></web:labelcontrol>
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
        <web:labelcontrol id="Label2" languagefile="WidgetTagRelatedObjects" labelkey="LabelPager" tooltipkey="TooltipPager" runat="server"></web:labelcontrol>
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
