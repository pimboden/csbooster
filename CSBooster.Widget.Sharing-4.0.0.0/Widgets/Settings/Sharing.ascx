<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Sharing.ascx.cs" Inherits="_4screen.CSB.Widget.Settings.Sharing" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:labelcontrol id="Lbl1" languagefile="WidgetSharing" labelkey="LabelVisibility" tooltipkey="TooltipVisibility" runat="server">
        </web:labelcontrol>
    </div>
    <div class="inputBlockContent">
        <div>
            <asp:CheckBox ID="CbxShowExtSharing" runat="server" /><label for="<%=CbxShowExtSharing.ClientID %>"><%=language.GetString("LabelShowExtSharing")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxILike" runat="server" /><label for="<%=CbxILike.ClientID %>"><%=language.GetString("LabelShowILike")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxShowSendUrl" runat="server" /><label for="<%=CbxShowSendUrl.ClientID %>"><%=language.GetString("LabelShowSendUrl")%></label>
        </div>
        <div>
            <asp:CheckBox ID="CbxShowEmbedAndCopy" runat="server" /><label for="<%=CbxShowEmbedAndCopy.ClientID %>"><%=language.GetString("LabelShowEmbedAndCopy")%></label>
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:labelcontrol id="Lbl2" languagefile="WidgetSharing" labelkey="LabelEmbedWidth" tooltipkey="TooltipEmbedWidth" runat="server">
        </web:labelcontrol>
    </div>
    <div class="inputBlockContent">
        <div>
            <telerik:RadNumericTextBox ID="RntbEmbedWidth" Width="120" runat="server" MinValue="50" MaxValue="1000" IncrementSettings-Step="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
        </div>
    </div>
</div>
<div class="inputBlock">
    <div class="inputBlockLabel">
        <web:labelcontrol id="Lbl3" languagefile="WidgetSharing" labelkey="LabelEmbedHeight" tooltipkey="TooltipEmbedHeight" runat="server">
        </web:labelcontrol>
    </div>
    <div class="inputBlockContent">
        <div>
            <telerik:RadNumericTextBox ID="RntbEmbedHeight" Width="120" runat="server" MinValue="50" MaxValue="1000" IncrementSettings-Step="50" Type="Number" ShowSpinButtons="True" NumberFormat-DecimalDigits="0" />
        </div>
    </div>
    <div class="inputBlockError">
    </div>
</div>
